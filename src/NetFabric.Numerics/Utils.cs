namespace NetFabric;

static class Utils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Pow2<T>(T x) 
        where T : struct, IMultiplyOperators<T, T, T>
        => x * x;
}
