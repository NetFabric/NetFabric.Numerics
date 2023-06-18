namespace NetFabric.Numerics.Geography;

public readonly record struct Datum<TDatum>
    where TDatum : IDatum<TDatum>?
{
    public string Name 
        => TDatum.Name;
    public Offset<double> Offset 
        => TDatum.Offset;
    public Ellipsoid<double> Ellipsoid 
        => TDatum.Ellipsoid;
}
