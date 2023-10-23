namespace NetFabric.Numerics.Spherical;

/// <summary>
/// Represents a point as an immutable struct.
/// </summary>
/// <typeparam name="TUnits">The units used for the angles.</typeparam>
/// <typeparam name="T">The type used for the coordinates.</typeparam>
/// <param name="Azimuth">The azimuth coordinate.</param>
/// <param name="Polar">The polar coordinate.</param>
/// <param name="Radius">The radius coordinate.</param>
[System.Diagnostics.DebuggerDisplay("Radius = {Radius}, Azimuth = {Azimuth}, Polar = {Polar}")]
[SkipLocalsInit]
public readonly record struct PointReduced<TUnits, T>(T Radius, AngleReduced<TUnits, T> Azimuth, AngleReduced<TUnits, T> Polar)
    : IPoint<PointReduced<TUnits, T>>
    where TUnits : struct, IAngleUnits<TUnits>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    public AngleReduced<TUnits, T> Polar { get; }
        = Polar.Value < Angle<TUnits, T>.Zero.Value || Polar.Value > Angle<TUnits, T>.Straight.Value
            ? Throw.ArgumentOutOfRangeException<AngleReduced<TUnits, T>>(nameof(Polar), Polar, "Polar must be in [0.0º, 180.0º]")
            : Polar;

    #region constants

    /// <summary>
    /// Represents the zero value. This field is read-only.
    /// </summary>
    public static readonly PointReduced<TUnits, T> Zero = new(T.Zero, Angle<TUnits, T>.Zero, Angle<TUnits, T>.Zero);

    static PointReduced<TUnits, T> INumericBase<PointReduced<TUnits, T>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly PointReduced<TUnits, T> MinValue = new(T.MinValue, AngleReduced<TUnits, T>.MinValue, AngleReduced<TUnits, T>.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly PointReduced<TUnits, T> MaxValue = new(T.MaxValue, AngleReduced<TUnits, T>.MaxValue, Angle<TUnits, T>.Straight);

    static PointReduced<TUnits, T> IMinMaxValue<PointReduced<TUnits, T>>.MinValue
        => MinValue;
    static PointReduced<TUnits, T> IMinMaxValue<PointReduced<TUnits, T>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TUnits, T> CoordinateSystem
        => new();
    ICoordinateSystem IPoint<PointReduced<TUnits, T>>.CoordinateSystem
        => CoordinateSystem;

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="PointReduced{TUnits, T}"/></param>
    /// <returns>An instance of <see cref="PointReduced{TUnits, T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="PointReduced{TUnits, T}"/>.</exception>
    public static PointReduced<TUnits, T> CreateChecked<TOther>(in PointReduced<TUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateChecked(point.Radius),
            AngleReduced<TUnits, T>.CreateChecked(point.Azimuth),
            AngleReduced<TUnits, T>.CreateChecked(point.Polar));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="PointReduced{TUnits, T}"/></param>
    /// <returns>An instance of <see cref="PointReduced{TUnits, T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="PointReduced{TUnits, T}"/>.</exception>
    public static PointReduced<TUnits, T> CreateSaturating<TOther>(in PointReduced<TUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateSaturating(point.Radius),
            AngleReduced<TUnits, T>.CreateSaturating(point.Azimuth),
            AngleReduced<TUnits, T>.CreateSaturating(point.Polar));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="PointReduced{TUnits, T}"/></param>
    /// <returns>An instance of <see cref="PointReduced{TUnits, T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="PointReduced{TUnits, T}"/>.</exception>
    public static PointReduced<TUnits, T> CreateTruncating<TOther>(in PointReduced<TUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateTruncating(point.Radius),
            AngleReduced<TUnits, T>.CreateTruncating(point.Azimuth),
            AngleReduced<TUnits, T>.CreateTruncating(point.Polar));

    /// <summary>
    /// Implicitly converts a <see cref="PointReduced{TUnits, T}"/> to a <see cref="Point{TUnits, T, T}"/>.
    /// </summary>
    /// <param name="angle">The <see cref="PointReduced{TUnits, T}"/> to convert.</param>
    /// <returns>A new <see cref="Point{TUnits, T, T}"/> with the same azimuth, polar and radius as the input angle.</returns>
    public static implicit operator Point<TUnits, T>(PointReduced<TUnits, T> angle)
        => new(angle.Radius, angle.Azimuth, angle.Polar);

    object IPoint<PointReduced<TUnits, T>>.this[int index]
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
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in degrees.</param>
    /// <returns>The converted point in radians.</returns>    
    public static PointReduced<Radians, T> ToRadians<T>(PointReduced<Degrees, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from degrees to gradians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in degrees.</param>
    /// <returns>The converted point in gradians.</returns>
    public static PointReduced<Gradians, T> ToGradians<T>(PointReduced<Degrees, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));

    /// <summary>
    /// Convert a point from degrees to revolutions.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in degrees.</param>
    /// <returns>The converted point in revolutions.</returns>
    public static PointReduced<Revolutions, T> ToRevolutions<T>(PointReduced<Degrees, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from radians to degrees.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in radians.</param>
    /// <returns>The converted point in degrees.</returns>
    public static PointReduced<Degrees, T> ToDegrees<T>(PointReduced<Radians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from radians to gradians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in radians.</param>
    /// <returns>The converted point in gradians.</returns>
    public static PointReduced<Gradians, T> ToGradians<T>(PointReduced<Radians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));

    /// <summary>
    /// Convert a point from radians to revolutions.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in radians.</param>
    /// <returns>The converted point in revolutions.</returns>
    public static PointReduced<Revolutions, T> ToRevolutions<T>(PointReduced<Radians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from gradians to degrees.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in gradians.</param>
    /// <returns>The converted point in degrees.</returns>
    public static PointReduced<Degrees, T> ToDegrees<T>(PointReduced<Gradians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from gradians to radians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in gradians.</param>
    /// <returns>The converted point in radians.</returns>
    public static PointReduced<Radians, T> ToRadians<T>(PointReduced<Gradians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from gradians to revolutions.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in gradians.</param>
    /// <returns>The converted point in revolutions.</returns>
    public static PointReduced<Revolutions, T> ToRevolutions<T>(PointReduced<Gradians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to degrees.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in revolutions.</param>
    /// <returns>The converted point in degrees.</returns>
    public static PointReduced<Degrees, T> ToDegrees<T>(PointReduced<Revolutions, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to radians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in revolutions.</param>
    /// <returns>The converted point in radians.</returns>
    public static PointReduced<Radians, T> ToRadians<T>(PointReduced<Revolutions, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to gradians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point in revolutions.</param>
    /// <returns>The converted point in gradians.</returns>
    public static PointReduced<Gradians, T> ToGradians<T>(PointReduced<Revolutions, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));
}
