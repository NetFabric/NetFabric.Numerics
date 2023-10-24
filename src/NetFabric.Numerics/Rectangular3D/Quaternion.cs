namespace NetFabric.Numerics.Rectangular3D;

/// <summary>
/// Represents a quaternion, a mathematical object used to represent rotations in 3D space.
/// The quaternion is represented by an immutable struct.
/// </summary>
/// <typeparam name="T">The type of the quaternion components.</typeparam>
/// <param name="X">The X component.</param>
/// <param name="Y">The Y component.</param>
/// <param name="Z">The Z component.</param>
/// <param name="W">The W component.</param>
[System.Diagnostics.DebuggerDisplay("X = {X}, Y = {Y}, Z = {Z}, W = {W}")]
[SkipLocalsInit]
public readonly record struct Quaternion<T>(T X, T Y, T Z, T W)
    : IEquatable<Quaternion<T>>,
      IEqualityOperators<Quaternion<T>, Quaternion<T>, bool>,
      IUnaryPlusOperators<Quaternion<T>, Quaternion<T>>,
      IAdditiveIdentity<Quaternion<T>, Quaternion<T>>,
      IUnaryNegationOperators<Quaternion<T>, Quaternion<T>>,
      ISubtractionOperators<Quaternion<T>, Quaternion<T>, Quaternion<T>>,
      IMultiplicativeIdentity<Quaternion<T>, Quaternion<T>>,
      IDivisionOperators<Quaternion<T>, Quaternion<T>, Quaternion<T>>,
      IMinMaxValue<Quaternion<T>>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
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

    /// <summary>
    /// Represents the minimum quaternion value. This field is read-only.
    /// </summary>
    public static readonly Quaternion<T> MinValue = new(T.MinValue, T.MinValue, T.MinValue, T.MinValue);

    /// <summary>
    /// Represents the maximum quaternion value. This field is read-only.
    /// </summary>
    public static readonly Quaternion<T> MaxValue = new(T.MaxValue, T.MaxValue, T.MaxValue, T.MaxValue);

    static Quaternion<T> IMinMaxValue<Quaternion<T>>.MinValue
        => MinValue;
    static Quaternion<T> IMinMaxValue<Quaternion<T>>.MaxValue
        => MaxValue;

    #endregion

    #region conversion

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="quaternion"/>.</typeparam>
    /// <param name="quaternion">The value which is used to create the instance of <see cref="Quaternion{T}"/></param>
    /// <returns>An instance of <see cref="Quaternion{T}"/> created from <paramref name="quaternion" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="quaternion" /> is not representable by <see cref="Quaternion{T}"/>.</exception>
    public static Quaternion<T> CreateChecked<TOther>(in Quaternion<TOther> quaternion)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateChecked(quaternion.X),
            T.CreateChecked(quaternion.Y),
            T.CreateChecked(quaternion.Z),
            T.CreateChecked(quaternion.W)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="quaternion"/>.</typeparam>
    /// <param name="quaternion">The value which is used to create the instance of <see cref="Quaternion{T}"/></param>
    /// <returns>An instance of <see cref="Quaternion{T}"/> created from <paramref name="quaternion" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="quaternion" /> is not representable by <see cref="Quaternion{T}"/>.</exception>
    public static Quaternion<T> CreateSaturating<TOther>(in Quaternion<TOther> quaternion)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateSaturating(quaternion.X),
            T.CreateSaturating(quaternion.Y),
            T.CreateSaturating(quaternion.Z),
            T.CreateSaturating(quaternion.W)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="quaternion"/>.</typeparam>
    /// <param name="quaternion">The value which is used to create the instance of <see cref="Quaternion{T}"/></param>
    /// <returns>An instance of <see cref="Quaternion{T}"/> created from <paramref name="quaternion" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="quaternion" /> is not representable by <see cref="Quaternion{T}"/>.</exception>
    public static Quaternion<T> CreateTruncating<TOther>(in Quaternion<TOther> quaternion)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateTruncating(quaternion.X),
            T.CreateTruncating(quaternion.Y),
            T.CreateTruncating(quaternion.Z),
            T.CreateTruncating(quaternion.W)
        );

    #endregion

    #region operators

    /// <summary>
    /// Returns the input quaternion unchanged.
    /// </summary>
    /// <param name="right">The input quaternion.</param>
    /// <returns>The input quaternion.</returns>
    /// <remarks>
    /// <para>
    /// The operator returns the input quaternion <paramref name="right"/> unchanged.
    /// This operator is implemented using aggressive inlining to optimize performance.
    /// </para>
    /// <para>
    /// The unary plus operator is primarily useful for consistency and readability in quaternion expressions
    /// where a positive sign is explicitly specified before a quaternion.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion<T> operator +(Quaternion<T> right)
        => right;

    /// <summary>
    /// Adds two quaternions together.
    /// </summary>
    /// <param name="left">The first quaternion.</param>
    /// <param name="right">The second quaternion.</param>
    /// <returns>The sum of the two quaternions.</returns>
    /// <remarks>
    /// <para>
    /// The operator adds the corresponding components of the quaternions <paramref name="left"/> and <paramref name="right"/>
    /// in an aggressively inlined manner, resulting in improved performance for quaternion arithmetic operations.
    /// </para>
    /// <para>
    /// This operator calculates the sum of the x, y, z, and w components of the quaternions,
    /// resulting in a new quaternion with the summed components.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion<T> operator +(Quaternion<T> left, Quaternion<T> right) 
        => new(
            left.X + right.X,
            left.Y + right.Y,
            left.Z + right.Z,
            left.W + right.W);

    /// <summary>
    /// Negates the specified quaternion.
    /// </summary>
    /// <param name="right">The quaternion to negate.</param>
    /// <returns>The negated quaternion.</returns>
    /// <remarks>
    /// <para>
    /// The operator negates each component of the specified quaternion <paramref name="right"/>
    /// in an aggressively inlined manner, resulting in improved performance for arithmetic operations involving quaternions.
    /// </para>
    /// <para>
    /// This operator calculates the negation of the x, y, z, and w components of the quaternion,
    /// resulting in a new quaternion with the negated components.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion<T> operator -(Quaternion<T> right) 
        => new(-right.X, -right.Y, -right.Z, -right.W);

    /// <summary>
    /// Subtracts the second quaternion from the first quaternion.
    /// </summary>
    /// <param name="left">The first quaternion.</param>
    /// <param name="right">The second quaternion.</param>
    /// <returns>The difference between the first and second quaternion.</returns>
    /// <remarks>
    /// <para>
    /// The operator subtracts each component of the second quaternion <paramref name="right"/>
    /// from the corresponding component of the first quaternion <paramref name="left"/> in an aggressively inlined manner,
    /// resulting in improved performance for arithmetic operations involving quaternions.
    /// </para>
    /// <para>
    /// This operator calculates the difference between the x, y, z, and w components of the quaternions,
    /// resulting in a new quaternion with the difference between their respective components.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion<T> operator -(Quaternion<T> left, Quaternion<T> right) 
        => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    /// <summary>
    /// Multiplies two quaternions together.
    /// </summary>
    /// <param name="left">The left quaternion operand.</param>
    /// <param name="right">The right quaternion operand.</param>
    /// <returns>The result of multiplying the two quaternions together.</returns>
    /// <remarks>
    /// <para>
    /// The operator multiplies the two input quaternions <paramref name="left"/> and <paramref name="right"/> together
    /// to produce a new quaternion that represents their combined rotation.
    /// </para>
    /// <para>
    /// The multiplication of quaternions follows the Hamilton product rule, which combines their rotation and scaling properties.
    /// </para>
    /// <para>
    /// This operator is implemented using aggressive inlining to optimize performance.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion<T> operator *(Quaternion<T> left, Quaternion<T> right) 
        => new(
            (left.W * right.X) + (left.X * right.W) + (left.Y * right.Z) - (left.Z * right.Y),
            (left.W * right.Y) - (left.X * right.Z) + (left.Y * right.W) + (left.Z * right.X),
            (left.W * right.Z) + (left.X * right.Y) - (left.Y * right.X) + (left.Z * right.W),
            (left.W * right.W) - (left.X * right.X) - (left.Y * right.Y) - (left.Z * right.Z));

    /// <summary>
    /// Multiplies a quaternion by a numeric value.
    /// </summary>
    /// <param name="left">The quaternion operand.</param>
    /// <param name="right">The numeric value to multiply the quaternion by.</param>
    /// <returns>The result of multiplying the quaternion by the numeric value.</returns>
    /// <remarks>
    /// <para>
    /// The operator multiplies a quaternion <paramref name="left"/> by a numeric value <paramref name="right"/>
    /// to produce a new quaternion where each component of the quaternion is multiplied by the scalar value.
    /// </para>
    /// <para>
    /// This operator is implemented using aggressive inlining to optimize performance.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion<T> operator *(Quaternion<T> left, T right) 
        => new(
            left.X * right,
            left.Y * right,
            left.Z * right,
            left.W * right);

    /// <summary>
    /// Divides a quaternion by another quaternion.
    /// </summary>
    /// <param name="left">The numerator quaternion.</param>
    /// <param name="right">The denominator quaternion.</param>
    /// <returns>The result of dividing the numerator quaternion by the denominator quaternion.</returns>
    /// <remarks>
    /// <para>
    /// The operator divides a quaternion <paramref name="left"/> by another quaternion <paramref name="right"/>
    /// to produce a new quaternion where each component of the numerator quaternion is divided by the corresponding component of the denominator quaternion.
    /// </para>
    /// <para>
    /// Note that quaternion division is not a commutative operation, meaning that the order of the quaternions matters.
    /// </para>
    /// </remarks>
    public static Quaternion<T> operator /(Quaternion<T> left, Quaternion<T> right) 
        => left * Quaternion.Inverse(in right);

    #endregion
}

