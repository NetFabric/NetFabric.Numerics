using System.Numerics;

namespace NetFabric.Numerics.Geography.Geodetic3;

public readonly record struct Point<TDatum, TAngle, THeight>(Angle<Degrees, TAngle> Latitude, Angle<Degrees, TAngle> Longitude, THeight Height) 
    : IPoint<Point<TDatum, TAngle, THeight>>
    where TDatum : IDatum<TDatum>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
    where THeight : struct, INumber<THeight>, IMinMaxValue<THeight>
{
    public Angle<Degrees, TAngle> Latitude { get; }
        = Latitude.Value >= TAngle.CreateChecked(-Degrees.Right) && Latitude.Value <= TAngle.CreateChecked(Degrees.Right)
            ? Latitude
            : throw new ArgumentOutOfRangeException(nameof(Latitude), Latitude, "Latitude must be >= -90.0º and <= 90.0º");

    public Angle<Degrees, TAngle> Longitude { get; }
        = Longitude.Value > TAngle.CreateChecked(-Degrees.Straight) && Longitude.Value <= TAngle.CreateChecked(Degrees.Straight)
            ? Longitude
            : throw new ArgumentOutOfRangeException(nameof(Longitude), Longitude, "Longitude must be > -180.0º and <= 180.0º");

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
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}