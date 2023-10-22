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
public readonly record struct PointReduced<TRadius, TAngleUnits, TAngle>(TRadius Radius, AngleReduced<TAngleUnits, TAngle> Azimuth, AngleReduced<TAngleUnits, TAngle> Polar)
    : IPoint<PointReduced<TRadius, TAngleUnits, TAngle>>
    where TRadius : struct, INumber<TRadius>, IMinMaxValue<TRadius>
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
{
    public AngleReduced<TAngleUnits, TAngle> Polar { get; }
        = Polar.Value < Angle<TAngleUnits, TAngle>.Zero.Value || Polar.Value > Angle<TAngleUnits, TAngle>.Straight.Value
            ? Throw.ArgumentOutOfRangeException<AngleReduced<TAngleUnits, TAngle>>(nameof(Polar), Polar, "Polar must be in [0.0º, 180.0º]")
            : Polar;

    #region constants

    /// <summary>
    /// Represents the zero value. This field is read-only.
    /// </summary>
    public static readonly PointReduced<TRadius, TAngleUnits, TAngle> Zero = new(TRadius.Zero, Angle<TAngleUnits, TAngle>.Zero, Angle<TAngleUnits, TAngle>.Zero);

    static PointReduced<TRadius, TAngleUnits, TAngle> INumericBase<PointReduced<TRadius, TAngleUnits, TAngle>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly PointReduced<TRadius, TAngleUnits, TAngle> MinValue = new(TRadius.MinValue, AngleReduced<TAngleUnits, TAngle>.MinValue, AngleReduced<TAngleUnits, TAngle>.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly PointReduced<TRadius, TAngleUnits, TAngle> MaxValue = new(TRadius.MaxValue, AngleReduced<TAngleUnits, TAngle>.MaxValue, Angle<TAngleUnits, TAngle>.Straight);

    static PointReduced<TRadius, TAngleUnits, TAngle> IMinMaxValue<PointReduced<TRadius, TAngleUnits, TAngle>>.MinValue
        => MinValue;
    static PointReduced<TRadius, TAngleUnits, TAngle> IMinMaxValue<PointReduced<TRadius, TAngleUnits, TAngle>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TRadius, TAngleUnits, TAngle> CoordinateSystem
        => new();
    ICoordinateSystem IPoint<PointReduced<TRadius, TAngleUnits, TAngle>>.CoordinateSystem
        => CoordinateSystem;

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TRadiusOther">The type of the radius coordinate of <paramref name="point"/>.</typeparam>
    /// <typeparam name="TAngleOther">The type used by the angle of the azimuth and polar coordinate of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/></param>
    /// <returns>An instance of <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> or <typeparamref name="TRadiusOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/>.</exception>
    public static PointReduced<TRadius, TAngleUnits, TAngle> CreateChecked<TRadiusOther, TAngleOther>(in PointReduced<TRadiusOther, TAngleUnits, TAngleOther> point)
        where TRadiusOther : struct, IFloatingPoint<TRadiusOther>, IMinMaxValue<TRadiusOther>
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        => new(
            TRadius.CreateChecked(point.Radius),
            AngleReduced<TAngleUnits, TAngle>.CreateChecked(point.Azimuth),
            AngleReduced<TAngleUnits, TAngle>.CreateChecked(point.Polar));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TRadiusOther">The type of the radius coordinate of <paramref name="point"/>.</typeparam>
    /// <typeparam name="TAngleOther">The type used by the angle of the azimuth and polar coordinate of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/></param>
    /// <returns>An instance of <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> or <typeparamref name="TRadiusOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/>.</exception>
    public static PointReduced<TRadius, TAngleUnits, TAngle> CreateSaturating<TRadiusOther, TAngleOther>(in PointReduced<TRadiusOther, TAngleUnits, TAngleOther> point)
        where TRadiusOther : struct, IFloatingPoint<TRadiusOther>, IMinMaxValue<TRadiusOther>
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        => new(
            TRadius.CreateSaturating(point.Radius),
            AngleReduced<TAngleUnits, TAngle>.CreateSaturating(point.Azimuth),
            AngleReduced<TAngleUnits, TAngle>.CreateSaturating(point.Polar));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TRadiusOther">The type of the radius coordinate of <paramref name="point"/>.</typeparam>
    /// <typeparam name="TAngleOther">The type used by the angle of the azimuth and polar coordinate of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/></param>
    /// <returns>An instance of <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> or <typeparamref name="TRadiusOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/>.</exception>
    public static PointReduced<TRadius, TAngleUnits, TAngle> CreateTruncating<TRadiusOther, TAngleOther>(in PointReduced<TRadiusOther, TAngleUnits, TAngleOther> point)
        where TRadiusOther : struct, IFloatingPoint<TRadiusOther>, IMinMaxValue<TRadiusOther>
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        => new(
            TRadius.CreateTruncating(point.Radius),
            AngleReduced<TAngleUnits, TAngle>.CreateTruncating(point.Azimuth),
            AngleReduced<TAngleUnits, TAngle>.CreateTruncating(point.Polar));

    /// <summary>
    /// Implicitly converts a <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/> to a <see cref="Point{TAngleUnits, TAngle, TRadius}"/>.
    /// </summary>
    /// <param name="angle">The <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/> to convert.</param>
    /// <returns>A new <see cref="Point{TAngleUnits, TAngle, TRadius}"/> with the same azimuth, polar and radius as the input angle.</returns>
    public static implicit operator Point<TRadius, TAngleUnits, TAngle>(PointReduced<TRadius, TAngleUnits, TAngle> angle)
        => new(angle.Radius, angle.Azimuth, angle.Polar);

    object IPoint<PointReduced<TRadius, TAngleUnits, TAngle>>.this[int index]
        => index switch
        {
            0 => Radius,
            1 => Azimuth,
            2 => Polar,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}

public static partial class Point
{
    /// <summary>
    /// Converts a point from degrees to radians.
    /// </summary>
    /// <typeparam name="TRadius">The type of the radius value.</typeparam>
    /// <typeparam name="TAngle">The type of the angle values.</typeparam>
    /// <param name="point">The input point in degrees.</param>
    /// <returns>The converted point in radians.</returns>    
    public static PointReduced<TRadius, Radians, TAngle> ToRadians<TRadius, TAngle>(PointReduced<TRadius, Degrees, TAngle> point)
        where TRadius : struct, INumber<TRadius>, IMinMaxValue<TRadius>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from degrees to gradians.
    /// </summary>
    /// <typeparam name="TRadius">The type of the radius value.</typeparam>
    /// <typeparam name="TAngle">The type of the angle values.</typeparam>
    /// <param name="point">The input point in degrees.</param>
    /// <returns>The converted point in gradians.</returns>
    public static PointReduced<TRadius, Gradians, TAngle> ToGradians<TRadius, TAngle>(PointReduced<TRadius, Degrees, TAngle> point)
        where TRadius : struct, INumber<TRadius>, IMinMaxValue<TRadius>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));

    /// <summary>
    /// Convert a point from degrees to revolutions.
    /// </summary>
    /// <typeparam name="TRadius">The type of the radius value.</typeparam>
    /// <typeparam name="TAngle">The type of the angle values.</typeparam>
    /// <param name="point">The input point in degrees.</param>
    /// <returns>The converted point in revolutions.</returns>
    public static PointReduced<TRadius, Revolutions, TAngle> ToRevolutions<TRadius, TAngle>(PointReduced<TRadius, Degrees, TAngle> point)
        where TRadius : struct, INumber<TRadius>, IMinMaxValue<TRadius>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from radians to degrees.
    /// </summary>
    /// <typeparam name="TRadius">The type of the radius value.</typeparam>
    /// <typeparam name="TAngle">The type of the angle values.</typeparam>
    /// <param name="point">The input point in radians.</param>
    /// <returns>The converted point in degrees.</returns>
    public static PointReduced<TRadius, Degrees, TAngle> ToDegrees<TRadius, TAngle>(PointReduced<TRadius, Radians, TAngle> point)
        where TRadius : struct, INumber<TRadius>, IMinMaxValue<TRadius>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from radians to gradians.
    /// </summary>
    /// <typeparam name="TRadius">The type of the radius value.</typeparam>
    /// <typeparam name="TAngle">The type of the angle values.</typeparam>
    /// <param name="point">The input point in radians.</param>
    /// <returns>The converted point in gradians.</returns>
    public static PointReduced<TRadius, Gradians, TAngle> ToGradians<TRadius, TAngle>(PointReduced<TRadius, Radians, TAngle> point)
        where TRadius : struct, INumber<TRadius>, IMinMaxValue<TRadius>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));

    /// <summary>
    /// Convert a point from radians to revolutions.
    /// </summary>
    /// <typeparam name="TRadius">The type of the radius value.</typeparam>
    /// <typeparam name="TAngle">The type of the angle values.</typeparam>
    /// <param name="point">The input point in radians.</param>
    /// <returns>The converted point in revolutions.</returns>
    public static PointReduced<TRadius, Revolutions, TAngle> ToRevolutions<TRadius, TAngle>(PointReduced<TRadius, Radians, TAngle> point)
        where TRadius : struct, INumber<TRadius>, IMinMaxValue<TRadius>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from gradians to degrees.
    /// </summary>
    /// <typeparam name="TRadius">The type of the radius value.</typeparam>
    /// <typeparam name="TAngle">The type of the angle values.</typeparam>
    /// <param name="point">The input point in gradians.</param>
    /// <returns>The converted point in degrees.</returns>
    public static PointReduced<TRadius, Degrees, TAngle> ToDegrees<TRadius, TAngle>(PointReduced<TRadius, Gradians, TAngle> point)
        where TRadius : struct, INumber<TRadius>, IMinMaxValue<TRadius>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from gradians to radians.
    /// </summary>
    /// <typeparam name="TRadius">The type of the radius value.</typeparam>
    /// <typeparam name="TAngle">The type of the angle values.</typeparam>
    /// <param name="point">The input point in gradians.</param>
    /// <returns>The converted point in radians.</returns>
    public static PointReduced<TRadius, Radians, TAngle> ToRadians<TRadius, TAngle>(PointReduced<TRadius, Gradians, TAngle> point)
        where TRadius : struct, INumber<TRadius>, IMinMaxValue<TRadius>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from gradians to revolutions.
    /// </summary>
    /// <typeparam name="TRadius">The type of the radius value.</typeparam>
    /// <typeparam name="TAngle">The type of the angle values.</typeparam>
    /// <param name="point">The input point in gradians.</param>
    /// <returns>The converted point in revolutions.</returns>
    public static PointReduced<TRadius, Revolutions, TAngle> ToRevolutions<TRadius, TAngle>(PointReduced<TRadius, Gradians, TAngle> point)
        where TRadius : struct, INumber<TRadius>, IMinMaxValue<TRadius>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to degrees.
    /// </summary>
    /// <typeparam name="TRadius">The type of the radius value.</typeparam>
    /// <typeparam name="TAngle">The type of the angle values.</typeparam>
    /// <param name="point">The input point in revolutions.</param>
    /// <returns>The converted point in degrees.</returns>
    public static PointReduced<TRadius, Degrees, TAngle> ToDegrees<TRadius, TAngle>(PointReduced<TRadius, Revolutions, TAngle> point)
        where TRadius : struct, INumber<TRadius>, IMinMaxValue<TRadius>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to radians.
    /// </summary>
    /// <typeparam name="TRadius">The type of the radius value.</typeparam>
    /// <typeparam name="TAngle">The type of the angle values.</typeparam>
    /// <param name="point">The input point in revolutions.</param>
    /// <returns>The converted point in radians.</returns>
    public static PointReduced<TRadius, Radians, TAngle> ToRadians<TRadius, TAngle>(PointReduced<TRadius, Revolutions, TAngle> point)
        where TRadius : struct, INumber<TRadius>, IMinMaxValue<TRadius>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to gradians.
    /// </summary>
    /// <typeparam name="TRadius">The type of the radius value.</typeparam>
    /// <typeparam name="TAngle">The type of the angle values.</typeparam>
    /// <param name="point">The input point in revolutions.</param>
    /// <returns>The converted point in gradians.</returns>
    public static PointReduced<TRadius, Gradians, TAngle> ToGradians<TRadius, TAngle>(PointReduced<TRadius, Revolutions, TAngle> point)
        where TRadius : struct, INumber<TRadius>, IMinMaxValue<TRadius>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));
}
