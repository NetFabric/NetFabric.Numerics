using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using System.Runtime.Intrinsics;

var config = DefaultConfig.Instance
    .WithSummaryStyle(SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend))
    .HideColumns(Column.EnvironmentVariables, Column.RatioSD, Column.Error)
    // .AddDiagnoser(new DisassemblyDiagnoser(new DisassemblyDiagnoserConfig
    //     (exportGithubMarkdown: true, printInstructionAddresses: false)))
    .AddJob(Job.Default.WithId("Scalar")
        .WithEnvironmentVariable("DOTNET_EnableHWIntrinsic", "0")
        .AsBaseline());

if (Vector128.IsHardwareAccelerated)
{
    config = config
        .AddJob(Job.Default.WithId("Vector128")
            .WithEnvironmentVariable("DOTNET_EnableAVX2", "0")
            .WithEnvironmentVariable("DOTNET_EnableAVX512F", "0"));
}
if (Vector256.IsHardwareAccelerated)
{
    config = config
        .AddJob(Job.Default.WithId("Vector256")
            .WithEnvironmentVariable("DOTNET_EnableAVX512F", "0"));
}
if (Vector512.IsHardwareAccelerated)
{
    config = config
        .AddJob(Job.Default.WithId("Vector512"));
}

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);