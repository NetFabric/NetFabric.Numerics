using NetFabric.Numerics.Geography.Geodetic2;
using System.Numerics;

namespace NetFabric.Numerics.Geography.Geodetic3;

[System.Diagnostics.DebuggerDisplay("Latitude = {Latitude}, Longitude = {Longitude}, Height = {Height}")]
public readonly record struct Point<TDatum, TAngle, THeight>(Angle<Degrees, TAngle> Latitude, Angle<Degrees, TAngle> Longitude, THeight Height) 
    : IPoint<Point<TDatum, TAngle, THeight>>
    where TDatum : IDatum<TDatum>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
    where THeight : struct, INumber<THeight>, IMinMaxValue<THeight>
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
    /// <typeparam name="TAngleOther">The type of the latitude and longitude components of <paramref name="point"/>.</typeparam>
    /// <typeparam name="THeightOther">The type of the height components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngle, THeight}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngle, THeight}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngle, THeight}"/>.</exception>
    public static Point<TDatum, TAngle, THeight> CreateChecked<TAngleOther, THeightOther>(in Point<TDatum, TAngleOther, THeightOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where THeightOther : struct, INumber<THeightOther>, IMinMaxValue<THeightOther>
        => new(
            Angle<Degrees, TAngle>.CreateChecked(point.Latitude),
            Angle<Degrees, TAngle>.CreateChecked(point.Longitude),
            THeight.CreateChecked(point.Height)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type of the latitude and longitude components of <paramref name="point"/>.</typeparam>
    /// <typeparam name="THeightOther">The type of the height components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngle, THeight}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngle, THeight}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngle, THeight}"/>.</exception>
    public static Point<TDatum, TAngle, THeight> CreateSaturating<TAngleOther, THeightOther>(in Point<TDatum, TAngleOther, THeightOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where THeightOther : struct, INumber<THeightOther>, IMinMaxValue<THeightOther>
        => new(
            Angle<Degrees, TAngle>.CreateSaturating(point.Latitude),
            Angle<Degrees, TAngle>.CreateSaturating(point.Longitude),
            THeight.CreateSaturating(point.Height)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type of the latitude and longitude components of <paramref name="point"/>.</typeparam>
    /// <typeparam name="THeightOther">The type of the height components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngle, THeight}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngleT}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngle, THeight}"/>.</exception>
    public static Point<TDatum, TAngle, THeight> CreateTruncating<TAngleOther, THeightOther>(in Point<TDatum, TAngleOther, THeightOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where THeightOther : struct, INumber<THeightOther>, IMinMaxValue<THeightOther>
        => new(
            Angle<Degrees, TAngle>.CreateTruncating(point.Latitude),
            Angle<Degrees, TAngle>.CreateTruncating(point.Longitude),
            THeight.CreateTruncating(point.Height)
        );

    #region constants

    public static readonly Point<TDatum, TAngle, THeight> Zero 
        = new(Angle<Degrees, TAngle>.Zero, Angle<Degrees, TAngle>.Zero, THeight.Zero);

    static Point<TDatum, TAngle, THeight> IPoint<Point<TDatum, TAngle, THeight>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<TDatum, TAngle, THeight> MinValue
        = new(-Angle<Degrees, TAngle>.Right, new(TAngle.CreateTruncating(-Degrees.Straight + double.Epsilon)), THeight.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<TDatum, TAngle, THeight> MaxValue
        = new(Angle<Degrees, TAngle>.Right, Angle<Degrees, TAngle>.Straight, THeight.MinValue);

    static Point<TDatum, TAngle, THeight> IMinMaxValue<Point<TDatum, TAngle, THeight>>.MinValue
        => MinValue;
    static Point<TDatum, TAngle, THeight> IMinMaxValue<Point<TDatum, TAngle, THeight>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TDatum, TAngle, THeight> CoordinateSystem 
        => new();
    ICoordinateSystem IPoint<Point<TDatum, TAngle, THeight>>.CoordinateSystem 
        => CoordinateSystem;

    object IPoint<Point<TDatum, TAngle, THeight>>.this[int index] 
        => index switch
        {
            0 => Latitude,
            1 => Longitude,
            2 => Height,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}