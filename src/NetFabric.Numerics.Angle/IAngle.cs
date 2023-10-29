namespace NetFabric.Numerics;

/// <summary>
/// Represents an angle.
/// </summary>
public interface IAngle
{
    /// <summary>
    /// Gets the units of measurement for this angle.
    /// </summary>
    /// <value>The units of measurement for this angle.</value>
    AngleUnits Units { get; }

    /// <summary>
    /// Gets the underlying value of the angle.
    /// </summary>
    /// <value>The underlying value of the angle.</value>
    object Value { get; }

    /// <summary>
    /// Gets the type of the underlying value of the angle.
    /// </summary>
    /// <value>The type of the underlying value of the angle.</value>
    Type ValueType { get; }
}

/// <summary>
/// Represents an angle of a specific unit and underlying value type.
/// </summary>
/// <typeparam name="TSelf">The implementing type.</typeparam>
/// <typeparam name="TUnits">The units of measurement for the angle.</typeparam>
/// <typeparam name="T">The underlying value type.</typeparam>
/// <typeparam name="TOther">The type of mathematical operations results.</typeparam>
public interface IAngle<TSelf, TUnits, T, TOther>
    : IAngle,
      IEquatable<TSelf>,
      IEqualityOperators<TSelf, TSelf, bool>,
      IComparable,
      IComparisonOperators<TSelf, TSelf, bool>,
      IAdditiveIdentity<TSelf, TSelf>,
      IUnaryPlusOperators<TSelf, TSelf>,
      IAdditionOperators<TSelf, TSelf, TOther>,
      IUnaryNegationOperators<TSelf, TOther>,
      ISubtractionOperators<TSelf, TSelf, TOther>,
      IMultiplicativeIdentity<TSelf, TSelf>,
      IDivisionOperators<TSelf, T, TOther>,
      IModulusOperators<TSelf, T, TOther>,
      IMinMaxValue<TSelf>,
      ISpanFormattable,
      ISpanParsable<TSelf>
    where TSelf : struct, IAngle<TSelf, TUnits, T, TOther>?
    where TUnits : IAngleUnits
    where T : struct, IFloatingPoint<T>
    where TOther : struct, IAngle?
{
    /// <summary>
    /// Gets the units of measurement for this angle.
    /// </summary>
    /// <value>The units of measurement for this angle.</value>
    new AngleUnits<TUnits> Units
      => AngleUnits<TUnits>.Instance;

    /// <summary>
    /// Gets the underlying value of the angle.
    /// </summary>
    /// <value>The underlying value of the angle.</value>
    new T Value { get; }

    AngleUnits IAngle.Units
      => Units;

    object IAngle.Value
      => Value;

    Type IAngle.ValueType
      => typeof(T);
}
