namespace NetFabric.Numerics.Rectangular2D;

/// <summary>
/// Represents a point in a 2D Rectangular coordinate system.
/// </summary>
/// <typeparam name="T">The numeric type used for the coordinates.</typeparam>
/// <remarks>
/// In a 2D Rectangular coordinate system, a point is represented by a pair of values (X, Y) that specify its position
/// in 2D space. The X-coordinate represents the horizontal position, and the Y-coordinate represents the vertical position.
/// </remarks>
/// <param name="X">The X coordinate.</param>
/// <param name="Y">The Y coordinate.</param>
[System.Diagnostics.DebuggerDisplay("X = {X}, Y = {Y}")]
[SkipLocalsInit]
public readonly record struct Point<T>(T X, T Y) 
    : IPoint<Point<T>, CoordinateSystem<T>>
    where T: struct, INumber<T>, IMinMaxValue<T>
{
    #region constants

    public static readonly Point<T> Zero = new(T.Zero, T.Zero);

    static Point<T> IGeometricBase<Point<T>, CoordinateSystem<T>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<T> MinValue = new(T.MinValue, T.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<T> MaxValue = new(T.MaxValue, T.MaxValue);

    static Point<T> IMinMaxValue<Point<T>>.MinValue
        => MinValue;
    static Point<T> IMinMaxValue<Point<T>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{T}"/></param>
    /// <returns>An instance of <see cref="Point{T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{T}"/>.</exception>
    public static Point<T> CreateChecked<TOther>(ref readonly Point<TOther> point)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateChecked(point.X),
            T.CreateChecked(point.Y)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{T}"/></param>
    /// <returns>An instance of <see cref="Point{T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{T}"/>.</exception>
    public static Point<T> CreateSaturating<TOther>(ref readonly Point<TOther> point)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateSaturating(point.X),
            T.CreateSaturating(point.Y)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{T}"/></param>
    /// <returns>An instance of <see cref="Point{T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{T}"/>.</exception>
    public static Point<T> CreateTruncating<TOther>(ref readonly Point<TOther> point)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateTruncating(point.X),
            T.CreateTruncating(point.Y)
        );

    #region addition

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point<T> operator +(in Point<T> left, in Vector<T> right)
        => new(left.X + right.X, left.Y + right.Y);

    #endregion

    #region subtraction

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point<T> operator -(in Point<T> left, in Vector<T> right)
        => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator -(in Point<T> left, in Point<T> right)
        => new(left.X - right.X, left.Y - right.Y);

    #endregion

    object IGeometricBase.this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}

/// <summary>
/// Provides static methods for point operations.
/// </summary>
public static class Point
{
    /// <summary>
    /// Calculates the distance between two points.
    /// </summary>
    /// <typeparam name="T">The type of the point coordinates.</typeparam>
    /// <param name="from">The starting point.</param>
    /// <param name="to">The target point.</param>
    /// <returns>The distance between the two points.</returns>
    /// <remarks>
    /// <para>
    /// The <see cref="Distance"/> method calculates the distance between two points specified by the <paramref name="from"/> and <paramref name="to"/> parameters.
    /// </para>
    /// <para>
    /// The distance is calculated as the Euclidean distance in the 2D Rectangular coordinate system.
    /// </para>
    /// </remarks>
    public static T Distance<T>(ref readonly Point<T> from, ref readonly Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>, IRootFunctions<T>
        => T.Sqrt(DistanceSquared(in from, in to));

    /// <summary>
    /// Calculates the square of the distance between two points.
    /// </summary>
    /// <typeparam name="T">The type of the point coordinates.</typeparam>
    /// <param name="from">The starting point.</param>
    /// <param name="to">The target point.</param>
    /// <returns>The square of the distance between the two points.</returns>
    /// <remarks>
    /// <para>
    /// The <see cref="DistanceSquared"/> method calculates the square of the distance between two points
    /// specified by the <paramref name="from"/> and <paramref name="to"/> parameters.
    /// </para>
    /// <para>
    /// The distance is calculated as the Euclidean distance in the 2D Rectangular coordinate system.
    /// </para>
    /// <para>
    /// Note that the square of the distance is returned instead of the actual distance to avoid the need for
    /// taking the square root, which can be a computationally expensive operation.
    /// </para>
    /// </remarks>
    public static T DistanceSquared<T>(ref readonly Point<T> from, ref readonly Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Utils.Square(to.X - from.X) + Utils.Square(to.Y - from.Y);

    /// <summary>
    /// Gets the Manhattan distance between two points.
    /// </summary>
    /// <typeparam name="T">The type of the point coordinates.</typeparam>
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
    public static T ManhattanDistance<T>(ref readonly Point<T> from, ref readonly Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => T.Abs(to.X - from.X) + T.Abs(to.Y - from.Y);

    /// <summary>
    /// Converts a rectangular 2D point to polar coordinates.
    /// </summary>
    /// <typeparam name="T">The type of the point coordinates.</typeparam>
    /// <param name="point">The rectangular 2D point to convert.</param>
    /// <returns>The polar coordinates representing the point.</returns>
    /// <remarks>
    /// If the type of point to convert doesn't meet the constraints, please convert it first to a suitable type using one of the conversion methods: 
    /// - <see cref="Point{T}.CreateChecked{TOther}(ref readonly Point{TOther})"/>
    /// - <see cref="Point{T}.CreateSaturating{TOther}(ref readonly Point{TOther})"/>
    /// - <see cref="Point{T}.CreateTruncating{TOther}(ref readonly Point{TOther})"/>
    /// </remarks>
    public static Polar.Point<Radians, T> ToPolar<T>(ref readonly Point<T> point)
        where T : struct, IFloatingPointIeee754<T>, IMinMaxValue<T>
    {
        var azimuth = Angle.Atan2(point.Y, point.X);
        var radius = Utils.Magnitude(point.X, point.Y);

        return new(radius, azimuth);
    }
}
