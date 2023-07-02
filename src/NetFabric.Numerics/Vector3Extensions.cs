namespace NetFabric.Numerics;

static class Vector3Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref System.Numerics.Vector3 AsVector3<T>(this in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => ref Unsafe.As<Vector3<T>, System.Numerics.Vector3>(ref Unsafe.AsRef(in vector));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.Numerics.Vector<T> ToVector<T>(this in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Unsafe.ReadUnaligned<Vector<T>>(ref Unsafe.As<Vector3<T>, byte>(ref Unsafe.AsRef(in vector)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Vector3<T> AsVector3<T>(this in System.Numerics.Vector3 vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => ref Unsafe.As<System.Numerics.Vector3, Vector3<T>>(ref Unsafe.AsRef(in vector));

#pragma warning disable EPS02 // Non-readonly struct used as in-parameter
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> ToVector3<T>(this in System.Numerics.Vector<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Unsafe.ReadUnaligned<Vector3<T>>(ref Unsafe.As<System.Numerics.Vector<T>, byte>(ref Unsafe.AsRef(in vector)));
#pragma warning restore EPS02 // Non-readonly struct used as in-parameter
}
