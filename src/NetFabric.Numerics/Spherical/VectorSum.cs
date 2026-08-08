namespace NetFabric.Numerics.Spherical;

public static partial class Vector
{
    /// <summary>
    /// Calculates the sum of a collection of vectors.
    /// </summary>
    /// <param name="source">The enumerable collection of vectors.</param>
    /// <returns>The sum of the vectors in the collection.</returns>
    /// <remarks>
    /// The sum of vectors is computed by adding all the vectors in the given <paramref name="source"/> collection.
    /// </remarks>
    public static Vector<TAngleUnits, T> Sum<TAngleUnits, T>(this IEnumerable<Vector<TAngleUnits, T>> source)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        if (source.TryGetSpan(out var span))
            return span.Sum();

        var sumRadius = T.Zero;
        var sumAzimuth = T.Zero;
        var sumPolar = T.Zero;
        foreach (var vector in source)
        {
            checked
            {
                sumRadius += vector.Radius;
                sumAzimuth += vector.Azimuth.Value;
                sumPolar += vector.Polar.Value;
            }
        }
        return new Vector<TAngleUnits, T>(sumRadius, new Angle<TAngleUnits, T>(sumAzimuth), new Angle<TAngleUnits, T>(sumPolar));
    }

    /// <summary>
    /// Calculates the sum of an array of vectors.
    /// </summary>
    /// <param name="source">The array of vectors.</param>
    /// <returns>The sum of the vectors in the array.</returns>
    /// <remarks>
    /// The sum of vectors is computed by adding all the vectors in the given <paramref name="source"/> array.
    /// </remarks>
    public static Vector<TAngleUnits, T> Sum<TAngleUnits, T>(this Vector<TAngleUnits, T>[] source)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.AsSpan().Sum();

    /// <summary>
    /// Calculates the sum of a span of vectors.
    /// </summary>
    /// <param name="source">The <see cref="Span{T}"/> of vectors.</param>
    /// <returns>The sum of the vectors in the span.</returns>
    /// <remarks>
    /// The sum of vectors is computed by adding all the vectors in the given <paramref name="source"/> span.
    /// </remarks>
    public static Vector<TAngleUnits, T> Sum<TAngleUnits, T>(this Span<Vector<TAngleUnits, T>> source)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ((ReadOnlySpan<Vector<TAngleUnits, T>>)source).Sum();

    /// <summary>
    /// Calculates the sum of a read-only span of vectors.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> of vectors.</param>
    /// <returns>The sum of the vectors in the span.</returns>
    /// <remarks>
    /// The sum of vectors is computed by adding all the vectors in the given <paramref name="source"/> span.
    /// </remarks>
    public static Vector<TAngleUnits, T> Sum<TAngleUnits, T>(this ReadOnlySpan<Vector<TAngleUnits, T>> source)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        (var sumRadius, var sumAzimuth, var sumPolar) = Tensor.SumTriplets(MemoryMarshal.Cast<Vector<TAngleUnits, T>, T>(source));
        return new Vector<TAngleUnits, T>(sumRadius, new Angle<TAngleUnits, T>(sumAzimuth), new Angle<TAngleUnits, T>(sumPolar));
    }
}
