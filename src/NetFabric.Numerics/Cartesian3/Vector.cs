namespace NetFabric.Numerics.Cartesian3;

/// <summary>
/// Represents a vector as an immutable struct.
/// <typeparam name="T">The type of the vector coordinates.</typeparam>
/// <param name="X">The X coordinate.</param>
/// <param name="Y">The X coordinate.</param>
/// <param name="Z">The X coordinate.</param>
[System.Diagnostics.DebuggerDisplay("X = {X}, Y = {Y}, Z = {Z}")]
public readonly record struct Vector<T>(T X, T Y, T Z) 
    : IVector<Vector<T>>
    where T: struct, INumber<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<T> CoordinateSystem 
        => new();
    ICoordinateSystem IVector<Vector<T>>.CoordinateSystem 
        => CoordinateSystem;

    /// <summary>
    /// Calculates the length (magnitude) of the vector.
    /// </summary>
    /// <returns>The length of the vector.</returns>
    /// <remarks>
    /// <para>
    /// The length is calculated as the Euclidean distance in the 2D Cartesian coordinate system.
    /// </para>
    /// </remarks>
    public double Length
        => Math.Sqrt(double.CreateChecked(LengthSquared));

    /// <summary>
    /// Calculates the square of the length (magnitude) of the vector.
    /// </summary>
    /// <returns>The square of the length of the vector.</returns>
    /// <remarks>
    /// <para>
    /// The square of the length is calculated as the Euclidean distance in the 2D Cartesian coordinate system.
    /// </para>
    /// <para>
    /// Note that the square of the length is returned instead of the actual length to avoid the need for
    /// taking the square root, which can be a computationally expensive operation.
    /// </para>
    /// </remarks>
    public T LengthSquared
        => Utils.Pow2(X) + Utils.Pow2(Y) + Utils.Pow2(Z);

    #region constants

    /// <summary>
    /// Represents a vector whose 3 coordinates are equal to zero. This field is read-only.
    /// </summary>
    public static readonly Vector<T> Zero = new(T.Zero, T.Zero, T.Zero);

    static Vector<T> IVector<Vector<T>>.Zero
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

    static Vector<T> IAdditiveIdentity<Vector<T>, Vector<T>>.AdditiveIdentity
        => new(T.AdditiveIdentity, T.AdditiveIdentity, T.AdditiveIdentity);

    static Vector<T> IMinMaxValue<Vector<T>>.MinValue
        => MinValue;
    static Vector<T> IMinMaxValue<Vector<T>>.MaxValue
        => MaxValue;

    #endregion

    #region comparison

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int CompareTo(Vector<T> other)
        => this.LengthSquared.CompareTo(other.LengthSquared);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Vector<T> left, Vector<T> right)
        => left.CompareTo(right) < 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Vector<T> left, Vector<T> right)
        => left.CompareTo(right) <= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Vector<T> left, Vector<T> right)
        => left.CompareTo(right) > 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Vector<T> left, Vector<T> right)
        => left.CompareTo(right) >= 0;

    readonly int IComparable.CompareTo(object? obj)
        => obj switch
        {
            null => 1,
            Vector<T> other => CompareTo(other),
            _ => Throw.ArgumentException<int>($"Argument must be of type {nameof(Vector<T>)}.", nameof(obj)),
        };

    #endregion

    #region addition

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator +(Vector<T> right)
        => right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator +(Vector<T> left, Vector<T> right)
        => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    #endregion

    #region subtraction

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator -(Vector<T> right)
        => new(-right.X, -right.Y, -right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator -(Vector<T> left, Vector<T> right)
        => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    #endregion

    #region multiplication

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator *(T left, Vector<T> right)
        => new(left * right.X, left * right.Y, left * right.Z);

    #endregion

    #region division

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator /(Vector<T> left, T right)
        => new(left.X / right, left.Y / right, left.Z / right);

    #endregion

    object IVector<Vector<T>>.this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}

/// <summary>
/// Provides static methods for vector operations.
/// </summary>
public static class Vector
{
    /// <summary>
    /// Applies a quaternion to a 3-dimensional vector.
    /// </summary>
    /// <typeparam name="T">The underlying numeric type of the quaternion and vector coordinates.</typeparam>
    /// <param name="quaternion">The quaternion to apply.</param>
    /// <param name="vector">The 3-dimensional vector to transform.</param>
    /// <returns>The transformed 3-dimensional vector.</returns>
    /// <remarks>
    /// <para>
    /// The <paramref name="quaternion"/> is not required to be a unit quaternion.
    /// </para>
    /// <para>
    /// The vector coordinates type must be a floating point.
    /// </para>
    /// <para>
    /// The transformation is applied by multiplying the vector with the quaternion,
    /// resulting in a new 3-dimensional vector that represents the original vector
    /// after being transformed by the quaternion.
    /// </para>
    /// </remarks>
    public static Vector<T> Apply<T>(Quaternion<T> quaternion, Vector<T> vector)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(
            (quaternion.W * vector.X) + (quaternion.Y * vector.Z) - (quaternion.Z * vector.Y),
            (quaternion.W * vector.Y) + (quaternion.Z * vector.X) - (quaternion.X * vector.Z),
            (quaternion.W * vector.Z) + (quaternion.X * vector.Y) - (quaternion.Y * vector.X)
        );

    /// <summary>
    /// Returns a new vector that is clamped within the specified minimum and maximum values for each component.
    /// </summary>
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
    public static Vector<T> Clamp<T>(in Vector<T> vector, in Vector<T> min, in Vector<T> max)
    where T : struct, INumber<T>, IMinMaxValue<T>
        => new(T.Clamp(vector.X, min.X, max.X), T.Clamp(vector.Y, min.Y, max.Y), T.Clamp(vector.Z, min.Z, max.Z));

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
    public static Vector<T> Normalize<T>(in Vector<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        var length = T.CreateChecked(vector.Length);
        return length != T.Zero
            ? new(vector.X / length, vector.Y / length, vector.Z / length)
            : Vector<T>.Zero;
    }

    /// <summary>
    /// Calculates the dot product.
    /// </summary>
    /// <param name="left">A vector.</param>
    /// <param name="right">A vector.</param>
    /// <returns>The dot product.</returns>
    public static T DotProduct<T>(in Vector<T> left, in Vector<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);


    /// <summary>
    /// Calculates the cross product magnitude.
    /// </summary>
    /// <param name="left">A vector.</param>
    /// <param name="right">A vector.</param>
    /// <returns>The cross products.</returns>
    public static Vector<T> CrossProduct<T>(in Vector<T> left, in Vector<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new((left.Y * right.Z) - (left.Z * right.Y),
                (left.Z * right.X) - (left.X * right.Z),
                (left.X * right.Y) - (left.Y * right.X));

    /// <summary>
    /// Gets the angle between two vectors.
    /// </summary>
    /// <param name="from">The vector where the angle measurement starts at.</param>
    /// <param name="to">The vector where the angle measurement stops at.</param>
    /// <returns>The angle between two vectors.</returns>
    /// <remarks>The angle signal is determined by the right-hand rule.</remarks>
    public static Angle<Radians, TAngle> Angle<T, TAngle>(in Vector<T> from, in Vector<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        => new(TAngle.CreateChecked(Math.Acos(double.CreateChecked(DotProduct(from, to)) / (from.Length * to.Length))));
}
