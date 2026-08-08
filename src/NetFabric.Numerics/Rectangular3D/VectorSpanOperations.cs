namespace NetFabric.Numerics.Rectangular3D;

public static partial class Vector
{
    /// <summary>
    /// Adds corresponding elements from <paramref name="left"/> and <paramref name="right"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Add<T>(ReadOnlySpan<Vector<T>> left, ReadOnlySpan<Vector<T>> right, Span<Vector<T>> result)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (result.Length < left.Length)
            throw new ArgumentException("Destination span is too short.", nameof(result));
        TensorPrimitives.Add<Vector<T>>(left, right, result);
    }

    /// <summary>
    /// Subtracts corresponding elements of <paramref name="right"/> from <paramref name="left"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Subtract<T>(ReadOnlySpan<Vector<T>> left, ReadOnlySpan<Vector<T>> right, Span<Vector<T>> result)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (result.Length < left.Length)
            throw new ArgumentException("Destination span is too short.", nameof(result));
        TensorPrimitives.Subtract<Vector<T>>(left, right, result);
    }

}