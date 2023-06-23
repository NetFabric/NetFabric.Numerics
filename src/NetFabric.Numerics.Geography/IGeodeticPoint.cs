namespace NetFabric.Numerics.Geography;

/// <summary>
/// Represents a point in a geodetic coordinate system.
/// </summary>
public interface IGeodeticPoint<TSelf, TDatum>
    : IPoint<TSelf>
    where TSelf : struct, IPoint<TSelf>?
    where TDatum : IDatum<TDatum>
{

}