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
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this IEnumerable<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        if(source.TryGetSpan(out var span))
            return span.Sum();

        var sum = T.Zero;
        foreach (var angle in source)
        {
            checked { sum += angle.Value; }
        }
        return new Angle<TUnits, T>(sum);
    }

    /// <summary>
    /// Calculates the sum of an array of angles.
    /// </summary>
    /// <param name="source">The array collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this Angle<TUnits, T>[] source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.AsSpan().Sum();

    /// <summary>
    /// Calculates the sum of a span of angles.
    /// </summary>
    /// <param name="source">The <see cref="Span{T}"/> collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this Span<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ((ReadOnlySpan<Angle<TUnits, T>>)source).Sum();

    /// <summary>
    /// Calculates the sum of a read-only span of angles.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this ReadOnlySpan<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        var sum = T.Zero;
        if(Vector.IsHardwareAccelerated && source.Length > Vector<T>.Count * 2)
        {
            var vectors = MemoryMarshal.Cast<Angle<TUnits, T>, Vector<T>>(source);
            var sumVector = Vector<T>.Zero;

            foreach (ref readonly var vector in vectors)
                sumVector += vector;

            sum = sumVector.SumItems();
            var remainder = source.Length % Vector<T>.Count;
            source = source[^remainder..];
        }
        foreach (ref readonly var angle in source)
        {
            checked { sum += angle.Value; }
        }
        return new(sum);
    }
}
