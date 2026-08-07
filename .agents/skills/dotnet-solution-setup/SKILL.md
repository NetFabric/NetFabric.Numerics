---
name: dotnet-solution-setup
description: "Repository and solution-level scaffolding best practices for .NET: .slnx solution format, Directory.Build.props shared MSBuild properties, Directory.Packages.props central package management (CPM), .editorconfig, SourceLink, deterministic/reproducible builds, global.json SDK pinning. USE FOR: scaffolding a new .NET repo or solution; choosing .sln vs .slnx; sharing MSBuild properties (TargetFramework, LangVersion, Nullable, ImplicitUsings, TreatWarningsAsErrors, EnforceCodeStyleInBuild, EnableNETAnalyzers, Deterministic) across every project; configuring SourceLink for debuggable NuGet packages; central package version management and GlobalPackageReference; pinning the SDK with global.json; .editorconfig code-style rules; CI-only ContinuousIntegrationBuild flag; reproducible/deterministic builds and packages; repo root file checklist. DO NOT USE FOR: C# language feature choices (use csharp-best-practices); benchmarking (use dotnet-benchmarking); generic math (use dotnet-generic-math)."
---

# .NET Solution & Project Setup

Targets .NET 9 SDK (9.0.200+) and later, where noted.

## Solution File

- Use **`.slnx`** (XML solution format), not `.sln`. Default for `dotnet new sln` since .NET 10 SDK; available since 9.0.200.
- Migrate an existing solution: `dotnet sln migrate` (creates `.slnx`, keeps `.sln` untouched).
- Create new: `dotnet new sln` (or `--format sln` to opt back out).

## Repository Root Checklist

| File | Purpose |
|------|---------|
| `<Name>.slnx` | solution definition |
| `.editorconfig` | code style + analyzer severities, enforced at build via `EnforceCodeStyleInBuild` |
| `Directory.Build.props` | MSBuild properties shared by every project |
| `Directory.Packages.props` | central package management (CPM) — package versions in one place |
| `global.json` | pin SDK version (`rollForward: latestMinor`) |
| `.gitattributes` | enforce line endings (`* text=auto eol=lf`) for deterministic diffs |
| `NuGet.config` | package sources; add source mapping to silence `NU1507` when using CPM with >1 source |
| `.config/dotnet-tools.json` | local tool manifest (`dotnet tool restore`) |

## Directory.Build.props — Required Properties

| Property | Value | Why |
|----------|-------|-----|
| `TargetFramework` | e.g. `net10.0` | single source of truth for the whole repo |
| `LangVersion` | `latest` | newest C# features as soon as the SDK supports them |
| `Nullable` | `enable` | nullable reference type analysis |
| `ImplicitUsings` | `enable` | drop boilerplate `using` directives |
| `TreatWarningsAsErrors` | `true` | fail the build instead of accumulating warnings |
| `EnforceCodeStyleInBuild` | `true` | `.editorconfig` IDE0xxx style rules run on `dotnet build`, not just in the IDE |
| `Deterministic` | `true` | byte-identical assemblies for identical inputs (default `true` already, set explicitly) |
| `EnableNETAnalyzers` | `true` | run the built-in `CAxxxx` code-quality analyzers |

Full example + extra hardening properties (`AnalysisLevel`, `AnalysisMode`, test-project overrides, versioning) → [references/directory-build-props.md](references/directory-build-props.md).

## SourceLink

Every packable/publishable project needs SourceLink so consumers can step into source while debugging:

```xml
<PropertyGroup>
  <PublishRepositoryUrl>true</PublishRepositoryUrl>
  <EmbedUntrackedSources>true</EmbedUntrackedSources>
  <DebugType>portable</DebugType>
  <ContinuousIntegrationBuild Condition="'$(CI)' == 'true'">true</ContinuousIntegrationBuild>
</PropertyGroup>
<ItemGroup>
  <PackageReference Include="Microsoft.SourceLink.GitHub" Version="8.0.0" PrivateAssets="All" />
</ItemGroup>
```

`ContinuousIntegrationBuild` must only be `true` on CI (it maps local paths to repo-relative paths); leaving it `true` locally breaks the "Just My Code" experience. Details, symbol packages, alternate providers → [references/directory-build-props.md](references/directory-build-props.md).

## Central Package Management

`Directory.Packages.props` sets `ManagePackageVersionsCentrally=true` and declares `<PackageVersion>` items; every project's `<PackageReference>` then omits `Version`. Full setup, `GlobalPackageReference`, transitive pinning, override rules → [references/central-package-management.md](references/central-package-management.md).

## Reference Files

| File | Load When |
|------|-----------|
| [references/directory-build-props.md](references/directory-build-props.md) | Writing the full `Directory.Build.props`, SourceLink details, analyzer levels, test-project overrides, git-based versioning |
| [references/central-package-management.md](references/central-package-management.md) | Writing `Directory.Packages.props`, `GlobalPackageReference`, transitive pinning, package source mapping |
| [references/repo-scaffold.md](references/repo-scaffold.md) | `.slnx` migration, `.editorconfig`, `global.json`, `.gitattributes`, local tool manifest, CI formatting checks |
