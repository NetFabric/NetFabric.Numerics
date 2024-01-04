namespace NetFabric.Numerics;

public static partial class Tensor
{
    /// <summary>
    /// Multiplies each element of the left span by the right value and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left operands.</param>
    /// <param name="right">The right operand.</param>
    /// <param name="destination">The span to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the spans have different lengths.</exception>
    public static void Multiply<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyOperator<T>>(left, right, destination);

    /// <summary>
    /// Multiplies each element of the left span by the right value and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left operands.</param>
    /// <param name="right">The right tuple operand.</param>
    /// <param name="destination">The span to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the spans have different lengths.</exception>
    /// <remarks>
    /// This method can be used to calculate the division of 2D vectors.
    /// </remarks>
    public static void Multiply<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyOperator<T>>(left, right, destination);

    /// <summary>
    /// Multiplies each element of the left span by the right value and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left operands.</param>
    /// <param name="right">The right tuple operand.</param>
    /// <param name="destination">The span to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the spans have different lengths.</exception>
    /// <remarks>
    /// This method can be used to calculate the division of 3D vectors.
    /// </remarks>
    public static void Multiply<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyOperator<T>>(left, right, destination);

    /// <summary>
    /// Multiplies each element of the left span by the corresponding element of the right span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left operands.</param>
    /// <param name="right">The span containing the right operands.</param>
    /// <param name="destination">The span to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the spans have different lengths.</exception>
    public static void Multiply<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyOperator<T>>(left, right, destination);
}
