using System.Runtime.Intrinsics;

namespace NetFabric.Numerics;

static class Vector3Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<ushort> AsVector64(this Vector3<ushort> vector)
        => new Vector4<ushort>(vector.X, vector.Y, vector.Z, 0).AsVector64();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<short> AsVector64(this Vector3<short> vector)
        => new Vector4<short>(vector.X, vector.Y, vector.Z, 0).AsVector64();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<Half> AsVector64(this Vector3<Half> vector)
        => new Vector4<Half>(vector.X, vector.Y, vector.Z, Half.Zero).AsVector64();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<ushort> AsVector128(this Vector3<ushort> vector)
        => new Vector4<ushort>(vector.X, vector.Y, vector.Z, 0).AsVector128();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<short> AsVector128(this Vector3<short> vector)
        => new Vector4<short>(vector.X, vector.Y, vector.Z, 0).AsVector128();

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Vector128<Half> AsVector128(this Vector3<Half> vector)
    //    => new Vector4<Half>(vector.X, vector.Y, vector.Z, Half.Zero).AsVector128();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<uint> AsVector128(this Vector3<uint> vector)
        => new Vector4<uint>(vector.X, vector.Y, vector.Z, 0).AsVector128();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> AsVector128(this Vector3<int> vector)
        => new Vector4<int>(vector.X, vector.Y, vector.Z, 0).AsVector128();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> AsVector128(this Vector3<float> vector)
        => new Vector4<float>(vector.X, vector.Y, vector.Z, 0.0f).AsVector128();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<ulong> AsVector256(this Vector3<ulong> vector)
        => new Vector4<ulong>(vector.X, vector.Y, vector.Z, 0).AsVector256();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<long> AsVector256(this Vector3<long> vector)
        => new Vector4<long>(vector.X, vector.Y, vector.Z, 0).AsVector256();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> AsVector256(this Vector3<double> vector)
        => new Vector4<double>(vector.X, vector.Y, vector.Z, 0.0).AsVector256();
}
