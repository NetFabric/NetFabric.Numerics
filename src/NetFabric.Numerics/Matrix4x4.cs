using NetFabric.Numerics.Cartesian3;

namespace NetFabric.Numerics;

/// <summary>
/// Represents a 4x4 matrix of elements of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the matrix elements.</typeparam>
/// <remarks>
/// This struct provides a representation of a 4x4 matrix with four rows and four columns.
/// The matrix is stored using four <see cref="Vector4{T}"/> instances representing the
/// columns of the matrix.
/// </remarks>
public readonly record struct Matrix4x4<T>(Vector4<T> X, Vector4<T> Y, Vector4<T> Z, Vector4<T> W)
    : IMatrix<Matrix4x4<T>, T>
    where T : struct, INumber<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="matrix"/>.</typeparam>
    /// <param name="matrix">The value which is used to create the instance of <see cref="Matrix4x4{T}"/></param>
    /// <returns>An instance of <see cref="Matrix4x4{T}"/> created from <paramref name="matrix" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="matrix" /> is not representable by <see cref="Matrix4x4{T}"/>.</exception>
    public static Matrix4x4<T> CreateChecked<TOther>(in Matrix4x4<TOther> matrix)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>
        => new(
            Vector4<T>.CreateChecked(matrix.X),
            Vector4<T>.CreateChecked(matrix.Y),
            Vector4<T>.CreateChecked(matrix.Z),
            Vector4<T>.CreateChecked(matrix.W)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="matrix"/>.</typeparam>
    /// <param name="matrix">The value which is used to create the instance of <see cref="Matrix4x4{T}"/></param>
    /// <returns>An instance of <see cref="Matrix4x4{T}"/> created from <paramref name="matrix" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="matrix" /> is not representable by <see cref="Matrix4x4{T}"/>.</exception>
    public static Matrix4x4<T> CreateSaturating<TOther>(in Matrix4x4<TOther> matrix)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>
        => new(
            Vector4<T>.CreateSaturating(matrix.X),
            Vector4<T>.CreateSaturating(matrix.Y),
            Vector4<T>.CreateSaturating(matrix.Z),
            Vector4<T>.CreateSaturating(matrix.W)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="matrix"/>.</typeparam>
    /// <param name="matrix">The value which is used to create the instance of <see cref="Matrix4x4{T}"/></param>
    /// <returns>An instance of <see cref="Matrix4x4{T}"/> created from <paramref name="matrix" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="matrix" /> is not representable by <see cref="Matrix4x4{T}"/>.</exception>
    public static Matrix4x4<T> CreateTruncating<TOther>(in Matrix4x4<TOther> matrix)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>
        => new(
            Vector4<T>.CreateTruncating(matrix.X),
            Vector4<T>.CreateTruncating(matrix.Y),
            Vector4<T>.CreateTruncating(matrix.Z),
            Vector4<T>.CreateTruncating(matrix.W)
        );

    #region constants

    int IMatrix<Matrix4x4<T>, T>.RowCount 
        => 4;
    int IMatrix<Matrix4x4<T>, T>.ColumnCount 
        => 4;


    public static readonly Matrix4x4<T> Zero = new(Vector4<T>.Zero, Vector4<T>.Zero, Vector4<T>.Zero, Vector4<T>.Zero);

    public static readonly Matrix4x4<T> Identity = new(
        new(T.One, T.Zero, T.Zero, T.Zero),
        new(T.Zero, T.One, T.Zero, T.Zero),
        new(T.Zero, T.Zero, T.One, T.Zero),
        new(T.Zero, T.Zero, T.Zero, T.One);

    static Matrix4x4<T> INumericBase<Matrix4x4<T>>.Zero
        => Zero;

    static Matrix4x4<T> IMatrix<Matrix4x4<T>, T>.Identity
        => Identity;

    #endregion

    #region operators

    public static Matrix4x4<T> operator +(Matrix4x4<T> left, Matrix4x4<T> right)
        => Matrix4x4.Add(left, right);

    public static Matrix4x4<T> operator -(Matrix4x4<T> right)
        => Matrix4x4.Negate(right);

    public static Matrix4x4<T> operator -(Matrix4x4<T> left, Matrix4x4<T> right)
        => Matrix4x4.Subtract(left, right);

    public static Matrix4x4<T> operator *(Matrix4x4<T> left, Matrix4x4<T> right)
        => Matrix4x4.Multiply(left, right);

    public static Matrix4x4<T> operator *(Matrix4x4<T> matrix, T scalar)
        => Matrix4x4.Multiply(matrix, scalar);

    public static Matrix4x4<T> operator *(T scalar, Matrix4x4<T> matrix)
        => Matrix4x4.Multiply(matrix, scalar);

    public static Matrix4x4<T> operator /(Matrix4x4<T> matrix, T scalar)
        => Matrix4x4.Divide(matrix, scalar);

    #endregion
}

public static class Matrix4x4
{

    #region arithmetic

    public static Matrix4x4<T> Add<T>(Matrix4x4<T> left, Matrix4x4<T> right)
        where T : struct, IFloatingPoint<T>
        => new(
            left.M00 + right.M00, left.M01 + right.M01, left.M02 + right.M02, left.M03 + right.M03,
            left.M10 + right.M10, left.M11 + right.M11, left.M12 + right.M12, left.M13 + right.M13,
            left.M20 + right.M20, left.M21 + right.M21, left.M22 + right.M22, left.M23 + right.M23,
            left.M30 + right.M30, left.M31 + right.M31, left.M32 + right.M32, left.M33 + right.M33);

    public static Matrix4x4<T> Subtract<T>(Matrix4x4<T> left, Matrix4x4<T> right)
        where T : struct, IFloatingPoint<T>
        => new(
            left.M00 - right.M00, left.M01 - right.M01, left.M02 - right.M02, left.M03 - right.M03,
            left.M10 - right.M10, left.M11 - right.M11, left.M12 - right.M12, left.M13 - right.M13,
            left.M20 - right.M20, left.M21 - right.M21, left.M22 - right.M22, left.M23 - right.M23,
            left.M30 - right.M30, left.M31 - right.M31, left.M32 - right.M32, left.M33 - right.M33);

    public static Matrix4x4<T> Multiply<T>(Matrix4x4<T> left, Matrix4x4<T> right)
        where T : struct, IFloatingPoint<T>
        => new(
            left.M00 * right.M00 + left.M01 * right.M10 + left.M02 * right.M20 + left.M03 * right.M30,
            left.M00 * right.M01 + left.M01 * right.M11 + left.M02 * right.M21 + left.M03 * right.M31,
            left.M00 * right.M02 + left.M01 * right.M12 + left.M02 * right.M22 + left.M03 * right.M32,
            left.M00 * right.M03 + left.M01 * right.M13 + left.M02 * right.M23 + left.M03 * right.M33,
            left.M10 * right.M00 + left.M11 * right.M10 + left.M12 * right.M20 + left.M13 * right.M30,
            left.M10 * right.M01 + left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31,
            left.M10 * right.M02 + left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32,
            left.M10 * right.M03 + left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33,
            left.M20 * right.M00 + left.M21 * right.M10 + left.M22 * right.M20 + left.M23 * right.M30,
            left.M20 * right.M01 + left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31,
            left.M20 * right.M02 + left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32,
            left.M20 * right.M03 + left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33,
            left.M30 * right.M00 + left.M31 * right.M10 + left.M32 * right.M20 + left.M33 * right.M30,
            left.M30 * right.M01 + left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31,
            left.M30 * right.M02 + left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32,
            left.M30 * right.M03 + left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33);

    public static Matrix4x4<T> Multiply<T>(Matrix4x4<T> matrix, T scalar)
        where T : struct, IFloatingPoint<T>
        => new(
            matrix.M00 * scalar, matrix.M01 * scalar, matrix.M02 * scalar, matrix.M03 * scalar,
            matrix.M10 * scalar, matrix.M11 * scalar, matrix.M12 * scalar, matrix.M13 * scalar,
            matrix.M20 * scalar, matrix.M21 * scalar, matrix.M22 * scalar, matrix.M23 * scalar,
            matrix.M30 * scalar, matrix.M31 * scalar, matrix.M32 * scalar, matrix.M33 * scalar);

    public static Matrix4x4<T> Divide<T>(Matrix4x4<T> matrix, T scalar)
        where T : struct, IFloatingPoint<T>
        => new(
            matrix.M00 / scalar, matrix.M01 / scalar, matrix.M02 / scalar, matrix.M03 / scalar,
            matrix.M10 / scalar, matrix.M11 / scalar, matrix.M12 / scalar, matrix.M13 / scalar,
            matrix.M20 / scalar, matrix.M21 / scalar, matrix.M22 / scalar, matrix.M23 / scalar,
            matrix.M30 / scalar, matrix.M31 / scalar, matrix.M32 / scalar, matrix.M33 / scalar);

    public static Matrix4x4<T> Negate<T>(Matrix4x4<T> matrix)
        where T : struct, IFloatingPoint<T>
        => new(-matrix.X, -matrix.Y, -matrix.Z, -matrix.W);

    #endregion

    public static bool IsIdentity<T>(in Matrix4x4<T> matrix)
        where T : struct, INumber<T>
        => matrix == Matrix4x4<T>.Identity;

    public static bool IsZero<T>(in Matrix4x4<T> matrix)
        where T : struct, INumber<T>
        => matrix == Matrix4x4<T>.Zero;

    public static bool IsNaN<T>(in Matrix4x4<T> matrix)
        where T : struct, INumber<T>
        => T.IsNaN(matrix.M00) || T.IsNaN(matrix.M01) || T.IsNaN(matrix.M02) || T.IsNaN(matrix.M03)
        || T.IsNaN(matrix.M10) || T.IsNaN(matrix.M11) || T.IsNaN(matrix.M12) || T.IsNaN(matrix.M13)
        || T.IsNaN(matrix.M20) || T.IsNaN(matrix.M21) || T.IsNaN(matrix.M22) || T.IsNaN(matrix.M23)
        || T.IsNaN(matrix.M30) || T.IsNaN(matrix.M31) || T.IsNaN(matrix.M32) || T.IsNaN(matrix.M33);

    public static bool IsInfinity<T>(in Matrix4x4<T> matrix)
        where T : struct, INumber<T>
        => T.IsInfinity(matrix.M00) || T.IsInfinity(matrix.M01) || T.IsInfinity(matrix.M02) || T.IsInfinity(matrix.M03)
        || T.IsInfinity(matrix.M10) || T.IsInfinity(matrix.M11) || T.IsInfinity(matrix.M12) || T.IsInfinity(matrix.M13)
        || T.IsInfinity(matrix.M20) || T.IsInfinity(matrix.M21) || T.IsInfinity(matrix.M22) || T.IsInfinity(matrix.M23)
        || T.IsInfinity(matrix.M30) || T.IsInfinity(matrix.M31) || T.IsInfinity(matrix.M32) || T.IsInfinity(matrix.M33);

    public static bool IsFinite<T>(in Matrix4x4<T> matrix)
        where T : struct, INumber<T>
        => T.IsFinite(matrix.M00) && T.IsFinite(matrix.M01) && T.IsFinite(matrix.M02) && T.IsFinite(matrix.M03)
        && T.IsFinite(matrix.M10) && T.IsFinite(matrix.M11) && T.IsFinite(matrix.M12) && T.IsFinite(matrix.M13)
        && T.IsFinite(matrix.M20) && T.IsFinite(matrix.M21) && T.IsFinite(matrix.M22) && T.IsFinite(matrix.M23)
        && T.IsFinite(matrix.M30) && T.IsFinite(matrix.M31) && T.IsFinite(matrix.M32) && T.IsFinite(matrix.M33);

    public static bool IsNormalized<T>(in Matrix4x4<T> matrix)
        where T : struct, INumber<T>
        => T.IsOne(matrix.M00) && T.IsZero(matrix.M01) && T.IsZero(matrix.M02) && T.IsZero(matrix.M03)
        && T.IsZero(matrix.M10) && T.IsOne(matrix.M11) && T.IsZero(matrix.M12) && T.IsZero(matrix.M13)
        && T.IsZero(matrix.M20) && T.IsZero(matrix.M21) && T.IsOne(matrix.M22) && T.IsZero(matrix.M23)
        && T.IsZero(matrix.M30) && T.IsZero(matrix.M31) && T.IsZero(matrix.M32) && T.IsOne(matrix.M33);

    public static bool IsOrthogonal(Matrix4x4 matrix)
    {
        var transposed = Matrix4x4.Transpose(matrix);
        var inverse = Matrix4x4.Invert(matrix);

        return matrix == transposed && matrix == inverse;
    }

    public static Matrix4x4<T> CreateTranslation<T>(in Point<T> position)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(
            T.One, T.Zero, T.Zero, position.X,
            T.Zero, T.One, T.Zero, position.Y,
            T.Zero, T.Zero, T.One, position.Z,
            T.Zero, T.Zero, T.Zero, T.One);

    public static Matrix4x4<T> CreateScale<T>(in Point<T> scale)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(
            scale.X, T.Zero, T.Zero, T.Zero,
            T.Zero, scale.Y, T.Zero, T.Zero,
            T.Zero, T.Zero, scale.Z, T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);

    public static Matrix4x4<T> CreateRotationX<T>(Angle<Radians, T> angle)
        where T : struct, INumber<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var (sin, cos) = Angle.SinCos(angle);
        return new(
            T.One, T.Zero, T.Zero, T.Zero,
            T.Zero, cos, sin, T.Zero,
            T.Zero, -sin, cos, T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> CreateRotationY<T>(Angle<Radians, T> angle)
        where T : struct, INumber<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var (sin, cos) = Angle.SinCos(angle);
        return new(
            cos, T.Zero, -sin, T.Zero,
            T.Zero, T.One, T.Zero, T.Zero,
            sin, T.Zero, cos, T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> CreateRotationZ<T>(Angle<Radians, T> angle)
        where T : struct, INumber<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var (sin, cos) = Angle.SinCos(angle);
        return new(
            cos, sin, T.Zero, T.Zero,
            -sin, cos, T.Zero, T.Zero,
            T.Zero, T.Zero, T.One, T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> CreateRotation<T>(in Vector3<T> axis, Angle<Radians, T> angle)
        where T : struct, INumber<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var (sin, cos) = Angle.SinCos(angle);
        var (x, y, z) = axis;
        var oneMinusCos = T.One - cos;
        var xy = x * y;
        var xz = x * z;
        var yz = y * z;
        return new(
            cos + x * x * oneMinusCos, xy * oneMinusCos + z * sin, xz * oneMinusCos - y * sin, T.Zero,
            xy * oneMinusCos - z * sin, cos + y * y * oneMinusCos, yz * oneMinusCos + x * sin, T.Zero,
            xz * oneMinusCos + y * sin, yz * oneMinusCos - x * sin, cos + z * z * oneMinusCos, T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> CreateFromQuaternion<T>(in Quaternion<T> quaternion)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        var (x, y, z, w) = quaternion;
        var xx = x * x;
        var yy = y * y;
        var zz = z * z;
        var xy = x * y;
        var zw = z * w;
        var zx = z * x;
        var yw = y * w;
        var yz = y * z;
        var xw = x * w;
        var two = T.One + T.One;
        return new(
            T.One - two * (yy + zz), two * (xy + zw), two * (zx - yw), T.Zero,
            two * (xy - zw), T.One - two * (zz + xx), two * (yz + xw), T.Zero,
            two * (zx + yw), two * (yz - xw), T.One - two * (yy + xx), T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> CreateLookAt<T>(in Point<T> cameraPosition, in Point<T> cameraTarget, in Vector3<T> cameraUpVector)
        where T : struct, INumber<T>, IMinMaxValue<T>, IRootFunctions<T>
    {
        var z = Vector3.Normalize(cameraPosition - cameraTarget);
        var x = Vector3.Normalize(Vector3.Cross(cameraUpVector, z));
        var y = Vector3.Cross(z, x);
        return new(
            x.X, x.Y, x.Z, -Point.DotProduct(cameraPosition, x),
            y.X, y.Y, y.Z, -Point.DotProduct(cameraPosition, y),
            z.X, z.Y, z.Z, -Point.DotProduct(cameraPosition, z),
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> CreatePerspectiveFieldOfView<T>(Angle<Radians, T> fieldOfView, T aspectRatio, T nearPlaneDistance, T farPlaneDistance)
        where T : struct, INumber<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var two = T.One + T.One;
        var (sin, cos) = Angle.SinCos(fieldOfView / two);
        var height = cos / sin;
        var width = height / aspectRatio;
        var range = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
        return new(
            width, T.Zero, T.Zero, T.Zero,
            T.Zero, height, T.Zero, T.Zero,
            T.Zero, T.Zero, range, -range * nearPlaneDistance,
            T.Zero, T.Zero, -T.One, T.Zero);
    }

    public static Matrix4x4<T> CreatePerspective<T>(T width, T height, T nearPlaneDistance, T farPlaneDistance)
        where T : struct, INumber<T>
    {
        var range = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
        return new(
            width, T.Zero, T.Zero, T.Zero,
            T.Zero, height, T.Zero, T.Zero,
            T.Zero, T.Zero, range, -range * nearPlaneDistance,
            T.Zero, T.Zero, -T.One, T.Zero);
    }

    public static Matrix4x4<T> CreatePerspectiveOffCenter<T>(T left, T right, T bottom, T top, T nearPlaneDistance, T farPlaneDistance)
        where T : struct, INumber<T>
    {
        var two = T.One + T.One;
        var width = right - left;
        var height = top - bottom;
        var range = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
        return new(
            two * nearPlaneDistance / width, T.Zero, (right + left) / width, T.Zero,
            T.Zero, two * nearPlaneDistance / height, (top + bottom) / height, T.Zero,
            T.Zero, T.Zero, range, -range * nearPlaneDistance,
            T.Zero, T.Zero, -T.One, T.Zero);
    }

    public static Matrix4x4<T> CreateOrthographic<T>(T width, T height, T zNearPlane, T zFarPlane)
        where T : struct, INumber<T>
    {
        var range = T.One / (zNearPlane - zFarPlane);
        return new(
            T.One / width, T.Zero, T.Zero, T.Zero,
            T.Zero, T.One / height, T.Zero, T.Zero,
            T.Zero, T.Zero, range, range * zNearPlane,
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> CreateOrthographicOffCenter<T>(T left, T right, T bottom, T top, T zNearPlane, T zFarPlane)
        where T : struct, INumber<T>
    {
        var two = T.One + T.One;
        var width = right - left;
        var height = top - bottom;
        var range = T.One / (zNearPlane - zFarPlane);
        return new(
            two / width, T.Zero, T.Zero, (right + left) / width,
            T.Zero, two / height, T.Zero, (top + bottom) / height,
            T.Zero, T.Zero, range, range * zNearPlane,
            T.Zero, T.Zero, T.Zero, T.One);
    }


    public static Matrix4x4<T> Lerp<T>(Matrix4x4<T> from, Matrix4x4<T> to, T factor)
        where T : struct, INumber<T>
        => new(
            from.M00 + (to.M00 - from.M00) * factor,
            from.M01 + (to.M01 - from.M01) * factor,
            from.M02 + (to.M02 - from.M02) * factor,
            from.M03 + (to.M03 - from.M03) * factor,
            from.M10 + (to.M10 - from.M10) * factor,
            from.M11 + (to.M11 - from.M11) * factor,
            from.M12 + (to.M12 - from.M12) * factor,
            from.M13 + (to.M13 - from.M13) * factor,
            from.M20 + (to.M20 - from.M20) * factor,
            from.M21 + (to.M21 - from.M21) * factor,
            from.M22 + (to.M22 - from.M22) * factor,
            from.M23 + (to.M23 - from.M23) * factor,
            from.M30 + (to.M30 - from.M30) * factor,
            from.M31 + (to.M31 - from.M31) * factor,
            from.M32 + (to.M32 - from.M32) * factor,
            from.M33 + (to.M33 - from.M33) * factor);

    public static Matrix4x4<T> Transpose<T>(Matrix4x4<T> matrix)
        where T : struct, IFloatingPoint<T>
        => new(
            matrix.M00, matrix.M10, matrix.M20, matrix.M30,
            matrix.M01, matrix.M11, matrix.M21, matrix.M31,
            matrix.M02, matrix.M12, matrix.M22, matrix.M32,
            matrix.M03, matrix.M13, matrix.M23, matrix.M33);

    public static Matrix4x4<T> Invert<T>(Matrix4x4<T> matrix)
        where T : struct, IFloatingPointIeee754<T>
    {
        if (IsIdentity(matrix))
            return Matrix4x4<T>.Identity;

        var det = Determinant(matrix);
        if (T.Abs(det) < T.Epsilon)
            throw new InvalidOperationException("Matrix is not invertible.");

        var invDet = T.One / det;

        return new(
            invDet * (matrix.M22 * matrix.M33 * matrix.M44 + matrix.M23 * matrix.M34 * matrix.M42 + matrix.M24 * matrix.M32 * matrix.M43 - matrix.M22 * matrix.M34 * matrix.M43 - matrix.M23 * matrix.M32 * matrix.M44 - matrix.M24 * matrix.M33 * matrix.M42),
            invDet * (matrix.M12 * matrix.M34 * matrix.M43 + matrix.M13 * matrix.M32 * matrix.M44 + matrix.M14 * matrix.M33 * matrix.M42 - matrix.M12 * matrix.M33 * matrix.M44 - matrix.M13 * matrix.M34 * matrix.M42 - matrix.M14 * matrix.M32 * matrix.M43),
            invDet * (matrix.M12 * matrix.M23 * matrix.M44 + matrix.M13 * matrix.M24 * matrix.M42 + matrix.M14 * matrix.M22 * matrix.M43 - matrix.M12 * matrix.M24 * matrix.M43 - matrix.M13 * matrix.M22 * matrix.M44 - matrix.M14 * matrix.M23 * matrix.M42),
            invDet * (matrix.M12 * matrix.M24 * matrix.M33 + matrix.M13 * matrix.M22 * matrix.M34 + matrix.M14 * matrix.M23 * matrix.M32 - matrix.M12 * matrix.M23 * matrix.M34 - matrix.M13 * matrix.M24 * matrix.M32 - matrix.M14 * matrix.M22 * matrix.M33),

            invDet * (matrix.M21 * matrix.M34 * matrix.M43 + matrix.M23 * matrix.M31 * matrix.M44 + matrix.M24 * matrix.M33 * matrix.M41 - matrix.M21 * matrix.M33 * matrix.M44 - matrix.M23 * matrix.M34 * matrix.M41 - matrix.M24 * matrix.M31 * matrix.M43),
            invDet * (matrix.M11 * matrix.M33 * matrix.M44 + matrix.M13 * matrix.M34 * matrix.M41 + matrix.M14 * matrix.M31 * matrix.M43 - matrix.M11 * matrix.M34 * matrix.M43 - matrix.M13 * matrix.M31 * matrix.M44 - matrix.M14 * matrix.M33 * matrix.M41),
            invDet * (matrix.M11 * matrix.M24 * matrix.M43 + matrix.M13 * matrix.M21 * matrix.M44 + matrix.M14 * matrix.M23 * matrix.M41 - matrix.M11 * matrix.M23 * matrix.M44 - matrix.M13 * matrix.M24 * matrix.M41 - matrix.M14 * matrix.M21 * matrix.M43),
            invDet * (matrix.M11 * matrix.M23 * matrix.M34 + matrix.M13 * matrix.M24 * matrix.M31 + matrix.M14 * matrix.M21 * matrix.M33 - matrix.M11 * matrix.M24 * matrix.M33 - matrix.M13 * matrix.M21 * matrix.M34 - matrix.M14 * matrix.M23 * matrix.M31),

            invDet * (matrix.M21 * matrix.M32 * matrix.M44 + matrix.M22 * matrix.M34 * matrix.M41 + matrix.M24 * matrix.M31 * matrix.M42 - matrix.M21 * matrix.M34 * matrix.M42 - matrix.M22 * matrix.M31 * matrix.M44 - matrix.M24 * matrix.M32 * matrix.M41),
            invDet * (matrix.M11 * matrix.M34 * matrix.M42 + matrix.M12 * matrix.M31 * matrix.M44 + matrix.M14 * matrix.M32 * matrix.M41 - matrix.M11 * matrix.M32 * matrix.M44 - matrix.M12 * matrix.M34 * matrix.M41 - matrix.M14 * matrix.M31 * matrix.M42),
            invDet * (matrix.M11 * matrix.M22 * matrix.M44 + matrix.M12 * matrix.M24 * matrix.M41 + matrix.M14 * matrix.M21 * matrix.M42 - matrix.M11 * matrix.M24 * matrix.M42 - matrix.M12 * matrix.M21 * matrix.M44 - matrix.M14 * matrix.M22 * matrix.M41),
            invDet * (matrix.M11 * matrix.M24 * matrix.M32 + matrix.M12 * matrix.M21 * matrix.M34 + matrix.M14 * matrix.M22 * matrix.M31 - matrix.M11 * matrix.M22 * matrix.M34 - matrix.M12 * matrix.M24 * matrix.M31 - matrix.M14 * matrix.M21 * matrix.M32),

            invDet * (matrix.M21 * matrix.M33 * matrix.M42 + matrix.M22 * matrix.M31 * matrix.M43 + matrix.M23 * matrix.M32 * matrix.M41 - matrix.M21 * matrix.M32 * matrix.M43 - matrix.M22 * matrix.M33 * matrix.M41 - matrix.M23 * matrix.M31 * matrix.M42),
            invDet * (matrix.M11 * matrix.M32 * matrix.M43 + matrix.M12 * matrix.M33 * matrix.M41 + matrix.M13 * matrix.M31 * matrix.M42 - matrix.M11 * matrix.M33 * matrix.M42 - matrix.M12 * matrix.M31 * matrix.M43 - matrix.M13 * matrix.M32 * matrix.M41),
            invDet * (matrix.M11 * matrix.M23 * matrix.M42 + matrix.M12 * matrix.M21 * matrix.M43 + matrix.M13 * matrix.M22 * matrix.M41 - matrix.M11 * matrix.M22 * matrix.M43 - matrix.M12 * matrix.M23 * matrix.M41 - matrix.M13 * matrix.M21 * matrix.M42),
            invDet * (matrix.M11 * matrix.M22 * matrix.M33 + matrix.M12 * matrix.M23 * matrix.M31 + matrix.M13 * matrix.M21 * matrix.M32 - matrix.M11 * matrix.M23 * matrix.M32 - matrix.M12 * matrix.M21 * matrix.M33 - matrix.M13 * matrix.M22 * matrix.M31)
        );
    }

    public static T Determinant<T>(Matrix4x4<T> matrix)
        where T : struct, INumber<T>
    {
        var a = matrix.M00;
        var b = matrix.M01;
        var c = matrix.M02;
        var d = matrix.M03;
        var e = matrix.M10;
        var f = matrix.M11;
        var g = matrix.M12;
        var h = matrix.M13;
        var i = matrix.M20;
        var j = matrix.M21;
        var k = matrix.M22;
        var l = matrix.M23;
        var m = matrix.M30;
        var n = matrix.M31;
        var o = matrix.M32;
        var p = matrix.M33;

        // Calculate the determinant using the matrix elements
        var det1 = a * (f * (k * p - l * o) - g * (j * p - l * n) + h * (j * o - k * n));
        var det2 = b * (e * (k * p - l * o) - g * (i * p - l * m) + h * (i * o - k * m));
        var det3 = c * (e * (j * p - l * n) - f * (i * p - l * m) + h * (i * n - j * m));
        var det4 = d * (e * (j * o - k * n) - f * (i * o - k * m) + g * (i * n - j * m));

        return det1 - det2 + det3 - det4;
    }


}

