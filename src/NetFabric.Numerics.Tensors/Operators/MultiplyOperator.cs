namespace NetFabric.Numerics;

/// <summary>
/// Represents a multiply operator for a specified type.
/// </summary>
/// <typeparam name="T">The type of the operands and result.</typeparam>
public readonly struct MultiplyOperator<T> 
    : IBinaryOperator<T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    /// <summary>
    /// Multiplies two values of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The result of multiplying <paramref name="x"/> and <paramref name="y"/>.</returns>
    public static T Invoke(T x, T y) 
        => x * y;

    /// <summary>
    /// Multiplies two vectors of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The first vector.</param>
    /// <param name="y">The second vector.</param>
    /// <returns>The result of multiplying <paramref name="x"/> and <paramref name="y"/>.</returns>
    public static Vector<T> Invoke(Vector<T> x, Vector<T> y)
        => x * y;
}