/// <summary>
/// Provides static methods for quaternion operations.
/// </summary>
public static class Quaternion
{
    /// <summary>
    /// Determines whether a quaternion is an identity quaternion.
    /// </summary>
    /// <typeparam name="T">The type of the quaternion components.</typeparam>
    /// <param name="quaternion">The quaternion to check.</param>
    /// <returns>
    ///   <c>true</c> if the quaternion is an identity quaternion; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///   An identity quaternion is a quaternion with components (0, 0, 0, 1).
    ///   This method checks whether the given <paramref name="quaternion"/> has the components (0, 0, 0, 1)
    ///   to determine if it is an identity quaternion.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsIdentity<T>(in Quaternion<T> quaternion)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => quaternion == Quaternion<T>.Identity;

    /// <summary>
    /// Creates a quaternion representing a rotation around a specified axis by the given angle.
    /// </summary>
    /// <typeparam name="T">The numeric type of the quaternion's components.</typeparam>
    /// <param name="axis">The axis of rotation specified as a vector.</param>
    /// <param name="angle">The angle of rotation specified as an angle in radians.</param>
    /// <returns>A new quaternion representing the rotation around the specified axis by the given angle.</returns>
    /// <remarks>
    /// <para>
    /// The <see cref="FromAxisAngle{T}"/> method creates a new quaternion that represents a rotation around a specified axis
    /// by the given angle. The axis of rotation is specified as a quaternion in the <paramref name="axis"/> parameter,
    /// while the angle of rotation is specified as an angle in radians in the <paramref name="angle"/> parameter.
    /// </para>
    /// <para>
    /// The resulting quaternion represents the rotation in a 3D space and can be used to transform points or other quaternions.
    /// </para>
    /// <para>
    /// Note that the axis vector must be a unit vector, meaning its magnitude (length) must be equal to 1.
    /// If the axis vector is not normalized, unexpected results may occur.
    /// </para>
    /// </remarks>
    public static Quaternion<T> FromAxisAngle<T>(in Vector<T> axis, Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var halfAngle = angle / (T.One + T.One);
        var (sin, cos) = Angle.SinCos(halfAngle);
        return new(
            axis.X * sin,
            axis.Y * sin,
            axis.Z * sin,
            cos
        );
    }

