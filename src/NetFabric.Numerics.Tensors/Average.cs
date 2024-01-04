namespace NetFabric.Numerics;

public static partial class Tensor
{
    /// <summary>
    /// Computes the average of a span of values.
    /// </summary>
    /// <typeparam name="T">The type of the values in the span.</typeparam>
    /// <param name="source">The span of values.</param>
    /// <returns>The average value.</returns>
    /// <remarks>
    /// The <paramref name="source"/> span must contain at least one value.
    /// </remarks>
    public static T? Average<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
        => source.Length is 0
            ? null
            : Sum(source) / T.CreateChecked(source.Length);
}
