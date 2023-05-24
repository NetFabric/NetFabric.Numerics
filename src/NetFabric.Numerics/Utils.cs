namespace NetFabric;

static class Utils
{
    public static T Pow2<T>(T x) where T : struct, INumber<T>
        => x * x;
}
