namespace NetFabric.Numerics.Polar;

public readonly record struct CoordinateSystem<TRadius, TAngleUnits, TAngle>
    : ICoordinateSystem
    where TRadius : struct, IFloatingPoint<TRadius>
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
{
    static readonly IReadOnlyList<Coordinate> coordinates 
        = new[] {
            new Coordinate("Radius", typeof(TRadius)),
            new Coordinate("Azimuth", typeof(Angle<TAngleUnits, TAngle>)),
        };

    public IReadOnlyList<Coordinate> Coordinates 
        => coordinates;
}