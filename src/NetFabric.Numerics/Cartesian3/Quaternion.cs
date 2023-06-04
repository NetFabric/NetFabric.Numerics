namespace NetFabric.Numerics.Cartesian3;

[System.Diagnostics.DebuggerDisplay("X = {X}, Y = {Y}, Z = {Z}, W = {W}")]
public readonly record struct Quaternion<T>(T X, T Y, T Z, T W)
    : IEquatable<Quaternion<T>>,
      IEqualityOperators<Quaternion<T>, Quaternion<T>, bool>,
      IComparable,
      IComparisonOperators<Quaternion<T>, Quaternion<T>, bool>,
      IAdditiveIdentity<Quaternion<T>, Quaternion<T>>,
      IMultiplicativeIdentity<Quaternion<T>, Quaternion<T>>,
      IUnaryPlusOperators<Quaternion<T>, Quaternion<T>>,
      IAdditionOperators<Quaternion<T>, Quaternion<T>, Quaternion<T>>,
      IUnaryNegationOperators<Quaternion<T>, Quaternion<T>>,
      ISubtractionOperators<Quaternion<T>, Quaternion<T>, Quaternion<T>>,
      IMinMaxValue<Quaternion<T>>
    where T : struct, INumber<T>, IMinMaxValue<T>
{
    #region constants

    /// <summary>
    /// Represents a quaternion whose 4 coordinates are equal to zero. This field is read-only.
    /// </summary>
    public static readonly Quaternion<T> Zero = new(T.Zero, T.Zero, T.Zero, T.Zero);

    /// <summary>
    /// Represents the identity quaternion.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="Identity"/> quaternion is a special quaternion that has a real part equal to 1 and an imaginary part equal to (0, 0, 0).
    /// Multiplying any quaternion by the <see cref="Identity"/> quaternion results in the same quaternion, and multiplying a quaternion by the conjugate of the <see cref="Identity"/> quaternion
    /// will give the original quaternion.
    /// </para>
    /// <para>
    /// The <see cref="Identity"/> quaternion serves as the multiplicative identity in quaternion algebra, similar to how the number 1 serves as the multiplicative identity in real number arithmetic.
    /// </para>
    /// </remarks>
    public static readonly Quaternion<T> Identity = new(T.Zero, T.Zero, T.Zero, T.One);

    static Quaternion<T> IAdditiveIdentity<Quaternion<T>, Quaternion<T>>.AdditiveIdentity
        => Zero;
    static Quaternion<T> IMultiplicativeIdentity<Quaternion<T>, Quaternion<T>>.MultiplicativeIdentity
        => Identity;

    static Quaternion<T> IMinMaxValue<Quaternion<T>>.MinValue
        => MinValue;
    static Quaternion<T> IMinMaxValue<Quaternion<T>>.MaxValue
        => MaxValue;

    #endregion

}
