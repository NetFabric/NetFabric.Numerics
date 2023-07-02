using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Numerics;

namespace NetFabric.Numerics.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class EqualsVectorFloatBenchmark
{
    NetFabric.Numerics.Vector2<float>[]? netfabricVector2;
    System.Numerics.Vector2[]? systemVector2s;

    NetFabric.Numerics.Vector3<float>[]? netfabricVector3;
    System.Numerics.Vector3[]? systemVector3s;

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

        netfabricVector3 = new NetFabric.Numerics.Vector3<float>[Count];
        systemVector3s = new System.Numerics.Vector3[Count];

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

            netfabricVector3[index] = new NetFabric.Numerics.Vector3<float>(x, y, z);
            systemVector3s[index] = new System.Numerics.Vector3(x, y, z);

            netfabricVector4[index] = new NetFabric.Numerics.Vector4<float>(x, y, z, w);
            systemVector4s[index] = new System.Numerics.Vector4(x, y, z, w);
        }
    }

    [BenchmarkCategory("Vector2")]
    [Benchmark(Baseline = true)]
    public bool SystemVector2()
        => Contains(systemVector2s!, System.Numerics.Vector2.Zero);

    [BenchmarkCategory("Vector2")]
    [Benchmark]
    public bool NetFabricVector2()
        => Contains(netfabricVector2!, NetFabric.Numerics.Vector2<float>.Zero);

    [BenchmarkCategory("Vector3")]
    [Benchmark(Baseline = true)]
    public bool SystemVector3()
        => Contains(systemVector3s!, System.Numerics.Vector3.Zero);

    [BenchmarkCategory("Vector3")]
    [Benchmark]
    public bool NetFabricVector3()
        => Contains(netfabricVector3!, NetFabric.Numerics.Vector3<float>.Zero);

    [BenchmarkCategory("Vector4")]
    [Benchmark(Baseline = true)]
    public bool SystemVector4()
        => Contains(systemVector4s!, System.Numerics.Vector4.Zero);

    [BenchmarkCategory("Vector4")]
    [Benchmark]
    public bool NetFabricVector4()
        => Contains(netfabricVector4!, NetFabric.Numerics.Vector4<float>.Zero);

    static bool Contains(System.Numerics.Vector2[] vectors, System.Numerics.Vector2 item)
    {
        foreach (var value in vectors)
            if (value.Equals(item))
                return true;
        return false;
    }

    static bool Contains<T>(Vector2<T>[] vectors, Vector2<T> item)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        foreach (var value in vectors)
            if (value.Equals(item))
                return true;
        return false;
    }

    static bool Contains(System.Numerics.Vector3[] vectors, System.Numerics.Vector3 item)
    {
        foreach (var value in vectors)
            if (value.Equals(item))
                return true;
        return false;
    }

    static bool Contains<T>(Vector3<T>[] vectors, Vector3<T> item)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        foreach (var value in vectors)
            if (value.Equals(item))
                return true;
        return false;
    }

    static bool Contains(System.Numerics.Vector4[] vectors, System.Numerics.Vector4 item)
    {
        foreach (var value in vectors)
            if (value.Equals(item))
                return true;
        return false;
    }

    static bool Contains<T>(Vector4<T>[] vectors, Vector4<T> item)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        foreach (var value in vectors)
            if (value.Equals(item))
                return true;
        return false;
    }
}