namespace NetFabric.Numerics;

/// <summary>
/// Represents a point in a coordinate system.
/// </summary>
/// <typeparam name="TSelf">The type implementing the interface.</typeparam>
/// <typeparam name="TCoordinateSystem">The type representing the coordinate system.</typeparam>
public interface IPoint<TSelf, TCoordinateSystem>
    : IGeometricBase<TSelf, TCoordinateSystem>,
      IMinMaxValue<TSelf>
    where TSelf : struct, IPoint<TSelf, TCoordinateSystem>?
    where TCoordinateSystem : ICoordinateSystem
{
}