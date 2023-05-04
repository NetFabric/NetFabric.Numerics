using System;
using System.Numerics;

namespace NetFabric.Numerics.Cartesian3;

public readonly record struct Point<T>(T X, T Y, T Z) 
    : IPoint<Point<T>>
    where T: struct, INumber<T>
{
    public CoordinateSystem<T> CoordinateSystem => CoordinateSystem<T>.Instance;
    ICoordinateSystem IPoint<Point<T>>.CoordinateSystem => CoordinateSystem;

    object IPoint<Point<T>>.this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}