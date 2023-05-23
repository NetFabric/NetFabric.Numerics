namespace NetFabric.Numerics.Geography;

/// <summary>
/// Represents a point in a geodetic coordinate system.
/// </summary>
public interface IGeodeticPoint<TSelf, TDatum>
    : IPoint<TSelf>
    where TSelf : IPoint<TSelf>?
    where TDatum : IDatum<TDatum>
{

}