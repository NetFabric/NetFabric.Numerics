using BenchmarkDotNet.Attributes;

namespace NetFabric.Numerics.Benchmarks;

public class MathBenchmarks
{
    const int COUNT = 10_000;
    static readonly double PI = Math.PI;
    static readonly Angle<Radians, double> RadiansPI = new(PI);

    [Benchmark(Baseline = true)]
    public double Double()
    {
        var sum = 0.0;
        for(var counter = 0; counter < COUNT; counter++)
        {
            sum += PI;
        }
        return sum;
    }

    [Benchmark]
    public Angle<Radians, double> Angle()
    {
        Angle<Radians, double> sum = Angle<Radians, double>.Zero;
        for(var counter = 0; counter < COUNT; counter++)
        {
            sum += RadiansPI;
        }
        return sum;
    }

    [Benchmark]
    public Angle<Radians, double> Mix()
    {
        var sum = 0.0;
        for (var counter = 0; counter < COUNT; counter++)
        {
            sum += RadiansPI.Value;
        }
        return new(sum);
    }
}
