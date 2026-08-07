# Vector128 / Vector256 / Vector512 (System.Runtime.Intrinsics)

Only reach for this once [TensorPrimitives](tensor-primitives.md) doesn't cover the operation. `Vector128<T>` is the **recommended starting point for new hand-written vectorized algorithms** — it's the common denominator across every platform that supports vectorization.

> `Vector<T>` ([vector-t-and-fixed-shape.md](vector-t-and-fixed-shape.md)) is simpler to write — one generic type, no width-specific code — and is a reasonable choice for a quick portable loop. But `Vector128`/`256`/`512` gives fine-grained control over width, lane layout, and the full `System.Runtime.Intrinsics` op set, and is what official guidance recommends starting new hand-written vectorization with. Prefer `Vector<T>` only when the simplicity is worth more than that control for your case.

## Widths

`Vector128<T>` holds 16 bytes: 8 shorts, 4 ints/floats, or 2 longs/doubles. `Vector256<T>` is twice as wide, `Vector512<T>` twice again. Each width has a generic type (the data) and a matching non-generic static class (`Vector128`, `Vector256`, `Vector512`) holding factory methods and operations.

Prefer operators (`+`, `&`, `<<`) over named-method equivalents for readability and to avoid precedence bugs.

Note: on x86/x64, `Vector256<T>` is generally two independent 128-bit lanes internally. Element-wise ops (`+`, `*`, ...) are transparent across this, but **lane-crossing** ops (shuffles, horizontal/pairwise reductions) don't just "widen for free" — see [Lane-Crossing Pitfall](#lane-crossing-pitfall) below.

## Hardware Detection

- `Vector128.IsHardwareAccelerated` / `Vector256.IsHardwareAccelerated` / `Vector512.IsHardwareAccelerated` — JIT-time constants; check the width you actually use (accelerated `Vector256` usually, but not always, implies accelerated `Vector128`).
- `Vector128<T>.IsSupported` — whether element type `T` is valid for that width (`byte`, `sbyte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`, `float`, `double`, `nint`, `nuint` today).
- `Vector128<T>.Count` — elements per vector; also a JIT-time constant.

Don't cache any of these — read them directly where needed; the JIT folds them to constants and eliminates unreachable branches.

`char`/`bool` aren't directly supported — reinterpret via `MemoryMarshal.Cast` (span) or `.As<TFrom,TTo>()` (vector already held), keeping the underlying bits valid for the reinterpreted type.

## Structure the Code Path

Fan out into a path per vector width plus a scalar fallback, checking the **widest** vector first:

```csharp
public static T Sum<T>(ReadOnlySpan<T> buffer) where T : unmanaged, INumberBase<T>
{
    // Vector512 / Vector256 blocks go here first, identical in shape to the Vector128
    // block below but using the wider type — omitted for brevity.

    if (Vector128.IsHardwareAccelerated && Vector128<T>.IsSupported)
    {
        return buffer.Length >= Vector128<T>.Count
            ? SumVector128(buffer)
            : SumVectorSmall(buffer);   // too small for even the narrowest vector
    }

    return SumScalar(buffer);          // no vectorization available at all
}
```

`sizeof(T)` is also a JIT-time constant, so a small-input fallback can dispatch on element width to a `switch` jump table sized for the widest vector's worth of elements — the same technique `TensorPrimitives` uses internally.

## Remainder Handling

The robust way to handle a tail smaller than one vector: reprocess the **last full vector**, overlapping elements already processed, instead of falling out to a separate scalar loop.

- **Non-idempotent** operations (e.g. sum) would double-count the overlap — mask the tail down to the operation's additive identity with `ConditionalSelect` before folding it in.
- **Idempotent** operations (e.g. a search) can fold the overlap in directly, no mask needed.

```csharp
// Non-idempotent example: masked overlap so elements aren't double-counted.
Vector128<T> end = Vector128.Create(buffer.Slice(buffer.Length - Vector128<T>.Count));
// ...accumulate full vectors via a do/while loop...
(int blocks, int trailing) = Math.DivRem(buffer.Length, Vector128<T>.Count);
Vector128<T> mask = CreateRemainderMask128<T>(trailing);   // last `trailing` lanes all-bits-set
sum += Vector128.ConditionalSelect(mask, end, Vector128<T>.Zero);
return Vector128.Sum(sum);
```

