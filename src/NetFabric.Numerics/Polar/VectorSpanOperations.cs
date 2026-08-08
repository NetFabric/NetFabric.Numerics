namespace NetFabric.Numerics.Polar;

public static partial class Vector
{
    /// <summary>
    /// Adds a <paramref name="value"/> vector to each element in <paramref name="angles"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Add<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> angles, Vector<TAngleUnits, T> value, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        if (result.Length < angles.Length)
            throw new ArgumentException("Destination span is too short.", nameof(result));
        for (var i = 0; i < angles.Length; i++)
            result[i] = new Vector<TAngleUnits, T>(
                angles[i].Radius + value.Radius,
                new Angle<TAngleUnits, T>(angles[i].Azimuth.Value + value.Azimuth.Value));
    }

    /// <summary>
    /// Adds corresponding elements from <paramref name="left"/> and <paramref name="right"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Add<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> left, ReadOnlySpan<Vector<TAngleUnits, T>> right, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => TensorPrimitives.Add(
            MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(left),
            MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(right),
            MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(result));

    /// <summary>
    /// Subtracts a <paramref name="value"/> vector from each element in <paramref name="angles"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Subtract<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> angles, Vector<TAngleUnits, T> value, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        if (result.Length < angles.Length)
            throw new ArgumentException("Destination span is too short.", nameof(result));
        for (var i = 0; i < angles.Length; i++)
            result[i] = new Vector<TAngleUnits, T>(
                angles[i].Radius - value.Radius,
                new Angle<TAngleUnits, T>(angles[i].Azimuth.Value - value.Azimuth.Value));
    }

    /// <summary>
    /// Subtracts corresponding elements of <paramref name="right"/> from <paramref name="left"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Subtract<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> left, ReadOnlySpan<Vector<TAngleUnits, T>> right, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => TensorPrimitives.Subtract(
            MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(left),
            MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(right),
            MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(result));

    /// <summary>
    /// Multiplies each element in <paramref name="angles"/> by the <paramref name="value"/> vector and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Multiply<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> angles, Vector<TAngleUnits, T> value, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        if (result.Length < angles.Length)
            throw new ArgumentException("Destination span is too short.", nameof(result));
        for (var i = 0; i < angles.Length; i++)
            result[i] = new Vector<TAngleUnits, T>(
                angles[i].Radius * value.Radius,
                new Angle<TAngleUnits, T>(angles[i].Azimuth.Value * value.Azimuth.Value));
    }

    /// <summary>
    /// Divides each element in <paramref name="angles"/> by the <paramref name="value"/> vector and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Divide<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> angles, Vector<TAngleUnits, T> value, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        if (result.Length < angles.Length)
            throw new ArgumentException("Destination span is too short.", nameof(result));
        for (var i = 0; i < angles.Length; i++)
            result[i] = new Vector<TAngleUnits, T>(
                angles[i].Radius / value.Radius,
                new Angle<TAngleUnits, T>(angles[i].Azimuth.Value / value.Azimuth.Value));
    }

}