namespace NetFabric.Numerics.Spherical;

/// <summary>
/// Represents a point as an immutable struct.
/// </summary>
/// <typeparam name="TAngleUnits">The angle units used for the azimuth and zenith coordinates.</typeparam>
/// <typeparam name="TAngle">The type used by the angle of the azimuth and zenith coordinates.</typeparam>
/// <typeparam name="TRadius">The type of the radius coordinate.</typeparam>
/// <param name="Azimuth">The azimuth coordinate.</param>
/// <param name="Zenith">The zenith coordinate.</param>
/// <param name="Radius">The radius coordinate.</param>
[System.Diagnostics.DebuggerDisplay("Radius = {Radius}, Azimuth = {Azimuth}, Zenith = {Zenith}")]
[SkipLocalsInit]
public readonly record struct PointReduced<TAngleUnits, TAngle, TRadius>(TRadius Radius, AngleReduced<TAngleUnits, TAngle> Azimuth, AngleReduced<TAngleUnits, TAngle> Zenith)
    : IPoint<PointReduced<TAngleUnits, TAngle, TRadius>>
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
    where TRadius : struct, IFloatingPoint<TRadius>, IMinMaxValue<TRadius>
{
    public AngleReduced<TAngleUnits, TAngle> Zenith { get; }
        = Zenith.Value < Angle<TAngleUnits, TAngle>.Zero.Value || Zenith.Value > Angle<TAngleUnits, TAngle>.Straight.Value
            ? Throw.ArgumentOutOfRangeException<AngleReduced<TAngleUnits, TAngle>>(nameof(Zenith), Zenith, "Zenith must be in [0.0�, 180.0�]")
            : Zenith;

    #region constants

    /// <summary>
    /// Represents the zero value. This field is read-only.
    /// </summary>
    public static readonly PointReduced<TAngleUnits, TAngle, TRadius> Zero = new(TRadius.Zero, Angle<TAngleUnits, TAngle>.Zero, Angle<TAngleUnits, TAngle>.Zero);

    static PointReduced<TAngleUnits, TAngle, TRadius> INumericBase<PointReduced<TAngleUnits, TAngle, TRadius>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly PointReduced<TAngleUnits, TAngle, TRadius> MinValue = new(TRadius.MinValue, AngleReduced<TAngleUnits, TAngle>.MinValue, AngleReduced<TAngleUnits, TAngle>.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly PointReduced<TAngleUnits, TAngle, TRadius> MaxValue = new(TRadius.MaxValue, AngleReduced<TAngleUnits, TAngle>.MaxValue, Angle<TAngleUnits, TAngle>.Straight);

    static PointReduced<TAngleUnits, TAngle, TRadius> IMinMaxValue<PointReduced<TAngleUnits, TAngle, TRadius>>.MinValue
        => MinValue;
    static PointReduced<TAngleUnits, TAngle, TRadius> IMinMaxValue<PointReduced<TAngleUnits, TAngle, TRadius>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TAngleUnits, TAngle, TRadius> CoordinateSystem
        => new();
    ICoordinateSystem IPoint<PointReduced<TAngleUnits, TAngle, TRadius>>.CoordinateSystem
        => CoordinateSystem;

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type used by the angle of the azimuth and zenith coordinate of <paramref name="point"/>.</typeparam>
    /// <typeparam name="TRadiusOther">The type of the radius coordinate of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/></param>
    /// <returns>An instance of <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> or <typeparamref name="TRadiusOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/>.</exception>
    public static PointReduced<TAngleUnits, TAngle, TRadius> CreateChecked<TAngleOther, TRadiusOther>(in PointReduced<TAngleUnits, TAngleOther, TRadiusOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where TRadiusOther : struct, IFloatingPoint<TRadiusOther>, IMinMaxValue<TRadiusOther>
        => new(
            TRadius.CreateChecked(point.Radius)
,
            AngleReduced<TAngleUnits, TAngle>.CreateChecked(point.Azimuth),
            AngleReduced<TAngleUnits, TAngle>.CreateChecked(point.Zenith));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type used by the angle of the azimuth and zenith coordinate of <paramref name="point"/>.</typeparam>
    /// <typeparam name="TRadiusOther">The type of the radius coordinate of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/></param>
    /// <returns>An instance of <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> or <typeparamref name="TRadiusOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/>.</exception>
    public static PointReduced<TAngleUnits, TAngle, TRadius> CreateSaturating<TAngleOther, TRadiusOther>(in PointReduced<TAngleUnits, TAngleOther, TRadiusOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where TRadiusOther : struct, IFloatingPoint<TRadiusOther>, IMinMaxValue<TRadiusOther>
        => new(
            TRadius.CreateSaturating(point.Radius)
,
            AngleReduced<TAngleUnits, TAngle>.CreateSaturating(point.Azimuth),
            AngleReduced<TAngleUnits, TAngle>.CreateSaturating(point.Zenith));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type used by the angle of the azimuth and zenith coordinate of <paramref name="point"/>.</typeparam>
    /// <typeparam name="TRadiusOther">The type of the radius coordinate of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/></param>
    /// <returns>An instance of <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> or <typeparamref name="TRadiusOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/>.</exception>
    public static PointReduced<TAngleUnits, TAngle, TRadius> CreateTruncating<TAngleOther, TRadiusOther>(in PointReduced<TAngleUnits, TAngleOther, TRadiusOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where TRadiusOther : struct, IFloatingPoint<TRadiusOther>, IMinMaxValue<TRadiusOther>
        => new(
            TRadius.CreateTruncating(point.Radius)
,
            AngleReduced<TAngleUnits, TAngle>.CreateTruncating(point.Azimuth),
            AngleReduced<TAngleUnits, TAngle>.CreateTruncating(point.Zenith));

    /// <summary>
    /// Implicitly converts a <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/> to a <see cref="Point{TAngleUnits, TAngle, TRadius}"/>.
    /// </summary>
    /// <param name="angle">The <see cref="PointReduced{TAngleUnits, TAngle, TRadius}"/> to convert.</param>
    /// <returns>A new <see cref="Point{TAngleUnits, TAngle, TRadius}"/> with the same azimuth, zenith and radius as the input angle.</returns>
    public static implicit operator Point<TAngleUnits, TAngle, TRadius>(PointReduced<TAngleUnits, TAngle, TRadius> angle)
        => new(angle.Radius, angle.Azimuth, angle.Zenith);

    object IPoint<PointReduced<TAngleUnits, TAngle, TRadius>>.this[int index]
        => index switch
        {
            0 => Radius,
            1 => Azimuth,
            2 => Zenith,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}

/// <summary>
/// Provides static methods for point operations.
/// </summary>
public static partial class Point
{
    public static PointReduced<Degrees, T, T> ToDegrees<T>(PointReduced<Degrees, T, T> point)
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    => point;

    public static PointReduced<Radians, T, T> ToRadians<T>(PointReduced<Degrees, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Zenith));

    public static PointReduced<Gradians, T, T> ToGradians<T>(PointReduced<Degrees, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Zenith));

    public static PointReduced<Revolutions, T, T> ToRevolutions<T>(PointReduced<Degrees, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Zenith));

    public static PointReduced<Degrees, T, T> ToDegrees<T>(PointReduced<Radians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Zenith));

    public static PointReduced<Radians, T, T> ToRadians<T>(PointReduced<Radians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => point;

    public static PointReduced<Gradians, T, T> ToGradians<T>(PointReduced<Radians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Zenith));

    public static PointReduced<Revolutions, T, T> ToRevolutions<T>(PointReduced<Radians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Zenith));

    public static PointReduced<Degrees, T, T> ToDegrees<T>(PointReduced<Gradians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Zenith));

    public static PointReduced<Radians, T, T> ToRadians<T>(PointReduced<Gradians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Zenith));

    public static PointReduced<Gradians, T, T> ToGradians<T>(PointReduced<Gradians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => point;

    public static PointReduced<Revolutions, T, T> ToRevolutions<T>(PointReduced<Gradians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Zenith));

    public static PointReduced<Degrees, T, T> ToDegrees<T>(PointReduced<Revolutions, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Zenith));

    public static PointReduced<Radians, T, T> ToRadians<T>(PointReduced<Revolutions, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Zenith));

    public static PointReduced<Gradians, T, T> ToGradians<T>(PointReduced<Revolutions, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Zenith));

    public static PointReduced<Revolutions, T, T> ToRevolutions<T>(PointReduced<Revolutions, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => point;

    public static PointReduced<TAngleUnits, TAngle, TRadius> Reduce<TAngleUnits, TAngle, TRadius>(PointReduced<TAngleUnits, TAngle, TRadius> point)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        where TRadius : struct, IFloatingPoint<TRadius>, IMinMaxValue<TRadius>
        => point;
}
