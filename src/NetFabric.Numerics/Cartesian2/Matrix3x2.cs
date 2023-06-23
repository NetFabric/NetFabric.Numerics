namespace NetFabric.Numerics.Cartesian2;

public readonly record struct Matrix3x2<T>(
    T M11, T M12, 
    T M21, T M22, 
    T M31, T M32)
    where T : struct, IFloatingPoint<T>
{
    #region constants

    public static readonly Matrix3x2<T> Zero = new(
        T.Zero, T.Zero,
        T.Zero, T.Zero,
        T.Zero, T.Zero);

    public static readonly Matrix3x2<T> Identity = new(
        T.One, T.Zero,
        T.Zero, T.One,
        T.Zero, T.Zero);

    #endregion

    #region operators

    public static Matrix3x2<T> operator +(Matrix3x2<T> left, Matrix3x2<T> right) 
        => Matrix3x2.Add(left, right);

    public static Matrix3x2<T> operator -(Matrix3x2<T> right) 
        => Matrix3x2.Negate(right);

    public static Matrix3x2<T> operator -(Matrix3x2<T> left, Matrix3x2<T> right) 
        => Matrix3x2.Subtract(left, right);

    public static Matrix3x2<T> operator *(Matrix3x2<T> left, Matrix3x2<T> right) 
        => Matrix3x2.Multiply(left, right);

    public static Matrix3x2<T> operator *(Matrix3x2<T> matrix, T scalar) 
        => Matrix3x2.Multiply(matrix, scalar);

    public static Matrix3x2<T> operator *(T scalar, Matrix3x2<T> matrix) 
        => Matrix3x2.Multiply(matrix, scalar);

    public static Matrix3x2<T> operator /(Matrix3x2<T> matrix, T scalar) 
        => Matrix3x2.Divide(matrix, scalar);

    #endregion
}

public static class Matrix3x2
{

    #region arithmetic

    public static Matrix3x2<T> Add<T>(Matrix3x2<T> left, Matrix3x2<T> right)
        where T : struct, IFloatingPoint<T>
        => new(
            left.M11 + right.M11, left.M12 + right.M12,
            left.M21 + right.M21, left.M22 + right.M22,
            left.M31 + right.M31, left.M32 + right.M32);

    public static Matrix3x2<T> Negate<T>(Matrix3x2<T> matrix)
        where T : struct, IFloatingPoint<T>
        => new(
            -matrix.M11, -matrix.M12,
            -matrix.M21, -matrix.M22,
            -matrix.M31, -matrix.M32);

    public static Matrix3x2<T> Subtract<T>(Matrix3x2<T> left, Matrix3x2<T> right) 
        where T : struct, IFloatingPoint<T> 
        => new(
            left.M11 - right.M11, left.M12 - right.M12,
            left.M21 - right.M21, left.M22 - right.M22,
            left.M31 - right.M31, left.M32 - right.M32);

    public static Matrix3x2<T> Multiply<T>(Matrix3x2<T> left, Matrix3x2<T> right) 
        where T : struct, IFloatingPoint<T> 
        => new(
            left.M11 * right.M11 + left.M12 * right.M21, left.M11 * right.M12 + left.M12 * right.M22,
            left.M21 * right.M11 + left.M22 * right.M21, left.M21 * right.M12 + left.M22 * right.M22,
            left.M31 * right.M11 + left.M32 * right.M21 + right.M31, left.M31 * right.M12 + left.M32 * right.M22 + right.M32);

    public static Matrix3x2<T> Multiply<T>(Matrix3x2<T> matrix, T scalar) 
        where T : struct, IFloatingPoint<T> 
        => new(
            matrix.M11 * scalar, matrix.M12 * scalar,
            matrix.M21 * scalar, matrix.M22 * scalar,
            matrix.M31 * scalar, matrix.M32 * scalar);

    public static Matrix3x2<T> Divide<T>(Matrix3x2<T> matrix, T scalar) 
        where T : struct, IFloatingPoint<T> 
        => new(
            matrix.M11 / scalar, matrix.M12 / scalar,
            matrix.M21 / scalar, matrix.M22 / scalar,
            matrix.M31 / scalar, matrix.M32 / scalar);

    #endregion

    public static bool IsIdentity<T>(this Matrix3x2<T> matrix)
        where T : struct, IFloatingPoint<T>
        => matrix == Matrix3x2<T>.Identity;

    public static bool IsZero<T>(this Matrix3x2<T> matrix)
        where T : struct, IFloatingPoint<T>
        => matrix == Matrix3x2<T>.Zero;

    public static bool IsNaN<T>(this Matrix3x2<T> matrix)
        where T : struct, IFloatingPoint<T>
        => T.IsNaN(matrix.M11) || T.IsNaN(matrix.M12)

        || T.IsNaN(matrix.M21) || T.IsNaN(matrix.M22)

            || T.IsNaN(matrix.M31) || T.IsNaN(matrix.M32);  

    public static bool IsFinite<T>(this Matrix3x2<T> matrix)
        where T : struct, IFloatingPoint<T>
        => T.IsFinite(matrix.M11) && T.IsFinite(matrix.M12)

        && T.IsFinite(matrix.M21) && T.IsFinite(matrix.M22)

            && T.IsFinite(matrix.M31) && T.IsFinite(matrix.M32);

    public static bool IsNormalized<T>(this Matrix3x2<T> matrix)
        where T : struct, IFloatingPoint<T>
        => matrix.M11 * matrix.M11 + matrix.M12 * matrix.M12 == T.One

        && matrix.M21 * matrix.M21 + matrix.M22 * matrix.M22 == T.One

            && matrix.M31 * matrix.M31 + matrix.M32 * matrix.M32 == T.One;

    public static bool IsOrthogonal<T>(this Matrix3x2<T> matrix)
        where T : struct, IFloatingPoint<T>
        => matrix.M11 * matrix.M12 + matrix.M21 * matrix.M22 == T.Zero

        && matrix.M11 * matrix.M31 + matrix.M21 * matrix.M32 == T.Zero

            && matrix.M12 * matrix.M31 + matrix.M22 * matrix.M32 == T.Zero;
}
