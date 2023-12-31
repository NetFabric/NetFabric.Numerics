using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Rectangular2D;

public static partial class Vector
{
    public static void Add<T>(ReadOnlySpan<Vector<T>> angles, Vector<T> value, Span<Vector<T>> result)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.Add(MemoryMarshal.Cast<Vector<T>, T>(angles), (value.X, value.Y), MemoryMarshal.Cast<Vector<T>, T>(result));

    public static void Add<T>(ReadOnlySpan<Vector<T>> left, ReadOnlySpan<Vector<T>> right, Span<Vector<T>> result)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.Add(MemoryMarshal.Cast<Vector<T>, T>(left), MemoryMarshal.Cast<Vector<T>, T>(right), MemoryMarshal.Cast<Vector<T>, T>(result));

    public static void Subtract<T>(ReadOnlySpan<Vector<T>> angles, Vector<T> value, Span<Vector<T>> result)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.Subtract(MemoryMarshal.Cast<Vector<T>, T>(angles), (value.X, value.Y), MemoryMarshal.Cast<Vector<T>, T>(result));

    public static void Subtract<T>(ReadOnlySpan<Vector<T>> left, ReadOnlySpan<Vector<T>> right, Span<Vector<T>> result)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.Subtract(MemoryMarshal.Cast<Vector<T>, T>(left), MemoryMarshal.Cast<Vector<T>, T>(right), MemoryMarshal.Cast<Vector<T>, T>(result));

    public static void Multiply<T>(ReadOnlySpan<Vector<T>> angles, Vector<T> value, Span<Vector<T>> result)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.Multiply(MemoryMarshal.Cast<Vector<T>, T>(angles), (value.X, value.Y), MemoryMarshal.Cast<Vector<T>, T>(result));

    public static void Divide<T>(ReadOnlySpan<Vector<T>> angles, Vector<T> value, Span<Vector<T>> result)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.Divide(MemoryMarshal.Cast<Vector<T>, T>(angles), (value.X, value.Y), MemoryMarshal.Cast<Vector<T>, T>(result));

}
