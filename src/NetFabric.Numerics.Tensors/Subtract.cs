using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Tensor
{
    /// <summary>
    /// Subtracts a value from each element in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The source span.</param>
    /// <param name="value">The value to subtract from each element.</param>
    /// <param name="destination">The destination span to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the type <typeparamref name="T"/> does not implement the <see cref="ISubtractionOperators{T, T, T}"/> interface.</exception>
    public static void Subtract<T>(ReadOnlySpan<T> source, T value, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
    {
        var subtract = new SubtractValueOperation<T>(value);
        Apply(source, destination, ref subtract);
    }

    /// <summary>
    /// Subtracts a pair of values from each corresponding pair of elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The source span.</param>
    /// <param name="value1">The value to subtract from the first element of the pair.</param>
    /// <param name="value2">The value to subtract from the second element of the pair.</param>
    /// <param name="destination">The destination span to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the type <typeparamref name="T"/> does not implement the <see cref="ISubtractionOperators{T, T, T}"/> interface.</exception>
    public static void Subtract<T>(ReadOnlySpan<T> source, T value1, T value2, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
    {
        var subtract = new SubtractValueOperation2D<T>(value1, value2);
        Apply2D(source, destination, ref subtract);
    }

    /// <summary>
    /// Subtracts corresponding elements in the left and right spans and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The left span.</param>
    /// <param name="right">The right span.</param>
    /// <param name="destination">The destination span to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the left, right, and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the type <typeparamref name="T"/> does not implement the <see cref="ISubtractionOperators{T, T, T}"/> interface.</exception>
    public static void Subtract<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
    {
        var subtract = new SubtractOperation<T>();
        Apply(left, right, destination, ref subtract);
    }
}
