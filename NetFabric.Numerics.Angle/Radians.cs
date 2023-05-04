using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics.Angle;

public readonly record struct Radians<TRadians>(TRadians Value) 
    : IAngle<Radians<TRadians>>
    where TRadians: 
        struct, 
        IFloatingPoint<TRadians>,
        IMinMaxValue<TRadians>
{
    static TRadians Reduce(TRadians degrees) =>
        Utils.Reduce(degrees, Full.Value);

    static Quadrant GetQuadrant(TRadians degrees) =>
        Utils.GetQuadrant(degrees, Right.Value, Straight.Value, Full.Value);

    static TRadians GetReference(TRadians degrees) =>
        Utils.GetReference(degrees, Right.Value, Straight.Value, Full.Value);

    #region constants

    /// <summary>
    /// Represents the zero angle value (0 radians). This field is read-only.
    /// </summary>
    public static readonly Radians<TRadians> Zero = new(TRadians.Zero);

    /// <summary>
    /// Represents the golden angle value. This field is read-only.
    /// </summary>
    public static readonly Radians<TRadians> Golden = new(TRadians.CreateChecked(Math.PI * (3.0 - Math.Sqrt(5.0))));

    /// <summary>
    /// Represents the right angle value (PI/2 radians). This field is read-only.
    /// </summary>
    public static readonly Radians<TRadians> Right = new(TRadians.CreateChecked(Math.PI / 2.0));

    /// <summary>
    /// Represents the straight angle value (PI radians). This field is read-only.
    /// </summary>
    public static readonly Radians<TRadians> Straight = new(TRadians.CreateChecked(Math.PI));

    /// <summary>
    /// Represents the full angle value (PI*2 radians). This field is read-only.
    /// </summary>
    public static readonly Radians<TRadians> Full = new(TRadians.CreateChecked(Math.PI * 2.0));

    /// <summary>
    /// Represents the minimum angle value. This field is read-only.
    /// </summary>
    public static readonly Radians<TRadians> MinValue = new(TRadians.MinValue);

    /// <summary>
    /// Represents the maximum angle value. This field is read-only.
    /// </summary>
    public static readonly Radians<TRadians> MaxValue = new(TRadians.MaxValue);

    static Radians<TRadians> IAngle<Radians<TRadians>>.Right => Right;
    static Radians<TRadians> IAngle<Radians<TRadians>>.Straight => Straight;
    static Radians<TRadians> IAngle<Radians<TRadians>>.Full => Full;

    static Radians<TRadians> IMinMaxValue<Radians<TRadians>>.MinValue => MinValue;
    static Radians<TRadians> IMinMaxValue<Radians<TRadians>>.MaxValue => MaxValue;

    #endregion

    #region comparison

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int CompareTo(Radians<TRadians> other) =>
        Value.CompareTo(other.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int ReducedCompareTo(Radians<TRadians> other) =>
        Reduce(Value).CompareTo(Reduce(other.Value));

    public static bool operator <(Radians<TRadians> left, Radians<TRadians> right) =>
        left.CompareTo(right) < 0;

    public static bool operator <=(Radians<TRadians> left, Radians<TRadians> right) =>
        left.CompareTo(right) <= 0;

    public static bool operator >(Radians<TRadians> left, Radians<TRadians> right) =>
        left.CompareTo(right) > 0;

    public static bool operator >=(Radians<TRadians> left, Radians<TRadians> right) =>
        left.CompareTo(right) >= 0;        

    readonly int IComparable.CompareTo(object? obj) 
        => obj switch
        {
            null => 1,
            Radians<TRadians> other => CompareTo(other),
            _ => Throw.ArgumentException<int>($"Argument must be of type {nameof(Radians<TRadians>)}.", nameof(obj)),
        };

    #endregion

    #region addition

    public static Radians<TRadians> operator +(Radians<TRadians> value) =>
        new(+value.Value);

    public static Radians<TRadians> operator +(Radians<TRadians> left, Radians<TRadians> right) => 
        new (left.Value + right.Value);

    public static Radians<TRadians> AdditiveIdentity => new (TRadians.AdditiveIdentity);

    #endregion

    #region subtraction

    public static Radians<TRadians> operator -(Radians<TRadians> value) =>
        new(-value.Value);

    public static Radians<TRadians> operator -(Radians<TRadians> left, Radians<TRadians> right) => 
        new (left.Value - right.Value);

    #endregion
}

public static partial class Angle
{
    #region trigonometry

    public static Radians<TRadians> Acos<TRadians>(TRadians cos)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => new(TRadians.CreateChecked(Math.Acos(double.CreateChecked(cos))));

    public static Radians<TRadians> Acos<TCos, TRadians>(TCos cos) 
        where TRadians: struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TCos: IFloatingPoint<TCos>
        => new (TRadians.CreateChecked(Math.Acos(double.CreateChecked(cos))));

    public static Radians<TRadians> Asin<TRadians>(TRadians sin)
    where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
    => new(TRadians.CreateChecked(Math.Asin(double.CreateChecked(sin))));

    public static Radians<TRadians> Asin<TSin, TRadians>(TSin sin)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TSin : IFloatingPoint<TSin>
        => new(TRadians.CreateChecked(Math.Asin(double.CreateChecked(sin))));

    public static Radians<TRadians> Atan<TRadians>(TRadians tan)
    where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
    => new(TRadians.CreateChecked(Math.Atan(double.CreateChecked(tan))));

    public static Radians<TRadians> Atan<TTan, TRadians>(TTan tan)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TTan : IFloatingPoint<TTan>
        => new(TRadians.CreateChecked(Math.Atan(double.CreateChecked(tan))));

    public static Radians<TRadians> Atan2<TRadians>(TRadians x, TRadians y)
    where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
    => new(TRadians.CreateChecked(Math.Atan2(double.CreateChecked(x), double.CreateChecked(y))));

    public static Radians<TRadians> Atan2<TTan, TRadians>(TTan x, TTan y)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TTan : IFloatingPoint<TTan>
        => new(TRadians.CreateChecked(Math.Atan2(double.CreateChecked(x), double.CreateChecked(y))));

    public static double Cos<TRadians>(Radians<TRadians> radians)
        where TRadians: struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Cos(double.CreateChecked(radians.Value));

    public static double Cosh<TRadians>(Radians<TRadians> radians)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Cosh(double.CreateChecked(radians.Value));

    public static double Sin<TRadians>(Radians<TRadians> radians)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Sin(double.CreateChecked(radians.Value));

    public static double Sinh<TRadians>(Radians<TRadians> radians)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Sinh(double.CreateChecked(radians.Value));

    public static double Tan<TRadians>(Radians<TRadians> radians)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Tan(double.CreateChecked(radians.Value));

    #endregion
}