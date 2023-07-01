using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Angle
{
    #region from degrees

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Degrees, T> ToDegrees<T>(Angle<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Degrees, T> ToDegrees<T>(AngleReduced<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    #endregion

    #region from radians

    static class DegreesInRadians<T>
        where T : struct, IFloatingPoint<T>
    {
        public static readonly T Value = T.CreateChecked(Degrees.Full) / T.CreateChecked(Radians.Full);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Degrees, T> ToDegrees<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * DegreesInRadians<T>.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Degrees, T> ToDegrees<T>(AngleReduced<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * DegreesInRadians<T>.Value);

    #endregion

    #region from gradians

    static class DegreesInGradians<T>
        where T : struct, IFloatingPoint<T>
    {
        public static readonly T Value = T.CreateChecked(Degrees.Full) / T.CreateChecked(Gradians.Full);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Degrees, T> ToDegrees<T>(Angle<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * DegreesInGradians<T>.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Degrees, T> ToDegrees<T>(AngleReduced<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * DegreesInGradians<T>.Value);

    #endregion

    #region from revolutions

    static class DegreesInRevolutions<T>
        where T : struct, IFloatingPoint<T>
    {
        public static readonly T Value = T.CreateChecked(Degrees.Full) / T.CreateChecked(Revolutions.Full);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Degrees, T> ToDegrees<T>(Angle<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * DegreesInRevolutions<T>.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Degrees, T> ToDegrees<T>(AngleReduced<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * DegreesInRevolutions<T>.Value);

    #endregion

    public static Angle<Degrees, TMinutes> ToDegrees<TDegrees, TMinutes>(TDegrees degrees, TMinutes minutes)
        where TDegrees : struct, IBinaryInteger<TDegrees>, ISignedNumber<TDegrees>
        where TMinutes : struct, IFloatingPoint<TMinutes>, IMinMaxValue<TMinutes>
    {
        if (minutes < TMinutes.Zero || minutes >= TMinutes.CreateChecked(60.0))
            Throw.ArgumentOutOfRangeException(nameof(minutes), minutes, "Argument must be positive and less than 60.");

        var floatDegrees = TMinutes.CreateChecked(degrees);
        var reciprocal60 = TMinutes.One / TMinutes.CreateChecked(60.0);
        return TDegrees.Sign(degrees) < 0
            ? new(floatDegrees - (minutes * reciprocal60))
            : new(floatDegrees + (minutes * reciprocal60));
    }

    public static Angle<Degrees, TSeconds> ToDegrees<TDegrees, TMinutes, TSeconds>(TDegrees degrees, TMinutes minutes, TSeconds seconds)
        where TDegrees : struct, IBinaryInteger<TDegrees>, ISignedNumber<TDegrees>
        where TMinutes : struct, IBinaryInteger<TMinutes>
        where TSeconds : struct, IFloatingPoint<TSeconds>, IMinMaxValue<TSeconds>
    {
        if (minutes < TMinutes.Zero || minutes >= TMinutes.CreateChecked(60))
            Throw.ArgumentOutOfRangeException(nameof(minutes), minutes, "Argument must be positive and less than 60.");
        if (seconds < TSeconds.Zero || seconds >= TSeconds.CreateChecked(60.0))
            Throw.ArgumentOutOfRangeException(nameof(seconds), seconds, "Argument must be positive and less than 60.");

        var floatDegrees = TSeconds.CreateChecked(degrees);
        var floatMinutes = TSeconds.CreateChecked(minutes);
        var reciprocal60 = TSeconds.One / TSeconds.CreateChecked(60.0);
        var reciprocal3600 = TSeconds.One / TSeconds.CreateChecked(3600.0);
        return TDegrees.Sign(degrees) < 0
            ? new(floatDegrees - (floatMinutes * reciprocal60) - (seconds * reciprocal3600))
            : new(floatDegrees + (floatMinutes * reciprocal60) + (seconds * reciprocal3600));
    }

    /// <summary>
    /// Gets the value of the angle expressed in Degrees and minutes.
    /// </summary>
    public static (TDegrees Degrees, TMinutes Minutes) ToDegreesMinutes<T, TDegrees, TMinutes>(Angle<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        where TDegrees : IBinaryInteger<TDegrees>
        where TMinutes : IFloatingPoint<TMinutes>
    {
        var sixty = T.CreateChecked(60.0);
        var degrees = TDegrees.CreateChecked(angle.Value);
        var minutes = TMinutes.CreateChecked(T.Abs(angle.Value - T.CreateChecked(degrees)) * sixty);
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
        var sixty = T.CreateChecked(60.0);
        var degrees = TDegrees.CreateChecked(angle.Value);
        var decimalMinutes = T.Abs(angle.Value - T.CreateChecked(degrees)) * sixty;
        var minutes = TMinutes.CreateChecked(decimalMinutes);
        var seconds = TSeconds.CreateChecked((decimalMinutes - T.CreateChecked(minutes)) * sixty);
        return (degrees, minutes, seconds);
    }
}
