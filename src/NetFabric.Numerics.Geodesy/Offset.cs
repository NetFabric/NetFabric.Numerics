using System.Numerics;

namespace NetFabric.Numerics.Geodesy;

/// <summary>
/// Represents a geodetic offset in a three-dimensional space.
/// </summary>
/// <typeparam name="T">The type of the offset values, which must be a floating-point type implementing IFloatingPoint and IMinMaxValue.</typeparam>
public readonly record struct Offset<T>(Rectangular3D.Point<T> XYZOffset, T RX, T RY, T RZ, T SC)
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    /// <summary>
    /// A constant zero offset.
    /// </summary>
    public static readonly Offset<T> Zero = new(Rectangular3D.Point<T>.Zero, T.Zero, T.Zero, T.Zero, T.Zero);
}
