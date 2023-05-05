using System.Numerics;
using System;
using System.Reflection.Metadata;

namespace NetFabric.Numerics;

public static class Angle
{
    /// <summary>
    /// Reduce an angle to positive and less than one revolution.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>The reduced angle.</returns>
    public static TAngle Reduce<TAngle>(TAngle angle)
        where TAngle : struct, IAngle<TAngle>
    {
        var reduced = angle % TAngle.Full;
        return reduced < TAngle.AdditiveIdentity 
            ? reduced + TAngle.Full
            : reduced;
    }

    /// <summary>
    /// Returns the absolute value of angle.
    /// </summary>
    /// <param name="angle">The angle for which to get its absolute.</param>
    /// <returns>
    /// The absolute of <paramref name="angle" />.
    /// </returns>
    public static TAngle Abs<TAngle>(TAngle angle)
        where TAngle : struct, IAngle<TAngle>
        => TAngle.Abs(angle);

    /// <summary>
    /// Returns a value indicating the sign of angle.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>A number that indicates the sign of value, -1 if value is less than zero, 0 if value equal to zero, 1 if value is grater than zero.</returns>
    public static int Sign<TAngle>(TAngle angle)
        where TAngle : struct, IAngle<TAngle>
        => TAngle.Sign(angle);

    /// <summary>
    /// Performs a linear interpolation.
    /// </summary>
    /// <param name="a1">The first angle.</param>
    /// <param name="a2">The second angle.</param>
    /// <param name="t">A value that linearly interpolates between the a1 parameter and the a2 parameter.</param>
    /// <returns>The result of the linear interpolation.</returns>
    public static TAngle Lerp<TAngle, T>(TAngle a1, TAngle a2, T t)
        where TAngle : struct, IAngle<TAngle>
        where T : struct, IFloatingPoint<T>
        => TAngle.Lerp(a1, a2, t);

    /// <summary>
    /// Returns the smallest of two angles.
    /// </summary>
    /// <param name="left">The first of two angles to compare.</param>
    /// <param name="right">The second of two angles to compare.</param>
    /// <returns>The smallest of the two angles.</returns>
    public static TAngle Min<TAngle>(TAngle left, TAngle right)
        where TAngle : struct, IAngle<TAngle>
        => left < right ? left : right;

    /// <summary>
    /// Returns the largest of two angles.
    /// </summary>
    /// <param name="left">The first of two angles to compare.</param>
    /// <param name="right">The second of two angles to compare.</param>
    /// <returns>The largest of the two angles.</returns>
    public static TAngle Max<TAngle>(TAngle left, TAngle right)
        where TAngle : struct, IAngle<TAngle>
        => left > right ? left : right;

    #region classification

    /// <summary>
    /// Indicates whether the specified angle is equal to Zero when reduced.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the reduction of the absolute angle is zero; otherwise false.</returns>
    public static bool IsZero<TAngle>(TAngle angle)
        where TAngle : struct, IAngle<TAngle>
        => angle % TAngle.Full == TAngle.AdditiveIdentity;

    /// <summary>
    /// Indicates whether the specified angle is acute.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the reduction of the absolute angle is greater than zero and less than 90 degrees; otherwise false.</returns>
    public static bool IsAcute<TAngle>(TAngle angle)
        where TAngle : struct, IAngle<TAngle>
    {
        var reduced = Reduce(angle);
        return reduced > TAngle.AdditiveIdentity && reduced < TAngle.Right;
    }

    /// <summary>
    /// Indicates whether the specified angle is right.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the reduction of the absolute angle is 90 degrees; otherwise false.</returns>
    public static bool IsRight<TAngle>(TAngle angle)
        where TAngle : struct, IAngle<TAngle>
        => Reduce(angle) == TAngle.Right;

    /// <summary>
    /// Indicates whether the specified angle is obtuse.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the reduction of the absolute angle is greater than 90 degrees and less than 180 degrees; otherwise false.</returns>
    public static bool IsObtuse<TAngle>(TAngle angle)
        where TAngle : struct, IAngle<TAngle>
    {
        var reduced = Reduce(angle);
        return reduced > TAngle.Right && reduced < TAngle.Straight;
    }

    /// <summary>
    /// Indicates whether the specified angle is straight.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the reduction of the absolute angle is 180 degrees; otherwise false.</returns>
    public static bool IsStraight<TAngle>(TAngle angle)
        where TAngle : struct, IAngle<TAngle>
        => Reduce(angle) == TAngle.Straight;

    /// <summary>
    /// Indicates whether the specified angle is reflex.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the reduction of the absolute angle is greater than 180 degrees and less than 360 degrees; otherwise false.</returns>
    public static bool IsReflex<TAngle>(TAngle angle)
        where TAngle : struct, IAngle<TAngle>
        => Reduce(angle) > TAngle.Straight;

    /// <summary>
    /// Indicates whether the specified angle is oblique.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the angle is not right or a multiple of a right angle; otherwise false.</returns>
    public static bool IsOblique<TAngle>(TAngle angle)
        where TAngle : struct, IAngle<TAngle>
        => angle % TAngle.Right != TAngle.AdditiveIdentity;

    #endregion

    #region trigonometry

    public static Radians<TRadians> Acos<TRadians>(TRadians cos)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => new(TRadians.CreateChecked(Math.Acos(double.CreateChecked(cos))));

    public static Radians<TRadians> Acos<TCos, TRadians>(TCos cos)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TCos : struct, IFloatingPoint<TCos>
        => new(TRadians.CreateChecked(Math.Acos(double.CreateChecked(cos))));

    public static Radians<TRadians> Asin<TRadians>(TRadians sin)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => new(TRadians.CreateChecked(Math.Asin(double.CreateChecked(sin))));

    public static Radians<TRadians> Asin<TSin, TRadians>(TSin sin)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TSin : struct, IFloatingPoint<TSin>
        => new(TRadians.CreateChecked(Math.Asin(double.CreateChecked(sin))));

    public static Radians<TRadians> Atan<TRadians>(TRadians tan)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => new(TRadians.CreateChecked(Math.Atan(double.CreateChecked(tan))));

    public static Radians<TRadians> Atan<TTan, TRadians>(TTan tan)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TTan : struct, IFloatingPoint<TTan>
        => new(TRadians.CreateChecked(Math.Atan(double.CreateChecked(tan))));

    public static Radians<TRadians> Atan2<TRadians>(TRadians x, TRadians y)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => new(TRadians.CreateChecked(Math.Atan2(double.CreateChecked(x), double.CreateChecked(y))));

    public static Radians<TRadians> Atan2<TTan, TRadians>(TTan x, TTan y)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        where TTan : struct, IFloatingPoint<TTan>
        => new(TRadians.CreateChecked(Math.Atan2(double.CreateChecked(x), double.CreateChecked(y))));

    public static double Cos<TRadians>(Radians<TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Cos(double.CreateChecked(angle.Value));

    public static double Cosh<TRadians>(Radians<TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Cosh(double.CreateChecked(angle.Value));

    public static double Sin<TRadians>(Radians<TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Sin(double.CreateChecked(angle.Value));

    public static double Sinh<TRadians>(Radians<TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Sinh(double.CreateChecked(angle.Value));

    public static double Tan<TRadians>(Radians<TRadians> angle)
        where TRadians : struct, IFloatingPoint<TRadians>, IMinMaxValue<TRadians>
        => Math.Tan(double.CreateChecked(angle.Value));

    #endregion
}
