namespace NetFabric.Numerics;

static class Utils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Pow2<T>(T x) 
        where T : struct, IMultiplyOperators<T, T, T>
        => x * x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Magnitude<T>(T x, T y)
        where T : struct, IMultiplyOperators<T, T, T>, IRootFunctions<T>
        => T.Sqrt(Pow2(x) + Pow2(y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Magnitude<T>(T x, T y, T z)
        where T : struct, IMultiplyOperators<T, T, T>, IRootFunctions<T>
        => T.Sqrt(Pow2(x) + Pow2(y) + Pow2(z));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AreApproximatelyEqual<T>(T a, T b, T tolerance)
        where T : struct, IFloatingPoint<T>
        => T.Abs(a - b) <= tolerance;

}
