namespace NetFabric.Numerics.Rectangular3D;

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
    public static Vector<T> Sum<T>(this IEnumerable<Vector<T>> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (source.TryGetSpan(out var span))
            return Sum(span);

        var sumX = T.Zero;
        var sumY = T.Zero;
        var sumZ = T.Zero;
        foreach (var vector in source)
        {
            checked
            {
                sumX += vector.X;
                sumY += vector.Y;
                sumZ += vector.Z;
            }
        }
        return new Vector<T>(sumX, sumY, sumZ);
    }

    /// <summary>
    /// Calculates the sum of an array of vectors.
    /// </summary>
    /// <param name="source">The array collection of vectors.</param>
    /// <returns>The sum of the vectors in the collection.</returns>
    /// <remarks>
    /// The sum of vectors is computed by adding all the vectors in the given <paramref name="source"/> collection.
    /// </remarks>
    public static Vector<T> Sum<T>(this Vector<T>[] source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Sum(source.AsSpan());

    /// <summary>
    /// Calculates the sum of a span of vectors.
    /// </summary>
    /// <param name="source">The <see cref="Span{T}"/> collection of vectors.</param>
    /// <returns>The sum of the vectors in the collection.</returns>
    /// <remarks>
    /// The sum of vectors is computed by adding all the vectors in the given <paramref name="source"/> collection.
    /// </remarks>
    public static Vector<T> Sum<T>(this Span<Vector<T>> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Sum((ReadOnlySpan<Vector<T>>)source);

    /// <summary>
    /// Calculates the sum of a read-only span of vectors.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> collection of vectors.</param>
    /// <returns>The sum of the vectors in the collection.</returns>
    /// <remarks>
    /// The sum of vectors is computed by adding all the vectors in the given <paramref name="source"/> collection.
    /// </remarks>
    public static Vector<T> Sum<T>(this ReadOnlySpan<Vector<T>> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        var sumX = T.Zero;
        var sumY = T.Zero;
        var sumZ = T.Zero;
        foreach (var v in source)
        {
            checked
            {
                sumX += v.X;
                sumY += v.Y;
                sumZ += v.Z;
            }
        }
        return new Vector<T>(sumX, sumY, sumZ);
    }
}