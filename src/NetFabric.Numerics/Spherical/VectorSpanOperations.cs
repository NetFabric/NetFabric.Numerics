namespace NetFabric.Numerics.Spherical;

public static partial class Vector
{
    /// <summary>
    /// Adds a scalar <paramref name="value"/> to each element in <paramref name="source"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    /// <param name="source">The source span of vectors.</param>
    /// <param name="value">The vector value to add.</param>
    /// <param name="result">The destination span for the results.</param>
    public static void Add<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> source, Vector<TAngleUnits, T> value, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Add(MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(source), (value.Radius, value.Azimuth.Value, value.Polar.Value), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(result));

    /// <summary>
    /// Adds corresponding elements from <paramref name="left"/> and <paramref name="right"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    /// <param name="left">The left span of vectors.</param>
    /// <param name="right">The right span of vectors.</param>
    /// <param name="result">The destination span for the results.</param>
    public static void Add<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> left, ReadOnlySpan<Vector<TAngleUnits, T>> right, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Add(MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(left), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(right), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(result));

    /// <summary>
    /// Subtracts a scalar <paramref name="value"/> from each element in <paramref name="source"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    /// <param name="source">The source span of vectors.</param>
    /// <param name="value">The vector value to subtract.</param>
    /// <param name="result">The destination span for the results.</param>
    public static void Subtract<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> source, Vector<TAngleUnits, T> value, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Subtract(MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(source), (value.Radius, value.Azimuth.Value, value.Polar.Value), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(result));

    /// <summary>
    /// Subtracts corresponding elements of <paramref name="right"/> from <paramref name="left"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    /// <param name="left">The left span of vectors.</param>
    /// <param name="right">The right span of vectors.</param>
    /// <param name="result">The destination span for the results.</param>
    public static void Subtract<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> left, ReadOnlySpan<Vector<TAngleUnits, T>> right, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Subtract(MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(left), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(right), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(result));

    /// <summary>
    /// Multiplies each element in <paramref name="source"/> by a scalar <paramref name="value"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    /// <param name="source">The source span of vectors.</param>
    /// <param name="value">The vector value to multiply by.</param>
    /// <param name="result">The destination span for the results.</param>
    public static void Multiply<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> source, Vector<TAngleUnits, T> value, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Multiply(MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(source), (value.Radius, value.Azimuth.Value, value.Polar.Value), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(result));

    /// <summary>
    /// Divides each element in <paramref name="source"/> by a scalar <paramref name="value"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    /// <param name="source">The source span of vectors.</param>
    /// <param name="value">The vector value to divide by.</param>
    /// <param name="result">The destination span for the results.</param>
    public static void Divide<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> source, Vector<TAngleUnits, T> value, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Divide(MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(source), (value.Radius, value.Azimuth.Value, value.Polar.Value), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(result));
}