using System.Numerics;
using System;
using System.Collections.Generic;

namespace NetFabric.Numerics;

public static class Angle
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
    public static Angle<TUnits, T> Reduce<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(Utils.Reduce(angle));

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
    public static Angle<TUnits, T> GetReference<TUnits, T>(Angle<TUnits, T> angle)
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
    public static Quadrant GetQuadrant<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.GetQuadrant(angle, out _);

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
    /// Indicates whether the specified angle is equal to Zero when reduced.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the reduction of the absolute angle is zero; otherwise false.</returns>
    public static bool IsZero<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle % Angle<TUnits, T>.Full == Angle<TUnits, T>.Zero;

    /// <summary>
    /// Indicates whether the specified angle is acute when reduced.
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
    public static bool IsAcute<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        var reduced = Utils.Reduce(angle);
        return reduced > T.Zero && reduced < Angle<TUnits, T>.Right.Value;
    }

    /// <summary>
    /// Indicates whether the specified angle is right when reduced.
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
    public static bool IsRight<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.Reduce(angle) == Angle<TUnits, T>.Right.Value;

    /// <summary>
    /// Indicates whether the specified angle is obtuse when reduced.
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
    public static bool IsObtuse<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        var reduced = Utils.Reduce(angle);
        return reduced > Angle<TUnits, T>.Right.Value && reduced < Angle<TUnits, T>.Straight.Value;
    }

    /// <summary>
    /// Indicates whether the specified angle is straight when reduced.
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
    public static bool IsStraight<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.Reduce(angle) == Angle<TUnits, T>.Straight.Value;

    /// <summary>
    /// Indicates whether the specified angle is reflex when reduced.
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
    public static bool IsReflex<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.Reduce(angle) > Angle<TUnits, T>.Straight.Value;

    /// <summary>
    /// Indicates whether the specified angle is oblique when reduced.
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
    public static bool IsOblique<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle.Value % Angle<TUnits, T>.Right.Value != T.Zero;

    #endregion

    #region trigonometry

    /// <summary>
    /// Returns the shortest angle whose cosine is <paramref name="cos"/>
    /// </summary>
    /// <typeparam name="TRadians">The floating point type used internally by <paramref name="cos"/>.</typeparam>
    /// <param name="cos"></param>
    /// <remarks>
    /// The acos function is a mathematical function that returns the inverse cosine (cos⁻¹) of a 
    /// given value. In other words, it returns the angle in radians whose cosine is equal to the
    /// specified value. The input value must be between -1 and 1, inclusive, and the output value 
    /// will always be in the range of 0 to π radians or 0 to 180 degrees. 
    /// </remarks>
    /// <returns>The shortest angle whose cosine is <paramref name="cos"/></returns>
    public static Angle<Radians, TRadians> Acos<TRadians>(TRadians cos)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => new(TRadians.CreateChecked(Math.Acos(double.CreateChecked(cos))));

    public static Angle<Radians, TRadians> Acos<TCos, TRadians>(TCos cos)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TCos : struct, IFloatingPoint<TCos>
        => new(TRadians.CreateChecked(Math.Acos(double.CreateChecked(cos))));

    public static Angle<Radians, TRadians> Asin<TRadians>(TRadians sin)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => new(TRadians.CreateChecked(Math.Asin(double.CreateChecked(sin))));

    public static Angle<Radians, TRadians> Asin<TSin, TRadians>(TSin sin)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TSin : struct, IFloatingPoint<TSin>
        => new(TRadians.CreateChecked(Math.Asin(double.CreateChecked(sin))));

    public static Angle<Radians, TRadians> Atan<TRadians>(TRadians tan)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => new(TRadians.CreateChecked(Math.Atan(double.CreateChecked(tan))));

    public static Angle<Radians, TRadians> Atan<TTan, TRadians>(TTan tan)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TTan : struct, IFloatingPoint<TTan>
        => new(TRadians.CreateChecked(Math.Atan(double.CreateChecked(tan))));

    public static Angle<Radians, TRadians> Atan2<TRadians>(TRadians x, TRadians y)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => new(TRadians.CreateChecked(Math.Atan2(double.CreateChecked(x), double.CreateChecked(y))));

    public static Angle<Radians, TRadians> Atan2<TTan, TRadians>(TTan x, TTan y)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TTan : struct, IFloatingPoint<TTan>
        => new(TRadians.CreateChecked(Math.Atan2(double.CreateChecked(x), double.CreateChecked(y))));

    public static double Cos<TRadians>(Angle<Radians, TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Cos(double.CreateChecked(angle.Value));

    public static double Cosh<TRadians>(Angle<Radians, TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Cosh(double.CreateChecked(angle.Value));

    public static double Sin<TRadians>(Angle<Radians, TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Sin(double.CreateChecked(angle.Value));

    public static double Sinh<TRadians>(Angle<Radians, TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Sinh(double.CreateChecked(angle.Value));

    public static double Tan<TRadians>(Angle<Radians, TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Tan(double.CreateChecked(angle.Value));

    #endregion

    #region conversion from degrees

    static readonly double RadiansInDegrees = Math.PI / 180.0;
    static readonly double GradiansInDegrees = 100.0 / 90.0;
    static readonly double RevolutionsInDegrees = 1.0 / 360.0;

    public static Angle<Degrees, TTo> ToDegrees<TFrom, TTo>(Angle<Degrees, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(TTo.CreateChecked(angle.Value));

    public static Angle<Radians, T> ToRadians<T>(Angle<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRadians<T, T>(angle);

    public static Angle<Radians, TRadians> ToRadians<TDegrees, TRadians>(Angle<Degrees, TDegrees> angle)
        where TDegrees : struct, IFloatingPoint<TDegrees>, IMinMaxValue<TDegrees>
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => new(TRadians.CreateChecked(angle.Value * TDegrees.CreateChecked(RadiansInDegrees)));

    public static Angle<Gradians, T> ToGradians<T>(Angle<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToGradians<T, T>(angle);

    public static Angle<Gradians, TGradians> ToGradians<TDegrees, TGradians>(Angle<Degrees, TDegrees> angle)
        where TDegrees : struct, IFloatingPoint<TDegrees>, IMinMaxValue<TDegrees>
        where TGradians : struct, IFloatingPoint<TGradians>, IMinMaxValue<TGradians>
        => new(TGradians.CreateChecked(angle.Value * TDegrees.CreateChecked(GradiansInDegrees)));

    public static Angle<Revolutions, T> ToRevolutions<T>(Angle<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRevolutions<T, T>(angle);

    public static Angle<Revolutions, TRevolutions> ToRevolutions<TDegrees, TRevolutions>(Angle<Degrees, TDegrees> angle)
        where TDegrees : struct, IFloatingPoint<TDegrees>, IMinMaxValue<TDegrees>
        where TRevolutions : struct, IFloatingPoint<TRevolutions>, IMinMaxValue<TRevolutions>
        => new(TRevolutions.CreateChecked(angle.Value * TDegrees.CreateChecked(RevolutionsInDegrees)));

    public static Angle<Degrees, T> ToDegrees<T>(int degrees, double minutes)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        if (minutes < 0.0 || minutes >= 60.0)
            Throw.ArgumentOutOfRangeException(nameof(minutes), minutes, "Argument must be positive and less than 60.");

        return degrees < 0 
            ? new(T.CreateChecked(degrees - (minutes / 60.0)))
            : new(T.CreateChecked(degrees + (minutes / 60.0)));
    }

    public static Angle<Degrees, T> ToDegrees<T>(int degrees, int minutes, double seconds)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        if (minutes < 0.0 || minutes >= 60.0)
            Throw.ArgumentOutOfRangeException(nameof(minutes), minutes, "Argument must be positive and less than 60.");
        if (seconds < 0.0 || seconds >= 60.0)
            Throw.ArgumentOutOfRangeException(nameof(seconds), seconds, "Argument must be positive and less than 60.");

        return degrees < 0
            ? new(T.CreateChecked(degrees - (minutes / 60.0) - (seconds / 3600.0)))
            : new(T.CreateChecked(degrees + (minutes / 60.0) + (seconds / 3600.0)));
    }

    /// <summary>
    /// Gets the value of the angle expressed in Degrees and minutes.
    /// </summary>
    public static (TDegrees Degrees, TMinutes Minutes) ToDegreesMinutes<T, TDegrees, TMinutes>(Angle<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        where TDegrees : IBinaryInteger<TDegrees>
        where TMinutes : IFloatingPoint<TMinutes>
    {
       var degrees = TDegrees.CreateChecked(angle.Value);
       var minutes = TMinutes.CreateChecked(Math.Abs(double.CreateChecked(angle.Value) - double.CreateChecked(degrees)) * 60.0);
       return (degrees, minutes);
    }

    /// <summary>
    /// Gets the value of the current Angle structure expressed in Degrees, minutes and seconds.
    /// </summary>
    public static (TDegrees Degrees, TMinutes Minutes, TSeconds Seconds) ToDegreesMinutesSeconds<T, TDegrees, TMinutes, TSeconds>(Angle<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        where TDegrees : IBinaryInteger<TDegrees>
        where TMinutes : IBinaryInteger<TMinutes>
        where TSeconds : IFloatingPoint<TSeconds>
    {
       var degrees = TDegrees.CreateChecked(angle.Value);
       var decimalMinutes = Math.Abs(double.CreateChecked(angle.Value) - double.CreateChecked(degrees)) * 60.0;
       var minutes = TMinutes.CreateChecked(decimalMinutes);
       var seconds = TSeconds.CreateChecked((decimalMinutes - double.CreateChecked(minutes)) * 60.0);
       return (degrees, minutes, seconds);
    }

    #endregion

    #region conversion from radians

    static readonly double DegreesInRadians = 180.0 / Math.PI;
    static readonly double GradiansInRadians = 100.0 / Math.PI;
    static readonly double RevolutionsInRadians = 0.5 / Math.PI;

    public static Angle<Degrees, T> ToDegrees<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToDegrees<T, T>(angle);

    public static Angle<Degrees, TDegrees> ToDegrees<TRadians, TDegrees>(Angle<Radians, TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TDegrees : struct, IFloatingPoint<TDegrees>, IMinMaxValue<TDegrees>
        => new(TDegrees.CreateChecked(angle.Value * TRadians.CreateChecked(DegreesInRadians)));

    public static Angle<Radians, TTo> ToRadians<TFrom, TTo>(Angle<Radians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(TTo.CreateChecked(angle.Value));

    public static Angle<Gradians, T> ToGradians<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToGradians<T, T>(angle);

    public static Angle<Gradians, TGradians> ToGradians<TRadians, TGradians>(Angle<Radians, TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TGradians : struct, IFloatingPoint<TGradians>, IMinMaxValue<TGradians>
        => new(TGradians.CreateChecked(angle.Value * TRadians.CreateChecked(GradiansInRadians)));

    public static Angle<Revolutions, T> ToRevolutions<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRevolutions<T, T>(angle);

    public static Angle<Revolutions, TRevolutions> ToRevolutions<TRadians, TRevolutions>(Angle<Radians, TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TRevolutions : struct, IFloatingPoint<TRevolutions>, IMinMaxValue<TRevolutions>
        => new(TRevolutions.CreateChecked(angle.Value * TRadians.CreateChecked(RevolutionsInRadians)));

    #endregion

    #region conversion from gradians

    static readonly double DegreesInGradians = 90.0 / 100.0;
    static readonly double RadiansInGradians = Math.PI / 200.0;
    static readonly double RevolutionsInGradians = 1.0 / 400.0;

    public static Angle<Degrees, T> ToDegrees<T>(Angle<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToDegrees<T, T>(angle);

    public static Angle<Degrees, TDegrees> ToDegrees<TGradians, TDegrees>(Angle<Gradians, TGradians> angle)
        where TGradians : struct, IFloatingPoint<TGradians>, IMinMaxValue<TGradians>
        where TDegrees : struct, IFloatingPoint<TDegrees>, IMinMaxValue<TDegrees>
        => new(TDegrees.CreateChecked(angle.Value * TGradians.CreateChecked(DegreesInGradians)));

    public static Angle<Radians, T> ToRadians<T>(Angle<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRadians<T, T>(angle);

    public static Angle<Radians, TRadians> ToRadians<TGradians, TRadians>(Angle<Gradians, TGradians> angle)
        where TGradians : struct, IFloatingPoint<TGradians>, IMinMaxValue<TGradians>
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => new(TRadians.CreateChecked(angle.Value * TGradians.CreateChecked(RadiansInGradians)));

    public static Angle<Gradians, TTo> ToGradians<TFrom, TTo>(Angle<Gradians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(TTo.CreateChecked(angle.Value));

    public static Angle<Revolutions, T> ToRevolutions<T>(Angle<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRevolutions<T, T>(angle);

    public static Angle<Revolutions, TRevolutions> ToRevolutions<TGradians, TRevolutions>(Angle<Gradians, TGradians> angle)
        where TGradians : struct, IFloatingPoint<TGradians>, IMinMaxValue<TGradians>
        where TRevolutions : struct, IFloatingPoint<TRevolutions>, IMinMaxValue<TRevolutions>
        => new(TRevolutions.CreateChecked(angle.Value * TGradians.CreateChecked(RevolutionsInGradians)));

    #endregion

    #region conversion from revolutions

    static readonly double DegreesInRevolutions = 360.0;
    static readonly double RadiansInRevolutions = 2.0 * Math.PI;
    static readonly double GradiansInRevolutions = 400.0;

    public static Angle<Degrees, T> ToDegrees<T>(Angle<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToDegrees<T, T>(angle);

    public static Angle<Degrees, TDegrees> ToDegrees<TRevolutions, TDegrees>(Angle<Revolutions, TRevolutions> angle)
        where TRevolutions : struct, IFloatingPoint<TRevolutions>, IMinMaxValue<TRevolutions>
        where TDegrees : struct, IFloatingPoint<TDegrees>, IMinMaxValue<TDegrees>
        => new(TDegrees.CreateChecked(angle.Value * TRevolutions.CreateChecked(DegreesInRevolutions)));

    public static Angle<Radians, T> ToRadians<T>(Angle<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRadians<T, T>(angle);

    public static Angle<Radians, TRadians> ToRadians<TRevolutions, TRadians>(Angle<Revolutions, TRevolutions> angle)
        where TRevolutions : struct, IFloatingPoint<TRevolutions>, IMinMaxValue<TRevolutions>
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => new(TRadians.CreateChecked(angle.Value * TRevolutions.CreateChecked(RadiansInRevolutions)));

    public static Angle<Gradians, T> ToGradians<T>(Angle<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToGradians<T, T>(angle);

    public static Angle<Gradians, TGradians> ToGradians<TRevolutions, TGradians>(Angle<Revolutions, TRevolutions> angle)
        where TRevolutions : struct, IFloatingPoint<TRevolutions>, IMinMaxValue<TRevolutions>
        where TGradians : struct, IFloatingPoint<TGradians>, IMinMaxValue<TGradians>
        => new(TGradians.CreateChecked(angle.Value * TRevolutions.CreateChecked(GradiansInRevolutions)));

    public static Angle<Revolutions, TTo> ToRevolutions<TFrom, TTo>(Angle<Revolutions, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(TTo.CreateChecked(angle.Value));

    #endregion

    #region sum

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

    public static Angle<TUnits, T> Sum<TUnits, T>(this Angle<TUnits, T>[] source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.AsSpan().Sum();

    public static Angle<TUnits, T> Sum<TUnits, T>(this Memory<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.Span.Sum();

    public static Angle<TUnits, T> Sum<TUnits, T>(this ReadOnlyMemory<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.Span.Sum();

    public static Angle<TUnits, T> Sum<TUnits, T>(this Span<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ((ReadOnlySpan<Angle<TUnits, T>>)source).Sum();

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
        return new Angle<TUnits, T>(sum / count);
    }

    public static Angle<TUnits, T>? Average<TUnits, T>(this Angle<TUnits, T>[] source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.AsSpan().Sum();

    public static Angle<TUnits, T>? Average<TUnits, T>(this Memory<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.Span.Sum();

    public static Angle<TUnits, T>? Average<TUnits, T>(this ReadOnlyMemory<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.Span.Sum();

    public static Angle<TUnits, T>? Average<TUnits, T>(this Span<Angle<TUnits, T>> source)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ((ReadOnlySpan<Angle<TUnits, T>>)source).Sum();

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
        return new Angle<TUnits, T>(sum / count);
    }

    #endregion
}
