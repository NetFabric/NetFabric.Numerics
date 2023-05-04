using System;
using System.Numerics;

namespace NetFabric.Numerics;

/// <summary>
/// Represents a vector in a coordinate system.
/// </summary>
public interface IVector<TSelf>
    : IEquatable<TSelf>,
      IEqualityOperators<TSelf, TSelf, bool>,
      IAdditionOperators<TSelf, TSelf, TSelf>,
      IAdditiveIdentity<TSelf, TSelf>
    where TSelf: IVector<TSelf>?
{
    /// <summary>
    /// Gets a coordinate system of the vector.
    /// </summary>
    /// <value>The coordinate system of the vector.</value>
    ICoordinateSystem CoordinateSystem { get; }

    /// <summary>
    /// Gets a coordinate of the vector.
    /// </summary>
    /// <param name="index">The index of the coordinate to get the value.</param>
    /// <value>The value of the coordinate indexed by index.</value>
    object this[int index] { get; }
}