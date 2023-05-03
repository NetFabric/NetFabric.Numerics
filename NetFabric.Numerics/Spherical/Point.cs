using System;
using System.Numerics;

namespace NetFabric.Numerics.Spherical;

public readonly record struct Point<TAngle, TRadius>(TAngle Azimuth, TAngle Zenith, TRadius Radius) 
    : IPoint<Point<TAngle, TRadius>>,
      IAdditionOperators<Point<TAngle, TRadius>, Point<TAngle, TRadius>, Point<TAngle, TRadius>>
    where TAngle: struct, IAngle<TAngle>
    where TRadius: struct, IFloatingPoint<TRadius>
{
    public CoordinateSystem<TAngle, TRadius> CoordinateSystem => CoordinateSystem<TAngle, TRadius>.Instance;
    ICoordinateSystem IPoint<Point<TAngle, TRadius>>.CoordinateSystem => CoordinateSystem;

    public static Point<TAngle, TRadius> operator +(Point<TAngle, TRadius> left, Point<TAngle, TRadius> right)
        => new (left.Azimuth + right.Azimuth, left.Zenith + right.Zenith, left.Radius + right.Radius);

        
    public static Point<TAngle, TRadius> op_CheckedAddition(Point<TAngle, TRadius> left, Point<TAngle, TRadius> right)
        => checked(new (left.Azimuth + right.Azimuth, left.Zenith + right.Zenith, left.Radius + right.Radius));

    object IPoint<Point<TAngle, TRadius>>.this[int index] 
        => index switch
        {
            0 => Azimuth,
            1 => Zenith,
            2 => Radius,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}