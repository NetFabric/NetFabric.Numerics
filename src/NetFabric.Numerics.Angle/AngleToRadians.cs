namespace NetFabric.Numerics;

public static partial class Angle
{
    #region from degrees

    static class RadiansInDegrees<T>
        where T : struct, IFloatingPoint<T>
    {
        public static readonly T Value = T.CreateChecked(Radians.Full) / T.CreateChecked(Degrees.Full);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Radians, T> ToRadians<T>(Angle<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * RadiansInDegrees<T>.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Radians, T> ToRadians<T>(AngleReduced<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * RadiansInDegrees<T>.Value);

    #endregion

    #region from radians

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Radians, T> ToRadians<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Radians, T> ToRadians<T>(AngleReduced<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    #endregion

    #region from gradians

    static class RadiansInGradians<T>
        where T : struct, IFloatingPoint<T>
    {
        public static readonly T Value = T.CreateChecked(Radians.Full) / T.CreateChecked(Gradians.Full);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Radians, T> ToRadians<T>(Angle<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * RadiansInGradians<T>.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Radians, T> ToRadians<T>(AngleReduced<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * RadiansInGradians<T>.Value);

    #endregion

    #region from revolutions

    static class RadiansInRevolutions<T>
        where T : struct, IFloatingPoint<T>
    {
        public static readonly T Value = T.CreateChecked(Radians.Full) / T.CreateChecked(Revolutions.Full);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Radians, T> ToRadians<T>(Angle<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * RadiansInRevolutions<T>.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Radians, T> ToRadians<T>(AngleReduced<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * RadiansInRevolutions<T>.Value);

    #endregion
}
