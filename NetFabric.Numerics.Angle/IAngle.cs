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
      IAdditiveIdentity<TSelf, TSelf>,
      IUnaryPlusOperators<TSelf, TSelf>,
      IAdditionOperators<TSelf, TSelf, TSelf>,
      IUnaryNegationOperators<TSelf, TSelf>,
      ISubtractionOperators<TSelf, TSelf, TSelf>,
      IMultiplicativeIdentity<TSelf, TSelf>,
      //IMultiplyOperators<TSelf, TSelf, TSelf>,
      //IDivisionOperators<TSelf, TSelf, TSelf>,
      IModulusOperators<TSelf, TSelf, TSelf>
    where TSelf: IAngle<TSelf>?
{
    static abstract TSelf Zero { get; }
    static abstract TSelf Right { get; }
    static abstract TSelf Straight { get; }
    static abstract TSelf Full { get; }

    /// <summary>
    /// Returns the absolute value of angle.
    /// </summary>
    /// <param name="angle">The angle for which to get its absolute.</param>
    /// <returns>
    /// The absolute of <paramref name="angle" />.
    /// </returns>
    static abstract TSelf Abs(TSelf angle);

    /// <summary>
    /// Returns a value indicating the sign of angle.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>A number that indicates the sign of value, -1 if value is less than zero, 0 if value equal to zero, 1 if value is grater than zero.</returns>
    static abstract int Sign(TSelf angle);

    /// <summary>
    /// Performs a linear interpolation.
    /// </summary>
    /// <param name="a1">The first angle.</param>
    /// <param name="a2">The second angle.</param>
    /// <param name="t">A value that linearly interpolates between the a1 parameter and the a2 parameter.</param>
    /// <returns>The result of the linear interpolation.</returns>
    static abstract TSelf Lerp<T>(TSelf a1, TSelf a2, T t) 
        where T : struct, IFloatingPoint<T>;
}