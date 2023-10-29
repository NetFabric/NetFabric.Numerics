namespace NetFabric.Numerics;

/// <summary>
/// Represents a point in a coordinate system.
/// </summary>
public interface IPoint : IGeometricBase
{
}

/// <summary>
/// Represents a point in a coordinate system with type-specific implementations.
/// </summary>
/// <typeparam name="TSelf">The type implementing the interface.</typeparam>
/// <typeparam name="TCoordinateSystem">The type representing the coordinate system.</typeparam>
/// <remarks>
/// This interface extends the base 'IPoint' interface with type-specific functionality.
/// </remarks>
public interface IPoint<TSelf, TCoordinateSystem> : IPoint, IGeometricBase<TSelf, TCoordinateSystem>, IMinMaxValue<TSelf>
    where TSelf : struct, IPoint<TSelf, TCoordinateSystem>?
    where TCoordinateSystem : ICoordinateSystem
{
}
