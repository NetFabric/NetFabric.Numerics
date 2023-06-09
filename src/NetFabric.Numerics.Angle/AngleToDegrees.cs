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
