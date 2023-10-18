using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics.Geography.Geodetic3;

[System.Diagnostics.DebuggerDisplay("Latitude = {Latitude}, Longitude = {Longitude}, Height = {Height}")]
[SkipLocalsInit]
public readonly record struct Point<TDatum, TAngleUnits, TAngle, THeight>(Angle<TAngleUnits, TAngle> Latitude, Angle<TAngleUnits, TAngle> Longitude, THeight Height) 
    : IPoint<Point<TDatum, TAngleUnits, TAngle, THeight>>
    where TDatum : IDatum<TDatum>
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
    where THeight : struct, INumber<THeight>, IMinMaxValue<THeight>
{
    public Angle<TAngleUnits, TAngle> Latitude { get; }
        = Latitude < -Angle<TAngleUnits, TAngle>.Right || Latitude > Angle<TAngleUnits, TAngle>.Right
            ? Throw.ArgumentOutOfRangeException<Angle<TAngleUnits, TAngle>>(nameof(Latitude), Latitude, "Latitude must be in [-90.0º, 90.0º]")
            : Latitude;

    public Angle<TAngleUnits, TAngle> Longitude { get; }
        = Longitude.Value <= -TAngle.CreateChecked(TAngleUnits.Straight) || Longitude.Value > TAngle.CreateChecked(TAngleUnits.Straight)
            ? Throw.ArgumentOutOfRangeException<Angle<TAngleUnits, TAngle>>(nameof(Longitude), Longitude, "Longitude must be in ]-180.0º, 180.0º]")
            : Longitude;

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type of the latitude and longitude components of <paramref name="point"/>.</typeparam>
    /// <typeparam name="THeightOther">The type of the height components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngleUnits, TAngle, THeight}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngleUnits, TAngle, THeight}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngleUnits, TAngle, THeight}"/>.</exception>
    public static Point<TDatum, TAngleUnits, TAngle, THeight> CreateChecked<TAngleOther, THeightOther>(in Point<TDatum, TAngleUnits, TAngleOther, THeightOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where THeightOther : struct, INumber<THeightOther>, IMinMaxValue<THeightOther>
        => new(
            Angle<TAngleUnits, TAngle>.CreateChecked(point.Latitude),
            Angle<TAngleUnits, TAngle>.CreateChecked(point.Longitude),
            THeight.CreateChecked(point.Height)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type of the latitude and longitude components of <paramref name="point"/>.</typeparam>
    /// <typeparam name="THeightOther">The type of the height components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngleUnits, TAngle, THeight}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngleUnits, TAngle, THeight}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngleUnits, TAngle, THeight}"/>.</exception>
    public static Point<TDatum, TAngleUnits, TAngle, THeight> CreateSaturating<TAngleOther, THeightOther>(in Point<TDatum, TAngleUnits, TAngleOther, THeightOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where THeightOther : struct, INumber<THeightOther>, IMinMaxValue<THeightOther>
        => new(
            Angle<TAngleUnits, TAngle>.CreateSaturating(point.Latitude),
            Angle<TAngleUnits, TAngle>.CreateSaturating(point.Longitude),
            THeight.CreateSaturating(point.Height)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type of the latitude and longitude components of <paramref name="point"/>.</typeparam>
    /// <typeparam name="THeightOther">The type of the height components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngleUnits, TAngle, THeight}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngleUnits, TAngle, THeight}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngleUnits, TAngle, THeight}"/>.</exception>
    public static Point<TDatum, TAngleUnits, TAngle, THeight> CreateTruncating<TAngleOther, THeightOther>(in Point<TDatum, TAngleUnits, TAngleOther, THeightOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where THeightOther : struct, INumber<THeightOther>, IMinMaxValue<THeightOther>
        => new(
            Angle<TAngleUnits, TAngle>.CreateTruncating(point.Latitude),
            Angle<TAngleUnits, TAngle>.CreateTruncating(point.Longitude),
            THeight.CreateTruncating(point.Height)
        );

    #region constants

    public static readonly Point<TDatum, TAngleUnits, TAngle, THeight> Zero 
        = new(Angle<TAngleUnits, TAngle>.Zero, Angle<TAngleUnits, TAngle>.Zero, THeight.Zero);

    static Point<TDatum, TAngleUnits, TAngle, THeight> INumericBase<Point<TDatum, TAngleUnits, TAngle, THeight>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<TDatum, TAngleUnits, TAngle, THeight> MinValue
        = new(-Angle<TAngleUnits, TAngle>.Right, new(TAngle.CreateTruncating(-TAngleUnits.Straight + Utils.Epsilon)), THeight.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<TDatum, TAngleUnits, TAngle, THeight> MaxValue
        = new(Angle<TAngleUnits, TAngle>.Right, Angle<TAngleUnits, TAngle>.Straight, THeight.MinValue);

    static Point<TDatum, TAngleUnits, TAngle, THeight> IMinMaxValue<Point<TDatum, TAngleUnits, TAngle, THeight>>.MinValue
        => MinValue;
    static Point<TDatum, TAngleUnits, TAngle, THeight> IMinMaxValue<Point<TDatum, TAngleUnits, TAngle, THeight>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TDatum, TAngle, THeight> CoordinateSystem 
        => new();
    ICoordinateSystem IPoint<Point<TDatum, TAngleUnits, TAngle, THeight>>.CoordinateSystem 
        => CoordinateSystem;

    object IPoint<Point<TDatum, TAngleUnits, TAngle, THeight>>.this[int index] 
        => index switch
        {
            0 => Latitude,
            1 => Longitude,
            2 => Height,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}