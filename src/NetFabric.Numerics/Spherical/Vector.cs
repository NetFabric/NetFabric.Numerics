using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace NetFabric.Numerics.Spherical;

/// <summary>
/// Represents a vector in a Spherical coordinate system.
/// </summary>
/// <typeparam name="TAngleUnits">The units used for the angles.</typeparam>
/// <typeparam name="T">The type of the vector's components.</typeparam>
/// <remarks>
/// In a Spherical coordinate system, a vector is represented by three values: the magnitude (radius), the azimuth, and the polar angle.
/// The magnitude represents the length or distance of the vector, the azimuth represents the angle measured counterclockwise
/// from a reference direction in the XY plane, and the polar angle represents the angle measured from the positive Z-axis.
/// The choice of angle units is determined by the specified angle units type, TAngleUnits.
/// </remarks>
/// <param name="Radius">The radius coordinate.</param>
/// <param name="Azimuth">The azimuth coordinate.</param>
/// <param name="Polar">The polar coordinate.</param>
[System.Diagnostics.DebuggerDisplay("Radius = {Radius}, Azimuth = {Azimuth}, Polar = {Polar}")]
[SkipLocalsInit]
public readonly record struct Vector<TAngleUnits, T>(T Radius, Angle<TAngleUnits, T> Azimuth, Angle<TAngleUnits, T> Polar)
    : IVector<Vector<TAngleUnits, T>, CoordinateSystem<TAngleUnits, T>, T>
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{

    #region constants

    /// <summary>
    /// Represents a vector whose coordinates are equal to zero. This field is read-only.
    /// </summary>
    public static readonly Vector<TAngleUnits, T> Zero = new(T.Zero, Angle<TAngleUnits, T>.Zero, Angle<TAngleUnits, T>.Zero);

    static Vector<TAngleUnits, T> IGeometricBase<Vector<TAngleUnits, T>, CoordinateSystem<TAngleUnits, T>>.Zero
        => Zero;

    static Vector<TAngleUnits, T> IAdditiveIdentity<Vector<TAngleUnits, T>, Vector<TAngleUnits, T>>.AdditiveIdentity
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Vector<TAngleUnits, T> MinValue = new(T.MinValue, Angle<TAngleUnits, T>.MinValue, Angle<TAngleUnits, T>.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Vector<TAngleUnits, T> MaxValue = new(T.MaxValue, Angle<TAngleUnits, T>.MaxValue, Angle<TAngleUnits, T>.MaxValue);

    static Vector<TAngleUnits, T> IMinMaxValue<Vector<TAngleUnits, T>>.MinValue
        => MinValue;
    static Vector<TAngleUnits, T> IMinMaxValue<Vector<TAngleUnits, T>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TAngleUnits, T> CoordinateSystem
        => new();

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="vector"/>.</typeparam>
    /// <param name="vector">The value which is used to create the instance of <see cref="Vector{TAngleUnits, T}"/></param>
    /// <returns>An instance of <see cref="Vector{TAngleUnits, T}"/> created from <paramref name="vector" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="vector" /> is not representable by <see cref="Vector{TAngleUnits, T}"/>.</exception>
    public static Vector<TAngleUnits, T> CreateChecked<TOther>(in Vector<TAngleUnits, TOther> vector)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateChecked(vector.Radius),
            Angle<TAngleUnits, T>.CreateChecked(vector.Azimuth),
            Angle<TAngleUnits, T>.CreateChecked(vector.Polar));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="vector"/>.</typeparam>
    /// <param name="vector">The value which is used to create the instance of <see cref="Vector{TAngleUnits, T}"/></param>
    /// <returns>An instance of <see cref="Vector{TAngleUnits, T}"/> created from <paramref name="vector" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="vector" /> is not representable by <see cref="Vector{TAngleUnits, T}"/>.</exception>
    public static Vector<TAngleUnits, T> CreateSaturating<TOther>(in Vector<TAngleUnits, TOther> vector)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateSaturating(vector.Radius),
            Angle<TAngleUnits, T>.CreateSaturating(vector.Azimuth),
            Angle<TAngleUnits, T>.CreateSaturating(vector.Polar));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="vector"/>.</typeparam>
    /// <param name="vector">The value which is used to create the instance of <see cref="Vector{TAngleUnits, T}"/></param>
    /// <returns>An instance of <see cref="Vector{TAngleUnits, T}"/> created from <paramref name="vector" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="vector" /> is not representable by <see cref="Vector{TAngleUnits, T}"/>.</exception>
    public static Vector<TAngleUnits, T> CreateTruncating<TOther>(in Vector<TAngleUnits, TOther> vector)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateTruncating(vector.Radius),
            Angle<TAngleUnits, T>.CreateTruncating(vector.Azimuth),
            Angle<TAngleUnits, T>.CreateTruncating(vector.Polar));

    object IGeometricBase<Vector<TAngleUnits, T>, CoordinateSystem<TAngleUnits, T>>.this[int index]
        => index switch
        {
            0 => Radius,
            1 => Azimuth,
            2 => Polar,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };

    #region comparison

    /// <summary>
    /// Determines whether the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> is less than the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>.
    /// </summary>
    /// <param name="left">The left <see cref="Vector{TAngleUnits, T}"/> to compare.</param>
    /// <param name="right">The right <see cref="Vector{TAngleUnits, T}"/> to compare.</param>
    /// <returns>True if the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> is less than the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>; otherwise, false.</returns>
    /// <remarks>
    /// <para>
    /// This operator compares the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> with the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>. 
    /// It returns true if and only if the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> is less than the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>. 
    /// Otherwise, it returns false. The comparison is based on the magnitudes of the vectors and is independent of their directions.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Vector<TAngleUnits, T> left, Vector<TAngleUnits, T> right)
        => Vector.Compare(in left, in right) < 0;

    /// <summary>
    /// Determines whether the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> is less than or equal to the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>.
    /// </summary>
    /// <param name="left">The left <see cref="Vector{TAngleUnits, T}"/> to compare.</param>
    /// <param name="right">The right <see cref="Vector{TAngleUnits, T}"/> to compare.</param>
    /// <returns>True if the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> is less than or equal to the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>; otherwise, false.</returns>
    /// <remarks>
    /// <para>
    /// This operator compares the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> with the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>. 
    /// It returns true if and only if the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> is less than or equal to the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>. 
    /// Otherwise, it returns false. The comparison is based on the magnitudes of the vectors and is independent of their directions.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Vector<TAngleUnits, T> left, Vector<TAngleUnits, T> right)
        => Vector.Compare(in left, in right) <= 0;

    /// <summary>
    /// Determines whether the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> is greater than the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>.
    /// </summary>
    /// <param name="left">The left <see cref="Vector{TAngleUnits, T}"/> to compare.</param>
    /// <param name="right">The right <see cref="Vector{TAngleUnits, T}"/> to compare.</param>
    /// <returns>True if the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> is greater than the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>; otherwise, false.</returns>
    /// <remarks>
    /// <para>
    /// This operator compares the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> with the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>. 
    /// It returns true if and only if the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> is greater than the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>. 
    /// Otherwise, it returns false. The comparison is based on the magnitudes of the vectors and is independent of their directions.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Vector<TAngleUnits, T> left, Vector<TAngleUnits, T> right)
        => Vector.Compare(in left, in right) > 0;

    /// <summary>
    /// Determines whether the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> is greater than or equal to the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>.
    /// </summary>
    /// <param name="left">The left <see cref="Vector{TAngleUnits, T}"/> to compare.</param>
    /// <param name="right">The right <see cref="Vector{TAngleUnits, T}"/> to compare.</param>
    /// <returns>True if the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> is greater than or equal to the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>; otherwise, false.</returns>
    /// <remarks>
    /// <para>
    /// This operator compares the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> with the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>. 
    /// It returns true if and only if the magnitude of the left <see cref="Vector{TAngleUnits, T}"/> is greater than or equal to the magnitude of the right <see cref="Vector{TAngleUnits, T}"/>. 
    /// Otherwise, it returns false. The comparison is based on the magnitudes of the vectors and is independent of their directions.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Vector<TAngleUnits, T> left, Vector<TAngleUnits, T> right)
        => Vector.Compare(in left, in right) >= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    int IComparable<Vector<TAngleUnits, T>>.CompareTo(Vector<TAngleUnits, T> other)
        => Vector.Magnitude(in this).CompareTo(Vector.Magnitude(in other));

    readonly int IComparable.CompareTo(object? obj)
        => obj switch
        {
            null => 1,
            Vector<TAngleUnits, T> other => Vector.Compare(in this, in other),
            _ => Throw.ArgumentException<int>($"Argument must be of type {nameof(Vector<TAngleUnits, T>)}.", nameof(obj)),
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
    public static Vector<TAngleUnits, T> operator +(Vector<TAngleUnits, T> right)
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
    public static Vector<TAngleUnits, T> operator +(Vector<TAngleUnits, T> left, Vector<TAngleUnits, T> right)
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
    public static Vector<TAngleUnits, T> operator -(Vector<TAngleUnits, T> right)
        => new(-right.Radius, -right.Azimuth, -right.Polar);

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
    public static Vector<TAngleUnits, T> operator -(Vector<TAngleUnits, T> left, Vector<TAngleUnits, T> right)
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
    public static Vector<TAngleUnits, T> operator *(Vector<TAngleUnits, T> left, T right)
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
    public static Vector<TAngleUnits, T> operator *(T left, Vector<TAngleUnits, T> right)
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
    public static Vector<TAngleUnits, T> operator /(Vector<TAngleUnits, T> left, T right)
        => Vector.Divide(in left, right);

    #endregion

    /// <summary>
    /// Converts the vector to its string representation using the specified format and format provider.
    /// </summary>
    /// <param name="format">The format specifier to apply to the vector's components. If null, the default format will be used.</param>
    /// <param name="formatProvider">The format provider to use for culture-specific formatting. If null, the current culture will be used.</param>
    /// <returns>A string representation of the vector.</returns>
    public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider = default)
    {
        var separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
        return $"<{Radius.ToString(format, formatProvider)}{separator} {Azimuth.ToString(format, formatProvider)}{separator} {Polar.ToString(format, formatProvider)}>";
    }
}

