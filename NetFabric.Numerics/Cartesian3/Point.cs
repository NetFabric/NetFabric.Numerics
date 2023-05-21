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
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}

public static class Point
{
    public static double Distance<T>(Point<T> from, Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Math.Sqrt(SquareOfDistance(from, to));

    public static double SquareOfDistance<T>(Point<T> from, Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Math.Pow(double.CreateChecked(to.X - from.X), 2.0) + 
            Math.Pow(double.CreateChecked(to.Y - from.Y), 2.0) +
            Math.Pow(double.CreateChecked(to.Z - from.Z), 2.0);

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
