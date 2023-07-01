using System.Runtime.Intrinsics;

namespace NetFabric.Numerics;

static class Vector4Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Vector64<T> AsVector64<T>(this in Vector4<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => ref Unsafe.As<Vector4<T>, Vector64<T>>(ref Unsafe.AsRef(in vector));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<T> ReadUnalignedVector64<T>(this in Vector4<T> vector)
    where T : struct, INumber<T>, IMinMaxValue<T>
        => Unsafe.ReadUnaligned<Vector64<T>>(ref Unsafe.As<Vector4<T>, byte>(ref Unsafe.AsRef(in vector)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Vector128<T> AsVector128<T>(this in Vector4<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => ref Unsafe.As<Vector4<T>, Vector128<T>>(ref Unsafe.AsRef(in vector));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> ReadUnalignedVector128<T>(this in Vector4<T> vector)
    where T : struct, INumber<T>, IMinMaxValue<T>
        => Unsafe.ReadUnaligned<Vector128<T>>(ref Unsafe.As<Vector4<T>, byte>(ref Unsafe.AsRef(in vector)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Vector256<T> AsVector256<T>(this in Vector4<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => ref Unsafe.As<Vector4<T>, Vector256<T>>(ref Unsafe.AsRef(in vector));

    // -----------------------------------------------------

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Vector4<T> AsVector4<T>(this ref Vector64<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => ref Unsafe.As<Vector64<T>, Vector4<T>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<T> ReadUnalignedVector4<T>(this ref Vector128<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        ref byte address = ref Unsafe.As<Vector128<T>, byte>(ref vector);
        return Unsafe.ReadUnaligned<Vector4<T>>(ref address);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Vector4<T> AsVector4<T>(this ref Vector128<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => ref Unsafe.As<Vector128<T>, Vector4<T>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Vector4<T> AsVector4<T>(this ref Vector256<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => ref Unsafe.As<Vector256<T>, Vector4<T>>(ref vector);
}