/// <summary>
/// Provides static methods for vector operations.
/// </summary>
public static class Vector
{
    /// <summary>
    /// Determines whether the specified vector is a zero vector, where all components are zero.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="vector">The vector to check.</param>
    /// <returns><c>true</c> if all components of the vector are zero; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero<TAngleUnits, T>(in Vector<TAngleUnits, T> vector)
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => vector == Vector<TAngleUnits, T>.Zero;

    /// <summary>
    /// Determines whether the specified vector is a zero vector, where all components are zero.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="vector">The vector to check.</param>
    /// <param name="tolerance">The tolerance value.</param>
    /// <returns><c>true</c> if all components of the vector are zero; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// The <see cref="Vector.IsZero{TAngleUnits, T}(in Vector{TAngleUnits, T}, T)"/> method checks whether all components of the vector are equal to zero within the <paramref name="tolerance"/> range.
    /// The tolerance is a small value used to account for floating-point precision errors.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero<TAngleUnits, T>(in Vector<TAngleUnits, T> vector, T tolerance)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => AreApproximatelyEqual(in vector, in Vector<TAngleUnits, T>.Zero, tolerance);

    /// <summary>
    /// Determines whether any component of the specified <see cref="Vector{T}"/> is NaN (Not-a-Number).
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="vector">The <see cref="Vector{T}"/> to check for NaN values.</param>
    /// <returns>
    /// <c>true</c> if any component of the vector is NaN; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNaN<TAngleUnits, T>(in Vector<TAngleUnits, T> vector)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => T.IsNaN(vector.Radius) || Angle.IsNaN(vector.Azimuth) || Angle.IsNaN(vector.Polar);

