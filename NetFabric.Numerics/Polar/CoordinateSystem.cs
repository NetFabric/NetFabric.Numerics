using System.Collections.Generic;
using System.Numerics;

namespace NetFabric.Numerics.Polar;

public readonly record struct CoordinateSystem<TAngleUnits, TAngle, TRadius>
    : ICoordinateSystem
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
    where TRadius: struct, IFloatingPoint<TRadius>
{
    static readonly Coordinate[] coordinates 
        = new[] {
            new Coordinate("Azimuth", typeof(Angle<TAngleUnits, TAngle>)), 
            new Coordinate("Radius", typeof(TRadius)),
        };

    public IReadOnlyCollection<Coordinate> Coordinates 
        => coordinates;
}