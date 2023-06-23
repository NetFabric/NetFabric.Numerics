using System.Numerics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace NetFabric.Numerics.Benchmarks;

public class IndexerBenchmarks
{
    SwitchIndexerStruct<double>[]? switchIndexerStructs;
    OffsetIndexerStruct<double>[]? offsetIndexerStructs;

    [Params(1_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var random = new Random();

        switchIndexerStructs = new SwitchIndexerStruct<double>[Count];
        for (var index = 0; index < Count; index++)
            switchIndexerStructs[index] = new SwitchIndexerStruct<double>(random.NextDouble(), random.NextDouble(), random.NextDouble(), random.NextDouble());

        offsetIndexerStructs = new OffsetIndexerStruct<double>[Count];
        for (var index = 0; index < Count; index++)
            offsetIndexerStructs[index] = new OffsetIndexerStruct<double>(random.NextDouble(), random.NextDouble(), random.NextDouble(), random.NextDouble());
    }

    [Benchmark(Baseline = true)]
    public double SwitchIndexer()
    {
        var sum = 0.0;
        foreach (var indexer in switchIndexerStructs!)
            for (var index = 0; index < 4; index++)
                sum += indexer[index];
        return sum;
    }

    [Benchmark]
    public double OffsetIndexer()
    {
        var sum = 0.0;
        foreach (var indexer in offsetIndexerStructs!)
            for (var index = 0; index < 4; index++)
                sum += indexer[index];
        return sum;
    }

    struct SwitchIndexerStruct<T>
        where T : struct, INumber<T>
    {
        public T X;
        public T Y;
        public T Z;
        public T W;

        public SwitchIndexerStruct(T x, T y, T z, T w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public T this[int index]
            => index switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                3 => W,
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };
    }

    struct OffsetIndexerStruct<T>
        where T : struct, INumber<T>
    {
        public T X;
        public T Y;
        public T Z;
        public T W;

        public OffsetIndexerStruct(T x, T y, T z, T w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public T this[int index] 
            => (uint)index >= 4 
                ? throw new ArgumentOutOfRangeException(nameof(index)) 
                : Unsafe.Add(ref Unsafe.AsRef(in X), index);
    }

}
