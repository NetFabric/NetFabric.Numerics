---
name: csharp-file-based-app
description: "Build, run, and publish .NET applications from a single C# file without a .csproj, using .NET 10 SDK file-based apps. USE FOR: dotnet run file.cs; dotnet run --file; shebang/shell-executable C# scripts; colon-prefixed directives (sdk, package, project, property, include); converting a file-based app to a project with dotnet project convert; publishing file-based apps with native AOT; packaging a file-based app as a .NET tool (PackAsTool); user secrets for file-based apps; flat *.run.json launch profiles; dotnet build/clean/publish/pack/restore on a .cs file; build caching behavior and dotnet clean file-based-apps; folder layout to avoid project-file cone conflicts. DO NOT USE FOR: traditional .csproj solution scaffolding (use dotnet-solution-setup); C# language idioms unrelated to the SDK/CLI (use csharp-best-practices); benchmarking (use dotnet-benchmarking)."
---

# C# File-Based Apps

Requires .NET 10 SDK+. Run a single `.cs` file directly — the SDK generates a virtual project from `#:` directives, no `.csproj` needed.

## Run, Build, Publish

| Command | Effect |
|---------|--------|
| `dotnet run file.cs` / `dotnet run --file file.cs` / `dotnet file.cs` | Run the file-based app |
| `dotnet run file.cs -- arg1 arg2` | Pass args to the app |
| `echo 'Console.WriteLine(1);' \| dotnet run -` | Run C# piped from stdin |
| `dotnet build file.cs` | Build; output under `<temp>/dotnet/runfile/<name>-<hash>/bin/<config>/` |
| `dotnet publish file.cs` | Publish; **native AOT by default** → `artifacts/<name>/` next to the file |
| `dotnet pack file.cs` | Pack as a .NET tool; **`PackAsTool=true` by default** |
| `dotnet project convert file.cs` | Materialize a `.csproj` + copy of the file in a new folder; original untouched |
| `dotnet restore file.cs` | Restore packages (implicit on build/run; skip with `--no-restore`) |
| `dotnet clean file.cs` | Remove build artifacts for this file |
| `dotnet clean file-based-apps [--days N]` | Purge stale cached build folders (default 30 days) |

Disable AOT: `#:property PublishAot=false`. Disable tool packaging: `#:property PackAsTool=false`.

## `#:` Directives

Place at the top of the `.cs` file, one per line.

| Directive | Purpose | Example |
|-----------|---------|---------|
| `#:sdk` | Choose SDK (default `Microsoft.NET.Sdk`) | `#:sdk Microsoft.NET.Sdk.Web` |
| `#:package` | Add a NuGet package | `#:package Serilog@3.1.1` |
| `#:project` | Reference another project | `#:project ../Shared/Shared.csproj` |
| `#:property` | Set an MSBuild property | `#:property TargetFramework=net10.0` |
| `#:include` | Add extra source/resource files (.NET 11 Preview 3+ / SDK 10.0.300+) | `#:include helpers.cs` |

Full syntax, glob patterns, conditional property expressions, item-type mapping → [references/directives-reference.md](references/directives-reference.md).

## Key Behaviors

- **Shell execution**: shebang `#!/usr/bin/env -S dotnet --` + `chmod +x` runs the file directly on Unix (`LF` endings, no BOM).
- **User secrets**: stable ID hashed from file path; `dotnet user-secrets set "K" "V" --file file.cs`.
- **Launch profiles**: flat `<name>.run.json` next to the `.cs` file; traditional `Properties/launchSettings.json` wins if both exist.
- **Build caching**: keyed on file content, directives, SDK version, implicit build files; concurrent runs of the same app can race on output — `dotnet build` first, then run with `--no-build`.
- **Folder layout**: never nest a file-based app inside a `.csproj`'s directory cone, and isolate it from ambient `Directory.Build.props`/`Directory.Packages.props` when different settings are needed.

Shebang details, secrets/launch-profile examples, caching pitfalls, folder-layout dos/don'ts → [references/runtime-behavior.md](references/runtime-behavior.md).

## Reference Files

| File | Load When |
|------|-----------|
| [references/directives-reference.md](references/directives-reference.md) | Writing/debugging `#:` directives, glob `#:include` patterns, conditional `#:property` expressions |
| [references/runtime-behavior.md](references/runtime-behavior.md) | Shell execution, user secrets, launch profiles, build caching, folder layout guidance |
