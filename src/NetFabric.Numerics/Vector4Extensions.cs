namespace NetFabric.Numerics;

static class Vector4Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref System.Numerics.Vector4 AsVector4<T>(this in Vector4<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => ref Unsafe.As<Vector4<T>, System.Numerics.Vector4>(ref Unsafe.AsRef(in vector));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.Numerics.Vector<T> ToVector<T>(this in Vector4<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Unsafe.ReadUnaligned<Vector<T>>(ref Unsafe.As<Vector4<T>, byte>(ref Unsafe.AsRef(in vector)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Vector4<T> AsVector4<T>(this in System.Numerics.Vector4 vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => ref Unsafe.As<System.Numerics.Vector4, Vector4<T>>(ref Unsafe.AsRef(in vector));

#pragma warning disable EPS02 // Non-readonly struct used as in-parameter
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<T> ToVector4<T>(this in System.Numerics.Vector<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Unsafe.ReadUnaligned<Vector4<T>>(ref Unsafe.As<System.Numerics.Vector<T>, byte>(ref Unsafe.AsRef(in vector)));
#pragma warning restore EPS02 // Non-readonly struct used as in-parameter
}
