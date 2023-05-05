namespace NetFabric.Numerics.Geography;

public interface IGeodeticCoordinateSystem<TDatum>
    : ICoordinateSystem
    where TDatum : IDatum<TDatum>
{
    Datum<TDatum> Datum { get; }
}
