namespace NetFabric.Numerics;

/// <summary>
/// Represents a divide operator for a specified type.
/// </summary>
/// <typeparam name="T">The type of the operands.</typeparam>
public readonly struct DivideOperator<T> 
    : IBinaryOperator<T>
    where T : struct, IDivisionOperators<T, T, T>
{
    /// <summary>
    /// Divides two values of type T.
    /// </summary>
    /// <param name="x">The dividend.</param>
    /// <param name="y">The divisor.</param>
    /// <returns>The result of the division.</returns>
    public static T Invoke(T x, T y) 
        => x / y;

    /// <summary>
    /// Divides two vectors of type T.
    /// </summary>
    /// <param name="x">The dividend vector.</param>
    /// <param name="y">The divisor vector.</param>
    /// <returns>The result of the division.</returns>
    public static Vector<T> Invoke(Vector<T> x, Vector<T> y)
        => x / y;
}