namespace NetFabric.Numerics.Spherical;

/// <summary>
/// Represents a reduced point in a Spherical coordinate system.
/// </summary>
/// <typeparam name="TAngleUnits">The units used for the angles.</typeparam>
/// <typeparam name="T">The type used for the coordinates.</typeparam>
/// <remarks>
/// In a Spherical coordinate system, a reduced point is represented by a triplet of values: the radius, the azimuth, and the polar angle.
/// The radius represents the distance from the origin, the azimuth represents the angle measured counterclockwise
/// from a reference direction in the XY plane within a full revolution, and the polar angle represents the angle measured from the positive Z-axis within half a revolution. 
/// The choice of angle units is determined by the specified angle units type, TAngleUnits.
/// </remarks>
/// <param name="Azimuth">The azimuth coordinate.</param>
/// <param name="Polar">The polar coordinate.</param>
/// <param name="Radius">The radius coordinate.</param>
[System.Diagnostics.DebuggerDisplay("Radius = {Radius}, Azimuth = {Azimuth}, Polar = {Polar}")]
[SkipLocalsInit]
public readonly record struct PointReduced<TAngleUnits, T>(T Radius, AngleReduced<TAngleUnits, T> Azimuth, AngleReduced<TAngleUnits, T> Polar)
    : IPoint<PointReduced<TAngleUnits, T>, CoordinateSystem<TAngleUnits, T>>
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    public AngleReduced<TAngleUnits, T> Polar { get; }
        = Polar.Value < Angle<TAngleUnits, T>.Zero.Value || Polar.Value > Angle<TAngleUnits, T>.Straight.Value
            ? Throw.ArgumentOutOfRangeException<AngleReduced<TAngleUnits, T>>(nameof(Polar), Polar, "Polar must be in [0.0�, 180.0�]")
            : Polar;

    #region constants

    /// <summary>
    /// Represents the zero value. This field is read-only.
    /// </summary>
    public static readonly PointReduced<TAngleUnits, T> Zero = new(T.Zero, Angle<TAngleUnits, T>.Zero, Angle<TAngleUnits, T>.Zero);

    static PointReduced<TAngleUnits, T> IGeometricBase<PointReduced<TAngleUnits, T>, CoordinateSystem<TAngleUnits, T>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly PointReduced<TAngleUnits, T> MinValue = new(T.MinValue, AngleReduced<TAngleUnits, T>.MinValue, AngleReduced<TAngleUnits, T>.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly PointReduced<TAngleUnits, T> MaxValue = new(T.MaxValue, AngleReduced<TAngleUnits, T>.MaxValue, Angle<TAngleUnits, T>.Straight);

    static PointReduced<TAngleUnits, T> IMinMaxValue<PointReduced<TAngleUnits, T>>.MinValue
        => MinValue;
    static PointReduced<TAngleUnits, T> IMinMaxValue<PointReduced<TAngleUnits, T>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TAngleUnits, T> CoordinateSystem
        => new();

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="PointReduced{TAngleUnits, T}"/></param>
    /// <returns>An instance of <see cref="PointReduced{TAngleUnits, T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="PointReduced{TAngleUnits, T}"/>.</exception>
    public static PointReduced<TAngleUnits, T> CreateChecked<TOther>(in PointReduced<TAngleUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateChecked(point.Radius),
            AngleReduced<TAngleUnits, T>.CreateChecked(point.Azimuth),
            AngleReduced<TAngleUnits, T>.CreateChecked(point.Polar));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="PointReduced{TAngleUnits, T}"/></param>
    /// <returns>An instance of <see cref="PointReduced{TAngleUnits, T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="PointReduced{TAngleUnits, T}"/>.</exception>
    public static PointReduced<TAngleUnits, T> CreateSaturating<TOther>(in PointReduced<TAngleUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateSaturating(point.Radius),
            AngleReduced<TAngleUnits, T>.CreateSaturating(point.Azimuth),
            AngleReduced<TAngleUnits, T>.CreateSaturating(point.Polar));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="PointReduced{TAngleUnits, T}"/></param>
    /// <returns>An instance of <see cref="PointReduced{TAngleUnits, T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="PointReduced{TAngleUnits, T}"/>.</exception>
    public static PointReduced<TAngleUnits, T> CreateTruncating<TOther>(in PointReduced<TAngleUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateTruncating(point.Radius),
            AngleReduced<TAngleUnits, T>.CreateTruncating(point.Azimuth),
            AngleReduced<TAngleUnits, T>.CreateTruncating(point.Polar));

    /// <summary>
    /// Implicitly converts a <see cref="PointReduced{TAngleUnits, T}"/> to a <see cref="Point{TAngleUnits, T}"/>.
    /// </summary>
    /// <param name="angle">The <see cref="PointReduced{TAngleUnits, T}"/> to convert.</param>
    /// <returns>A new <see cref="Point{TAngleUnits, T}"/> with the same azimuth, polar and radius as the input angle.</returns>
    public static implicit operator Point<TAngleUnits, T>(PointReduced<TAngleUnits, T> angle)
        => new(angle.Radius, angle.Azimuth, angle.Polar);

    object IGeometricBase<PointReduced<TAngleUnits, T>, CoordinateSystem<TAngleUnits, T>>.this[int index]
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
