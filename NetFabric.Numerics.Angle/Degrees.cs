using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics.Angle;

public readonly record struct Degrees<TDegrees>(TDegrees Value) 
    : IAngle<Degrees<TDegrees>>
    where TDegrees: 
        struct, 
        IFloatingPoint<TDegrees>, 
        IMinMaxValue<TDegrees>
{
    static TDegrees Reduce(TDegrees degrees) =>
        Utils.Reduce(degrees, Full.Value);

    static Quadrant GetQuadrant(TDegrees degrees) =>
        Utils.GetQuadrant(degrees, Right.Value, Straight.Value, Full.Value);

    static TDegrees GetReference(TDegrees degrees) =>
        Utils.GetReference(degrees, Right.Value, Straight.Value, Full.Value);

    #region constants

    /// <summary>
    /// Represents the zero angle value (0 degrees). This field is read-only.
    /// </summary>
    public static readonly Degrees<TDegrees> Zero = new(TDegrees.Zero);

    /// <summary>
    /// Represents the right angle value (90 degrees). This field is read-only.
    /// </summary>
    public static readonly Degrees<TDegrees> Right = new(TDegrees.CreateChecked(90.0));

    /// <summary>
    /// Represents the straight angle value (180 degrees). This field is read-only.
    /// </summary>
    public static readonly Degrees<TDegrees> Straight = new(TDegrees.CreateChecked(180.0));

    /// <summary>
    /// Represents the full angle value (360 degrees). This field is read-only.
    /// </summary>
    public static readonly Degrees<TDegrees> Full = new(TDegrees.CreateChecked(360.0));

    /// <summary>
    /// Represents the minimum angle value. This field is read-only.
    /// </summary>
    public static readonly Degrees<TDegrees> MinValue = new(TDegrees.MinValue);

    /// <summary>
    /// Represents the maximum angle value. This field is read-only.
    /// </summary>
    public static readonly Degrees<TDegrees> MaxValue = new(TDegrees.MaxValue);

    static Degrees<TDegrees> IAngle<Degrees<TDegrees>>.Right => Right;
    static Degrees<TDegrees> IAngle<Degrees<TDegrees>>.Straight => Straight;
    static Degrees<TDegrees> IAngle<Degrees<TDegrees>>.Full => Full;
    
    static Degrees<TDegrees> IMinMaxValue<Degrees<TDegrees>>.MinValue => MinValue;
    static Degrees<TDegrees> IMinMaxValue<Degrees<TDegrees>>.MaxValue => MaxValue;
    #endregion

    #region comparison

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int CompareTo(Degrees<TDegrees> other) =>
        Value.CompareTo(other.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int ReducedCompareTo(Degrees<TDegrees> other) =>
        Reduce(Value).CompareTo(Reduce(other.Value));

    public static bool operator <(Degrees<TDegrees> left, Degrees<TDegrees> right) =>
        left.CompareTo(right) < 0;

    public static bool operator <=(Degrees<TDegrees> left, Degrees<TDegrees> right) =>
        left.CompareTo(right) <= 0;

    public static bool operator >(Degrees<TDegrees> left, Degrees<TDegrees> right) =>
        left.CompareTo(right) > 0;

    public static bool operator >=(Degrees<TDegrees> left, Degrees<TDegrees> right) =>
        left.CompareTo(right) >= 0;        

    readonly int IComparable.CompareTo(object? obj) 
        => obj switch
        {
            null => 1,
            Degrees<TDegrees> other => CompareTo(other),
            _ => Throw.ArgumentException<int>($"Argument must be of type {nameof(Degrees<TDegrees>)}.", nameof(obj)),
        };

    #endregion

    #region addition

    public static Degrees<TDegrees> operator +(Degrees<TDegrees> value) =>
        new(+value.Value);

    public static Degrees<TDegrees> operator +(Degrees<TDegrees> left, Degrees<TDegrees> right) => 
        new (left.Value + right.Value);

    public static Degrees<TDegrees> AdditiveIdentity => new (TDegrees.AdditiveIdentity);

    #endregion

    #region subtraction

    public static Degrees<TDegrees> operator -(Degrees<TDegrees> value) =>
        new(-value.Value);

    public static Degrees<TDegrees> operator -(Degrees<TDegrees> left, Degrees<TDegrees> right) => 
        new (left.Value - right.Value);

    #endregion
}