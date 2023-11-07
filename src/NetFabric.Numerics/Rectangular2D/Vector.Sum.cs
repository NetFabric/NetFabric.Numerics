using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Rectangular2D;

public static partial class Vector
{
    /// <summary>
    /// Calculates the sum of a collection of angles.
    /// </summary>
    /// <param name="source">The enumerable collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// </remarks>
    public static Vector<T> Sum<T>(this IEnumerable<Vector<T>> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if(source.TryGetSpan(out var span))
            return span.Sum();

        var sum = Vector<T>.Zero;
        foreach (var value in source)
        {
            checked { sum += value; }
        }
        return sum;
    }

    /// <summary>
    /// Calculates the sum of an array of angles.
    /// </summary>
    /// <param name="source">The array collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// </remarks>
    public static Vector<T> Sum<T>(this Vector<T>[] source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => source.AsSpan().Sum();

    /// <summary>
    /// Calculates the sum of a span of angles.
    /// </summary>
    /// <param name="source">The <see cref="Span{T}"/> collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// </remarks>
    public static Vector<T> Sum<T>(this Span<Vector<T>> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => ((ReadOnlySpan<Vector<T>>)source).Sum();

    /// <summary>
    /// Calculates the sum of a read-only span of angles.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// </remarks>
    public static Vector<T> Sum<T>(this ReadOnlySpan<Vector<T>> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(Tensor.Sum2D(MemoryMarshal.Cast<Vector<T>, T>(source)));
}
