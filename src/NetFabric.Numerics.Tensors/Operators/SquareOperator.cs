using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public readonly struct SquareOperator<T> 
    : IUnaryOperator<T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    public static T Invoke(T x) 
        => x * x;

    public static Vector<T> Invoke(Vector<T> x)
        => x * x;
}