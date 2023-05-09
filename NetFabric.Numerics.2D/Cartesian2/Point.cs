using System;
using System.Numerics;

namespace NetFabric.Numerics.Cartesian2;

public readonly record struct Point<T>(T X, T Y) 
    : IPoint<Point<T>>
    where T: struct, INumber<T>
{
    public static readonly Point<T> Zero = new(T.Zero, T.Zero);

    static Point<T> IPoint<Point<T>>.Zero
        => Zero;

    public CoordinateSystem<T> CoordinateSystem 
        => CoordinateSystem<T>.Instance;
    ICoordinateSystem IPoint<Point<T>>.CoordinateSystem 
        => CoordinateSystem;

    object IPoint<Point<T>>.this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}