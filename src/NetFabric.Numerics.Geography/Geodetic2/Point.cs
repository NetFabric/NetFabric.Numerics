using System.Numerics;

namespace NetFabric.Numerics.Geography.Geodetic2;

[System.Diagnostics.DebuggerDisplay("Latitude = {Latitude}, Longitude = {Longitude}")]
public readonly record struct Point<TDatum, TAngle>(Angle<Degrees, TAngle> Latitude, Angle<Degrees, TAngle> Longitude) 
    : IGeodeticPoint<Point<TDatum, TAngle>, TDatum>
    where TDatum : IDatum<TDatum>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
{
    public Angle<Degrees, TAngle> Latitude { get; } 
        = Latitude.Value >= TAngle.CreateChecked(-Degrees.Right) && Latitude.Value <= TAngle.CreateChecked(Degrees.Right)
            ? Latitude
            : Throw.ArgumentOutOfRangeException<Angle<Degrees, TAngle>>(nameof(Latitude), Latitude, "Latitude must be >= -90.0º and <= 90.0º");

    public Angle<Degrees, TAngle> Longitude { get; } 
        = Longitude.Value > TAngle.CreateChecked(-Degrees.Straight) && Longitude.Value <= TAngle.CreateChecked(Degrees.Straight)
            ? Longitude
            : Throw.ArgumentOutOfRangeException<Angle<Degrees, TAngle>>(nameof(Longitude), Longitude, "Longitude must be > -180.0º and <= 180.0º");

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngle}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngle}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngle}"/>.</exception>
    public static Point<TDatum, TAngle> CreateChecked<TAngleOther>(in Point<TDatum, TAngleOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        => new(
            Angle<Degrees, TAngle>.CreateChecked(point.Latitude),
            Angle<Degrees, TAngle>.CreateChecked(point.Longitude)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngle}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngle}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngle}"/>.</exception>
    public static Point<TDatum, TAngle> CreateSaturating<TAngleOther>(in Point<TDatum, TAngleOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        => new(
            Angle<Degrees, TAngle>.CreateSaturating(point.Latitude),
            Angle<Degrees, TAngle>.CreateSaturating(point.Longitude)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngle}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngleT}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngle}"/>.</exception>
    public static Point<TDatum, TAngle> CreateTruncating<TAngleOther>(in Point<TDatum, TAngleOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        => new(
            Angle<Degrees, TAngle>.CreateTruncating(point.Latitude),
            Angle<Degrees, TAngle>.CreateTruncating(point.Longitude)
        );

    #region constants

    public static readonly Point<TDatum, TAngle> Zero
        = new(Angle<Degrees, TAngle>.Zero, Angle<Degrees, TAngle>.Zero);

    static Point<TDatum, TAngle> IPoint<Point<TDatum, TAngle>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<TDatum, TAngle> MinValue
        = new(-Angle<Degrees, TAngle>.Right, new(TAngle.CreateChecked(-Degrees.Straight + double.Epsilon)));

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<TDatum, TAngle> MaxValue
        = new(Angle<Degrees, TAngle>.Right, Angle<Degrees, TAngle>.Straight);

    static Point<TDatum, TAngle> IMinMaxValue<Point<TDatum, TAngle>>.MinValue
        => MinValue;
    static Point<TDatum, TAngle> IMinMaxValue<Point<TDatum, TAngle>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TDatum, TAngle> CoordinateSystem 
        => new();
    ICoordinateSystem IPoint<Point<TDatum, TAngle>>.CoordinateSystem 
        => CoordinateSystem;

    object IPoint<Point<TDatum, TAngle>>.this[int index] 
        => index switch
        {
            0 => Latitude,
            1 => Longitude,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}