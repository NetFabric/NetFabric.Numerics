namespace NetFabric.Numerics.Spherical;

public static partial class Vector
{
    /// <summary>
    /// Adds corresponding elements from <paramref name="left"/> and <paramref name="right"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    /// <param name="left">The left span of vectors.</param>
    /// <param name="right">The right span of vectors.</param>
    /// <param name="result">The destination span for the results.</param>
    public static void Add<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> left, ReadOnlySpan<Vector<TAngleUnits, T>> right, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        if (result.Length < left.Length)
            throw new ArgumentException("Destination span is too short.", nameof(result));
        TensorPrimitives.Add<Vector<TAngleUnits, T>>(left, right, result);
    }

    /// <summary>
    /// Subtracts corresponding elements of <paramref name="right"/> from <paramref name="left"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    /// <param name="left">The left span of vectors.</param>
    /// <param name="right">The right span of vectors.</param>
    /// <param name="result">The destination span for the results.</param>
    public static void Subtract<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> left, ReadOnlySpan<Vector<TAngleUnits, T>> right, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        if (result.Length < left.Length)
            throw new ArgumentException("Destination span is too short.", nameof(result));
        TensorPrimitives.Subtract<Vector<TAngleUnits, T>>(left, right, result);
    }

}