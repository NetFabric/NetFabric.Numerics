using System;
using System.Numerics;

namespace NetFabric.Numerics.Geography.Geodetic;

public readonly record struct Point<TAngle>(TAngle Latitude, TAngle Longitude) 
    : IPoint<Point<TAngle>>,
      IAdditionOperators<Point<TAngle>, Point<TAngle>, Point<TAngle>>
    where TAngle: struct, IAngle<TAngle>
{
    public CoordinateSystem<TAngle> CoordinateSystem => CoordinateSystem<TAngle>.Instance;
    ICoordinateSystem IPoint<Point<TAngle>>.CoordinateSystem => CoordinateSystem;

    public static Point<TAngle> operator +(Point<TAngle> left, Point<TAngle> right)
        => new (left.Latitude + right.Latitude, left.Longitude + right.Longitude);

        
    public static Point<TAngle> op_CheckedAddition(Point<TAngle> left, Point<TAngle> right)
        => checked(new (left.Latitude + right.Latitude, left.Longitude + right.Longitude));

    object IPoint<Point<TAngle>>.this[int index] 
        => index switch
        {
            0 => Latitude,
            1 => Longitude,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}