    /// <summary>
    /// Determines whether any component of the specified <see cref="Vector{T}"/> is positive or negative infinity.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="vector">The <see cref="Vector{T}"/> to check for infinity values.</param>
    /// <returns>
    /// <c>true</c> if any component of the vector is positive or negative infinity; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInfinity<TAngleUnits, T>(in Vector<TAngleUnits, T> vector)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => T.IsInfinity(vector.Radius) || Angle.IsInfinity(vector.Azimuth) || Angle.IsInfinity(vector.Polar);

    /// <summary>
    /// Determines whether all components of the specified <see cref="Vector{T}"/> are finite numbers (not NaN, infinity, or negative infinity).
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="vector">The <see cref="Vector{T}"/> to check for finite values.</param>
    /// <returns>
    /// <c>true</c> if all components of the vector are finite numbers; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite<TAngleUnits, T>(in Vector<TAngleUnits, T> vector)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => T.IsFinite(vector.Radius) && Angle.IsFinite(vector.Azimuth) && Angle.IsFinite(vector.Polar);

    /// <summary>
    /// Determines whether the specified <see cref="Vector{T}"/> is a normalized vector.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="vector">The <see cref="Vector{T}"/> to check for normalization.</param>
    /// <returns>
    /// <c>true</c> if the vector is normalized (its magnitude is 1); otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNormalized<TAngleUnits, T>(in Vector<TAngleUnits, T> vector)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Vector.Magnitude(in vector) == T.One;

