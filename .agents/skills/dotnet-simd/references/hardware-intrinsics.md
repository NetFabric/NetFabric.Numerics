# Platform-Specific Hardware Intrinsics

Last resort on the decision ladder — only reach for `System.Runtime.Intrinsics.X86`, `.Arm`, or `.Wasm` when a specific processor instruction gives a measured edge that `Vector128`/`Vector256`/`Vector512` don't expose. See [vector128-vectorization.md](vector128-vectorization.md) first.

Each intrinsic class exposes an `IsSupported` property (a JIT-time constant) so you can guard a specialized path and fall back to portable code elsewhere:

```csharp
public static bool AllBitsClear(Vector128<byte> vector, Vector128<byte> mask)
{
    if (Sse41.IsSupported)
    {
        return Sse41.TestZ(vector, mask);                          // x86/x64: single ptest
    }
    else if (AdvSimd.Arm64.IsSupported)
    {
        var anded = AdvSimd.And(vector, mask);                      // Arm64: AND + reduce max byte
        return AdvSimd.Arm64.MaxAcross(anded).ToScalar() == 0;
    }
    else if (PackedSimd.IsSupported)
    {
        return !PackedSimd.AnyTrue(PackedSimd.And(vector, mask));   // WebAssembly
    }
    else
    {
        return (vector & mask) == Vector128<byte>.Zero;             // portable fallback
    }
}
```

**Before writing per-architecture branches like this, check whether the portable expression already does the job.** `(vector & mask) == Vector128<byte>.Zero` already lowers to the optimal instruction on each platform (`ptest` on x86/x64) — the hand-written branches above do the same work with more code to maintain, for no gain. Reach for explicit intrinsics only when a specific instruction measurably beats what the portable APIs generate.

Hardware intrinsics require a separate implementation per instruction set — treat them as an optimization for measured hot paths, never a default. Confirm the difference with a benchmark (see [testing-and-benchmarking.md](testing-and-benchmarking.md)) before committing to the extra maintenance burden.
