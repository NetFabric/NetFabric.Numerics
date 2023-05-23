namespace NetFabric.Numerics.Geography;

public readonly record struct Offset(Cartesian3.Point<double> XYZOffset, double RX, double RY, double RZ, double SC)
{
    public static readonly Offset Zero = new(new(0.0, 0.0, 0.0), 0.0, 0.0, 0.0, 0.0);
}