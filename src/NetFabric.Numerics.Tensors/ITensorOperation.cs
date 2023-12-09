namespace NetFabric.Numerics;

public interface IUnaryOperator<T>
    where T : struct
{
    static abstract T Invoke(T x);
    static abstract Vector<T> Invoke(Vector<T> x);
}

public interface IBinaryOperator<T> 
    where T : struct
{
    static abstract T Invoke(T x, T y);
    static abstract Vector<T> Invoke(Vector<T> x, Vector<T> y);
}

public interface IAggregationOperator<T> 
    : IBinaryOperator<T>
    where T : struct
{
    static virtual T Seed 
        => Throw.NotSupportedException<T>();

    static abstract T ResultSelector(T value, Vector<T> vector);
}

public interface ITernaryOperator<T>
    where T : struct
{
    static abstract T Invoke(T x, T y, T z);
    static abstract Vector<T> Invoke(Vector<T> x, Vector<T> y, Vector<T> z);
}