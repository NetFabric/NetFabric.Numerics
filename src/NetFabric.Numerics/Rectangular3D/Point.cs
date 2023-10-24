namespace NetFabric.Numerics.Rectangular3D;

/// <summary>
/// Represents a point as an immutable struct.
/// </summary>
/// <typeparam name="T">The type of the point coordinates.</typeparam>
/// <param name="X">The X coordinate.</param>
/// <param name="Y">The X coordinate.</param>
/// <param name="Z">The X coordinate.</param>
[System.Diagnostics.DebuggerDisplay("X = {X}, Y = {Y}, Z = {Z}")]
[SkipLocalsInit]
public readonly record struct Point<T>(T X, T Y, T Z) 
    : IPoint<Point<T>>
    where T: struct, INumber<T>, IMinMaxValue<T>
{
    #region constants

    public static readonly Point<T> Zero = new(T.Zero, T.Zero, T.Zero);

    static Point<T> IGeometricBase<Point<T>>.Zero
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

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<T> CoordinateSystem 
        => new();
    ICoordinateSystem IGeometricBase<Point<T>>.CoordinateSystem 
        => CoordinateSystem;

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
            T.CreateChecked(point.Y),
            T.CreateChecked(point.Z)
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
            T.CreateSaturating(point.Y),
            T.CreateSaturating(point.Z)
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
            T.CreateTruncating(point.Y),
            T.CreateTruncating(point.Z)
        );

    #region addition

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point<T> operator +(in Point<T> left, in Vector<T> right)
        => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    #endregion

    #region subtraction

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point<T> operator -(in Point<T> left, in Vector<T> right)
        => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator -(in Point<T> left, in Point<T> right)
        => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    #endregion

    object IGeometricBase<Point<T>>.this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}

/// <summary>
/// Provides static methods for point operations.
/// </summary>
public static class Point
{

    /// <summary>
    /// Applies a quaternion to a 3-dimensional point.
    /// </summary>
    /// <typeparam name="T">The underlying numeric type of the quaternion and point coordinates.</typeparam>
    /// <param name="quaternion">The quaternion to apply.</param>
    /// <param name="point">The 3-dimensional point to transform.</param>
    /// <returns>The transformed 3-dimensional point.</returns>
    /// <remarks>
    /// <para>
    /// The <paramref name="quaternion"/> is not required to be a unit quaternion.
    /// </para>
    /// <para>
    /// The point coordinates type must be a floating point.
    /// </para>
    /// <para>
    /// The transformation is applied by multiplying the point with the quaternion,
    /// resulting in a new 3-dimensional point that represents the original point
    /// after being transformed by the quaternion.
    /// </para>
    /// </remarks>
    public static Point<T> Apply<T>(Quaternion<T> quaternion, Point<T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new (
            (quaternion.W * point.X) + (quaternion.Y * point.Z) - (quaternion.Z * point.Y),
            (quaternion.W * point.Y) + (quaternion.Z * point.X) - (quaternion.X * point.Z),
            (quaternion.W * point.Z) + (quaternion.X * point.Y) - (quaternion.Y * point.X)
        );

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
    /// The distance is calculated as the Euclidean distance in the 3D Rectangular coordinate system.
    /// </para>
    /// </remarks>
    public static T Distance<T>(ref readonly Point<T> from, ref readonly Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>, IRootFunctions<T>
        => T.Sqrt(DistanceSquared(in from, in to));

    /// <summary>
    /// Calculates the square of the distance between two points.
    /// </summary>
    /// <param name="from">The starting point.</param>
    /// <param name="to">The target point.</param>
    /// <returns>The square of the distance between the two points.</returns>
    /// <remarks>
    /// <para>
    /// The <see cref="DistanceSquared"/> method calculates the square of the distance between two points
    /// specified by the <paramref name="from"/> and <paramref name="to"/> parameters.
    /// </para>
    /// <para>
    /// The distance is calculated as the Euclidean distance in the 3D Rectangular coordinate system.
    /// </para>
    /// <para>
    /// Note that the square of the distance is returned instead of the actual distance to avoid the need for
    /// taking the square root, which can be a computationally expensive operation.
    /// </para>
    /// </remarks>
    public static T DistanceSquared<T>(ref readonly Point<T> from, ref readonly Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Utils.Square(to.X - from.X) + Utils.Square(to.Y - from.Y) + Utils.Square(to.Z - from.Z);

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
    public static T ManhattanDistance<T>(ref readonly Point<T> from, ref readonly Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => T.Abs(to.X - from.X) + T.Abs(to.Y - from.Y) + T.Abs(to.Z - from.Z);

    /// <summary>
    /// Converts a Rectangular 3D point to spherical coordinates.
    /// </summary>
    /// <typeparam name="T">The type of the point coordinates, which must adhere to IEEE 754 standards and provide min/max values.</typeparam>
    /// <param name="point">The Rectangular 3D point to convert.</param>
    /// <returns>The spherical coordinates representing the point.</returns>
    /// <remarks>
    /// If the type of point to convert doesn't meet the constraints, please convert it first to a suitable type using one of the conversion methods: 
    /// - <see cref="Point{T}.CreateChecked{TOther}(ref readonly Point{TOther})"/>
    /// - <see cref="Point{T}.CreateSaturating{TOther}(ref readonly Point{TOther})"/>
    /// - <see cref="Point{T}.CreateTruncating{TOther}(ref readonly Point{TOther})"/>
    /// </remarks>
    public static Spherical.Point<Radians, T> ToSpherical<T>(Point<T> point)
        where T : struct, IFloatingPointIeee754<T>, IMinMaxValue<T>
    {
        var radius = Utils.Magnitude(point.X, point.Y, point.Z);
        if (radius == T.Zero)
            return Spherical.Point<Radians, T>.Zero;

        var azimuth = Angle.Atan2(point.Y, point.X);
        var polar = Angle.Acos(point.Z / radius);

        return new(radius, azimuth, polar);
    }
}
