namespace NetFabric.Numerics.Cartesian3;

public readonly record struct Point<T>(T X, T Y, T Z) 
    : IPoint<Point<T>>
    where T: struct, INumber<T>, IMinMaxValue<T>
{
    #region constants

    public static readonly Point<T> Zero = new(T.Zero, T.Zero, T.Zero);

    static Point<T> IPoint<Point<T>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<T> MinValue = new(T.MinValue, T.MinValue, T.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<T> MaxValue = new(T.MaxValue, T.MaxValue, T.MaxValue);

    static Point<T> IMinMaxValue<Point<T>>.MinValue
        => MinValue;
    static Point<T> IMinMaxValue<Point<T>>.MaxValue
        => MaxValue;

    #endregion

    public CoordinateSystem<T> CoordinateSystem 
        => new();
    ICoordinateSystem IPoint<Point<T>>.CoordinateSystem 
        => CoordinateSystem;

    #region addition

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point<T> operator +(Point<T> left, Vector<T> right)
        => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    #endregion

    #region subtraction

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point<T> operator -(Point<T> left, Vector<T> right)
        => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator +(Point<T> left, Point<T> right)
        => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    #endregion

    object IPoint<Point<T>>.this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}

public static class Point
{
    /// <summary>
    /// Calculates the distance between two points.
    /// </summary>
    /// <param name="from">The starting point.</param>
    /// <param name="to">The target point.</param>
    /// <returns>The distance between the two points.</returns>
    /// <remarks>
    /// <para>
    /// The <see cref="Distance"/> method calculates the distance between two points specified by the <paramref name="from"/> and <paramref name="to"/> parameters.
    /// </para>
    /// <para>
    /// The distance is calculated as the Euclidean distance in the 3D Cartesian coordinate system.
    /// </para>
    /// </remarks>
    public static double Distance<T>(Point<T> from, Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Math.Sqrt(double.CreateChecked(SquareOfDistance(from, to)));

    /// <summary>
    /// Calculates the square of the distance between two points.
    /// </summary>
    /// <param name="from">The starting point.</param>
    /// <param name="to">The target point.</param>
    /// <returns>The square of the distance between the two points.</returns>
    /// <remarks>
    /// <para>
    /// The <see cref="SquareOfDistance"/> method calculates the square of the distance between two points
    /// specified by the <paramref name="from"/> and <paramref name="to"/> parameters.
    /// </para>
    /// <para>
    /// The distance is calculated as the Euclidean distance in the 3D Cartesian coordinate system.
    /// </para>
    /// <para>
    /// Note that the square of the distance is returned instead of the actual distance to avoid the need for
    /// taking the square root, which can be a computationally expensive operation.
    /// </para>
    /// </remarks>
    public static T SquareOfDistance<T>(Point<T> from, Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Utils.Pow2(to.X - from.X) + Utils.Pow2(to.Y - from.Y) + Utils.Pow2(to.Z - from.Z);

    /// <summary>
    /// Gets the Manhattan distance between two points.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <remarks>
    /// <para>
    /// The term "Manhattan Distance" comes from the idea of measuring the distance a taxi 
    /// would have to travel along a grid of city blocks (which are typically arranged in 
    /// a rectangular or square grid pattern) to reach the destination point from the 
    /// starting point. 
    /// </para>
    /// <para>
    /// The Manhattan distance between two points, (x1, y1) and (x2, y2), is defined as the 
    /// sum of the absolute differences of their coordinates along each dimension.
    /// </para>
    /// </remarks>
    /// <returns>The Manhattan distance between two points.</returns>
    public static T ManhattanDistance<T>(Point<T> from, Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => T.Abs(to.X - from.X) + T.Abs(to.Y - from.Y) + T.Abs(to.Z - from.Z);

}
