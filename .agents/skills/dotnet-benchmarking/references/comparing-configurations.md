# Comparing Configurations

Use multiple BenchmarkDotNet **jobs** to run the same benchmark methods under different runtimes or environment variables and compare them in one table.

## Comparing .NET Runtimes/Versions

Configure jobs globally in `Program.cs`:

```csharp
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

var config = DefaultConfig.Instance
    .WithSummaryStyle(SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend))
    .HideColumns(Column.RatioSD)
    .AddJob(Job.Default.WithRuntime(CoreRuntime.Core60))
    .AddJob(Job.Default.WithRuntime(CoreRuntime.Core80));

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
```

- The host runtime must support every job's runtime — the `.csproj` `TargetFramework` must be the same as, or older than, the oldest job runtime.
- Each job re-runs every `[Benchmark]` in the assembly, adding a runtime column to the results.
- `RatioStyle.Trend` renders `Ratio` as `"1.22x faster"`/`"1.90x slower"` instead of a raw decimal — easier to read across many rows.

## Reusable Job Config Classes

For per-class configuration (rather than assembly-wide), define a `ManualConfig` subclass and apply it with `[Config(typeof(...))]`:

```csharp
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;

class MyConfig : ManualConfig
{
    public MyConfig()
    {
        _ = WithSummaryStyle(SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend));
        _ = HideColumns(Column.EnvironmentVariables, Column.RatioSD, Column.Error);
        _ = AddJob(Job.Default.WithId("Baseline").AsBaseline());
        _ = AddJob(Job.Default.WithId("Variant")
            .WithEnvironmentVariable("SOME_FEATURE_FLAG", "0"));
    }
}
```

```csharp
[Config(typeof(MyConfig))]
public class MyBenchmarks { /* ... */ }
```

`WithEnvironmentVariable(name, value)` launches that job's benchmark process with the variable set — use this to compare any environment-toggled code path (feature flags, GC modes, etc.), not just runtime versions. Guard conditional `AddJob` calls with a hardware/feature capability check when a job only makes sense on supporting hosts, so a job never reports meaningless results for an unsupported configuration.

`Column.EnvironmentVariables` is worth hiding once each job already has a descriptive `WithId(...)` — otherwise the table repeats the same env-var values on every row.

For a ready-made config comparing SIMD widths (`Scalar`/`Vector128`/`Vector256`/`Vector512` jobs via `DOTNET_Enable*` variables), see the `dotnet-simd` skill's `testing-and-benchmarking.md` reference — it's the same pattern applied to SIMD-specific environment variables.
