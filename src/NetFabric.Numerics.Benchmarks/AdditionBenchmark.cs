using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class AdditionBenchmarks
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
    public BaselineVector2<int> BaselineVector2Int()
    {
        var sum = BaselineVector2<int>.Zero;
        foreach (var value in baselineVector2Ints!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector2", "Int")]
    [Benchmark]
    public NetFabric.Numerics.Vector2<int> NetFabricVector2Int()
    {
        var sum = NetFabric.Numerics.Vector2<int>.Zero;
        foreach (var value in netfabricVector2Ints!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector2", "Float")]
    [Benchmark(Baseline = true)]
    public BaselineVector2<float> BaselineVector2Float()
    {
        var sum = BaselineVector2<float>.Zero;
        foreach (var value in baselineVector2Floats!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector2", "Float")]
    [Benchmark]
    public NetFabric.Numerics.Vector2<float> NetFabricVector2Float()
    {
        var sum = NetFabric.Numerics.Vector2<float>.Zero;
        foreach (var value in netfabricVector2Floats!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector2", "Float")]
    [Benchmark]
    public System.Numerics.Vector2 SystemVector2Float()
    {
        var sum = System.Numerics.Vector2.Zero;
        foreach (var value in systemVector2s!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector2", "Double")]
    [Benchmark(Baseline = true)]
    public BaselineVector2<double> BaselineVector2Double()
    {
        var sum = BaselineVector2<double>.Zero;
        foreach (var value in baselineVector2Doubles!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector2", "Double")]
    [Benchmark]
    public NetFabric.Numerics.Vector2<double> NetFabricVector2Double()
    {
        var sum = NetFabric.Numerics.Vector2<double>.Zero;
        foreach (var value in netfabricVector2Doubles!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector4", "Int")]
    [Benchmark(Baseline = true)]
    public BaselineVector4<int> BaselineVector4Int()
    {
        var sum = BaselineVector4<int>.Zero;
        foreach (var value in baselineVector4Ints!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector4", "Int")]
    [Benchmark]
    public NetFabric.Numerics.Vector4<int> NetFabricVector4Int()
    {
        var sum = NetFabric.Numerics.Vector4<int>.Zero;
        foreach (var value in netfabricVector4Ints!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector4", "Float")]
    [Benchmark(Baseline = true)]
    public BaselineVector4<float> BaselineVector4Float()
    {
        var sum = BaselineVector4<float>.Zero;
        foreach (var value in baselineVector4Floats!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector4", "Float")]
    [Benchmark]
    public NetFabric.Numerics.Vector4<float> NetFabricVector4Float()
    {
        var sum = NetFabric.Numerics.Vector4<float>.Zero;
        foreach (var value in netfabricVector4Floats!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector4", "Float")]
    [Benchmark]
    public System.Numerics.Vector4 SystemVector4Float()
    {
        var sum = System.Numerics.Vector4.Zero;
        foreach (var value in systemVector4s!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector4", "Double")]
    [Benchmark(Baseline = true)]
    public BaselineVector4<double> BaselineVector4Double()
    {
        var sum = BaselineVector4<double>.Zero;
        foreach (var value in baselineVector4Doubles!)
            sum += value;
        return sum;
    }

    [BenchmarkCategory("Vector4", "Double")]
    [Benchmark]
    public NetFabric.Numerics.Vector4<double> NetFabricVector4Double()
    {
        var sum = NetFabric.Numerics.Vector4<double>.Zero;
        foreach (var value in netfabricVector4Doubles!)
            sum += value;
        return sum;
    }

    public readonly struct BaselineVector2<T>
        where T : struct, INumber<T>
    {
        public static readonly BaselineVector2<T> Zero = new(T.Zero, T.Zero);

        public readonly T X;
        public readonly T Y;

        public BaselineVector2(T x, T y)
        {
            X = x;
            Y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaselineVector2<T> operator +(BaselineVector2<T> left, BaselineVector2<T> right)
            => new(left.X + right.X, left.Y + right.Y);
    }

    public readonly struct BaselineVector3<T>
        where T : struct, INumber<T>
    {
        public static readonly BaselineVector3<T> Zero = new(T.Zero, T.Zero, T.Zero);

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
        public static BaselineVector3<T> operator +(BaselineVector3<T> left, BaselineVector3<T> right)
            => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }

    public readonly struct BaselineVector4<T>
        where T : struct, INumber<T>
    {
        public static readonly BaselineVector4<T> Zero = new(T.Zero, T.Zero, T.Zero, T.Zero);

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
        public static BaselineVector4<T> operator +(BaselineVector4<T> left, BaselineVector4<T> right)
            => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
    }
}