using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public readonly struct SumOperator<T> 
    : IAggregationOperator<T>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    public static T Seed 
        => T.AdditiveIdentity;

    public static T ResultSelector(T value, Vector<T> vector)
        => Vector.Sum(vector) + value;

    public static T Invoke(T x, T y)
        => x + y;

    public static Vector<T> Invoke(Vector<T> x, Vector<T> y)
        => x + y;
}