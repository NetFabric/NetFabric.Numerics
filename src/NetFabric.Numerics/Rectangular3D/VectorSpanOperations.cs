namespace NetFabric.Numerics.Rectangular3D;

public static partial class Vector
{
    /// <summary>
    /// Adds a <paramref name="value"/> vector to each element in <paramref name="angles"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Add<T>(ReadOnlySpan<Vector<T>> angles, Vector<T> value, Span<Vector<T>> result)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (result.Length < angles.Length)
            throw new ArgumentException("Destination span is too short.", nameof(result));
        TensorPrimitives.Add<Vector<T>>(angles, value, result);
    }

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
    /// Subtracts a <paramref name="value"/> vector from each element in <paramref name="angles"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Subtract<T>(ReadOnlySpan<Vector<T>> angles, Vector<T> value, Span<Vector<T>> result)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (result.Length < angles.Length)
            throw new ArgumentException("Destination span is too short.", nameof(result));
        TensorPrimitives.Subtract<Vector<T>>(angles, value, result);
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

    /// <summary>
    /// Multiplies each element in <paramref name="angles"/> by the <paramref name="value"/> vector and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Multiply<T>(ReadOnlySpan<Vector<T>> angles, Vector<T> value, Span<Vector<T>> result)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (result.Length < angles.Length)
            throw new ArgumentException("Destination span is too short.", nameof(result));
        for (var i = 0; i < angles.Length; i++)
            result[i] = new Vector<T>(angles[i].X * value.X, angles[i].Y * value.Y, angles[i].Z * value.Z);
    }

    /// <summary>
    /// Divides each element in <paramref name="angles"/> by the <paramref name="value"/> vector and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Divide<T>(ReadOnlySpan<Vector<T>> angles, Vector<T> value, Span<Vector<T>> result)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (result.Length < angles.Length)
            throw new ArgumentException("Destination span is too short.", nameof(result));
        for (var i = 0; i < angles.Length; i++)
            result[i] = new Vector<T>(angles[i].X / value.X, angles[i].Y / value.Y, angles[i].Z / value.Z);
    }

}