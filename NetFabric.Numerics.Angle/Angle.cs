using System.Numerics;
using System;

namespace NetFabric.Numerics;

public static class Angle
{
    /// <summary>
    /// Reduce an angle to positive and less than one revolution.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>The reduced angle.</returns>
    public static Angle<TUnits, T> Reduce<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(Utils.Reduce(angle));

    public static Angle<TUnits, T> GetReference<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(Utils.GetReference(angle));

    public static Quadrant GetQuadrant<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.GetQuadrant(angle, out _);

    /// <summary>
    /// Returns the absolute value of angle.
    /// </summary>
    /// <param name="angle">The angle for which to get its absolute.</param>
    /// <returns>
    /// The absolute of <paramref name="angle" />.
    /// </returns>
    public static Angle<TUnits, T> Abs<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(T.Abs(angle.Value));

    /// <summary>
    /// Returns a value indicating the sign of angle.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>A number that indicates the sign of value, -1 if value is less than zero, 0 if value equal to zero, 1 if value is grater than zero.</returns>
    public static int Sign<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => T.Sign(angle.Value);

    /// <summary>
    /// Performs a linear interpolation.
    /// </summary>
    /// <param name="a1">The first angle.</param>
    /// <param name="a2">The second angle.</param>
    /// <param name="t">A value that linearly interpolates between the a1 parameter and the a2 parameter.</param>
    /// <returns>The result of the linear interpolation.</returns>
    public static Angle<TUnits, T> Lerp<TUnits, T, TFactor>(Angle<TUnits, T> a1, Angle<TUnits, T> a2, TFactor t)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        where TFactor : struct, IFloatingPoint<TFactor>
        => new(Utils.Lerp<T, TFactor>(a1.Value, a2.Value, t));

    /// <summary>
    /// Returns the smallest of two angles.
    /// </summary>
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
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the reduction of the absolute angle is zero; otherwise false.</returns>
    public static bool IsZero<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle % Angle<TUnits, T>.Full == Angle<TUnits, T>.Zero;

    /// <summary>
    /// Indicates whether the specified angle is acute.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the reduction of the absolute angle is greater than zero and less than 90 degrees; otherwise false.</returns>
    public static bool IsAcute<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        var reduced = Utils.Reduce(angle);
        return reduced > T.Zero && reduced < Angle<TUnits, T>.Right.Value;
    }

    /// <summary>
    /// Indicates whether the specified angle is right.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the reduction of the absolute angle is 90 degrees; otherwise false.</returns>
    public static bool IsRight<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.Reduce(angle) == Angle<TUnits, T>.Right.Value;

    /// <summary>
    /// Indicates whether the specified angle is obtuse.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the reduction of the absolute angle is greater than 90 degrees and less than 180 degrees; otherwise false.</returns>
    public static bool IsObtuse<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        var reduced = Utils.Reduce(angle);
        return reduced > Angle<TUnits, T>.Right.Value && reduced < Angle<TUnits, T>.Straight.Value;
    }

    /// <summary>
    /// Indicates whether the specified angle is straight.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the reduction of the absolute angle is 180 degrees; otherwise false.</returns>
    public static bool IsStraight<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.Reduce(angle) == Angle<TUnits, T>.Straight.Value;

    /// <summary>
    /// Indicates whether the specified angle is reflex.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the reduction of the absolute angle is greater than 180 degrees and less than 360 degrees; otherwise false.</returns>
    public static bool IsReflex<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => Utils.Reduce(angle) > Angle<TUnits, T>.Straight.Value;

    /// <summary>
    /// Indicates whether the specified angle is oblique.
    /// </summary>
    /// <param name="angle">Source angle.</param>
    /// <returns>true if the angle is not right or a multiple of a right angle; otherwise false.</returns>
    public static bool IsOblique<TUnits, T>(Angle<TUnits, T> angle)
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle.Value % Angle<TUnits, T>.Right.Value != T.Zero;

    #endregion

    #region trigonometry

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
       var Degrees = TDegrees.CreateChecked(angle.Value);
       var minutes = TMinutes.CreateChecked(Math.Abs(double.CreateChecked(angle.Value) - double.CreateChecked(Degrees)) * 60.0);
       return (Degrees, minutes);
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
       var Degrees = TDegrees.CreateChecked(angle.Value);
       var decimalMinutes = Math.Abs(double.CreateChecked(angle.Value) - double.CreateChecked(Degrees)) * 60.0;
       var minutes = TMinutes.CreateChecked(decimalMinutes);
       var seconds = TSeconds.CreateChecked((decimalMinutes - double.CreateChecked(minutes)) * 60.0);
       return (Degrees, minutes, seconds);
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
}
