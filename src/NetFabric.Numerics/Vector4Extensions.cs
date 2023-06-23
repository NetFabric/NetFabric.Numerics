using System.Runtime.Intrinsics;

namespace NetFabric.Numerics;

static class Vector4Extensions
{
    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<ushort> AsVector64(this Vector4<ushort> vector)
        => Vector64.LoadUnsafe(ref Unsafe.AsRef(in vector.X));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<short> AsVector64(this Vector4<short> vector)
        => Vector64.LoadUnsafe(ref Unsafe.AsRef(in vector.X));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<Half> AsVector64(this Vector4<Half> vector)
        => Vector64.LoadUnsafe(ref Unsafe.AsRef(in vector.X));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<uint> AsVector128(this Vector4<uint> vector)
        => Vector128.LoadUnsafe(ref Unsafe.AsRef(in vector.X));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> AsVector128(this Vector4<int> vector)
        => Vector128.LoadUnsafe(ref Unsafe.AsRef(in vector.X));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> AsVector128(this Vector4<float> vector)
        => Vector128.LoadUnsafe(ref Unsafe.AsRef(in vector.X));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<ulong> AsVector256(this Vector4<ulong> vector)
        => Vector256.LoadUnsafe(ref Unsafe.AsRef(in vector.X));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<long> AsVector256(this Vector4<long> vector)
        => Vector256.LoadUnsafe(ref Unsafe.AsRef(in vector.X));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> AsVector256(this Vector4<double> vector)
        => Vector256.LoadUnsafe(ref Unsafe.AsRef(in vector.X));
}
