using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using NetFabric.Numerics;

namespace Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class AdditionVectorDoubleBenchmark
{
    Vector2<double>[]? vectors2;
    Vector3<double>[]? vectors3;
    Vector4<double>[]? vectors4;

    [Params(1_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var random = new Random(42);

        vectors2 = new Vector2<double>[Count];
        vectors3 = new Vector3<double>[Count];
        vectors4 = new Vector4<double>[Count];

        for (var index = 0; index < Count; index++)
        {
            var x = random.Next();
            var y = random.Next();
            var z = random.Next();
            var w = random.Next();

            vectors2[index] = new Vector2<double>(x, y);
            vectors3[index] = new Vector3<double>(x, y, z);
            vectors4[index] = new Vector4<double>(x, y, z, w);
        }
    }

    [BenchmarkCategory("Vector2")]
    [Benchmark]
    public Vector2<double> NetFabricVector2()
    {
        var sum = Vector2<double>.Zero;
        foreach (var value in vectors2!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector3")]
    [Benchmark]
    public Vector3<double> NetFabricVector3()
    {
        var sum = Vector3<double>.Zero;
        foreach (var value in vectors3!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector4")]
    [Benchmark]
    public Vector4<double> NetFabricVector4()
    {
        var sum = Vector4<double>.Zero;
        foreach (var value in vectors4!)
            sum += value;
        return sum;
    }
}