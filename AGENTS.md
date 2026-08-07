# AGENTS.md

## Project overview

NetFabric.Numerics is a multi-package .NET 8 library providing strongly-typed geometric primitives — angles, points, vectors, and geodetic coordinates — built on C# 11+ generic math (`System.Numerics` static abstract interface members). Each type is parameterized by both a unit type and a numeric type, enabling zero-allocation, type-safe math across `float`, `double`, `decimal`, and other numeric primitives. See [README.md](README.md) and the [docs/](docs/) site for full feature descriptions.

## Solution structure

| Project | Purpose |
|---------|---------|
| `src/NetFabric.Numerics.Angle/` | Strongly-typed angles (`Angle<TUnits, T>`) with degrees, radians, gradians, revolutions |
| `src/NetFabric.Numerics/` | Rectangular 2D/3D, polar, and spherical point/vector primitives |
| `src/NetFabric.Numerics.Geodesy/` | Geodetic coordinates with datum and ellipsoid support |
| `src/NetFabric.Numerics.Angle.UnitTests/` | xUnit tests for the angle package |
| `src/NetFabric.Numerics.UnitTests/` | xUnit tests for the core package |
| `src/NetFabric.Numerics.Geodesy.UnitTests/` | xUnit tests for the geodesy package |
| `src/NetFabric.Numerics.Benchmarks/` | BenchmarkDotNet micro-benchmarks — run separately in Release only |
| `docs/` | DocFX documentation source |
| `.agents/skills/` | Domain-specific APM skills for AI agents; do not edit |

## Build & test commands

```sh
dotnet restore
dotnet build --no-restore -c Release
dotnet test --no-build --verbosity normal -c Release
```

Run benchmarks separately (never via `dotnet test`):

```sh
dotnet run -c Release --project src/NetFabric.Numerics.Benchmarks
```

## Code style

- **Language**: C# 12 / .NET 8; target framework `net8.0` for all projects
- **Generic math**: implement and constrain to `System.Numerics` interfaces (`INumber<T>`, `IFloatingPoint<T>`, etc.) — prefer static abstract interface members over overloads
- **Nullable**: enabled; `WarningsAsErrors` for nullable diagnostics — resolve, never suppress
- **Usings**: implicit usings on; add shared project-wide types as `<Using>` entries in the `.csproj`
- **Analyzers**: `ErrorProne.NET.Structs` (mutable struct safety) and `NetFabric.Hyperlinq.Analyzer` (LINQ allocations) — fix, never suppress
- **XML docs**: required on all `public` and `protected` API members (`GenerateDocumentationFile` is enabled)
- **Unsafe blocks**: allowed in library projects only for performance-critical SIMD paths; keep unsafe surface minimal

## Testing

- **Framework**: xUnit + FluentAssertions
- **Coverage**: add a test for every new or changed public API; prefer `[Theory]` + `[InlineData]` / `[MemberData]` over multiple `[Fact]`s
- All tests must pass: `dotnet test --no-build --verbosity normal -c Release`

## Security

- Pure-math computation library — no credentials, network calls, or connection strings belong here
- Never commit secrets, API keys, or tokens in any file
- Do not hand-edit generated artifacts: `obj/`, `bin/`, `*.nupkg`, `*.snupkg`
- `apm.lock.yaml` is a generated lockfile — do not edit manually
