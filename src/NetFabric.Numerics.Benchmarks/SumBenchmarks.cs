using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace NetFabric.Numerics.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class SumBenchmarks
{
    Angle<Degrees, float>[]? array;
    List<Angle<Degrees, float>>? list;
    ReadOnlyCollection<Angle<Degrees, float>>? enumerable; 

    [Params(1_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        array = Enumerable.Range(0, Count)
            .Select(value => new Angle<Degrees, float>(value))
            .ToArray();
        list = new(array);
        enumerable = new ReadOnlyCollection<Angle<Degrees, float>>(array);
    }

    [BenchmarkCategory("Array")]
    [Benchmark(Baseline = true)]
    public Angle<Degrees, float> Array()
    {
        Angle<Degrees, float> sum = Angle<Degrees, float>.Zero;
        foreach (var angle in array!)
            sum += angle;
        return sum;
    }

    [BenchmarkCategory("Array")]
    [Benchmark]
    public Angle<Degrees, float> ArraySum()
        => array!.Sum();

    [BenchmarkCategory("List")]
    [Benchmark(Baseline = true)]
    public Angle<Degrees, float> ListSumLinq()
        => new(Enumerable.Sum(list!, angle => angle.Value));

    [BenchmarkCategory("List")]
    [Benchmark]
    public Angle<Degrees, float> ListSum()
        => list!.Sum();

    [BenchmarkCategory("Enumerable")]
    [Benchmark(Baseline = true)]
    public Angle<Degrees, float> EnumerableSumLinq()
        => new(Enumerable.Sum(enumerable!, angle => angle.Value));


    [BenchmarkCategory("Enumerable")]
    [Benchmark]
    public Angle<Degrees, float> EnumerableSum()
        => enumerable!.Sum();
}
