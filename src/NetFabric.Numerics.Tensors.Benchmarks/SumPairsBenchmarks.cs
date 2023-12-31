using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.Benchmarks;

public class SumPairsBenchmarks
{
    ValueTuple<short, short>[]? arrayShort;
    ValueTuple<int, int>[]? arrayInt;
    ValueTuple<long, long>[]? arrayLong;
    ValueTuple<Half, Half>[]? arrayHalf;
    ValueTuple<float, float>[]? arrayFloat;
    ValueTuple<double, double>[]? arrayDouble;

    [Params(10_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var range = Enumerable.Range(0, Count);
        arrayShort = range
            .Select(value => ((short)value, (short)(value + 1)))
            .ToArray();
        arrayInt = range
            .Select(value => ((int)value, (int)(value + 1)))
            .ToArray();
        arrayLong = range
            .Select(value => ((long)value, (long)(value + 1)))
            .ToArray();
        arrayHalf = range
            .Select(value => ((Half)value, (Half)(value + 1)))
            .ToArray();
        arrayFloat = range
            .Select(value => ((float)value, (float)(value + 1)))
            .ToArray();
        arrayDouble = range
            .Select(value => ((double)value, (double)(value + 1)))
            .ToArray();
    }

    [Benchmark]
    public ValueTuple<short, short> Sum_Short()
        => Tensor.SumPairs<short>(MemoryMarshal.Cast<ValueTuple<short, short>, short>(arrayShort!));

    [Benchmark]
    public ValueTuple<int, int> Sum_Int()
        => Tensor.SumPairs<int>(MemoryMarshal.Cast<ValueTuple<int, int>, int>(arrayInt!));

    [Benchmark]
    public ValueTuple<long, long> Sum_Long()
        => Tensor.SumPairs<long>(MemoryMarshal.Cast<ValueTuple<long, long>, long>(arrayLong!));

    [Benchmark]
    public ValueTuple<Half, Half> Sum_Half()
        => Tensor.SumPairs<Half>(MemoryMarshal.Cast<ValueTuple<Half, Half>, Half>(arrayHalf!));

    [Benchmark]
    public ValueTuple<float, float> Sum_Float()
        => Tensor.SumPairs<float>(MemoryMarshal.Cast<ValueTuple<float, float>, float>(arrayFloat!));

    [Benchmark]
    public ValueTuple<double, double> Sum_Double()
        => Tensor.SumPairs<double>(MemoryMarshal.Cast<ValueTuple<double, double>, double>(arrayDouble!));
}
