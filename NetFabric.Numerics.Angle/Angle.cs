﻿using System.Numerics;
using System;
using System.Collections.Generic;

namespace NetFabric.Numerics;

public static partial class Angle
{
    /// <summary>
    /// Gets the reduced angle of <paramref name="angle" />.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/> and the reduced angle.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle to get the reduced angle from.</param>
    /// <remarks>
    /// A reduced angle is an angle that is within one full revolution (360 degrees or 2π radians) 
    /// and is expressed as an angle between 0 and 360 degrees or between 0 and 2π radians. In 
    /// other words, a reduced angle is an angle that has been simplified by subtracting or adding
    /// multiples of 360 degrees or 2π radians until it falls within this range. Reduced angles 
    /// are commonly used in trigonometry to simplify calculations and to find the primary 
    /// solution of trigonometric equations.
    /// </remarks>
    /// <returns>The reduced angle of <paramref name="angle"/>.</returns>
    public static AngleReduced<TUnits, T> Reduce<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(Utils.Reduce(angle));

    /// <summary>
    /// Gets the reduced angle of <paramref name="angle" />.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/> and the reduced angle.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle to get the reduced angle from.</param>
    /// <remarks>
    /// A reduced angle is an angle that is within one full revolution (360 degrees or 2π radians) 
    /// and is expressed as an angle between 0 and 360 degrees or between 0 and 2π radians. In 
    /// other words, a reduced angle is an angle that has been simplified by subtracting or adding
    /// multiples of 360 degrees or 2π radians until it falls within this range. Reduced angles 
    /// are commonly used in trigonometry to simplify calculations and to find the primary 
    /// solution of trigonometric equations.
    /// </remarks>
    /// <returns>The reduced angle of <paramref name="angle"/>.</returns>
    public static AngleReduced<TUnits, T> Reduce<TUnits, T>(AngleReduced<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    /// <summary>
    /// Gets the reference angle of <paramref name="angle" />.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/> and the reference angle.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle to get the reference angle from.</param>
    /// <remarks>
    /// A reference angle is the positive acute angle formed between the terminal side of an angle 
    /// in standard position (i.e., starting from the positive x-axis) and the x-axis. It is always 
    /// measured in a counterclockwise direction and has the same trigonometric function values 
    /// (sine, cosine, tangent, etc.) as the original angle or its coterminal angles. Reference 
    /// angles are often used to simplify calculations involving trigonometric functions and to 
    /// find the general solutions of trigonometric equations.
    /// </remarks>
    /// <returns>The reference angle of <paramref name="angle"/>.</returns>
    public static Angle<TUnits, T> GetReference<TUnits, T>(AngleReduced<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(Utils.GetReference(angle));

    /// <summary>
    /// Gets the quadrant of <paramref name="angle" />.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle"></param>
    /// <remarks>
    /// <para>
    /// The quadrant of an angle is a region of the coordinate plane that is determined by the signs
    /// of the x and y coordinates of a point on the terminal side of the angle in standard position
    /// (i.e., starting from the positive x-axis). There are four quadrants in the coordinate plane,
    /// numbered counterclockwise from <see cref="Quadrant.First" /> to <see cref="Quadrant.Fourth" />. 
    /// <see cref="Quadrant.First" /> is where both x and y coordinates are positive, 
    /// <see cref="Quadrant.Second" /> is where x is negative and y is positive, 
    /// <see cref="Quadrant.Third" /> is where both x and y coordinates are negative, and 
    /// <see cref="Quadrant.Fourth" /> is where x is positive and y is negative. 
    /// The quadrant of an angle is used to determine the sign of its trigonometric 
    /// functions (sine, cosine, tangent, etc.) and to locate its reference angle.
    /// </para>
    /// <para>
    /// A quadrantal angle is an angle whose terminal side lies on one of the coordinate axes 
    /// (i.e., the x-axis or the y-axis) in standard position. Quadrantal angles have a measure
    /// of 0 degrees, 90 degrees, 180 degrees, or 270 degrees, or an equivalent radian measure. 
    /// In other words, they are angles that are exact multiples of 90 degrees or π/2 radians. 
    /// Quadrantal angles have special properties in trigonometry, such as having a sine or cosine 
    /// value of 1 or -1, and are often used in solving problems involving trigonometric functions.
    /// </para>
    /// </remarks>
    /// <returns>The quadrant of <paramref name="angle"/>.</returns>
    public static Quadrant GetQuadrant<TUnits, T>(AngleReduced<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.GetQuadrant(angle);

    /// <summary>
    /// Returns the absolute value of <paramref name="angle" />.
    /// </summary>
    /// <param name="angle">The angle for which to get its absolute value.</param>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle for which to get its absolute value.</param>
    /// <remarks>
    /// The absolute value of an angle is the smallest angle that can be added or subtracted from 
    /// the original angle to bring it within the range of -π to π radians (or -180 to 180 degrees). 
    /// It is a non-negative value that represents the distance between the angle and zero on the 
    /// number line. The notation used to denote the absolute value of an angle is two vertical 
    /// bars enclosing the angle, like this: |θ|.
    /// </remarks>
    /// <returns>
    /// The absolute values of <paramref name="angle" />.
    /// </returns>
    public static Angle<TUnits, T> Abs<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.Abs(angle.Value));

    /// <summary>
    /// Returns a value indicating the sign of <paramref name="angle" />.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">Source angle.</param>
    /// <returns>A number that indicates the sign of value, -1 if value is less than zero, 0 if value equal to zero, 1 if value is grater than zero.</returns>
    public static int Sign<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => T.Sign(angle.Value);

    /// <summary>
    /// Performs a linear interpolation.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="a1"/> and <paramref name="a2"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="a1"/> and <paramref name="a2"/>.</typeparam>
    /// <param name="a1">The first angle.</param>
    /// <param name="a2">The second angle.</param>
    /// <param name="t">A value that linearly interpolates between the a1 parameter and the a2 parameter.</param>
    /// <remarks>
    /// <para>
    /// The Lerp (Linear Interpolation) operation calculates a value that lies somewhere between 
    /// <paramref name="a1"/> and <paramref name="a2"/>, based on <paramref name="t"/> that represents 
    /// the position between those endpoints. 
    /// The formula for the Lerp function is: 
    /// result = (1 - <paramref name="t"/>) * <paramref name="a1"/> + <paramref name="t"/> * <paramref name="a2"/>, 
    /// where <paramref name="t"/> is usually a value between 0 and 1.
    /// </para>
    /// <para>
    /// The Lerp function can also support values less than zero and greater than one for <paramref name="t"/>, 
    /// allowing for extrapolation beyond the range defined by the start and end values. 
    /// If the third value is less than zero, the Lerp function returns a value that is extrapolated 
    /// in the negative direction. Similarly, if <paramref name="t"/> is greater than one, the Lerp 
    /// function returns a value that is extrapolated in the positive direction.
    /// </para>
    /// </remarks>
    /// <returns>The result of the linear interpolation.</returns>
    public static Angle<TUnits, T> Lerp<TUnits, T, TFactor>(Angle<TUnits, T> a1, Angle<TUnits, T> a2, TFactor t)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        where TFactor : struct, IFloatingPoint<TFactor>
        => new(Utils.Lerp<T, TFactor>(a1.Value, a2.Value, t));

    /// <summary>
    /// Returns the smallest of two angles.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="left"/> and <paramref name="right"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="left"/> and <paramref name="right"/>.</typeparam>
    /// <param name="left">The first of two angles to compare.</param>
    /// <param name="right">The second of two angles to compare.</param>
    /// <returns>The smallest of the two angles.</returns>
    public static Angle<TUnits, T> Min<TUnits, T>(Angle<TUnits, T> left, Angle<TUnits, T> right)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.Min(left.Value, right.Value));

    /// <summary>
    /// Returns the largest of two angles.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="left"/> and <paramref name="right"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="left"/> and <paramref name="right"/>.</typeparam>
    /// <param name="left">The first of two angles to compare.</param>
    /// <param name="right">The second of two angles to compare.</param>
    /// <returns>The largest of the two angles.</returns>
    public static Angle<TUnits, T> Max<TUnits, T>(Angle<TUnits, T> left, Angle<TUnits, T> right)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.Max(left.Value, right.Value));

