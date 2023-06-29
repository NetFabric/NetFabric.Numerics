using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace NetFabric.Numerics.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class AdditionVectorFloatBenchmark
{
    NetFabric.Numerics.Vector2<float>[]? netfabricVector2;
    System.Numerics.Vector2[]? systemVector2s;

    NetFabric.Numerics.Vector4<float>[]? netfabricVector4;
    System.Numerics.Vector4[]? systemVector4s;

    [Params(1_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var random = new Random(42);

        netfabricVector2 = new NetFabric.Numerics.Vector2<float>[Count];
        systemVector2s = new System.Numerics.Vector2[Count];

        netfabricVector4 = new NetFabric.Numerics.Vector4<float>[Count];
        systemVector4s = new System.Numerics.Vector4[Count];

        for (var index = 0; index < Count; index++)
        {
            var x = random.Next();
            var y = random.Next();
            var z = random.Next();
            var w = random.Next();

            netfabricVector2[index] = new NetFabric.Numerics.Vector2<float>(x, y);
            systemVector2s[index] = new System.Numerics.Vector2(x, y);

            netfabricVector4[index] = new NetFabric.Numerics.Vector4<float>(x, y, z, w);
            systemVector4s[index] = new System.Numerics.Vector4(x, y, z, w);
        }
    }

    [BenchmarkCategory("Vector2")]
    [Benchmark(Baseline = true)]
    public System.Numerics.Vector2 SystemVector2()
    {
        var sum = System.Numerics.Vector2.Zero;
        foreach (var value in systemVector2s!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector2")]
    [Benchmark]
    public NetFabric.Numerics.Vector2<float> NetFabricVector2()
    {
        var sum = NetFabric.Numerics.Vector2<float>.Zero;
        foreach (var value in netfabricVector2!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector4")]
    [Benchmark(Baseline = true)]
    public System.Numerics.Vector4 SystemVector4()
    {
        var sum = System.Numerics.Vector4.Zero;
        foreach (var value in systemVector4s!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector4")]
    [Benchmark]
    public NetFabric.Numerics.Vector4<float> NetFabricVector4()
    {
        var sum = NetFabric.Numerics.Vector4<float>.Zero;
        foreach (var value in netfabricVector4!)
            sum += value;
        return sum;
    }
}