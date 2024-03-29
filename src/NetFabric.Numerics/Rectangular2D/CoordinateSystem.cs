namespace NetFabric.Numerics.Rectangular2D;

/// <summary>
/// Represents a 2D Rectangular coordinate system.
/// </summary>
/// <typeparam name="T">The numeric type used for the coordinates.</typeparam>
/// <remarks>
/// A Rectangular coordinate system uses a pair of values (X, Y) to specify the position of a point in 2D space.
/// The X-coordinate represents the horizontal position, and the Y-coordinate represents the vertical position.
/// </remarks>
public abstract class CoordinateSystem<T>
    : ICoordinateSystem
    where T: struct, INumber<T>
{
    /// <summary>
    /// Gets the list of coordinates in the polar coordinate system.
    /// </summary>
    /// <remarks>
    /// Each coordinate contains information about its name and type.
    /// </remarks>
    public static IReadOnlyList<Coordinate> Coordinates { get; } 
        = new[] {
            new Coordinate("X", typeof(T)),
            new Coordinate("Y", typeof(T)),
        };
}