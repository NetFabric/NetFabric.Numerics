namespace NetFabric.Numerics;

public static partial class Angle
{
    /// <summary>
    /// Calculates the average of a collection of angles.
    /// </summary>
    /// <param name="source">The enumerable of angles.</param>
    /// <returns>The average angle in the collection, or null if the collection is empty.</returns>
    /// <remarks>
    /// The average angle is computed by summing all the angles in the given <paramref name="source"/> collection and dividing the sum by the count of angles.
    /// If the array is empty, the method returns null to indicate that there are no angles to compute the average from.
    /// The resulting angle represents the average value of all the angles.
    /// </remarks>
    public static Angle<TUnits, T>? Average<TUnits, T>(this IEnumerable<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        if(source.TryGetSpan(out var span))
            return span.Average();

        var sum = T.Zero;
        var count = T.Zero;
        foreach (var angle in source)
        {
            checked 
            { 
                sum += angle.Value;
                count++;
            }
        }
        return T.IsZero(count) 
            ? null 
            : new Angle<TUnits, T>(sum / count);
    }

    /// <summary>
    /// Calculates the average of an array of angles.
    /// </summary>
    /// <param name="source">The array of angles.</param>
    /// <returns>The average angle in the collection, or null if the collection is empty.</returns>
    /// <remarks>
    /// The average angle is computed by summing all the angles in the given <paramref name="source"/> collection and dividing the sum by the count of angles.
    /// If the array is empty, the method returns null to indicate that there are no angles to compute the average from.
    /// The resulting angle represents the average value of all the angles.
    /// </remarks>
    public static Angle<TUnits, T>? Average<TUnits, T>(this Angle<TUnits, T>[] source)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.AsSpan().Average();

    /// <summary>
    /// Calculates the average of a span of angles.
    /// </summary>
    /// <param name="source">The <see cref="Span{T}"/> of angles.</param>
    /// <returns>The average angle in the collection, or null if the collection is empty.</returns>
    /// <remarks>
    /// The average angle is computed by summing all the angles in the given <paramref name="source"/> collection and dividing the sum by the count of angles.
    /// If the array is empty, the method returns null to indicate that there are no angles to compute the average from.
    /// The resulting angle represents the average value of all the angles.
    /// </remarks>
    public static Angle<TUnits, T>? Average<TUnits, T>(this Span<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ((ReadOnlySpan<Angle<TUnits, T>>)source).Average();

    /// <summary>
    /// Calculates the average of a read-only span of angles.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> of angles.</param>
    /// <returns>The average angle in the collection, or null if the collection is empty.</returns>
    /// <remarks>
    /// The average angle is computed by summing all the angles in the given <paramref name="source"/> collection and dividing the sum by the count of angles.
    /// If the array is empty, the method returns null to indicate that there are no angles to compute the average from.
    /// The resulting angle represents the average value of all the angles.
    /// </remarks>
    public static Angle<TUnits, T>? Average<TUnits, T>(this ReadOnlySpan<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T> 
        => source.Length is 0
            ? null
            : Sum(source) / T.CreateChecked(source.Length);
}
