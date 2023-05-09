using System;
using System.Collections.Generic;
using System.Numerics;

namespace NetFabric.Numerics.Spherical;

public class CoordinateSystem<TAngle, TRadius>
    : ICoordinateSystem
    where TAngle: struct, IAngle<TAngle>
    where TRadius: struct, IFloatingPoint<TRadius>
{
    static readonly Lazy<CoordinateSystem<TAngle, TRadius>> instance 
        = new (() => new CoordinateSystem<TAngle, TRadius>());

    CoordinateSystem() 
    {}

    public static CoordinateSystem<TAngle, TRadius> Instance 
        => instance.Value;

    static readonly Coordinate[] coordinates 
        = new[] {
            new Coordinate("Azimuth", typeof(TAngle)), 
            new Coordinate("Zenith", typeof(TAngle)),
            new Coordinate("Radius", typeof(TAngle)),
        };

    public IReadOnlyCollection<Coordinate> Coordinates 
        => coordinates;
}