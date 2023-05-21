using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics;

/// <summary>
/// Represents an angle value of type <typeparamref name="T"/> in the specified angle measurement units <typeparamref name="TUnits"/>.
/// </summary>
/// <typeparam name="TUnits">The type representing the angle measurement units.</typeparam>
/// <typeparam name="T">The underlying numeric type of the angle value.</typeparam>
/// <remarks>
/// <para>
/// The <see cref="Angle{TUnits,T}"/> struct provides a generic representation of an angle value with a specific numeric type and measurement units.
/// It allows you to work with angles in a type-safe manner and perform angle-related operations using the specified measurement units.
/// The angle measurement units are defined by the type <typeparamref name="TUnits"/>, while the underlying numeric value of the angle is of type <typeparamref name="T"/>.
/// </para>
/// <para>
/// The struct supports various mathematical operations such as addition, subtraction, multiplication, division, and trigonometric functions,
/// which can be performed on angles of the same measurement units.
/// </para>
/// <para>
/// To create an instance of the <see cref="Angle{TUnits,T}"/> struct, you can use the provided constructors or explicit conversions from other angle types.
/// </para>
/// <para>
/// Note that the <see cref="Angle{TUnits,T}"/> struct is an immutable value type, meaning that its properties cannot be modified after creation.
/// </para>
/// <para>
/// Examples of angle measurement units include <see cref="Degrees"/>, <see cref="Radians"/>, <see cref="Gradians"/>, and <see cref="Revolutions"/>.
/// </para>
/// </remarks>
[DebuggerTypeProxy(typeof(AngleDebugView<,>))]
public struct Angle<TUnits, T>
    : IEquatable<Angle<TUnits, T>>,
      IEqualityOperators<Angle<TUnits, T>, Angle<TUnits, T>, bool>,
      IComparable,
      IComparisonOperators<Angle<TUnits, T>, Angle<TUnits, T>, bool>,
      IAdditiveIdentity<Angle<TUnits, T>, Angle<TUnits, T>>,
      IMultiplicativeIdentity<Angle<TUnits, T>, Angle<TUnits, T>>,
      IUnaryPlusOperators<Angle<TUnits, T>, Angle<TUnits, T>>,
      IAdditionOperators<Angle<TUnits, T>, Angle<TUnits, T>, Angle<TUnits, T>>,
      IAdditionOperators<Angle<TUnits, T>, AngleReduced<TUnits, T>, Angle<TUnits, T>>,
      IUnaryNegationOperators<Angle<TUnits, T>, Angle<TUnits, T>>,
      ISubtractionOperators<Angle<TUnits, T>, Angle<TUnits, T>, Angle<TUnits, T>>,
      ISubtractionOperators<Angle<TUnits, T>, AngleReduced<TUnits, T>, Angle<TUnits, T>>,
      IDivisionOperators<Angle<TUnits, T>, T, Angle<TUnits, T>>,
      IModulusOperators<Angle<TUnits, T>, T, Angle<TUnits, T>>,
      IMinMaxValue<Angle<TUnits, T>>
    where TUnits : IAngleUnits<TUnits>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    public T Value { get; }

    public Angle(T value)
    {
        Value = value; 
    }

    #region constants

    /// <summary>
    /// Represents the zero angle value (0 Degrees). This field is read-only.
    /// </summary>
    public static readonly AngleReduced<TUnits, T> Zero = new(T.CreateChecked(TUnits.Zero));

    /// <summary>
    /// Represents the right angle value (90 Degrees). This field is read-only.
    /// </summary>
    public static readonly AngleReduced<TUnits, T> Right = new(T.CreateChecked(TUnits.Right));

    /// <summary>
    /// Represents the straight angle value (180 Degrees). This field is read-only.
    /// </summary>
    public static readonly AngleReduced<TUnits, T> Straight = new(T.CreateChecked(TUnits.Straight));

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

    #region equality

    /// <summary>
    /// Indicates whether two <see cref="Angle{TUnits, T}"/> instances are equal.
    /// </summary>
    /// <param name="left">The first angle to compare.</param>
    /// <param name="right">The second angle to compare.</param>
    /// <returns>true if the two angles are equal, false otherwise.</returns>
    /// <remarks>
    /// The method compares the numerical values of the <paramref name="left"/> and <paramref name="right"/> angles to determine their equality.
    /// </remarks>
    public static bool operator ==(Angle<TUnits, T> left, Angle<TUnits, T> right)
        => left.Equals(right);

    /// <summary>
    /// Indicates whether two <see cref="Angle{TUnits, T}"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first angle to compare.</param>
    /// <param name="right">The second angle to compare.</param>
    /// <returns>true if the two angles are equal, false otherwise.returns>
    /// <remarks>
    /// The method compares the numerical values of the <paramref name="left"/> and <paramref name="right"/> angles to determine their equality.
    /// </remarks>
    public static bool operator !=(Angle<TUnits, T> left, Angle<TUnits, T> right)
        => !left.Equals(right);

    /// <summary>
    /// Returns the hash code for the current <see cref="Angle{TUnits, T}"/> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
    => EqualityComparer<T>.Default.GetHashCode(Value);

    /// <summary>
    /// Indicates whether the current <see cref="Angle{TUnits, T}"/> instance is equal to another <see cref="Angle{TUnits, T}"/> instance.
    /// </summary>
    /// <param name="other">An <see cref="Angle{TUnits, T}"/> value to compare to this instance.</param>
    /// <returns>true if <paramref name="other"/> has the same value as this instance; otherwise, false.</returns>
    public bool Equals(Angle<TUnits, T> other)
        => EqualityComparer<T>.Default.Equals(Value, other.Value);

    /// <summary>
    /// Indicates whether the current <see cref="Angle{TUnits, T}"/> instance is equal to another <see cref="AngleReduced{TUnits, T}"/> instance.
    /// </summary>
    /// <param name="other">An <see cref="AngleReduced{TUnits, T}"/> value to compare to this instance.</param>
    /// <returns>true if <paramref name="other"/> has the same value as this instance; otherwise, false.</returns>
    public bool Equals(AngleReduced<TUnits, T> other)
        => EqualityComparer<T>.Default.Equals(Value, other.Value);

    /// <summary>
    /// Indicates whether the current <see cref="Angle{TUnits, T}"/> instance is equal to another object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>
    /// true if <paramref name="obj"/> is an instance of an <see cref="Angle{TUnits, T}"/> or 
    /// <see cref="AngleReduced{TUnits, T}"/> and equals the value of this instance; otherwise, false.
    /// </returns>
    public override bool Equals(object? obj)
        => obj switch
        {
            Angle<TUnits, T> angle => Equals(angle),
            AngleReduced<TUnits, T> angle => Equals(angle),
            _ => false
        };

    #endregion

    #region comparison

    /// <summary>
    /// Compares the current <see cref="Angle{TUnits, T}"/> instance to another <see cref="Angle{TUnits, T}"/> instance.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int CompareTo(Angle<TUnits, T> other) 
        => Value.CompareTo(other.Value);

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

    /// <summary>
    /// Unary plus operator for an angle multiplied by a number.
    /// </summary>
    /// <param name="right">The number to multiply the angle by.</param>
    /// <returns>The resulting angle after the multiplication.</returns>
    /// <remarks>
    /// The unary plus operator allows multiplying an angle by a positive number, effectively preserving the direction
    /// of rotation and scaling up the magnitude of the angle. The resulting angle represents the original angle
    /// multiplied by the scalar value of the <paramref name="right"/> number. For example, if the angle is 90 degrees
    /// in the counterclockwise direction and the <paramref name="right"/> number is 2, the resulting angle would be
    /// 180 degrees in the counterclockwise direction. Note that this operator has no effect on negative numbers as it
    /// doesn't change the direction of rotation.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator +(Angle<TUnits, T> right) 
        => right;

    /// <summary>
    /// Addition operator for two angles.
    /// </summary>
    /// <param name="left">The first angle.</param>
    /// <param name="right">The second angle.</param>
    /// <returns>The resulting angle after the addition.</returns>
    /// <remarks>
    /// The addition operator combines two angles and produces a new angle that represents their combined rotation.
    /// Adding <paramref name="right"/> angle to <paramref name="left"/> angle results in an angle that corresponds
    /// to the total rotation obtained by first rotating by the <paramref name="left"/> angle and then by the
    /// <paramref name="right"/> angle. The direction of rotation is preserved in the resulting angle.
    /// For example, if the <paramref name="left"/> angle is 90 degrees in the counterclockwise direction and the
    /// <paramref name="right"/> angle is 45 degrees in the counterclockwise direction, the resulting angle would
    /// be 135 degrees in the counterclockwise direction. Adding angles in the opposite direction will subtract the
    /// smaller angle from the larger angle, resulting in a new angle that represents the difference in rotation.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator +(Angle<TUnits, T> left, Angle<TUnits, T> right) 
        => new (left.Value + right.Value);

    /// <summary>
    /// Addition operator for two angles.
    /// </summary>
    /// <param name="left">The first angle.</param>
    /// <param name="right">The second angle.</param>
    /// <returns>The resulting angle after the addition.</returns>
    /// <remarks>
    /// The addition operator combines two angles and produces a new angle that represents their combined rotation.
    /// Adding <paramref name="right"/> angle to <paramref name="left"/> angle results in an angle that corresponds
    /// to the total rotation obtained by first rotating by the <paramref name="left"/> angle and then by the
    /// <paramref name="right"/> angle. The direction of rotation is preserved in the resulting angle.
    /// For example, if the <paramref name="left"/> angle is 90 degrees in the counterclockwise direction and the
    /// <paramref name="right"/> angle is 45 degrees in the counterclockwise direction, the resulting angle would
    /// be 135 degrees in the counterclockwise direction. Adding angles in the opposite direction will subtract the
    /// smaller angle from the larger angle, resulting in a new angle that represents the difference in rotation.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator +(Angle<TUnits, T> left, AngleReduced<TUnits, T> right)
        => new(left.Value + right.Value);

    #endregion

    #region subtraction

    /// <summary>
    /// Unary negation operator for an angle.
    /// </summary>
    /// <param name="right">The angle to negate.</param>
    /// <returns>The negated angle.</returns>
    /// <remarks>
    /// The unary negation operator calculates the opposite direction of rotation for an angle by negating its value.
    /// Negating the <paramref name="right"/> angle will produce a new angle with the same magnitude but in the
    /// opposite direction of rotation. If the <paramref name="right"/> angle is positive, the resulting angle will be
    /// negative, and vice versa.
    /// For example, if the <paramref name="right"/> angle is 45 degrees in the counterclockwise direction, the resulting
    /// angle would be -45 degrees in the clockwise direction. Similarly, if the <paramref name="right"/> angle is -90 degrees
    /// in the clockwise direction, the resulting angle would be 90 degrees in the counterclockwise direction.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator -(Angle<TUnits, T> right) 
        => new(-right.Value);

    /// <summary>
    /// Subtraction operator for two angles.
    /// </summary>
    /// <param name="left">The angle to subtract from.</param>
    /// <param name="right">The angle to subtract.</param>
    /// <returns>The resulting angle after the subtraction.</returns>
    /// <remarks>
    /// The subtraction operator calculates the difference between two angles and produces a new angle that represents
    /// the relative rotation obtained by subtracting the <paramref name="right"/> angle from the <paramref name="left"/>
    /// angle. The resulting angle represents the rotation needed to go from the <paramref name="right"/> angle to the
    /// <paramref name="left"/> angle. The direction of rotation is preserved in the resulting angle.
    /// For example, if the <paramref name="left"/> angle is 180 degrees in the counterclockwise direction and the
    /// <paramref name="right"/> angle is 45 degrees in the counterclockwise direction, the resulting angle would be
    /// 135 degrees in the counterclockwise direction, representing the rotation needed to go from 45 degrees to 180 degrees.
    /// Subtracting angles in the opposite direction will produce a new angle that represents the difference in rotation.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator -(Angle<TUnits, T> left, Angle<TUnits, T> right) 
        => new (left.Value - right.Value);

    /// <summary>
    /// Subtraction operator for two angles.
    /// </summary>
    /// <param name="left">The angle to subtract from.</param>
    /// <param name="right">The angle to subtract.</param>
    /// <returns>The resulting angle after the subtraction.</returns>
    /// <remarks>
    /// The subtraction operator calculates the difference between two angles and produces a new angle that represents
    /// the relative rotation obtained by subtracting the <paramref name="right"/> angle from the <paramref name="left"/>
    /// angle. The resulting angle represents the rotation needed to go from the <paramref name="right"/> angle to the
    /// <paramref name="left"/> angle. The direction of rotation is preserved in the resulting angle.
    /// For example, if the <paramref name="left"/> angle is 180 degrees in the counterclockwise direction and the
    /// <paramref name="right"/> angle is 45 degrees in the counterclockwise direction, the resulting angle would be
    /// 135 degrees in the counterclockwise direction, representing the rotation needed to go from 45 degrees to 180 degrees.
    /// Subtracting angles in the opposite direction will produce a new angle that represents the difference in rotation.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator -(Angle<TUnits, T> left, AngleReduced<TUnits, T> right)
        => new(left.Value - right.Value);

    #endregion

    #region multiplication

    /// <summary>
    /// Multiplication operator for a number multiplied by an angle.
    /// </summary>
    /// <param name="left">The number to multiply the angle by.</param>
    /// <param name="right">The angle.</param>
    /// <returns>The resulting angle after the multiplication.</returns>
    /// <remarks>
    /// The multiplication operator allows scaling up the magnitude of an angle by multiplying it with a number,
    /// while preserving the direction of rotation. The resulting angle represents the original angle multiplied
    /// by the scalar value of the <paramref name="left"/> number. For example, if the <paramref name="left"/> number
    /// is 2 and the angle is 90 degrees in the counterclockwise direction, the resulting angle would be 180 degrees
    /// in the counterclockwise direction. Note that multiplying the angle by a negative number will reverse the
    /// direction of rotation.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator *(T left, Angle<TUnits, T> right)
        => new(left * right.Value);

    #endregion

    #region division

    /// <summary>
    /// Division operator for an angle divided by a number.
    /// </summary>
    /// <param name="left">The angle to be divided.</param>
    /// <param name="right">The number to divide the angle by.</param>
    /// <returns>The resulting angle after the division.</returns>
    /// <remarks>
    /// Dividing <paramref name="left"/> angle by <paramref name="right"/> involves scaling down the magnitude of
    /// the angle by the value of the number, while preserving the direction of rotation. The resulting angle represents
    /// the original angle divided by the scalar value of the <paramref name="right"/> number. For example, if the
    /// <paramref name="left"/> angle is 180 degrees in the counterclockwise direction and the <paramref name="right"/>
    /// number is 2, the resulting angle would be 90 degrees in the counterclockwise direction. This division operation
    /// effectively reduces the magnitude of the angle by dividing it by the number.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator /(Angle<TUnits, T> left, T right)
        => new(left.Value / right);

    /// <summary>
    /// Remainder operator for an angle divided by a number.
    /// </summary>
    /// <param name="left">The angle to be divided.</param>
    /// <param name="right">The number to divide the angle by.</param>
    /// <returns>The remainder angle after the division.</returns>
    /// <remarks>
    /// Taking the remainder of <paramref name="left"/> angle divided by <paramref name="right"/> involves finding
    /// the angle that remains after the division, while preserving the direction of rotation. The resulting angle
    /// represents the remainder obtained when dividing the original angle by the scalar value of the number.
    /// For example, if the <paramref name="left"/> angle is 270 degrees in the counterclockwise direction and the
    /// <paramref name="right"/> number is 90, the remainder would be 0 degrees in the counterclockwise direction
    /// since 270 divided by 90 equals 3 with no remainder. In this case, the remainder angle would be 0 degrees
    /// because the division results in an exact quotient without any leftover angle.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator %(Angle<TUnits, T> left, T right)
        => new(left.Value % right);

    #endregion

    /// <summary>
    /// Returns a string representation of the current angle.
    /// </summary>
    /// <returns>A string that represents the current angle.</returns>
    /// <remarks>
    /// The string representation of the angle includes the numerical value followed by the unit of measurement (e.g., º, rad, grad, or rev).
    /// </remarks>
    public override readonly string ToString()
        => $"{Value}{TUnits.Symbol}";

}