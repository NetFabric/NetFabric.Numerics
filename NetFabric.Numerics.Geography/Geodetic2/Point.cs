using System;
using System.Numerics;

namespace NetFabric.Numerics.Geography.Geodetic;

public readonly record struct Point<TAngle>(TAngle Latitude, TAngle Longitude) 
    : IPoint<Point<TAngle>>
    where TAngle: struct, IAngle<TAngle>
{
    public TAngle Latitude { get; } = Latitude >= -TAngle.Right && Latitude <= TAngle.Right
        ? Latitude 
        : throw new ArgumentOutOfRangeException(nameof(Latitude), Latitude, "Latitude must be >= -90.0º and <= 90.0º");

    public TAngle Longitude { get; } = Longitude > -TAngle.Straight && Longitude <= TAngle.Straight
        ? Longitude 
        : throw new ArgumentOutOfRangeException(nameof(Longitude), Longitude, "Longitude must be > -180.0º and <= 180.0º");

    public CoordinateSystem<TAngle> CoordinateSystem => CoordinateSystem<TAngle>.Instance;
    ICoordinateSystem IPoint<Point<TAngle>>.CoordinateSystem => CoordinateSystem;

    object IPoint<Point<TAngle>>.this[int index] 
        => index switch
        {
            0 => Latitude,
            1 => Longitude,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}