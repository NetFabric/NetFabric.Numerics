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
    {
        var sum = new SumOperation<T>();
        Apply(source, ref sum);
        return sum.Result;
    }

    /// <summary>
    /// Computes the sum of a 2D span of values.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    /// <param name="source">The input span.</param>
    /// <returns>A tuple containing the sum of the elements along the rows and columns.</returns>
    /// <remarks>
    /// This method requires the type <typeparamref name="T"/> to implement the <see cref="IAdditionOperators{T, T, T}"/> and <see cref="IAdditiveIdentity{T, T}"/> interfaces.
    /// </remarks>
    public static (T, T) Sum2D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
    {        
        var sum = new SumOperation2D<T>();
        Apply2D(source, ref sum);
        return sum.Result;
    }
}
