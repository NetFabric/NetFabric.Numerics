# Writing Benchmarks

## Project Setup

1. Create a console application project.
2. Add the `BenchmarkDotNet` NuGet package.
3. `Program.cs` (top-level statements, C# 10+):

```csharp
using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
```

`BenchmarkSwitcher` reflects over the assembly to find every class containing `[Benchmark]` methods and lists them as menu options at startup — no manual registration needed.

## Running

Must run **Release** config, **without a debugger attached** — Debug builds and attached debuggers disable JIT optimizations the benchmark is trying to measure.

```shell
dotnet run -c Release
```

The printed menu accepts a single index, a comma-separated list, or a range to run multiple benchmark classes in one pass. Each run executes multiple pipeline/warmup/actual stages automatically; to tune stage counts, iteration counts, etc., see the [jobs documentation](https://benchmarkdotnet.org/articles/configs/jobs.html).

## Attribute Reference

| Attribute | Applies to | Effect |
|---|---|---|
| `[Benchmark]` | method | Marks a method to measure. A class can have many. |
| `[Benchmark(Baseline = true)]` | method | Exactly one per class (or per category when using `[BenchmarkCategory]`) — anchors the `Ratio` column. |
| `[Params(v1, v2, ...)]` | property | Runs every benchmark once per listed value; adds a matching column to the results table. Requires the field it feeds to be set in `[GlobalSetup]`, not inline, since properties are assigned after construction. |
| `[GlobalSetup]` | method | Runs once before all iterations of a benchmark method — use to build fixtures sized from `[Params]`. |
| `[GlobalCleanup]` | method | Runs once after all iterations — dispose fixtures here. |
| `[MemoryDiagnoser]` | class | Adds `Allocated`/`Gen0`/`Gen1`/`Gen2` columns (see [interpreting-results.md](interpreting-results.md)). |

## Avoiding Dead-Code Elimination

The JIT can remove code whose result is never observed. Always compute and `return` a value from every `[Benchmark]` method:

```csharp
[Benchmark(Baseline = true)]
public int Foreach()
{
    var sum = 0;
    foreach (var item in list)
        sum += item;
    return sum;   // forces the loop to survive optimization
}
```

The extra `return` cost is negligible and identical across sibling benchmarks in the same class, so it cancels out in the `Ratio` comparison.

## Multiple Lists / Types in One Class

Benchmark methods can share a class to compare variants side by side — combine with `[Params]` for input size and `[BenchmarkCategory]` for grouping by data type (see [interpreting-results.md](interpreting-results.md)):

```csharp
public class ListBenchmarks
{
    List<int> list;

    [Params(10, 1_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup() => list = Enumerable.Range(0, Count).ToList();

    [Benchmark(Baseline = true)]
    public int Foreach()
    {
        var sum = 0;
        foreach (var item in list) sum += item;
        return sum;
    }

    [Benchmark]
    public int Foreach_AsSpan()
    {
        var sum = 0;
        foreach (var item in CollectionsMarshal.AsSpan(list)) sum += item;
        return sum;
    }
}
```
