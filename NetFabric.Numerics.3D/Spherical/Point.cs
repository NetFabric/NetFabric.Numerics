using System;
using System.Numerics;

namespace NetFabric.Numerics.Spherical;

public readonly record struct Point<TAngle, TRadius>(TAngle Azimuth, TAngle Zenith, TRadius Radius) 
    : IPoint<Point<TAngle, TRadius>>
    where TAngle: struct, IAngle<TAngle>
    where TRadius: struct, IFloatingPoint<TRadius>
{
    public CoordinateSystem<TAngle, TRadius> CoordinateSystem 
        => CoordinateSystem<TAngle, TRadius>.Instance;
    ICoordinateSystem IPoint<Point<TAngle, TRadius>>.CoordinateSystem 
        => CoordinateSystem;

    object IPoint<Point<TAngle, TRadius>>.this[int index] 
        => index switch
        {
            0 => Azimuth,
            1 => Zenith,
            2 => Radius,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}