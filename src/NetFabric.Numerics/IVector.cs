namespace NetFabric.Numerics;

/// <summary>
/// Represents a vector.
/// </summary>
/// <typeparam name="TSelf">The type that implements the interface.</typeparam>
/// <typeparam name="T">The type of the vector coordinates.</typeparam>
public interface IVector<TSelf, T>
    : INumericBase<TSelf>,
      IComparable,
      IComparable<TSelf>,
      IComparisonOperators<TSelf, TSelf, bool>,
      IAdditiveIdentity<TSelf, TSelf>,
      IUnaryPlusOperators<TSelf, TSelf>,
      IAdditionOperators<TSelf, TSelf, TSelf>,
      IUnaryNegationOperators<TSelf, TSelf>,
      ISubtractionOperators<TSelf, TSelf, TSelf>,
      IMultiplyOperators<TSelf, T, TSelf>,
      IDivisionOperators<TSelf, T, TSelf>,
      IMinMaxValue<TSelf>
    where TSelf : struct, IVector<TSelf, T>?
    where T : struct, INumber<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Gets the number of coordinates/dimensions of the vector.
    /// </summary>
    /// <remarks>
    /// The <see cref="Dimension"/> property represents the size or number of coordinates in the vector.
    /// The value of this property will depend on the specific implementation of the Vector type.
    /// </remarks>
    int Dimension { get; }

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
    T this[int index] { get; }

    /// <summary>
    /// Determines whether the vector is normalized, i.e., has a magnitude of approximately 1.
    /// </summary>
    /// <param name="vector">The vector to check.</param>
    /// <returns><c>true</c> if the vector is normalized; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// A normalized vector is a vector that has a magnitude of approximately 1. This means that its components
    /// are scaled such that the vector represents only the direction without any magnitude. In other words,
    /// the vector points in a specific direction, but its length or magnitude is 1.
    /// 
    /// To determine if a vector is normalized, the magnitude of the vector is compared to a tolerance value,
    /// which represents the allowable deviation from the ideal magnitude of 1. If the magnitude falls within
    /// the specified tolerance, the vector is considered normalized.
    /// 
    /// Note that due to the nature of floating-point arithmetic, it is recommended to use a tolerance value
    /// when checking for vector normalization. This accounts for potential rounding errors and precision
    /// limitations that can occur during calculations.
    /// </remarks>
    public static virtual bool IsNormalized(in TSelf vector) 
        => TSelf.MagnitudeSquared(vector) == T.One;

    /// <summary>
    /// Calculates the magnitude (length) of the vector.
    /// </summary>
    /// <typeparam name="TOut">The numeric type used for the magnitude.</typeparam>
    /// <returns>The magnitude of the vector.</returns>
    /// <remarks>
    /// <para>
    /// The magnitude is calculated as the Euclidean distance in the 2D Cartesian coordinate system.
    /// </para>
    /// </remarks>
    public static virtual TOut Magnitude<TOut>(in TSelf vector)
        where TOut : struct, INumber<TOut>, IRootFunctions<TOut>
        => TOut.Sqrt(TOut.CreateChecked(TSelf.MagnitudeSquared(vector)));

    /// <summary>
    /// Calculates the square of the magnitude (length) of the vector.
    /// </summary>
    /// <returns>The square of the magnitude of the vector.</returns>
    /// <remarks>
    /// <para>
    /// The square of the magnitude is calculated as the Euclidean distance in the 2D Cartesian coordinate system.
    /// </para>
    /// <para>
    /// Note that the square of the magnitude is returned instead of the actual magnitude to avoid the need for
    /// taking the square root, which can be a computationally expensive operation.
    /// </para>
    /// </remarks>
    public static abstract T MagnitudeSquared(in TSelf vector);

    /// <summary>
    /// Calculates the dot product.
    /// </summary>
    /// <param name="left">A vector.</param>
    /// <param name="right">A vector.</param>
    /// <returns>The dot product.</returns>
    static abstract T Dot(in TSelf left, in TSelf right);
}