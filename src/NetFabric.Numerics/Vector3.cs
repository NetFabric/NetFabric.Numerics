using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Intrinsics;

namespace NetFabric.Numerics;

/// <summary>
/// Represents a vector as an immutable struct.
/// </summary>  
/// <typeparam name="T">The type of the vector coordinates.</typeparam>
/// <param name="X">The X coordinate.</param>
/// <param name="Y">The X coordinate.</param>
/// <param name="Z">The X coordinate.</param>
[System.Diagnostics.DebuggerDisplay("X = {X}, Y = {Y}, Z = {Z}")]
[SkipLocalsInit]
public readonly struct Vector3<T>
    : IVector<Vector3<T>, T>
    where T : struct, INumber<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Gets the X coordinate. This field is read-only.
    /// </summary>
    public readonly T X;

    /// <summary>
    /// Gets the Y coordinate. This field is read-only.
    /// </summary>
    public readonly T Y;

    /// <summary>
    /// Gets the Z coordinate. This field is read-only.
    /// </summary>
    public readonly T Z;

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector3{T}"/> struct.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="z">The Z coordinate.</param>
    public Vector3(T x, T y, T z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="vector"/>.</typeparam>
    /// <param name="vector">The value which is used to create the instance of <see cref="Vector3{T}"/></param>
    /// <returns>An instance of <see cref="Vector3{T}"/> created from <paramref name="vector" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="vector" /> is not representable by <see cref="Vector3{T}"/>.</exception>
    public static Vector3<T> CreateChecked<TOther>(in Vector3<TOther> vector)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>
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
    /// <param name="vector">The value which is used to create the instance of <see cref="Vector3{T}"/></param>
    /// <returns>An instance of <see cref="Vector3{T}"/> created from <paramref name="vector" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="vector" /> is not representable by <see cref="Vector3{T}"/>.</exception>
    public static Vector3<T> CreateSaturating<TOther>(in Vector3<TOther> vector)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>
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
    /// <param name="vector">The value which is used to create the instance of <see cref="Vector3{T}"/></param>
    /// <returns>An instance of <see cref="Vector3{T}"/> created from <paramref name="vector" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="vector" /> is not representable by <see cref="Vector3{T}"/>.</exception>
    public static Vector3<T> CreateTruncating<TOther>(in Vector3<TOther> vector)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateTruncating(vector.X),
            T.CreateTruncating(vector.Y),
            T.CreateTruncating(vector.Z)
        );

    #region constants

    public const int Count = 3;

    int IVector<Vector3<T>, T>.Count
        => Count;

    /// <summary>
    /// Represents a vector whose coordinates are equal to zero. This field is read-only.
    /// </summary>
    public static readonly Vector3<T> Zero = new(T.Zero, T.Zero, T.Zero);

    static Vector3<T> INumericBase<Vector3<T>>.Zero
        => Zero;

    static Vector3<T> IAdditiveIdentity<Vector3<T>, Vector3<T>>.AdditiveIdentity
        => Zero;

    /// <summary>
    /// Represents a vector whose X coordinate is one and others are zero. This field is read-only.
    /// </summary>
    public static readonly Vector3<T> UnitX = new(T.One, T.Zero, T.Zero);

    /// <summary>
    /// Represents a vector whose Y coordinate is one and others are zero. This field is read-only.
    /// </summary>
    public static readonly Vector3<T> UnitY = new(T.Zero, T.One, T.Zero);

    /// <summary>
    /// Represents a vector whose Z coordinate is one and others are zero. This field is read-only.
    /// </summary>
    public static readonly Vector3<T> UnitZ = new(T.Zero, T.Zero, T.One);

    /// <summary>
    /// Represents a vector whose Z coordinate is one and others are zero. This field is read-only.
    /// </summary>
    public static readonly Vector3<T> UnitW = new(T.Zero, T.Zero, T.Zero);

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Vector3<T> MinValue = new(T.MinValue, T.MinValue, T.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Vector3<T> MaxValue = new(T.MaxValue, T.MaxValue, T.MaxValue);

    static Vector3<T> IMinMaxValue<Vector3<T>>.MinValue
        => MinValue;
    static Vector3<T> IMinMaxValue<Vector3<T>>.MaxValue
        => MaxValue;

    #endregion

    #region equality

    /// <summary>
    /// Indicates whether two <see cref="Vector3{T}"/> instances are equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>true if the two vectors are equal, false otherwise.</returns>
    /// <remarks>
    /// The method compares the numerical values of the <paramref name="left"/> and <paramref name="right"/> vectors to determine their equality.
    /// </remarks>
    public static bool operator ==(Vector3<T> left, Vector3<T> right)
        => left.Equals(right);

    /// <summary>
    /// Indicates whether two <see cref="Vector3{T}"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>true if the two vectors are equal, false otherwise.returns>
    /// <remarks>
    /// The method compares the numerical values of the <paramref name="left"/> and <paramref name="right"/> vectors to determine their equality.
    /// </remarks>
    public static bool operator !=(Vector3<T> left, Vector3<T> right)
        => !left.Equals(right);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        => HashCode.Combine(X, Y, Z);

    /// <summary>
    /// Determines whether the current vector is equal to another vector.
    /// </summary>
    /// <param name="other">The vector to compare with the current vector.</param>
    /// <returns><c>true</c> if the current vector is equal to the other vector; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Vector3<T> other)
    {
        if (typeof(T) == typeof(float))
        {
            return Unsafe.As<Vector3<T>, System.Numerics.Vector3>(ref Unsafe.AsRef(in this))
                .Equals(Unsafe.As<Vector3<T>, System.Numerics.Vector3>(ref Unsafe.AsRef(in other)));
        }
        
        if (Vector.IsHardwareAccelerated && Vector<T>.Count == Count && Vector<T>.IsSupported)
        {
            return Unsafe.As<Vector3<T>, Vector<T>>(ref Unsafe.AsRef(in this))
                .Equals(Unsafe.As<Vector3<T>, Vector<T>>(ref Unsafe.AsRef(in other)));
        }

        return EqualityComparer<T>.Default.Equals(X, other.X) &&
            EqualityComparer<T>.Default.Equals(Y, other.Y) &&
            EqualityComparer<T>.Default.Equals(Z, other.Z);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj switch
        {
            Vector3<T> vector => Equals(vector),
            _ => false
        };

    #endregion

    #region comparison

    /// <summary>
    /// Determines whether the magnitude of the left <see cref="Vector3{T}"/> is less than the magnitude of the right <see cref="Vector3{T}"/>.
    /// </summary>
    /// <param name="left">The left <see cref="Vector3{T}"/> to compare.</param>
    /// <param name="right">The right <see cref="Vector3{T}"/> to compare.</param>
    /// <returns>True if the magnitude of the left <see cref="Vector3{T}"/> is less than the magnitude of the right <see cref="Vector3{T}"/>; otherwise, false.</returns>
    /// <remarks>
    /// <para>
    /// This operator compares the magnitude of the left <see cref="Vector3{T}"/> with the magnitude of the right <see cref="Vector3{T}"/>. 
    /// It returns true if and only if the magnitude of the left <see cref="Vector3{T}"/> is less than the magnitude of the right <see cref="Vector3{T}"/>. 
    /// Otherwise, it returns false. The comparison is based on the magnitudes of the vectors and is independent of their directions.
    /// </para>
    /// <para>
    /// The comparison of magnitudes is performed using the <see cref="Vector3.Magnitude{T}(in Vector3{T})"/> method, which calculates the square root of the magnitude squared. 
    /// However, to optimize the performance, this operator directly compares the magnitude squared values of the vectors, accessible through the <see cref="Vector3.MagnitudeSquared{T}(in Vector3{T})"/> method.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Vector3<T> left, Vector3<T> right)
        => Vector3.Compare(in left, in right) < 0;

    /// <summary>
    /// Determines whether the magnitude of the left <see cref="Vector3{T}"/> is less than or equal to the magnitude of the right <see cref="Vector3{T}"/>.
    /// </summary>
    /// <param name="left">The left <see cref="Vector3{T}"/> to compare.</param>
    /// <param name="right">The right <see cref="Vector3{T}"/> to compare.</param>
    /// <returns>True if the magnitude of the left <see cref="Vector3{T}"/> is less than or equal to the magnitude of the right <see cref="Vector3{T}"/>; otherwise, false.</returns>
    /// <remarks>
    /// <para>
    /// This operator compares the magnitude of the left <see cref="Vector3{T}"/> with the magnitude of the right <see cref="Vector3{T}"/>. 
    /// It returns true if and only if the magnitude of the left <see cref="Vector3{T}"/> is less than or equal to the magnitude of the right <see cref="Vector3{T}"/>. 
    /// Otherwise, it returns false. The comparison is based on the magnitudes of the vectors and is independent of their directions.
    /// </para>
    /// <para>
    /// The comparison of magnitudes is performed using the <see cref="Vector3.Magnitude{T}(in Vector3{T})"/> method, which calculates the square root of the magnitude squared. 
    /// However, to optimize the performance, this operator directly compares the magnitude squared values of the vectors, accessible through the <see cref="Vector3.MagnitudeSquared{T}(in Vector3{T})"/> method.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Vector3<T> left, Vector3<T> right)
        => Vector3.Compare(in left, in right) <= 0;

    /// <summary>
    /// Determines whether the magnitude of the left <see cref="Vector3{T}"/> is greater than the magnitude of the right <see cref="Vector3{T}"/>.
    /// </summary>
    /// <param name="left">The left <see cref="Vector3{T}"/> to compare.</param>
    /// <param name="right">The right <see cref="Vector3{T}"/> to compare.</param>
    /// <returns>True if the magnitude of the left <see cref="Vector3{T}"/> is greater than the magnitude of the right <see cref="Vector3{T}"/>; otherwise, false.</returns>
    /// <remarks>
    /// <para>
    /// This operator compares the magnitude of the left <see cref="Vector3{T}"/> with the magnitude of the right <see cref="Vector3{T}"/>. 
    /// It returns true if and only if the magnitude of the left <see cref="Vector3{T}"/> is greater than the magnitude of the right <see cref="Vector3{T}"/>. 
    /// Otherwise, it returns false. The comparison is based on the magnitudes of the vectors and is independent of their directions.
    /// </para>
    /// <para>
    /// The comparison of magnitudes is performed using the <see cref="Vector3.Magnitude{T}(in Vector3{T})"/> method, which calculates the square root of the magnitude squared. 
    /// However, to optimize the performance, this operator directly compares the magnitude squared values of the vectors, accessible through the <see cref="Vector3.MagnitudeSquared{T}(in Vector3{T})"/> method.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Vector3<T> left, Vector3<T> right)
        => Vector3.Compare(in left, in right) > 0;

    /// <summary>
    /// Determines whether the magnitude of the left <see cref="Vector3{T}"/> is greater than or equal to the magnitude of the right <see cref="Vector3{T}"/>.
    /// </summary>
    /// <param name="left">The left <see cref="Vector3{T}"/> to compare.</param>
    /// <param name="right">The right <see cref="Vector3{T}"/> to compare.</param>
    /// <returns>True if the magnitude of the left <see cref="Vector3{T}"/> is greater than or equal to the magnitude of the right <see cref="Vector3{T}"/>; otherwise, false.</returns>
    /// <remarks>
    /// <para>
    /// This operator compares the magnitude of the left <see cref="Vector3{T}"/> with the magnitude of the right <see cref="Vector3{T}"/>. 
    /// It returns true if and only if the magnitude of the left <see cref="Vector3{T}"/> is greater than or equal to the magnitude of the right <see cref="Vector3{T}"/>. 
    /// Otherwise, it returns false. The comparison is based on the magnitudes of the vectors and is independent of their directions.
    /// </para>
    /// <para>
    /// The comparison of magnitudes is performed using the <see cref="Vector3.Magnitude{T}(in Vector3{T})"/> method, which calculates the square root of the magnitude squared. 
    /// However, to optimize the performance, this operator directly compares the magnitude squared values of the vectors, accessible through the <see cref="Vector3.MagnitudeSquared{T}(in Vector3{T})"/> method.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Vector3<T> left, Vector3<T> right)
        => Vector3.Compare(in left, in right) >= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    int IComparable<Vector3<T>>.CompareTo(Vector3<T> other)
        => Vector3.MagnitudeSquared(this).CompareTo(Vector3.MagnitudeSquared(other));

    readonly int IComparable.CompareTo(object? obj)
        => obj switch
        {
            null => 1,
            Vector3<T> other => Vector3.Compare(in this, in other),
            _ => Throw.ArgumentException<int>($"Argument must be of type {nameof(Vector3<T>)}.", nameof(obj)),
        };

    #endregion

    #region operators

    /// <summary>
    /// Returns the input <see cref="Vector3{T}"/> unchanged.
    /// </summary>
    /// <param name="right">The input <see cref="Vector3{T}"/>.</param>
    /// <returns>The input <see cref="Vector3{T}"/> unchanged.</returns>
    /// <remarks>
    /// <para>
    /// This operator returns the input <see cref="Vector3{T}"/> unchanged. 
    /// It effectively represents the identity operation, where the same vector is returned without any modification.
    /// </para>
    /// <para>
    /// This operator is useful in scenarios where a unary plus sign is needed to explicitly indicate that the vector remains the same.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> operator +(Vector3<T> right)
        => right;

    /// <summary>
    /// Adds two <see cref="Vector3{T}"/> instances component-wise.
    /// </summary>
    /// <param name="left">The first <see cref="Vector3{T}"/> to add.</param>
    /// <param name="right">The second <see cref="Vector3{T}"/> to add.</param>
    /// <returns>A new <see cref="Vector3{T}"/> that is the result of adding the corresponding components of the input vectors.</returns>
    /// <remarks>
    /// This operator performs component-wise addition of the input vectors, where each component of the resulting vector is the sum of the corresponding components of the input vectors.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> operator +(Vector3<T> left, Vector3<T> right)
        => Vector3.Add(in left, in right);

    /// <summary>
    /// Negates the specified <see cref="Vector3{T}"/> by reversing the sign of each component.
    /// </summary>
    /// <param name="right">The <see cref="Vector3{T}"/> to negate.</param>
    /// <returns>A new <see cref="Vector3{T}"/> with the negated values.</returns>
    /// <remarks>
    /// This operator reverses the sign of each component of the input vector, resulting in a new vector with the opposite direction.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> operator -(Vector3<T> right)
        => new(-right.X, -right.Y, -right.Z);

    /// <summary>
    /// Subtracts the components of the second <see cref="Vector3{T}"/> from the corresponding components of the first <see cref="Vector3{T}"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Vector3{T}"/>.</param>
    /// <param name="right">The second <see cref="Vector3{T}"/>.</param>
    /// <returns>A new <see cref="Vector3{T}"/> representing the element-wise subtraction of the two input vectors.</returns>
    /// <remarks>
    /// This operator subtracts the corresponding components of the second vector from the components of the first vector,
    /// resulting in a new vector with the subtracted values.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> operator -(Vector3<T> left, Vector3<T> right)
        => Vector3.Subtract(in left, in right);

    /// <summary>
    /// Multiplies each component of the <see cref="Vector3{T}"/> by the specified scalar value.
    /// </summary>
    /// <param name="left">The <see cref="Vector3{T}"/> to multiply.</param>
    /// <param name="right">The scalar value to multiply by.</param>
    /// <returns>A new <see cref="Vector3{T}"/> with each component multiplied by the scalar value.</returns>
    /// <remarks>
    /// This operator multiplies each component of the vector by the specified scalar value,
    /// resulting in a new vector with the scaled values.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> operator *(Vector3<T> left, T right)
        => Vector3.Multiply(right, in left);

    /// <summary>
    /// Multiplies the specified scalar value by each component of the <see cref="Vector3{T}"/>.
    /// </summary>
    /// <param name="left">The scalar value to multiply.</param>
    /// <param name="right">The <see cref="Vector3{T}"/> to multiply.</param>
    /// <returns>A new <see cref="Vector3{T}"/> with each component multiplied by the scalar value.</returns>
    /// <remarks>
    /// This operator multiplies the specified scalar value by each component of the vector,
    /// resulting in a new vector with the scaled values.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> operator *(T left, Vector3<T> right)
        => Vector3.Multiply(left, in right);

    /// <summary>
    /// Divides each component of the <see cref="Vector3{T}"/> by the specified scalar value.
    /// </summary>
    /// <param name="left">The <see cref="Vector3{T}"/> to divide.</param>
    /// <param name="right">The scalar value to divide by.</param>
    /// <returns>A new <see cref="Vector3{T}"/> with each component divided by the scalar value.</returns>
    /// <remarks>
    /// This operator divides each component of the vector by the specified scalar value,
    /// resulting in a new vector with the scaled values.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> operator /(Vector3<T> left, T right)
        => Vector3.Divide(in left, right);

    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static T IVector<Vector3<T>, T>.MagnitudeSquared(in Vector3<T> vector)
        => Vector3.MagnitudeSquared(in vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static T IVector<Vector3<T>, T>.Dot(in Vector3<T> left, in Vector3<T> right)
        => Vector3.Dot(in left, in right);

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
    public T this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (uint)index >= Count
             ? Throw.ArgumentOutOfRangeException<T>(nameof(index), index)
             : Unsafe.Add(ref Unsafe.AsRef(in X), index);
    }

    /// <summary>
    /// Deconstructs the vector into its individual components.
    /// </summary>
    /// <param name="X">The output parameter to store the X component of the vector.</param>
    /// <param name="Y">The output parameter to store the Y component of the vector.</param>
    /// <param name="Z">The output parameter to store the Z component of the vector.</param>
    public void Deconstruct(out T X, out T Y, out T Z)
    {
        X = this.X;
        Y = this.Y;
        Z = this.Z;
    }

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
public static class Vector3
{
    /// <summary>
    /// Determines whether the specified vector is a zero vector, where all components are zero.
    /// </summary>
    /// <typeparam name="T">The numeric type used internally by <paramref name="vector"/>.</typeparam>
    /// <param name="vector">The vector to check.</param>
    /// <returns><c>true</c> if all components of the vector are zero; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero<T>(in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => vector == Vector3<T>.Zero;

    /// <summary>
    /// Determines whether the specified vector is a zero vector, where all components are zero.
    /// </summary>
    /// <typeparam name="T">The numeric type used internally by <paramref name="vector"/>.</typeparam>
    /// <param name="vector">The vector to check.</param>
    /// <param name="tolerance">The tolerance value.</param>
    /// <returns><c>true</c> if all components of the vector are zero; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// The <see cref="IsZero{T}(in Vector3{T}, T)"/> method checks whether all components of the vector are equal to zero within the <paramref name="tolerance"/> range.
    /// The tolerance is a small value used to account for floating-point precision errors.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero<T>(in Vector3<T> vector, T tolerance)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => AreApproximatelyEqual(vector, Vector3<T>.Zero, tolerance);

    /// <summary>
    /// Determines whether any component of the specified <see cref="Vector3{T}"/> is NaN (Not-a-Number).
    /// </summary>
    /// <typeparam name="T">The type of the vector's components.</typeparam>
    /// <param name="vector">The <see cref="Vector3{T}"/> to check for NaN values.</param>
    /// <returns>
    /// <c>true</c> if any component of the vector is NaN; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNaN<T>(in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => T.IsNaN(vector.X) || T.IsNaN(vector.Y) || T.IsNaN(vector.Z);

    /// <summary>
    /// Determines whether any component of the specified <see cref="Vector3{T}"/> is positive or negative infinity.
    /// </summary>
    /// <typeparam name="T">The type of the vector's components.</typeparam>
    /// <param name="vector">The <see cref="Vector3{T}"/> to check for infinity values.</param>
    /// <returns>
    /// <c>true</c> if any component of the vector is positive or negative infinity; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInfinity<T>(in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => T.IsInfinity(vector.X) || T.IsInfinity(vector.Y) || T.IsInfinity(vector.Z);

    /// <summary>
    /// Determines whether all components of the specified <see cref="Vector3{T}"/> are finite numbers (not NaN, infinity, or negative infinity).
    /// </summary>
    /// <typeparam name="T">The type of the vector's components.</typeparam>
    /// <param name="vector">The <see cref="Vector3{T}"/> to check for finite values.</param>
    /// <returns>
    /// <c>true</c> if all components of the vector are finite numbers; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite<T>(in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => T.IsFinite(vector.X) && T.IsFinite(vector.Y) && T.IsFinite(vector.Z);

    /// <summary>
    /// Determines whether the specified <see cref="Vector3{T}"/> is a normalized vector.
    /// </summary>
    /// <typeparam name="T">The type of the vector's components.</typeparam>
    /// <param name="vector">The <see cref="Vector3{T}"/> to check for normalization.</param>
    /// <returns>
    /// <c>true</c> if the vector is normalized (its magnitude is 1); otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNormalized<T>(in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Vector3.MagnitudeSquared(vector) == T.One;

    /// <summary>
    /// Determines whether the specified <see cref="Vector3{T}"/> is a normalized vector within the specified tolerance.
    /// </summary>
    /// <typeparam name="T">The type of the vector's components.</typeparam>
    /// <param name="vector">The <see cref="Vector3{T}"/> to check for normalization.</param>
    /// <param name="tolerance">The tolerance used for the comparison.</param>
    /// <returns>
    /// <c>true</c> if the vector is normalized (its magnitude is within the specified tolerance of 1); otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNormalized<T>(in Vector3<T> vector, T tolerance)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.AreApproximatelyEqual(Vector3.MagnitudeSquared(vector), T.One, tolerance);

    /// <summary>
    /// Checks if two floating-point values are approximately equal within the specified tolerance.
    /// </summary>
    /// <typeparam name="T">The numeric type used internally by <paramref name="vector"/>.</typeparam>
    /// <param name="a">The first value to compare.</param>
    /// <param name="b">The second value to compare.</param>
    /// <param name="tolerance">The tolerance value.</param>
    /// <returns><c>true</c> if the values are approximately equal; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This method compares the absolute difference between the two values to the specified tolerance value.
    /// If the absolute difference is less than or equal to the tolerance, the values are considered approximately equal.
    /// </remarks>    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AreApproximatelyEqual<T>(in Vector3<T> a, in Vector3<T> b, T tolerance)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.AreApproximatelyEqual(a.X, b.X, tolerance) &&
            Utils.AreApproximatelyEqual(a.Y, b.Y, tolerance) &&
            Utils.AreApproximatelyEqual(a.Z, b.Z, tolerance);

    /// <summary>
    /// Compares two Vector3 instances and returns an indication of their relative values.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <param name="vector">The first Vector3 to compare.</param>
    /// <param name="other">The second Vector3 to compare.</param>
    /// <returns>
    /// A value that indicates the relative order of the Vector3 instances being compared.
    /// The return value has the following meanings:
    /// - Less than zero: <paramref name="vector"/> is less than <paramref name="other"/>.
    /// - Zero: <paramref name="vector"/> is equal to <paramref name="other"/>.
    /// - Greater than zero: <paramref name="vector"/> is greater than <paramref name="other"/>.
    /// </returns>
    /// <remarks>
    /// This method compares the squared magnitudes of the two Vector3 instances.
    /// The squared magnitude is used to avoid the costly square root operation and is sufficient
    /// for comparing the relative values of vectors. 
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Compare<T>(in Vector3<T> vector, in Vector3<T> other)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => MagnitudeSquared(vector).CompareTo(MagnitudeSquared(other));

    #region arithmetic

    /// <summary>
    /// Negates the specified Vector3 by reversing the sign of each of its coordinates.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <param name="right">The Vector3 to negate.</param>
    /// <returns>A new Vector3 with the negated coordinates.</returns>
    /// <remarks>
    /// This method creates a new Vector3 instance with the same magnitude as the input vector
    /// but with reversed signs for each coordinate. The resulting vector points in the opposite
    /// direction as the input vector. The input vector remains unchanged.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> Negate<T>(in Vector3<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>, ISignedNumber<T>
        => new(-right.X, -right.Y, -right.Z);

    /// <summary>
    /// Adds two vectors component-wise and returns the result as a new Vector3.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <param name="left">The first Vector3 to add.</param>
    /// <param name="right">The second Vector3 to add.</param>
    /// <returns>A new Vector3 that is the component-wise sum of the input vectors.</returns>
    /// <remarks>
    /// This method adds the corresponding coordinates of the two input vectors and returns
    /// a new Vector3 with the resulting sums. The operation is performed component-wise, which
    /// means that the X, Y, Z, and W coordinates of the resulting vector are the sums of the
    /// X, Y, Z, and W coordinates of the input vectors, respectively. The input vectors remain
    /// unchanged.
    /// </remarks>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> Add<T>(in Vector3<T> left, in Vector3<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (typeof(T) == typeof(uint) || typeof(T) == typeof(int) || typeof(T) == typeof(float))
        {
            var v1 = Unsafe.As<Vector3<T>, System.Numerics.Vector3>(ref Unsafe.AsRef(in left));
            var v2 = Unsafe.As<Vector3<T>, System.Numerics.Vector3>(ref Unsafe.AsRef(in right));
            return Unsafe.As<System.Numerics.Vector3, Vector3<T>>(ref Unsafe.AsRef(v1 + v2));
        }
        
        if (Vector.IsHardwareAccelerated && Vector<T>.Count >= Vector3<T>.Count && Vector<T>.IsSupported)
        {
            var v1 = Unsafe.As<Vector3<T>, Vector<T>>(ref Unsafe.AsRef(in left));
            var v2 = Unsafe.As<Vector3<T>, Vector<T>>(ref Unsafe.AsRef(in right));
            return Unsafe.As<Vector<T>, Vector3<T>>(ref Unsafe.AsRef(v1 + v2));
        }

        return new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }

    /// <summary>
    /// Subtracts the second vector from the first vector component-wise and returns the result as a new Vector3.
    /// </summary>
    /// <typeparam name="T">The type of the vector coordinates.</typeparam>
    /// <param name="left">The Vector3 to subtract from (the minuend).</param>
    /// <param name="right">The Vector3 to subtract (the subtrahend).</param>
    /// <returns>A new Vector3 that is the component-wise difference of the input vectors.</returns>
    /// <remarks>
    /// This method subtracts the corresponding coordinates of the second vector from the first vector
    /// and returns a new Vector3 with the resulting differences. The operation is performed component-wise,
    /// which means that the X, Y, Z, and W coordinates of the resulting vector are the differences of the
    /// X, Y, Z, and W coordinates of the input vectors, respectively. The input vectors remain unchanged.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> Subtract<T>(in Vector3<T> left, in Vector3<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    /// <summary>
    /// Multiplies a scalar value with each coordinate of the input Vector3 and returns the result as a new Vector3.
    /// </summary>
    /// <typeparam name="T">The type of the scalar and vector coordinates.</typeparam>
    /// <param name="left">The scalar value to multiply with each coordinate of the input vector.</param>
    /// <param name="right">The Vector3 to multiply.</param>
    /// <returns>A new Vector3 that is the result of multiplying each coordinate of the input vector by the scalar value.</returns>
    /// <remarks>
    /// This method multiplies the specified scalar value with each coordinate of the input Vector3 and returns a new Vector3 with the resulting products. 
    /// The operation is performed independently on each coordinate, meaning that the scalar value is multiplied with the X, Y, Z, and W coordinates of the input vector separately. 
    /// The input vector remains unchanged after the operation.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> Multiply<T>(T left, in Vector3<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(left * right.X, left * right.Y, left * right.Z);

    /// <summary>
    /// Divides each coordinate of the input Vector3 by a scalar value and returns the result as a new Vector3.
    /// </summary>
    /// <typeparam name="T">The type of the scalar and vector coordinates.</typeparam>
    /// <param name="left">The Vector3 to divide.</param>
    /// <param name="right">The scalar value to divide each coordinate of the input vector by.</param>
    /// <returns>A new Vector3 that is the result of dividing each coordinate of the input vector by the scalar value.</returns>
    /// <remarks>
    /// This method divides each coordinate of the input Vector3 by the specified scalar value and returns a new Vector3 with the resulting quotients. 
    /// The operation is performed independently on each coordinate, meaning that each coordinate of the input vector is divided by the scalar value separately. 
    /// The input vector remains unchanged after the operation.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> Divide<T>(in Vector3<T> left, T right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(left.X / right, left.Y / right, left.Z / right);

    #endregion

    /// <summary>
    /// Returns a new vector that is clamped within the specified minimum and maximum values for each component.
    /// </summary>
    /// <typeparam name="T">The numeric type used internally by <paramref name="vector"/>.</typeparam>
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
    public static Vector3<T> Clamp<T>(in Vector3<T> vector, in Vector3<T> min, in Vector3<T> max)
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
    public static Vector3<T> Lerp<T>(in Vector3<T> start, in Vector3<T> end, T factor)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => (start * (T.One - factor)) + (end * factor);

    /// <summary>
    /// Calculates the magnitude (length) of the vector.
    /// </summary>
    /// <typeparam name="T">The numeric type used internally by <paramref name="vector"/>.</typeparam>
    /// <returns>The magnitude of the vector.</returns>
    /// <remarks>
    /// <para>
    /// The magnitude is calculated as the Euclidean distance in the 2D Cartesian coordinate system.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Magnitude<T>(in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>, IRootFunctions<T>
        => T.Sqrt(MagnitudeSquared(in vector));

    /// <summary>
    /// Calculates the magnitude (length) of the vector.
    /// </summary>
    /// <typeparam name="T">The numeric type used internally by <paramref name="vector"/>.</typeparam>
    /// <typeparam name="TOut">The numeric type used for the magnitude.</typeparam>
    /// <returns>The magnitude of the vector.</returns>
    /// <remarks>
    /// <para>
    /// The magnitude is calculated as the Euclidean distance in the 2D Cartesian coordinate system.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Magnitude<T, TOut>(in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        where TOut : struct, INumber<TOut>, IRootFunctions<TOut>
        => TOut.Sqrt(TOut.CreateChecked(MagnitudeSquared(in vector)));

    /// <summary>
    /// Calculates the square of the magnitude (length) of the vector.
    /// </summary>
    /// <typeparam name="T">The numeric type used internally by <paramref name="vector"/>.</typeparam>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T MagnitudeSquared<T>(in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Utils.Pow2(vector.X) + Utils.Pow2(vector.Y) + Utils.Pow2(vector.Z);

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
    public static Vector3<T> Normalize<T>(in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>, IRootFunctions<T>
    {
        var length = T.CreateChecked(Magnitude(vector));
        return length != T.Zero
            ? Divide(in vector, length)
            : Vector3<T>.Zero;
    }

    /// <summary>
    /// Calculates the dot product.
    /// </summary>
    /// <param name="left">A vector.</param>
    /// <param name="right">A vector.</param>
    /// <returns>The dot product.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Dot<T>(in Vector3<T> left, in Vector3<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);

    /// <summary>
    /// Calculates the cross product.
    /// </summary>
    /// <param name="left">A vector.</param>
    /// <param name="right">A vector.</param>
    /// <returns>The cross products.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3<T> Cross<T>(in Vector3<T> left, in Vector3<T> right)
            where T : struct, INumber<T>, IMinMaxValue<T>
            => new((left.Y * right.Z) - (left.Z * right.Y),
                    (left.Z * right.X) - (left.X * right.Z),
                    (left.X * right.Y) - (left.Y * right.X));

    /// <summary>
    /// Gets the smallest angle between two vectors.
    /// </summary>
    /// <typeparam name="T">The numeric type used internally by <paramref name="from"/> and <paramref name="to"/>.</typeparam>
    /// <typeparam name="TAngle">The floating point type used internally by the returned angle.</typeparam>
    /// <param name="from">The vector where the angle measurement starts at.</param>
    /// <param name="to">The vector where the angle measurement stops at.</param>
    /// <returns>The angle between two vectors.</returns>
    /// <remarks>The angle is always less than 180 degrees.</remarks>
    public static AngleReduced<Radians, TAngle> AngleBetween<T, TAngle>(in Vector3<T> from, in Vector3<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>, ITrigonometricFunctions<TAngle>, IRootFunctions<TAngle>
        => Angle.Acos(TAngle.CreateChecked(Dot(in from, in to)) / (Magnitude<T, TAngle>(in from) * Magnitude<T, TAngle>(in to)));
}