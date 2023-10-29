namespace NetFabric.Numerics;

/// <summary>
/// Represents an abstract base class for a coordinate system.
/// </summary>
public abstract class CoordinateSystem
{
    /// <summary>
    /// Gets a read-only list of coordinates in the system.
    /// </summary>
    public abstract IReadOnlyList<Coordinate> Coordinates { get; }
}

/// <summary>
/// Represents a concrete implementation of a coordinate system.
/// </summary>
/// <typeparam name="T">The type of coordinate system.</typeparam>
/// <remarks>
/// This class is meant to be used as a singleton to represent a specific coordinate system.
/// </remarks>
public class CoordinateSystem<T> : CoordinateSystem
    where T : ICoordinateSystem
{
    // Create a lazy singleton instance of the coordinate system.
    static readonly Lazy<CoordinateSystem<T>> lazyInstance = new(() => new());

    /// <summary>
    /// Private constructor to prevent direct instantiation.
    /// </summary>
    private CoordinateSystem() { }

    /// <summary>
    /// Gets the singleton instance of the coordinate system.
    /// </summary>
    internal static CoordinateSystem<T> Instance
        => lazyInstance.Value;

    /// <summary>
    /// Gets a read-only list of coordinates in the system, as defined by the associated coordinate system type 'T'.
    /// </summary>
    public override IReadOnlyList<Coordinate> Coordinates
        => T.Coordinates;
}

