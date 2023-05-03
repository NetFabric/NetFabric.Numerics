using System;

namespace NetFabric.Numerics;

public readonly record struct Coordinate(string Name, Type Type);

public interface ICoordinateSystem {
    Coordinate[] Coordinates{ get; }
}