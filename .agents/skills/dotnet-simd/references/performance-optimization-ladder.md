# Performance Optimization Ladder

Full recipe for maximizing single-core, then multi-core, throughput on a `Span<T>` element-wise operation. Apply steps in order; each is independent and compounds.

This ladder is for the scalar/remainder code paths inside a hand-written [Vector128 algorithm](vector128-vectorization.md), or for a genuinely custom reduction that [TensorPrimitives](tensor-primitives.md) doesn't expose — check both of those first.

## 1. Bounds-Check Elimination

C# checks span/array bounds on every indexed access. Use `MemoryMarshal.GetReference()` + `Unsafe.Add()` to index without a bounds check:

```csharp
static void For<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
    where T : struct, IAdditionOperators<T, T, T>
{
    ref var leftRef = ref MemoryMarshal.GetReference(left);
    ref var rightRef = ref MemoryMarshal.GetReference(right);
    ref var destRef = ref MemoryMarshal.GetReference(destination);

    for (var index = 0; index < left.Length; index++)
        Unsafe.Add(ref destRef, index) = Unsafe.Add(ref leftRef, index) + Unsafe.Add(ref rightRef, index);
}
```

Caller is responsible for verifying spans are equal length beforehand — `Unsafe.Add` performs no validation.

## 2. Branch Removal

A CPU branch misprediction costs ~10-20 cycles. When a loop body contains data-dependent `if`, convert the boolean to a multiplier instead of branching:

```csharp
var predicateValue = predicate(item);
sum += item * Unsafe.As<bool, byte>(ref predicateValue);   // false→0, true→1
```

This assumes `bool` is represented as a single byte (true in the current .NET runtime). Net effect: `0` is added when the predicate is false, the item itself when true — no branch. Worthwhile when predicate outcomes are unpredictable (e.g., data-dependent); if the CPU can predict the branch well already, this can be a net loss on tiny collections.

## 3. Multi-Accumulator Unrolling (CPU-level parallelism)

Modern CPUs can execute independent arithmetic ops concurrently within one core. Splitting a reduction across 2+ independent accumulator variables lets the CPU overlap their execution:

```csharp
var sum0 = 0; var sum1 = 0;
for (var index = 0; index < source.Length - 1; index += 2)
{
    sum0 += source[index];
    sum1 += source[index + 1];
}
var isOdd = (source.Length & 1) is not 0;
return sum0 + sum1 + (Unsafe.As<bool, byte>(ref isOdd) * source[^1]);
```

For element-wise (non-reduction) ops, unroll 4-wide and use a `switch` on the remainder count (3/2/1) instead of another loop, to minimize branching on the tail:

```csharp
var end = left.Length - 3;
for (; index < end; index += 4)
{
    Unsafe.Add(ref destRef, index)     = Unsafe.Add(ref leftRef, index)     + Unsafe.Add(ref rightRef, index);
    Unsafe.Add(ref destRef, index + 1) = Unsafe.Add(ref leftRef, index + 1) + Unsafe.Add(ref rightRef, index + 1);
    Unsafe.Add(ref destRef, index + 2) = Unsafe.Add(ref leftRef, index + 2) + Unsafe.Add(ref rightRef, index + 2);
    Unsafe.Add(ref destRef, index + 3) = Unsafe.Add(ref leftRef, index + 3) + Unsafe.Add(ref rightRef, index + 3);
}
switch (left.Length - index)
{
    case 3: /* handle index, index+1, index+2 */ break;
    case 2: /* handle index, index+1 */ break;
    case 1: /* handle index */ break;
}
```

## 4. SIMD

See [vector128-vectorization.md](vector128-vectorization.md) (recommended starting point) or [vector-t-and-fixed-shape.md](vector-t-and-fixed-shape.md) for the simpler, portable alternative. Use the unrolled scalar loop from step 3 to process the SIMD remainder, passing the vectorized index as the starting offset.

## 5. Multi-Core Parallelization

`Parallel.For(0, length, i => ...)` passes a lambda per-element — this prevents SIMD inside the lambda and adds per-iteration overhead. Instead, slice the data into per-core chunks and run one SIMD-optimized call per chunk with `Parallel.Invoke`:

```csharp
const int minChunkCount = 4;
const int minChunkSize = 1_000;

static void ParallelApply<T>(ReadOnlyMemory<T> left, ReadOnlyMemory<T> right, Memory<T> destination)
    where T : struct, IAdditionOperators<T, T, T>
{
    var coreCount = Environment.ProcessorCount;
    if (coreCount < minChunkCount || left.Length <= minChunkCount * minChunkSize)
    {
        TensorPrimitives.Add(left.Span, right.Span, destination.Span);  // too small: single-threaded
        return;
    }

    var chunkSize = int.Max(left.Length / coreCount, minChunkSize);
    var actions = new Action[left.Length / chunkSize];
    var start = 0;
    for (var i = 0; i < actions.Length; i++)
    {
        var length = (i == actions.Length - 1) ? left.Length - start : chunkSize;
        var l = left.Slice(start, length); var r = right.Slice(start, length); var d = destination.Slice(start, length);
        actions[i] = () => TensorPrimitives.Add(l.Span, r.Span, d.Span);
        start += length;
    }
    Parallel.Invoke(actions);
}
```

Parameters are `Memory<T>`, not `Span<T>` — `Span<T>` is a `ref struct` and cannot be captured in a closure. Gate parallelization behind `minChunkCount`/`minChunkSize` thresholds: thread overhead outweighs gains on small spans.
