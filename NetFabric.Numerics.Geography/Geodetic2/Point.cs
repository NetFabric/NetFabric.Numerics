using System;
using System.Numerics;

namespace NetFabric.Numerics.Geography.Geodetic2;

public readonly record struct Point<TDatum, TAngle>(Angle<Degrees, TAngle> Latitude, Angle<Degrees, TAngle> Longitude) 
    : IGeodeticPoint<Point<TDatum, TAngle>, TDatum>
    where TDatum : IDatum<TDatum>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
{
    public Angle<Degrees, TAngle> Latitude { get; init; } = Latitude >= -Angle<Degrees, TAngle>.Right && Latitude <= Angle<Degrees, TAngle>.Right
        ? Latitude
        : throw new ArgumentOutOfRangeException(nameof(Latitude), Latitude, "Latitude must be >= -90.0º and <= 90.0º");

    public Angle<Degrees, TAngle> Longitude { get; init; } = Longitude > -Angle<Degrees, TAngle>.Straight && Longitude <= Angle<Degrees, TAngle>.Straight
        ? Longitude
        : throw new ArgumentOutOfRangeException(nameof(Longitude), Longitude, "Longitude must be > -180.0º and <= 180.0º");

    #region constants

    public static readonly Point<TDatum, TAngle> Zero
        = new(new Angle<Degrees, TAngle>(TAngle.CreateChecked(0.0)), Angle<Degrees, TAngle>.Zero);

    static Point<TDatum, TAngle> IPoint<Point<TDatum, TAngle>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<TDatum, TAngle> MinValue
        = new(Angle<Degrees, TAngle>.MinValue, Angle<Degrees, TAngle>.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<TDatum, TAngle> MaxValue
        = new(Angle<Degrees, TAngle>.MaxValue, Angle<Degrees, TAngle>.MaxValue);

    static Point<TDatum, TAngle> IMinMaxValue<Point<TDatum, TAngle>>.MinValue
        => MinValue;
    static Point<TDatum, TAngle> IMinMaxValue<Point<TDatum, TAngle>>.MaxValue
        => MaxValue;

    #endregion

    public CoordinateSystem<TDatum, TAngle> CoordinateSystem 
        => new();
    ICoordinateSystem IPoint<Point<TDatum, TAngle>>.CoordinateSystem 
        => CoordinateSystem;

    object IPoint<Point<TDatum, TAngle>>.this[int index] 
        => index switch
        {
            0 => Latitude,
            1 => Longitude,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}