# Interpreting Results

## Results Table Columns

| Column | Meaning |
|---|---|
| `Mean` | Average execution time — the primary number to compare. |
| `Error` | Half of a 99.9% confidence interval. |
| `StdDev` | Standard deviation of the measured runs. |
| `Ratio` | Mean of this benchmark ÷ mean of the `[Benchmark(Baseline = true)]` in the same group. `<1.00` is faster than baseline, `>1.00` is slower. |

BenchmarkDotNet automatically filters outliers and warns when the distribution isn't normal. It also prints the BenchmarkDotNet version, .NET SDK/runtime version, and host hardware (CPU, core count) above the table — always include this when sharing results, since numbers aren't comparable across machines.

For small inputs a technique can look worse and for large inputs the same technique can look better (or vice versa) — always read `Mean` per `[Params]` row rather than eyeballing `Ratio` alone across rows with very different absolute magnitudes.

## Memory Diagnoser

Add `[MemoryDiagnoser]` to a class to add allocation columns:

```csharp
[MemoryDiagnoser]
public class ListBenchmarks { /* ... */ }
```

| Column | Meaning |
|---|---|
| `Gen0`/`Gen1`/`Gen2` | GC collections per 1000 operations of that generation. `-` means none observed. |
| `Allocated` | Bytes allocated on the managed heap per operation. |

A method that casts a value-type-enumerable source (e.g. `List<T>`) to `IEnumerable<T>` before iterating typically shows non-zero `Allocated` (boxed enumerator) and a much worse `Mean`/`Ratio` than iterating the concrete type or a `Span<T>` directly.

## Grouping with Categories

```csharp
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[HideColumns(Column.Error)]
public class ListBenchmarks
{
    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public int Foreach_Int() { /* ... */ }

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int Foreach_AsSpan_Int() { /* ... */ }

    [BenchmarkCategory("Single")]
    [Benchmark(Baseline = true)]
    public float Foreach_Single() { /* ... */ }
}
```

- `[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]` groups the results table by category instead of interleaving all methods.
- `[CategoriesColumn]` adds a `Categories` column.
- One `[Benchmark(Baseline = true)]` is required **per category** when grouping this way.
- `[HideColumns(Column.Error, ...)]` trims noisy columns from the printed table.

## Artifacts

Full results are also written to disk as markdown (plus other formats via [exporters](https://benchmarkdotnet.org/articles/configs/exporters.html)):

| OS | Path |
|---|---|
| macOS/Linux | `BenchmarkDotNet.Artifacts/results/` |
| Windows | `bin/Release/<tfm>/BenchmarkDotNet.Artifacts/results/` |
