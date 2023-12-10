using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public readonly struct MultiplyAddOperator<T> 
    : ITernaryOperator<T>
    where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
{
    public static T Invoke(T x, T y, T z) 
        => (x * y) + z;

    public static Vector<T> Invoke(Vector<T> x, Vector<T> y, Vector<T> z)
        => (x * y) + z;
}