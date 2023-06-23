using System.Runtime.Intrinsics;

namespace NetFabric.Numerics;

static class Vector2Extensions
{
    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<uint> AsVector64(this Vector2<uint> vector)
        => Vector64.LoadUnsafe(ref Unsafe.AsRef(in vector.X));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<int> AsVector64(this Vector2<int> vector)
        => Vector64.LoadUnsafe(ref Unsafe.AsRef(in vector.X));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> AsVector64(this Vector2<float> vector)
        => Vector64.LoadUnsafe(ref Unsafe.AsRef(in vector.X));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<uint> AsVector128(this Vector2<uint> vector)
        => new Vector4<uint>(vector.X, vector.Y, 0, 0).AsVector128();

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> AsVector128(this Vector2<int> vector)
        => new Vector4<int>(vector.X, vector.Y, 0, 0).AsVector128();

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> AsVector128(this Vector2<float> vector)
        => new Vector4<float>(vector.X, vector.Y, 0.0f, 0.0f).AsVector128();

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<ulong> AsVector128(this Vector2<ulong> vector)
        => Vector128.LoadUnsafe(ref Unsafe.AsRef(in vector.X));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<long> AsVector128(this Vector2<long> vector)
        => Vector128.LoadUnsafe(ref Unsafe.AsRef(in vector.X));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<double> AsVector128(this Vector2<double> vector)
        => Vector128.LoadUnsafe(ref Unsafe.AsRef(in vector.X));
}
