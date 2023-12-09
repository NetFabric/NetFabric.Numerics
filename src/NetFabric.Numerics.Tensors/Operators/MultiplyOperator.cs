using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public readonly struct MultiplyOperator<T> 
    : IBinaryOperator<T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    public static T Invoke(T x, T y) 
        => x * y;

    public static Vector<T> Invoke(Vector<T> x, Vector<T> y)
        => x * y;
}