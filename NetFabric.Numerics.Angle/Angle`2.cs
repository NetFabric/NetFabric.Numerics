using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics;

[DebuggerTypeProxy(typeof(AngleDebugView<,>))]
public readonly record struct Angle<TUnits, T>(T Value) 
    : IEquatable<Angle<TUnits, T>>,
      IEqualityOperators<Angle<TUnits, T>, Angle<TUnits, T>, bool>,
      IComparable,
      IComparisonOperators<Angle<TUnits, T>, Angle<TUnits, T>, bool>,
      IAdditiveIdentity<Angle<TUnits, T>, Angle<TUnits, T>>,
      IMultiplicativeIdentity<Angle<TUnits, T>, Angle<TUnits, T>>,
      IUnaryPlusOperators<Angle<TUnits, T>, Angle<TUnits, T>>,
      IAdditionOperators<Angle<TUnits, T>, Angle<TUnits, T>, Angle<TUnits, T>>,
      IUnaryNegationOperators<Angle<TUnits, T>, Angle<TUnits, T>>,
      ISubtractionOperators<Angle<TUnits, T>, Angle<TUnits, T>, Angle<TUnits, T>>,
      //IMultiplyOperators<Angle<TUnits, T>, Angle<TUnits, T>, Angle<TUnits, T>>,
      IDivisionOperators<Angle<TUnits, T>, T, Angle<TUnits, T>>,
      IModulusOperators<Angle<TUnits, T>, Angle<TUnits, T>, Angle<TUnits, T>>,
      IMinMaxValue<Angle<TUnits, T>>
    where TUnits : IAngleUnits<TUnits>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    #region constants

    /// <summary>
    /// Represents the zero angle value (0 Degrees). This field is read-only.
    /// </summary>
    public static readonly Angle<TUnits, T> Zero = new(T.Zero);

    /// <summary>
    /// Represents the right angle value (90 Degrees). This field is read-only.
    /// </summary>
    public static readonly Angle<TUnits, T> Right = new(T.CreateChecked(TUnits.Right));

    /// <summary>
    /// Represents the straight angle value (180 Degrees). This field is read-only.
    /// </summary>
    public static readonly Angle<TUnits, T> Straight = new(T.CreateChecked(TUnits.Straight));

    /// <summary>
    /// Represents the full angle value (360 Degrees). This field is read-only.
    /// </summary>
    public static readonly Angle<TUnits, T> Full = new(T.CreateChecked(TUnits.Full));

    /// <summary>
    /// Represents the minimum angle value. This field is read-only.
    /// </summary>
    public static readonly Angle<TUnits, T> MinValue = new(T.MinValue);

    /// <summary>
    /// Represents the maximum angle value. This field is read-only.
    /// </summary>
    public static readonly Angle<TUnits, T> MaxValue = new(T.MaxValue);

    static Angle<TUnits, T> IAdditiveIdentity<Angle<TUnits, T>, Angle<TUnits, T>>.AdditiveIdentity
        => new(T.AdditiveIdentity);
    static Angle<TUnits, T> IMultiplicativeIdentity<Angle<TUnits, T>, Angle<TUnits, T>>.MultiplicativeIdentity
        => new(T.MultiplicativeIdentity);
    
    static Angle<TUnits, T> IMinMaxValue<Angle<TUnits, T>>.MinValue 
        => MinValue;
    static Angle<TUnits, T> IMinMaxValue<Angle<TUnits, T>>.MaxValue 
        => MaxValue;

    #endregion

    #region comparison

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int CompareTo(Angle<TUnits, T> other) 
        => Value.CompareTo(other.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int ReducedCompareTo(Angle<TUnits, T> other) 
        => Angle.Reduce(this).CompareTo(Angle.Reduce(other));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Angle<TUnits, T> left, Angle<TUnits, T> right) 
        => left.CompareTo(right) < 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Angle<TUnits, T> left, Angle<TUnits, T> right) 
        => left.CompareTo(right) <= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Angle<TUnits, T> left, Angle<TUnits, T> right) 
        => left.CompareTo(right) > 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Angle<TUnits, T> left, Angle<TUnits, T> right) 
        => left.CompareTo(right) >= 0;        

    readonly int IComparable.CompareTo(object? obj) 
        => obj switch
        {
            null => 1,
            Angle<TUnits, T> other => CompareTo(other),
            _ => Throw.ArgumentException<int>($"Argument must be of type {nameof(Angle<TUnits, T>)}.", nameof(obj)),
        };

    #endregion

    #region addition

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator +(Angle<TUnits, T> right) 
        => new(+right.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator +(Angle<TUnits, T> left, Angle<TUnits, T> right) 
        => new (left.Value + right.Value);

    #endregion

    #region subtraction

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator -(Angle<TUnits, T> right) 
        => new(-right.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator -(Angle<TUnits, T> left, Angle<TUnits, T> right) 
        => new (left.Value - right.Value);

    #endregion

    #region multiplication

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator *(Angle<TUnits, T> left, T right)
        => new(left.Value * right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator *(T left, Angle<TUnits, T> right)
        => new(left * right.Value);

    #endregion

    #region division

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator /(Angle<TUnits, T> left, T right)
        => new(left.Value / right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator %(Angle<TUnits, T> left, Angle<TUnits, T> right)
        => new(left.Value % right.Value);

    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => $"{Value}{TUnits.Symbol}";
}