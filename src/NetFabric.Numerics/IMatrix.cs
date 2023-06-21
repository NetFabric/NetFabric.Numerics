namespace NetFabric.Numerics;

/// <summary>
/// Represents a matrix.
/// </summary>
/// <typeparam name="TSelf">The type that implements the interface.</typeparam>
/// <typeparam name="T">The type of the matrix elements.</typeparam>
public interface IMatrix<TSelf, T>
    : INumericBase<TSelf>,
      IAdditiveIdentity<TSelf, TSelf>,
      IUnaryPlusOperators<TSelf, TSelf>,
      IAdditionOperators<TSelf, TSelf, TSelf>,
      IUnaryNegationOperators<TSelf, TSelf>,
      ISubtractionOperators<TSelf, TSelf, TSelf>,
      IMultiplyOperators<TSelf, T, TSelf>,
      IDivisionOperators<TSelf, T, TSelf>,
      IMinMaxValue<TSelf>
    where TSelf : struct, IMatrix<TSelf, T>?
    where T : struct, INumber<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Gets the number of rows in the matrix.
    /// </summary>
    /// <value>The number of rows in the matrix.</value>
    int RowCount { get; }

    /// <summary>
    /// Gets the number of columns in the matrix.
    /// </summary>
    /// <value>The number of columns in the matrix.</value>
    int ColumnCount { get; }

    /// <summary>
    /// Gets the value at the specified row and column in the matrix.
    /// </summary>
    /// <param name="row">The zero-based index of the row.</param>
    /// <param name="column">The zero-based index of the column.</param>
    /// <returns>The value at the specified row and column.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the specified row or column is outside the valid range of indices for the matrix.
    /// </exception>
    T this[int row, int column] { get; }

    /// <summary>
    /// Gets the identity matrix.
    /// </summary>
    static abstract TSelf Identity { get; }

}
