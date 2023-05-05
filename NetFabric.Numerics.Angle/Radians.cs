using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics;

[DebuggerDisplay("{Value} radians")]
[DebuggerTypeProxy(typeof(RadiansDebugView<>))]
public readonly record struct Radians<T>(T Value) 
    : IAngle<Radians<T>>,
      IMultiplyOperators<Radians<T>, T, Radians<T>>,
      IDivisionOperators<Radians<T>, T, Radians<T>>
    where T : 
        struct, 
        IFloatingPoint<T>,
        IMinMaxValue<T>
{
    static T Reduce(T degrees) 
        => Utils.Reduce(degrees, Full.Value);

    static Quadrant GetQuadrant(T degrees) 
        => Utils.GetQuadrant(degrees, Right.Value, Straight.Value, Full.Value);

    static T GetReference(T degrees) 
        => Utils.GetReference(degrees, Right.Value, Straight.Value, Full.Value);

    static Radians<T> IAngle<Radians<T>>.Abs(Radians<T> angle)
        => new(T.Abs(angle.Value));

    static int IAngle<Radians<T>>.Sign(Radians<T> angle)
        => T.Sign(angle.Value);

    static Radians<T> IAngle<Radians<T>>.Lerp<TFactor>(Radians<T> a1, Radians<T> a2, TFactor t)
        => new(Utils.Lerp(a1.Value, a2.Value, t));

    #region constants

    /// <summary>
    /// Represents the zero angle value (0 radians). This field is read-only.
    /// </summary>
    public static readonly Radians<T> Zero = new(T.Zero);

    /// <summary>
    /// Represents the golden angle value. This field is read-only.
    /// </summary>
    public static readonly Radians<T> Golden = new(T.CreateChecked(Math.PI * (3.0 - Math.Sqrt(5.0))));

    /// <summary>
    /// Represents the right angle value (PI/2 radians). This field is read-only.
    /// </summary>
    public static readonly Radians<T> Right = new(T.CreateChecked(Math.PI / 2.0));

    /// <summary>
    /// Represents the straight angle value (PI radians). This field is read-only.
    /// </summary>
    public static readonly Radians<T> Straight = new(T.CreateChecked(Math.PI));

    /// <summary>
    /// Represents the full angle value (PI*2 radians). This field is read-only.
    /// </summary>
    public static readonly Radians<T> Full = new(T.CreateChecked(Math.PI * 2.0));

    /// <summary>
    /// Represents the minimum angle value. This field is read-only.
    /// </summary>
    public static readonly Radians<T> MinValue = new(T.MinValue);

    /// <summary>
    /// Represents the maximum angle value. This field is read-only.
    /// </summary>
    public static readonly Radians<T> MaxValue = new(T.MaxValue);

    static Radians<T> IAdditiveIdentity<Radians<T>, Radians<T>>.AdditiveIdentity
        => new(T.AdditiveIdentity);
    static Radians<T> IMultiplicativeIdentity<Radians<T>, Radians<T>>.MultiplicativeIdentity
        => new(T.MultiplicativeIdentity);

    static Radians<T> IAngle<Radians<T>>.Right 
        => Right;
    static Radians<T> IAngle<Radians<T>>.Straight 
        => Straight;
    static Radians<T> IAngle<Radians<T>>.Full 
        => Full;

    static Radians<T> IMinMaxValue<Radians<T>>.MinValue 
        => MinValue;
    static Radians<T> IMinMaxValue<Radians<T>>.MaxValue 
        => MaxValue;

    #endregion

    #region comparison

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int CompareTo(Radians<T> other) 
        => Value.CompareTo(other.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int ReducedCompareTo(Radians<T> other) 
        => Reduce(Value).CompareTo(Reduce(other.Value));

    public static bool operator <(Radians<T> left, Radians<T> right) 
        => left.CompareTo(right) < 0;

    public static bool operator <=(Radians<T> left, Radians<T> right) 
        => left.CompareTo(right) <= 0;

    public static bool operator >(Radians<T> left, Radians<T> right) 
        => left.CompareTo(right) > 0;

    public static bool operator >=(Radians<T> left, Radians<T> right) 
        => left.CompareTo(right) >= 0;        

    readonly int IComparable.CompareTo(object? obj) 
        => obj switch
        {
            null => 1,
            Radians<T> other => CompareTo(other),
            _ => Throw.ArgumentException<int>($"Argument must be of type {nameof(Radians<T>)}.", nameof(obj)),
        };

    #endregion

    #region addition

    public static Radians<T> operator +(Radians<T> value) 
        => new(+value.Value);

    public static Radians<T> operator +(Radians<T> left, Radians<T> right) 
        => new (left.Value + right.Value);

    #endregion

    #region subtraction

    public static Radians<T> operator -(Radians<T> value) 
        => new(-value.Value);

    public static Radians<T> operator -(Radians<T> left, Radians<T> right) 
        => new (left.Value - right.Value);

    #endregion

    #region multiplication

    public static Radians<T> operator *(Radians<T> left, T right)
        => new(left.Value * right);

    public static Radians<T> operator *(T left, Radians<T> right)
        => new(left * right.Value);

    #endregion

    #region division

    public static Radians<T> operator /(Radians<T> left, T right)
        => new(left.Value / right);

    public static Radians<T> operator %(Radians<T> left, Radians<T> right)
        => new(left.Value % right.Value);

    #endregion

    #region conversions

    static readonly T DegreesInRadians = T.CreateChecked(180.0 / Math.PI);
    static readonly T GradiansInRadians = T.CreateChecked(200.0 / Math.PI);
    static readonly T RevolutionsInRadians = T.CreateChecked(0.5 / Math.PI);

    public Degrees<T> ToDegrees<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value * DegreesInRadians));

    public Radians<T> ToRadians<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value));

    public Gradians<T> ToGradians<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value * GradiansInRadians));

    public Revolutions<T> ToRevolutions<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value * RevolutionsInRadians));

    #endregion
}