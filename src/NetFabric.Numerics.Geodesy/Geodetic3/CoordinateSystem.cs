using System.Numerics;

namespace NetFabric.Numerics.Geodesy.Geodetic3;

public readonly record struct CoordinateSystem<TDatum, T>
    : IGeodeticCoordinateSystem<CoordinateSystem<TDatum, T>, TDatum, T>
    where TDatum : IDatum<T>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    static readonly IReadOnlyList<Coordinate> coordinates 
        = new[] {
            new Coordinate("Latitude", typeof(Angle<Degrees, T>)), 
            new Coordinate("Longitude", typeof(Angle<Degrees, T>)),
            new Coordinate("Height", typeof(T)),
        };

    public static IReadOnlyList<Coordinate> Coordinates 
        => coordinates;
}