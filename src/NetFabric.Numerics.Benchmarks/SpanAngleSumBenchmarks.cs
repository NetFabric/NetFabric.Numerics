using BenchmarkDotNet.Attributes;

namespace NetFabric.Numerics.Benchmarks;

public class SpanAngleSumBenchmarks
{
    Angle<Degrees, Half>[]? arrayHalf;
    Angle<Degrees, float>[]? arrayFloat;
    Angle<Degrees, double>[]? arrayDouble;

    [Params(10_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var range = Enumerable.Range(0, Count);
        arrayHalf = range
            .Select(value => new Angle<Degrees, Half>((Half)value))
            .ToArray();
        arrayFloat = range
            .Select(value => new Angle<Degrees, float>(value))
            .ToArray();
        arrayDouble = range
            .Select(value => new Angle<Degrees, double>(value))
            .ToArray();
    }

    [Benchmark]
    public Angle<Degrees, Half> Sum_Half()
        => arrayHalf!.Sum();

    [Benchmark]
    public Angle<Degrees, float> Sum_Float()
        => arrayFloat!.Sum();

    [Benchmark]
    public Angle<Degrees, double> Sum_Double()
        => arrayDouble!.Sum();
}
