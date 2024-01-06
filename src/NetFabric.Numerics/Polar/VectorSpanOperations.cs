namespace NetFabric.Numerics.Polar;

public static partial class Vector
{
    public static void Add<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> angles, Vector<TAngleUnits, T> value, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Add(MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(angles), (value.Radius, value.Azimuth.Value), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(result));

    public static void Add<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> left, ReadOnlySpan<Vector<TAngleUnits, T>> right, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Add(MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(left), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(right), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(result));

    public static void Subtract<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> angles, Vector<TAngleUnits, T> value, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Subtract(MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(angles), (value.Radius, value.Azimuth.Value), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(result));

    public static void Subtract<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> left, ReadOnlySpan<Vector<TAngleUnits, T>> right, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Subtract(MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(left), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(right), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(result));

    public static void Multiply<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> angles, Vector<TAngleUnits, T> value, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Multiply(MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(angles), (value.Radius, value.Azimuth.Value), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(result));

    public static void Divide<TAngleUnits, T>(ReadOnlySpan<Vector<TAngleUnits, T>> angles, Vector<TAngleUnits, T> value, Span<Vector<TAngleUnits, T>> result)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Divide(MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(angles), (value.Radius, value.Azimuth.Value), MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(result));

}
