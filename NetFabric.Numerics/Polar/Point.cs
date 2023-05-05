using System;
using System.Numerics;

namespace NetFabric.Numerics.Polar;

public readonly record struct Point<TAngleUnits, TAngle, TRadius>(Angle<TAngleUnits, TAngle> Azimuth, TRadius Radius) 
    : IPoint<Point<TAngleUnits, TAngle, TRadius>>
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
    where TRadius : struct, IFloatingPoint<TRadius>, IMinMaxValue<TRadius>
{
    #region constants

    public static readonly Point<TAngleUnits, TAngle, TRadius> Zero = new(Angle<TAngleUnits, TAngle>.Zero, TRadius.Zero);

    static Point<TAngleUnits, TAngle, TRadius> IPoint<Point<TAngleUnits, TAngle, TRadius>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<TAngleUnits, TAngle, TRadius> MinValue = new(Angle<TAngleUnits, TAngle>.MinValue, TRadius.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<TAngleUnits, TAngle, TRadius> MaxValue = new(Angle<TAngleUnits, TAngle>.MaxValue, TRadius.MaxValue);

    static Point<TAngleUnits, TAngle, TRadius> IMinMaxValue<Point<TAngleUnits, TAngle, TRadius>>.MinValue
        => MinValue;
    static Point<TAngleUnits, TAngle, TRadius> IMinMaxValue<Point<TAngleUnits, TAngle, TRadius>>.MaxValue
        => MaxValue;

    #endregion

    public CoordinateSystem<TAngleUnits, TAngle, TRadius> CoordinateSystem 
        => new();
    ICoordinateSystem IPoint<Point<TAngleUnits, TAngle, TRadius>>.CoordinateSystem 
        => CoordinateSystem;

    object IPoint<Point<TAngleUnits, TAngle, TRadius>>.this[int index] 
        => index switch
        {
            0 => Azimuth,
            1 => Radius,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}