---
name: dotnet-benchmarking
description: "Measure C#/.NET performance with BenchmarkDotNet instead of stopwatches. USE FOR: BenchmarkDotNet console project setup, BenchmarkSwitcher.FromAssembly; [Benchmark]/[Benchmark(Baseline = true)]; [Params]; [GlobalSetup]/[GlobalCleanup]; [MemoryDiagnoser] allocation columns; [BenchmarkCategory]/[GroupBenchmarksBy]/[CategoriesColumn]/[HideColumns]; reading Mean/Error/StdDev/Ratio/Allocated results and BenchmarkDotNet.Artifacts output; avoiding dead-code elimination via return values; comparing .NET runtimes/versions via AddJob/WithRuntime; ManualConfig job classes with WithEnvironmentVariable and RatioStyle.Trend; testing environment-variable variants via .runsettings, `dotnet test -e`/`-s`, Visual Studio run-settings. DO NOT USE FOR: SIMD API selection (dotnet-simd, has its own SIMD-width benchmarking config); LINQ/enumerable tradeoffs (dotnet-enumerable); generic math (dotnet-generic-math)."
---

# .NET Benchmarking with BenchmarkDotNet

Requires `using BenchmarkDotNet.Attributes;` / `using BenchmarkDotNet.Running;` (package `BenchmarkDotNet`, from [NuGet](https://www.nuget.org/packages/BenchmarkDotNet/)). The JIT applies deeper optimizations only after code runs "hot" for a while, so a plain `Stopwatch` around one call misrepresents steady-state performance — BenchmarkDotNet runs enough iterations across pipeline/warmup/actual stages to account for this.

## Setup

1. Create a console app, add the `BenchmarkDotNet` package.
2. Replace `Program.cs`:

```csharp
using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
```

3. Run in Release, without a debugger: `dotnet run -c Release`. Pick which benchmark(s) to run from the printed menu.

## Writing a Benchmark

```csharp
[MemoryDiagnoser]
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
        return sum;   // always return computed results — otherwise the JIT may eliminate the loop
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

One `[Benchmark(Baseline = true)]` per class (or per category) anchors the `Ratio` column. `[Params]` on a property multiplies every benchmark across each listed value, populated via `[GlobalSetup]` (inline field init would run before `Count` is assigned).

## Reference Files

| File | Load When |
|------|-----------|
| [references/writing-benchmarks.md](references/writing-benchmarks.md) | Project setup detail, benchmark attribute reference (`[Benchmark]`, `[Params]`, `[GlobalSetup]`/`[GlobalCleanup]`), avoiding JIT dead-code elimination |
| [references/interpreting-results.md](references/interpreting-results.md) | Results table columns, `[MemoryDiagnoser]` allocation columns, `[BenchmarkCategory]`/`[GroupBenchmarksBy]`/`[HideColumns]`, artifact file locations |
| [references/comparing-configurations.md](references/comparing-configurations.md) | Multi-job `ManualConfig` classes, comparing .NET runtimes/versions with `AddJob`/`WithRuntime`, custom environment-variable jobs, `RatioStyle.Trend` |
| [references/runtime-variant-testing.md](references/runtime-variant-testing.md) | Testing code under environment-variable variants outside benchmarks: `.runsettings` files, `dotnet test -e`/`-s`, Visual Studio run-settings selection |
