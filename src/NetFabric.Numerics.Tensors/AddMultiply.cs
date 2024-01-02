using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void AddMultiply<T>(ReadOnlySpan<T> x, T y, T z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    public static void AddMultiply<T>(ReadOnlySpan<T> x, ValueTuple<T, T> y, ValueTuple<T, T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    public static void AddMultiply<T>(ReadOnlySpan<T> x, T y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    public static void AddMultiply<T>(ReadOnlySpan<T> x, ValueTuple<T, T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    public static void AddMultiply<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, T z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    public static void AddMultiply<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ValueTuple<T, T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    public static void AddMultiply<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);
}
