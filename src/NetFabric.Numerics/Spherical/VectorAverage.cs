namespace NetFabric.Numerics.Spherical;

public static partial class Vector
{
    /// <summary>
    /// Calculates the average of a collection of vectors.
    /// </summary>
    /// <param name="source">The enumerable collection of vectors.</param>
    /// <returns>The average of the vectors in the collection, or <see langword="null"/> if the collection is empty.</returns>
    /// <remarks>
    /// The average of vectors is computed by summing all the vectors in the given <paramref name="source"/> collection
    /// and dividing by the number of elements.
    /// </remarks>
    public static Vector<TAngleUnits, T>? Average<TAngleUnits, T>(this IEnumerable<Vector<TAngleUnits, T>> source)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        if (source.TryGetSpan(out var span))
            return Average(span);

        var sumRadius = T.Zero;
        var sumAzimuth = T.Zero;
        var sumPolar = T.Zero;
        var count = T.Zero;
        foreach (var vector in source)
        {
            checked
            {
                sumRadius += vector.Radius;
                sumAzimuth += vector.Azimuth.Value;
                sumPolar += vector.Polar.Value;
                count++;
            }
        }
        return T.IsZero(count)
            ? null
            : new Vector<TAngleUnits, T>(sumRadius / count, new Angle<TAngleUnits, T>(sumAzimuth / count), new Angle<TAngleUnits, T>(sumPolar / count));
    }

    /// <summary>
    /// Calculates the average of an array of vectors.
    /// </summary>
    /// <param name="source">The array of vectors.</param>
    /// <returns>The average of the vectors in the array, or <see langword="null"/> if the array is empty.</returns>
    /// <remarks>
    /// The average of vectors is computed by summing all the vectors in the given <paramref name="source"/> array
    /// and dividing by the number of elements.
    /// </remarks>
    public static Vector<TAngleUnits, T>? Average<TAngleUnits, T>(this Vector<TAngleUnits, T>[] source)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Average(source.AsSpan());

    /// <summary>
    /// Calculates the average of a span of vectors.
    /// </summary>
    /// <param name="source">The <see cref="Span{T}"/> of vectors.</param>
    /// <returns>The average of the vectors in the span, or <see langword="null"/> if the span is empty.</returns>
    /// <remarks>
    /// The average of vectors is computed by summing all the vectors in the given <paramref name="source"/> span
    /// and dividing by the number of elements.
    /// </remarks>
    public static Vector<TAngleUnits, T>? Average<TAngleUnits, T>(this Span<Vector<TAngleUnits, T>> source)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Average((ReadOnlySpan<Vector<TAngleUnits, T>>)source);

    /// <summary>
    /// Calculates the average of a read-only span of vectors.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> of vectors.</param>
    /// <returns>The average of the vectors in the span, or <see langword="null"/> if the span is empty.</returns>
    /// <remarks>
    /// The average of vectors is computed by summing all the vectors in the given <paramref name="source"/> span
    /// and dividing by the number of elements.
    /// </remarks>
    public static Vector<TAngleUnits, T>? Average<TAngleUnits, T>(this ReadOnlySpan<Vector<TAngleUnits, T>> source)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.Length is 0
            ? null
            : Sum(source) / T.CreateChecked(source.Length);
}