using NetFabric.Numerics.Rectangular3D;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace NetFabric.Numerics;

/// <summary>
/// Represents a 4x4 matrix of elements of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the matrix elements.</typeparam>
/// <remarks>
/// This struct provides a representation of a 4x4 matrix with four rows and four columns.
/// The matrix is stored using four <see cref="Vector4{T}"/> instances representing the
/// rows of the matrix.
/// </remarks>
[SkipLocalsInit]
public readonly struct Matrix4x4<T>
    : IMatrix<Matrix4x4<T>, T>
    where T : struct, INumber<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Gets the X coordinate. This field is read-only.
    /// </summary>
    public readonly Vector4<T> X;

    /// <summary>
    /// Gets the Y coordinate. This field is read-only.
    /// </summary>
    public readonly Vector4<T> Y;

    /// <summary>
    /// Gets the Z coordinate. This field is read-only.
    /// </summary>
    public readonly Vector4<T> Z;

    /// <summary>
    /// Gets the W coordinate. This field is read-only.
    /// </summary>
    public readonly Vector4<T> W;

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector4{T}"/> struct.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="z">The Z coordinate.</param>
    /// <param name="w">The W coordinate.</param>
    public Matrix4x4(Vector4<T> x, Vector4<T> y, Vector4<T> z, Vector4<T> w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="matrix"/>.</typeparam>
    /// <param name="matrix">The value which is used to create the instance of <see cref="Matrix4x4{T}"/></param>
    /// <returns>An instance of <see cref="Matrix4x4{T}"/> created from <paramref name="matrix" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="matrix" /> is not representable by <see cref="Matrix4x4{T}"/>.</exception>
    public static Matrix4x4<T> CreateChecked<TOther>(ref readonly Matrix4x4<TOther> matrix)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>
        => new(
            Vector4<T>.CreateChecked(in matrix.X),
            Vector4<T>.CreateChecked(in matrix.Y),
            Vector4<T>.CreateChecked(in matrix.Z),
            Vector4<T>.CreateChecked(in matrix.W)
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
    public static Matrix4x4<T> CreateSaturating<TOther>(ref readonly Matrix4x4<TOther> matrix)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>
        => new(
            Vector4<T>.CreateSaturating(in matrix.X),
            Vector4<T>.CreateSaturating(in matrix.Y),
            Vector4<T>.CreateSaturating(in matrix.Z),
            Vector4<T>.CreateSaturating(in matrix.W)
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
    public static Matrix4x4<T> CreateTruncating<TOther>(ref readonly Matrix4x4<TOther> matrix)
        where TOther : struct, INumber<TOther>, IMinMaxValue<TOther>
        => new(
            Vector4<T>.CreateTruncating(in matrix.X),
            Vector4<T>.CreateTruncating(in matrix.Y),
            Vector4<T>.CreateTruncating(in matrix.Z),
            Vector4<T>.CreateTruncating(in matrix.W)
        );

    #region constants

    const int rowCount = 4;
    const int columnCount = 4;

    int IMatrix<Matrix4x4<T>, T>.RowCount 
        => rowCount;
    int IMatrix<Matrix4x4<T>, T>.ColumnCount 
        => columnCount;

    public static readonly Matrix4x4<T> Zero = new(Vector4<T>.Zero, Vector4<T>.Zero, Vector4<T>.Zero, Vector4<T>.Zero);

    static Matrix4x4<T> INumericBase<Matrix4x4<T>>.Zero
        => Zero;

    static Matrix4x4<T> IAdditiveIdentity<Matrix4x4<T>, Matrix4x4<T>>.AdditiveIdentity
        => Zero;

    public static readonly Matrix4x4<T> Identity = new(
        new(T.One, T.Zero, T.Zero, T.Zero),
        new(T.Zero, T.One, T.Zero, T.Zero),
        new(T.Zero, T.Zero, T.One, T.Zero),
        new(T.Zero, T.Zero, T.Zero, T.One));

    static Matrix4x4<T> IMatrix<Matrix4x4<T>, T>.Identity
        => Identity;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Matrix4x4<T> MinValue = new(Vector4<T>.MinValue, Vector4<T>.MinValue, Vector4<T>.MinValue, Vector4<T>.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Matrix4x4<T> MaxValue = new(Vector4<T>.MaxValue, Vector4<T>.MaxValue, Vector4<T>.MaxValue, Vector4<T>.MaxValue);

    static Matrix4x4<T> IMinMaxValue<Matrix4x4<T>>.MinValue
        => MinValue;
    static Matrix4x4<T> IMinMaxValue<Matrix4x4<T>>.MaxValue
        => MaxValue;

    #endregion

    #region operators

    public static Matrix4x4<T> operator +(Matrix4x4<T> right)
        => right;

    public static Matrix4x4<T> operator +(Matrix4x4<T> left, Matrix4x4<T> right)
        => Matrix4x4.Add(left, right);

    public static Matrix4x4<T> operator -(Matrix4x4<T> right)
        => new(-right.X, -right.Y, -right.Z, -right.W);

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

    #region equality

    /// <summary>
    /// Indicates whether two <see cref="Matrix4x4{T}"/> instances are equal.
    /// </summary>
    /// <param name="left">The first matrix to compare.</param>
    /// <param name="right">The second matrix to compare.</param>
    /// <returns>true if the two vectors are equal, false otherwise.</returns>
    /// <remarks>
    /// The method compares the numerical values of the <paramref name="left"/> and <paramref name="right"/> vectors to determine their equality.
    /// </remarks>
    public static bool operator ==(Matrix4x4<T> left, Matrix4x4<T> right)
        => left.Equals(right);

    /// <summary>
    /// Indicates whether two <see cref="Matrix4x4{T}"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first matrix to compare.</param>
    /// <param name="right">The second matrix to compare.</param>
    /// <returns>true if the two vectors are equal, false otherwise.returns>
    /// <remarks>
    /// The method compares the numerical values of the <paramref name="left"/> and <paramref name="right"/> vectors to determine their equality.
    /// </remarks>
    public static bool operator !=(Matrix4x4<T> left, Matrix4x4<T> right)
        => !left.Equals(right);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        => HashCode.Combine(X, Y, Z, W);

    /// <summary>
    /// Determines whether the current matrix is equal to another matrix.
    /// </summary>
    /// <param name="other">The matrix to compare with the current matrix.</param>
    /// <returns><c>true</c> if the current matrix is equal to the other matrix; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Matrix4x4<T> other)
        => X.Equals(other.X) &&
            Y.Equals(other.Y) &&
            Z.Equals(other.Z) &&
            W.Equals(other.W);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj switch
        {
            Matrix4x4<T> matrix => Equals(matrix),
            _ => false
        };

    #endregion

    /// <summary>
    /// Gets the element at the specified row and column ref readonly the <see cref="Matrix4x4{T}"/>.
    /// </summary>
    /// <param name="row">The zero-based row index.</param>
    /// <param name="column">The zero-based column index.</param>
    /// <returns>The element at the specified position.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="row"/> or <paramref name="column"/> is less than 0 or greater than or equal to 4.
    /// </exception>
    public T this[int row, int column]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (uint)row >= rowCount
            ? Throw.ArgumentOutOfRangeException<T>(nameof(row), row)
            : Unsafe.Add(ref Unsafe.AsRef(in X), row)[column];
    }

    /// <summary>
    /// Deconstructs the matrix into its individual components.
    /// </summary>
    /// <param name="X">The output parameter to store the X component of the matrix.</param>
    /// <param name="Y">The output parameter to store the Y component of the matrix.</param>
    /// <param name="Z">The output parameter to store the Z component of the matrix.</param>
    /// <param name="W">The output parameter to store the W component of the matrix.</param>
    public void Deconstruct(out Vector4<T> X, out Vector4<T> Y, out Vector4<T> Z, out Vector4<T> W)
    {
        X = this.X;
        Y = this.Y;
        Z = this.Z;
        W = this.W;
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
        return $"<{X.ToString(format, formatProvider)}{separator} {Y.ToString(format, formatProvider)}{separator} {Z.ToString(format, formatProvider)}{separator} {W.ToString(format, formatProvider)}>";
    }

}

/// <summary>
/// Provides static methods for matrix operations.
/// </summary>
public static class Matrix4x4
{

    #region arithmetic

    /// <summary>
    /// Negates each component of the specified <see cref="Matrix4x4{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the matrix's components.</typeparam>
    /// <param name="matrix">The <see cref="Matrix4x4{T}"/> to negate.</param>
    /// <returns>A new <see cref="Matrix4x4{T}"/> with negated components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Matrix4x4<T> Negate<T>(ref readonly Matrix4x4<T> matrix)
        where T : struct, INumber<T>, IMinMaxValue<T>, ISignedNumber<T>
        => new(-matrix.X, -matrix.Y, -matrix.Z, -matrix.W);

    /// <summary>
    /// Adds two matrices component-wise.
    /// </summary>
    /// <typeparam name="T">The type of the matrix's components.</typeparam>
    /// <param name="left">The first <see cref="Matrix4x4{T}"/> to add.</param>
    /// <param name="right">The second <see cref="Matrix4x4{T}"/> to add.</param>
    /// <returns>A new <see cref="Matrix4x4{T}"/> representing the element-wise addition of the two matrices.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Matrix4x4<T> Add<T>(ref readonly Matrix4x4<T> left, ref readonly Matrix4x4<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(
            left.X + right.X,
            left.Y + right.Y,
            left.Z + right.Z,
            left.W + right.W);

    /// <summary>
    /// Subtracts two matrices component-wise.
    /// </summary>
    /// <typeparam name="T">The type of the matrix's components.</typeparam>
    /// <param name="left">The <see cref="Matrix4x4{T}"/> to subtract from.</param>
    /// <param name="right">The <see cref="Matrix4x4{T}"/> to subtract.</param>
    /// <returns>A new <see cref="Matrix4x4{T}"/> representing the element-wise subtraction of the two matrices.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Matrix4x4<T> Subtract<T>(ref readonly Matrix4x4<T> left, ref readonly Matrix4x4<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(
            left.X - right.X,
            left.Y - right.Y,
            left.Z - right.Z,
            left.W - right.W);

    /// <summary>
    /// Multiplies two matrices.
    /// </summary>
    /// <typeparam name="T">The type of the matrix's components.</typeparam>
    /// <param name="left">The first <see cref="Matrix4x4{T}"/> to multiply.</param>
    /// <param name="right">The second <see cref="Matrix4x4{T}"/> to multiply.</param>
    /// <returns>A new <see cref="Matrix4x4{T}"/> representing the result of the matrix multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Matrix4x4<T> Multiply<T>(ref readonly Matrix4x4<T> left, ref readonly Matrix4x4<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(
            (right.X * left.X.X) + (right.Y * left.X.Y) + (right.Z * left.X.Z) + (right.W * left.X.W),
            (right.X * left.Y.X) + (right.Y * left.Y.Y) + (right.Z * left.Y.Z) + (right.W * left.Y.W),
            (right.X * left.Z.X) + (right.Y * left.Z.Y) + (right.Z * left.Z.Z) + (right.W * left.Z.W),
            (right.X * left.W.X) + (right.Y * left.W.Y) + (right.Z * left.W.Z) + (right.W * left.W.W));

    /// <summary>
    /// Multiplies a matrix by a scalar value.
    /// </summary>
    /// <typeparam name="T">The type of the matrix's components.</typeparam>
    /// <param name="matrix">The <see cref="Matrix4x4{T}"/> to multiply.</param>
    /// <param name="scalar">The scalar value to multiply the matrix by.</param>
    /// <returns>A new <see cref="Matrix4x4{T}"/> representing the result of the matrix multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Matrix4x4<T> Multiply<T>(ref readonly Matrix4x4<T> matrix, T scalar)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(matrix.X * scalar, matrix.Y * scalar, matrix.Z * scalar, matrix.W * scalar);

    /// <summary>
    /// Divides a matrix by a scalar value.
    /// </summary>
    /// <typeparam name="T">The type of the matrix's components.</typeparam>
    /// <param name="matrix">The <see cref="Matrix4x4{T}"/> to divide.</param>
    /// <param name="scalar">The scalar value to divide the matrix by.</param>
    /// <returns>A new <see cref="Matrix4x4{T}"/> representing the result of the matrix division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Matrix4x4<T> Divide<T>(ref readonly Matrix4x4<T> matrix, T scalar)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(matrix.X / scalar, matrix.Y / scalar, matrix.Z / scalar, matrix.W / scalar);

    #endregion

    /// <summary>
    /// Determines whether the specified <see cref="Matrix4x4{T}"/> is an identity matrix.
    /// </summary>
    /// <typeparam name="T">The type of the matrix's components.</typeparam>
    /// <param name="matrix">The <see cref="Matrix4x4{T}"/> to check for identity.</param>
    /// <returns>
    /// <c>true</c> if the matrix is an identity matrix; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsIdentity<T>(ref readonly Matrix4x4<T> matrix)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => matrix == Matrix4x4<T>.Identity;

    /// <summary>
    /// Determines whether the specified <see cref="Matrix4x4{T}"/> is a zero matrix.
    /// </summary>
    /// <typeparam name="T">The type of the matrix's components.</typeparam>
    /// <param name="matrix">The <see cref="Matrix4x4{T}"/> to check for zero.</param>
    /// <returns>
    /// <c>true</c> if the matrix is a zero matrix; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsZero<T>(ref readonly Matrix4x4<T> matrix)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => matrix == Matrix4x4<T>.Zero;

    /// <summary>
    /// Determines whether any component of the specified <see cref="Matrix4x4{T}"/> is NaN (Not-a-Number).
    /// </summary>
    /// <typeparam name="T">The type of the matrix's components.</typeparam>
    /// <param name="matrix">The <see cref="Matrix4x4{T}"/> to check for NaN values.</param>
    /// <returns>
    /// <c>true</c> if any component of the matrix is NaN; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNaN<T>(ref readonly Matrix4x4<T> matrix)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Vector4.IsNaN(matrix.X) || Vector4.IsNaN(matrix.Y) || Vector4.IsNaN(matrix.Z) || Vector4.IsNaN(matrix.W);

    /// <summary>
    /// Determines whether any component of the specified <see cref="Matrix4x4{T}"/> is positive or negative infinity.
    /// </summary>
    /// <typeparam name="T">The type of the matrix's components.</typeparam>
    /// <param name="matrix">The <see cref="Matrix4x4{T}"/> to check for infinity values.</param>
    /// <returns>
    /// <c>true</c> if any component of the matrix is positive or negative infinity; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsInfinity<T>(ref readonly Matrix4x4<T> matrix)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Vector4.IsInfinity(matrix.X) || Vector4.IsInfinity(matrix.Y) || Vector4.IsInfinity(matrix.Z) || Vector4.IsInfinity(matrix.W);

    /// <summary>
    /// Determines whether all components of the specified <see cref="Matrix4x4{T}"/> are finite numbers (not NaN, infinity, or negative infinity).
    /// </summary>
    /// <typeparam name="T">The type of the matrix's components.</typeparam>
    /// <param name="matrix">The <see cref="Matrix4x4{T}"/> to check for finite values.</param>
    /// <returns>
    /// <c>true</c> if all components of the matrix are finite numbers; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsFinite<T>(ref readonly Matrix4x4<T> matrix)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Vector4.IsFinite(matrix.X) && Vector4.IsFinite(matrix.Y) && Vector4.IsFinite(matrix.Z) && Vector4.IsFinite(matrix.W);

    ///// <summary>
    ///// Determines whether a matrix is orthogonal.
    ///// </summary>
    ///// <typeparam name="T">The type of the matrix's components.</typeparam>
    ///// <param name="matrix">The <see cref="Matrix4x4{T}"/> to check.</param>
    ///// <returns><c>true</c> if the matrix is orthogonal; otherwise, <c>false</c>.</returns>
    ///// <remarks>
    /////   <para>An orthogonal matrix is a square matrix whose transpose is equal to its inverse.</para>
    /////   <para>In an orthogonal matrix, the dot product of any two distinct rows or columns is zero, and the dot product of each row or column with itself is equal to 1.</para>
    ///// </remarks>
    //public static bool IsOrthogonal<T>(Matrix4x4<T> matrix)
    //    where T : struct, IFloatingPointIeee754<T>, IMinMaxValue<T>
    //    => Matrix4x4.Transpose(matrix) == Matrix4x4.Invert(matrix);

    /// <summary>
    /// Creates a translation matrix that represents a translation along the X, Y, and Z axes using the provided translation offsets.
    /// </summary>
    /// <typeparam name="T">The type of the matrix elements.</typeparam>
    /// <param name="x">The translation offset along the X-axis.</param>
    /// <param name="y">The translation offset along the Y-axis.</param>
    /// <param name="z">The translation offset along the Z-axis.</param>
    /// <returns>A translation matrix.</returns>
    /// <remarks>
    /// The resulting translation matrix is represented by the following form:
    ///     [ 1  0  0  X ]
    ///     [ 0  1  0  Y ]
    ///     [ 0  0  1  Z ]
    ///     [ 0  0  0  1 ]
    /// The translation offsets along the X, Y, and Z axes are set based on the provided parameters.
    /// </remarks>
    public static Matrix4x4<T> CreateTranslation<T>(T x, T y, T z)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(
            new(T.One,  T.Zero, T.Zero, x),
            new(T.Zero, T.One,  T.Zero, y),
            new(T.Zero, T.Zero, T.One,  z),
            new(T.Zero, T.Zero, T.Zero, T.One));

    /// <summary>
    /// Creates a scaling matrix that represents scaling along the X, Y, and Z axes using the provided scale factors.
    /// </summary>
    /// <typeparam name="T">The type of the matrix elements.</typeparam>
    /// <param name="x">The scale factor along the X-axis.</param>
    /// <param name="y">The scale factor along the Y-axis.</param>
    /// <param name="z">The scale factor along the Z-axis.</param>
    /// <returns>A scaling matrix.</returns>
    /// <remarks>
    /// The resulting scaling matrix is represented by the following form:
    ///     [ X  0  0  0 ]
    ///     [ 0  Y  0  0 ]
    ///     [ 0  0  Z  0 ]
    ///     [ 0  0  0  1 ]
    /// The scale factors along the X, Y, and Z axes are set based on the provided parameters.
    /// </remarks>
    public static Matrix4x4<T> CreateScale<T>(T x, T y, T z)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(
            new(x,      T.Zero, T.Zero, T.Zero),
            new(T.Zero, y,      T.Zero, T.Zero),
            new(T.Zero, T.Zero, z,      T.Zero),
            new(T.Zero, T.Zero, T.Zero, T.One));

    /// <summary>
    /// Creates a rotation matrix that represents a rotation around the X-axis by the specified angle.
    /// </summary>
    /// <typeparam name="T">The type of the matrix elements.</typeparam>
    /// <param name="angle">The angle of rotation around the X-axis.</param>
    /// <returns>A rotation matrix.</returns>
    /// <remarks>
    /// The resulting rotation matrix is represented by the following form:
    ///     [  1     0      0      0  ]
    ///     [  0  cos(a)   sin(a)  0  ]
    ///     [  0  -sin(a)  cos(a)  0  ]
    ///     [  0     0      0      1  ]
    /// The angle of rotation around the X-axis is specified by the provided <paramref name="angle"/> parameter.
    /// </remarks>
    public static Matrix4x4<T> CreateRotationX<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var (sin, cos) = Angle.SinCos(angle);
        return new(
            new(T.One,  T.Zero, T.Zero, T.Zero),
            new(T.Zero, cos,    sin,    T.Zero),
            new(T.Zero, -sin,   cos,    T.Zero),
            new(T.Zero, T.Zero, T.Zero, T.One));
    }

    /// <summary>
    /// Creates a rotation matrix that represents a rotation around the Y-axis by the specified angle.
    /// </summary>
    /// <typeparam name="T">The type of the matrix elements.</typeparam>
    /// <param name="angle">The angle of rotation around the Y-axis.</param>
    /// <returns>A rotation matrix.</returns>
    /// <remarks>
    /// The resulting rotation matrix is represented by the following form:
    ///     [  cos(a)  0  -sin(a)  0  ]
    ///     [    0     1    0      0  ]
    ///     [  sin(a)  0  cos(a)   0  ]
    ///     [    0     0    0      1  ]
    /// The angle of rotation around the Y-axis is specified by the provided angle parameter.
    /// </remarks>
    public static Matrix4x4<T> CreateRotationY<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var (sin, cos) = Angle.SinCos(angle);
        return new(
            new(cos,    T.Zero, -sin,   T.Zero),
            new(T.Zero, T.One,  T.Zero, T.Zero),
            new(sin,    T.Zero, cos,    T.Zero),
            new(T.Zero, T.Zero, T.Zero, T.One));
    }

    /// <summary>
    /// Creates a rotation matrix that represents a rotation around the Z-axis by the specified angle.
    /// </summary>
    /// <typeparam name="T">The type of the matrix elements.</typeparam>
    /// <param name="angle">The angle of rotation around the Z-axis.</param>
    /// <returns>A rotation matrix.</returns>
    /// <remarks>
    /// The resulting rotation matrix is represented by the following form:
    ///     [  cos(a)   sin(a)  0  0  ]
    ///     [  -sin(a)  cos(a)  0  0  ]
    ///     [    0        0     1  0  ]
    ///     [    0        0     0  1  ]
    /// The angle of rotation around the Z-axis is specified by the provided angle parameter.
    /// </remarks>
    public static Matrix4x4<T> CreateRotationZ<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var (sin, cos) = Angle.SinCos(angle);
        return new(
            new(cos,    sin,    T.Zero, T.Zero),
            new(-sin,   cos,    T.Zero, T.Zero),
            new(T.Zero, T.Zero, T.One,  T.Zero),
            new(T.Zero, T.Zero, T.Zero, T.One));
    }

    /// <summary>
    /// Creates a rotation matrix that represents a rotation around the specified axis by the specified angle.
    /// </summary>
    /// <typeparam name="T">The type of the matrix elements.</typeparam>
    /// <param name="axis">The axis of rotation.</param>
    /// <param name="angle">The angle of rotation.</param>
    /// <returns>A rotation matrix.</returns>
    /// <remarks>
    /// The resulting rotation matrix is based on the Rodrigues' rotation formula and is computed as follows:
    ///     K = (axis.X, axis.Y, axis.Z)
    ///     c = cos(angle)
    ///     s = sin(angle)
    ///     v = 1 - c
    ///     R = c * I + v * K * K' + s * [0, -Kz, Ky; Kz, 0, -Kx; -Ky, Kx, 0]
    /// Where:
    ///     - K is a unit vector ref readonly the direction of the axis of rotation.
    ///     - I is the identity matrix.
    ///     - K' is the skew-symmetric matrix of K.
    ///     - c is the cosine of the angle of rotation.
    ///     - s is the sine of the angle of rotation.
    ///     - v is 1 minus the cosine of the angle of rotation.
    /// The resulting rotation matrix represents a rotation around the specified axis by the specified angle.
    /// </remarks>
    public static Matrix4x4<T> CreateRotation<T>(ref readonly Vector3<T> axis, Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var (sin, cos) = Angle.SinCos(angle);
        var (x, y, z) = axis;
        var oneMinusCos = T.One - cos;
        var xy = x * y;
        var xz = x * z;
        var yz = y * z;
        return new(
            new(cos + (x * x * oneMinusCos),    (xy * oneMinusCos) + (z * sin), (xz * oneMinusCos) - (y * sin), T.Zero),
            new((xy * oneMinusCos) - (z * sin), cos + (y * y * oneMinusCos),    (yz * oneMinusCos) + (x * sin), T.Zero),
            new((xz * oneMinusCos) + (y * sin), (yz * oneMinusCos) - (x * sin), cos + (z * z * oneMinusCos),    T.Zero),
            new(T.Zero,                         T.Zero,                         T.Zero,                         T.One));
    }

    /// <summary>
    /// Creates a matrix that represents a transformation from a quaternion rotation.
    /// </summary>
    /// <typeparam name="T">The type of the matrix and quaternion elements.</typeparam>
    /// <param name="quaternion">The quaternion representing the rotation.</param>
    /// <returns>A matrix representing the quaternion rotation.</returns>
    /// <remarks>
    /// The resulting matrix is computed as follows:
    ///     Q = (w, x, y, z)
    ///     xx = x * x, xy = x * y, xz = x * z, xw = x * w
    ///     yy = y * y, yz = y * z, yw = y * w
    ///     zz = z * z, zw = z * w
    ///     M = [1 - 2 * (yy + zz), 2 * (xy - zw), 2 * (xz + yw), 0;
    ///          2 * (xy + zw), 1 - 2 * (xx + zz), 2 * (yz - xw), 0;
    ///          2 * (xz - yw), 2 * (yz + xw), 1 - 2 * (xx + yy), 0;
    ///          0, 0, 0, 1]
    /// Where:
    ///     - Q is the quaternion representing the rotation.
    ///     - w, x, y, z are the components of the quaternion.
    /// The resulting matrix represents a transformation equivalent to the rotation described by the quaternion.
    /// </remarks>
    public static Matrix4x4<T> CreateFromQuaternion<T>(ref readonly Quaternion<T> quaternion)
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
            new(T.One - (two * (yy + zz)), two * (xy + zw),           two * (zx - yw),           T.Zero),
            new(two * (xy - zw),           T.One - (two * (zz + xx)), two * (yz + xw),           T.Zero),
            new(two * (zx + yw),           two * (yz - xw),           T.One - (two * (yy + xx)), T.Zero),
            new(T.Zero,                    T.Zero,                    T.Zero,                    T.One));
    }

    /// <summary>
    /// Creates a view matrix that simulates a camera's look-at transformation.
    /// </summary>
    /// <typeparam name="T">The type of the matrix and vector elements.</typeparam>
    /// <param name="cameraPosition">The position of the camera.</param>
    /// <param name="cameraTarget">The target point that the camera is looking at.</param>
    /// <param name="cameraUpVector">The up direction vector of the camera.</param>
    /// <returns>A matrix representing the camera's look-at transformation.</returns>
    /// <remarks>
    /// The resulting matrix is computed as follows:
    ///     zAxis = Normalize(cameraPosition - cameraTarget)
    ///     xAxis = Normalize(Cross(cameraUpVector, zAxis))
    ///     yAxis = Cross(zAxis, xAxis)
    ///     M = [xAxis.X, yAxis.X, zAxis.X, 0;
    ///          xAxis.Y, yAxis.Y, zAxis.Y, 0;
    ///          xAxis.Z, yAxis.Z, zAxis.Z, 0;
    ///          -Dot(xAxis, cameraPosition), -Dot(yAxis, cameraPosition), -Dot(zAxis, cameraPosition), 1]
    /// Where:
    ///     - cameraPosition is the position of the camera.
    ///     - cameraTarget is the target point that the camera is looking at.
    ///     - cameraUpVector is the up direction vector of the camera.
    /// The resulting matrix represents a view transformation that simulates the camera's position, orientation, and viewing direction.
    /// </remarks>
    public static Matrix4x4<T> CreateLookAt<T>(ref readonly Vector3<T> cameraPosition, ref readonly Vector3<T> cameraTarget, ref readonly Vector3<T> cameraUpVector)
        where T : struct, INumber<T>, IMinMaxValue<T>, IRootFunctions<T>
    {
        var z = Vector3.Normalize(cameraPosition - cameraTarget);
        var x = Vector3.Normalize(Vector3.Cross(cameraUpVector, z));
        var y = Vector3.Cross(z, x);
        return new(
            new(x.X,    x.Y,    x.Z,    -Vector3.Dot(cameraPosition, x)),
            new(y.X,    y.Y,    y.Z,    -Vector3.Dot(cameraPosition, y)),
            new(z.X,    z.Y,    z.Z,    -Vector3.Dot(cameraPosition, z)),
            new(T.Zero, T.Zero, T.Zero, T.One));
    }

    /// <summary>
    /// Creates a perspective projection matrix based on the field of view.
    /// </summary>
    /// <typeparam name="T">The type of the matrix elements.</typeparam>
    /// <param name="fieldOfView">The field of view angle ref readonly radians.</param>
    /// <param name="aspectRatio">The aspect ratio of the projection.</param>
    /// <param name="nearPlaneDistance">The distance to the near clipping plane.</param>
    /// <param name="farPlaneDistance">The distance to the far clipping plane.</param>
    /// <returns>A matrix representing the perspective projection.</returns>
    /// <remarks>
    /// The resulting matrix is computed as follows:
    ///     yScale = Cot(fieldOfView / 2)
    ///     xScale = yScale / aspectRatio
    ///     q = farPlaneDistance / (farPlaneDistance - nearPlaneDistance)
    ///     M = [xScale, 0, 0, 0;
    ///          0, yScale, 0, 0;
    ///          0, 0, q, 1;
    ///          0, 0, -q * nearPlaneDistance, 0]
    /// Where:
    ///     - fieldOfView is the field of view angle ref readonly radians.
    ///     - aspectRatio is the aspect ratio of the projection.
    ///     - nearPlaneDistance is the distance to the near clipping plane.
    ///     - farPlaneDistance is the distance to the far clipping plane.
    /// The resulting matrix represents a perspective projection with the specified field of view, aspect ratio,
    /// and clipping plane distances.
    /// </remarks>
    public static Matrix4x4<T> CreatePerspectiveFieldOfView<T>(Angle<Radians, T> fieldOfView, T aspectRatio, T nearPlaneDistance, T farPlaneDistance)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var two = T.One + T.One;
        var (sin, cos) = Angle.SinCos(fieldOfView / two);
        var height = cos / sin;
        var width = height / aspectRatio;
        var range = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
        return new(
            new(width,  T.Zero, T.Zero, T.Zero),
            new(T.Zero, height, T.Zero, T.Zero),
            new(T.Zero, T.Zero, range, -range * nearPlaneDistance),
            new(T.Zero, T.Zero, -T.One, T.Zero));
    }

    //public static Matrix4x4<T> CreatePerspective<T>(T width, T height, T nearPlaneDistance, T farPlaneDistance)
    //    where T : struct, INumber<T>, IMinMaxValue<T>
    //{
    //    var range = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
    //    return new(
    //        width, T.Zero, T.Zero, T.Zero,
    //        T.Zero, height, T.Zero, T.Zero,
    //        T.Zero, T.Zero, range, -range * nearPlaneDistance,
    //        T.Zero, T.Zero, -T.One, T.Zero);
    //}

    //public static Matrix4x4<T> CreatePerspectiveOffCenter<T>(T left, T right, T bottom, T top, T nearPlaneDistance, T farPlaneDistance)
    //    where T : struct, INumber<T>, IMinMaxValue<T>
    //{
    //    var two = T.One + T.One;
    //    var width = right - left;
    //    var height = top - bottom;
    //    var range = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
    //    return new(
    //        two * nearPlaneDistance / width, T.Zero, (right + left) / width, T.Zero,
    //        T.Zero, two * nearPlaneDistance / height, (top + bottom) / height, T.Zero,
    //        T.Zero, T.Zero, range, -range * nearPlaneDistance,
    //        T.Zero, T.Zero, -T.One, T.Zero);
    //}

    //public static Matrix4x4<T> CreateOrthographic<T>(T width, T height, T zNearPlane, T zFarPlane)
    //    where T : struct, INumber<T>, IMinMaxValue<T>
    //{
    //    var range = T.One / (zNearPlane - zFarPlane);
    //    return new(
    //        T.One / width, T.Zero, T.Zero, T.Zero,
    //        T.Zero, T.One / height, T.Zero, T.Zero,
    //        T.Zero, T.Zero, range, range * zNearPlane,
    //        T.Zero, T.Zero, T.Zero, T.One);
    //}

    //public static Matrix4x4<T> CreateOrthographicOffCenter<T>(T left, T right, T bottom, T top, T zNearPlane, T zFarPlane)
    //    where T : struct, INumber<T>, IMinMaxValue<T>
    //{
    //    var two = T.One + T.One;
    //    var width = right - left;
    //    var height = top - bottom;
    //    var range = T.One / (zNearPlane - zFarPlane);
    //    return new(
    //        two / width, T.Zero, T.Zero, (right + left) / width,
    //        T.Zero, two / height, T.Zero, (top + bottom) / height,
    //        T.Zero, T.Zero, range, range * zNearPlane,
    //        T.Zero, T.Zero, T.Zero, T.One);
    //}

    /// <summary>
    /// Performs a linear interpolation between two matrices.
    /// </summary>
    /// <typeparam name="T">The type of the matrix components.</typeparam>
    /// <param name="from">The starting matrix.</param>
    /// <param name="to">The target matrix.</param>
    /// <param name="factor">The interpolation factor. Value should be between 0 and 1.</param>
    /// <returns>A new matrix that represents the result of the linear interpolation.</returns>
    /// <remarks>
    /// The linear interpolation (lerp) is a mathematical operation that computes a value between two given values based on a weighting factor.
    /// In the context of matrices, the lerp operation is performed on each component of the matrices individually.
    /// The resulting matrix is a combination of the starting matrix and the target matrix based on the interpolation factor.
    /// The interpolation factor should be between 0 and 1, where 0 returns the starting matrix and 1 returns the target matrix.
    /// </remarks>
    public static Matrix4x4<T> Lerp<T>(Matrix4x4<T> from, Matrix4x4<T> to, T factor)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(
            Vector4.Lerp(from.X, to.X, factor),
            Vector4.Lerp(from.Y, to.Y, factor),
            Vector4.Lerp(from.Z, to.Z, factor),
            Vector4.Lerp(from.W, to.W, factor));

    /// <summary>
    /// Computes the transpose of a matrix.
    /// </summary>
    /// <typeparam name="T">The type of the matrix components.</typeparam>
    /// <param name="matrix">The input matrix.</param>
    /// <returns>A new matrix representing the transpose of the input matrix.</returns>
    /// <remarks>
    /// The transpose of a matrix is obtained by exchanging the rows and columns of the original matrix.
    /// This method creates a new matrix where the rows of the input matrix become the columns and the columns become the rows.
    /// </remarks>
    public static Matrix4x4<T> Transpose<T>(Matrix4x4<T> matrix)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(
            new(matrix.X.X, matrix.Y.X, matrix.Z.X, matrix.W.X),
            new(matrix.X.Y, matrix.Y.Y, matrix.Z.Y, matrix.W.Y),
            new(matrix.X.Z, matrix.Y.Z, matrix.Z.Z, matrix.W.Z),
            new(matrix.X.W, matrix.Y.W, matrix.Z.W, matrix.W.W));

    //public static Matrix4x4<T> Invert<T>(Matrix4x4<T> matrix)
    //    where T : struct, IFloatingPointIeee754<T>
    //{
    //    if (IsIdentity(matrix))
    //        return Matrix4x4<T>.Identity;

    //    var det = Determinant(matrix);
    //    if (T.Abs(det) < T.Epsilon)
    //        throw new InvalidOperationException("Matrix is not invertible.");

    //    var invDet = T.One / det;

    //    return new(
    //        invDet * ((matrix.M22 * matrix.M33 * matrix.M44) + (matrix.M23 * matrix.M34 * matrix.M42) + (matrix.M24 * matrix.M32 * matrix.M43) - (matrix.M22 * matrix.M34 * matrix.M43) - (matrix.M23 * matrix.M32 * matrix.M44) - (matrix.M24 * matrix.M33 * matrix.M42)),
    //        invDet * ((matrix.M12 * matrix.M34 * matrix.M43) + (matrix.M13 * matrix.M32 * matrix.M44) + (matrix.M14 * matrix.M33 * matrix.M42) - (matrix.M12 * matrix.M33 * matrix.M44) - (matrix.M13 * matrix.M34 * matrix.M42) - (matrix.M14 * matrix.M32 * matrix.M43)),
    //        invDet * ((matrix.M12 * matrix.M23 * matrix.M44) + (matrix.M13 * matrix.M24 * matrix.M42) + (matrix.M14 * matrix.M22 * matrix.M43) - (matrix.M12 * matrix.M24 * matrix.M43) - (matrix.M13 * matrix.M22 * matrix.M44) - (matrix.M14 * matrix.M23 * matrix.M42)),
    //        invDet * ((matrix.M12 * matrix.M24 * matrix.M33) + (matrix.M13 * matrix.M22 * matrix.M34) + (matrix.M14 * matrix.M23 * matrix.M32) - (matrix.M12 * matrix.M23 * matrix.M34) - (matrix.M13 * matrix.M24 * matrix.M32) - (matrix.M14 * matrix.M22 * matrix.M33)),

    //        invDet * ((matrix.M21 * matrix.M34 * matrix.M43) + (matrix.M23 * matrix.M31 * matrix.M44) + (matrix.M24 * matrix.M33 * matrix.M41) - (matrix.M21 * matrix.M33 * matrix.M44) - (matrix.M23 * matrix.M34 * matrix.M41) - (matrix.M24 * matrix.M31 * matrix.M43)),
    //        invDet * ((matrix.M11 * matrix.M33 * matrix.M44) + (matrix.M13 * matrix.M34 * matrix.M41) + (matrix.M14 * matrix.M31 * matrix.M43) - (matrix.M11 * matrix.M34 * matrix.M43) - (matrix.M13 * matrix.M31 * matrix.M44) - (matrix.M14 * matrix.M33 * matrix.M41)),
    //        invDet * ((matrix.M11 * matrix.M24 * matrix.M43) + (matrix.M13 * matrix.M21 * matrix.M44) + (matrix.M14 * matrix.M23 * matrix.M41) - (matrix.M11 * matrix.M23 * matrix.M44) - (matrix.M13 * matrix.M24 * matrix.M41) - (matrix.M14 * matrix.M21 * matrix.M43)),
    //        invDet * ((matrix.M11 * matrix.M23 * matrix.M34) + (matrix.M13 * matrix.M24 * matrix.M31) + (matrix.M14 * matrix.M21 * matrix.M33) - (matrix.M11 * matrix.M24 * matrix.M33) - (matrix.M13 * matrix.M21 * matrix.M34) - (matrix.M14 * matrix.M23 * matrix.M31)),

    //        invDet * ((matrix.M21 * matrix.M32 * matrix.M44) + (matrix.M22 * matrix.M34 * matrix.M41) + (matrix.M24 * matrix.M31 * matrix.M42) - (matrix.M21 * matrix.M34 * matrix.M42) - (matrix.M22 * matrix.M31 * matrix.M44) - (matrix.M24 * matrix.M32 * matrix.M41)),
    //        invDet * ((matrix.M11 * matrix.M34 * matrix.M42) + (matrix.M12 * matrix.M31 * matrix.M44) + (matrix.M14 * matrix.M32 * matrix.M41) - (matrix.M11 * matrix.M32 * matrix.M44) - (matrix.M12 * matrix.M34 * matrix.M41) - (matrix.M14 * matrix.M31 * matrix.M42)),
    //        invDet * ((matrix.M11 * matrix.M22 * matrix.M44) + (matrix.M12 * matrix.M24 * matrix.M41) + (matrix.M14 * matrix.M21 * matrix.M42) - (matrix.M11 * matrix.M24 * matrix.M42) - (matrix.M12 * matrix.M21 * matrix.M44) - (matrix.M14 * matrix.M22 * matrix.M41)),
    //        invDet * ((matrix.M11 * matrix.M24 * matrix.M32) + (matrix.M12 * matrix.M21 * matrix.M34) + (matrix.M14 * matrix.M22 * matrix.M31) - (matrix.M11 * matrix.M22 * matrix.M34) - (matrix.M12 * matrix.M24 * matrix.M31) - (matrix.M14 * matrix.M21 * matrix.M32)),

    //        invDet * ((matrix.M21 * matrix.M33 * matrix.M42) + (matrix.M22 * matrix.M31 * matrix.M43) + (matrix.M23 * matrix.M32 * matrix.M41) - (matrix.M21 * matrix.M32 * matrix.M43) - (matrix.M22 * matrix.M33 * matrix.M41) - (matrix.M23 * matrix.M31 * matrix.M42)),
    //        invDet * ((matrix.M11 * matrix.M32 * matrix.M43) + (matrix.M12 * matrix.M33 * matrix.M41) + (matrix.M13 * matrix.M31 * matrix.M42) - (matrix.M11 * matrix.M33 * matrix.M42) - (matrix.M12 * matrix.M31 * matrix.M43) - (matrix.M13 * matrix.M32 * matrix.M41)),
    //        invDet * ((matrix.M11 * matrix.M23 * matrix.M42) + (matrix.M12 * matrix.M21 * matrix.M43) + (matrix.M13 * matrix.M22 * matrix.M41) - (matrix.M11 * matrix.M22 * matrix.M43) - (matrix.M12 * matrix.M23 * matrix.M41) - (matrix.M13 * matrix.M21 * matrix.M42)),
    //        invDet * ((matrix.M11 * matrix.M22 * matrix.M33) + (matrix.M12 * matrix.M23 * matrix.M31) + (matrix.M13 * matrix.M21 * matrix.M32) - (matrix.M11 * matrix.M23 * matrix.M32) - (matrix.M12 * matrix.M21 * matrix.M33) - (matrix.M13 * matrix.M22 * matrix.M31))
    //    );
    //}

    //public static T Determinant<T>(Matrix4x4<T> matrix)
    //    where T : struct, INumber<T>
    //{
    //    var a = matrix.M00;
    //    var b = matrix.M01;
    //    var c = matrix.M02;
    //    var d = matrix.M03;
    //    var e = matrix.M10;
    //    var f = matrix.M11;
    //    var g = matrix.M12;
    //    var h = matrix.M13;
    //    var i = matrix.M20;
    //    var j = matrix.M21;
    //    var k = matrix.M22;
    //    var l = matrix.M23;
    //    var m = matrix.M30;
    //    var n = matrix.M31;
    //    var o = matrix.M32;
    //    var p = matrix.M33;

    //    // Calculate the determinant using the matrix elements
    //    var det1 = a * ((f * ((k * p) - (l * o))) - (g * ((j * p) - (l * n))) + (h * ((j * o) - (k * n))));
    //    var det2 = b * ((e * ((k * p) - (l * o))) - (g * ((i * p) - (l * m))) + (h * ((i * o) - (k * m))));
    //    var det3 = c * ((e * ((j * p) - (l * n))) - (f * ((i * p) - (l * m))) + (h * ((i * n) - (j * m))));
    //    var det4 = d * ((e * ((j * o) - (k * n))) - (f * ((i * o) - (k * m))) + (g * ((i * n) - (j * m))));

    //    return det1 - det2 + det3 - det4;
    //}


}

