using System;
using System.Numerics;

namespace NetFabric.Numerics.Cartesian3;

public readonly record struct Point<T>(T X, T Y, T Z) 
    : IPoint<Point<T>>,
      IAdditionOperators<Point<T>, Point<T>, Point<T>>
    where T: struct, INumber<T>
{
    public CoordinateSystem<T> CoordinateSystem => CoordinateSystem<T>.Instance;
    ICoordinateSystem IPoint<Point<T>>.CoordinateSystem => CoordinateSystem;

    public static Point<T> operator +(Point<T> left, Point<T> right)
        => new (left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        
    public static Point<T> op_CheckedAddition(Point<T> left, Point<T> right)
        => checked(new (left.X + right.X, left.Y + right.Y, left.Z + right.Z));

    object IPoint<Point<T>>.this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}