    /// <summary>
    /// Determines whether the specified <see cref="Vector{T}"/> is a normalized vector within the specified tolerance.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="vector">The <see cref="Vector{T}"/> to check for normalization.</param>
    /// <param name="tolerance">The tolerance used for the comparison.</param>
    /// <returns>
    /// <c>true</c> if the vector is normalized (its magnitude is within the specified tolerance of 1); otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNormalized<TAngleUnits, T>(in Vector<TAngleUnits, T> vector, T tolerance)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.AreApproximatelyEqual(Vector.Magnitude(in vector), T.One, tolerance);

    /// <summary>
    /// Checks if two floating-point values are approximately equal within the specified tolerance.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="a">The first value to compare.</param>
    /// <param name="b">The second value to compare.</param>
    /// <param name="tolerance">The tolerance value.</param>
    /// <returns><c>true</c> if the values are approximately equal; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This method compares the absolute difference between the two values to the specified tolerance value.
    /// If the absolute difference is less than or equal to the tolerance, the values are considered approximately equal.
    /// </remarks>    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AreApproximatelyEqual<TAngleUnits, T>(in Vector<TAngleUnits, T> a, in Vector<TAngleUnits, T> b, T tolerance)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.AreApproximatelyEqual(a.Radius, b.Radius, tolerance) &&
            Utils.AreApproximatelyEqual(a.Azimuth.Value, b.Azimuth.Value, tolerance) &&
            Utils.AreApproximatelyEqual(a.Polar.Value, b.Polar.Value, tolerance);

    /// <summary>
    /// Compares two Vector instances and returns an indication of their relative values.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
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
    public static int Compare<TAngleUnits, T>(in Vector<TAngleUnits, T> vector, in Vector<TAngleUnits, T> other)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Magnitude(in vector).CompareTo(Magnitude(in other));

    #region arithmetic

    /// <summary>
    /// Negates the specified Vector by reversing the sign of each of its coordinates.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="right">The Vector to negate.</param>
    /// <returns>A new Vector with the negated coordinates.</returns>
    /// <remarks>
    /// This method creates a new Vector instance with the same magnitude as the input vector
    /// but with reversed signs for each coordinate. The resulting vector points in the opposite
    /// direction as the input vector. The input vector remains unchanged.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<TAngleUnits, T> Negate<TAngleUnits, T>(in Vector<TAngleUnits, T> right)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(-right.Radius, -right.Azimuth, -right.Polar);

    /// <summary>
    /// Adds two vectors component-wise and returns the result as a new Vector.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="left">The first Vector to add.</param>
    /// <param name="right">The second Vector to add.</param>
    /// <returns>A new Vector that is the component-wise sum of the input vectors.</returns>
    /// <remarks>
    /// This method adds the corresponding coordinates of the two input vectors and returns
    /// a new Vector with the resulting sums. The operation is performed component-wise, which
    /// means that the Radius, Azimuth, and Polar coordinates of the resulting vector are the sums of the
    /// Radius, Azimuth, and Polar coordinates of the input vectors, respectively. The input vectors remain
    /// unchanged.
    /// </remarks>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<TAngleUnits, T> Add<TAngleUnits, T>(in Vector<TAngleUnits, T> left, in Vector<TAngleUnits, T> right)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(left.Radius + right.Radius, left.Azimuth + right.Azimuth, left.Polar + right.Polar);

    /// <summary>
    /// Subtracts the second vector from the first vector component-wise and returns the result as a new Vector.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="left">The Vector to subtract from (the minuend).</param>
    /// <param name="right">The Vector to subtract (the subtrahend).</param>
    /// <returns>A new Vector that is the component-wise difference of the input vectors.</returns>
    /// <remarks>
    /// This method subtracts the corresponding coordinates of the second vector from the first vector
    /// and returns a new Vector with the resulting differences. The operation is performed component-wise,
    /// which means that the Radius, Azimuth, and Polar coordinates of the resulting vector are the differences of the
    /// Radius, Azimuth, and Polar coordinates of the input vectors, respectively. The input vectors remain unchanged.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<TAngleUnits, T> Subtract<TAngleUnits, T>(in Vector<TAngleUnits, T> left, in Vector<TAngleUnits, T> right)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(left.Radius - right.Radius, left.Azimuth - right.Azimuth, left.Polar - right.Polar);

