using System;
using System.Collections.Generic;
using System.Numerics;

namespace NetFabric.Numerics.Geography.Geodetic;

public class CoordinateSystem<TAngle, THeight>
    : ICoordinateSystem
    where TAngle: struct, IAngle<TAngle>
    where THeight: struct, INumber<THeight>
{
    static readonly Lazy<CoordinateSystem<TAngle, THeight>> instance 
        = new (() => new CoordinateSystem<TAngle, THeight>());

    CoordinateSystem() 
    {}

    public static CoordinateSystem<TAngle, THeight> Instance 
        => instance.Value;

    static readonly Lazy<Coordinate[]> coordinates 
        = new (() => new[] {
            new Coordinate("Latitude", typeof(TAngle)), 
            new Coordinate("Longitude", typeof(TAngle)),
            new Coordinate("Height", typeof(THeight)),
        });

    public IReadOnlyCollection<Coordinate> Coordinates 
        => coordinates.Value;
}