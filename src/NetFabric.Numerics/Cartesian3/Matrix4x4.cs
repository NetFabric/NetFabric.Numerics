namespace NetFabric.Numerics.Cartesian3;

public readonly record struct Matrix4x4<T>(
    T M00, T M01, T M02, T M03,
    T M10, T M11, T M12, T M13,
    T M20, T M21, T M22, T M23,
    T M30, T M31, T M32, T M33)
    where T : struct, IFloatingPoint<T>
{
    #region constants

    public static readonly Matrix4x4<T> Zero = new(
        T.Zero, T.Zero, T.Zero, T.Zero,
        T.Zero, T.Zero, T.Zero, T.Zero,
        T.Zero, T.Zero, T.Zero, T.Zero,
        T.Zero, T.Zero, T.Zero, T.Zero);

    public static readonly Matrix4x4<T> Identity = new(
        T.One, T.Zero, T.Zero, T.Zero,
        T.Zero, T.One, T.Zero, T.Zero,
        T.Zero, T.Zero, T.One, T.Zero,
        T.Zero, T.Zero, T.Zero, T.One);

    #endregion

    #region operators

    public static Matrix4x4<T> operator +(Matrix4x4<T> left, Matrix4x4<T> right) => Add(left, right);

    public static Matrix4x4<T> operator -(Matrix4x4<T> left, Matrix4x4<T> right) => Subtract(left, right);

    public static Matrix4x4<T> operator *(Matrix4x4<T> left, Matrix4x4<T> right) => Multiply(left, right);

    public static Matrix4x4<T> operator *(Matrix4x4<T> matrix, T scalar) => Multiply(matrix, scalar);

    public static Matrix4x4<T> operator *(T scalar, Matrix4x4<T> matrix) => Multiply(matrix, scalar);

    public static Matrix4x4<T> operator /(Matrix4x4<T> matrix, T scalar) => Divide(matrix, scalar);

    #endregion

    #region arithmetic

    public static Matrix4x4<T> Add(Matrix4x4<T> left, Matrix4x4<T> right) => new(
        left.M00 + right.M00, left.M01 + right.M01, left.M02 + right.M02, left.M03 + right.M03,
        left.M10 + right.M10, left.M11 + right.M11, left.M12 + right.M12, left.M13 + right.M13,
        left.M20 + right.M20, left.M21 + right.M21, left.M22 + right.M22, left.M23 + right.M23,
        left.M30 + right.M30, left.M31 + right.M31, left.M32 + right.M32, left.M33 + right.M33);

    public static Matrix4x4<T> Subtract(Matrix4x4<T> left, Matrix4x4<T> right) => new(
        left.M00 - right.M00, left.M01 - right.M01, left.M02 - right.M02, left.M03 - right.M03,
        left.M10 - right.M10, left.M11 - right.M11, left.M12 - right.M12, left.M13 - right.M13,
        left.M20 - right.M20, left.M21 - right.M21, left.M22 - right.M22, left.M23 - right.M23,
        left.M30 - right.M30, left.M31 - right.M31, left.M32 - right.M32, left.M33 - right.M33);

    public static Matrix4x4<T> Multiply(Matrix4x4<T> left, Matrix4x4<T> right) => new(
        (left.M00 * right.M00) + (left.M01 * right.M10) + (left.M02 * right.M20) + (left.M03 * right.M30),
        (left.M00 * right.M01) + (left.M01 * right.M11) + (left.M02 * right.M21) + (left.M03 * right.M31),
        (left.M00 * right.M02) + (left.M01 * right.M12) + (left.M02 * right.M22) + (left.M03 * right.M32),
        (left.M00 * right.M03) + (left.M01 * right.M13) + (left.M02 * right.M23) + (left.M03 * right.M33),
        (left.M10 * right.M00) + (left.M11 * right.M10) + (left.M12 * right.M20) + (left.M13 * right.M30),
        (left.M10 * right.M01) + (left.M11 * right.M11) + (left.M12 * right.M21) + (left.M13 * right.M31),
        (left.M10 * right.M02) + (left.M11 * right.M12) + (left.M12 * right.M22) + (left.M13 * right.M32),
        (left.M10 * right.M03) + (left.M11 * right.M13) + (left.M12 * right.M23) + (left.M13 * right.M33),
        (left.M20 * right.M00) + (left.M21 * right.M10) + (left.M22 * right.M20) + (left.M23 * right.M30),
        (left.M20 * right.M01) + (left.M21 * right.M11) + (left.M22 * right.M21) + (left.M23 * right.M31),
        (left.M20 * right.M02) + (left.M21 * right.M12) + (left.M22 * right.M22) + (left.M23 * right.M32),
        (left.M20 * right.M03) + (left.M21 * right.M13) + (left.M22 * right.M23) + (left.M23 * right.M33),
        (left.M30 * right.M00) + (left.M31 * right.M10) + (left.M32 * right.M20) + (left.M33 * right.M30),
        (left.M30 * right.M01) + (left.M31 * right.M11) + (left.M32 * right.M21) + (left.M33 * right.M31),
        (left.M30 * right.M02) + (left.M31 * right.M12) + (left.M32 * right.M22) + (left.M33 * right.M32),
        (left.M30 * right.M03) + (left.M31 * right.M13) + (left.M32 * right.M23) + (left.M33 * right.M33));

    public static Matrix4x4<T> Multiply(Matrix4x4<T> matrix, T scalar) => new(
        matrix.M00 * scalar, matrix.M01 * scalar, matrix.M02 * scalar, matrix.M03 * scalar,
        matrix.M10 * scalar, matrix.M11 * scalar, matrix.M12 * scalar, matrix.M13 * scalar,
        matrix.M20 * scalar, matrix.M21 * scalar, matrix.M22 * scalar, matrix.M23 * scalar,
        matrix.M30 * scalar, matrix.M31 * scalar, matrix.M32 * scalar, matrix.M33 * scalar);

    public static Matrix4x4<T> Divide(Matrix4x4<T> matrix, T scalar) => new(
        matrix.M00 / scalar, matrix.M01 / scalar, matrix.M02 / scalar, matrix.M03 / scalar,
        matrix.M10 / scalar, matrix.M11 / scalar, matrix.M12 / scalar, matrix.M13 / scalar,
        matrix.M20 / scalar, matrix.M21 / scalar, matrix.M22 / scalar, matrix.M23 / scalar,
        matrix.M30 / scalar, matrix.M31 / scalar, matrix.M32 / scalar, matrix.M33 / scalar);

    public static Matrix4x4<T> Negate(Matrix4x4<T> matrix) => new(
        -matrix.M00, -matrix.M01, -matrix.M02, -matrix.M03,
        -matrix.M10, -matrix.M11, -matrix.M12, -matrix.M13,
        -matrix.M20, -matrix.M21, -matrix.M22, -matrix.M23,
        -matrix.M30, -matrix.M31, -matrix.M32, -matrix.M33);

    public static Matrix4x4<T> Transpose(Matrix4x4<T> matrix) => new(
        matrix.M00, matrix.M10, matrix.M20, matrix.M30,
        matrix.M01, matrix.M11, matrix.M21, matrix.M31,
        matrix.M02, matrix.M12, matrix.M22, matrix.M32,
        matrix.M03, matrix.M13, matrix.M23, matrix.M33);

    public static Matrix4x4<T> Lerp(Matrix4x4<T> left, Matrix4x4<T> right, T amount) => new(
        left.M00 + ((right.M00 - left.M00) * amount),
        left.M01 + ((right.M01 - left.M01) * amount),
        left.M02 + ((right.M02 - left.M02) * amount),
        left.M03 + ((right.M03 - left.M03) * amount),
        left.M10 + ((right.M10 - left.M10) * amount),
        left.M11 + ((right.M11 - left.M11) * amount),
        left.M12 + ((right.M12 - left.M12) * amount),
        left.M13 + ((right.M13 - left.M13) * amount),
        left.M20 + ((right.M20 - left.M20) * amount),
        left.M21 + ((right.M21 - left.M21) * amount),
        left.M22 + ((right.M22 - left.M22) * amount),
        left.M23 + ((right.M23 - left.M23) * amount),
        left.M30 + ((right.M30 - left.M30) * amount),
        left.M31 + ((right.M31 - left.M31) * amount),
        left.M32 + ((right.M32 - left.M32) * amount),
        left.M33 + ((right.M33 - left.M33) * amount));

    #endregion
}

