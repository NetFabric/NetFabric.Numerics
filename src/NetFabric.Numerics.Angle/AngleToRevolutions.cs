namespace NetFabric.Numerics;

public static partial class Angle
{
    #region from degrees

    static class RevolutionsInDegrees<T>
        where T : struct, IFloatingPoint<T>
    {
        public static readonly T Value = T.CreateChecked(Revolutions.Full) / T.CreateChecked(Degrees.Full);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Revolutions, T> ToRevolutions<T>(Angle<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * RevolutionsInDegrees<T>.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Revolutions, T> ToRevolutions<T>(AngleReduced<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * RevolutionsInDegrees<T>.Value);

    #endregion

    #region from radians

    static class RevolutionsInRadians<T>
        where T : struct, IFloatingPoint<T>
    {
        public static readonly T Value = T.CreateChecked(Revolutions.Full) / T.CreateChecked(Radians.Full);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Revolutions, T> ToRevolutions<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * RevolutionsInRadians<T>.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Revolutions, T> ToRevolutions<T>(AngleReduced<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * RevolutionsInRadians<T>.Value);

    #endregion

    #region from gradians

    static class RevolutionsInGradians<T>
        where T : struct, IFloatingPoint<T>
    {
        public static readonly T Value = T.CreateChecked(Revolutions.Full) / T.CreateChecked(Gradians.Full);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<Revolutions, T> ToRevolutions<T>(Angle<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * RevolutionsInGradians<T>.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleReduced<Revolutions, T> ToRevolutions<T>(AngleReduced<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(angle.Value * RevolutionsInGradians<T>.Value);

    #endregion
}
