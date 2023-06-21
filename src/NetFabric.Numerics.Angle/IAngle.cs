namespace NetFabric.Numerics;

/// <summary>
/// Represents an angle of a specific unit and underlying value type.
/// </summary>
/// <typeparam name="TSelf">The implementing type.</typeparam>
/// <typeparam name="T">The underlying value type of the angle.</typeparam>
public interface IAngle<TSelf, T>
    : IEquatable<TSelf>,
      IEqualityOperators<TSelf, TSelf, bool>,
      IComparable,
      IComparisonOperators<TSelf, TSelf, bool>,
      IAdditiveIdentity<TSelf, TSelf>,
      IMultiplicativeIdentity<TSelf, TSelf>,
      IMinMaxValue<TSelf>
    where TSelf : struct, IAngle<TSelf, T>?
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
}
