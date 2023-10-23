namespace NetFabric.Numerics;

/// <summary>
/// Utility methods for performing mathematical operations on generic types.
/// </summary>
static class Utils
{
    /// <summary>
    /// Calculates the square of a value without requiring <see cref="IPowerFunctions{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of value to calculate the square for.</typeparam>
    /// <param name="x">The value to calculate the square of.</param>
    /// <returns>The square of the input value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Square<T>(T x)
        where T : struct, IMultiplyOperators<T, T, T>
        => x * x;

    /// <summary>
    /// Calculates the vector magnitude using the Euclidean distance for two points.
    /// </summary>
    /// <typeparam name="T">The type of values for the points.</typeparam>
    /// <param name="x">The x-coordinate of the vector.</param>
    /// <param name="y">The y-coordinate of the vector.</param>
    /// <returns>The vector magnitude using the Euclidean distance for the two points.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Magnitude<T>(T x, T y)
        where T : struct, IMultiplyOperators<T, T, T>, IRootFunctions<T>
        => T.Sqrt(Square(x) + Square(y));

    /// <summary>
    /// Calculates the 3D vector magnitude using the Euclidean distance for three points.
    /// </summary>
    /// <typeparam name="T">The type of values for the points.</typeparam>
    /// <param name="x">The x-coordinate of the vector.</param>
    /// <param name="y">The y-coordinate of the vector.</param>
    /// <param name="z">The z-coordinate of the vector.</param>
    /// <returns>The 3D vector magnitude using the Euclidean distance for the three points.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Magnitude<T>(T x, T y, T z)
        where T : struct, IMultiplyOperators<T, T, T>, IRootFunctions<T>
        => T.Sqrt(Square(x) + Square(y) + Square(z));

    /// <summary>
    /// Checks if two values are approximately equal within a given tolerance.
    /// </summary>
    /// <typeparam name="T">The type of values to compare.</typeparam>
    /// <param name="a">The first value to compare.</param>
    /// <param name="b">The second value to compare.</param>
    /// <param name="tolerance">The tolerance within which the values are considered equal.</param>
    /// <returns>True if the values are approximately equal; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AreApproximatelyEqual<T>(T a, T b, T tolerance)
        where T : struct, IFloatingPoint<T>
        => T.Abs(a - b) <= tolerance;
}
