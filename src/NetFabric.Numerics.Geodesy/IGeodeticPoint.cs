using System.Numerics;

namespace NetFabric.Numerics.Geodesy;

/// <summary>
/// Represents an interface for geodetic points with type-specific operations.
/// </summary>
/// <typeparam name="T">The floating-point type used in the geodetic point.</typeparam>
/// <remarks>
/// This interface provides functionality related to geodetic points for a specific floating-point type 'T'.
/// </remarks>
public interface IGeodeticPoint<T> : IPoint, IGeodeticBase<T>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
}

/// <summary>
/// Represents a point in a geodetic coordinate system with type-specific operations.
/// </summary>
/// <typeparam name="TSelf">The type implementing the interface.</typeparam>
/// <typeparam name="TCoordinateSystem">The type of geodetic coordinate system.</typeparam>
/// <typeparam name="TDatum">The type representing the datum.</typeparam>
/// <typeparam name="T">The floating-point type used in the geodetic point.</typeparam>
/// <remarks>
/// This interface extends the base 'IGeodeticPoint' with type-specific operations and coordinates.
/// </remarks>
public interface IGeodeticPoint<TSelf, TCoordinateSystem, TDatum, T> : IGeodeticPoint<T>, IPoint<TSelf, TCoordinateSystem>, IGeodeticBase<TCoordinateSystem, TDatum, T>
    where TSelf : struct, IGeodeticPoint<TSelf, TCoordinateSystem, TDatum, T>?
    where TCoordinateSystem : IGeodeticCoordinateSystem<TCoordinateSystem, TDatum, T>
    where TDatum : IDatum<T>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
}