    /// <summary>
    /// Creates a quaternion from the specified yaw, pitch and roll angles.
    /// </summary>
    /// <typeparam name="T">The type of the quaternion components.</typeparam>
    /// <param name="yaw">The yaw angle in radians.</param>
    /// <param name="pitch">The pitch angle in radians.</param>
    /// <param name="roll">The roll angle in radians.</param>
    /// <returns>The quaternion representing the specified Euler angles.</returns>
    public static Quaternion<T> FromYawPitchRoll<T>(Angle<Radians, T> yaw, Angle<Radians, T> pitch, Angle<Radians, T> roll)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var half = T.CreateChecked(0.5);
        var halfYaw = yaw.Value * half;
        var halfPitch = pitch.Value * half;
        var halfRoll = roll.Value * half;

        var sinHalfYaw = T.Sin(halfYaw);
        var cosHalfYaw = T.Cos(halfYaw);
        var sinHalfPitch = T.Sin(halfPitch);
        var cosHalfPitch = T.Cos(halfPitch);
        var sinHalfRoll = T.Sin(halfRoll);
        var cosHalfRoll = T.Cos(halfRoll);

        return new(
            (cosHalfYaw * sinHalfPitch * cosHalfRoll) + (sinHalfYaw * cosHalfPitch * sinHalfRoll),
            (sinHalfYaw * cosHalfPitch * cosHalfRoll) - (cosHalfYaw * sinHalfPitch * sinHalfRoll),
            (cosHalfYaw * cosHalfPitch * sinHalfRoll) - (sinHalfYaw * sinHalfPitch * cosHalfRoll),
            (cosHalfYaw * cosHalfPitch * cosHalfRoll) + (sinHalfYaw * sinHalfPitch * sinHalfRoll));
    }

    /// <summary>
    /// Normalizes the quaternion.
    /// </summary>
    /// <typeparam name="T">The numeric type of the quaternion's components.</typeparam>
    /// <param name="quaternion">The quaternion.</param>
    /// <returns>The normalized quaternion.</returns>
    /// <remarks>
    /// <para>
    /// The normalization of a quaternion involves dividing each component by the quaternion's norm to ensure its length becomes 1.
    /// </para>
    /// <para>
    /// This method modifies the original quaternion in place and returns the normalized quaternion.
    /// </para>
    /// <para>
    /// If the quaternion's norm is zero, the method returns the original quaternion without performing any normalization to avoid division by zero.
    /// </para>
    /// </remarks>
    public static Quaternion<T> Normalize<T>(in Quaternion<T> quaternion)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, IRootFunctions<T>
    {
        var norm = Norm(in quaternion);
        if (T.IsZero(norm))
            return quaternion;

        var reciprocalNorm = T.One / norm;
        return new(
            quaternion.X * reciprocalNorm,
            quaternion.Y * reciprocalNorm,
            quaternion.Z * reciprocalNorm,
            quaternion.W * reciprocalNorm);
    }

    /// <summary>
    /// Calculates the norm of the quaternion.
    /// </summary>
    /// <typeparam name="T">The numeric type of the quaternion's components.</typeparam>
    /// <param name="quaternion">The quaternion.</param>
    /// <returns>The norm of the quaternion.</returns>
    /// <remarks>
    /// <para>
    /// The norm of a quaternion is the square root of the sum of the squares of its components: √(x^2 + y^2 + z^2 + w^2).
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Norm<T>(in Quaternion<T> quaternion)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, IRootFunctions<T>
        => T.Sqrt(NormSquared(in quaternion));

    /// <summary>
    /// Calculates the squared norm of the quaternion.
    /// </summary>
    /// <typeparam name="T">The numeric type of the quaternion's components.</typeparam>
    /// <param name="quaternion">The quaternion.</param>
    /// <returns>The squared norm of the quaternion.</returns>
    /// <remarks>
    /// <para>
    /// The squared norm of a quaternion is the sum of the squares of its components: <c>x^2 + y^2 + z^2 + w^2</c>.
    /// </para>
    /// <para>
    /// This method is implemented using aggressive inlining to optimize performance.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T NormSquared<T>(in Quaternion<T> quaternion)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.Square(quaternion.X) + Utils.Square(quaternion.Y) + Utils.Square(quaternion.Z) + Utils.Square(quaternion.W);

    /// <summary>
    /// Returns the conjugate of the specified quaternion.
    /// </summary>
    /// <param name="quaternion">The quaternion to conjugate.</param>
    /// <returns>The conjugate of the quaternion.</returns>
    /// <remarks>
    /// The conjugate of a quaternion is obtained by negating the vector part (X, Y, Z) of the quaternion
    /// while keeping the scalar part (W) unchanged. The conjugate is useful in various quaternion operations,
    /// such as quaternion multiplication and division.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion<T> Conjugate<T>(in Quaternion<T> quaternion)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(-quaternion.X, -quaternion.Y, -quaternion.Z, quaternion.W);

    /// <summary>
    /// Calculates the inverse of the quaternion.
    /// </summary>
    /// <typeparam name="T">The type of the quaternion components.</typeparam>
    /// <param name="quaternion">The quaternion to calculate the inverse of.</param>
    /// <returns>The inverse of the quaternion.</returns>
    /// <remarks>
    /// <para>
    /// The inverse of a quaternion represents the opposite rotation. When a quaternion is multiplied by its inverse,
    /// the result is the identity quaternion (1, 0, 0, 0). The inverse of a non-zero quaternion is obtained by
    /// negating the vector part (x, y, z) and dividing each component by the squared magnitude.
    /// </para>
    /// <para>
    ///     Q^-1 = (x, y, z, w)^-1 = (-x / |Q|^2, -y / |Q|^2, -z / |Q|^2, w / |Q|^2)
    /// </para>
    /// <para>
    /// Note that the inverse of a quaternion is only defined when the quaternion is non-zero. In the case of a zero quaternion,
    /// attempting to calculate the inverse will result in division by zero, which is an undefined operation.
    /// </para>
    /// </remarks>
    public static Quaternion<T> Inverse<T>(in Quaternion<T> quaternion)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        var normSquared = NormSquared(in quaternion);
        if (T.IsZero(normSquared))
            Throw.InvalidOperationException<Quaternion<T>>("Cannot calculate the inverse of a zero quaternion.");

        var reciprocalNormSquared = T.One / normSquared;
        return new(
            -quaternion.X * reciprocalNormSquared,
            -quaternion.Y * reciprocalNormSquared,
            -quaternion.Z * reciprocalNormSquared,
            quaternion.W * reciprocalNormSquared);
    }

    // Slerp and Lerp based on: https://blog.magnum.graphics/backstage/the-unnecessarily-short-ways-to-do-a-quaternion-slerp/

    /// <summary>
    /// Calculates the dot product of two quaternions.
    /// </summary>
    /// <typeparam name="T">The type of the quaternion components.</typeparam>
    /// <param name="left">The first quaternion.</param>
    /// <param name="right">The second quaternion.</param>
    /// <returns>The dot product of the two quaternions.</returns>
    /// <remarks>
    /// The dot product of two quaternions represents the cosine of the angle between them. It is calculated
    /// by multiplying the corresponding components of the quaternions and summing the results.
    /// 
    ///     dot = (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z) + (left.W * right.W)
    /// 
    /// The dot product can be useful for various quaternion operations, such as determining if two quaternions
    /// are parallel or perpendicular to each other.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T DotProduct<T>(in Quaternion<T> left, in Quaternion<T> right)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z) + (left.W * right.W);

    /// <summary>
    /// Performs linear interpolation (LERP) between two quaternions.
    /// </summary>
    /// <typeparam name="T">The underlying numeric type of the quaternion coordinates.</typeparam>
    /// <param name="start">The starting quaternion, returned when <paramref name="factor"/> is 0.</param>
    /// <param name="end">The ending quaternion, returned when <paramref name="factor"/> is 1.</param>
    /// <param name="factor">The interpolation factor, ranging from 0 to 1.</param>
    /// <returns>The interpolated quaternion.</returns>
    /// <remarks>
    /// This method performs linear interpolation between the start and end quaternions using the given factor.
    /// The interpolation is performed in a straight line and does not consider the shortest path between the quaternions.
    /// The resulting interpolated quaternion may not be normalized. If normalization is desired, call the
    /// <see cref="Quaternion.Normalize"/> method on the result.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion<T> Lerp<T>(in Quaternion<T> start, in Quaternion<T> end, T factor)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T> 
        => (start * (T.One - factor)) + (end * factor);

    /// <summary>
    /// Performs linear interpolation (LERP) between two quaternions using the shortest path.
    /// </summary>
    /// <typeparam name="T">The underlying numeric type of the quaternion coordinates.</typeparam>
    /// <param name="start">The starting quaternion, returned when <paramref name="factor"/> is 0.</param>
    /// <param name="end">The ending quaternion, returned when <paramref name="factor"/> is 1.</param>
    /// <param name="factor">The interpolation factor, ranging from 0 to 1.</param>
    /// <returns>The interpolated quaternion.</returns>
    /// <remarks>
    /// This method performs linear interpolation between the start and end quaternions using the given factor.
    /// The interpolation is performed along the shortest path on the quaternion unit sphere, ensuring smooth
    /// and continuous rotations. The resulting interpolated quaternion may not be normalized. If normalization
    /// is desired, call the <see cref="Quaternion.Normalize"/> method on the result.
    /// </remarks>
    public static Quaternion<T> LerpShortestPath<T>(in Quaternion<T> start, in Quaternion<T> end, T factor)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => (T.Sign(DotProduct(in start, in end)) < 0)
            ? (start * (T.One - factor)) - (end * factor)
            : (start * (T.One - factor)) + (end * factor);

    /// <summary>
    /// Interpolates between two quaternions using spherical linear interpolation (SLERP).
    /// </summary>
    /// <typeparam name="T">The numeric type of the quaternion components.</typeparam>
    /// <param name="start">The starting quaternion.</param>
    /// <param name="end">The ending quaternion.</param>
    /// <param name="factor">The interpolation factor, where 0 returns the start quaternion and 1 returns the end quaternion.</param>
    /// <returns>The interpolated quaternion.</returns>
    /// <remarks>
    /// The method performs spherical linear interpolation (SLERP) between the start and end quaternions
    /// using the provided interpolation factor. The resulting quaternion represents an intermediate rotation
    /// between the two input quaternions. 
    /// </remarks>
    public static Quaternion<T> Slerp<T>(in Quaternion<T> start, in Quaternion<T> end, T factor)
        where T : struct, IFloatingPointIeee754<T>, IMinMaxValue<T>
    {
        // Ensure the quaternions are normalized
        var startNormalized = Normalize(in start);
        var endNormalized = Normalize(in end);

        // Calculate the dot product between the quaternions
        var dot = DotProduct(in startNormalized, in endNormalized);

        // Clamp the dot product to ensure valid interpolation
        dot = T.Clamp(dot, T.Zero, T.One);

        // Calculate the angle between the quaternions
        var theta = T.Acos(dot);

        // Perform the spherical linear interpolation
        var sinTheta = T.Sin(theta);
        if (sinTheta < T.Epsilon)
        {
            // Start and end quaternions are parallel or antiparallel
            // Return either the start or end quaternion
            return startNormalized;
        }

        return
            (startNormalized * (T.Sin((T.One - factor) * theta) / sinTheta)) +
            (endNormalized * (T.Sin(factor * theta) / sinTheta));
    }

    /// <summary>
    /// Interpolates between two quaternions using spherical linear interpolation (SLERP) using the shortest path.
    /// </summary>
    /// <typeparam name="T">The numeric type of the quaternion components.</typeparam>
    /// <param name="start">The starting quaternion.</param>
    /// <param name="end">The ending quaternion.</param>
    /// <param name="factor">The interpolation factor, where 0 returns the start quaternion and 1 returns the end quaternion.</param>
    /// <returns>The interpolated quaternion.</returns>
    /// <remarks>
    /// The method performs spherical linear interpolation (SLERP) between the start and end quaternions
    /// using the provided interpolation factor. The resulting quaternion represents an intermediate rotation
    /// between the two input quaternions. The interpolation is always performed along the shortest path
    /// on the surface of the unit sphere.
    /// </remarks>
    public static Quaternion<T> SlerpShortestPath<T>(in Quaternion<T> start, in Quaternion<T> end, T factor)
        where T : struct, IFloatingPointIeee754<T>, IMinMaxValue<T>
    {
        // Ensure the quaternions are normalized
        var startNormalized = Normalize(in start);
        var endNormalized = Normalize(in end);

        // Calculate the dot product between the quaternions
        var dot = DotProduct(in startNormalized, in endNormalized);

        // Adjust the end quaternion to take the shortest path
        if (T.Sign(dot) < 0)
        {
            dot = -dot;
            startNormalized = -startNormalized;
        }

        // Clamp the dot product to ensure valid interpolation
        dot = T.Clamp(dot, T.Zero, T.One);

        // Calculate the angle between the quaternions
        var theta = T.Acos(dot);

        // Perform the spherical linear interpolation
        var sinTheta = T.Sin(theta);
        if (sinTheta < T.Epsilon)
        {
            // Start and end quaternions are parallel or antiparallel
            // Return either the start or end quaternion
            return startNormalized;
        }

        return
            (startNormalized * (T.Sin((T.One - factor) * theta) / sinTheta)) + 
            (endNormalized * (T.Sin(factor * theta) / sinTheta));
    }

}

