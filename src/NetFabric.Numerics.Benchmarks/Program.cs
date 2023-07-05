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

var net70 = Job.Default
    .WithRuntime(CoreRuntime.Core70)
    .WithWarmupCount(1)
    .WithIterationTime(TimeInterval.FromSeconds(0.25))
    .WithMaxIterationCount(20);

var net80 = Job.Default
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
    .AddJob(net70.WithEnvironmentVariable("DOTNET_EnableHWIntrinsic", "0").WithId(".NET 7 Scalar").AsBaseline())
    .AddJob(net80.WithEnvironmentVariable("DOTNET_EnableHWIntrinsic", "0").WithId(".NET 8 Scalar"));

if (Vector256.IsHardwareAccelerated)
{
    config = config
        .AddJob(net70.WithId(".NET 7 Vector256"))
        .AddJob(net80.WithId(".NET 8 Vector256"))
        .AddJob(net70.WithEnvironmentVariable("DOTNET_EnableAVX2", "0").WithId(".NET 7 Vector128"))
        .AddJob(net80.WithEnvironmentVariable("DOTNET_EnableAVX2", "0").WithId(".NET 8 Vector128"));

}
else if (Vector128.IsHardwareAccelerated)
{
    config = config
        .AddJob(net70.WithId(".NET 7 Vector128"))
        .AddJob(net80.WithId(".NET 8 Vector128"));
}

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);