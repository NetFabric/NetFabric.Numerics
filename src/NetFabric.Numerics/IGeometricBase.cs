namespace NetFabric.Numerics;

/// <summary>
/// Represents a geometric type.
/// </summary>
/// <typeparam name="TSelf">The type that implements the interface.</typeparam>
public interface IGeometricBase<TSelf>
    : IEquatable<TSelf>,
      IEqualityOperators<TSelf, TSelf, bool>
    where TSelf : struct, IGeometricBase<TSelf>?
{    
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

    /// <summary>Gets the value <c>0</c> for the type.</summary>
    static abstract TSelf Zero { get; }

    /// <summary>
    /// Determines whether <paramref name="value"/> is zero.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if the value is a zero vector; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(TSelf value) 
        => value.Equals(TSelf.Zero);
}