public static class Matrix4x4
{
    public static Matrix4x4<T> CreateTranslation<T>(Point<T> position)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(
            T.One,  T.Zero, T.Zero, position.X,
            T.Zero, T.One,  T.Zero, position.Y,
            T.Zero, T.Zero, T.One,  position.Z,
            T.Zero, T.Zero, T.Zero, T.One);

    public static Matrix4x4<T> CreateScale<T>(Point<T> scale)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(
            scale.X, T.Zero,  T.Zero,  T.Zero,
            T.Zero,  scale.Y, T.Zero,  T.Zero,
            T.Zero,  T.Zero,  scale.Z, T.Zero,
            T.Zero,  T.Zero,  T.Zero,  T.One);

    public static Matrix4x4<T> CreateRotationX<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var (sin, cos) = Angle.SinCos(angle);
        return new(
            T.One,  T.Zero, T.Zero, T.Zero,
            T.Zero, cos,    sin,    T.Zero,
            T.Zero, -sin,   cos,    T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> CreateRotationY<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var (sin, cos) = Angle.SinCos(angle);
        return new(
            cos,    T.Zero, -sin,   T.Zero,
            T.Zero, T.One,  T.Zero, T.Zero,
            sin,    T.Zero, cos,    T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> CreateRotationZ<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var (sin, cos) = Angle.SinCos(angle);
        return new(
            cos,    sin,    T.Zero, T.Zero,
            -sin,   cos,    T.Zero, T.Zero,
            T.Zero, T.Zero, T.One,  T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> CreateRotation<T>(Vector<T> axis, Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var (sin, cos) = Angle.SinCos(angle);
        var (x, y, z) = axis;
        var oneMinusCos = T.One - cos;
        var xy = x * y;
        var xz = x * z;
        var yz = y * z;
        return new(
            cos + (x * x * oneMinusCos),    (xy * oneMinusCos) + (z * sin), (xz * oneMinusCos) - (y * sin), T.Zero,
            (xy * oneMinusCos) - (z * sin), cos + (y * y * oneMinusCos),    (yz * oneMinusCos) + (x * sin), T.Zero,
            (xz * oneMinusCos) + (y * sin), (yz * oneMinusCos) - (x * sin), cos + (z * z * oneMinusCos),    T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> CreateFromQuaternion<T>(Quaternion<T> quaternion)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
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
            T.One - (two * (yy + zz)), two * (xy + zw), two * (zx - yw), T.Zero,
            two * (xy - zw), T.One - (two * (zz + xx)), two * (yz + xw), T.Zero,
            two * (zx + yw), two * (yz - xw), T.One - (two * (yy + xx)), T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> CreateLookAt<T>(Point<T> cameraPosition, Point<T> cameraTarget, Vector<T> cameraUpVector)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, IRootFunctions<T>
    {
        var z = Vector.Normalize(cameraPosition - cameraTarget);
        var x = Vector.Normalize(Vector.CrossProduct(cameraUpVector, z));
        var y = Vector.CrossProduct(z, x);
        return new(
            x.X,    x.Y,    x.Z,    -Point.DotProduct(cameraPosition, x),
            y.X,    y.Y,    y.Z,    -Point.DotProduct(cameraPosition, y),
            z.X,    z.Y,    z.Z,    -Point.DotProduct(cameraPosition, z),
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> CreatePerspectiveFieldOfView<T>(Angle<Radians, T> fieldOfView, T aspectRatio, T nearPlaneDistance, T farPlaneDistance)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var two = T.One + T.One;
        var (sin, cos) = Angle.SinCos(fieldOfView / two);
        var height = cos / sin;
        var width = height / aspectRatio;
        var range = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
        return new(
            width,  T.Zero, T.Zero, T.Zero,
            T.Zero, height, T.Zero, T.Zero,
            T.Zero, T.Zero, range,  -range * nearPlaneDistance,
            T.Zero, T.Zero, -T.One, T.Zero);
    }

    public static Matrix4x4<T> CreatePerspective<T>(T width, T height, T nearPlaneDistance, T farPlaneDistance)
        where T : struct, IFloatingPoint<T>
    {
        var range = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
        return new(
            width,  T.Zero, T.Zero, T.Zero,
            T.Zero, height, T.Zero, T.Zero,
            T.Zero, T.Zero, range,  -range * nearPlaneDistance,
            T.Zero, T.Zero, -T.One, T.Zero);
    }

    public static Matrix4x4<T> CreatePerspectiveOffCenter<T>(T left, T right, T bottom, T top, T nearPlaneDistance, T farPlaneDistance)
        where T : struct, IFloatingPoint<T>
    {
        var two = T.One + T.One;
        var width = right - left;
        var height = top - bottom;
        var range = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
        return new(
            two * nearPlaneDistance / width, T.Zero,                            (right + left) / width,    T.Zero,
            T.Zero,                          two * nearPlaneDistance / height, (top + bottom) / height,    T.Zero,
            T.Zero,                          T.Zero,                           range,                      -range * nearPlaneDistance,
            T.Zero,                          T.Zero,                           -T.One,                      T.Zero);
    }

    public static Matrix4x4<T> CreateOrthographic<T>(T width, T height, T zNearPlane, T zFarPlane)
        where T : struct, IFloatingPoint<T>
    {
        var range = T.One / (zNearPlane - zFarPlane);
        return new(
            T.One / width, T.Zero,       T.Zero,       T.Zero,
            T.Zero,        T.One / height, T.Zero,       T.Zero,
            T.Zero,        T.Zero,       range,        range * zNearPlane,
            T.Zero,        T.Zero,       T.Zero,       T.One);
    }

    public static Matrix4x4<T> CreateOrthographicOffCenter<T>(T left, T right, T bottom, T top, T zNearPlane, T zFarPlane)
        where T : struct, IFloatingPoint<T>
    {
        var two = T.One + T.One;
        var width = right - left;
        var height = top - bottom;
        var range = T.One / (zNearPlane - zFarPlane);
        return new(
            two / width, T.Zero,       T.Zero,  (right + left) / width,
            T.Zero,       two / height, T.Zero, (top + bottom) / height,
            T.Zero,       T.Zero,       range,  range * zNearPlane,
            T.Zero,       T.Zero,       T.Zero, T.One);
    }
}

