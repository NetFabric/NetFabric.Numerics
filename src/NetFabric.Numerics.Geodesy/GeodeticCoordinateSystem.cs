using System.Numerics;

namespace NetFabric.Numerics.Geodesy;

/// <summary>
/// Represents an abstract base class for a geodetic coordinate system.
/// </summary>
/// <typeparam name="T">The floating-point type used in the coordinate system.</typeparam>
/// <remarks>
/// This abstract class provides a foundation for geodetic coordinate systems for a specific floating-point type 'T'.
/// </remarks>
public abstract class GeodeticCoordinateSystem<T>
    : CoordinateSystem
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Gets the datum associated with this geodetic coordinate system.
    /// </summary>
    public abstract Datum<T> Datum { get; }
}

/// <summary>
/// Represents a concrete implementation of a geodetic coordinate system with type-specific operations.
/// </summary>
/// <typeparam name="TCoordinateSystem">The type of geodetic coordinate system to be represented.</typeparam>
/// <typeparam name="T">The floating-point type used in the coordinate system.</typeparam>
/// <remarks>
/// This class is meant to be used as a singleton to represent a specific geodetic coordinate system.
/// </remarks>
public class GeodeticCoordinateSystem<TCoordinateSystem, T> 
    : GeodeticCoordinateSystem<T>
    where TCoordinateSystem : IGeodeticCoordinateSystem<T>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    // Create a lazy singleton instance of the geodetic coordinate system.
    static readonly Lazy<GeodeticCoordinateSystem<TCoordinateSystem, T>> lazyInstance = new(() => new());

    /// <summary>
    /// Private constructor to prevent direct instantiation.
    /// </summary>
    private GeodeticCoordinateSystem() { }

    /// <summary>
    /// Gets the singleton instance of the geodetic coordinate system for the specified type 'TCoordinateSystem'.
    /// </summary>
    internal static GeodeticCoordinateSystem<TCoordinateSystem, T> Instance
        => lazyInstance.Value;

    /// <inheritdoc/>
    public override IReadOnlyList<Coordinate> Coordinates
        => TCoordinateSystem.Coordinates;

    /// <inheritdoc/>
    public override Datum<T> Datum
        => TCoordinateSystem.Datum;
}
