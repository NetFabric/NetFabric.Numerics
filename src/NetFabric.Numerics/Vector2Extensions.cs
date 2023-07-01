using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace NetFabric.Numerics;

static class Vector2Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Vector64<T> AsVector64<T>(this in Vector2<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => ref Unsafe.As<Vector2<T>, Vector64<T>>(ref Unsafe.AsRef(in vector));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<T> ReadUnalignedVector64<T>(this in Vector2<T> vector)
    where T : struct, INumber<T>, IMinMaxValue<T> 
        => Unsafe.ReadUnaligned<Vector64<T>>(ref Unsafe.As<Vector2<T>, byte>(ref Unsafe.AsRef(in vector)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Vector128<T> AsVector128<T>(this in Vector2<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T> => ref Unsafe.As<Vector2<T>, Vector128<T>>(ref Unsafe.AsRef(in vector));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> ReadUnalignedVector128<T>(this in Vector2<T> vector)
    where T : struct, INumber<T>, IMinMaxValue<T>
        => Unsafe.ReadUnaligned<Vector128<T>>(ref Unsafe.As<Vector2<T>, byte>(ref Unsafe.AsRef(in vector)));

    // -----------------------------------------------------

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Vector2<T> AsVector2<T>(this ref Vector64<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => ref Unsafe.As<Vector64<T>, Vector2<T>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<T> ReadUnalignedVector2<T>(this ref Vector64<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        ref byte address = ref Unsafe.As<Vector64<T>, byte>(ref vector);
        return Unsafe.ReadUnaligned<Vector2<T>>(ref address);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Vector2<T> AsVector2<T>(this ref Vector128<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => ref Unsafe.As<Vector128<T>, Vector2<T>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<T> ReadUnalignedVector2<T>(this ref Vector128<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        ref byte address = ref Unsafe.As<Vector128<T>, byte>(ref vector);
        return Unsafe.ReadUnaligned<Vector2<T>>(ref address);
    }

    // -----------------------------------------------------

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<T> ToVector4<T>(this in Vector2<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(vector.X, vector.Y, T.Zero, T.Zero);
}