    #region classification

    /// <summary>
    /// Indicates whether the specified angle is equal to Zero.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the reduction of the absolute angle is zero; otherwise false.</returns>
    public static bool IsZero<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle % Angle<TUnits, T>.Full.Value == Angle<TUnits, T>.Zero;

    /// <summary>
    /// Indicates whether the specified angle is acute.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">Source angle.</param>
    /// <remarks>
    /// An acute angle is a type of angle that measures less than 90 degrees (π/2 radians). 
    /// It is formed by the intersection of two straight lines or line segments that do not 
    /// create a right angle. 
    /// </remarks>
    /// <returns>true if the reduction of the absolute angle is greater than zero and less than 90 degrees; otherwise false.</returns>
    public static bool IsAcute<TUnits, T>(AngleReduced<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle.Value > T.Zero && angle.Value < Angle<TUnits, T>.Right.Value;

    /// <summary>
    /// Indicates whether the specified angle is right.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">Source angle.</param>
    /// <remarks>
    /// A right angle is an angle that measures exactly 90 degrees (π/2 radians) and is formed by the 
    /// intersection of two straight lines or line segments that are perpendicular to each other. 
    /// It is a quarter turn, 90 degrees, π/2 radians, or 100 gradians
    /// </remarks>
    /// <returns>true if the reduction of the absolute angle is 90 degrees; otherwise false.</returns>
    public static bool IsRight<TUnits, T>(AngleReduced<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle.Value == Angle<TUnits, T>.Right.Value;

    /// <summary>
    /// Indicates whether the specified angle is obtuse.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">Source angle.</param>
    /// <remarks>
    /// An obtuse angle is a type of angle that measures more than 90 degrees (π/2 radians) but less 
    /// than 180 degrees (π radians). It is formed by the intersection of two straight lines or line 
    /// segments that create a turn greater than a right angle. 
    /// </remarks>
    /// <returns>true if the reduction of the absolute angle is greater than 90 degrees and less than 180 degrees; otherwise false.</returns>
    public static bool IsObtuse<TUnits, T>(AngleReduced<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle.Value > Angle<TUnits, T>.Right.Value && angle.Value < Angle<TUnits, T>.Straight.Value;

    /// <summary>
    /// Indicates whether the specified angle is straight.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">Source angle.</param>
    /// <remarks>
    /// A straight angle is a type of angle that measures exactly 180 degrees (π radians). It is formed
    /// by the intersection of two straight lines or line segments that create a straight line. 
    /// It is a half turn, 180 degrees, π radians, or 200 gradians
    /// </remarks>
    /// <returns>true if the reduction of the absolute angle is 180 degrees; otherwise false.</returns>
    public static bool IsStraight<TUnits, T>(AngleReduced<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle.Value == Angle<TUnits, T>.Straight.Value;

    /// <summary>
    /// Indicates whether the specified angle is reflex.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">Source angle.</param>
    /// <remarks>
    /// A reflex angle is a type of angle that measures more than 180 degrees (π radians) but less than 
    /// 360 degrees (2π radians). It is formed by the intersection of two straight lines or line segments 
    /// that create a turn greater than a straight angle. 
    /// </remarks>
    /// <returns>true if the reduction of the absolute angle is greater than 180 degrees and less than 360 degrees; otherwise false.</returns>
    public static bool IsReflex<TUnits, T>(AngleReduced<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle.Value > Angle<TUnits, T>.Straight.Value;

    /// <summary>
    /// Indicates whether the specified angle is oblique.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">Source angle.</param>
    /// <remarks>
    /// An oblique angle is a type of angle that is not a right angle (90 degrees) or a multiple of a 
    /// right angle. It is formed by the intersection of two non-perpendicular lines or line segments 
    /// that create a turn that is neither a right angle nor a straight angle.
    /// </remarks>
    /// <returns>true if the angle is not right or a multiple of a right angle; otherwise false.</returns>
    public static bool IsOblique<TUnits, T>(AngleReduced<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle.Value % Angle<TUnits, T>.Right.Value != T.Zero;

    /// <summary>
    /// Checks if two angles are complementary.
    /// </summary>
    /// <param name="first">The first angle to compare.</param>
    /// <param name="second">The second angle to compare.</param>
    /// <returns>True if the two angles are complementary, false otherwise.</returns>
    /// <remarks>
    /// Two angles are considered complementary if their sum equals a right angle (90 degrees or π/2 radians).
    /// The method compares the sum of the <paramref name="first"/> and <paramref name="second"/> angles with a right angle value to determine if they are complementary.
    /// </remarks>
    public static bool AreComplementary<TUnits, T>(AngleReduced<TUnits, T> first, AngleReduced<TUnits, T> second)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => first.Value + second.Value == Angle<TUnits, T>.Right.Value;

    /// <summary>
    /// Checks if two angles are supplementary.
    /// </summary>
    /// <param name="first">The first angle to compare.</param>
    /// <param name="second">The second angle to compare.</param>
    /// <returns>True if the two angles are supplementary, false otherwise.</returns>
    /// <remarks>
    /// Two angles are considered supplementary if their sum equals a straight angle (180 degrees or π radians).
    /// The method compares the sum of the <paramref name="first"/> and <paramref name="second"/> angles with a straight angle value to determine if they are supplementary.
    /// </remarks>
    public static bool AreSupplementary<TUnits, T>(AngleReduced<TUnits, T> first, AngleReduced<TUnits, T> second)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => first.Value + second.Value == Angle<TUnits, T>.Straight.Value;

    #endregion

    #region trigonometry

    /// <summary>
    /// Calculates the arc cosine (inverse cosine) of an angle.
    /// </summary>
    /// <typeparam name="TRadians">The floating point type used internally by the returned angle.</typeparam>
    /// <param name="cos">The cosine value of the angle.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when the provided <paramref name="sin"/> value is outside the range [-1, 1].
    /// </exception>
    /// <returns>The angle in radians whose cosine is equal to the given value.</returns>
    /// <remarks>
    /// <para>
    /// The arc cosine function calculates the angle whose cosine is equal to the given <paramref name="cos"/> value.
    /// It returns an angle between 0 and π (180 degrees) in radians.
    /// The resulting angle represents the measure of the arc whose cosine is equal to the given <paramref name="cos"/> value.
    /// For example, if the <paramref name="cos"/> value is 0.5, the resulting angle would be approximately 60 degrees.
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    public static Angle<Radians, TRadians> Acos<TRadians>(TRadians cos)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        =>  cos < -TRadians.One || cos > TRadians.One
            ? Throw.ArgumentOutOfRangeException<Angle<Radians, TRadians>>(nameof(cos), cos, "The cosine value must be in the range [-1, 1].")
            : new(TRadians.CreateChecked(Math.Acos(double.CreateChecked(cos))));

    /// <summary>
    /// Calculates the arc cosine (inverse cosine) of an angle.
    /// </summary>
    /// <typeparam name="TCos">The floating point type of <paramref name="cos"/>.</typeparam>
    /// <typeparam name="TRadians">The floating point type used internally by the returned angle.</typeparam>
    /// <param name="cos">The cosine value of the angle.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when the provided <paramref name="cos"/> value is outside the range [-1, 1].
    /// </exception>
    /// <returns>The angle in radians whose cosine is equal to the given value.</returns>
    /// <remarks>
    /// <para>
    /// The arc cosine function calculates the angle whose cosine is equal to the given <paramref name="cos"/> value.
    /// The resulting angle represents the measure of the arc whose cosine is equal to the given <paramref name="cos"/> value.
    /// For example, if the <paramref name="cos"/> value is 0.5, the resulting angle would be approximately 60 degrees.
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    public static Angle<Radians, TRadians> Acos<TCos, TRadians>(TCos cos)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TCos : struct, IFloatingPoint<TCos>
        => cos < -TCos.One || cos > TCos.One
            ? Throw.ArgumentOutOfRangeException<Angle<Radians, TRadians>>(nameof(cos), cos, "The cosine value must be in the range [-1, 1].")
            : new(TRadians.CreateChecked(Math.Acos(double.CreateChecked(cos))));

    /// <summary>
    /// Calculates the arc sine (inverse sine) of an angle.
    /// </summary>
    /// <typeparam name="TRadians">The floating point type used internally by the returned angle.</typeparam>
    /// <param name="sin">The sine value of the angle.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when the provided <paramref name="sin"/> value is outside the range [-1, 1].
    /// </exception>
    /// <returns>The angle in radians whose sine is equal to the given value.</returns>
    /// <remarks>
    /// <para>
    /// The arc sine function calculates the angle whose sine is equal to the given <paramref name="sin"/> value.
    /// It returns an angle between -π/2 (-90 degrees) and π/2 (90 degrees) in radians.
    /// The resulting angle represents the measure of the arc whose sine is equal to the given <paramref name="sin"/> value.
    /// For example, if the <paramref name="sin"/> value is 0.5, the resulting angle would be approximately 30 degrees.
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    public static Angle<Radians, TRadians> Asin<TRadians>(TRadians sin)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => sin < -TRadians.One || sin > TRadians.One
            ? Throw.ArgumentOutOfRangeException<Angle<Radians, TRadians>>(nameof(sin), sin, "The sine value must be in the range [-1, 1].")
            : new(TRadians.CreateChecked(Math.Asin(double.CreateChecked(sin))));

    /// <summary>
    /// Calculates the arc sine (inverse sine) of an angle.
    /// </summary>
    /// <typeparam name="TSin">The floating point type of <paramref name="sin"/>.</typeparam>
    /// <typeparam name="TRadians">The floating point type used internally by the returned angle.</typeparam>
    /// <param name="sin">The sine value of the angle.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when the provided <paramref name="sin"/> value is outside the range [-1, 1].
    /// </exception>
    /// <returns>The angle in radians whose sine is equal to the given value.</returns>
    /// <remarks>
    /// <para>
    /// The arc sine function calculates the angle whose sine is equal to the given <paramref name="sin"/> value.
    /// It returns an angle between -π/2 (-90 degrees) and π/2 (90 degrees) in radians.
    /// The resulting angle represents the measure of the arc whose sine is equal to the given <paramref name="sin"/> value.
    /// For example, if the <paramref name="sin"/> value is 0.5, the resulting angle would be approximately 30 degrees.
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    public static Angle<Radians, TRadians> Asin<TSin, TRadians>(TSin sin)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TSin : struct, IFloatingPoint<TSin>
        => sin < -TSin.One || sin > TSin.One
            ? Throw.ArgumentOutOfRangeException<Angle<Radians, TRadians>>(nameof(sin), sin, "The sine value must be in the range [-1, 1].")
            : new(TRadians.CreateChecked(Math.Asin(double.CreateChecked(sin))));

    /// <summary>
    /// Calculates the arc tangent (inverse tangent) of an angle.
    /// </summary>
    /// <typeparam name="TRadians">The floating point type used internally by the returned angle.</typeparam>
    /// <param name="tan">The tangent value of the angle.</param>
    /// <returns>The angle in radians whose tangent is equal to the given value.</returns>
    /// <remarks>
    /// <para>
    /// The arc tangent function calculates the angle whose tangent is equal to the given <paramref name="tan"/> value.
    /// It returns an angle between -π/2 (-90 degrees) and π/2 (90 degrees) in radians.
    /// The resulting angle represents the measure of the arc whose tangent is equal to the given <paramref name="tan"/> value.
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    public static Angle<Radians, TRadians> Atan<TRadians>(TRadians tan)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => new(TRadians.CreateChecked(Math.Atan(double.CreateChecked(tan))));

    /// <summary>
    /// Calculates the arc tangent (inverse tangent) of an angle.
    /// </summary>
    /// <typeparam name="TTan">The floating point type of <paramref name="tan"/>.</typeparam>
    /// <typeparam name="TRadians">The floating point type used internally by the returned angle.</typeparam>
    /// <param name="tan">The tangent value of the angle.</param>
    /// <returns>The angle in radians whose tangent is equal to the given value.</returns>
    /// <remarks>
    /// <para>
    /// The arc tangent function calculates the angle whose tangent is equal to the given <paramref name="tan"/> value.
    /// It returns an angle between -π/2 (-90 degrees) and π/2 (90 degrees) in radians.
    /// The resulting angle represents the measure of the arc whose tangent is equal to the given <paramref name="tan"/> value.
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    public static Angle<Radians, TRadians> Atan<TTan, TRadians>(TTan tan)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TTan : struct, IFloatingPoint<TTan>
        => new(TRadians.CreateChecked(Math.Atan(double.CreateChecked(tan))));

    /// <summary>
    /// Calculates the arc tangent (inverse tangent) of the ratio of two specified numbers.
    /// </summary>
    /// <typeparam name="TRadians">The floating point type used internally by the returned angle.</typeparam>
    /// <param name="y">The y-coordinate of the point.</param>
    /// <param name="x">The x-coordinate of the point.</param>
    /// <returns>The angle whose tangent is equal to the ratio <paramref name="y"/> / <paramref name="x"/>.</returns>
    /// <remarks>
    /// <para>
    /// The arc tangent function calculates the angle whose tangent is equal to the ratio of <paramref name="y"/> and <paramref name="x"/>.
    /// It returns an angle between -π and π (-180 degrees and 180 degrees) in radians.
    /// The resulting angle represents the measure of the arc whose tangent is equal to the ratio <paramref name="y"/> / <paramref name="x"/>.
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    public static Angle<Radians, TRadians> Atan2<TRadians>(TRadians x, TRadians y)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => new(TRadians.CreateChecked(Math.Atan2(double.CreateChecked(x), double.CreateChecked(y))));

    /// <summary>
    /// Calculates the arc tangent (inverse tangent) of the ratio of two specified numbers.
    /// </summary>
    /// <typeparam name="T">The floating point type of the parameters.</typeparam>
    /// <typeparam name="TRadians">The floating point type used internally by the returned angle.</typeparam>
    /// <param name="y">The y-coordinate of the point.</param>
    /// <param name="x">The x-coordinate of the point.</param>
    /// <returns>The angle whose tangent is equal to the ratio <paramref name="y"/> / <paramref name="x"/>.</returns>
    /// <remarks>
    /// <para>
    /// The arc tangent function calculates the angle whose tangent is equal to the ratio of <paramref name="y"/> and <paramref name="x"/>.
    /// It returns an angle between -π and π (-180 degrees and 180 degrees) in radians.
    /// The resulting angle represents the measure of the arc whose tangent is equal to the ratio <paramref name="y"/> / <paramref name="x"/>.
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    public static Angle<Radians, TRadians> Atan2<T, TRadians>(T x, T y)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where T : struct, IFloatingPoint<T>
        => new(TRadians.CreateChecked(Math.Atan2(double.CreateChecked(x), double.CreateChecked(y))));

    /// <summary>
    /// Calculates the cosine of an angle.
    /// </summary>
    /// <typeparam name="TRadians">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>The cosine of the given angle.</returns>
    /// <remarks>
    /// <para>
    /// The cosine function calculates the ratio of the adjacent side to the hypotenuse of a right triangle
    /// with the given <paramref name="angle"/>.
    /// The returned value ranges between -1 and 1, where -1 represents a 180-degree angle (π radians)
    /// and 1 represents a 0-degree angle (0 radians).
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    public static double Cos<TRadians>(Angle<Radians, TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Cos(double.CreateChecked(angle.Value));

    /// <summary>
    /// Calculates the hyperbolic cosine of an angle.
    /// </summary>
    /// <typeparam name="TRadians">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>The hyperbolic cosine of the given angle.</returns>
    /// <remarks>
    /// <para>
    /// The hyperbolic cosine function calculates the ratio (e^<paramref name="angle"/> + e^(-<paramref name="angle"/>)) / 2,
    /// where e is Euler's number (approximately 2.71828).
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    public static double Cosh<TRadians>(Angle<Radians, TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Cosh(double.CreateChecked(angle.Value));

    /// <summary>
    /// Calculates the sine of an angle.
    /// </summary>
    /// <typeparam name="TRadians">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>The sine of the given angle.</returns>
    /// <remarks>
    /// <para>
    /// The sine function calculates the ratio of the opposite side to the hypotenuse of a right triangle
    /// with the given <paramref name="angle"/>.
    /// The returned value ranges between -1 and 1, where -1 represents a 270-degree angle (-3π/2 radians)
    /// and 1 represents a 90-degree angle (π/2 radians).
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    public static double Sin<TRadians>(Angle<Radians, TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Sin(double.CreateChecked(angle.Value));

    /// <summary>
    /// Calculates the hyperbolic sine of an angle.
    /// </summary>
    /// <typeparam name="TRadians">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>The hyperbolic sine of the given angle.</returns>
    /// <remarks>
    /// <para>
    /// The hyperbolic sine function calculates the ratio (e^<paramref name="angle"/> - e^(-<paramref name="angle"/>)) / 2,
    /// where e is Euler's number (approximately 2.71828).
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    public static double Sinh<TRadians>(Angle<Radians, TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Sinh(double.CreateChecked(angle.Value));

    /// <summary>
    /// Calculates the tangent of an angle.
    /// </summary>
    /// <typeparam name="TRadians">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>The tangent of the given angle.</returns>
    /// <remarks>
    /// <para>
    /// The tangent function calculates the ratio of the sine of the angle to the cosine of the angle.
    /// It represents the slope of the line tangent to the unit circle at the given <paramref name="angle"/>.
    /// The returned value can range from negative infinity to positive infinity.
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    public static double Tan<TRadians>(Angle<Radians, TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Tan(double.CreateChecked(angle.Value));

    /// <summary>
    /// Calculates the hyperbolic tangent of an angle.
    /// </summary>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>The hyperbolic tangent of the given angle.</returns>
    /// <remarks>
    /// <para>
    /// The hyperbolic tangent function calculates the ratio (e^<paramref name="angle"/> - e^(-<paramref name="angle"/>)) /
    /// (e^<paramref name="angle"/> + e^(-<paramref name="angle"/>)), where e is Euler's number (approximately 2.71828).
    /// The returned value represents the hyperbolic tangent of the given <paramref name="angle"/>.
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    public static double Tanh<TRadians>(Angle<Radians, TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Tanh(double.CreateChecked(angle.Value));

    #endregion

    #region sum

    /// <summary>
    /// Calculates the sum of a collection of angles.
    /// </summary>
    /// <param name="source">The enumerable collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// The resulting angle is the cumulative sum of all the angles in radians.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this IEnumerable<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        var sum = T.Zero;
        foreach (var angle in source)
        {
            checked { sum += angle.Value; }
        }
        return new Angle<TUnits, T>(sum);
    }

    /// <summary>
    /// Calculates the sum of a collection of angles.
    /// </summary>
    /// <param name="source">The array collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// The resulting angle is the cumulative sum of all the angles in radians.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this Angle<TUnits, T>[] source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.AsSpan().Sum();

    /// <summary>
    /// Calculates the sum of a collection of angles.
    /// </summary>
    /// <param name="source">The <see cref="Memory{T}"/> collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// The resulting angle is the cumulative sum of all the angles in radians.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this Memory<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.Span.Sum();

    /// <summary>
    /// Calculates the sum of a collection of angles.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlyMemory{T}"/> collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// The resulting angle is the cumulative sum of all the angles in radians.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this ReadOnlyMemory<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.Span.Sum();

    /// <summary>
    /// Calculates the sum of a collection of angles.
    /// </summary>
    /// <param name="source">The <see cref="Span{T}"/> collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// The resulting angle is the cumulative sum of all the angles in radians.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this Span<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ((ReadOnlySpan<Angle<TUnits, T>>)source).Sum();

    /// <summary>
    /// Calculates the sum of a collection of angles.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> collection of angles.</param>
    /// <returns>The sum of the angles in the collection.</returns>
    /// <remarks>
    /// The sum of angles is computed by adding all the angles in the given <paramref name="source"/> collection.
    /// The resulting angle is the cumulative sum of all the angles in radians.
    /// </remarks>
    public static Angle<TUnits, T> Sum<TUnits, T>(this ReadOnlySpan<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        var sum = T.Zero;
        foreach (var angle in source)
        {
            checked { sum += angle.Value; }
        }
        return new Angle<TUnits, T>(sum);
    }

    #endregion

    #region average

    /// <summary>
    /// Calculates the average of an collection of angles.
    /// </summary>
    /// <param name="source">The enumerable of angles.</param>
    /// <returns>The average angle in the collection, or null if the collection is empty.</returns>
    /// <remarks>
    /// The average angle is computed by summing all the angles in the given <paramref name="source"/> collection and dividing the sum by the count of angles.
    /// If the array is empty, the method returns null to indicate that there are no angles to compute the average from.
    /// The resulting angle represents the average value of all the angles.
    /// </remarks>
    public static Angle<TUnits, T>? Average<TUnits, T>(this IEnumerable<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        var sum = T.Zero;
        var count = T.Zero;
        foreach (var angle in source)
        {
            checked 
            { 
                sum += angle.Value;
                count++;
            }
        }
        return T.IsZero(count) 
            ? null 
            : new Angle<TUnits, T>(sum / count);
    }

    /// <summary>
    /// Calculates the average of an collection of angles.
    /// </summary>
    /// <param name="source">The array of angles.</param>
    /// <returns>The average angle in the collection, or null if the collection is empty.</returns>
    /// <remarks>
    /// The average angle is computed by summing all the angles in the given <paramref name="source"/> collection and dividing the sum by the count of angles.
    /// If the array is empty, the method returns null to indicate that there are no angles to compute the average from.
    /// The resulting angle represents the average value of all the angles.
    /// </remarks>
    public static Angle<TUnits, T>? Average<TUnits, T>(this Angle<TUnits, T>[] source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.AsSpan().Sum();

    /// <summary>
    /// Calculates the average of an collection of angles.
    /// </summary>
    /// <param name="source">The <see cref="Memory{T}"/> of angles.</param>
    /// <returns>The average angle in the collection, or null if the collection is empty.</returns>
    /// <remarks>
    /// The average angle is computed by summing all the angles in the given <paramref name="source"/> collection and dividing the sum by the count of angles.
    /// If the array is empty, the method returns null to indicate that there are no angles to compute the average from.
    /// The resulting angle represents the average value of all the angles.
    /// </remarks>
    public static Angle<TUnits, T>? Average<TUnits, T>(this Memory<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.Span.Sum();

    /// <summary>
    /// Calculates the average of an collection of angles.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlyMemory{T}"/> of angles.</param>
    /// <returns>The average angle in the collection, or null if the collection is empty.</returns>
    /// <remarks>
    /// The average angle is computed by summing all the angles in the given <paramref name="source"/> collection and dividing the sum by the count of angles.
    /// If the array is empty, the method returns null to indicate that there are no angles to compute the average from.
    /// The resulting angle represents the average value of all the angles.
    /// </remarks>
    public static Angle<TUnits, T>? Average<TUnits, T>(this ReadOnlyMemory<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.Span.Sum();

    /// <summary>
    /// Calculates the average of an collection of angles.
    /// </summary>
    /// <param name="source">The <see cref="Span{T}"/> of angles.</param>
    /// <returns>The average angle in the collection, or null if the collection is empty.</returns>
    /// <remarks>
    /// The average angle is computed by summing all the angles in the given <paramref name="source"/> collection and dividing the sum by the count of angles.
    /// If the array is empty, the method returns null to indicate that there are no angles to compute the average from.
    /// The resulting angle represents the average value of all the angles.
    /// </remarks>
    public static Angle<TUnits, T>? Average<TUnits, T>(this Span<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ((ReadOnlySpan<Angle<TUnits, T>>)source).Sum();

    /// <summary>
    /// Calculates the average of an collection of angles.
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> of angles.</param>
    /// <returns>The average angle in the collection, or null if the collection is empty.</returns>
    /// <remarks>
    /// The average angle is computed by summing all the angles in the given <paramref name="source"/> collection and dividing the sum by the count of angles.
    /// If the array is empty, the method returns null to indicate that there are no angles to compute the average from.
    /// The resulting angle represents the average value of all the angles.
    /// </remarks>
    public static Angle<TUnits, T>? Average<TUnits, T>(this ReadOnlySpan<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        var sum = T.Zero;
        var count = T.Zero;
        foreach (var angle in source)
        {
            checked
            {
                sum += angle.Value;
                count++;
            }
        }
        return T.IsZero(count)
            ? null
            : new Angle<TUnits, T>(sum / count);
    }

    #endregion
}
