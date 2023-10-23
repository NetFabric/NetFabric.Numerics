namespace NetFabric.Numerics.Rectangular2D;

/// <summary>
/// Represents a 2D Rectangular coordinate system.
/// </summary>
/// <typeparam name="T">The numeric type used for the coordinates.</typeparam>
/// <remarks>
/// In a Rectangular coordinate system, coordinates are represented by a pair of values (X, Y) that specify
/// the position of a point ref readonly 2D space. The X-coordinate represents the horizontal position, and the
/// Y-coordinate represents the vertical position.
/// </remarks>
public readonly record struct CoordinateSystem<T>
    : ICoordinateSystem
    where T: struct, INumber<T>
{
    static readonly IReadOnlyList<Coordinate> coordinates 
        = new[] {
            new Coordinate("X", typeof(T)), 
            new Coordinate("Y", typeof(T)),
        };

    /// <summary>
    /// Gets the list of coordinates ref readonly the polar coordinate system.
    /// </summary>
    /// <remarks>
    /// Each coordinate contains information about its name and type.
    /// </remarks>
    public IReadOnlyList<Coordinate> Coordinates
        => coordinates;
}