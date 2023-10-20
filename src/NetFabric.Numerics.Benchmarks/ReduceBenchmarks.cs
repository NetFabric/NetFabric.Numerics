using BenchmarkDotNet.Attributes;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics.Benchmarks;

public class ReduceBenchmarks
{
    double[]? array;

    [Params(10_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        array = GetValues(Count).ToArray();

        static IEnumerable<double> GetValues(int count)
        {
            var random = new Random(42);
            for(var i = 0; i < count; i++)
                yield return random.NextDouble();
        }
    }

    [Benchmark(Baseline = true)]
    public double Branched_LargerThan()
    {
        double sum = 0;
        foreach (var angle in array!)
            sum += ReduceBranched_LargerThan(angle, 360);
        return sum;
    }

    [Benchmark]
    public double Branched_Sign()
    {
        double sum = 0;
        foreach (var angle in array!)
            sum += ReduceBranched_Sign(angle, 360);
        return sum;
    }

    [Benchmark]
    public double Branchless()
    {
        double sum = 0;
        foreach (var angle in array!)
            sum += ReduceBranchless(angle, 360);
        return sum;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static T ReduceBranched_LargerThan<T>(T angle, T full)
        where T : struct, IFloatingPoint<T>
    {
        var reduced = angle % full;
        return angle >= T.Zero
            ? reduced
            : reduced + full;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static T ReduceBranched_Sign<T>(T angle, T full)
        where T : struct, IFloatingPoint<T>
    {
        var reduced = angle % full;
        return T.Sign(reduced) >= 0
            ? reduced
            : reduced + full;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ReduceBranchless<T>(T angle, T full)
        where T : struct, IFloatingPoint<T>
    {
        angle = (angle % full) + full;
        return angle - (T.Floor(angle / full) * full);
    }
}
