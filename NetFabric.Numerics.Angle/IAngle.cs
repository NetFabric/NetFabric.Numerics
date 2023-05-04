using System;
using System.Numerics;

namespace NetFabric.Numerics;

public interface IAngle<TSelf>
    : IEquatable<TSelf>,
      IEqualityOperators<TSelf, TSelf, bool>,
      IComparable,
      IComparable<TSelf>,
      IComparisonOperators<TSelf, TSelf, bool>,
      IMinMaxValue<TSelf>,
      IUnaryPlusOperators<TSelf, TSelf>,
      IAdditionOperators<TSelf, TSelf, TSelf>,
      IAdditiveIdentity<TSelf, TSelf>,
      IUnaryNegationOperators<TSelf, TSelf>,
      ISubtractionOperators<TSelf, TSelf, TSelf>
    where TSelf: IAngle<TSelf>?
{
    static abstract TSelf Right { get; }
    static abstract TSelf Straight { get; }
    static abstract TSelf Full { get; }
}