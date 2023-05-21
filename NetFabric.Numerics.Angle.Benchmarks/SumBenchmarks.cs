using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;

namespace NetFabric.Numerics.Benchmarks;

public class SumBenchmarks
{
    Angle<Degrees, double>[]? array;
    IEnumerable<Angle<Degrees, double>>? enumerable;

    [Params(0, 1, 10, 1_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        enumerable = GetEnumerable(Count);
        array = enumerable.ToArray();
    }

    [Benchmark(Baseline = true)]
    public Angle<Degrees, double> LinqArray()
        => new(Enumerable.Sum(array!, angle => angle.Value));

    [Benchmark]
    public Angle<Degrees, double> LinqEnumerable()
        => new(Enumerable.Sum(enumerable!, angle => angle.Value));

    [Benchmark]
    public Angle<Degrees, double> AngleArray()
        => array!.Sum();

    [Benchmark]
    public Angle<Degrees, double> AngleSpan()
        => array!.AsSpan().Sum();

    [Benchmark]
    public Angle<Degrees, double> AngleEnumerable()
        => enumerable!.Sum();

    static IEnumerable<Angle<Degrees, double>> GetEnumerable(int count)
    {
        for (var value = 0; value < count; value++)
            yield return new Angle<Degrees, double>(value);
    }
}
