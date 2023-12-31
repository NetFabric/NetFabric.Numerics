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

public readonly struct SumPairsOperator<T> 
    : IAggregationPairsOperator<T>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    public static ValueTuple<T, T> Seed 
        => (T.AdditiveIdentity, T.AdditiveIdentity);

    public static ValueTuple<T, T> ResultSelector(ValueTuple<T, T> value, Vector<T> vector)
    {
        for (var index = 0; index < Vector<T>.Count; index += 2)
        {
            value.Item1 += vector[index];
            value.Item2 += vector[index + 1];
        }
        return value;
    }

    public static T Invoke(T x, T y)
        => x + y;

    public static Vector<T> Invoke(Vector<T> x, Vector<T> y)
        => x + y;
}