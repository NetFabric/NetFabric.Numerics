using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Markup;

namespace NetFabric.Numerics;

/// <summary>
/// Represents a reduced angle value of type <typeparamref name="T"/> in the specified angle measurement units <typeparamref name="TUnits"/>.
/// </summary>
/// <typeparam name="TUnits">The type representing the angle measurement units.</typeparam>
/// <typeparam name="T">The underlying numeric type of the reduced angle value.</typeparam>
/// <remarks>
/// <para>
/// The <see cref="AngleReduced{TUnits,T}"/> struct provides a generic representation of a reduced angle value with a specific numeric type and measurement units.
/// Unlike the <see cref="Angle{TUnits,T}"/> struct, which allows any angle value, the <see cref="AngleReduced{TUnits,T}"/> struct represents angles in their reduced form.
/// This means that the angle value is always within a specific range, between <see cref="TUnits.Zero"/> and <see cref="TUnits.Full"/>.
/// </para>
/// <para>
/// The struct supports various mathematical operations such as addition, subtraction, multiplication, division, and trigonometric functions,
/// which can be performed on reduced angles of the same measurement units.
/// </para>
/// <para>
/// To create an instance of the <see cref="AngleReduced{TUnits,T}"/> struct, you can use the provided constructors or explicit conversions from other angle types.
/// </para>
/// <para>
/// The <see cref="AngleReduced{TUnits,T}"/> struct can be implicitly converted to <see cref="Angle{TUnits,T}"/>.
/// </para>
/// <para>
/// Note that the <see cref="AngleReduced{TUnits,T}"/> struct is an immutable value type, meaning that its properties cannot be modified after creation.
/// </para>
/// <para>
/// Examples of angle measurement units include <see cref="Degrees"/>, <see cref="Radians"/>, <see cref="Gradians"/>, and <see cref="Revolutions"/>.
/// </para>
/// </remarks>
[DebuggerDisplay("{Value}")]
[DebuggerTypeProxy(typeof(AngleReducedDebugView<,>))]
[SkipLocalsInit]
public readonly struct AngleReduced<TUnits, T>
    : IAngle<AngleReduced<TUnits, T>, T>,
      IUnaryPlusOperators<AngleReduced<TUnits, T>, AngleReduced<TUnits, T>>,
      IUnaryNegationOperators<AngleReduced<TUnits, T>, Angle<TUnits, T>>,
      IAdditionOperators<AngleReduced<TUnits, T>, Angle<TUnits, T>, Angle<TUnits, T>>,
      ISubtractionOperators<AngleReduced<TUnits, T>, Angle<TUnits, T>, Angle<TUnits, T>>,
      IDivisionOperators<AngleReduced<TUnits, T>, T, Angle<TUnits, T>>,
      IModulusOperators<AngleReduced<TUnits, T>, T, Angle<TUnits, T>>
    where TUnits : IAngleUnits<TUnits>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Gets angle measurement in the units specified by <typeparamref name="TUnits"/>.
    /// </summary>
    public T Value { get; }

    /// <summary>
    /// Creates an instance of the current type from a value.
    /// </summary>
    /// <param name="value">The angle measurement in the units specified by <typeparamref name="TUnits"/>.</param>
    public AngleReduced(T value)
    {
        if (T.IsNegative(value) || value >= T.CreateChecked(TUnits.Full))
            Throw.ArgumentOutOfRangeException(nameof(value), value, "Must be positive and less than a full revolution.");
        Value = value;
    }

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The value which is used to create the instance of <see cref="Angle{TUnits, T}"/></param>
    /// <returns>An instance of <see cref="Angle{TUnits, T}"/> created from <paramref name="angle" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="angle" /> is not representable by <see cref="Angle{TUnits, T}"/>.</exception>
    public static AngleReduced<TUnits, T> CreateChecked<TOther>(in AngleReduced<TUnits, TOther> angle)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(T.CreateChecked(angle.Value));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The value which is used to create the instance of <see cref="Angle{TUnits, T}"/></param>
    /// <returns>An instance of <see cref="Angle{TUnits, T}"/> created from <paramref name="angle" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="angle" /> is not representable by <see cref="Angle{TUnits, T}"/>.</exception>
    public static AngleReduced<TUnits, T> CreateSaturating<TOther>(in AngleReduced<TUnits, TOther> angle)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(T.CreateSaturating(angle.Value));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The value which is used to create the instance of <see cref="Angle{TUnits, T}"/></param>
    /// <returns>An instance of <see cref="Angle{TUnits, T}"/> created from <paramref name="angle" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="angle" /> is not representable by <see cref="Angle{TUnits, T}"/>.</exception>
    public static AngleReduced<TUnits, T> CreateTruncating<TOther>(in AngleReduced<TUnits, TOther> angle)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(T.CreateTruncating(angle.Value));

    /// <summary>
    /// Implicitly converts an <see cref="Angle{TUnits, T}"/> to a <see cref="AngleReduced{TUnits, T}"/>.
    /// </summary>
    /// <param name="angle">The angle to convert.</param>
    /// <returns>A <see cref="AngleReduced{TUnits, T}"/> representing the same angle value as the input <see cref="Angle{TUnits, T}"/>.</returns>
    /// <remarks>
    /// This implicit conversion allows for seamless conversion from an <see cref="Angle{TUnits, T}"/> to a <see cref="AngleReduced{TUnits, T}"/>.
    /// The resulting <see cref="AngleReduced{TUnits, T}"/> will have the same angle value as the input <see cref="Angle{TUnits, T}"/>.
    /// </remarks>
    public static implicit operator Angle<TUnits, T>(AngleReduced<TUnits, T> angle)
        => new(angle.Value);

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
    public static bool operator ==(AngleReduced<TUnits, T> left, AngleReduced<TUnits, T> right)
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
    public static bool operator !=(AngleReduced<TUnits, T> left, AngleReduced<TUnits, T> right)
        => !left.Equals(right);

    /// <summary>
    /// Returns the hash code for the current <see cref="Angle{TUnits, T}"/> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
    => EqualityComparer<T>.Default.GetHashCode(Value);

    /// <summary>
    /// Indicates whether the current <see cref="AngleReduced{TUnits, T}"/> instance is equal to another <see cref="Angle{TUnits, T}"/> instance.
    /// </summary>
    /// <param name="other">An <see cref="Angle{TUnits, T}"/> value to compare to this instance.</param>
    /// <returns>true if <paramref name="other"/> has the same value as this instance; otherwise, false.</returns>
    public bool Equals(Angle<TUnits, T> other)
        => EqualityComparer<T>.Default.Equals(Value, other.Value);

    /// <summary>
    /// Indicates whether the current <see cref="AngleReduced{TUnits, T}"/> instance is equal to another <see cref="AngleReduced{TUnits, T}"/> instance.
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

    #region constants

    /// <summary>
    /// Represents the minimum angle value. This field is read-only.
    /// </summary>
    public static readonly AngleReduced<TUnits, T> MinValue = new(T.Zero);

    /// <summary>
    /// Represents the maximum angle value. This field is read-only.
    /// </summary>
    public static readonly AngleReduced<TUnits, T> MaxValue = new(T.CreateChecked(TUnits.Full - double.Epsilon));

    static AngleReduced<TUnits, T> IAdditiveIdentity<AngleReduced<TUnits, T>, AngleReduced<TUnits, T>>.AdditiveIdentity
        => new(T.AdditiveIdentity);
    static AngleReduced<TUnits, T> IMultiplicativeIdentity<AngleReduced<TUnits, T>, AngleReduced<TUnits, T>>.MultiplicativeIdentity
        => new(T.MultiplicativeIdentity);

    static AngleReduced<TUnits, T> IMinMaxValue<AngleReduced<TUnits, T>>.MinValue 
        => MinValue;
    static AngleReduced<TUnits, T> IMinMaxValue<AngleReduced<TUnits, T>>.MaxValue 
        => MaxValue;

    #endregion

    #region comparison

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int CompareTo(AngleReduced<TUnits, T> other)
        => Value.CompareTo(other.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(AngleReduced<TUnits, T> left, AngleReduced<TUnits, T> right)
        => left.CompareTo(right) < 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(AngleReduced<TUnits, T> left, AngleReduced<TUnits, T> right)
        => left.CompareTo(right) <= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(AngleReduced<TUnits, T> left, AngleReduced<TUnits, T> right)
        => left.CompareTo(right) > 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(AngleReduced<TUnits, T> left, AngleReduced<TUnits, T> right)
        => left.CompareTo(right) >= 0;

    readonly int IComparable.CompareTo(object? obj)
        => obj switch
        {
            null => 1,
            AngleReduced<TUnits, T> other => CompareTo(other),
            _ => Throw.ArgumentException<int>($"Argument must be of type {nameof(AngleReduced<TUnits, T>)}.", nameof(obj)),
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
    public static AngleReduced<TUnits, T> operator +(AngleReduced<TUnits, T> right)
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
    public static Angle<TUnits, T> operator +(AngleReduced<TUnits, T> left, AngleReduced<TUnits, T> right)
        => new(left.Value + right.Value);


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
    public static Angle<TUnits, T> operator +(AngleReduced<TUnits, T> left, Angle<TUnits, T> right)
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
    public static Angle<TUnits, T> operator -(AngleReduced<TUnits, T> right)
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
    public static Angle<TUnits, T> operator -(AngleReduced<TUnits, T> left, AngleReduced<TUnits, T> right)
        => new(left.Value - right.Value);

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
    public static Angle<TUnits, T> operator -(AngleReduced<TUnits, T> left, Angle<TUnits, T> right)
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
    public static Angle<TUnits, T> operator *(T left, AngleReduced<TUnits, T> right)
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
    public static Angle<TUnits, T> operator /(AngleReduced<TUnits, T> left, T right)
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
    public static Angle<TUnits, T> operator %(AngleReduced<TUnits, T> left, T right)
        => new(left.Value % right);

    #endregion

    /// <summary>
    /// Returns a string representation of the current angle.
    /// </summary>
    /// <returns>A string that represents the current angle.</returns>
    /// <remarks>
    /// The string representation of the angle includes the numerical value followed by the unit of measurement (e.g., º, rad, grad, or rev).
    /// </remarks>
    public override readonly string? ToString()
        => Value.ToString();

    /// <summary>Tries to format the value of the current instance into the provided span of characters.</summary>
    /// <param name="destination">When this method returns, this instance's value formatted as a span of characters.</param>
    /// <param name="charsWritten">When this method returns, the number of characters that were written in <paramref name="destination"/>.</param>
    /// <param name="format">A span containing the characters that represent a standard or custom format string that defines the acceptable format for <paramref name="destination"/>.</param>
    /// <param name="provider">An optional object that supplies culture-specific formatting information for <paramref name="destination"/>.</param>
    /// <returns><see langword="true"/> if the formatting was successful; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// An implementation of this interface should produce the same string of characters as an implementation of <see cref="IFormattable.ToString(string?, IFormatProvider?)"/>
    /// on the same type.
    /// TryFormat should return false only if there is not enough space in the destination buffer. Any other failures should throw an exception.
    /// </remarks>
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Value.TryFormat(destination, out charsWritten, format, provider);

    /// <summary>
    /// Returns a string representation of the current angle using the specified format.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns></returns>
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
        => Value.ToString(format, formatProvider);
}