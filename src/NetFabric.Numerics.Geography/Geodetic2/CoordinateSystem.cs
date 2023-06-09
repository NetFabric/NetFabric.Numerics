using System.Collections.ObjectModel;
using System.Numerics;

namespace NetFabric.Numerics.Geography.Geodetic2;

public readonly record struct CoordinateSystem<TDatum, TAngle>
    : IGeodeticCoordinateSystem<TDatum>
    where TDatum : IDatum<TDatum>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
{
    public Datum<TDatum> Datum 
        => new();

    static readonly IReadOnlyList<Coordinate> coordinates 
        = new[] {
            new Coordinate("Latitude", typeof(Angle<Degrees, TAngle>)), 
            new Coordinate("Longitude", typeof(Angle<Degrees, TAngle>)),
        };

    public IReadOnlyList<Coordinate> Coordinates 
        => coordinates;
}