```csharp
// Idempotent example: no mask, safe to re-scan the overlap.
public static bool Contains(ReadOnlySpan<int> buffer, int searched)
{
    var needle = Vector128.Create(searched);
    var remaining = buffer;
    while (remaining.Length >= Vector128<int>.Count)
    {
        if (Vector128.EqualsAny(Vector128.Create(remaining), needle)) return true;
        remaining = remaining.Slice(Vector128<int>.Count);
    }
    if (buffer.Length >= Vector128<int>.Count)
        return Vector128.EqualsAny(Vector128.Create(buffer.Slice(buffer.Length - Vector128<int>.Count)), needle);
    foreach (int value in remaining) if (value == searched) return true;
    return false;
}
```

Mishandling the remainder is a common source of bugs (out-of-bounds reads are non-deterministic and can crash) — always test buffer lengths that aren't an exact multiple of the vector width, plus lengths smaller than one vector.

## Lane-Crossing Pitfall

Element-wise ops (`v1 + v2`) combine same-index elements regardless of width. A **pairwise/horizontal reduction** combines *adjacent* elements, so widening changes what's paired — a naive two-round `HorizontalAdd` on `Vector256<float>` gives the lower lane's sum broadcast in the lower lane and the upper lane's sum broadcast in the upper lane, **not** the total. Bridge the lane boundary explicitly:

```csharp
Vector256<float> step2 = Avx.HorizontalAdd(Avx.HorizontalAdd(v, v), Avx.HorizontalAdd(v, v));
float total = step2.GetLower().ToScalar() + step2.GetUpper().ToScalar();   // must add the two lanes
```

Confirm with a benchmark before assuming a wider vector speeds up a lane-crossing algorithm — it isn't "free" the way element-wise ops are.

## Load and Store Safely

`Vector128.Create(span)` / `.CopyTo(span)` are the simplest way to move data and stay JIT-efficient for most code. For walking a buffer by managed reference, prefer `LoadUnsafe`/`StoreUnsafe` (managed reference + `nuint` offset) over pointer-based `Load`/`Store` (which require pinning) or raw reference arithmetic.

Get the starting reference from `MemoryMarshal.GetReference` (or `GetArrayDataReference` for arrays), not `ref span[0]`, so empty buffers don't throw.

**Warning:** offset arithmetic uses unsigned `nuint`. Always check the buffer length before computing an offset like `buffer.Length - Vector128<int>.Count` — if the buffer is smaller than one vector, that subtraction underflows to a huge value and the read goes out of bounds.

## Common Operations

| Category | Representative APIs |
|--|--|
| Creation | `Create`, `CreateScalar`, `Create(ReadOnlySpan<T>)`, `CreateSequence` |
| Load/store | `Load`, `LoadUnsafe`, `LoadAligned`, `Store`, `StoreUnsafe`, `CopyTo` |
| Arithmetic | `Add`, `Subtract`, `Multiply`, `Divide`, `Abs`, `Sqrt`, `FusedMultiplyAdd`, `Sum` |
| Bit ops | `BitwiseAnd`, `BitwiseOr`, `Xor`, `ShiftLeft`, `ShiftRightArithmetic` |
| Min/max/clamp | `Min`, `Max`, `Clamp` |
| Comparison | `Equals`, `GreaterThan`, ... (returns a mask vector, not `bool`) |
| Comparison reductions | `EqualsAll`, `EqualsAny`, `GreaterThanAll`, ... (collapses to a single `bool`) |
| Selection | `ConditionalSelect(mask, x, y)` — blend two vectors bit by bit |
| Reorder | `Shuffle`, `Reverse`, `Zip`, `Unzip` |
| Lane access | `GetElement`, `WithElement`, `ToScalar`, `GetLower`, `GetUpper`, `ToVector256` |
| Conversion | `ConvertToInt32`, `ConvertToSingle`, ... |

Every operation has a software fallback on platforms that can't accelerate it. `Estimate`/`Native` variants (`MultiplyAddEstimate`, `ClampNative`, `ShuffleNative`, ...) trade precision or an IEEE edge-case guarantee for speed — use only when a benchmark shows the exact form is the bottleneck.

See [hardware-intrinsics.md](hardware-intrinsics.md) for platform-specific instructions (`System.Runtime.Intrinsics.X86`/`.Arm`/`.Wasm`) when `Vector128`/`256` don't expose what you need.
