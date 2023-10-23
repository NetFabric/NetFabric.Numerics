using System;

namespace NetFabric.Numerics.Spherical;

/// <summary>
/// Represents a point as an immutable struct.
/// </summary>
/// <typeparam name="TUnits">The units used for the angles.</typeparam>
/// <typeparam name="T">The type used for the coordinates.</typeparam>
[System.Diagnostics.DebuggerDisplay("Radius = {Radius}, Azimuth = {Azimuth}, Polar = {Polar}")]
[SkipLocalsInit]
public readonly struct Point<TUnits, T>
    : IPoint<Point<TUnits, T>>
    where TUnits : struct, IAngleUnits<TUnits>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    public T Radius { get; }

    public Angle<TUnits, T> Azimuth { get; }

    public Angle<TUnits, T> Polar { get; }

    /// <summary>
    /// Creates an instance of the current type from spherical coordinates.
    /// </summary>
    /// <param name="radius">The radial distance from the origin (usually the radial distance).</param>
    /// <param name="azimuth">The horizontal angle in radians with units defined by <typeparamref name="TUnits"/> (often called the azimuth angle).</param>
    /// <param name="polar">The vertical angle in radians with units defined by <typeparamref name="TUnits"/> (frequently referred to as the polar or zenith angle).</param>
    /// <remarks>
    /// These parameters collectively define the position of a 3D point in space based on spherical coordinates.
    /// </remarks>
    public Point(T radius, Angle<TUnits, T> azimuth, Angle<TUnits, T> polar)
    {
        Radius = radius;
        Azimuth = azimuth;
        Polar = polar;
    }

    #region constants

    public static readonly PointReduced<TUnits, T> Zero = new(T.Zero, Angle<TUnits, T>.Zero, Angle<TUnits, T>.Zero);

    static Point<TUnits, T> INumericBase<Point<TUnits, T>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<TUnits, T> MinValue = new(T.MinValue, Angle<TUnits, T>.MinValue, Angle<TUnits, T>.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<TUnits, T> MaxValue = new(T.MaxValue, Angle<TUnits, T>.MaxValue, Angle<TUnits, T>.MaxValue);

    static Point<TUnits, T> IMinMaxValue<Point<TUnits, T>>.MinValue
        => MinValue;
    static Point<TUnits, T> IMinMaxValue<Point<TUnits, T>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TUnits, T> CoordinateSystem 
        => new();
    ICoordinateSystem IPoint<Point<TUnits, T>>.CoordinateSystem 
        => CoordinateSystem;

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TUnits, T}"/></param>
    /// <returns>An instance of <see cref="Point{TUnits, T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TUnits, T}"/>.</exception>
    public static Point<TUnits, T> CreateChecked<TOther>(in Point<TUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateChecked(point.Radius),
            Angle<TUnits, T>.CreateChecked(point.Azimuth),
            Angle<TUnits, T>.CreateChecked(point.Polar));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TUnits, T}"/></param>
    /// <returns>An instance of <see cref="Point{TUnits, T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TUnits, T}"/>.</exception>
    public static Point<TUnits, T> CreateSaturating<TOther>(in Point<TUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateSaturating(point.Radius),
            Angle<TUnits, T>.CreateSaturating(point.Azimuth),
            Angle<TUnits, T>.CreateSaturating(point.Polar));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TUnits, T}"/></param>
    /// <returns>An instance of <see cref="Point{TUnits, T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TUnits, T}"/>.</exception>
    public static Point<TUnits, T> CreateTruncating<TOther>(in Point<TUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateTruncating(point.Radius),
            Angle<TUnits, T>.CreateTruncating(point.Azimuth),
            Angle<TUnits, T>.CreateTruncating(point.Polar));

    object IPoint<Point<TUnits, T>>.this[int index] 
        => index switch
        {
            0 => Radius,
            1 => Azimuth,
            2 => Polar,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };

    #region equality

    /// <summary>
    /// Indicates whether two <see cref="Point{TUnits, T}"/> instances are equal.
    /// </summary>
    /// <param name="left">The first point to compare.</param>
    /// <param name="right">The second point to compare.</param>
    /// <returns>true if the two points are equal, false otherwise.</returns>
    /// <remarks>
    /// The method compares the numerical values of the <paramref name="left"/> and <paramref name="right"/> points to determine their equality.
    /// </remarks>
    public static bool operator ==(Point<TUnits, T> left, Point<TUnits, T> right)
        => left.Equals(right);

    /// <summary>
    /// Indicates whether two <see cref="Point{TUnits, T}"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first point to compare.</param>
    /// <param name="right">The second point to compare.</param>
    /// <returns>true if the two points are not equal, false otherwise.</returns>
    /// <remarks>
    /// The method compares the numerical values of the <paramref name="left"/> and <paramref name="right"/> points to determine their equality.
    /// </remarks>
    public static bool operator !=(Point<TUnits, T> left, Point<TUnits, T> right)
        => !left.Equals(right);

    /// <summary>
    /// Returns the hash code for the current <see cref="Point{TUnits, T}"/> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
        => HashCode.Combine(Radius, Azimuth, Polar);

    /// <summary>
    /// Indicates whether the current <see cref="Point{TUnits, T}"/> instance is equal to another <see cref="Point{TUnits, T}"/> instance.
    /// </summary>
    /// <param name="other">A <see cref="Point{TUnits, T}"/> value to compare to this instance.</param>
    /// <returns>true if <paramref name="other"/> has the same value as this instance; otherwise, false.</returns>
    public bool Equals(Point<TUnits, T> other)
        => EqualityComparer<T>.Default.Equals(Radius, other.Radius) &&
            Azimuth.Equals(other.Azimuth) &&
            Polar.Equals(other.Polar);

    /// <summary>
    /// Indicates whether the current <see cref="Point{TUnits, T}"/> instance is equal to another <see cref="PointReduced{TUnits, T}"/> instance.
    /// </summary>
    /// <param name="other">A <see cref="PointReduced{TUnits, T}"/> value to compare to this instance.</param>
    /// <returns>true if <paramref name="other"/> has the same value as this instance; otherwise, false.
    public bool Equals(PointReduced<TUnits, T> other)
        => EqualityComparer<T>.Default.Equals(Radius, other.Radius) &&
            Azimuth.Equals(other.Azimuth) &&
            Polar.Equals(other.Polar);

    /// <summary>
    /// Indicates whether the current <see cref="Point{TUnits, T}"/> instance is equal to another object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>
    /// true if <paramref name="obj"/> is an instance of a <see cref="Point{TUnits, T}"/> or 
    /// <see cref="PointReduced{TUnits, T}"/> and equals the value of this instance; otherwise, false.
    /// </returns>
    public override bool Equals(object? obj)
        => obj switch
        {
            Point<TUnits, T> point => Equals(point),
            PointReduced<TUnits, T> point => Equals(point),
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
    /// Converts a point from degrees to radians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in degrees.</param>
    /// <returns>The converted point in radians.</returns>    
    public static Point<Radians, T> ToRadians<T>(Point<Degrees, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from degrees to gradians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in degrees.</param>
    /// <returns>The converted point in gradians.</returns>
    public static Point<Gradians, T> ToGradians<T>(Point<Degrees, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));

    /// <summary>
    /// Convert a point from degrees to revolutions.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in degrees.</param>
    /// <returns>The converted point in revolutions.</returns>
    public static Point<Revolutions, T> ToRevolutions<T>(Point<Degrees, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from radians to degrees.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in radians.</param>
    /// <returns>The converted point in degrees.</returns>
    public static Point<Degrees, T> ToDegrees<T>(Point<Radians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from radians to gradians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in radians.</param>
    /// <returns>The converted point in gradians.</returns>
    public static Point<Gradians, T> ToGradians<T>(Point<Radians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));

    /// <summary>
    /// Convert a point from radians to revolutions.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in radians.</param>
    /// <returns>The converted point in revolutions.</returns>
    public static Point<Revolutions, T> ToRevolutions<T>(Point<Radians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from gradians to degrees.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in gradians.</param>
    /// <returns>The converted point in degrees.</returns>
    public static Point<Degrees, T> ToDegrees<T>(Point<Gradians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from gradians to radians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in gradians.</param>
    /// <returns>The converted point in radians.</returns>
    public static Point<Radians, T> ToRadians<T>(Point<Gradians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from gradians to revolutions.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in gradians.</param>
    /// <returns>The converted point in revolutions.</returns>
    public static Point<Revolutions, T> ToRevolutions<T>(Point<Gradians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to degrees.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in revolutions.</param>
    /// <returns>The converted point in degrees.</returns>
    public static Point<Degrees, T> ToDegrees<T>(Point<Revolutions, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to radians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in revolutions.</param>
    /// <returns>The converted point in radians.</returns>
    public static Point<Radians, T> ToRadians<T>(Point<Revolutions, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to gradians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in revolutions.</param>
    /// <returns>The converted point in gradians.</returns>
    public static Point<Gradians, T> ToGradians<T>(Point<Revolutions, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));

    /// <summary>
    /// Reduces a point in spherical coordinates by applying reduction functions to its components.
    /// </summary>
    /// <typeparam name="TUnits">The type representing angle units.</typeparam>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in spherical coordinates to be reduced.</param>
    /// <returns>
    /// A new <see cref="PointReduced{TUnits, T}"/> object with a reduced
    /// azimuthal angle and polar angle, while keeping the original radius.
    /// </returns>
    /// <remarks>
    /// The reduction process involves applying reduction functions to the azimuthal and polar angles
    /// of the input spherical point. The radius component remains unchanged.
    /// </remarks>
    public static PointReduced<TUnits, T> Reduce<TUnits, T>(Point<TUnits, T> point)
        where TUnits : struct, IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        var azimuth = Angle.Reduce(point.Azimuth);
        var polar = Angle.Reduce(point.Polar);
        if (polar > Angle<TUnits, T>.Straight)
        {
            polar = new(Angle<TUnits, T>.Full.Value - polar.Value);
        }
        return new(point.Radius, azimuth, polar);
    }

    /// <summary>
    /// Converts a point in spherical coordinates to cartesian 3D coordinates.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The point in spherical coordinates to convert.</param>
    /// <returns>The cartesian 3D coordinates representing the point.</returns>
    /// <remarks>
    /// If the type of point to convert doesn't meet the constraints, please convert it first to a suitable type using one of the conversion methods: 
    /// - <see cref="CreateChecked"/>
    /// - <see cref="CreateSaturating"/>
    /// - <see cref="CreateTruncating"/>
    /// </remarks>
    public static Cartesian3.Point<T> ToCartesian<T>(Point<Radians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var (sinAzimuth, cosAzimuth) = Angle.SinCos(point.Azimuth);
        var (sinPolar, cosPolar) = Angle.SinCos(point.Polar);

        var x = point.Radius * sinPolar * cosAzimuth;
        var y = point.Radius * sinPolar * sinAzimuth;
        var z = point.Radius * cosPolar;

        return new(x, y, z);
    }
}