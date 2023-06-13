using BenchmarkDotNet.Attributes;

namespace NetFabric.Numerics.Benchmarks;

public class SumBenchmarks
{
    Angle<Degrees, double>[]? array;
    ReadOnlyCollection<Angle<Degrees, double>>? collection; 

    [Params(0, 1, 10, 1_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        array = Enumerable.Range(0, Count)
            .Select(value => new Angle<Degrees, double>(value))
            .ToArray();
        collection = new ReadOnlyCollection<Angle<Degrees, double>>(array);
    }

    [Benchmark(Baseline = true)]
    public Angle<Degrees, double> Array()
    {
        Angle<Degrees, double> sum = Angle<Degrees, double>.Zero;
        foreach (var angle in array!)
            sum += angle;
        return sum;
    }   

    [Benchmark]
    public Angle<Degrees, double> Linq()
        => new(Enumerable.Sum(collection!, angle => angle.Value));

    [Benchmark]
    public Angle<Degrees, double> AngleArray()
        => array!.Sum();

    [Benchmark]
    public Angle<Degrees, double> AngleEnumerable()
        => collection!.Sum();
}
