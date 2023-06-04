namespace NetFabric.Numerics;

/// <summary>
/// Represents a coordinate in a coordinate system.
/// </summary>
/// <param name="Name">The name of the coordinate.</param>
/// <param name="Type">The type of the coordinate.</param>
public readonly record struct Coordinate(string Name, Type Type);

/// <summary>
/// Represents a coordinate system.
/// </summary>
public interface ICoordinateSystem {

    /// <summary>
    /// Gets information for each coordinate in a coordinate system.
    /// </summary>
    /// <value>
    /// An <see cref="IReadOnlyCollection{Coordinate}"/> containing the information
    /// for each coordinate in the coordinate system.
    /// </value>
    /// <remarks>
    /// <para>
    /// The <see cref="Coordinates"/> property allows you to access the information
    /// for each coordinate in a given coordinate system. It returns an <see cref="IReadOnlyCollection{Coordinate}"/>
    /// containing the information for each coordinate.
    /// </para>
    /// <para>
    /// The number of coordinates in the coordinate system can be obtained by accessing the <see cref="IReadOnlyCollection{Coordinate}.Count"/> property.
    /// </para>
    /// </remarks>
    ReadOnlyMemory<Coordinate> Coordinates { get; }
}