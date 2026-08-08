using System.Numerics.Tensors;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Angle
{
    /// <summary>
    /// Adds a scalar <paramref name="value"/> to each element of <paramref name="angles"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Add<TUnits, T>(ReadOnlySpan<Angle<TUnits, T>> angles, Angle<TUnits, T> value, Span<Angle<TUnits, T>> result)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => TensorPrimitives.Add(angles, value, result);

    /// <summary>
    /// Adds corresponding elements of <paramref name="left"/> and <paramref name="right"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Add<TUnits, T>(ReadOnlySpan<Angle<TUnits, T>> left, ReadOnlySpan<Angle<TUnits, T>> right, Span<Angle<TUnits, T>> result)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => TensorPrimitives.Add(left, right, result);

    /// <summary>
    /// Subtracts a scalar <paramref name="value"/> from each element of <paramref name="angles"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Subtract<TUnits, T>(ReadOnlySpan<Angle<TUnits, T>> angles, Angle<TUnits, T> value, Span<Angle<TUnits, T>> result)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => TensorPrimitives.Subtract(angles, value, result);

    /// <summary>
    /// Subtracts corresponding elements of <paramref name="right"/> from <paramref name="left"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Subtract<TUnits, T>(ReadOnlySpan<Angle<TUnits, T>> left, ReadOnlySpan<Angle<TUnits, T>> right, Span<Angle<TUnits, T>> result)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => TensorPrimitives.Subtract(left, right, result);

    public static void Multiply<TUnits, T>(ReadOnlySpan<Angle<TUnits, T>> angles, Angle<TUnits, T> value, Span<Angle<TUnits, T>> result)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => TensorPrimitives.Multiply(MemoryMarshal.Cast<Angle<TUnits, T>, T>(angles), value.Value, MemoryMarshal.Cast<Angle<TUnits, T>, T>(result));

    public static void Divide<TUnits, T>(ReadOnlySpan<Angle<TUnits, T>> angles, Angle<TUnits, T> value, Span<Angle<TUnits, T>> result)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => TensorPrimitives.Divide(MemoryMarshal.Cast<Angle<TUnits, T>, T>(angles), value.Value, MemoryMarshal.Cast<Angle<TUnits, T>, T>(result));

}