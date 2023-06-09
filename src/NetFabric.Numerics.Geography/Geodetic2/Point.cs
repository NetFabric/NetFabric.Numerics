using System.Numerics;

namespace NetFabric.Numerics.Geography.Geodetic2;

[System.Diagnostics.DebuggerDisplay("Latitude = {Latitude}, Longitude = {Longitude}")]
public readonly record struct Point<TDatum, TAngleUnits, TAngle>(Angle<TAngleUnits, TAngle> Latitude, Angle<TAngleUnits, TAngle> Longitude) 
    : IGeodeticPoint<Point<TDatum, TAngleUnits, TAngle>, TDatum>
    where TDatum : IDatum<TDatum>
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
{
    public Angle<TAngleUnits, TAngle> Latitude { get; } 
        = Latitude.Value >= TAngle.CreateChecked(-TAngleUnits.Right) && Latitude.Value <= TAngle.CreateChecked(TAngleUnits.Right)
            ? Latitude
            : Throw.ArgumentOutOfRangeException<Angle<TAngleUnits, TAngle>>(nameof(Latitude), Latitude, "Latitude must be in [-90.0º, 90.0º]");

    public Angle<TAngleUnits, TAngle> Longitude { get; } 
        = Longitude.Value > TAngle.CreateChecked(-TAngleUnits.Straight) && Longitude.Value <= TAngle.CreateChecked(TAngleUnits.Straight)
            ? Longitude
            : Throw.ArgumentOutOfRangeException<Angle<TAngleUnits, TAngle>>(nameof(Longitude), Longitude, "Longitude must be in ]-180.0º, 180.0º]");

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngleUnits, TAngle}"/>.</exception>
    public static Point<TDatum, TAngleUnits, TAngle> CreateChecked<TAngleOther>(in Point<TDatum, TAngleUnits, TAngleOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        => new(
            Angle<TAngleUnits, TAngle>.CreateChecked(point.Latitude),
            Angle<TAngleUnits, TAngle>.CreateChecked(point.Longitude)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngleUnits, TAngle}"/>.</exception>
    public static Point<TDatum, TAngleUnits, TAngle> CreateSaturating<TAngleOther>(in Point<TDatum, TAngleUnits, TAngleOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        => new(
            Angle<TAngleUnits, TAngle>.CreateSaturating(point.Latitude),
            Angle<TAngleUnits, TAngle>.CreateSaturating(point.Longitude)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngleUnits, TAngle}"/>.</exception>
    public static Point<TDatum, TAngleUnits, TAngle> CreateTruncating<TAngleOther>(in Point<TDatum, TAngleUnits, TAngleOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        => new(
            Angle<TAngleUnits, TAngle>.CreateTruncating(point.Latitude),
            Angle<TAngleUnits, TAngle>.CreateTruncating(point.Longitude)
        );

    #region constants

    public static readonly Point<TDatum, TAngleUnits, TAngle> Zero
        = new(Angle<TAngleUnits, TAngle>.Zero, Angle<TAngleUnits, TAngle>.Zero);

    static Point<TDatum, TAngleUnits, TAngle> IPoint<Point<TDatum, TAngleUnits, TAngle>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<TDatum, TAngleUnits, TAngle> MinValue
        = new(-Angle<TAngleUnits, TAngle>.Right, new(TAngle.CreateChecked(-TAngleUnits.Straight + double.Epsilon)));

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<TDatum, TAngleUnits, TAngle> MaxValue
        = new(Angle<TAngleUnits, TAngle>.Right, Angle<TAngleUnits, TAngle>.Straight);

    static Point<TDatum, TAngleUnits, TAngle> IMinMaxValue<Point<TDatum, TAngleUnits, TAngle>>.MinValue
        => MinValue;
    static Point<TDatum, TAngleUnits, TAngle> IMinMaxValue<Point<TDatum, TAngleUnits, TAngle>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TDatum, TAngle> CoordinateSystem 
        => new();
    ICoordinateSystem IPoint<Point<TDatum, TAngleUnits, TAngle>>.CoordinateSystem 
        => CoordinateSystem;

    object IPoint<Point<TDatum, TAngleUnits, TAngle>>.this[int index] 
        => index switch
        {
            0 => Latitude,
            1 => Longitude,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}