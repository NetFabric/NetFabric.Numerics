using System;
using System.Numerics;

namespace NetFabric.Numerics.Polar;

public readonly record struct Point<TAngle, TRadius>(TAngle Azimuth, TRadius Radius) 
    : IPoint<Point<TAngle, TRadius>>
    where TAngle: struct, IAngle<TAngle>
    where TRadius: struct, IFloatingPoint<TRadius>
{
    public static readonly Point<TAngle, TRadius> Zero = new(TAngle.Zero, TRadius.Zero);

    static Point<TAngle, TRadius> IPoint<Point<TAngle, TRadius>>.Zero
        => Zero;

    public CoordinateSystem<TAngle, TRadius> CoordinateSystem 
        => CoordinateSystem<TAngle, TRadius>.Instance;
    ICoordinateSystem IPoint<Point<TAngle, TRadius>>.CoordinateSystem 
        => CoordinateSystem;

    object IPoint<Point<TAngle, TRadius>>.this[int index] 
        => index switch
        {
            0 => Azimuth,
            1 => Radius,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}