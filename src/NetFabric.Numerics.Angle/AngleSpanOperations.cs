using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Angle
{
    public static void Add<TUnits, T>(ReadOnlySpan<Angle<TUnits, T>> angles, Angle<TUnits, T> value, Span<Angle<TUnits, T>> result)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Add(MemoryMarshal.Cast<Angle<TUnits, T>, T>(angles), value.Value, MemoryMarshal.Cast<Angle<TUnits, T>, T>(result));

    public static void Add<TUnits, T>(ReadOnlySpan<Angle<TUnits, T>> left, ReadOnlySpan<Angle<TUnits, T>> right, Span<Angle<TUnits, T>> result)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Add(MemoryMarshal.Cast<Angle<TUnits, T>, T>(left), MemoryMarshal.Cast<Angle<TUnits, T>, T>(right), MemoryMarshal.Cast<Angle<TUnits, T>, T>(result));

    public static void Subtract<TUnits, T>(ReadOnlySpan<Angle<TUnits, T>> angles, Angle<TUnits, T> value, Span<Angle<TUnits, T>> result)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Subtract(MemoryMarshal.Cast<Angle<TUnits, T>, T>(angles), value.Value, MemoryMarshal.Cast<Angle<TUnits, T>, T>(result));

    public static void Subtract<TUnits, T>(ReadOnlySpan<Angle<TUnits, T>> left, ReadOnlySpan<Angle<TUnits, T>> right, Span<Angle<TUnits, T>> result)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Subtract(MemoryMarshal.Cast<Angle<TUnits, T>, T>(left), MemoryMarshal.Cast<Angle<TUnits, T>, T>(right), MemoryMarshal.Cast<Angle<TUnits, T>, T>(result));

    public static void Multiply<TUnits, T>(ReadOnlySpan<Angle<TUnits, T>> angles, Angle<TUnits, T> value, Span<Angle<TUnits, T>> result)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Multiply(MemoryMarshal.Cast<Angle<TUnits, T>, T>(angles), value.Value, MemoryMarshal.Cast<Angle<TUnits, T>, T>(result));

    public static void Divide<TUnits, T>(ReadOnlySpan<Angle<TUnits, T>> angles, Angle<TUnits, T> value, Span<Angle<TUnits, T>> result)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Tensor.Divide(MemoryMarshal.Cast<Angle<TUnits, T>, T>(angles), value.Value, MemoryMarshal.Cast<Angle<TUnits, T>, T>(result));

}
