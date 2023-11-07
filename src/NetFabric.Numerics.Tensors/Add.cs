using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Tensor
{
    /// <summary>
    /// Adds a value to each element in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The source span.</param>
    /// <param name="value">The value to add to each element.</param>
    /// <param name="destination">The destination span to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the type <typeparamref name="T"/> does not implement the <see cref="IAdditionOperators{T, T, T}"/> interface.</exception>
    public static void Add<T>(ReadOnlySpan<T> source, T value, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
    {
        var add = new AddValueOperation<T>(value);
        Apply(source, destination, ref add);
    }

    /// <summary>
    /// Adds a value to a pair of elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The source span.</param>
    /// <param name="value1">The value to be added to the first element of the pair.</param>
    /// <param name="value2">The value to be added to the second element of the pair.</param>
    /// <param name="destination">The destination span to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the type <typeparamref name="T"/> does not implement the <see cref="IAdditionOperators{T, T, T}"/> interface.</exception>
    public static void Add<T>(ReadOnlySpan<T> source, T value1, T value2, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
    {
        var add = new AddValueOperation2D<T>(value1, value2);
        Apply2D(source, destination, ref add);
    }

    /// <summary>
    /// Adds corresponding elements in the left and right spans and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The left span.</param>
    /// <param name="right">The right span.</param>
    /// <param name="destination">The destination span to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the left, right, and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the type <typeparamref name="T"/> does not implement the <see cref="IAdditionOperators{T, T, T}"/> interface.</exception>
    public static void Add<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
    {
        var add = new AddOperation<T>();
        Apply(left, right, destination, ref add);
    }
}
