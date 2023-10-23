namespace NetFabric.Numerics;

/// <summary>
/// Represents a point ref readonly a coordinate system.
/// </summary>
public interface IPoint<TSelf>
    : IGeometricBase<TSelf>,
      IMinMaxValue<TSelf>
    where TSelf : struct, IPoint<TSelf>?
{
}