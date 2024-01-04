namespace NetFabric.Numerics;

public static partial class Tensor
{
    /// <summary>
    /// Adds a scalar value to each element of the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The scalar value to add.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the addition operation is not defined for the type <typeparamref name="T"/>.</exception>
    public static void Add<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Apply<T, AddOperator<T>>(x, y, destination);

    /// <summary>
    /// Adds a scalar value to each element of the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of two values to add.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the addition operation is not defined for the type <typeparamref name="T"/>.</exception>
    /// <remarks>
    /// This method can be used to calculate the addition of 2D vectors.
    /// </remarks>
    public static void Add<T>(ReadOnlySpan<T> x, ValueTuple<T, T> y, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Apply<T, AddOperator<T>>(x, y, destination);

    /// <summary>
    /// Adds a scalar value to each element of the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of three values to add.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the addition operation is not defined for the type <typeparamref name="T"/>.</exception>
    /// <remarks>
    /// This method can be used to calculate the addition of 3D vectors.
    /// </remarks>
    public static void Add<T>(ReadOnlySpan<T> x, ValueTuple<T, T, T> y, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Apply<T, AddOperator<T>>(x, y, destination);

    /// <summary>
    /// Adds corresponding elements of two source spans and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The second source span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the addition operation is not defined for the type <typeparamref name="T"/>.</exception>
    public static void Add<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Apply<T, AddOperator<T>>(x, y, destination);
}
