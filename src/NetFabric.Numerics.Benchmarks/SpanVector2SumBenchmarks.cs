using BenchmarkDotNet.Attributes;

namespace NetFabric.Numerics.Rectangular2D.Benchmarks;

public class SpanVector2SumBenchmarks
{
    Vector<short>[]? arrayShort;
    Vector<int>[]? arrayInt;
    Vector<long>[]? arrayLong;
    Vector<Half>[]? arrayHalf;
    Vector<float>[]? arrayFloat;
    Vector<double>[]? arrayDouble;

    [Params(10_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var range = Enumerable.Range(0, Count);
        arrayShort = range
            .Select(value => new Vector<short>((short)value, (short)(value + 1)))
            .ToArray();
        arrayInt = range
            .Select(value => new Vector<int>(value, value + 1))
            .ToArray();
        arrayLong = range
            .Select(value => new Vector<long>(value, value + 1))
            .ToArray();
        arrayHalf = range
            .Select(value => new Vector<Half>((Half)value, (Half)(value + 1)))
            .ToArray();
        arrayFloat = range
            .Select(value => new Vector<float>(value, value + 1))
            .ToArray();
        arrayDouble = range
            .Select(value => new Vector<double>(value, value + 1))
            .ToArray();
    }

    [Benchmark]
    public Vector<short> Sum_Short()
        => arrayShort!.Sum();

    [Benchmark]
    public Vector<int> Sum_Int()
        => arrayInt!.Sum();

    [Benchmark]
    public Vector<long> Sum_Long()
        => arrayLong!.Sum();

    [Benchmark]
    public Vector<Half> Sum_Half()
        => arrayHalf!.Sum();

    [Benchmark]
    public Vector<float> Sum_Float()
        => arrayFloat!.Sum();

    [Benchmark]
    public Vector<double> Sum_Double()
        => arrayDouble!.Sum();
}
