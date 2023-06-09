namespace NetFabric.Numerics;

public static partial class Angle
{
    #region from degrees

    static class GradiansInDegrees<T>
        where T : struct, IFloatingPoint<T>
    {
        public static readonly T Value = T.CreateChecked(Gradians.Full) / T.CreateChecked(Degrees.Full);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Gradians, T> ToGradians<T>(Angle<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * GradiansInDegrees<T>.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Gradians, T> ToGradians<T>(AngleReduced<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * GradiansInDegrees<T>.Value);

    #endregion

    #region from radians

    static class GradiansInRadians<T>
        where T : struct, IFloatingPoint<T>
    {
        public static readonly T Value = T.CreateChecked(Gradians.Full) / T.CreateChecked(Radians.Full);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Gradians, T> ToGradians<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * GradiansInRadians<T>.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Gradians, T> ToGradians<T>(AngleReduced<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * GradiansInRadians<T>.Value);

    #endregion

    #region from gradians

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Gradians, T> ToGradians<T>(Angle<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Gradians, T> ToGradians<T>(AngleReduced<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    #endregion

    #region from revolutions

    static class GradiansInRevolutions<T>
        where T : struct, IFloatingPoint<T>
    {
        public static readonly T Value = T.CreateChecked(Gradians.Full) / T.CreateChecked(Revolutions.Full);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Gradians, T> ToGradians<T>(Angle<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * GradiansInRevolutions<T>.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Gradians, T> ToGradians<T>(AngleReduced<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * GradiansInRevolutions<T>.Value);

    #endregion
}
