using System.Numerics.Tensors;

namespace NetFabric.Numerics;

public static partial class Angle
{
    /// <summary>
    /// Adds corresponding elements of <paramref name="left"/> and <paramref name="right"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Add<TUnits, T>(ReadOnlySpan<Angle<TUnits, T>> left, ReadOnlySpan<Angle<TUnits, T>> right, Span<Angle<TUnits, T>> result)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => TensorPrimitives.Add(left, right, result);

    /// <summary>
    /// Subtracts corresponding elements of <paramref name="right"/> from <paramref name="left"/> and stores the result in <paramref name="result"/>.
    /// </summary>
    public static void Subtract<TUnits, T>(ReadOnlySpan<Angle<TUnits, T>> left, ReadOnlySpan<Angle<TUnits, T>> right, Span<Angle<TUnits, T>> result)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => TensorPrimitives.Subtract(left, right, result);

}