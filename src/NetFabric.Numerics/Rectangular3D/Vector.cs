using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace NetFabric.Numerics.Rectangular3D;

/// <summary>
/// Represents a 3D vector in a Rectangular coordinate system.
/// </summary>
/// <typeparam name="T">The numeric type used for the vector's components.</typeparam>
/// <remarks>
/// In a 3D Rectangular coordinate system, a vector is represented by a triplet of values (X, Y, Z) that specify its
/// direction and magnitude in 3D space. The X-component represents the horizontal direction, the Y-component
/// represents the vertical direction, and the Z-component represents the depth or altitude direction.
/// </remarks>
/// <param name="X">The X coordinate.</param>
/// <param name="Y">The Y coordinate.</param>
/// <param name="Z">The Z coordinate.</param>
[System.Diagnostics.DebuggerDisplay("X = {X}, Y = {Y}, Z = {Z}")]
[SkipLocalsInit]
public readonly record struct Vector<T>(T X, T Y, T Z)
    : IVector<Vector<T>, CoordinateSystem<T>, T>
    where T : struct, INumber<T>, IMinMaxValue<T>
{

    #region constants

    /// <summary>
    /// Represents a vector whose coordinates are equal to zero. This field is read-only.
    /// </summary>
    public static readonly Vector<T> Zero = new(T.Zero, T.Zero, T.Zero);

    static Vector<T> IGeometricBase<Vector<T>, CoordinateSystem<T>>.Zero
        => Zero;

    static Vector<T> IAdditiveIdentity<Vector<T>, Vector<T>>.AdditiveIdentity
        => Zero;

    /// <summary>
    /// Represents a vector whose X coordinate is one and others are zero. This field is read-only.
    /// </summary>
    public static readonly Vector<T> UnitX = new(T.One, T.Zero, T.Zero);

    /// <summary>
    /// Represents a vector whose Y coordinate is one and others are zero. This field is read-only.
    /// </summary>
    public static readonly Vector<T> UnitY = new(T.Zero, T.One, T.Zero);

    /// <summary>
    /// Represents a vector whose Z coordinate is one and others are zero. This field is read-only.
    /// </summary>
    public static readonly Vector<T> UnitZ = new(T.Zero, T.Zero, T.One);

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Vector<T> MinValue = new(T.MinValue, T.MinValue, T.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Vector<T> MaxValue = new(T.MaxValue, T.MaxValue, T.MaxValue);

    static Vector<T> IMinMaxValue<Vector<T>>.MinValue
        => MinValue;
    static Vector<T> IMinMaxValue<Vector<T>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="vector"/>.</typeparam>
    /// <param name="vector">The value which is used to create the instance of <see cref="Vector{T}"/></param>
    /// <returns>An instance of <see cref="Vector{T}"/> created from <paramref name="vector" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="vector" /> is not representable by <see cref="Vector{T}"/>.</exception>
    public static Vector<T> CreateChecked<TOther>(ref readonly Vector<TOther> vector)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>, IRootFunctions<TOther>
        => new(
            T.CreateChecked(vector.X),
            T.CreateChecked(vector.Y),
            T.CreateChecked(vector.Z)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="vector"/>.</typeparam>
    /// <param name="vector">The value which is used to create the instance of <see cref="Vector{T}"/></param>
    /// <returns>An instance of <see cref="Vector{T}"/> created from <paramref name="vector" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="vector" /> is not representable by <see cref="Vector{T}"/>.</exception>
    public static Vector<T> CreateSaturating<TOther>(ref readonly Vector<TOther> vector)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>, IRootFunctions<TOther>
        => new(
            T.CreateSaturating(vector.X),
            T.CreateSaturating(vector.Y),
            T.CreateSaturating(vector.Z)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="vector"/>.</typeparam>
    /// <param name="vector">The value which is used to create the instance of <see cref="Vector{T}"/></param>
    /// <returns>An instance of <see cref="Vector{T}"/> created from <paramref name="vector" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="vector" /> is not representable by <see cref="Vector{T}"/>.</exception>
    public static Vector<T> CreateTruncating<TOther>(ref readonly Vector<TOther> vector)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>, IRootFunctions<TOther>
        => new(
            T.CreateTruncating(vector.X),
            T.CreateTruncating(vector.Y),
            T.CreateTruncating(vector.Z)
        );

    object IGeometricBase.this[int index]
        => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };

    #region comparison

    /// <summary>
    /// Determines whether the magnitude of the left <see cref="Vector{T}"/> is less than the magnitude of the right <see cref="Vector{T}"/>.
    /// </summary>
    /// <param name="left">The left <see cref="Vector{T}"/> to compare.</param>
    /// <param name="right">The right <see cref="Vector{T}"/> to compare.</param>
    /// <returns>True if the magnitude of the left <see cref="Vector{T}"/> is less than the magnitude of the right <see cref="Vector{T}"/>; otherwise, false.</returns>
    /// <remarks>
    /// <para>
    /// This operator compares the magnitude of the left <see cref="Vector{T}"/> with the magnitude of the right <see cref="Vector{T}"/>. 
    /// It returns true if and only if the magnitude of the left <see cref="Vector{T}"/> is less than the magnitude of the right <see cref="Vector{T}"/>. 
    /// Otherwise, it returns false. The comparison is based on the magnitudes of the vectors and is independent of their directions.
    /// </para>
    /// <para>
    /// The comparison of magnitudes is performed using the <see cref="Vector.Magnitude{T}(ref readonly Vector{T})"/> method, which calculates the square root of the magnitude squared. 
    /// However, to optimize the performance, this operator directly compares the magnitude squared values of the vectors, accessible through the <see cref="Vector.MagnitudeSquared{T}(ref readonly Vector{T})"/> method.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Vector<T> left, Vector<T> right)
        => Vector.Compare(in left, in right) < 0;

    /// <summary>
    /// Determines whether the magnitude of the left <see cref="Vector{T}"/> is less than or equal to the magnitude of the right <see cref="Vector{T}"/>.
    /// </summary>
    /// <param name="left">The left <see cref="Vector{T}"/> to compare.</param>
    /// <param name="right">The right <see cref="Vector{T}"/> to compare.</param>
    /// <returns>True if the magnitude of the left <see cref="Vector{T}"/> is less than or equal to the magnitude of the right <see cref="Vector{T}"/>; otherwise, false.</returns>
    /// <remarks>
    /// <para>
    /// This operator compares the magnitude of the left <see cref="Vector{T}"/> with the magnitude of the right <see cref="Vector{T}"/>. 
    /// It returns true if and only if the magnitude of the left <see cref="Vector{T}"/> is less than or equal to the magnitude of the right <see cref="Vector{T}"/>. 
    /// Otherwise, it returns false. The comparison is based on the magnitudes of the vectors and is independent of their directions.
    /// </para>
    /// <para>
    /// The comparison of magnitudes is performed using the <see cref="Vector.Magnitude{T}(ref readonly Vector{T})"/> method, which calculates the square root of the magnitude squared. 
    /// However, to optimize the performance, this operator directly compares the magnitude squared values of the vectors, accessible through the <see cref="Vector.MagnitudeSquared{T}(ref readonly Vector{T})"/> method.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Vector<T> left, Vector<T> right)
        => Vector.Compare(in left, in right) <= 0;

    /// <summary>
    /// Determines whether the magnitude of the left <see cref="Vector{T}"/> is greater than the magnitude of the right <see cref="Vector{T}"/>.
    /// </summary>
    /// <param name="left">The left <see cref="Vector{T}"/> to compare.</param>
    /// <param name="right">The right <see cref="Vector{T}"/> to compare.</param>
    /// <returns>True if the magnitude of the left <see cref="Vector{T}"/> is greater than the magnitude of the right <see cref="Vector{T}"/>; otherwise, false.</returns>
    /// <remarks>
    /// <para>
    /// This operator compares the magnitude of the left <see cref="Vector{T}"/> with the magnitude of the right <see cref="Vector{T}"/>. 
    /// It returns true if and only if the magnitude of the left <see cref="Vector{T}"/> is greater than the magnitude of the right <see cref="Vector{T}"/>. 
    /// Otherwise, it returns false. The comparison is based on the magnitudes of the vectors and is independent of their directions.
    /// </para>
    /// <para>
    /// The comparison of magnitudes is performed using the <see cref="Vector.Magnitude{T}(ref readonly Vector{T})"/> method, which calculates the square root of the magnitude squared. 
    /// However, to optimize the performance, this operator directly compares the magnitude squared values of the vectors, accessible through the <see cref="Vector.MagnitudeSquared{T}(ref readonly Vector{T})"/> method.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Vector<T> left, Vector<T> right)
        => Vector.Compare(in left, in right) > 0;

    /// <summary>
    /// Determines whether the magnitude of the left <see cref="Vector{T}"/> is greater than or equal to the magnitude of the right <see cref="Vector{T}"/>.
    /// </summary>
    /// <param name="left">The left <see cref="Vector{T}"/> to compare.</param>
    /// <param name="right">The right <see cref="Vector{T}"/> to compare.</param>
    /// <returns>True if the magnitude of the left <see cref="Vector{T}"/> is greater than or equal to the magnitude of the right <see cref="Vector{T}"/>; otherwise, false.</returns>
    /// <remarks>
    /// <para>
    /// This operator compares the magnitude of the left <see cref="Vector{T}"/> with the magnitude of the right <see cref="Vector{T}"/>. 
    /// It returns true if and only if the magnitude of the left <see cref="Vector{T}"/> is greater than or equal to the magnitude of the right <see cref="Vector{T}"/>. 
    /// Otherwise, it returns false. The comparison is based on the magnitudes of the vectors and is independent of their directions.
    /// </para>
    /// <para>
    /// The comparison of magnitudes is performed using the <see cref="Vector.Magnitude{T}(ref readonly Vector{T})"/> method, which calculates the square root of the magnitude squared. 
    /// However, to optimize the performance, this operator directly compares the magnitude squared values of the vectors, accessible through the <see cref="Vector.MagnitudeSquared{T}(ref readonly Vector{T})"/> method.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Vector<T> left, Vector<T> right)
        => Vector.Compare(in left, in right) >= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    int IComparable<Vector<T>>.CompareTo(Vector<T> other)
        => Vector.MagnitudeSquared(in this).CompareTo(Vector.MagnitudeSquared(in other));

    readonly int IComparable.CompareTo(object? obj)
        => obj switch
        {
            null => 1,
            Vector<T> other => Vector.Compare(in this, in other),
            _ => Throw.ArgumentException<int>($"Argument must be of type {nameof(Vector<T>)}.", nameof(obj)),
        };

    #endregion

    #region operators

    /// <summary>
    /// Returns the input <see cref="Vector{T}"/> unchanged.
    /// </summary>
    /// <param name="right">The input <see cref="Vector{T}"/>.</param>
    /// <returns>The input <see cref="Vector{T}"/> unchanged.</returns>
    /// <remarks>
    /// <para>
    /// This operator returns the input <see cref="Vector{T}"/> unchanged. 
    /// It effectively represents the identity operation, where the same vector is returned without any modification.
    /// </para>
    /// <para>
    /// This operator is useful in scenarios where a unary plus sign is needed to explicitly indicate that the vector remains the same.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator +(Vector<T> right)
        => right;

    /// <summary>
    /// Adds two <see cref="Vector{T}"/> instances component-wise.
    /// </summary>
    /// <param name="left">The first <see cref="Vector{T}"/> to add.</param>
    /// <param name="right">The second <see cref="Vector{T}"/> to add.</param>
    /// <returns>A new <see cref="Vector{T}"/> that is the result of adding the corresponding components of the input vectors.</returns>
    /// <remarks>
    /// This operator performs component-wise addition of the input vectors, where each component of the resulting vector is the sum of the corresponding components of the input vectors.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator +(Vector<T> left, Vector<T> right)
        => Vector.Add(in left, in right);

    /// <summary>
    /// Negates the specified <see cref="Vector{T}"/> by reversing the sign of each component.
    /// </summary>
    /// <param name="right">The <see cref="Vector{T}"/> to negate.</param>
    /// <returns>A new <see cref="Vector{T}"/> with the negated values.</returns>
    /// <remarks>
    /// This operator reverses the sign of each component of the input vector, resulting in a new vector with the opposite direction.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator -(Vector<T> right)
        => new(-right.X, -right.Y, -right.Z);

    /// <summary>
    /// Subtracts the components of the second <see cref="Vector{T}"/> from the corresponding components of the first <see cref="Vector{T}"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Vector{T}"/>.</param>
    /// <param name="right">The second <see cref="Vector{T}"/>.</param>
    /// <returns>A new <see cref="Vector{T}"/> representing the element-wise subtraction of the two input vectors.</returns>
    /// <remarks>
    /// This operator subtracts the corresponding components of the second vector from the components of the first vector,
    /// resulting in a new vector with the subtracted values.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator -(Vector<T> left, Vector<T> right)
        => Vector.Subtract(in left, in right);

    /// <summary>
    /// Multiplies each component of the <see cref="Vector{T}"/> by the specified scalar value.
    /// </summary>
    /// <param name="left">The <see cref="Vector{T}"/> to multiply.</param>
    /// <param name="right">The scalar value to multiply by.</param>
    /// <returns>A new <see cref="Vector{T}"/> with each component multiplied by the scalar value.</returns>
    /// <remarks>
    /// This operator multiplies each component of the vector by the specified scalar value,
    /// resulting in a new vector with the scaled values.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator *(Vector<T> left, T right)
        => Vector.Multiply(right, in left);

    /// <summary>
    /// Multiplies the specified scalar value by each component of the <see cref="Vector{T}"/>.
    /// </summary>
    /// <param name="left">The scalar value to multiply.</param>
    /// <param name="right">The <see cref="Vector{T}"/> to multiply.</param>
    /// <returns>A new <see cref="Vector{T}"/> with each component multiplied by the scalar value.</returns>
    /// <remarks>
    /// This operator multiplies the specified scalar value by each component of the vector,
    /// resulting in a new vector with the scaled values.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator *(T left, Vector<T> right)
        => Vector.Multiply(left, in right);

    /// <summary>
    /// Divides each component of the <see cref="Vector{T}"/> by the specified scalar value.
    /// </summary>
    /// <param name="left">The <see cref="Vector{T}"/> to divide.</param>
    /// <param name="right">The scalar value to divide by.</param>
    /// <returns>A new <see cref="Vector{T}"/> with each component divided by the scalar value.</returns>
    /// <remarks>
    /// This operator divides each component of the vector by the specified scalar value,
    /// resulting in a new vector with the scaled values.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator /(Vector<T> left, T right)
        => Vector.Divide(in left, right);

    #endregion

    /// <summary>
    /// Converts the vector to its string representation.
    /// </summary>
    /// <returns>A string representation of the vector.</returns>
    public readonly override string ToString()
        => ToString(null);

    /// <summary>
    /// Converts the vector to its string representation using the specified format and format provider.
    /// </summary>
    /// <param name="format">The format specifier to apply to the vector's components. If null, the default format will be used.</param>
    /// <param name="formatProvider">The format provider to use for culture-specific formatting. If null, the current culture will be used.</param>
    /// <returns>A string representation of the vector.</returns>
    public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider = default)
    {
        var separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
        return $"<{X.ToString(format, formatProvider)}{separator} {Y.ToString(format, formatProvider)}{separator} {Z.ToString(format, formatProvider)}>";
    }
}

/// <summary>
/// Provides static methods for vector operations.
/// </summary>
public static partial class Vector
{
    /// <summary>
    /// Determines whether the specified vector is a zero vector, where all components are zero.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <param name="vector">The vector to check.</param>
    /// <returns><c>true</c> if all components of the vector are zero; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero<T>(ref readonly Vector<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => vector == Vector<T>.Zero;

    /// <summary>
    /// Determines whether the specified vector is a zero vector, where all components are zero.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <param name="vector">The vector to check.</param>
    /// <param name="tolerance">The tolerance value.</param>
    /// <returns><c>true</c> if all components of the vector are zero; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// The <see cref="IsZero{T}(ref readonly Vector{T}, T)"/> method checks whether all components of the vector are equal to zero within the <paramref name="tolerance"/> range.
    /// The tolerance is a small value used to account for floating-point precision errors.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero<T>(ref readonly Vector<T> vector, T tolerance)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => AreApproximatelyEqual(in vector, in Vector<T>.Zero, tolerance);

    /// <summary>
    /// Determines whether any component of the specified <see cref="Vector{T}"/> is NaN (Not-a-Number).
    /// </summary>
    /// <typeparam name="T">The type of the vector's components.</typeparam>
    /// <param name="vector">The <see cref="Vector{T}"/> to check for NaN values.</param>
    /// <returns>
    /// <c>true</c> if any component of the vector is NaN; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNaN<T>(ref readonly Vector<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => T.IsNaN(vector.X) || T.IsNaN(vector.Y) || T.IsNaN(vector.Z);

    /// <summary>
    /// Determines whether any component of the specified <see cref="Vector{T}"/> is positive or negative infinity.
    /// </summary>
    /// <typeparam name="T">The type of the vector's components.</typeparam>
    /// <param name="vector">The <see cref="Vector{T}"/> to check for infinity values.</param>
    /// <returns>
    /// <c>true</c> if any component of the vector is positive or negative infinity; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInfinity<T>(ref readonly Vector<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => T.IsInfinity(vector.X) || T.IsInfinity(vector.Y) || T.IsInfinity(vector.Z);

    /// <summary>
    /// Determines whether all components of the specified <see cref="Vector{T}"/> are finite numbers (not NaN, infinity, or negative infinity).
    /// </summary>
    /// <typeparam name="T">The type of the vector's components.</typeparam>
    /// <param name="vector">The <see cref="Vector{T}"/> to check for finite values.</param>
    /// <returns>
    /// <c>true</c> if all components of the vector are finite numbers; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite<T>(ref readonly Vector<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => T.IsFinite(vector.X) && T.IsFinite(vector.Y) && T.IsFinite(vector.Z);

    /// <summary>
    /// Determines whether the specified <see cref="Vector{T}"/> is a normalized vector.
    /// </summary>
    /// <typeparam name="T">The type of the vector's components.</typeparam>
    /// <param name="vector">The <see cref="Vector{T}"/> to check for normalization.</param>
    /// <returns>
    /// <c>true</c> if the vector is normalized (its magnitude is 1); otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNormalized<T>(ref readonly Vector<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => MagnitudeSquared(in vector) == T.One;

    /// <summary>
    /// Determines whether the specified <see cref="Vector{T}"/> is a normalized vector within the specified tolerance.
    /// </summary>
    /// <typeparam name="T">The type of the vector's components.</typeparam>
    /// <param name="vector">The <see cref="Vector{T}"/> to check for normalization.</param>
    /// <param name="tolerance">The tolerance used for the comparison.</param>
    /// <returns>
    /// <c>true</c> if the vector is normalized (its magnitude is within the specified tolerance of 1); otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNormalized<T>(ref readonly Vector<T> vector, T tolerance)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.AreApproximatelyEqual(MagnitudeSquared(in vector), T.One, tolerance);

    /// <summary>
    /// Checks if two floating-point values are approximately equal within the specified tolerance.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <param name="a">The first value to compare.</param>
    /// <param name="b">The second value to compare.</param>
    /// <param name="tolerance">The tolerance value.</param>
    /// <returns><c>true</c> if the values are approximately equal; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This method compares the absolute difference between the two values to the specified tolerance value.
    /// If the absolute difference is less than or equal to the tolerance, the values are considered approximately equal.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AreApproximatelyEqual<T>(ref readonly Vector<T> a, ref readonly Vector<T> b, T tolerance)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.AreApproximatelyEqual(a.X, b.X, tolerance) &&
            Utils.AreApproximatelyEqual(a.Y, b.Y, tolerance) &&
            Utils.AreApproximatelyEqual(a.Z, b.Z, tolerance);

    /// <summary>
    /// Compares two Vector instances and returns an indication of their relative values.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <param name="vector">The first Vector to compare.</param>
    /// <param name="other">The second Vector to compare.</param>
    /// <returns>
    /// A value that indicates the relative order of the Vector instances being compared.
    /// The return value has the following meanings:
    /// - Less than zero: <paramref name="vector"/> is less than <paramref name="other"/>.
    /// - Zero: <paramref name="vector"/> is equal to <paramref name="other"/>.
    /// - Greater than zero: <paramref name="vector"/> is greater than <paramref name="other"/>.
    /// </returns>
    /// <remarks>
    /// This method compares the squared magnitudes of the two Vector instances.
    /// The squared magnitude is used to avoid the costly square root operation and is sufficient
    /// for comparing the relative values of vectors. 
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Compare<T>(ref readonly Vector<T> vector, ref readonly Vector<T> other)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => MagnitudeSquared(in vector).CompareTo(MagnitudeSquared(in other));

    #region arithmetic

    /// <summary>
    /// Negates the specified Vector by reversing the sign of each of its coordinates.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <param name="right">The Vector to negate.</param>
    /// <returns>A new Vector with the negated coordinates.</returns>
    /// <remarks>
    /// This method creates a new Vector instance with the same magnitude as the input vector
    /// but with reversed signs for each coordinate. The resulting vector points in the opposite
    /// direction as the input vector. The input vector remains unchanged.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Negate<T>(ref readonly Vector<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>, ISignedNumber<T>
        => new(-right.X, -right.Y, -right.Z);

    /// <summary>
    /// Adds two vectors component-wise and returns the result as a new Vector.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <param name="left">The first Vector to add.</param>
    /// <param name="right">The second Vector to add.</param>
    /// <returns>A new Vector that is the component-wise sum of the input vectors.</returns>
    /// <remarks>
    /// This method adds the corresponding coordinates of the two input vectors and returns
    /// a new Vector with the resulting sums. The operation is performed component-wise, which
    /// means that the X, Y, and Z coordinates of the resulting vector are the sums of the
    /// X, Y, and Z coordinates of the input vectors, respectively. The input vectors remain
    /// unchanged.
    /// </remarks>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Add<T>(ref readonly Vector<T> left, ref readonly Vector<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    /// <summary>
    /// Subtracts the second vector from the first vector component-wise and returns the result as a new Vector.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <param name="left">The Vector to subtract from (the minuend).</param>
    /// <param name="right">The Vector to subtract (the subtrahend).</param>
    /// <returns>A new Vector that is the component-wise difference of the input vectors.</returns>
    /// <remarks>
    /// This method subtracts the corresponding coordinates of the second vector from the first vector
    /// and returns a new Vector with the resulting differences. The operation is performed component-wise,
    /// which means that the X, Y, and Z coordinates of the resulting vector are the differences of the
    /// X, Y, and Z coordinates of the input vectors, respectively. The input vectors remain unchanged.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Subtract<T>(ref readonly Vector<T> left, ref readonly Vector<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    /// <summary>
    /// Multiplies a scalar value with each coordinate of the input Vector and returns the result as a new Vector.
    /// </summary>
    /// <typeparam name="T">The type of the scalar and vector coordinates.</typeparam>
    /// <param name="left">The scalar value to multiply with each coordinate of the input vector.</param>
    /// <param name="right">The Vector to multiply.</param>
    /// <returns>A new Vector that is the result of multiplying each coordinate of the input vector by the scalar value.</returns>
    /// <remarks>
    /// This method multiplies the specified scalar value with each coordinate of the input Vector and returns a new Vector with the resulting products. 
    /// The operation is performed independently on each coordinate, meaning that the scalar value is multiplied with the X, Y, and Z coordinates of the input vector separately. 
    /// The input vector remains unchanged after the operation.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Multiply<T>(T left, ref readonly Vector<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(left * right.X, left * right.Y, left * right.Z);

    /// <summary>
    /// Divides each coordinate of the input Vector by a scalar value and returns the result as a new Vector.
    /// </summary>
    /// <typeparam name="T">The type of the scalar and vector coordinates.</typeparam>
    /// <param name="left">The Vector to divide.</param>
    /// <param name="right">The scalar value to divide each coordinate of the input vector by.</param>
    /// <returns>A new Vector that is the result of dividing each coordinate of the input vector by the scalar value.</returns>
    /// <remarks>
    /// This method divides each coordinate of the input Vector by the specified scalar value and returns a new Vector with the resulting quotients. 
    /// The operation is performed independently on each coordinate, meaning that each coordinate of the input vector is divided by the scalar value separately. 
    /// The input vector remains unchanged after the operation.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Divide<T>(ref readonly Vector<T> left, T right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(left.X / right, left.Y / right, left.Z / right);

    #endregion

    /// <summary>
    /// Returns a new vector that is clamped within the specified minimum and maximum values for each component.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <param name="vector">The vector to clamp.</param>
    /// <param name="min">The minimum values for each component.</param>
    /// <param name="max">The maximum values for each component.</param>
    /// <returns>
    /// A new vector that is clamped within the specified minimum and maximum values for each component.
    /// If any component of the <paramref name="vector"/> is less than the corresponding component in <paramref name="min"/>,
    /// the minimum value is used. If any component of the <paramref name="vector"/> is greater than the corresponding component
    /// in <paramref name="max"/>, the maximum value is used. Otherwise, the original <paramref name="vector"/> is returned.
    /// </returns>
    /// <remarks>
    /// <para>
    /// The <see cref="Clamp"/> method ensures that each component of the resulting vector is within the specified range.
    /// If any component of the <paramref name="vector"/> is less than the corresponding component in <paramref name="min"/>,
    /// that component is clamped to the minimum value. If any component of the <paramref name="vector"/> is greater than the
    /// corresponding component in <paramref name="max"/>, that component is clamped to the maximum value. Otherwise,
    /// if all components of the <paramref name="vector"/> are already within the range, the original vector is returned.
    /// </para>
    /// <para>
    /// This method is useful when you want to restrict a 3D vector to a certain range for each component.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Clamp<T>(ref readonly Vector<T> vector, ref readonly Vector<T> min, ref readonly Vector<T> max)
    where T : struct, INumber<T>, IMinMaxValue<T>
        => new(T.Clamp(vector.X, min.X, max.X), T.Clamp(vector.Y, min.Y, max.Y), T.Clamp(vector.Z, min.Z, max.Z));

    /// <summary>
    /// Performs linear interpolation between two vectors.
    /// </summary>
    /// <typeparam name="T">The type of the vector components.</typeparam>
    /// <param name="start">The starting vector.</param>
    /// <param name="end">The ending vector.</param>
    /// <param name="factor">The interpolation factor. Should be between 0 and 1.</param>
    /// <returns>A new vector that represents the interpolated value.</returns>
    /// <remarks>
    /// This method performs linear interpolation between the start and end vectors using the specified factor.
    /// The factor should be a value between 0 and 1, where 0 represents the start vector and 1 represents the end vector.
    /// The resulting vector is calculated by multiplying the start vector by (1 - factor), multiplying the end vector by factor,
    /// and then adding the two resulting vectors together.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Lerp<T>(ref readonly Vector<T> start, ref readonly Vector<T> end, T factor)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => (start * (T.One - factor)) + (end * factor);

    /// <summary>
    /// Calculates the magnitude (length) of the vector.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <returns>The magnitude of the vector.</returns>
    /// <remarks>
    /// <para>
    /// The magnitude is calculated as the Euclidean distance in the 2D Rectangular coordinate system.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Magnitude<T>(ref readonly Vector<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>, IRootFunctions<T>
        => T.Sqrt(MagnitudeSquared(in vector));

    /// <summary>
    /// Calculates the magnitude (length) of the vector.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <typeparam name="TOut">The numeric type used for the magnitude.</typeparam>
    /// <returns>The magnitude of the vector.</returns>
    /// <remarks>
    /// <para>
    /// The magnitude is calculated as the Euclidean distance in the 2D Rectangular coordinate system.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Magnitude<T, TOut>(ref readonly Vector<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        where TOut : struct, INumber<TOut>, IRootFunctions<TOut>
        => TOut.Sqrt(TOut.CreateChecked(MagnitudeSquared(in vector)));

    /// <summary>
    /// Calculates the square of the magnitude (length) of the vector.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <returns>The square of the magnitude of the vector.</returns>
    /// <remarks>
    /// <para>
    /// The square of the magnitude is calculated as the Euclidean distance in the 2D Rectangular coordinate system.
    /// </para>
    /// <para>
    /// Note that the square of the magnitude is returned instead of the actual magnitude to avoid the need for
    /// taking the square root, which can be a computationally expensive operation.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T MagnitudeSquared<T>(ref readonly Vector<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Utils.Square(vector.X) + Utils.Square(vector.Y) + Utils.Square(vector.Z);

    /// <summary>
    /// Returns a new vector that represents the normalized form of the specified vector.
    /// </summary>
    /// <param name="vector">The vector to normalize.</param>
    /// <returns>
    /// A new vector that represents the normalized form of the specified vector.
    /// </returns>
    /// <remarks>
    /// <para>
    /// The <see cref="Normalize"/> method calculates a normalized form of the specified <paramref name="vector"/>.
    /// The resulting vector will have the same direction as the original vector but will have a magnitude of 1.
    /// </para>
    /// <para>
    /// To normalize a vector means to scale it to a magnitude of 1 while preserving its direction.
    /// This is useful when you want to work with direction vectors or ensure consistent scaling across different vectors.
    /// </para>
    /// <para>
    /// Note that if the specified <paramref name="vector"/> is a zero vector (all components are zero), 
    /// the method will return the zero vector itself, as it cannot be normalized.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Normalize<T>(ref readonly Vector<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>, IRootFunctions<T>
    {
        var length = Magnitude(in vector);
        return length != T.Zero
            ? Divide(in vector, length)
            : Vector<T>.Zero;
    }

    /// <summary>
    /// Calculates the dot product.
    /// </summary>
    /// <param name="left">A vector.</param>
    /// <param name="right">A vector.</param>
    /// <returns>The dot product.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Dot<T>(ref readonly Vector<T> left, ref readonly Vector<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);

    /// <summary>
    /// Calculates the cross product.
    /// </summary>
    /// <param name="left">A vector.</param>
    /// <param name="right">A vector.</param>
    /// <returns>The cross products.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Cross<T>(ref readonly Vector<T> left, ref readonly Vector<T> right)
            where T : struct, INumber<T>, IMinMaxValue<T>
            => new((left.Y * right.Z) - (left.Z * right.Y),
                    (left.Z * right.X) - (left.X * right.Z),
                    (left.X * right.Y) - (left.Y * right.X));

    /// <summary>
    /// Gets the smallest angle between two vectors.
    /// </summary>
    /// <typeparam name="T">The numeric type used internally by <paramref name="from"/> and <paramref name="to"/>.</typeparam>
    /// <param name="from">The vector where the angle measurement starts at.</param>
    /// <param name="to">The vector where the angle measurement stops at.</param>
    /// <returns>The angle between two vectors.</returns>
    /// <remarks>The angle is always less than 180 degrees.</remarks>
    public static AngleReduced<Radians, T> AngleBetween<T>(ref readonly Vector<T> from, ref readonly Vector<T> to)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>, IRootFunctions<T>
        => Angle.Acos(Dot(in from, in to) / (Magnitude(in from) * Magnitude(in to)));
}