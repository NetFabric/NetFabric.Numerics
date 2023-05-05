using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics;

[DebuggerDisplay("{Value}°")]
[DebuggerTypeProxy(typeof(DegreesDebugView<>))]
public readonly record struct Degrees<T>(T Value) 
    : IAngle<Degrees<T>>,
      IMultiplyOperators<Degrees<T>, T, Degrees<T>>,
      IDivisionOperators<Degrees<T>, T, Degrees<T>>
    where T : 
        struct, 
        IFloatingPoint<T>, 
        IMinMaxValue<T>
{
    /// <summary>
    /// Gets the value of the current Angle structure expressed in degrees and minutes.
    /// </summary>
    public (TDegrees Degrees, TMinutes Minutes) AsDegreesMinutes<TDegrees, TMinutes>()
        where TDegrees: IBinaryInteger<TDegrees>
        where TMinutes: IFloatingPoint<TMinutes>
    {
        var degrees = TDegrees.CreateChecked(Value);
        var minutes = TMinutes.CreateChecked(Math.Abs(double.CreateChecked(Value) - double.CreateChecked(degrees)) * 60.0);
        return (degrees, minutes);
    }

    /// <summary>
    /// Gets the value of the current Angle structure expressed in degrees, minutes and seconds.
    /// </summary>
    public (TDegrees Degrees, TMinutes Minutes, TSeconds Seconds) AsDegreesMinutesSeconds<TDegrees, TMinutes, TSeconds>()
    where TDegrees : IBinaryInteger<TDegrees>
    where TMinutes : IBinaryInteger<TMinutes>
    where TSeconds : IFloatingPoint<TSeconds>
    {
        var degrees = TDegrees.CreateChecked(Value);
        var decimalMinutes = Math.Abs(double.CreateChecked(Value) - double.CreateChecked(degrees)) * 60.0;
        var minutes = TMinutes.CreateChecked(decimalMinutes);
        var seconds = TSeconds.CreateChecked((decimalMinutes - double.CreateChecked(minutes)) * 60.0);
        return (degrees, minutes, seconds);
    }

    static T Reduce(T degrees) 
        => Utils.Reduce(degrees, Full.Value);

    static Quadrant GetQuadrant(T degrees) 
        => Utils.GetQuadrant(degrees, Right.Value, Straight.Value, Full.Value);

    static T GetReference(T degrees) 
        => Utils.GetReference(degrees, Right.Value, Straight.Value, Full.Value);

    static Degrees<T> IAngle<Degrees<T>>.Abs(Degrees<T> angle)
        => new(T.Abs(angle.Value));

    static int IAngle<Degrees<T>>.Sign(Degrees<T> angle)
        => T.Sign(angle.Value);

    static Degrees<T> IAngle<Degrees<T>>.Lerp<TFactor>(Degrees<T> a1, Degrees<T> a2, TFactor t)
        => new(Utils.Lerp(a1.Value, a2.Value, t));

    #region constants

    /// <summary>
    /// Represents the zero angle value (0 degrees). This field is read-only.
    /// </summary>
    public static readonly Degrees<T> Zero = new(T.Zero);

    /// <summary>
    /// Represents the right angle value (90 degrees). This field is read-only.
    /// </summary>
    public static readonly Degrees<T> Right = new(T.CreateChecked(90.0));

    /// <summary>
    /// Represents the straight angle value (180 degrees). This field is read-only.
    /// </summary>
    public static readonly Degrees<T> Straight = new(T.CreateChecked(180.0));

    /// <summary>
    /// Represents the full angle value (360 degrees). This field is read-only.
    /// </summary>
    public static readonly Degrees<T> Full = new(T.CreateChecked(360.0));

    /// <summary>
    /// Represents the minimum angle value. This field is read-only.
    /// </summary>
    public static readonly Degrees<T> MinValue = new(T.MinValue);

    /// <summary>
    /// Represents the maximum angle value. This field is read-only.
    /// </summary>
    public static readonly Degrees<T> MaxValue = new(T.MaxValue);

    static Degrees<T> IAdditiveIdentity<Degrees<T>, Degrees<T>>.AdditiveIdentity
        => new(T.AdditiveIdentity);
    static Degrees<T> IMultiplicativeIdentity<Degrees<T>, Degrees<T>>.MultiplicativeIdentity
        => new(T.MultiplicativeIdentity);

    static Degrees<T> AdditiveIdentity
        => new(T.AdditiveIdentity);

    static Degrees<T> IAngle<Degrees<T>>.Right 
        => Right;
    static Degrees<T> IAngle<Degrees<T>>.Straight 
        => Straight;
    static Degrees<T> IAngle<Degrees<T>>.Full 
        => Full;
    
    static Degrees<T> IMinMaxValue<Degrees<T>>.MinValue 
        => MinValue;
    static Degrees<T> IMinMaxValue<Degrees<T>>.MaxValue 
        => MaxValue;
    #endregion

    #region comparison

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int CompareTo(Degrees<T> other) 
        => Value.CompareTo(other.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int ReducedCompareTo(Degrees<T> other) 
        => Reduce(Value).CompareTo(Reduce(other.Value));

    public static bool operator <(Degrees<T> left, Degrees<T> right) 
        => left.CompareTo(right) < 0;

    public static bool operator <=(Degrees<T> left, Degrees<T> right) 
        => left.CompareTo(right) <= 0;

    public static bool operator >(Degrees<T> left, Degrees<T> right) 
        => left.CompareTo(right) > 0;

    public static bool operator >=(Degrees<T> left, Degrees<T> right) 
        => left.CompareTo(right) >= 0;        

    readonly int IComparable.CompareTo(object? obj) 
        => obj switch
        {
            null => 1,
            Degrees<T> other => CompareTo(other),
            _ => Throw.ArgumentException<int>($"Argument must be of type {nameof(Degrees<T>)}.", nameof(obj)),
        };

    #endregion

    #region addition

    public static Degrees<T> operator +(Degrees<T> value) 
        => new(+value.Value);

    public static Degrees<T> operator +(Degrees<T> left, Degrees<T> right) 
        => new (left.Value + right.Value);

    #endregion

    #region subtraction

    public static Degrees<T> operator -(Degrees<T> value) 
        => new(-value.Value);

    public static Degrees<T> operator -(Degrees<T> left, Degrees<T> right) 
        => new (left.Value - right.Value);

    #endregion

    #region multiplication

    public static Degrees<T> operator *(Degrees<T> left, T right)
        => new(left.Value * right);

    public static Degrees<T> operator *(T left, Degrees<T> right)
        => new(left * right.Value);

    #endregion

    #region division

    public static Degrees<T> operator /(Degrees<T> left, T right)
        => new(left.Value / right);

    public static Degrees<T> operator %(Degrees<T> left, Degrees<T> right)
        => new(left.Value % right.Value);

    #endregion

    #region conversions

    static readonly T RadiansInDegrees = T.CreateChecked(Math.PI / 180.0);
    static readonly T GradiansInDegrees = T.CreateChecked(100.0 / 90.0);
    static readonly T RevolutionsInDegrees = T.CreateChecked(1.0 / 360.0);

    public Radians<T> ToRadians<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value * RadiansInDegrees));

    public Degrees<T> ToDegrees<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value));

    public Gradians<T> ToGradians<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value * GradiansInDegrees));

    public Revolutions<T> ToRevolutions<T>()
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.CreateChecked(Value * RevolutionsInDegrees));

    #endregion
}