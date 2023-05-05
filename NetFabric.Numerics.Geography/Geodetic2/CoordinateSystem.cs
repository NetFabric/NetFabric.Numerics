using System;
using System.Collections.Generic;

namespace NetFabric.Numerics.Geography.Geodetic;

public class CoordinateSystem<TAngle>
    : ICoordinateSystem
    where TAngle: struct, IAngle<TAngle>
{
    static readonly Lazy<CoordinateSystem<TAngle>> instance 
        = new (() => new CoordinateSystem<TAngle>());

    CoordinateSystem() 
    {}

    public static CoordinateSystem<TAngle> Instance 
        => instance.Value;

    static readonly Lazy<Coordinate[]> coordinates 
        = new (() => new[] {
            new Coordinate("Latitude", typeof(TAngle)), 
            new Coordinate("Longitude", typeof(TAngle)),
        });

    public IReadOnlyCollection<Coordinate> Coordinates 
        => coordinates.Value;
}