namespace NetFabric.Numerics;

/// <summary>
/// Represents an add operator for a specified type.
/// </summary>
/// <typeparam name="T">The type of the values to add.</typeparam>
public readonly struct AddOperator<T> 
    : IBinaryOperator<T>
    where T : struct, IAdditionOperators<T, T, T>
{
    /// <summary>
    /// Adds two values of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The first value to add.</param>
    /// <param name="y">The second value to add.</param>
    /// <returns>The sum of <paramref name="x"/> and <paramref name="y"/>.</returns>
    public static T Invoke(T x, T y) 
        => x + y;

    /// <summary>
    /// Adds two vectors of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The first vector to add.</param>
    /// <param name="y">The second vector to add.</param>
    /// <returns>The sum of <paramref name="x"/> and <paramref name="y"/>.</returns>
    public static Vector<T> Invoke(Vector<T> x, Vector<T> y)
        => x + y;
}