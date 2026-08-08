# Testing & Benchmarking SIMD

SIMD behavior varies by hardware. Cover Scalar / Vector128 / Vector256 / Vector512 in both unit tests and benchmarks — even if you don't call SIMD APIs directly, a dependency might.

## Environment Variables

| Variable | Default | Effect |
|----------|:---:|--------|
| `DOTNET_EnableHWIntrinsic` | `1` | Master switch; `0` disables all SIMD/intrinsics, forcing the full software path |
| `DOTNET_EnableAVX2` | `1` | `0` caps SIMD at 128-bit (also gates BMI1/BMI2/F16C/FMA/LZCNT/MOVBE) |
| `DOTNET_EnableAVX512` | `1` | `0` caps SIMD at 256-bit (AVX-512 F+BW+CD+DQ+VL) |
| `DOTNET_MaxVectorTBitWidth` | system default | Caps `Vector<T>`'s max width in bits |
| `DOTNET_PreferredVectorBitWidth` | system default | Caps the widest fixed-width vector (`Vector128`/`256`/`512`) that reports `IsHardwareAccelerated` |

These only ever *narrow* what's used — you can't force a wider width than the hardware supports; setting a knob the CPU doesn't support is ignored. Knob names changed across .NET versions (e.g. the AVX-512 knob was `EnableAVX512F` pre-.NET 9) — confirm against the runtime version you target.

| Scenario | `EnableHWIntrinsic` | `EnableAVX2` | `EnableAVX512` |
|----------|:---:|:---:|:---:|
| No SIMD | `0` | – | – |
| Up to 128-bit | `1` | `0` | `0` |
| Up to 256-bit | `1` | `1` | `0` |
| Up to 512-bit | `1` | `1` | `1` |

**`Vector<T>` does not automatically grow to the widest width the hardware supports.** Its default max width (`MaxVectorTBitWidth`) can be narrower than the hardware's — e.g. `Vector512<T>.IsHardwareAccelerated` can be `true` while `Vector<T>` stays 256-bit unless you set `DOTNET_MaxVectorTBitWidth=512`. `Vector128<T>`/`Vector256<T>`/`Vector512<T>` availability depends independently on the knobs above.

These are diagnostic/testing knobs only — not a production tuning mechanism, and they don't affect ahead-of-time compiled code (ReadyToRun/Native AOT) or the runtime's own internal routines.

## Unit Testing via `.runsettings`

Create one file per scenario (e.g. `_Scalar.runsettings`, `_Vector128.runsettings`, `_Vector256.runsettings`, `_Vector512.runsettings`):

```xml
<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
    <RunConfiguration>
        <EnvironmentVariables>
            <DOTNET_EnableHWIntrinsic>1</DOTNET_EnableHWIntrinsic>
            <DOTNET_EnableAVX2>0</DOTNET_EnableAVX2>
            <DOTNET_EnableAVX512>0</DOTNET_EnableAVX512>
        </EnvironmentVariables>
    </RunConfiguration>
</RunSettings>
```

- **Visual Studio**: `Test > Configure Run Settings > Select Solution Wide runsettings File`.
- **CLI**: `dotnet test -s:_Vector128.runsettings` (or inline: `dotnet test -e:DOTNET_EnableAVX2=0 -e:DOTNET_EnableAVX512=0`).

## Benchmarking with BenchmarkDotNet

Define a reusable job config so a single benchmark class runs under every SIMD scenario:

```csharp
class VectorizationConfig : ManualConfig
{
    public VectorizationConfig()
    {
        _ = WithSummaryStyle(SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend));
        _ = HideColumns(Column.EnvironmentVariables, Column.RatioSD, Column.Error);
        _ = AddJob(Job.Default.WithId("Scalar")
            .WithEnvironmentVariable("DOTNET_EnableHWIntrinsic", "0")
            .AsBaseline());
        if (Vector128.IsHardwareAccelerated)
            _ = AddJob(Job.Default.WithId("Vector128")
                .WithEnvironmentVariable("DOTNET_EnableAVX2", "0")
                .WithEnvironmentVariable("DOTNET_EnableAVX512", "0"));
        if (Vector256.IsHardwareAccelerated)
            _ = AddJob(Job.Default.WithId("Vector256")
                .WithEnvironmentVariable("DOTNET_EnableAVX512", "0"));
        if (Vector512.IsHardwareAccelerated)
            _ = AddJob(Job.Default.WithId("Vector512"));
    }
}
```

Apply with `[Config(typeof(VectorizationConfig))]` only on the benchmark classes that need multi-width comparison. Use `[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]` + `[CategoriesColumn]` + `[BenchmarkCategory("Int")]`/`[BenchmarkCategory("Float")]` to group results by type alongside SIMD width.

Guard each conditional `AddJob` with the corresponding `VectorXXX.IsHardwareAccelerated` check — running a job for a width the host CPU doesn't support produces meaningless results.
