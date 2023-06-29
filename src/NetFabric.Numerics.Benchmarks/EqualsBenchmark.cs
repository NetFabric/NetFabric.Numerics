using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class EqualsBenchmarks
{
    BaselineVector2<int>[]? baselineVector2Ints;
    BaselineVector2<float>[]? baselineVector2Floats;
    BaselineVector2<double>[]? baselineVector2Doubles;
    NetFabric.Numerics.Vector2<int>[]? netfabricVector2Ints;
    NetFabric.Numerics.Vector2<float>[]? netfabricVector2Floats;
    NetFabric.Numerics.Vector2<double>[]? netfabricVector2Doubles;
    System.Numerics.Vector2[]? systemVector2s;

    BaselineVector4<int>[]? baselineVector4Ints;
    BaselineVector4<float>[]? baselineVector4Floats;
    BaselineVector4<double>[]? baselineVector4Doubles;
    NetFabric.Numerics.Vector4<int>[]? netfabricVector4Ints;
    NetFabric.Numerics.Vector4<float>[]? netfabricVector4Floats;
    NetFabric.Numerics.Vector4<double>[]? netfabricVector4Doubles;
    System.Numerics.Vector4[]? systemVector4s;

    [Params(1_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var random = new Random(0);

        baselineVector2Ints = new BaselineVector2<int>[Count];
        baselineVector2Floats = new BaselineVector2<float>[Count];
        baselineVector2Doubles = new BaselineVector2<double>[Count];
        netfabricVector2Ints = new NetFabric.Numerics.Vector2<int>[Count];
        netfabricVector2Floats = new NetFabric.Numerics.Vector2<float>[Count];
        netfabricVector2Doubles = new NetFabric.Numerics.Vector2<double>[Count];
        systemVector2s = new System.Numerics.Vector2[Count];

        baselineVector4Ints = new BaselineVector4<int>[Count];
        baselineVector4Floats = new BaselineVector4<float>[Count];
        baselineVector4Doubles = new BaselineVector4<double>[Count];
        netfabricVector4Ints = new NetFabric.Numerics.Vector4<int>[Count];
        netfabricVector4Floats = new NetFabric.Numerics.Vector4<float>[Count];
        netfabricVector4Doubles = new NetFabric.Numerics.Vector4<double>[Count];
        systemVector4s = new System.Numerics.Vector4[Count];

        for (var index = 0; index < Count; index++)
        {
            var x = random.Next();
            var y = random.Next();
            var z = random.Next();
            var w = random.Next();

            baselineVector2Ints[index] = new BaselineVector2<int>(x, y);
            baselineVector2Floats[index] = new BaselineVector2<float>(x, y);
            baselineVector2Doubles[index] = new BaselineVector2<double>(x, y);
            netfabricVector2Ints[index] = new NetFabric.Numerics.Vector2<int>(x, y);
            netfabricVector2Floats[index] = new NetFabric.Numerics.Vector2<float>(x, y);
            netfabricVector2Doubles[index] = new NetFabric.Numerics.Vector2<double>(x, y);
            systemVector2s[index] = new System.Numerics.Vector2(x, y);

            baselineVector4Ints[index] = new BaselineVector4<int>(x, y, z, w);
            baselineVector4Floats[index] = new BaselineVector4<float>(x, y, z, w);
            baselineVector4Doubles[index] = new BaselineVector4<double>(x, y, z, w);
            netfabricVector4Ints[index] = new NetFabric.Numerics.Vector4<int>(x, y, z, w);
            netfabricVector4Floats[index] = new NetFabric.Numerics.Vector4<float>(x, y, z, w);
            netfabricVector4Doubles[index] = new NetFabric.Numerics.Vector4<double>(x, y, z, w);
            systemVector4s[index] = new System.Numerics.Vector4(x, y, z, w);
        }
    }

    [BenchmarkCategory("Vector2", "Int")]
    [Benchmark(Baseline = true)]
    public bool BaselineVector2Int()
    {
        var reference = new BaselineVector2<int>(1, 1);
        var equal = false;
        foreach (var value in baselineVector2Ints!)
            equal = equal && value.Equals(reference);
        return equal;
    }

    [BenchmarkCategory("Vector2", "Int")]
    [Benchmark]
    public bool NetFabricVector2Int()
    {
        var reference = new NetFabric.Numerics.Vector2<int>(1, 1);
        var equal = false;
        foreach (var value in netfabricVector2Ints!)
            equal = equal && value.Equals(reference);
        return equal;
    }

    [BenchmarkCategory("Vector2", "Float")]
    [Benchmark(Baseline = true)]
    public bool BaselineVector2Float()
    {
        var reference = new BaselineVector2<float>(1.0f, 1.0f);
        var equal = false;
        foreach (var value in baselineVector2Floats!)
            equal = equal && value.Equals(reference);
        return equal;
    }

    [BenchmarkCategory("Vector2", "Float")]
    [Benchmark]
    public bool NetFabricVector2Float()
    {
        var reference = new NetFabric.Numerics.Vector2<float>(1.0f, 1.0f);
        var equal = false;
        foreach (var value in netfabricVector2Floats!)
            equal = equal && value.Equals(reference);
        return equal;
    }

    [BenchmarkCategory("Vector2", "Float")]
    [Benchmark]
    public bool SystemVector2Float()
    {
        var reference = new System.Numerics.Vector2(1.0f, 1.0f);
        var equal = false;
        foreach (var value in systemVector2s!)
            equal = equal && value.Equals(reference);
        return equal;
    }

    [BenchmarkCategory("Vector2", "Double")]
    [Benchmark(Baseline = true)]
    public bool BaselineVector2Double()
    {
        var reference = new BaselineVector2<double>(1.0, 1.0);
        var equal = false;
        foreach (var value in baselineVector2Doubles!)
            equal = equal && value.Equals(reference);
        return equal;
    }

    //[BenchmarkCategory("Vector2", "Double")]
    //[Benchmark]
    //public bool NetFabricVector2Double()
    //{
    //    var reference = new NetFabric.Numerics.Vector2<double>(1.0, 1.0);
    //    var equal = false;
    //    foreach (var value in netfabricVector2Doubles!)
    //        equal = equal && value.Equals(reference);
    //    return equal;
    //}

    //[BenchmarkCategory("Vector4", "Int")]
    //[Benchmark(Baseline = true)]
    //public bool BaselineVector4Int()
    //{
    //    var reference = new BaselineVector4<int>(1, 1, 1, 1);
    //    var equal = false;
    //    foreach (var value in baselineVector4Ints!)
    //        equal = equal && value.Equals(reference);
    //    return equal;
    //}

    //[BenchmarkCategory("Vector4", "Int")]
    //[Benchmark]
    //public bool NetFabricVector4Int()
    //{
    //    var reference = new NetFabric.Numerics.Vector4<int>(1, 1, 1, 1);
    //    var equal = false;
    //    foreach (var value in netfabricVector4Ints!)
    //        equal = equal && value.Equals(reference);
    //    return equal;
    //}

    //[BenchmarkCategory("Vector4", "Float")]
    //[Benchmark(Baseline = true)]
    //public bool BaselineVector4Float()
    //{
    //    var reference = new BaselineVector4<float>(1.0f, 1.0f, 1.0f, 1.0f);
    //    var equal = false;
    //    foreach (var value in baselineVector4Floats!)
    //        equal = equal && value.Equals(reference);
    //    return equal;
    //}

    //[BenchmarkCategory("Vector4", "Float")]
    //[Benchmark]
    //public bool NetFabricVector4Float()
    //{
    //    var reference = new NetFabric.Numerics.Vector4<float>(1.0f, 1.0f, 1.0f, 1.0f);
    //    var equal = false;
    //    foreach (var value in netfabricVector4Floats!)
    //        equal = equal && value.Equals(reference);
    //    return equal;
    //}

    //[BenchmarkCategory("Vector4", "Float")]
    //[Benchmark]
    //public bool SystemVector4Float()
    //{
    //    var reference = new System.Numerics.Vector4(1.0f, 1.0f, 1.0f, 1.0f);
    //    var equal = false;
    //    foreach (var value in systemVector4s!)
    //        equal = equal && value.Equals(reference);
    //    return equal;
    //}

    //[BenchmarkCategory("Vector4", "Double")]
    //[Benchmark(Baseline = true)]
    //public bool BaselineVector4Double()
    //{
    //    var reference = new BaselineVector4<double>(1.0, 1.0, 1.0, 1.0);
    //    var equal = false;
    //    foreach (var value in baselineVector4Doubles!)
    //        equal = equal && value.Equals(reference);
    //    return equal;
    //}

    //[BenchmarkCategory("Vector4", "Double")]
    //[Benchmark]
    //public bool NetFabricVector4Double()
    //{
    //    var reference = new NetFabric.Numerics.Vector4<double>(1.0, 1.0, 1.0, 1.0);
    //    var equal = false;
    //    foreach (var value in netfabricVector4Doubles!)
    //        equal = equal && value.Equals(reference);
    //    return equal;
    //}

    readonly struct BaselineVector2<T>
        where T : struct, INumber<T>
    {
        public readonly T X;
        public readonly T Y;

        public BaselineVector2(T x, T y)
        {
            X = x;
            Y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(BaselineVector2<T> other)
            => EqualityComparer<T>.Default.Equals(X, other.X) &&
            EqualityComparer<T>.Default.Equals(Y, other.Y);
    }

    readonly struct BaselineVector3<T>
        where T : struct, INumber<T>
    {
        public readonly T X;
        public readonly T Y;
        public readonly T Z;

        public BaselineVector3(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(BaselineVector3<T> other)
            => EqualityComparer<T>.Default.Equals(X, other.X) &&
            EqualityComparer<T>.Default.Equals(Y, other.Y) &&
            EqualityComparer<T>.Default.Equals(Z, other.Z);
    }

    readonly struct BaselineVector4<T>
        where T : struct, INumber<T>
    {
        public readonly T X;
        public readonly T Y;
        public readonly T Z;
        public readonly T W;

        public BaselineVector4(T x, T y, T z, T w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(BaselineVector4<T> other)
            => EqualityComparer<T>.Default.Equals(X, other.X) &&
            EqualityComparer<T>.Default.Equals(Y, other.Y) &&
            EqualityComparer<T>.Default.Equals(Z, other.Z) &&
            EqualityComparer<T>.Default.Equals(W, other.W);
    }
}