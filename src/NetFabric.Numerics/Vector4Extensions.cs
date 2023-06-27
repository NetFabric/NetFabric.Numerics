using System.Runtime.Intrinsics;

namespace NetFabric.Numerics;

static class Vector4Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<ushort> AsVector64(this Vector4<ushort> vector)
        => Unsafe.As<Vector4<ushort>, Vector64<ushort>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<short> AsVector64(this Vector4<short> vector)
        => Unsafe.As<Vector4<short>, Vector64<short>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<Half> AsVector64(this Vector4<Half> vector)
        => Unsafe.As<Vector4<Half>, Vector64<Half>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<ushort> AsVector128(this Vector4<ushort> vector)
        => Vector128.Create(vector.X, vector.Y, vector.Z, vector.W, 0, 0, 0, 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<short> AsVector128(this Vector4<short> vector)
        => Vector128.Create(vector.X, vector.Y, vector.Z, vector.W, 0, 0, 0, 0);

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Vector128<Half> AsVector128(this Vector4<Half> vector)
    //    => Vector128.Create(vector.X, vector.Y, vector.Z, vector.W, 0.0, 0.0, 0.0, 0.0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<uint> AsVector128(this Vector4<uint> vector)
        => Unsafe.As<Vector4<uint>, Vector128<uint>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> AsVector128(this Vector4<int> vector)
        => Unsafe.As<Vector4<int>, Vector128<int>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> AsVector128(this Vector4<float> vector)
        => Unsafe.As<Vector4<float>, Vector128<float>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<ulong> AsVector256(this Vector4<ulong> vector)
        => Unsafe.As<Vector4<ulong>, Vector256<ulong>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<long> AsVector256(this Vector4<long> vector)
        => Unsafe.As<Vector4<long>, Vector256<long>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> AsVector256(this Vector4<double> vector)
        => Unsafe.As<Vector4<double>, Vector256<double>>(ref vector);

    // -----------------------------------------------------

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<ushort> AsVector4(this Vector64<ushort> vector)
        => Unsafe.As<Vector64<ushort>, Vector4<ushort>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<short> AsVector4(this Vector64<short> vector)
        => Unsafe.As<Vector64<short>, Vector4<short>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<Half> AsVector4(this Vector64<Half> vector)
        => Unsafe.As<Vector64<Half>, Vector4<Half>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<ushort> AsVector4(this Vector128<ushort> vector)
    {
        ref var address = ref Unsafe.As<Vector128<ushort>, byte>(ref vector);
        return Unsafe.ReadUnaligned<Vector4<ushort>>(ref address);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<short> AsVector4(this Vector128<short> vector)
    {
        ref var address = ref Unsafe.As<Vector128<short>, byte>(ref vector);
        return Unsafe.ReadUnaligned<Vector4<short>>(ref address);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<Half> AsVector4(this Vector128<Half> vector)
    {
        ref var address = ref Unsafe.As<Vector128<Half>, byte>(ref vector);
        return Unsafe.ReadUnaligned<Vector4<Half>>(ref address);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<uint> AsVector4(this Vector128<uint> vector)
    => Unsafe.As<Vector128<uint>, Vector4<uint>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<int> AsVector4(this Vector128<int> vector)
        => Unsafe.As<Vector128<int>, Vector4<int>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<float> AsVector4(this Vector128<float> vector)
        => Unsafe.As<Vector128<float>, Vector4<float>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<ulong> AsVector4(this Vector256<ulong> vector)
    => Unsafe.As<Vector256<ulong>, Vector4<ulong>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<long> AsVector4(this Vector256<long> vector)
        => Unsafe.As<Vector256<long>, Vector4<long>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<double> AsVector4(this Vector256<double> vector)
        => Unsafe.As<Vector256<double>, Vector4<double>>(ref vector);
}
