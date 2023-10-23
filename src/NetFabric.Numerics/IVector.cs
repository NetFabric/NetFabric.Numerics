namespace NetFabric.Numerics;

/// <summary>
/// Represents a vector.
/// </summary>
/// <typeparam name="TSelf">The type that implements the interface.</typeparam>
/// <typeparam name="T">The type of the vector coordinates.</typeparam>
public interface IVector<TSelf, T>
    : IGeometricBase<TSelf>,
      IComparable,
      IComparable<TSelf>,
      IComparisonOperators<TSelf, TSelf, bool>,
      IAdditiveIdentity<TSelf, TSelf>,
      IUnaryPlusOperators<TSelf, TSelf>,
      IAdditionOperators<TSelf, TSelf, TSelf>,
      IUnaryNegationOperators<TSelf, TSelf>,
      ISubtractionOperators<TSelf, TSelf, TSelf>,
      IMultiplyOperators<TSelf, T, TSelf>,
      IDivisionOperators<TSelf, T, TSelf>,
      IMinMaxValue<TSelf>
    where TSelf : struct, IVector<TSelf, T>?
    where T : struct, INumber<T>, IMinMaxValue<T>
{
}