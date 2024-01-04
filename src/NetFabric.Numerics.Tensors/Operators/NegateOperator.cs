namespace NetFabric.Numerics;

/// <summary>
/// Represents a unary negate operator.
/// </summary>
/// <typeparam name="T">The type of the operand.</typeparam>
public readonly struct NegateOperator<T> 
    : IUnaryOperator<T>
    where T : struct, IUnaryNegationOperators<T, T>
{
    /// <summary>
    /// Negates the specified value.
    /// </summary>
    /// <param name="x">The value to negate.</param>
    /// <returns>The negated value.</returns>
    public static T Invoke(T x) 
        => -x;

    /// <summary>
    /// Negates the specified vector.
    /// </summary>
    /// <param name="x">The vector to negate.</param>
    /// <returns>The negated vector.</returns>
    public static Vector<T> Invoke(Vector<T> x)
        => -x;
}