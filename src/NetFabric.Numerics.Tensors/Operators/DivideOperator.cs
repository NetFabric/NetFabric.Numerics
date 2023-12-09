using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public readonly struct DivideOperator<T> 
    : IBinaryOperator<T>
    where T : struct, IDivisionOperators<T, T, T>
{
    public static T Invoke(T x, T y) 
        => x / y;

    public static Vector<T> Invoke(Vector<T> x, Vector<T> y)
        => x / y;
}