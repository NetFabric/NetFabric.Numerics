using BenchmarkDotNet.Attributes;

namespace NetFabric.Numerics.Tensors.Benchmarks;

public class AddBenchmarks
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
    public void Add_Short()
        => Tensor.Add<short>(arrayShort!, arrayShort!, arrayShort!);

    [Benchmark]
    public void Add_Int()
        => Tensor.Add<int>(arrayInt!, arrayInt!, arrayInt!);

    [Benchmark]
    public void Add_Long()
        => Tensor.Add<long>(arrayLong!, arrayLong!, arrayLong!);

    [Benchmark]
    public void Add_Half()
        => Tensor.Add<Half>(arrayHalf!, arrayHalf!, arrayHalf!);

    [Benchmark]
    public void Add_Float()
        => Tensor.Add<float>(arrayFloat!, arrayFloat!, arrayFloat!);

    [Benchmark]
    public void Add_Double()
        => Tensor.Add<double>(arrayDouble!, arrayDouble!, arrayDouble!);
}