    /// <summary>
    /// Multiplies a scalar value with each coordinate of the input Vector and returns the result as a new Vector.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="left">The scalar value to multiply with each coordinate of the input vector.</param>
    /// <param name="right">The Vector to multiply.</param>
    /// <returns>A new Vector that is the result of multiplying each coordinate of the input vector by the scalar value.</returns>
    /// <remarks>
    /// This method multiplies the specified scalar value with each coordinate of the input Vector and returns a new Vector with the resulting products. 
    /// The operation is performed independently on each coordinate, meaning that the scalar value is multiplied with the Radius, Azimuth, and Polar coordinates of the input vector separately. 
    /// The input vector remains unchanged after the operation.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<TAngleUnits, T> Multiply<TAngleUnits, T>(T left, in Vector<TAngleUnits, T> right)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(left * right.Radius, left * right.Azimuth, left * right.Polar);

    /// <summary>
    /// Divides each coordinate of the input Vector by a scalar value and returns the result as a new Vector.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="left">The Vector to divide.</param>
    /// <param name="right">The scalar value to divide each coordinate of the input vector by.</param>
    /// <returns>A new Vector that is the result of dividing each coordinate of the input vector by the scalar value.</returns>
    /// <remarks>
    /// This method divides each coordinate of the input Vector by the specified scalar value and returns a new Vector with the resulting quotients. 
    /// The operation is performed independently on each coordinate, meaning that each coordinate of the input vector is divided by the scalar value separately. 
    /// The input vector remains unchanged after the operation.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<TAngleUnits, T> Divide<TAngleUnits, T>(in Vector<TAngleUnits, T> left, T right)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(left.Radius / right, left.Azimuth / right, left.Polar / right);

    #endregion

    /// <summary>
    /// Returns a new vector that is clamped within the specified minimum and maximum values for each component.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
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
    public static Vector<TAngleUnits, T> Clamp<TAngleUnits, T>(in Vector<TAngleUnits, T> vector, in Vector<TAngleUnits, T> min, in Vector<TAngleUnits, T> max)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.Clamp(vector.Radius, min.Radius, max.Radius), Angle.Clamp(vector.Azimuth, min.Azimuth, max.Azimuth), Angle.Clamp(vector.Polar, min.Polar, max.Polar));

    /// <summary>
    /// Performs linear interpolation between two vectors.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
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
    public static Vector<TAngleUnits, T> Lerp<TAngleUnits, T>(in Vector<TAngleUnits, T> start, in Vector<TAngleUnits, T> end, T factor)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => (start * (T.One - factor)) + (end * factor);

    /// <summary>
    /// Calculates the magnitude (length) of the vector.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <returns>The magnitude of the vector.</returns>
    /// <remarks>
    /// <para>
    /// The magnitude is calculated as the Euclidean distance in the 2D Rectangular coordinate system.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Magnitude<TAngleUnits, T>(in Vector<TAngleUnits, T> vector)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => vector.Radius;

    /// <summary>
    /// Returns a new vector that represents the normalized form of the specified vector.
    /// </summary>
    /// <typeparam name="TAngleUnits">The angle units used for the azimuth and polar coordinates.</typeparam>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
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
    public static Vector<TAngleUnits, T> Normalize<TAngleUnits, T>(in Vector<TAngleUnits, T> vector)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        var length = Magnitude(in vector);
        return length != T.Zero
            ? Divide(in vector, length)
            : Vector<TAngleUnits, T>.Zero;
    }

    /// <summary>
    /// Calculates the dot product.
    /// </summary>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="left">A vector.</param>
    /// <param name="right">A vector.</param>
    /// <returns>The dot product.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Dot<T>(in Vector<Radians, T> left, in Vector<Radians, T> right)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
        => Magnitude(in left) * Magnitude(in right) * Angle.Cos(left.Azimuth - right.Azimuth);

    /// <summary>
    /// Gets the smallest angle between two vectors.
    /// </summary>
    /// <typeparam name="T">The type of the radius coordinate.</typeparam>
    /// <param name="from">The vector where the angle measurement starts at.</param>
    /// <param name="to">The vector where the angle measurement stops at.</param>
    /// <returns>The angle between two vectors.</returns>
    /// <remarks>The angle is always less than 180 degrees.</remarks>
    public static AngleReduced<Radians, T> AngleBetween<T>(in Vector<Radians, T> from, in Vector<Radians, T> to)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
        => Angle.Acos(Dot(in from, in to) / (Magnitude(in from) * Magnitude(in to)));
}