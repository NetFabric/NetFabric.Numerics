namespace NetFabric.Numerics;

/// <summary>
/// Represents a vector in a coordinate system.
/// </summary>
public interface IVector<TSelf>
    : IEquatable<TSelf>,
      IEqualityOperators<TSelf, TSelf, bool>,
      IComparable,
      IComparisonOperators<TSelf, TSelf, bool>,
      IAdditiveIdentity<TSelf, TSelf>,
      IUnaryPlusOperators<TSelf, TSelf>,
      IAdditionOperators<TSelf, TSelf, TSelf>,
      IUnaryNegationOperators<TSelf, TSelf>,
      ISubtractionOperators<TSelf, TSelf, TSelf>,
      IMinMaxValue<TSelf>
    where TSelf : IVector<TSelf>?
{
    static abstract TSelf Zero { get; }

    /// <summary>
    /// Gets the coordinate system of the vector.
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