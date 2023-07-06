namespace NetFabric.Numerics;

static class Vector2Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref System.Numerics.Vector2 AsVector2<T>(this in Vector2<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => ref Unsafe.As<Vector2<T>, System.Numerics.Vector2>(ref Unsafe.AsRef(in vector));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.Numerics.Vector<T> ToVector<T>(this in Vector2<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Unsafe.ReadUnaligned<Vector<T>>(ref Unsafe.As<Vector2<T>, byte>(ref Unsafe.AsRef(in vector)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Vector2<T> AsVector2<T>(this in System.Numerics.Vector2 vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => ref Unsafe.As<System.Numerics.Vector2, Vector2<T>>(ref Unsafe.AsRef(in vector));

#pragma warning disable EPS02 // Non-readonly struct used as in-parameter
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<T> ToVector2<T>(this in System.Numerics.Vector<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Unsafe.ReadUnaligned<Vector2<T>>(ref Unsafe.As<System.Numerics.Vector<T>, byte>(ref Unsafe.AsRef(in vector)));
#pragma warning restore EPS02 // Non-readonly struct used as in-parameter
}
