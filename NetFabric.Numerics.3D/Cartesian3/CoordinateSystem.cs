using System;
using System.Collections.Generic;
using System.Numerics;

namespace NetFabric.Numerics.Cartesian3;

public class CoordinateSystem<T>
    : ICoordinateSystem
    where T: struct, INumber<T>
{
    static readonly Lazy<CoordinateSystem<T>> instance 
        = new (() => new CoordinateSystem<T>());

    CoordinateSystem() 
    {}

    public static CoordinateSystem<T> Instance 
        => instance.Value;

    static readonly Lazy<Coordinate[]> coordinates 
        = new (() => new[] {
            new Coordinate("X", typeof(T)), 
            new Coordinate("Y", typeof(T)),
            new Coordinate("Z", typeof(T)),
        });

    public IReadOnlyCollection<Coordinate> Coordinates 
        => coordinates.Value;
}