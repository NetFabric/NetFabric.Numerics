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
    /// <summary>
    /// Represents the vector with zero length. This field is read-only.
    /// </summary>
    static abstract TSelf Zero { get; }

    /// <summary>
    /// Gets the coordinate system of the vector.
    /// </summary>
    /// <value>The coordinate system of the vector.</value>
    ICoordinateSystem CoordinateSystem { get; }

    /// <summary>
    /// Gets the value for a given coordinate of the vector.
    /// </summary>
    /// <param name="index">The index of the coordinate to get the value.</param>
    /// <value>The value of the coordinate indexed by index.</value>
    /// <remarks>
    /// <para>
    /// The maximum value for the index is the number of coordinates minus one.
    /// </para>
    /// <para>
    /// The number of coordinates can be obtained from the <see cref="ICoordinateSystem.Coordinates"/> property.
    /// </para>
    /// </remarks>
    object this[int index] { get; }
}