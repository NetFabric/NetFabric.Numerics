namespace NetFabric.Numerics.Cartesian2;

public readonly record struct Vector<T>(T X, T Y) 
    : IVector<Vector<T>>
    where T: struct, INumber<T>, IMinMaxValue<T>
{
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
        => Math.Sqrt(double.CreateChecked(SquareOfLength));

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
    public T SquareOfLength
        => Utils.Pow2(X) + Utils.Pow2(Y);

    #region constants

    /// <summary>
    /// Represents a vector whose 2 coordinates are equal to zero. This field is read-only.
    /// </summary>
    public static readonly Vector<T> Zero = new(T.Zero, T.Zero);

    static Vector<T> IVector<Vector<T>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Vector<T> MinValue = new(T.MinValue, T.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Vector<T> MaxValue = new(T.MaxValue, T.MaxValue);

    static Vector<T> IAdditiveIdentity<Vector<T>, Vector<T>>.AdditiveIdentity
        => new(T.AdditiveIdentity, T.AdditiveIdentity);

    static Vector<T> IMinMaxValue<Vector<T>>.MinValue
        => MinValue;
    static Vector<T> IMinMaxValue<Vector<T>>.MaxValue
        => MaxValue;

    #endregion

    #region comparison

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int CompareTo(Vector<T> other)
        => this.SquareOfLength.CompareTo(other.SquareOfLength);

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
        => new(+right.X, +right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator +(Vector<T> left, Vector<T> right)
        => new (left.X + right.X, left.Y + right.Y);

    #endregion

    #region subtraction

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator -(Vector<T> right)
        => new(-right.X, -right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator -(Vector<T> left, Vector<T> right)
        => new(left.X - right.X, left.Y - right.Y);

    #endregion

    #region multiplication

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator *(T left, Vector<T> right)
        => new(left * right.X, left * right.Y);

    #endregion

    #region division

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator /(Vector<T> left, T right)
        => new(left.X / right, left.Y / right);

    #endregion

    object IVector<Vector<T>>.this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}

public static class Vector
{

    public static Vector<T> Normalize<T>(Vector<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        double length = vector.Length;
        return new(vector.X / T.CreateChecked(length), vector.Y / T.CreateChecked(length));
    }

    /// <summary>
    /// Calculates the dot product.
    /// </summary>
    /// <param name="left">A vector.</param>
    /// <param name="right">A vector.</param>
    /// <returns>The dot product.</returns>
    public static T DotProduct<T>(Vector<T> left, Vector<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => (left.X * right.X) + (left.Y * right.Y);


    /// <summary>
    /// Calculates the cross product magnitude.
    /// </summary>
    /// <param name="left">A vector.</param>
    /// <param name="right">A vector.</param>
    /// <returns>The magnitude of the cross products.</returns>
    public static T CrossProduct<T>(Vector<T> left, Vector<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => (left.X * right.X) - (left.Y * right.Y);

    /// <summary>
    /// Gets the angle between two vectors.
    /// </summary>
    /// <param name="from">The vector where the angle measurement starts at.</param>
    /// <param name="to">The vector where the angle measurement stops at.</param>
    /// <returns>The angle between two vectors.</returns>
    /// <remarks>The angle signal is determined by the right-hand rule.</remarks>
    public static Angle<Radians, TAngle> Angle<T, TAngle>(Vector<T> from, Vector<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
    {
        var radians = Math.Acos(double.CreateChecked(DotProduct(from, to)) / (from.Length * to.Length));
        return T.Sign(CrossProduct(from, to)) < 0 
            ? new(TAngle.CreateChecked(-radians))
            : new(TAngle.CreateChecked(radians));
    }
}
