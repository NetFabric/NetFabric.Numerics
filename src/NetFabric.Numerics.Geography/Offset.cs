using System.Numerics;

namespace NetFabric.Numerics.Geography;

public readonly record struct Offset<T>(Rectangular3D.Point<T> XYZOffset, T RX, T RY, T RZ, T SC)
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    public static readonly Offset<T> Zero = new(new(T.Zero, T.Zero, T.Zero), T.Zero, T.Zero, T.Zero, T.Zero);
}