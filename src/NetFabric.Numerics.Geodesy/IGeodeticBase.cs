using System.Numerics;

namespace NetFabric.Numerics.Geodesy;

/// <summary>
/// Represents an interface for geodetic objects with type-specific operations.
/// </summary>
/// <typeparam name="T">The floating-point type used in the geodetic object.</typeparam>
/// <remarks>
/// This interface provides functionality related to geodetic objects for a specific floating-point type 'T'.
/// </remarks>
public interface IGeodeticBase<T> : IGeometricBase
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Gets the coordinate system of the geodetic object.
    /// </summary>
    /// <value>The coordinate system of the geodetic object.</value>
    new GeodeticCoordinateSystem<T> CoordinateSystem { get; }
}

/// <summary>
/// Represents an interface for geodetic objects with type-specific operations and coordinate system.
/// </summary>
/// <typeparam name="TCoordinateSystem">The type of geodetic coordinate system.</typeparam>
/// <typeparam name="TDatum">The type representing the datum.</typeparam>
/// <typeparam name="T">The floating-point type used in the geodetic object.</typeparam>
/// <remarks>
/// This interface extends the base 'IGeodeticBase' with type-specific operations and coordinate system.
/// </remarks>
public interface IGeodeticBase<TCoordinateSystem, TDatum, T> : IGeodeticBase<T>, IGeometricBase<TCoordinateSystem>
    where TCoordinateSystem : IGeodeticCoordinateSystem<T>
    where TDatum : IDatum<T>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Gets the coordinate system of the geometric object with type-specific coordinate system.
    /// </summary>
    /// <value>The coordinate system of the geometric object.</value>
    new GeodeticCoordinateSystem<TCoordinateSystem, T> CoordinateSystem
        => GeodeticCoordinateSystem<TCoordinateSystem, T>.Instance;

    /// <summary>
    /// Gets the coordinate system of the geodetic object.
    /// </summary>
    /// <value>The coordinate system of the geodetic object.</value>
    GeodeticCoordinateSystem<T> IGeodeticBase<T>.CoordinateSystem
        => CoordinateSystem;
}
