namespace NetFabric.Numerics;

public static partial class Angle
{
    #region from degrees

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static TTo DegreesToRevolutions<TFrom, TTo>(TFrom value)
        where TFrom : struct, IFloatingPoint<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => TTo.CreateChecked(double.CreateChecked(value) * 1.0 / 360.0);

    public static Angle<Revolutions, TTo> ToRevolutions<TFrom, TTo>(Angle<Degrees, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(DegreesToRevolutions<TFrom, TTo>(angle.Value));

    public static Angle<Revolutions, T> ToRevolutions<T>(Angle<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRevolutions<T, T>(angle);

    public static AngleReduced<Revolutions, TTo> ToRevolutions<TFrom, TTo>(AngleReduced<Degrees, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(DegreesToRevolutions<TFrom, TTo>(angle.Value));

    public static AngleReduced<Revolutions, T> ToRevolutions<T>(AngleReduced<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRevolutions<T, T>(angle);

    #endregion

    #region from radians

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static TTo RadiansToRevolutions<TFrom, TTo>(TFrom value)
        where TFrom : struct, IFloatingPoint<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => TTo.CreateChecked(double.CreateChecked(value) * 0.5 / Math.PI);

    public static Angle<Revolutions, TTo> ToRevolutions<TFrom, TTo>(Angle<Radians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(RadiansToRevolutions<TFrom, TTo>(angle.Value));

    public static Angle<Revolutions, T> ToRevolutions<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRevolutions<T, T>(angle);

    public static AngleReduced<Revolutions, TTo> ToRevolutions<TFrom, TTo>(AngleReduced<Radians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(RadiansToRevolutions<TFrom, TTo>(angle.Value));

    public static AngleReduced<Revolutions, T> ToRevolutions<T>(AngleReduced<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRevolutions<T, T>(angle);

    #endregion

    #region from gradians

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static TTo GradiansToRevolutions<TFrom, TTo>(TFrom value)
        where TFrom : struct, IFloatingPoint<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => TTo.CreateChecked(double.CreateChecked(value) * 1.0 / 400.0);

    public static Angle<Revolutions, TTo> ToRevolutions<TFrom, TTo>(Angle<Gradians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(GradiansToRevolutions<TFrom, TTo>(angle.Value));

    public static Angle<Revolutions, T> ToRevolutions<T>(Angle<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRevolutions<T, T>(angle);

    public static AngleReduced<Revolutions, TTo> ToRevolutions<TFrom, TTo>(AngleReduced<Gradians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(GradiansToRevolutions<TFrom, TTo>(angle.Value));

    public static AngleReduced<Revolutions, T> ToRevolutions<T>(AngleReduced<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRevolutions<T, T>(angle);

    #endregion

    #region from revolutions

    public static Angle<Revolutions, TTo> ToRevolutions<TFrom, TTo>(Angle<Revolutions, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(TTo.CreateChecked(angle.Value));

    public static Angle<Revolutions, T> ToRevolutions<T>(Angle<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    public static AngleReduced<Revolutions, TTo> ToRevolutions<TFrom, TTo>(AngleReduced<Revolutions, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(TTo.CreateChecked(angle.Value));

    public static AngleReduced<Revolutions, T> ToRevolutions<T>(AngleReduced<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    #endregion
}
