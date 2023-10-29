using System.Numerics;

namespace NetFabric.Numerics.Geodesy;

public abstract class Datum<T>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    public abstract string Name { get; }

    public abstract Offset<T> Offset { get; }
    public abstract Ellipsoid<T> Ellipsoid { get; }
}

public sealed class Datum<TDatum, T>
    : Datum<T>
    where TDatum : IDatum<T>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    static readonly Lazy<Datum<TDatum, T>> lazyInstance = new(() => new());

    private Datum() {}

    internal static Datum<TDatum, T> Instance
        => lazyInstance.Value;

    public override string Name 
        => TDatum.Name;
    public override Offset<T> Offset 
        => TDatum.Offset;
    public override Ellipsoid<T> Ellipsoid 
        => TDatum.Ellipsoid;
}
