using System.Runtime.Intrinsics;

namespace NetFabric.Numerics;

static class Vector3Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<T> ToVector64<T>(this in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Vector64.LoadUnsafe<T>(ref Unsafe.As<Vector3<T>, T>(ref Unsafe.AsRef(in vector)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> ToVector128<T>(this in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Vector128.LoadUnsafe<T>(ref Unsafe.As<Vector3<T>, T>(ref Unsafe.AsRef(in vector)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<T> ToVector256<T>(this in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Vector256.LoadUnsafe<T>(ref Unsafe.As<Vector3<T>, T>(ref Unsafe.AsRef(in vector)));

    // -----------------------------------------------------

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> ToVector3<T>(this ref Vector64<T> vector)
    where T : struct, INumber<T>, IMinMaxValue<T>
    {
        ref byte address = ref Unsafe.As<Vector64<T>, byte>(ref vector);
        return Unsafe.ReadUnaligned<Vector3<T>>(ref address);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> ToVector3<T>(this ref Vector128<T> vector)
    where T : struct, INumber<T>, IMinMaxValue<T>
    {
        ref byte address = ref Unsafe.As<Vector128<T>, byte>(ref vector);
        return Unsafe.ReadUnaligned<Vector3<T>>(ref address);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> ToVector3<T>(this ref Vector256<T> vector)
    where T : struct, INumber<T>, IMinMaxValue<T>
    {
        ref byte address = ref Unsafe.As<Vector256<T>, byte>(ref vector);
        return Unsafe.ReadUnaligned<Vector3<T>>(ref address);
    }
}