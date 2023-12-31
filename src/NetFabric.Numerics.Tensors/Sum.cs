using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Tensor
{
    /// <summary>
    /// Computes the sum of a span of values.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    /// <param name="source">The input span.</param>
    /// <returns>The sum of the elements.</returns>
    /// <remarks>
    /// This method requires the type <typeparamref name="T"/> to implement the <see cref="IAdditionOperators{T, T, T}"/> and <see cref="IAdditiveIdentity{T, T}"/> interfaces.
    /// </remarks>
    public static T Sum<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Aggregate<T, SumOperator<T>>(source);

    /// <summary>
    /// Computes the sum of pairs of values in a span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    /// <param name="source">The input span.</param>
    /// <returns>A tuple containing the sum of pairs of elements.</returns>
    /// <remarks>
    /// This method can be used to calculate the sum of 2D vectors.
    /// </remarks>
    /// This method requires the type <typeparamref name="T"/> to implement the <see cref="IAdditionOperators{T, T, T}"/> and <see cref="IAdditiveIdentity{T, T}"/> interfaces.
    /// </remarks>
    public static ValueTuple<T, T> SumPairs<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => AggregatePairs<T, SumPairsOperator<T>>(source);
}
