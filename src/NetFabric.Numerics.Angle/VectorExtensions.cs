namespace NetFabric.Numerics;

static class VectorExtensions
{
    public static T SumItems<T>(this Vector<T> vector)
        where T : struct, IFloatingPoint<T>
    {
        var sum = T.Zero;
        ref var item = ref Unsafe.As<Vector<T>, T>(ref Unsafe.AsRef(in vector));
        for (var index = 0; index < Vector<T>.Count; index++)
            sum += Unsafe.Add(ref item, index);
        return sum;
    }
}