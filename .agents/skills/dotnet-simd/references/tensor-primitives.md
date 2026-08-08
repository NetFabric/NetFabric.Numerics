# TensorPrimitives (System.Numerics.Tensors)

Default entry point for vectorized math over spans — reach for this before writing any vector code by hand. NuGet package `System.Numerics.Tensors`, namespace `System.Numerics.Tensors`.

> "Reach for the existing higher-level APIs first. `Span<T>`, `string`, LINQ, `TensorPrimitives`, and the tensor types already accelerate many common operations for you — don't hand-roll what's already optimized and tested." — .NET SIMD guidance

## Coverage

- .NET 8: ~40 overloads, `float` only.
- .NET 9+: ~200 overloads, generic over any `T` implementing the relevant generic-math interface (`INumber<T>`, `IRootFunctions<T>`, etc.) — covers `float`, `double`, `Half`, and custom numeric types.
- Element-wise arithmetic (`Add`, `Multiply`, `Subtract`, `Divide`, ...), math functions mirroring `Math`/`MathF`, and reductions (`Sum`, `Max`, `IndexOfMax`, `Dot`, `CosineSimilarity`, ...).
- Internally SIMD-optimized (`Vector128`/`Vector256`/`Vector512`-based) — you get acceleration without writing any vector code.

## Usage

```csharp
public static float[] MultiplyAdd(float[] left, float[] right, float[] addend)
{
    var result = new float[left.Length];
    TensorPrimitives.Multiply(left, right, result);   // result = left * right
    TensorPrimitives.Add(result, addend, result);     // result += addend (in-place)
    return result;
}

public static float CosineSimilarity(float[] left, float[] right)
    => TensorPrimitives.CosineSimilarity(left, right);
```

Destination can equal a source for in-place operations. Overlapping source/destination that don't start at the same location throw `ArgumentException`.

## Tensor<T> (experimental, .NET 9+)

Builds on `TensorPrimitives`; adds indexing/slicing over multi-dimensional data and zero-copy interop with ML.NET, TorchSharp, and ONNX Runtime. Use when you need actual multi-dimensional tensor semantics, not just span math — `TensorPrimitives` alone is sufficient for flat element-wise/reduction operations.

## When TensorPrimitives Isn't Enough

`TensorPrimitives` has no custom-operator extension point — you can only compose existing overloads (each pass reads/writes the whole span, so chaining several costs extra memory traffic vs. a single fused loop). Step down to hand-written [Vector128/256/512](vector128-vectorization.md) only when:

- The exact operation (or fusion of operations) you need has no `TensorPrimitives` overload, **and**
- A benchmark confirms composing existing `TensorPrimitives` calls (or a plain scalar loop) is a measured bottleneck.

Don't jump to hand-written SIMD "for performance" without both conditions — `TensorPrimitives` already reflects a heavily-optimized, hardware-tuned implementation that's easy to get wrong when reproduced by hand.
