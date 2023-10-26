namespace NetFabric.Numerics;

/// <summary>
/// Represents a vector in a coordinate system.
/// </summary>
/// <typeparam name="TSelf">The type implementing the interface.</typeparam>
/// <typeparam name="TCoordinateSystem">The type representing the coordinate system.</typeparam>
/// <typeparam name="T">The type of the vector coordinates.</typeparam>
public interface IVector<TSelf, TCoordinateSystem, T>
    : IGeometricBase<TSelf, TCoordinateSystem>,
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
    where TSelf : struct, IVector<TSelf, TCoordinateSystem, T>?
    where TCoordinateSystem : ICoordinateSystem
    where T : struct, INumber<T>, IMinMaxValue<T>
{
}