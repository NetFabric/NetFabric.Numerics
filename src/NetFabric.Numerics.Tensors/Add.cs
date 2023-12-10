using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Add<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Apply<T, AddOperator<T>>(x, y, destination);

    public static void Add<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Apply<T, AddOperator<T>>(x, y, destination);
}
