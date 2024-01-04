namespace NetFabric.Numerics;

/// <summary>
/// Represents a subtract operator for a specified type.
/// </summary>
/// <typeparam name="T">The type of the operands.</typeparam>
public readonly struct SubtractOperator<T> 
    : IBinaryOperator<T>
    where T : struct, ISubtractionOperators<T, T, T>
{
    /// <summary>
    /// Subtracts two values of type T.
    /// </summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The result of the subtraction.</returns>
    public static T Invoke(T x, T y) 
        => x - y;

    /// <summary>
    /// Subtracts two vectors of type T.
    /// </summary>
    /// <param name="x">The first vector.</param>
    /// <param name="y">The second vector.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector<T> Invoke(Vector<T> x, Vector<T> y)
        => x - y;
}
