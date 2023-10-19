namespace NetFabric.Numerics;

/// <summary>
/// Provides static operations and utility methods for instances of type <see cref="Angle{TUnits,T}"/> 
/// and <see cref="AngleReduced{TUnits,T}"/>.
/// </summary>
/// <remarks>
/// <para>
/// The <see cref="Angle"/> static class contains various static operations and utility methods that can 
/// be used with instances of the <see cref="Angle{TUnits,T}"/> and <see cref="AngleReduced{TUnits,T}"/> structs.
/// These methods enable you to perform common angle-related operations, such as trigonometric functions, 
/// conversions, and classification.
/// </para>
/// <para>
/// The static methods in this class are designed to work with instances of the <see cref="Angle{TUnits,T}"/> 
/// and <see cref="AngleReduced{TUnits,T}"/> structs, ensuring type safety and correct unit conversions.
/// </para>
/// <para>
/// You can use the static methods in the <see cref="Angle"/> class to perform operations on angles of the same 
/// measurement units, or convert between different measurement units using the provided conversion methods.
/// </para>
/// <para>
/// Note that all the methods in the <see cref="Angle"/> class are static, and they do not modify the original 
/// angle instances. Instead, they return new angle instances with the desired results.
/// </para>
/// </remarks>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<TUnits, T> Reduce<TUnits, T>(Angle<TUnits, T> angle)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<TUnits, T> GetReference<TUnits, T>(AngleReduced<TUnits, T> angle)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quadrant GetQuadrant<TUnits, T>(AngleReduced<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.GetQuadrant(angle);

    /// <summary>
    /// Returns the absolute value of the specified angle.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle.</param>
    /// <returns>The absolute value of the angle.</returns>
    /// <remarks>
    /// <para>
    /// The <see cref="Abs"/> method returns the absolute value of the angle, which is the non-negative value 
    /// representing the magnitude of the angle without considering its sign.
    /// </para>
    /// <para>
    /// If the input angle is positive or zero, the method returns the same angle. If the input angle is 
    /// negative, the method returns the angle with its sign reversed, resulting in a positive angle with the same magnitude.
    /// </para>
    /// </remarks>    
    public static Angle<TUnits, T> Abs<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => T.Sign(angle.Value) >= 0
            ? angle
            : new(-angle.Value);

    /// <summary>
    /// Returns a value indicating the sign of <paramref name="angle" />.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="angle"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">Source angle.</param>
    /// <returns>A number that indicates the sign of value, -1 if value is less than zero, 0 if value equal to zero, 1 if value is grater than zero.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> Lerp<TUnits, T>(Angle<TUnits, T> a1, Angle<TUnits, T> a2, T t)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPointIeee754<T>, IMinMaxValue<T>
#if NET8_0_OR_GREATER
        => new(T.Lerp(a1.Value, a2.Value, t)); 
#else
        => new(Utils.Lerp(a1.Value, a2.Value, t));
#endif

    /// <summary>
    /// Returns the smallest of two angles.
    /// </summary>
    /// <typeparam name="TUnits">The angle units of <paramref name="left"/> and <paramref name="right"/>.</typeparam>
    /// <typeparam name="T">The floating point type used internally by <paramref name="left"/> and <paramref name="right"/>.</typeparam>
    /// <param name="left">The first of two angles to compare.</param>
    /// <param name="right">The second of two angles to compare.</param>
    /// <returns>The smallest of the two angles.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle == Angle<TUnits, T>.Zero;

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
    /// <returns>true if the reduction of the absolute angle is greater than zero and less than 90 degrees; 
    /// otherwise false.
    /// </returns>
    public static bool IsAcute<TUnits, T>(AngleReduced<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle.Value > Angle<TUnits, T>.Zero.Value && angle.Value < Angle<TUnits, T>.Right.Value;

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
    /// <returns>
    /// true if the reduction of the absolute angle is greater than 90 degrees and less than 
    /// 180 degrees; otherwise false.
    /// </returns>
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
    /// <returns>
    /// true if the reduction of the absolute angle is greater than 180 degrees and less than 
    /// 360 degrees; otherwise false.
    /// </returns>
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
    /// The method compares the sum of the <paramref name="first"/> and <paramref name="second"/> angles with 
    /// a right angle value to determine if they are complementary.
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
    /// The method compares the sum of the <paramref name="first"/> and <paramref name="second"/> angles with 
    /// a straight angle value to determine if they are supplementary.
    /// </remarks>
    public static bool AreSupplementary<TUnits, T>(AngleReduced<TUnits, T> first, AngleReduced<TUnits, T> second)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => first.Value + second.Value == Angle<TUnits, T>.Straight.Value;

    #endregion
}
