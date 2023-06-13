namespace NetFabric.Numerics.Cartesian3;

public readonly record struct CoordinateSystem<T>
    : ICoordinateSystem
    where T: struct, INumber<T>
{
    static readonly IReadOnlyList<Coordinate> coordinates 
        = new[] {
            new Coordinate("X", typeof(T)), 
            new Coordinate("Y", typeof(T)),
            new Coordinate("Z", typeof(T)),
        };

    public IReadOnlyList<Coordinate> Coordinates 
        => coordinates;
}