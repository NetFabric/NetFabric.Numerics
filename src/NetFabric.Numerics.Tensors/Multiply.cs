using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Tensor
{
    /// <summary>
    /// Multiplies a value to each element in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The source span.</param>
    /// <param name="value">The value to multiply each element by.</param>
    /// <param name="destination">The destination span to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the type <typeparamref name="T"/> does not implement the <see cref="IMultiplyOperators{T, T, T}"/> interface.</exception>
    public static void Multiply<T>(ReadOnlySpan<T> source, T value, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
    {
        var multiply = new MultiplyValueOperation<T>(value);
        Apply(source, destination, ref multiply);
    }

    /// <summary>
    /// Multiplies corresponding elements in the left and right spans and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The left span.</param>
    /// <param name="right">The right span.</param>
    /// <param name="destination">The destination span to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the left, right, and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the type <typeparamref name="T"/> does not implement the <see cref="IMultiplyOperators{T, T, T}"/> interface.</exception>
    public static void Multiply<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
    {
        var multiply = new MultiplyOperation<T>();
        Apply(left, right, destination, ref multiply);
    }
}
