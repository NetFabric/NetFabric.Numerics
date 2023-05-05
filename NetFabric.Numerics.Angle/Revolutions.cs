using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics;

[DebuggerDisplay("{Value} revolutions")]
[DebuggerTypeProxy(typeof(RevolutionsDebugView<>))]
public readonly record struct Revolutions<T>(T Value) 
    : IAngle<Revolutions<T>>,
      IMultiplyOperators<Revolutions<T>, T, Revolutions<T>>,
      IDivisionOperators<Revolutions<T>, T, Revolutions<T>>
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

    static Revolutions<T> IAngle<Revolutions<T>>.Abs(Revolutions<T> angle)
        => new(T.Abs(angle.Value));

    static int IAngle<Revolutions<T>>.Sign(Revolutions<T> angle)
        => T.Sign(angle.Value);

    static Revolutions<T> IAngle<Revolutions<T>>.Lerp<TFactor>(Revolutions<T> a1, Revolutions<T> a2, TFactor t)
        => new(Utils.Lerp(a1.Value, a2.Value, t));

    #region constants

    /// <summary>
    /// Represents the zero angle value (0 degrees). This field is read-only.
    /// </summary>
    public static readonly Revolutions<T> Zero = new(T.Zero);

    /// <summary>
    /// Represents the right angle value (90 degrees). This field is read-only.
    /// </summary>
    public static readonly Revolutions<T> Right = new(T.CreateChecked(0.25));

    /// <summary>
    /// Represents the straight angle value (180 degrees). This field is read-only.
    /// </summary>
    public static readonly Revolutions<T> Straight = new(T.CreateChecked(0.5));

    /// <summary>
    /// Represents the full angle value (360 degrees). This field is read-only.
    /// </summary>
    public static readonly Revolutions<T> Full = new(T.CreateChecked(1.0));

    /// <summary>
    /// Represents the minimum angle value. This field is read-only.
    /// </summary>
    public static readonly Revolutions<T> MinValue = new(T.MinValue);

    /// <summary>
    /// Represents the maximum angle value. This field is read-only.
    /// </summary>
    public static readonly Revolutions<T> MaxValue = new(T.MaxValue);

    static Revolutions<T> IAdditiveIdentity<Revolutions<T>, Revolutions<T>>.AdditiveIdentity
        => new(T.AdditiveIdentity);
    static Revolutions<T> IMultiplicativeIdentity<Revolutions<T>, Revolutions<T>>.MultiplicativeIdentity
        => new(T.MultiplicativeIdentity);

    static Revolutions<T> AdditiveIdentity
        => new(T.AdditiveIdentity);

    static Revolutions<T> IAngle<Revolutions<T>>.Right 
        => Right;
    static Revolutions<T> IAngle<Revolutions<T>>.Straight 
        => Straight;
    static Revolutions<T> IAngle<Revolutions<T>>.Full 
        => Full;
    
    static Revolutions<T> IMinMaxValue<Revolutions<T>>.MinValue 
        => MinValue;
    static Revolutions<T> IMinMaxValue<Revolutions<T>>.MaxValue 
        => MaxValue;

    #endregion

    #region comparison

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int CompareTo(Revolutions<T> other) 
        => Value.CompareTo(other.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int ReducedCompareTo(Revolutions<T> other) 
        => Reduce(Value).CompareTo(Reduce(other.Value));

    public static bool operator <(Revolutions<T> left, Revolutions<T> right) 
        => left.CompareTo(right) < 0;

    public static bool operator <=(Revolutions<T> left, Revolutions<T> right) 
        => left.CompareTo(right) <= 0;

    public static bool operator >(Revolutions<T> left, Revolutions<T> right) 
        => left.CompareTo(right) > 0;

    public static bool operator >=(Revolutions<T> left, Revolutions<T> right) 
        => left.CompareTo(right) >= 0;        

    readonly int IComparable.CompareTo(object? obj) 
        => obj switch
        {
            null => 1,
            Revolutions<T> other => CompareTo(other),
            _ => Throw.ArgumentException<int>($"Argument must be of type {nameof(Revolutions<T>)}.", nameof(obj)),
        };

    #endregion

    #region addition

    public static Revolutions<T> operator +(Revolutions<T> value) 
        => new(+value.Value);

    public static Revolutions<T> operator +(Revolutions<T> left, Revolutions<T> right) 
        => new (left.Value + right.Value);

    #endregion

    #region subtraction

    public static Revolutions<T> operator -(Revolutions<T> value) 
        => new(-value.Value);

    public static Revolutions<T> operator -(Revolutions<T> left, Revolutions<T> right) 
        => new (left.Value - right.Value);

    #endregion

    #region multiplication

    public static Revolutions<T> operator *(Revolutions<T> left, T right)
        => new(left.Value * right);

    public static Revolutions<T> operator *(T left, Revolutions<T> right)
        => new(left * right.Value);

    #endregion

    #region division

    public static Revolutions<T> operator /(Revolutions<T> left, T right)
        => new(left.Value / right);

    public static Revolutions<T> operator %(Revolutions<T> left, Revolutions<T> right)
        => new(left.Value % right.Value);

    #endregion

    #region conversions

    static readonly T DegreesInRevolutions = T.CreateChecked(360.0);
    static readonly T RadiansInRevolutions = T.CreateChecked(Math.PI / 0.5);
    static readonly T GradiansInRevolutions = T.CreateChecked(400.0);

    public Degrees<T> ToDegrees<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value * DegreesInRevolutions));

    public Radians<T> ToRadians<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value * RadiansInRevolutions));

    public Revolutions<T> ToRevolutions<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value));

    public Gradians<T> ToGradians<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value * GradiansInRevolutions));

    #endregion
}