using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using BenchmarkDotNet.Attributes;

namespace NetFabric.Numerics.Benchmarks;

public class EqualsBenchmarks
{
    Vector4Struct<int>[]? intVectors;
    Vector4Struct<float>[]? floatVectors;
    Vector4Struct<double>[]? doubleVectors;

    [Params(1_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var random = new Random();

        intVectors = new Vector4Struct<int>[Count];
        for (var index = 0; index < Count; index++)
            intVectors[index] = new Vector4Struct<int>(random.Next(), random.Next(), random.Next(), random.Next());

        floatVectors = new Vector4Struct<float>[Count];
        for (var index = 0; index < Count; index++)
            floatVectors[index] = new Vector4Struct<float>((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());

        doubleVectors = new Vector4Struct<double>[Count];
        for (var index = 0; index < Count; index++)
            doubleVectors[index] = new Vector4Struct<double>(random.NextDouble(), random.NextDouble(), random.NextDouble(), random.NextDouble());
    }

    [Benchmark(Baseline = true)]
    public bool IntDefault()
    {
        var reference = new Vector4Struct<int>(1, 1, 1, 1);
        var equal = false;
        foreach (var comparer in intVectors!)
            equal = equal && comparer.EqualsDefault(reference);
        return equal;
    }

    [Benchmark]
    public bool IntAccelerated()
    {
        var reference = new Vector4Struct<int>(1, 1, 1, 1);
        var equal = false;
        foreach (var comparer in intVectors!)
            equal = equal && comparer.EqualsAccelerated(reference);
        return equal;
    }
}

struct Vector4Struct<T>
    where T : struct, INumber<T>
{
    public T X;
    public T Y;
    public T Z;
    public T W;

    public Vector4Struct(T x, T y, T z, T w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Determines whether the current vector is equal to another vector.
    /// </summary>
    /// <param name="other">The vector to compare with the current vector.</param>
    /// <returns><c>true</c> if the current vector is equal to the other vector; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool EqualsDefault(Vector4Struct<T> other)
        => EqualityComparer<T>.Default.Equals(X, other.X) &&
        EqualityComparer<T>.Default.Equals(Y, other.Y) &&
        EqualityComparer<T>.Default.Equals(Z, other.Z) &&
        EqualityComparer<T>.Default.Equals(W, other.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool EqualsAccelerated(Vector4Struct<T> other)
    {
        return Vector128.IsHardwareAccelerated
            ? this.AsVector128().Equals(other.AsVector128())
            : SoftwareFallback(in this, other);

        static bool SoftwareFallback(in Vector4Struct<T> self, Vector4Struct<T> other)
        {
            return EqualityComparer<T>.Default.Equals(self.X, other.X) &&
                EqualityComparer<T>.Default.Equals(self.Y, other.Y) &&
                EqualityComparer<T>.Default.Equals(self.Z, other.Z) &&
                EqualityComparer<T>.Default.Equals(self.W, other.W);
        }
    }
}

static class Extensions
{
    public static Vector128<T> AsVector128<T>(this Vector4Struct<T> value)
        where T : struct, INumber<T>
        => Unsafe.As<Vector4Struct<T>, Vector128<T>>(ref value);
}

