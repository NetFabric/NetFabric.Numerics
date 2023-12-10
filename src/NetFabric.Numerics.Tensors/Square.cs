using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Square<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, SquareOperator<T>>(left, destination);
}
