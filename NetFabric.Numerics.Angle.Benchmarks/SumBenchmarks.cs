using System.Linq;
using BenchmarkDotNet.Attributes;

namespace NetFabric.Numerics.Benchmarks;

public class SumBenchmarks
{
    Angle<Degrees, double>[] array;
    IEnumerable<Angle<Degrees, double>> enumerable;

    [GlobalSetup]
    public void GlobalSetup()
    {
        enumerable = GetEnumerable();
        array = enumerable.ToArray();
    }

    [Benchmark(Baseline = true)]
    public Angle<Degrees, double> LinqArray()
        => new(array.Sum(angle => angle.Value));

    [Benchmark]
    public Angle<Degrees, double> LinqEnumerable()
        => new(enumerable.Sum(angle => angle.Value));

    [Benchmark]
    public Angle<Degrees, double> AngleArray()
        => array.Sum();

    [Benchmark]
    public Angle<Degrees, double> AngleSpan()
        => array.AsSpan().Sum();

    [Benchmark]
    public Angle<Degrees, double> AngleEnumerable()
        => array.Sum();

    static IEnumerable<Angle<Degrees, double>> GetEnumerable()
    {
        for (var value = 0; value < 360; value++)
            yield return new Angle<Degrees, double>(value);
    }
}
