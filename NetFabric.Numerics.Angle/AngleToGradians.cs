using System.Numerics;
using System;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics;

public static partial class Angle
{
    #region from degrees

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static TTo DegreesToGradians<TFrom, TTo>(TFrom value)
        where TFrom : struct, IFloatingPoint<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => TTo.CreateChecked(double.CreateChecked(value) * 100.0 / 90.0);

    public static Angle<Gradians, TTo> ToGradians<TFrom, TTo>(Angle<Degrees, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(DegreesToGradians<TFrom, TTo>(angle.Value));

    public static Angle<Gradians, T> ToGradians<T>(Angle<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToGradians<T, T>(angle);

    public static AngleReduced<Gradians, TTo> ToGradians<TFrom, TTo>(AngleReduced<Degrees, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(DegreesToGradians<TFrom, TTo>(angle.Value));

    public static AngleReduced<Gradians, T> ToGradians<T>(AngleReduced<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToGradians<T, T>(angle);

    #endregion

    #region from radians

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static TTo RadiansToGradians<TFrom, TTo>(TFrom value)
        where TFrom : struct, IFloatingPoint<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => TTo.CreateChecked(double.CreateChecked(value) * 200.0 / Math.PI);

    public static Angle<Gradians, TTo> ToGradians<TFrom, TTo>(Angle<Radians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(RadiansToGradians<TFrom, TTo>(angle.Value));

    public static Angle<Gradians, T> ToGradians<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToGradians<T, T>(angle);

    public static AngleReduced<Gradians, TTo> ToGradians<TFrom, TTo>(AngleReduced<Radians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(RadiansToGradians<TFrom, TTo>(angle.Value));

    public static AngleReduced<Gradians, T> ToGradians<T>(AngleReduced<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToGradians<T, T>(angle);

    #endregion

    #region from gradians

    public static Angle<Gradians, TTo> ToGradians<TFrom, TTo>(Angle<Gradians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(TTo.CreateChecked(angle.Value));

    public static Angle<Gradians, T> ToGradians<T>(Angle<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    public static AngleReduced<Gradians, TTo> ToGradians<TFrom, TTo>(AngleReduced<Gradians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(TTo.CreateChecked(angle.Value));

    public static AngleReduced<Gradians, T> ToGradians<T>(AngleReduced<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    #endregion

    #region from revolutions

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static TTo RevolutionsToGradians<TFrom, TTo>(TFrom value)
        where TFrom : struct, IFloatingPoint<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => TTo.CreateChecked(double.CreateChecked(value) * 400.0);

    public static Angle<Gradians, TTo> ToGradians<TFrom, TTo>(Angle<Revolutions, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(RevolutionsToGradians<TFrom, TTo>(angle.Value));

    public static Angle<Gradians, T> ToGradians<T>(Angle<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToGradians<T, T>(angle);

    public static AngleReduced<Gradians, TTo> ToGradians<TFrom, TTo>(AngleReduced<Revolutions, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(RevolutionsToGradians<TFrom, TTo>(angle.Value));

    public static AngleReduced<Gradians, T> ToGradians<T>(AngleReduced<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToGradians<T, T>(angle);

    #endregion
}
