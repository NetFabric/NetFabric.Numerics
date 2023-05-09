using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics;

[DebuggerDisplay("{Value} gradians")]
[DebuggerTypeProxy(typeof(GradiansDebugView<>))]
public readonly record struct Gradians<T>(T Value) 
    : IAngle<Gradians<T>>,
      IMultiplyOperators<Gradians<T>, T, Gradians<T>>,
      IDivisionOperators<Gradians<T>, T, Gradians<T>>
    where T : 
        struct, 
        IFloatingPoint<T>,
        IMinMaxValue<T>
{
    static Gradians<T> IAngle<Gradians<T>>.Abs(Gradians<T> angle)
        => new(T.Abs(angle.Value));

    static int IAngle<Gradians<T>>.Sign(Gradians<T> angle)
        => T.Sign(angle.Value);

    static Gradians<T> IAngle<Gradians<T>>.Lerp<TFactor>(Gradians<T> a1, Gradians<T> a2, TFactor t)
        => new(Utils.Lerp(a1.Value, a2.Value, t));

    #region constants

    /// <summary>
    /// Represents the zero angle value (0 Gradians). This field is read-only.
    /// </summary>
    public static readonly Gradians<T> Zero = new(T.Zero);

    /// <summary>
    /// Represents the right angle value (100 Gradians). This field is read-only.
    /// </summary>
    public static readonly Gradians<T> Right = new(T.CreateChecked(100.0));

    /// <summary>
    /// Represents the straight angle value (200 Gradians). This field is read-only.
    /// </summary>
    public static readonly Gradians<T> Straight = new(T.CreateChecked(200.0));

    /// <summary>
    /// Represents the full angle value (400 Gradians). This field is read-only.
    /// </summary>
    public static readonly Gradians<T> Full = new(T.CreateChecked(400.0));

    /// <summary>
    /// Represents the minimum angle value. This field is read-only.
    /// </summary>
    public static readonly Gradians<T> MinValue = new(T.MinValue);

    /// <summary>
    /// Represents the maximum angle value. This field is read-only.
    /// </summary>
    public static readonly Gradians<T> MaxValue = new(T.MaxValue);

    static Gradians<T> IAdditiveIdentity<Gradians<T>, Gradians<T>>.AdditiveIdentity
        => new(T.AdditiveIdentity);
    static Gradians<T> IMultiplicativeIdentity<Gradians<T>, Gradians<T>>.MultiplicativeIdentity
        => new(T.MultiplicativeIdentity);

    static Gradians<T> IAngle<Gradians<T>>.Zero 
        => Zero;
    static Gradians<T> IAngle<Gradians<T>>.Right
        => Right;
    static Gradians<T> IAngle<Gradians<T>>.Straight 
        => Straight;
    static Gradians<T> IAngle<Gradians<T>>.Full 
        => Full;

    static Gradians<T> IMinMaxValue<Gradians<T>>.MinValue 
        => MinValue;
    static Gradians<T> IMinMaxValue<Gradians<T>>.MaxValue 
        => MaxValue;

    #endregion

    #region comparison

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int CompareTo(Gradians<T> other) 
        => Value.CompareTo(other.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int ReducedCompareTo(Gradians<T> other) 
        => Angle.Reduce(this).CompareTo(Angle.Reduce(other));

    public static bool operator <(Gradians<T> left, Gradians<T> right) 
        => left.CompareTo(right) < 0;

    public static bool operator <=(Gradians<T> left, Gradians<T> right) 
        => left.CompareTo(right) <= 0;

    public static bool operator >(Gradians<T> left, Gradians<T> right) 
        => left.CompareTo(right) > 0;

    public static bool operator >=(Gradians<T> left, Gradians<T> right) 
        => left.CompareTo(right) >= 0;        

    readonly int IComparable.CompareTo(object? obj) 
        => obj switch
        {
            null => 1,
            Gradians<T> other => CompareTo(other),
            _ => Throw.ArgumentException<int>($"Argument must be of type {nameof(Gradians<T>)}.", nameof(obj)),
        };

    #endregion

    #region addition

    public static Gradians<T> operator +(Gradians<T> value) 
        => new(+value.Value);

    public static Gradians<T> operator +(Gradians<T> left, Gradians<T> right) 
        => new (left.Value + right.Value);

    #endregion

    #region subtraction

    public static Gradians<T> operator -(Gradians<T> value) 
        => new(-value.Value);

    public static Gradians<T> operator -(Gradians<T> left, Gradians<T> right) 
        => new (left.Value - right.Value);

    #endregion

    #region multiplication

    public static Gradians<T> operator *(Gradians<T> left, T right)
        => new(left.Value * right);

    public static Gradians<T> operator *(T left, Gradians<T> right)
        => new(left * right.Value);

    #endregion

    #region division

    public static Gradians<T> operator /(Gradians<T> left, T right)
        => new(left.Value / right);

    public static Gradians<T> operator %(Gradians<T> left, Gradians<T> right)
        => new(left.Value % right.Value);

    #endregion

    #region conversions

    static readonly T DegreesInGradians = T.CreateChecked(90.0 / 100.0);
    static readonly T RadiansInGradians = T.CreateChecked(Math.PI / 200.0);
    static readonly T RevolutionsInGradians = T.CreateChecked(1.0 / 400.0);

    public Degrees<T> ToDegrees<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value * DegreesInGradians));

    public Radians<T> ToRadians<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value * RadiansInGradians));

    public Gradians<T> ToGradians<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value));

    public Revolutions<T> ToRevolutions<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value * RevolutionsInGradians));

    #endregion
}