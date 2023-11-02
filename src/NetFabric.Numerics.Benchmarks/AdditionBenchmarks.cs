using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Numerics;

namespace NetFabric.Numerics.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class AdditionBenchmarks
{
    Vector2[]? vector2;

    Rectangular2D.Vector<int>[]? rectangular2_int;
    Rectangular2D.Vector<long>[]? rectangular2_long;
    Rectangular2D.Vector<float>[]? rectangular2_float;
    Rectangular2D.Vector<double>[]? rectangular2_double;
  
    Polar.Vector<Degrees, float>[]? polar_float;
    Polar.Vector<Degrees, double>[]? polar_double;

    [Params(10_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        vector2 = GetEnumerable(Count).Select<(int x, int y), Vector2>(item => new(item.x, item.y)).ToArray();

        rectangular2_int = GetEnumerable(Count).Select<(int x, int y), Rectangular2D.Vector<int>>(item => new(item.x, item.y)).ToArray();
        rectangular2_long = GetEnumerable(Count).Select<(int x, int y), Rectangular2D.Vector<long>>(item => new(item.x, item.y)).ToArray();
        rectangular2_float = GetEnumerable(Count).Select<(int x, int y), Rectangular2D.Vector<float>>(item => new(item.x, item.y)).ToArray();
        rectangular2_double = GetEnumerable(Count).Select<(int x, int y), Rectangular2D.Vector<double>>(item => new(item.x, item.y)).ToArray();

        polar_float = GetEnumerable(Count).Select<(int x, int y), Polar.Vector<Degrees, float>>(item => new(item.x, new(item.y))).ToArray();
        polar_double = GetEnumerable(Count).Select<(int x, int y), Polar.Vector<Degrees, double>>(item => new(item.x, new(item.y))).ToArray();

        static IEnumerable<(int x, int y)> GetEnumerable(int count)
        {
            var random = new Random(42);
            for (var index = 0; index < count; index++)
            {
                yield return (random.Next(count), random.Next(count));
            }
        }
    }

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public Rectangular2D.Vector<int> Rectangular2D_Int()
    {
        var sum = Rectangular2D.Vector<int>.Zero;
        foreach (var item in rectangular2_int!)
            sum += item;
        return sum;
    }

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public Rectangular2D.Vector<long> Rectangular2D_Long()
    {
        var sum = Rectangular2D.Vector<long>.Zero;
        foreach (var item in rectangular2_long!)
            sum += item;
        return sum;
    }

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public Vector2 Vector2()
    {
        var sum = System.Numerics.Vector2.Zero;
        foreach (var item in vector2!)
            sum += item;
        return sum;
    }

    [BenchmarkCategory("Float")]
    [Benchmark]
    public Rectangular2D.Vector<float> Rectangular2D_Float()
    {
        var sum = Rectangular2D.Vector<float>.Zero;
        foreach (var item in rectangular2_float!)
            sum += item;
        return sum;
    }

    [BenchmarkCategory("Float")]
    [Benchmark]
    public Polar.Vector<Degrees, float> Polar_Float()
    {
        var sum = Polar.Vector<Degrees, float>.Zero;
        foreach (var item in polar_float!)
            sum += item;
        return sum;
    }

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public Rectangular2D.Vector<double> Rectangular2D_Double()
    {
        var sum = Rectangular2D.Vector<double>.Zero;
        foreach (var item in rectangular2_double!)
            sum += item;
        return sum;
    }

    [BenchmarkCategory("Double")]
    [Benchmark]
    public Polar.Vector<Degrees, double> Polar_Double()
    {
        var sum = Polar.Vector<Degrees, double>.Zero;
        foreach (var item in polar_double!)
            sum += item;
        return sum;
    }

}