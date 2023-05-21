using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Angle
{
    /// <summary>
    /// Calculates the sum of a collection of angles.
    /// </summary>
    /// <param name="source">The enumerable collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// The resulting angle is the cumulative sum of all the angles in radians.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this IEnumerable<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        var sum = T.Zero;
        foreach (var angle in source)
        {
            checked { sum += angle.Value; }
        }
        return new Angle<TUnits, T>(sum);
    }

    /// <summary>
    /// Calculates the sum of a collection of angles.
    /// </summary>
    /// <param name="source">The array collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// The resulting angle is the cumulative sum of all the angles in radians.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this Angle<TUnits, T>[] source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.AsSpan().Sum();

    /// <summary>
    /// Calculates the sum of a collection of angles.
    /// </summary>
    /// <param name="source">The <see cref="Memory{T}"/> collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// The resulting angle is the cumulative sum of all the angles in radians.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this Memory<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.Span.Sum();

    /// <summary>
    /// Calculates the sum of a collection of angles.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlyMemory{T}"/> collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// The resulting angle is the cumulative sum of all the angles in radians.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this ReadOnlyMemory<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.Span.Sum();

    /// <summary>
    /// Calculates the sum of a collection of angles.
    /// </summary>
    /// <param name="source">The <see cref="Span{T}"/> collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// The resulting angle is the cumulative sum of all the angles in radians.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this Span<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ((ReadOnlySpan<Angle<TUnits, T>>)source).Sum();

    /// <summary>
    /// Calculates the sum of a collection of angles.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// The resulting angle is the cumulative sum of all the angles in radians.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this ReadOnlySpan<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        return Vector.IsHardwareAccelerated && source.Length > Vector<T>.Count * 2 
            ? new(AcceleratedSum(source)) 
            : new(RegularSum(source));

        static T RegularSum(ReadOnlySpan<Angle<TUnits, T>> source)
        {
            var sum = T.Zero;
            foreach (var angle in source)
            {
                checked { sum += angle.Value; }
            }
            return sum;
        }

        static T AcceleratedSum(ReadOnlySpan<Angle<TUnits, T>> source)
        {
            var vectors = MemoryMarshal.Cast<Angle<TUnits, T>, Vector<T>>(source);
            var sum = Vector<T>.Zero;

            foreach (var vector in vectors)
                sum += vector;

            var remainder = source.Length % Vector<T>.Count;
            return sum.SumVectorItems() + RegularSum(source[^remainder..]);
        }
    }
}
