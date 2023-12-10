using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public readonly struct NegateOperator<T> 
    : IUnaryOperator<T>
    where T : struct, IUnaryNegationOperators<T, T>
{
    public static T Invoke(T x) 
        => -x;

    public static Vector<T> Invoke(Vector<T> x)
        => -x;
}