using System;
using System.Collections.Generic;

namespace NetFabric.Numerics;

public readonly record struct Coordinate(string Name, Type Type);

public interface ICoordinateSystem {
    IReadOnlyCollection<Coordinate> Coordinates { get; }
}