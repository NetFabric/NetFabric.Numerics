using System.Collections.Generic;
using System.Numerics;

namespace NetFabric.Numerics.Geography.Geodetic3;

public readonly record struct CoordinateSystem<TDatum, TAngle, THeight>
    : IGeodeticCoordinateSystem<TDatum>
    where TDatum : IDatum<TDatum>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
    where THeight : struct, INumber<THeight>
{
    public Datum<TDatum> Datum 
        => new();

    static readonly Coordinate[] coordinates 
        = new[] {
            new Coordinate("Latitude", typeof(Angle<Degrees, TAngle>)), 
            new Coordinate("Longitude", typeof(Angle<Degrees, TAngle>)),
            new Coordinate("Height", typeof(THeight)),
        };

    public IReadOnlyCollection<Coordinate> Coordinates 
        => coordinates;
}