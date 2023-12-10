using BenchmarkDotNet.Attributes;

namespace NetFabric.Numerics.Tensors.Benchmarks;

public class SumBenchmarks
{
    short[]? arrayShort;
    int[]? arrayInt;
    long[]? arrayLong;
    Half[]? arrayHalf;
    float[]? arrayFloat;
    double[]? arrayDouble;

    [Params(10_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var range = Enumerable.Range(0, Count);
        arrayShort = range
            .Select(value => (short)value)
            .ToArray();
        arrayInt = range
            .ToArray();
        arrayLong = range
            .Select(value => (long)value)
            .ToArray();
        arrayHalf = range
            .Select(value => (Half)value)
            .ToArray();
        arrayFloat = range
            .Select(value => (float)value)
            .ToArray();
        arrayDouble = range
            .Select(value => (double)value)
            .ToArray();
    }

    [Benchmark]
    public short Sum_Short()
        => Tensor.Sum<short>(arrayShort!);

    [Benchmark]
    public int Sum_Int()
        => Tensor.Sum<int>(arrayInt!);

    [Benchmark]
    public long Sum_Long()
        => Tensor.Sum<long>(arrayLong!);

    [Benchmark]
    public Half Sum_Half()
        => Tensor.Sum<Half>(arrayHalf!);

    [Benchmark]
    public float Sum_Float()
        => Tensor.Sum<float>(arrayFloat!);

    [Benchmark]
    public double Sum_Double()
        => Tensor.Sum<double>(arrayDouble);
}
