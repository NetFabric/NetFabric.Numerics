using System;
using System.Numerics;

namespace NetFabric.Numerics.Spherical;

public readonly record struct Vector<TAngle, TRadius>(TAngle Azimuth, TAngle Zenith, TRadius Radius) 
    : IVector<Vector<TAngle, TRadius>>
    where TAngle: struct, IAngle<TAngle>
    where TRadius: struct, IFloatingPoint<TRadius>
{
    public CoordinateSystem<TAngle, TRadius> CoordinateSystem 
        => CoordinateSystem<TAngle, TRadius>.Instance;
    ICoordinateSystem IVector<Vector<TAngle, TRadius>>.CoordinateSystem 
        => CoordinateSystem;

    public static Vector<TAngle, TRadius> operator +(Vector<TAngle, TRadius> left, Vector<TAngle, TRadius> right)
        => new (left.Azimuth + right.Azimuth, left.Zenith + right.Zenith, left.Radius + right.Radius);

    public static Vector<TAngle, TRadius> AdditiveIdentity 
        => new (TAngle.AdditiveIdentity, TAngle.AdditiveIdentity, TRadius.AdditiveIdentity);

    object IVector<Vector<TAngle, TRadius>>.this[int index] 
        => index switch
        {
            0 => Azimuth,
            1 => Zenith,
            2 => Radius,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}