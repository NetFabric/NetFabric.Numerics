using System.Collections.Generic;
using System.Numerics;

namespace NetFabric.Numerics.Cartesian2;

public readonly record struct CoordinateSystem<T>
    : ICoordinateSystem
    where T: struct, INumber<T>
{
    static readonly Coordinate[] coordinates 
        = new[] {
            new Coordinate("X", typeof(T)), 
            new Coordinate("Y", typeof(T)),
        };

    public IReadOnlyCollection<Coordinate> Coordinates
        => coordinates;
}