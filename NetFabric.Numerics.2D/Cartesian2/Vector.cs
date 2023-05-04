using System;
using System.Numerics;

namespace NetFabric.Numerics.Cartesian2;

public readonly record struct Vector<T>(T X, T Y) 
    : IVector<Vector<T>>
    where T: struct, INumber<T>
{
    public CoordinateSystem<T> CoordinateSystem => CoordinateSystem<T>.Instance;
    ICoordinateSystem IVector<Vector<T>>.CoordinateSystem => CoordinateSystem;

    public static Vector<T> operator +(Vector<T> left, Vector<T> right)
        => new (left.X + right.X, left.Y + right.Y);

    public static Vector<T> AdditiveIdentity => new (T.AdditiveIdentity, T.AdditiveIdentity);

    object IVector<Vector<T>>.this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}