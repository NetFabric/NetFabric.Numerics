namespace NetFabric.Numerics.Spherical;

/// <summary>
/// Represents a point as an immutable struct.
/// </summary>
/// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
/// <typeparam name="TAngle">The type used by the angle of the azimuth and polar coordinates.</typeparam>
/// <typeparam name="TRadius">The type of the radius coordinate.</typeparam>
/// <param name="Azimuth">The azimuth coordinate.</param>
/// <param name="Polar">The polar coordinate.</param>
/// <param name="Radius">The radius coordinate.</param>
[System.Diagnostics.DebuggerDisplay("Radius = {Radius}, Azimuth = {Azimuth}, Polar = {Polar}")]
[SkipLocalsInit]
public readonly struct Point<TAngleUnits, TAngle, TRadius>
    : IPoint<Point<TAngleUnits, TAngle, TRadius>>
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
    where TRadius : struct, IFloatingPoint<TRadius>, IMinMaxValue<TRadius>
{
    public TRadius Radius { get; }

    public Angle<TAngleUnits, TAngle> Azimuth { get; }

    public Angle<TAngleUnits, TAngle> Polar { get; }

    /// <summary>
    /// Creates an instance of the current type from spherical coordinates.
    /// </summary>
    /// <param name="radius">The radial distance from the origin (usually the radial distance).</param>
    /// <param name="azimuth">The horizontal angle in radians with units defined by <typeparamref name="TUnits"/> (often called the azimuth angle).</param>
    /// <param name="polar">The vertical angle in radians with units defined by <typeparamref name="TUnits"/> (frequently referred to as the polar or zenith angle).</param>
    /// <remarks>
    /// These parameters collectively define the position of a 3D point in space based on spherical coordinates.
    /// </remarks>
    public Point(TRadius radius, Angle<TAngleUnits, TAngle> azimuth, Angle<TAngleUnits, TAngle> polar)
    {
        Radius = radius;
        Azimuth = azimuth;
        Polar = polar;
    }

    #region constants

    public static readonly PointReduced<TAngleUnits, TAngle, TRadius> Zero = new(TRadius.Zero, Angle<TAngleUnits, TAngle>.Zero, Angle<TAngleUnits, TAngle>.Zero);

    static Point<TAngleUnits, TAngle, TRadius> INumericBase<Point<TAngleUnits, TAngle, TRadius>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<TAngleUnits, TAngle, TRadius> MinValue = new(TRadius.MinValue, Angle<TAngleUnits, TAngle>.MinValue, Angle<TAngleUnits, TAngle>.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<TAngleUnits, TAngle, TRadius> MaxValue = new(TRadius.MaxValue, Angle<TAngleUnits, TAngle>.MaxValue, Angle<TAngleUnits, TAngle>.MaxValue);

    static Point<TAngleUnits, TAngle, TRadius> IMinMaxValue<Point<TAngleUnits, TAngle, TRadius>>.MinValue
        => MinValue;
    static Point<TAngleUnits, TAngle, TRadius> IMinMaxValue<Point<TAngleUnits, TAngle, TRadius>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TAngleUnits, TAngle, TRadius> CoordinateSystem 
        => new();
    ICoordinateSystem IPoint<Point<TAngleUnits, TAngle, TRadius>>.CoordinateSystem 
        => CoordinateSystem;

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type used by the angle of the azimuth and polar coordinates of <paramref name="point"/>.</typeparam>
    /// <typeparam name="TRadiusOther">The type of the radius coordinate of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TAngleUnits, TAngle, TRadius}"/></param>
    /// <returns>An instance of <see cref="Point{TAngleUnits, TAngle, TRadius}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> or <typeparamref name="TRadiusOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TAngleUnits, TAngle, TRadius}"/>.</exception>
    public static Point<TAngleUnits, TAngle, TRadius> CreateChecked<TAngleOther, TRadiusOther>(in Point<TAngleUnits, TAngleOther, TRadiusOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where TRadiusOther : struct, IFloatingPoint<TRadiusOther>, IMinMaxValue<TRadiusOther>
        => new(
            TRadius.CreateChecked(point.Radius),
            Angle<TAngleUnits, TAngle>.CreateChecked(point.Azimuth),
            Angle<TAngleUnits, TAngle>.CreateChecked(point.Polar));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type used by the angle of the azimuth and polar coordinates of <paramref name="point"/>.</typeparam>
    /// <typeparam name="TRadiusOther">The type of the radius coordinate of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TAngleUnits, TAngle, TRadius}"/></param>
    /// <returns>An instance of <see cref="Point{TAngleUnits, TAngle, TRadius}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> or <typeparamref name="TRadiusOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TAngleUnits, TAngle, TRadius}"/>.</exception>
    public static Point<TAngleUnits, TAngle, TRadius> CreateSaturating<TAngleOther, TRadiusOther>(in Point<TAngleUnits, TAngleOther, TRadiusOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where TRadiusOther : struct, IFloatingPoint<TRadiusOther>, IMinMaxValue<TRadiusOther>
        => new(
            TRadius.CreateSaturating(point.Radius),
            Angle<TAngleUnits, TAngle>.CreateSaturating(point.Azimuth),
            Angle<TAngleUnits, TAngle>.CreateSaturating(point.Polar));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type used by the angle of the azimuth and polar coordinates of <paramref name="point"/>.</typeparam>
    /// <typeparam name="TRadiusOther">The type of the radius coordinate of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TAngleUnits, TAngle, TRadius}"/></param>
    /// <returns>An instance of <see cref="Point{TAngleUnits, TAngle, TRadius}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> or <typeparamref name="TRadiusOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TAngleUnits, TAngle, TRadius}"/>.</exception>
    public static Point<TAngleUnits, TAngle, TRadius> CreateTruncating<TAngleOther, TRadiusOther>(in Point<TAngleUnits, TAngleOther, TRadiusOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where TRadiusOther : struct, IFloatingPoint<TRadiusOther>, IMinMaxValue<TRadiusOther>
        => new(
            TRadius.CreateTruncating(point.Radius),
            Angle<TAngleUnits, TAngle>.CreateTruncating(point.Azimuth),
            Angle<TAngleUnits, TAngle>.CreateTruncating(point.Polar));

    object IPoint<Point<TAngleUnits, TAngle, TRadius>>.this[int index] 
        => index switch
        {
            0 => Radius,
            1 => Azimuth,
            2 => Polar,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };

    #region equality

    /// <summary>
    /// Indicates whether two <see cref="Point{TAngleUnits, TAngle, TRadius}"/> instances are equal.
    /// </summary>
    /// <param name="left">The first point to compare.</param>
    /// <param name="right">The second point to compare.</param>
    /// <returns>true if the two points are equal, false otherwise.</returns>
    /// <remarks>
    /// The method compares the numerical values of the <paramref name="left"/> and <paramref name="right"/> points to determine their equality.
    /// </remarks>
    public static bool operator ==(Point<TAngleUnits, TAngle, TRadius> left, Point<TAngleUnits, TAngle, TRadius> right)
        => left.Equals(right);

    /// <summary>
    /// Indicates whether two <see cref="Point{TAngleUnits, TAngle, TRadius}"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first point to compare.</param>
    /// <param name="right">The second point to compare.</param>
    /// <returns>true if the two points are not equal, false otherwise.</returns>
    /// <remarks>
    /// The method compares the numerical values of the <paramref name="left"/> and <paramref name="right"/> points to determine their equality.
    /// </remarks>
    public static bool operator !=(Point<TAngleUnits, TAngle, TRadius> left, Point<TAngleUnits, TAngle, TRadius> right)
        => !left.Equals(right);

    /// <summary>
    /// Returns the hash code for the current <see cref="Point{TAngleUnits, TAngle, TRadius}"/> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
        => HashCode.Combine(Radius, Azimuth, Polar);

    /// <summary>
    /// Indicates whether the current <see cref="Point{TAngleUnits, TAngle, TRadius}"/> instance is equal to another <see cref="Point{TAngleUnits, TAngle, TRadius}"/> instance.
    /// </summary>
    /// <param name="other">A <see cref="Point{TAngleUnits, TAngle, TRadius}"/> value to compare to this instance.</param>
    /// <returns>true if <paramref name="other"/> has the same value as this instance; otherwise, false.</returns>
    public bool Equals(Point<TAngleUnits, TAngle, TRadius> other)
        => EqualityComparer<TRadius>.Default.Equals(Radius, other.Radius) &&
            Azimuth.Equals(other.Azimuth) &&
            Polar.Equals(other.Polar);

    /// <summary>
    /// Indicates whether the current <see cref="Point{TAngleUnits, TAngle, TRadius}"/> instance is equal to another <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/> instance.
    /// </summary>
    /// <param name="other">A <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/> value to compare to this instance.</param>
    /// <returns>true if <paramref name="other"/> has the same value as this instance; otherwise, false.
    public bool Equals(PointReduced<TAngleUnits, TAngle, TRadius> other)
        => EqualityComparer<TRadius>.Default.Equals(Radius, other.Radius) &&
            Azimuth.Equals(other.Azimuth) &&
            Polar.Equals(other.Polar);

    /// <summary>
    /// Indicates whether the current <see cref="Point{TAngleUnits, TAngle, TRadius}"/> instance is equal to another object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>
    /// true if <paramref name="obj"/> is an instance of a <see cref="Point{TAngleUnits, TAngle, TRadius}"/> or 
    /// <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/> and equals the value of this instance; otherwise, false.
    /// </returns>
    public override bool Equals(object? obj)
        => obj switch
        {
            Point<TAngleUnits, TAngle, TRadius> point => Equals(point),
            PointReduced<TAngleUnits, TAngle, TRadius> point => Equals(point),
            _ => false
        };

    #endregion
}

/// <summary>
/// Provides static methods for point operations.
/// </summary>
public static partial class Point
{
    /// <summary>
    /// Convert a point from degrees to radians.
    /// </summary>
    /// <typeparam name="T">The generic type for floating-point calculations.</typeparam>
    /// <param name="point">The input point in degrees.</param>
    /// <returns>The converted point in radians.</returns>
    public static Point<Radians, T, T> ToRadians<T>(Point<Degrees, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from degrees to gradians.
    /// </summary>
    /// <typeparam name="T">The generic type for floating-point calculations.</typeparam>
    /// <param name="point">The input point in degrees.</param>
    /// <returns>The converted point in gradians.</returns>
    public static Point<Gradians, T, T> ToGradians<T>(Point<Degrees, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));

    /// <summary>
    /// Convert a point from degrees to revolutions.
    /// </summary>
    /// <typeparam name="T">The generic type for floating-point calculations.</typeparam>
    /// <param name="point">The input point in degrees.</param>
    /// <returns>The converted point in revolutions.</returns>
    public static Point<Revolutions, T, T> ToRevolutions<T>(Point<Degrees, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from radians to degrees.
    /// </summary>
    /// <typeparam name="T">The generic type for floating-point calculations.</typeparam>
    /// <param name="point">The input point in radians.</param>
    /// <returns>The converted point in degrees.</returns>
    public static Point<Degrees, T, T> ToDegrees<T>(Point<Radians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from radians to gradians.
    /// </summary>
    /// <typeparam name="T">The generic type for floating-point calculations.</typeparam>
    /// <param name="point">The input point in radians.</param>
    /// <returns>The converted point in gradians.</returns>
    public static Point<Gradians, T, T> ToGradians<T>(Point<Radians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));

    /// <summary>
    /// Convert a point from radians to revolutions.
    /// </summary>
    /// <typeparam name="T">The generic type for floating-point calculations.</typeparam>
    /// <param name="point">The input point in radians.</param>
    /// <returns>The converted point in revolutions.</returns>
    public static Point<Revolutions, T, T> ToRevolutions<T>(Point<Radians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from gradians to degrees.
    /// </summary>
    /// <typeparam name="T">The generic type for floating-point calculations.</typeparam>
    /// <param name="point">The input point in gradians.</param>
    /// <returns>The converted point in degrees.</returns>
    public static Point<Degrees, T, T> ToDegrees<T>(Point<Gradians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from gradians to radians.
    /// </summary>
    /// <typeparam name="T">The generic type for floating-point calculations.</typeparam>
    /// <param name="point">The input point in gradians.</param>
    /// <returns>The converted point in radians.</returns>
    public static Point<Radians, T, T> ToRadians<T>(Point<Gradians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from gradians to revolutions.
    /// </summary>
    /// <typeparam name="T">The generic type for floating-point calculations.</typeparam>
    /// <param name="point">The input point in gradians.</param>
    /// <returns>The converted point in revolutions.</returns>
    public static Point<Revolutions, T, T> ToRevolutions<T>(Point<Gradians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to degrees.
    /// </summary>
    /// <typeparam name="T">The generic type for floating-point calculations.</typeparam>
    /// <param name="point">The input point in revolutions.</param>
    /// <returns>The converted point in degrees.</returns>
    public static Point<Degrees, T, T> ToDegrees<T>(Point<Revolutions, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to radians.
    /// </summary>
    /// <typeparam name="T">The generic type for floating-point calculations.</typeparam>
    /// <param name="point">The input point in revolutions.</param>
    /// <returns>The converted point in radians.</returns>
    public static Point<Radians, T, T> ToRadians<T>(Point<Revolutions, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to gradians.
    /// </summary>
    /// <typeparam name="T">The generic type for floating-point calculations.</typeparam>
    /// <param name="point">The input point in revolutions.</param>
    /// <returns>The converted point in gradians.</returns>
    public static Point<Gradians, T, T> ToGradians<T>(Point<Revolutions, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));

    /// <summary>
    /// Reduces a point in spherical coordinates by applying reduction functions to its components.
    /// </summary>
    /// <typeparam name="TAngleUnits">The type representing angle units.</typeparam>
    /// <typeparam name="TAngle">The type representing angular values.</typeparam>
    /// <typeparam name="TRadius">The type representing radius values.</typeparam>
    /// <param name="point">The input point in spherical coordinates to be reduced.</param>
    /// <returns>
    /// A new <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/> object with a reduced
    /// azimuthal angle and polar angle, while keeping the original radius.
    /// </returns>
    /// <remarks>
    /// The reduction process involves applying reduction functions to the azimuthal and polar angles
    /// of the input spherical point. The radius component remains unchanged.
    /// </remarks>
    public static PointReduced<TAngleUnits, TAngle, TRadius> Reduce<TAngleUnits, TAngle, TRadius>(Point<TAngleUnits, TAngle, TRadius> point)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        where TRadius : struct, IFloatingPoint<TRadius>, IMinMaxValue<TRadius>
    {
        var azimuth = Angle.Reduce(point.Azimuth);
        var polar = Angle.Reduce(point.Polar);
        if (polar > Angle<TAngleUnits, TAngle>.Straight)
        {
            polar = new(Angle<TAngleUnits, TAngle>.Full.Value - polar.Value);
        }
        return new(point.Radius, azimuth, polar);
    }

    /// <summary>
    /// Converts a point in spherical coordinates to cartesian 3D coordinates.
    /// </summary>
    /// <typeparam name="T">The type of the coordinates of the point.</typeparam>
    /// <param name="point">The point in spherical coordinates to convert.</param>
    /// <returns>The cartesian 3D coordinates representing the point.</returns>
    public static Cartesian3.Point<T> ToCartesian<T>(Point<Radians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var sinAzimuth = Angle.Sin(point.Azimuth);
        var cosAzimuth = Angle.Cos(point.Azimuth);
        var sinPolar = Angle.Sin(point.Polar);
        var cosPolar = Angle.Cos(point.Polar);

        var x = T.CreateChecked(point.Radius * sinPolar * cosAzimuth);
        var y = T.CreateChecked(point.Radius * sinPolar * sinAzimuth);
        var z = T.CreateChecked(point.Radius * cosPolar);

        return new(x, y, z);
    }

    /// <summary>
    /// Converts a point in spherical coordinates to cartesian 3D coordinates.
    /// </summary>
    /// <typeparam name="TAngle">The type used by the angle of the azimuth and polar coordinates of <paramref name="point"/>.</typeparam>
    /// <typeparam name="TRadius">The type of the radius coordinate of <paramref name="point"/>.</typeparam>
    /// <typeparam name="T">The type used by the resulting cartesian point coordinates.</typeparam>
    /// <param name="point">The point in spherical coordinates to convert.</param>
    /// <returns>The cartesian 3D coordinates representing the point.</returns>
    public static Cartesian3.Point<T> ToCartesian<TAngle, TRadius, T>(Point<Radians, TAngle, TRadius> point)
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>, ITrigonometricFunctions<TAngle>
        where TRadius : struct, IFloatingPoint<TRadius>, IMinMaxValue<TRadius>
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        var sinAzimuth = Angle.Sin(point.Azimuth);
        var cosAzimuth = Angle.Cos(point.Azimuth);
        var sinPolar = Angle.Sin(point.Polar);
        var cosPolar = Angle.Cos(point.Polar);

        var x = T.CreateChecked(point.Radius * TRadius.CreateChecked(sinPolar * cosAzimuth));
        var y = T.CreateChecked(point.Radius * TRadius.CreateChecked(sinPolar * sinAzimuth));
        var z = T.CreateChecked(point.Radius * TRadius.CreateChecked(cosPolar));

        return new(x, y, z);
    }
}