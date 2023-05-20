using System.Numerics;
using System;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics;

public static partial class Angle
{
    #region from degrees

    public static Angle<Degrees, TTo> ToDegrees<TFrom, TTo>(Angle<Degrees, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(TTo.CreateChecked(angle.Value));

    public static Angle<Degrees, T> ToDegrees<T>(Angle<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    public static AngleReduced<Degrees, TTo> ToDegrees<TFrom, TTo>(AngleReduced<Degrees, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(TTo.CreateChecked(angle.Value));

    public static AngleReduced<Degrees, T> ToDegrees<T>(AngleReduced<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    #endregion

    #region from radians

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static TTo RadiansToDegrees<TFrom, TTo>(TFrom value)
        where TFrom : struct, IFloatingPoint<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => TTo.CreateChecked(double.CreateChecked(value) * 180.0 / Math.PI);

    public static Angle<Degrees, TTo> ToDegrees<TFrom, TTo>(Angle<Radians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(RadiansToDegrees<TFrom, TTo>(angle.Value));

    public static Angle<Degrees, T> ToDegrees<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToDegrees<T, T>(angle);

    public static AngleReduced<Degrees, TTo> ToDegrees<TFrom, TTo>(AngleReduced<Radians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(RadiansToDegrees<TFrom, TTo>(angle.Value));

    public static AngleReduced<Degrees, T> ToDegrees<T>(AngleReduced<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToDegrees<T, T>(angle);

    #endregion

    #region from gradians

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static TTo GradiansToDegrees<TFrom, TTo>(TFrom value)
        where TFrom : struct, IFloatingPoint<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => TTo.CreateChecked(double.CreateChecked(value) * 90.0 / 100.0);

    public static Angle<Degrees, TTo> ToDegrees<TFrom, TTo>(Angle<Gradians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(GradiansToDegrees<TFrom, TTo>(angle.Value));

    public static Angle<Degrees, T> ToDegrees<T>(Angle<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToDegrees<T, T>(angle);

    public static AngleReduced<Degrees, TTo> ToDegrees<TFrom, TTo>(AngleReduced<Gradians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(GradiansToDegrees<TFrom, TTo>(angle.Value));

    public static AngleReduced<Degrees, T> ToDegrees<T>(AngleReduced<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToDegrees<T, T>(angle);

    #endregion

    #region from revolutions

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static TTo RevolutionsToDegrees<TFrom, TTo>(TFrom value)
        where TFrom : struct, IFloatingPoint<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => TTo.CreateChecked(double.CreateChecked(value) * 360.0);

    public static Angle<Degrees, TTo> ToDegrees<TFrom, TTo>(Angle<Revolutions, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(RevolutionsToDegrees<TFrom, TTo>(angle.Value));

    public static Angle<Degrees, T> ToDegrees<T>(Angle<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToDegrees<T, T>(angle);

    public static AngleReduced<Degrees, TTo> ToDegrees<TFrom, TTo>(AngleReduced<Revolutions, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(RevolutionsToDegrees<TFrom, TTo>(angle.Value));

    public static AngleReduced<Degrees, T> ToDegrees<T>(AngleReduced<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToDegrees<T, T>(angle);

    #endregion

    public static Angle<Degrees, TTo> ToDegrees<TTo>(int degrees, double minutes)
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
    {
        if (minutes < 0.0 || minutes >= 60.0)
            Throw.ArgumentOutOfRangeException(nameof(minutes), minutes, "Argument must be positive and less than 60.");

        return degrees < 0 
            ? new(TTo.CreateChecked(degrees - (minutes / 60.0)))
            : new(TTo.CreateChecked(degrees + (minutes / 60.0)));
    }

    public static Angle<Degrees, TTo> ToDegrees<TTo>(int degrees, int minutes, double seconds)
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
    {
        if (minutes < 0.0 || minutes >= 60.0)
            Throw.ArgumentOutOfRangeException(nameof(minutes), minutes, "Argument must be positive and less than 60.");
        if (seconds < 0.0 || seconds >= 60.0)
            Throw.ArgumentOutOfRangeException(nameof(seconds), seconds, "Argument must be positive and less than 60.");

        return degrees < 0
            ? new(TTo.CreateChecked(degrees - (minutes / 60.0) - (seconds / 3600.0)))
            : new(TTo.CreateChecked(degrees + (minutes / 60.0) + (seconds / 3600.0)));
    }

    /// <summary>
    /// Gets the value of the angle expressed in Degrees and minutes.
    /// </summary>
    public static (TDegrees Degrees, TMinutes Minutes) ToDegreesMinutes<TFrom, TDegrees, TMinutes>(Angle<Degrees, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
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
    public static (TDegrees Degrees, TMinutes Minutes, TSeconds Seconds) ToDegreesMinutesSeconds<TFrom, TDegrees, TMinutes, TSeconds>(Angle<Degrees, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
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
}
