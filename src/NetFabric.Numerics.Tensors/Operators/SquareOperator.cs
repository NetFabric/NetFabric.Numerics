namespace NetFabric.Numerics;

/// <summary>
/// Represents a square operator that performs squaring operations on values of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of values to square.</typeparam>
public readonly struct SquareOperator<T> 
    : IUnaryOperator<T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    /// <summary>
    /// Squares the specified value.
    /// </summary>
    /// <param name="x">The value to square.</param>
    /// <returns>The squared value.</returns>
    public static T Invoke(T x) 
        => x * x;

    /// <summary>
    /// Squares each element of the specified vector.
    /// </summary>
    /// <param name="x">The vector to square.</param>
    /// <returns>A new vector with each element squared.</returns>
    public static Vector<T> Invoke(Vector<T> x)
        => x * x;
}