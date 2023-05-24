namespace NetFabric.Numerics;

/// <summary>
/// Represents a point in a coordinate system.
/// </summary>
public interface IPoint<TSelf>
    : IEquatable<TSelf>,
      IEqualityOperators<TSelf, TSelf, bool>,
      IMinMaxValue<TSelf>
    where TSelf : IPoint<TSelf>?
{
    /// <summary>
    /// Represents the point with all coordinates as zero. This field is read-only.
    /// </summary>
    static abstract TSelf Zero { get; }

    /// <summary>
    /// Gets a coordinate system of the point.
    /// </summary>
    /// <value>The coordinate system of the point.</value>
    ICoordinateSystem CoordinateSystem { get; }

    /// <summary>
    /// Gets the value for a given coordinate of the point.
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