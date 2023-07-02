using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Perfolizer.Horology;
using System.Runtime.Intrinsics;

var enough = Job.Default
    .WithRuntime(CoreRuntime.Core80)
    .WithWarmupCount(1)
    .WithIterationTime(TimeInterval.FromSeconds(0.25))
    .WithMaxIterationCount(20);

var config = DefaultConfig.Instance
    .WithSummaryStyle(SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend))
    .HideColumns(Column.EnvironmentVariables, Column.RatioSD, Column.Error)
    .AddDiagnoser(MemoryDiagnoser.Default)
    .AddDiagnoser(new DisassemblyDiagnoser(new DisassemblyDiagnoserConfig
        (exportGithubMarkdown: true, printInstructionAddresses: false)))
    .AddExporter(MarkdownExporter.GitHub)
    .AddJob(enough.WithEnvironmentVariable("DOTNET_EnableHWIntrinsic", "0").WithId("Scalar").AsBaseline());

if (Vector256.IsHardwareAccelerated)
{
    config = config
        .AddJob(enough.WithId("Vector256"))
        .AddJob(enough.WithEnvironmentVariable("DOTNET_EnableAVX2", "0").WithId("Vector128"));

}
else if (Vector128.IsHardwareAccelerated)
{
    config = config.AddJob(enough.WithId("Vector128"));
}

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);