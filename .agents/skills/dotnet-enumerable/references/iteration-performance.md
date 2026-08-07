# foreach Codegen & Iteration Benchmarks

## foreach Lowering

`foreach` desugars to a `while` loop calling `GetEnumerator()`, `MoveNext()`, `Current`, wrapped in `try`/`finally` disposing the enumerator (generic `IEnumerator<T>` only — non-generic `IEnumerator` is disposed conditionally via an `as IDisposable` check):

```csharp
IEnumerator<int> enumerator = source.GetEnumerator();
try
{
    while (enumerator.MoveNext())
        Console.WriteLine(enumerator.Current);
}
finally { enumerator?.Dispose(); }
```

## Value-Type vs Reference-Type Enumerators

If `GetEnumerator()`'s static return type is `IEnumerable`/`IEnumerable<T>` (an interface), the enumerator is a **reference type** — `MoveNext()`/`Current` are `callvirt` (virtual dispatch), and if the concrete enumerator were a struct it gets boxed onto the heap.

If the collection exposes a `GetEnumerator()` that returns a concrete `struct` (e.g. `List<int>.Enumerator`), and the code doesn't box it to an interface, the JIT emits direct `call` instructions — no virtual dispatch, no heap allocation. All BCL collections do this; if you implement a new collection type, do it too.

Benchmark (`foreach` over `List<int>` cast to `IEnumerable<int>` vs used directly), 100 & 10,000 items, .NET 6/7/8:

| Path | Result |
|---|---|
| `IEnumerable<int>` (reference-type enumerator) | 300–700% slower on x64, ~450% slower on Arm64; heap allocation per enumeration |
| `List<int>` (value-type enumerator) | baseline; zero heap allocation |

**Rule**: avoid casting a concrete collection to an interface right before iterating it in a hot path. On public APIs, prefer concrete types or immutable collections that keep value-type enumerators when possible.

## Array Iteration

`for` and `foreach` both compile to a `while` loop. When the array/span being iterated is **already a local variable**, the JIT recognizes the classic `for (int i = 0; i < arr.Length; i++) Use(arr[i]);` idiom and eliminates the bounds check on `arr[i]` on its own — `for` and `foreach` perform the same in this case (confirmed as of .NET 8; earlier runtimes were less consistent).

The gap opens up when the collection expression is **not** already a plain local — a field (`this.array`), a property (`someObj.Items`), or the result of a method call (e.g. `memory.Span`). A `for` loop re-evaluates that expression's length on every iteration (`i < this.array.Length` reloads the field each time), and the JIT must be conservative about whether the value could change between reads — especially if the loop body contains a call, since it can't always prove the callee doesn't mutate the field. `foreach` sidesteps this: it captures the array **reference** (or, for `Span<T>`, the reference+length pair via `Span<T>.Enumerator`) into a compiler-generated local exactly once, before the loop starts, so the JIT reasons about a true local for the rest of the loop and reliably elides the bounds check. Net effect: `foreach` over a field/property-backed array or span can be ~30% faster than the manual indexed `for` loop reading the same field/property each iteration.

**Rule**: iterating a local array/span — `for` and `foreach` are equivalent, pick whichever reads better. Iterating a field, property, or method result in a hot loop — either `foreach` it directly, or cache it to a local once before a `for` loop (`var local = this.array; for (...) local[i]`); both avoid the repeated re-evaluation.

Slicing: convert to `ReadOnlySpan<T>` via `.AsSpan().Slice(start, length)` and `foreach` that — same benefit, ~20% faster than a manually indexed sub-range `for` loop.

## ArraySegment\<T\>

Unlike arrays/spans, the C# compiler does **not** special-case `ArraySegment<T>` — `foreach` allocates its (value-type) enumerator and uses it, rather than the indexer. Benchmarks (.NET 6/7/8):

