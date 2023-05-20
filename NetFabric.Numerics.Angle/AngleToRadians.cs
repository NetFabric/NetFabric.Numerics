using System.Numerics;
using System;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics;

public static partial class Angle
{
    #region from degrees

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static TTo DegreesToRadians<TFrom, TTo>(TFrom value)
        where TFrom : struct, IFloatingPoint<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => TTo.CreateChecked(double.CreateChecked(value) * Math.PI / 180.0);

    public static Angle<Radians, TTo> ToRadians<TFrom, TTo>(Angle<Degrees, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(DegreesToRadians<TFrom, TTo>(angle.Value));

    public static Angle<Radians, T> ToRadians<T>(Angle<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRadians<T, T>(angle);

    public static AngleReduced<Radians, TTo> ToRadians<TFrom, TTo>(AngleReduced<Degrees, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(DegreesToRadians<TFrom, TTo>(angle.Value));

    public static AngleReduced<Radians, T> ToRadians<T>(AngleReduced<Degrees, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRadians<T, T>(angle);

    #endregion

    #region from radians

    public static Angle<Radians, TTo> ToRadians<TFrom, TTo>(Angle<Radians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(TTo.CreateChecked(angle.Value));

    public static Angle<Radians, T> ToRadians<T>(Angle<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    public static AngleReduced<Radians, TTo> ToRadians<TFrom, TTo>(AngleReduced<Radians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(TTo.CreateChecked(angle.Value));

    public static AngleReduced<Radians, T> ToRadians<T>(AngleReduced<Radians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => angle;

    #endregion

    #region from gradians

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static TTo GradiansToRadians<TFrom, TTo>(TFrom value)
        where TFrom : struct, IFloatingPoint<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => TTo.CreateChecked(double.CreateChecked(value) * Math.PI / 200.0);

    public static Angle<Radians, TTo> ToRadians<TFrom, TTo>(Angle<Gradians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(GradiansToRadians<TFrom, TTo>(angle.Value));

    public static Angle<Radians, T> ToRadians<T>(Angle<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRadians<T, T>(angle);

    public static AngleReduced<Radians, TTo> ToRadians<TFrom, TTo>(AngleReduced<Gradians, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(GradiansToRadians<TFrom, TTo>(angle.Value));

    public static AngleReduced<Radians, T> ToRadians<T>(AngleReduced<Gradians, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRadians<T, T>(angle);

    #endregion

    #region from revolutions

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static TTo RevolutionsToRadians<TFrom, TTo>(TFrom value)
        where TFrom : struct, IFloatingPoint<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => TTo.CreateChecked(double.CreateChecked(value) * Math.PI / 0.5);

    public static Angle<Radians, TTo> ToRadians<TFrom, TTo>(Angle<Revolutions, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(RevolutionsToRadians<TFrom, TTo>(angle.Value));

    public static Angle<Radians, T> ToRadians<T>(Angle<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRadians<T, T>(angle);

    public static AngleReduced<Radians, TTo> ToRadians<TFrom, TTo>(AngleReduced<Revolutions, TFrom> angle)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(RevolutionsToRadians<TFrom, TTo>(angle.Value));

    public static AngleReduced<Radians, T> ToRadians<T>(AngleReduced<Revolutions, T> angle)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ToRadians<T, T>(angle);

    #endregion
}
