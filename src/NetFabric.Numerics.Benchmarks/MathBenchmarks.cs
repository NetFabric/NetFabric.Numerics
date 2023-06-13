using BenchmarkDotNet.Attributes;

namespace NetFabric.Numerics.Benchmarks;

public class MathBenchmarks
{
    double[]? doubles;
    Angle<Radians, double>[]? angles;

    [Params(0, 1, 10, 1_000)]
    public int Count { get; set; }


    [GlobalSetup]
    public void GlobalSetup()
    {
        doubles= new double[Count];
        angles = new Angle<Radians, double>[Count];

        for (var index = 0; index < Count; index++)
        {
            doubles![index] = index;
            angles![index] = new Angle<Radians, double>(index);
        }
    }

    [Benchmark(Baseline = true)]
    public double Double()
    {
        var sum = 0.0;
        foreach (var value in doubles!)
            sum += value;
        return sum;
    }

    [Benchmark]
    public Angle<Radians, double> Angle()
    {
        Angle<Radians, double> sum = Angle<Radians, double>.Zero;
        foreach (var angle in angles!)
            sum += angle;
        return sum;
    }

    [Benchmark]
    public Angle<Radians, double> Mix()
    {
        var sum = 0.0;
        foreach (var angle in angles!)
            sum += angle.Value;
        return new(sum);
    }
}
