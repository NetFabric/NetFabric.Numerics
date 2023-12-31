using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace NetFabric.Numerics.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class ArrayAdditionBenchmarks
{
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
                yield return (random.Next(100), random.Next(100));
            }
        }
    }

    [Benchmark]
    public void Rectangular2D_Int()
        => Rectangular2D.Vector.Add<int>(rectangular2_int!, rectangular2_int!, rectangular2_int!);

    [Benchmark]
    public void Rectangular2D_Long()
        => Rectangular2D.Vector.Add<long>(rectangular2_long!, rectangular2_long!, rectangular2_long!);

    [Benchmark]
    public void Rectangular2D_Float()
        => Rectangular2D.Vector.Add<float>(rectangular2_float!, rectangular2_float!, rectangular2_float!);

    [Benchmark]
    public void Polar_Float()
        => Polar.Vector.Add<Degrees, float>(polar_float!, polar_float!, polar_float!);

    [Benchmark]
    public void Rectangular2D_Double()
        => Rectangular2D.Vector.Add<double>(rectangular2_double!, rectangular2_double!, rectangular2_double!);

    [Benchmark]
    public void Polar_Double()
        => Polar.Vector.Add<Degrees, double>(polar_double!, polar_double!, polar_double!);

}