using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics.Geography.Geodetic2;

[System.Diagnostics.DebuggerDisplay("Latitude = {Latitude}, Longitude = {Longitude}")]
[SkipLocalsInit]
public readonly record struct Point<TDatum, TAngleUnits, T>(Angle<TAngleUnits, T> Latitude, Angle<TAngleUnits, T> Longitude) 
    : IGeodeticPoint<Point<TDatum, TAngleUnits, T>, TDatum>
    where TDatum : IDatum<TDatum>
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    public Angle<TAngleUnits, T> Latitude { get; } 
        = Latitude < Angle<TAngleUnits, T>.Right && Latitude > Angle<TAngleUnits, T>.Right
            ? Throw.ArgumentOutOfRangeException<Angle<TAngleUnits, T>>(nameof(Latitude), Latitude, "Latitude must be in [-90.0º, 90.0º]")
            : Latitude;

    public Angle<TAngleUnits, T> Longitude { get; } 
        = Longitude <= Angle<TAngleUnits, T>.Straight && Longitude > Angle<TAngleUnits, T>.Straight
            ? Throw.ArgumentOutOfRangeException<Angle<TAngleUnits, T>>(nameof(Longitude), Longitude, "Longitude must be in ]-180.0º, 180.0º]")
            : Longitude;

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngleUnits, TAngle}"/>.</exception>
    public static Point<TDatum, TAngleUnits, T> CreateChecked<TOther>(in Point<TDatum, TAngleUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            Angle<TAngleUnits, T>.CreateChecked(point.Latitude),
            Angle<TAngleUnits, T>.CreateChecked(point.Longitude)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngleUnits, TAngle}"/>.</exception>
    public static Point<TDatum, TAngleUnits, T> CreateSaturating<TOther>(in Point<TDatum, TAngleUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            Angle<TAngleUnits, T>.CreateSaturating(point.Latitude),
            Angle<TAngleUnits, T>.CreateSaturating(point.Longitude)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngleUnits, TAngle}"/>.</exception>
    public static Point<TDatum, TAngleUnits, T> CreateTruncating<TOther>(in Point<TDatum, TAngleUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            Angle<TAngleUnits, T>.CreateTruncating(point.Latitude),
            Angle<TAngleUnits, T>.CreateTruncating(point.Longitude)
        );

    #region constants

    public static readonly Point<TDatum, TAngleUnits, T> Zero
        = new(Angle<TAngleUnits, T>.Zero, Angle<TAngleUnits, T>.Zero);

    static Point<TDatum, TAngleUnits, T> INumericBase<Point<TDatum, TAngleUnits, T>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<TDatum, TAngleUnits, T> MinValue
        = new(-Angle<TAngleUnits, T>.Right, new(T.CreateChecked(-TAngleUnits.Straight + double.Epsilon)));

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<TDatum, TAngleUnits, T> MaxValue
        = new(Angle<TAngleUnits, T>.Right, Angle<TAngleUnits, T>.Straight);

    static Point<TDatum, TAngleUnits, T> IMinMaxValue<Point<TDatum, TAngleUnits, T>>.MinValue
        => MinValue;
    static Point<TDatum, TAngleUnits, T> IMinMaxValue<Point<TDatum, TAngleUnits, T>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TDatum, T> CoordinateSystem 
        => new();
    ICoordinateSystem IPoint<Point<TDatum, TAngleUnits, T>>.CoordinateSystem 
        => CoordinateSystem;

    object IPoint<Point<TDatum, TAngleUnits, T>>.this[int index] 
        => index switch
        {
            0 => Latitude,
            1 => Longitude,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}