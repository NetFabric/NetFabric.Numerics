namespace NetFabric.Numerics;

public static partial class Tensor
{
    /// <summary>
    /// Subtracts a scalar value from each element of the left span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the elements to subtract from.</param>
    /// <param name="right">The scalar value to subtract from each element.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="NotSupportedException">Thrown when the subtraction operation is not supported for the specified type.</exception>
    public static void Subtract<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, SubtractOperator<T>>(left, right, destination);

    /// <summary>
    /// Subtracts a scalar value from each element of the left span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the elements to subtract from.</param>
    /// <param name="right">The tuple of two values to subtract from each element.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="NotSupportedException">Thrown when the subtraction operation is not supported for the specified type.</exception>
    /// <remarks>
    /// This method can be used to calculate the division of 2D vectors.
    /// </remarks>
    public static void Subtract<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, SubtractOperator<T>>(left, right, destination);

    /// <summary>
    /// Subtracts a scalar value from each element of the left span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the elements to subtract from.</param>
    /// <param name="right">The tuple of three values to subtract from each element.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="NotSupportedException">Thrown when the subtraction operation is not supported for the specified type.</exception>
    /// <remarks>
    /// This method can be used to calculate the division of 3D vectors.
    /// </remarks>
    public static void Subtract<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, SubtractOperator<T>>(left, right, destination);

    /// <summary>
    /// Subtracts each element of the right span from the corresponding element of the left span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the elements to subtract from.</param>
    /// <param name="right">The span containing the elements to subtract.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="NotSupportedException">Thrown when the subtraction operation is not supported for the specified type.</exception>
    public static void Subtract<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, SubtractOperator<T>>(left, right, destination);
}
