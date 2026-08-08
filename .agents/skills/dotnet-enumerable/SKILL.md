---
name: dotnet-enumerable
description: "Write and optimize C#/.NET enumerables, iterators, and LINQ usage. USE FOR: choosing IEnumerable<T>/IReadOnlyCollection<T>/IReadOnlyList<T>/ICollection<T>/IList<T>/IAsyncEnumerable<T> for params/returns; foreach codegen and value-type vs reference-type enumerator performance (List<T>, arrays, Span<T>, ImmutableArray<T>, ArraySegment<T>); avoiding boxed enumerator allocations; Count() vs Any() vs Count property complexity traps; First()/Single()(OrDefault) cost and predicate-overload collapsing; ToList()/ToArray() allocation cost vs lazy evaluation; returning Enumerable.Empty<T>()/yield break instead of null; writing yield-based iterators, lazy validation, coroutines, behavior trees; writing async iterators with await foreach/yield return, EnumeratorCancellation/WithCancellation token propagation, ConfigureAwait; handling enumerables via reflection/Roslyn (NetFabric.Reflection/CodeAnalysis). DO NOT USE FOR: SIMD/vectorized span math (dotnet-simd); generic math over numeric types (dotnet-generic-math)."
---

# .NET Enumerables & Iteration Performance

Core namespaces: `System.Collections.Generic` (`IEnumerable<T>`, `IReadOnlyList<T>`, ...), `System.Linq`.

## Interface Cheat Sheet

| Interface | Adds over parent | Cost profile |
|---|---|---|
| `IEnumerable<T>` | `GetEnumerator()` | sequential-only; `Count()` is O(n), `Any()` is O(1) |
| `IReadOnlyCollection<T>` | `Count` | O(1) count without enumerating |
| `IReadOnlyList<T>` | `this[int]` | random access; `for` + indexer avoids enumerator allocation |
| `ICollection<T>` / `IList<T>` | mutation (`Add`, `this[int] set`, ...) | backwards-compatible mutable counterparts, don't derive from the `IReadOnly*` pair |
| `IAsyncEnumerable<T>` | `GetAsyncEnumerator()` | async pull stream, use with `await foreach` |

Expose the most-capable interface a return type can honestly provide; consume the least-capable interface an algorithm needs. Never return `null` for "empty" — use `Enumerable.Empty<T>()` or `yield break`. Details: [references/enumerable-interfaces.md](references/enumerable-interfaces.md). Writing async iterators: [references/custom-iterators.md](references/custom-iterators.md#async-iterators). Consuming them — cancellation (`WithCancellation`/`[EnumeratorCancellation]`), `ConfigureAwait`: [references/async-enumeration.md](references/async-enumeration.md).

## Performance Quick Rules

| Rule | Why |
|---|---|
| Prefer `Any()` over `Count() == 0` | `Any()` calls `MoveNext()` once — O(1); `Count()` enumerates fully unless the source implements `ICollection` |
| Don't cast arrays/`List<T>` to `IEnumerable<T>` in hot paths | their value-type enumerators get boxed to reference types — heap allocation + virtual `MoveNext()`/`Current` calls |
| Reuse LINQ query variables instead of `.ToList()` mid-pipeline | keeps lazy evaluation; `ToList()`/`ToArray()` force an allocation + full copy |
| Use `First(predicate)` / `Count(predicate)` overloads | one enumerator instead of `Where(predicate).First()` |
| Avoid `Single()`/`SingleOrDefault()` in production hot paths | it enumerates to the end even on the first match, just to prove uniqueness |
| Use `CollectionsMarshal.AsSpan(list)` for hot-path `List<T>` iteration | random-access span over the internal array, bypassing `List<T>.Enumerator` — matches array/`Span<T>` speed; never resize the list while the span is alive |
| Prefer `foreach` over `for` when iterating a field/property/method-returned array or span | `foreach` caches the reference into a local once, enabling reliable bounds-check elimination; `for` re-reads the field/property each iteration |

Full benchmark data for arrays/`Span<T>`/`List<T>`/`ImmutableArray<T>`/`ArraySegment<T>`: [references/iteration-performance.md](references/iteration-performance.md). `Count`/`Any`/`First`/`Single`/`ToList` complexity: [references/linq-patterns.md](references/linq-patterns.md).

## Quick Pattern: `yield` for Lazy Sequences

```csharp
public static IEnumerable<int> Range(int start, int count)
{
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
    return GetEnumerable(start, count);   // validate eagerly, iterate lazily

    static IEnumerable<int> GetEnumerable(int start, int count)
    {
        var end = start + count;
        for (var value = start; value < end; value++)
            yield return value;
    }
}
```

Splitting eager validation from the `yield`-based local function avoids the surprise of an exception only surfacing on the first `MoveNext()` call. Details, `yield break`, and enumerable-vs-enumerator return choice: [references/custom-iterators.md](references/custom-iterators.md).

## Reference Files

| File | Load When |
|------|-----------|
| [references/enumerable-interfaces.md](references/enumerable-interfaces.md) | Choosing which collection interface to expose/consume; `IEnumerable`/`IEnumerator`, `IReadOnlyCollection<T>`/`IReadOnlyList<T>`, `ICollection<T>`/`IList<T>`, `IAsyncEnumerable<T>` semantics |
| [references/iteration-performance.md](references/iteration-performance.md) | `foreach` codegen, value-type vs reference-type enumerators, array/`Span<T>`/`List<T>`/`ImmutableArray<T>`/`ArraySegment<T>` iteration benchmarks, bounds-check elimination, `CollectionsMarshal.AsSpan` |
| [references/linq-patterns.md](references/linq-patterns.md) | `Count()`/`Any()`, `First()`/`Single()`(`OrDefault`), `ToList()`/`ToArray()` cost, query composition & lazy evaluation, `null` vs `Enumerable.Empty<T>()`, `IQueryable`, why LINQ operators don't apply to `IAsyncEnumerable<T>` |
| [references/custom-iterators.md](references/custom-iterators.md) | Writing `yield`-based iterators (sync and async), lazy-evaluation pitfalls, enumerable vs enumerator return types, coroutines, behavior trees |
| [references/async-enumeration.md](references/async-enumeration.md) | Consuming `IAsyncEnumerable<T>` with `await foreach`, `foreach` vs `await foreach` diagnostics, cancellation (`[EnumeratorCancellation]`, `WithCancellation`), `ConfigureAwait` on async streams, `IAsyncEnumerable<T>` vs `Task<IReadOnlyList<T>>` |
| [references/tooling.md](references/tooling.md) | Detecting/handling enumerables via reflection (`NetFabric.Reflection`) or Roslyn (`NetFabric.CodeAnalysis`), expression-tree `foreach` generation, testing enumerables |
