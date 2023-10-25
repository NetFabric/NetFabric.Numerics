namespace NetFabric.Numerics.Rectangular3D;

/// <summary>
/// Represents a 3D Rectangular coordinate system.
/// </summary>
/// <typeparam name="T">The numeric type used for the coordinates.</typeparam>
/// <remarks>
/// In a 3D Rectangular coordinate system, coordinates are represented by a triplet of values (X, Y, Z) that specify
/// the position of a point in 3D space. The X-coordinate represents the horizontal position, the Y-coordinate
/// represents the vertical position, and the Z-coordinate represents the depth or altitude.
/// </remarks>
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

    /// <summary>
    /// Gets the list of coordinates in the polar coordinate system.
    /// </summary>
    /// <remarks>
    /// Each coordinate contains information about its name and type.
    /// </remarks>
    public IReadOnlyList<Coordinate> Coordinates 
            => coordinates;
    }