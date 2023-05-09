using System;
using System.Numerics;

namespace NetFabric.Numerics.Geography.Geodetic;

public readonly record struct Point<TAngle, THeight>(TAngle Latitude, TAngle Longitude, THeight Height) 
    : IPoint<Point<TAngle, THeight>>
    where TAngle: struct, IAngle<TAngle>
    where THeight: struct, INumber<THeight>
{
    public TAngle Latitude { get; } = Latitude >= -TAngle.Right && Latitude <= TAngle.Right
        ? Latitude 
        : throw new ArgumentOutOfRangeException(nameof(Latitude), Latitude, "Latitude must be >= -90.0º and <= 90.0º");

    public TAngle Longitude { get; } = Longitude > -TAngle.Straight && Longitude <= TAngle.Straight
        ? Longitude 
        : throw new ArgumentOutOfRangeException(nameof(Longitude), Longitude, "Longitude must be > -180.0º and <= 180.0º");

    public static readonly Point<TAngle, THeight> Zero = new(TAngle.Zero, TAngle.Zero, THeight.Zero);

    static Point<TAngle, THeight> IPoint<Point<TAngle, THeight>>.Zero
        => Zero;

    public CoordinateSystem<TAngle, THeight> CoordinateSystem 
        => CoordinateSystem<TAngle, THeight>.Instance;
    ICoordinateSystem IPoint<Point<TAngle, THeight>>.CoordinateSystem 
        => CoordinateSystem;

    object IPoint<Point<TAngle, THeight>>.this[int index] 
        => index switch
        {
            0 => Latitude,
            1 => Longitude,
            2 => Height,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}