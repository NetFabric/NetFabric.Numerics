---
name: dotnet-simd
description: "Vectorize hot loops in C#/.NET the right way — reach for System.Numerics.Tensors (TensorPrimitives) first, then System.Runtime.Intrinsics (Vector128/256/512) for hand-written algorithms, hardware intrinsics only as a last resort. USE FOR: choosing between TensorPrimitives, Vector<T>, Vector128/256/512, and X86/Arm/Wasm intrinsics; MemoryMarshal/LoadUnsafe/StoreUnsafe span-to-vector patterns; hardware-acceleration checks (IsHardwareAccelerated, IsSupported); widest-vector-first code paths with scalar/small-input fallbacks; masked remainder handling for non-idempotent vs idempotent ops; lane-crossing pitfalls with Vector256/512 horizontal reductions; eliminating bounds checks/branches in scalar/remainder code; multi-core parallelization via Parallel.Invoke chunking over TensorPrimitives calls; testing SIMD paths via DOTNET_Enable* env vars and .runsettings; benchmarking with BenchmarkDotNet job configs. DO NOT USE FOR: GPU compute (CUDA/TorchSharp); SIMD in non-.NET languages."
---

# .NET SIMD & Vectorization

Requires `using System.Numerics;` / `using System.Runtime.Intrinsics;` / `using System.Numerics.Tensors;` (`TensorPrimitives`, package `System.Numerics.Tensors`) depending on the step below.

## Decision Ladder

Only escalate to the next step when a benchmark shows the current one doesn't cover your case — most code should stop at step 1.

| Step | API | When |
|------|-----|------|
| 1 | `TensorPrimitives` ([tensor-primitives.md](references/tensor-primitives.md)) | Default choice — ~200 ready-made vectorized span operations (arithmetic, math functions, reductions, cosine similarity, ...). Don't hand-roll what's already optimized and tested. |
| 2 | `Vector128<T>`/`Vector256<T>`/`Vector512<T>` ([vector128-vectorization.md](references/vector128-vectorization.md)) | No `TensorPrimitives` overload covers your exact operation. **Recommended starting point** for hand-written vectorization — broadest hardware support, fine-grained control. |
| 3 | `Vector<T>` ([vector-t-and-fixed-shape.md](references/vector-t-and-fixed-shape.md)) | A quick, simple portable loop where per-width control isn't worth the complexity. Simpler to write than step 2, but less control. |
| 4 | Platform intrinsics — `System.Runtime.Intrinsics.X86`/`.Arm`/`.Wasm` ([hardware-intrinsics.md](references/hardware-intrinsics.md)) | A specific processor instruction measurably beats what Vector128/256 generate, on a proven hot path. |

Steps 1–2 handle almost everything. General CPU-level techniques for the scalar/remainder code inside a hand-written algorithm (bounds-check elimination, branch removal, multi-accumulator unrolling) and multi-core chunking live in [performance-optimization-ladder.md](references/performance-optimization-ladder.md).

## Quick Pattern: Reach for TensorPrimitives First

```csharp
public static float[] MultiplyAdd(float[] left, float[] right, float[] addend)
{
    var result = new float[left.Length];
    TensorPrimitives.Multiply(left, right, result);   // result = left * right
    TensorPrimitives.Add(result, addend, result);     // result += addend (in-place)
    return result;
}
```

Only drop to hand-written `Vector128<T>` (see [vector128-vectorization.md](references/vector128-vectorization.md)) when no `TensorPrimitives` overload exists for your exact operation:

```csharp
public static T Sum<T>(ReadOnlySpan<T> buffer) where T : unmanaged, INumberBase<T>
{
    if (Vector128.IsHardwareAccelerated && Vector128<T>.IsSupported)
        return buffer.Length >= Vector128<T>.Count ? SumVector128(buffer) : SumVectorSmall(buffer);
    return SumScalar(buffer);
}
```

## Reference Files

| File | Load When |
|------|-----------|
| [references/tensor-primitives.md](references/tensor-primitives.md) | Using `TensorPrimitives`/`Tensor<T>`; deciding when it's not enough |
| [references/vector128-vectorization.md](references/vector128-vectorization.md) | Hand-writing `Vector128`/`256`/`512` code, hardware-acceleration checks, remainder handling, lane-crossing pitfalls, safe load/store |
| [references/hardware-intrinsics.md](references/hardware-intrinsics.md) | Platform-specific `X86`/`Arm`/`Wasm` intrinsics as a last resort |
| [references/vector-t-and-fixed-shape.md](references/vector-t-and-fixed-shape.md) | `Vector<T>` portable vectorization; `Vector2`/`Vector3`/`Vector4`/`Matrix`/`Quaternion` graphics types |
| [references/performance-optimization-ladder.md](references/performance-optimization-ladder.md) | Bounds-check elimination, branch removal, loop unrolling, multi-core chunking recipes |
| [references/testing-and-benchmarking.md](references/testing-and-benchmarking.md) | Testing across Scalar/Vector128/256/512; `.runsettings`; BenchmarkDotNet job configs |

