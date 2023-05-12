using System.Linq;
using BenchmarkDotNet.Attributes;

namespace NetFabric.Numerics.Benchmarks;

public class SumBenchmarks
{
    static readonly Angle<Degrees, double>[] source 
        = Enumerable.Range(0, 360)
        .Select(value => new Angle<Degrees, double>(value))
        .ToArray();

    [Benchmark(Baseline = true)]
    public Angle<Degrees, double> LinqArray()
        => new(source.Sum(angle => angle.Value));

    [Benchmark]
    public Angle<Degrees, double> LinqEnumerable()
        => new(source.AsEnumerable().Sum(angle => angle.Value));

    [Benchmark]
    public Angle<Degrees, double> AngleArray()
        => source.Sum();

    [Benchmark]
    public Angle<Degrees, double> AngleEnumerable()
        => source.Sum();
}
