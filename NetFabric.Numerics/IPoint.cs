using System.Numerics;

namespace NetFabric.Numerics;

/// <summary>
/// Represents a point in a coordinate system.
/// </summary>
public interface IPoint<TSelf>
    : IAdditionOperators<TSelf, TSelf, TSelf>
    where TSelf: IPoint<TSelf>?
{
    /// <summary>
    /// Gets a coordinate system of the point.
    /// </summary>
    /// <value>The coordinate system of the point.</value>
    ICoordinateSystem CoordinateSystem { get; }

    /// <summary>
    /// Gets a coordinate of the point.
    /// </summary>
    /// <param name="index">The index of the coordinate to get the value.</param>
    /// <value>The value of the coordinate indexed by index.</value>
    object this[int index] { get; }
}