| Approach | vs. indexed `for` on the segment |
|---|---|
| `foreach` on `ArraySegment<T>` | ~1.3x slower — enumerator's `Current` does two bounds checks vs the indexer's one |
| `for` + segment indexer | baseline |
| `for` on `.Array` using `.Offset`/`.Count` directly, or `.AsSpan()` + `foreach` | 1.6–2x **faster** — array reference is copied locally, enabling bounds-check elimination like a plain array |
| LINQ `Sum()` on the segment | ~10x slower (3–5x on .NET 8) — boxes the enumerator to a reference type on top of the double bounds check |
| `.Array!.Skip(offset).Take(count).Sum()` | 8–20x slower — stacks multiple enumerators |

**Rule**: given an `ArraySegment<T>`, prefer `.AsSpan()` (or manual `.Array`/`.Offset`/`.Count` indexing) over `foreach`ing the segment directly, and avoid LINQ on it.

## ImmutableArray\<T\>

`ImmutableArray<T>.Enumerator` is a value type, so a naive read of the IL suggests it should perform like `List<T>` (value-type enumerator, still two method calls per item). Surprising finding: **since JIT improvements around .NET 8, the generated machine code for `foreach` on `ImmutableArray<T>` is nearly identical to a plain array or `ReadOnlySpan<T>`** — not to `List<T>`. Confirmed via disassembly diagnoser across .NET 6 → .NET 8: `ImmutableArray<T>` iteration is as fast as array iteration, a pure JIT-level win requiring no code changes, only a runtime upgrade.

## `List<T>` via `CollectionsMarshal.AsSpan`

`System.Runtime.InteropServices.CollectionsMarshal.AsSpan<T>(List<T>?)` returns a `Span<T>` over `List<T>`'s internal backing array, bypassing `List<T>.Enumerator` entirely — indexed access into the span hits the array directly, matching plain-array/`Span<T>` iteration speed instead of the two-call-per-item enumerator path.

```csharp
var span = CollectionsMarshal.AsSpan(list);
for (var index = 0; index < span.Length; index++)
    Process(span[index]);
```

Constraints:
- The span's length is `list.Count`, not its capacity — extra allocated-but-unused capacity isn't exposed.
- **Never add or remove items while the span is in use.** A `List<T>` resize reallocates the backing array, silently detaching the span from the list — reads/writes through the stale span become invisible to the list or, worse, corrupt an array no one references anymore.
- The span aliases the list's storage, so writes through it mutate the list's elements in place (useful for in-place transforms without `list[i] = ...` indexer calls).
- Prefer this only in hot paths iterating large `List<T>` instances; for small lists or one-off iteration, the readability cost isn't worth it — plain `foreach` is already allocation-free.

## Practical Ranking (fastest → slowest, typical int collections)

1. `for`/`foreach` on `int[]`, `ReadOnlySpan<int>`, or `ImmutableArray<int>` (.NET 8+); `for` over `CollectionsMarshal.AsSpan(list)` — all roughly equivalent
2. `foreach` on `List<int>` (value-type enumerator, two calls per item, no bounds-check elision)
3. `for` + indexer on `ArraySegment<int>`
4. `foreach` on `ArraySegment<int>` (enumerator, double bounds check)
5. Anything cast to `IEnumerable<int>` before iterating — reference-type enumerator, heap allocation, virtual calls

## Why This Doesn't Apply to `IAsyncEnumerable<T>`

An async iterator method already compiles to a heap-allocated state machine (needed to suspend/resume across `await` points), so there's no value-type-enumerator-vs-boxing trade-off to make: `await foreach` always dispatches through the `IAsyncEnumerator<T>` interface, and that one allocation is unavoidable regardless of how the sequence is produced. Optimize async streams by reducing *how much* is awaited and allocated per item (batch I/O, avoid `ConfigureAwait` capturing you don't need — see [async-enumeration.md](async-enumeration.md)), not by chasing struct enumerators.
