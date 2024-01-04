namespace NetFabric.Numerics;

/// <summary>
/// Represents a multiply-add operator for a given type.
/// </summary>
/// <typeparam name="T">The type of the values.</typeparam>
public readonly struct MultiplyAddOperator<T> 
    : ITernaryOperator<T>
    where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
{
    /// <summary>
    /// Computes the result of multiplying two values and adding a third value.
    /// </summary>
    /// <param name="x">The first value to multiply.</param>
    /// <param name="y">The second value to multiply.</param>
    /// <param name="z">The value to add.</param>
    /// <returns>The result of multiplying <paramref name="x"/> and <paramref name="y"/> and adding <paramref name="z"/>.</returns>
    public static T Invoke(T x, T y, T z) 
        => (x * y) + z;

    /// <summary>
    /// Computes the result of multiplying two vectors element-wise and adding a third vector element-wise.
    /// </summary>
    /// <param name="x">The first vector to multiply.</param>
    /// <param name="y">The second vector to multiply.</param>
    /// <param name="z">The vector to add.</param>
    /// <returns>The result of multiplying <paramref name="x"/> and <paramref name="y"/> element-wise and adding <paramref name="z"/> element-wise.</returns>
    public static Vector<T> Invoke(Vector<T> x, Vector<T> y, Vector<T> z)
        => (x * y) + z;
}