namespace NetFabric.Numerics;

/// <summary>
/// Represents a point in a coordinate system.
/// </summary>
public interface IPoint<TSelf>
    : IGeometricBase<TSelf>,
      IMinMaxValue<TSelf>
    where TSelf : struct, IPoint<TSelf>?
{
}