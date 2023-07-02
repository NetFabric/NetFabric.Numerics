using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace NetFabric.Numerics.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class AdditionVectorFloatBenchmark
{
    NetFabric.Numerics.Vector2<float>[]? netfabricVectors2;
    System.Numerics.Vector2[]? systemVectors2;

    NetFabric.Numerics.Vector3<float>[]? netfabricVectors3;
    System.Numerics.Vector3[]? systemVectors3;

    NetFabric.Numerics.Vector4<float>[]? netfabricVectors4;
    System.Numerics.Vector4[]? systemVectors4;

    [Params(1_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var random = new Random(42);

        netfabricVectors2 = new NetFabric.Numerics.Vector2<float>[Count];
        systemVectors2 = new System.Numerics.Vector2[Count];

        netfabricVectors3 = new NetFabric.Numerics.Vector3<float>[Count];
        systemVectors3 = new System.Numerics.Vector3[Count];

        netfabricVectors4 = new NetFabric.Numerics.Vector4<float>[Count];
        systemVectors4 = new System.Numerics.Vector4[Count];

        for (var index = 0; index < Count; index++)
        {
            var x = random.Next();
            var y = random.Next();
            var z = random.Next();
            var w = random.Next();

            netfabricVectors2[index] = new NetFabric.Numerics.Vector2<float>(x, y);
            systemVectors2[index] = new System.Numerics.Vector2(x, y);

            netfabricVectors3[index] = new NetFabric.Numerics.Vector3<float>(x, y, z);
            systemVectors3[index] = new System.Numerics.Vector3(x, y, z);

            netfabricVectors4[index] = new NetFabric.Numerics.Vector4<float>(x, y, z, w);
            systemVectors4[index] = new System.Numerics.Vector4(x, y, z, w);
        }
    }

    [BenchmarkCategory("Vector2")]
    [Benchmark(Baseline = true)]
    public System.Numerics.Vector2 SystemVector2()
    {
        var sum = System.Numerics.Vector2.Zero;
        foreach (var value in systemVectors2!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector2")]
    [Benchmark]
    public NetFabric.Numerics.Vector2<float> NetFabricVector2()
    {
        var sum = NetFabric.Numerics.Vector2<float>.Zero;
        foreach (var value in netfabricVectors2!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector3")]
    [Benchmark(Baseline = true)]
    public System.Numerics.Vector3 SystemVector3()
    {
        var sum = System.Numerics.Vector3.Zero;
        foreach (var value in systemVectors3!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector3")]
    [Benchmark]
    public NetFabric.Numerics.Vector3<float> NetFabricVector3()
    {
        var sum = NetFabric.Numerics.Vector3<float>.Zero;
        foreach (var value in netfabricVectors3!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector4")]
    [Benchmark(Baseline = true)]
    public System.Numerics.Vector4 SystemVector4()
    {
        var sum = System.Numerics.Vector4.Zero;
        foreach (var value in systemVectors4!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector4")]
    [Benchmark]
    public NetFabric.Numerics.Vector4<float> NetFabricVector4()
    {
        var sum = NetFabric.Numerics.Vector4<float>.Zero;
        foreach (var value in netfabricVectors4!)
            sum += value;
        return sum;
    }
}