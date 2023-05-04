using System;
using System.Numerics;

namespace NetFabric.Numerics.Polar;

public class CoordinateSystem<TAngle, TRadius>
    : ICoordinateSystem
    where TAngle: struct, IAngle<TAngle>
    where TRadius: struct, IFloatingPoint<TRadius>
{
    static readonly Lazy<CoordinateSystem<TAngle, TRadius>> instance = new (() => new CoordinateSystem<TAngle, TRadius>());

    CoordinateSystem() {}

    public static CoordinateSystem<TAngle, TRadius> Instance => instance.Value;

    static readonly Lazy<Coordinate[]> coordinates = new (() => new[] {
        new Coordinate("Azimuth", typeof(TAngle)), 
        new Coordinate("Radius", typeof(TAngle)),
    });

    public Coordinate[] Coordinates => coordinates.Value;
}