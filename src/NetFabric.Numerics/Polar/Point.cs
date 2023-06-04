namespace NetFabric.Numerics.Polar;

/// <summary>
/// Represents a point as an immutable struct.
/// </summary>
/// <typeparam name="TAngleUnits">The angle units used for the azimuth coordinate.</typeparam>
/// <typeparam name="TAngle">The type used by the angle of the azimuth coordinate.</typeparam>
/// <typeparam name="TRadius">The type of the radius coordinate.</typeparam>
/// <param name="Azimuth">The azimuth coordinate.</param>
/// <param name="Radius">The radius coordinate.</param>
[System.Diagnostics.DebuggerDisplay("Azimuth = {Azimuth}, Radius = {Radius}")]
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

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TAngleUnits, TAngle, TRadius> CoordinateSystem
        => new();
    ICoordinateSystem IPoint<Point<TAngleUnits, TAngle, TRadius>>.CoordinateSystem
        => CoordinateSystem;

    object IPoint<Point<TAngleUnits, TAngle, TRadius>>.this[int index]
        => index switch
        {
            0 => Azimuth,
            1 => Radius,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}
