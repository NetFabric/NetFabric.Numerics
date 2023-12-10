using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Negate<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IUnaryNegationOperators<T, T>
        => Apply<T, NegateOperator<T>>(left, destination);
}
