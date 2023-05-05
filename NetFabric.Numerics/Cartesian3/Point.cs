using System;
using System.Numerics;

namespace NetFabric.Numerics.Cartesian3;

public readonly record struct Point<T>(T X, T Y, T Z) 
    : IPoint<Point<T>>
    where T: struct, INumber<T>, IMinMaxValue<T>
{
    #region constants

    public static readonly Point<T> Zero = new(T.Zero, T.Zero, T.Zero);

    static Point<T> IPoint<Point<T>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<T> MinValue = new(T.MinValue, T.MinValue, T.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<T> MaxValue = new(T.MaxValue, T.MaxValue, T.MaxValue);

    static Point<T> IMinMaxValue<Point<T>>.MinValue
        => MinValue;
    static Point<T> IMinMaxValue<Point<T>>.MaxValue
        => MaxValue;

    #endregion

    public CoordinateSystem<T> CoordinateSystem 
        => new();
    ICoordinateSystem IPoint<Point<T>>.CoordinateSystem 
        => CoordinateSystem;

    object IPoint<Point<T>>.this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range")
        };
}

public static class Point
{
    public static double Distance<T>(Point<T> from, Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Math.Sqrt(SquareOfDistance(from, to));

    public static double SquareOfDistance<T>(Point<T> from, Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Math.Pow(double.CreateChecked(to.X - from.X), 2.0) + 
            Math.Pow(double.CreateChecked(to.Y - from.Y), 2.0) +
            Math.Pow(double.CreateChecked(to.Z - from.Z), 2.0);
}
