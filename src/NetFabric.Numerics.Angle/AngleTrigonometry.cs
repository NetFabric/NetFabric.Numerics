namespace NetFabric.Numerics;

public static partial class Angle
{
    /// <summary>
    /// Calculates the arc cosine (inverse cosine) of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by the returned angle.</typeparam>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Radians, T> Acos<T>(T cos)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
        =>  cos < -T.One || cos > T.One
            ? Throw.ArgumentOutOfRangeException<AngleReduced<Radians, T>>(nameof(cos), cos, "The cosine value must be in the range [-1, 1].")
            : new(T.Acos(cos));

    /// <summary>
    /// Calculates the arc sine (inverse sine) of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by the returned angle.</typeparam>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Radians, T> Asin<T>(T sin)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
        => sin < -T.One || sin > T.One
            ? Throw.ArgumentOutOfRangeException<Angle<Radians, T>>(nameof(sin), sin, "The sine value must be in the range [-1, 1].")
            : new(T.Asin(sin));

    /// <summary>
    /// Calculates the arc tangent (inverse tangent) of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by the returned angle.</typeparam>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Radians, T> Atan<T>(T tan)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
        => new(T.Atan(tan));

    /// <summary>
    /// Calculates the arc tangent (inverse tangent) of the ratio of two specified numbers.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by the returned angle.</typeparam>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Radians, T> Atan2<T>(T x, T y)
        where T : struct, IFloatingPointIeee754<T>, IMinMaxValue<T>
        => new(T.Atan2(x, y));

    /// <summary>
    /// Calculates the arc cotangent of a value.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by the returned angle.</typeparam>
    /// <param name="cot">The cotangent value.</param>
    /// <returns>The arc cotangent of the specified value.</returns>
    /// <remarks>
    /// <para>
    /// The arc cotangent (acot) of a value is the angle whose cotangent equals the given value.
    /// The method calculates the arc cotangent of the <paramref name="cot"/> value using the formula: acot(cot) = atan(1 / cot).
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Radians, T> Acot<T>(T cot)
    where T : struct, IFloatingPointIeee754<T>, IMinMaxValue<T>
        => new(T.Atan2(T.One, cot));

    /// <summary>
    /// Calculates the arc secant of a value.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by the returned angle.</typeparam>
    /// <param name="sec">The secant value.</param>
    /// <returns>The arc secant of the specified value.</returns>
    /// <remarks>
    /// <para>
    /// The arc secant (asec) of a value is the angle whose secant equals the given value.
    /// The method calculates the arc secant of the <paramref name="sec"/> value using the formula: asec(sec) = acos(1 / sec).
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Radians, T> Asec<T>(T sec)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
        => new(T.Acos(T.One / sec));

    /// <summary>
    /// Calculates the arc cosecant of a value.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by the returned angle.</typeparam>
    /// <param name="csc">The cosecant value.</param>
    /// <returns>The arc cosecant of the specified value.</returns>
    /// <remarks>
    /// <para>
    /// The arc cosecant (acsc) of a value is the angle whose cosecant equals the given value.
    /// The method calculates the arc cosecant of the <paramref name="csc"/> value using the formula: acsc(csc) = asin(1 / csc).
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Radians, T> Acsc<T>(T csc)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
        => new(T.Asin(T.One / csc));

    /// <summary>
    /// Calculates the cosine of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Cos<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
        => T.Cos(angle.Value);

    /// <summary>
    /// Calculates the hyperbolic cosine of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Cosh<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, IHyperbolicFunctions<T>
        => T.Cosh(angle.Value);

    /// <summary>
    /// Calculates the sine of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Sin<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
        => T.Sin(angle.Value);

    /// <summary>
    /// Calculates the hyperbolic sine of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Sinh<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, IHyperbolicFunctions<T>
        => T.Sinh(angle.Value);


    /// <summary>
    /// Calculates the sine and the cosine of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>A tuple containing the sine and the cosine of the given angle.</returns>
    /// <remarks>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (T Sin, T Cos) SinCos<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
        => T.SinCos(angle.Value);

    /// <summary>
    /// Calculates the tangent of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Tan<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
        => T.Tan(angle.Value);

    /// <summary>
    /// Calculates the hyperbolic tangent of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Tanh<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, IHyperbolicFunctions<T>
        => T.Tanh(angle.Value);

    /// <summary>
    /// Calculates the cotangent of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>The cotangent of the specified angle.</returns>
    /// <remarks>
    /// <para>
    /// The cotangent of an angle is defined as the reciprocal of the tangent function.
    /// The method calculates the cotangent of the <paramref name="angle"/> using the formula: cot(angle) = 1 / cot(angle).
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Cot<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
        => T.One / T.Tan(angle.Value);

    /// <summary>
    /// Calculates the hyperbolic cotangent of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>The hyperbolic cotangent of the specified angle.</returns>
    /// <remarks>
    /// <para>
    /// The hyperbolic cotangent (coth) of an angle is defined as the reciprocal of the hyperbolic tangent (tanh) function.
    /// The method calculates the hyperbolic cotangent of the <paramref name="angle"/> using the formula: coth(angle) = 1 / tanh(angle).
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Coth<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, IHyperbolicFunctions<T>
        => T.One / T.Tanh(angle.Value);

    /// <summary>
    /// Calculates the secant of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>The secant of the specified angle.</returns>
    /// <remarks>
    /// <para>
    /// The secant of an angle is defined as the reciprocal of the cosine function.
    /// The method calculates the secant of the <paramref name="angle"/> using the formula: sec(angle) = 1 / cos(angle).
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Sec<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
        => T.One / T.Cos(angle.Value);

    /// <summary>
    /// Calculates the hyperbolic secant of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>The hyperbolic secant of the specified angle.</returns>
    /// <remarks>
    /// <para>
    /// The hyperbolic secant (sech) of an angle is defined as the reciprocal of the hyperbolic cosine (cosh) function.
    /// The method calculates the hyperbolic secant of the <paramref name="angle"/> using the formula: sech(angle) = 1 / cosh(angle).
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Sech<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, IHyperbolicFunctions<T>
        => T.One / T.Cosh(angle.Value);

    /// <summary>
    /// Calculates the cosecant of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>The cosecant of the specified angle.</returns>
    /// <remarks>
    /// <para>
    /// The cosecant of an angle is defined as the reciprocal of the sine function.
    /// The method calculates the cosecant of the <paramref name="angle"/> using the formula: csc(angle) = 1 / sin(angle).
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Csc<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
        => T.One / T.Sin(angle.Value);

    /// <summary>
    /// Calculates the hyperbolic cosecant of an angle.
    /// </summary>
    /// <typeparam name="T">The floating point type used internally by <paramref name="angle"/>.</typeparam>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>The hyperbolic cosecant of the specified angle.</returns>
    /// <remarks>
    /// <para>
    /// The hyperbolic cosecant (csch) of an angle is defined as the reciprocal of the hyperbolic sine (sinh) function.
    /// The method calculates the hyperbolic cosecant of the <paramref name="angle"/> using the formula: csch(angle) = 1 / sinh(angle).
    /// </para>
    /// <para>
    /// This method calls into the underlying C runtime, and the exact result or valid input range may differ between different operating systems or architectures.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Csch<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, IHyperbolicFunctions<T>
        => T.One / T.Sinh(angle.Value);

}
