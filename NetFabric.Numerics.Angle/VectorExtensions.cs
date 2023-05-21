using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics;

static class VectorExtensions
{
    public static T SumVectorItems<T>(this Vector<T> vector)
        where T : struct, IFloatingPoint<T>
    {
        ref var item = ref Unsafe.As<Vector<T>, T>(ref Unsafe.AsRef(in vector));
        var sum = T.Zero;
        for (var index = 0; index < Vector<T>.Count; index++)
            sum += Unsafe.Add(ref item, index);
        return sum;
    }
}