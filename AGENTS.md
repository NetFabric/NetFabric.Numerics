# AGENTS.md

## Project overview

NetFabric.Numerics is a multi-package .NET 8 library providing strongly typed angles, points, vectors, quaternions, and geodetic coordinates. It uses C# generic math (`System.Numerics` static abstract interface members) for type-safe, allocation-conscious algorithms across numeric types. See [README.md](README.md) and [docs/](docs/) for user-facing documentation.

## Solution structure

| Project | Purpose |
| --- | --- |
| `src/NetFabric.Numerics.Angle/` | Strongly-typed angles (`Angle<TUnits, T>`) with degrees, radians, gradians, revolutions |
| `src/NetFabric.Numerics/` | Rectangular 2D/3D, polar, and spherical point/vector primitives |
| `src/NetFabric.Numerics.Geodesy/` | Geodetic coordinates with datum and ellipsoid support |
| `src/NetFabric.Numerics.Angle.UnitTests/` | xUnit tests for the angle package |
| `src/NetFabric.Numerics.UnitTests/` | xUnit tests for the core package |
| `src/NetFabric.Numerics.Geodesy.UnitTests/` | xUnit tests for the geodesy package |
| `src/NetFabric.Numerics.Benchmarks/` | BenchmarkDotNet microbenchmarks; run separately in Release only |
| `docs/` | DocFX documentation source |
| `.agents/skills/` | Domain skills for coding agents; edit only when the task explicitly targets agent skills |

Nested `AGENTS.md` files contain package-specific guidance and override this file for their subtrees.

## Setup and quality commands

Run commands from the repository root in this order:

```bash
dotnet restore NetFabric.Numerics.slnx
dotnet format NetFabric.Numerics.slnx --verify-no-changes
dotnet build NetFabric.Numerics.slnx --no-restore -c Release
dotnet test NetFabric.Numerics.slnx --no-build --verbosity normal -c Release
```

Run benchmarks separately (never via `dotnet test`):

```bash
dotnet run -c Release --project src/NetFabric.Numerics.Benchmarks
```

## Code style

- Follow the root [.editorconfig](.editorconfig); apply fixes with `dotnet format NetFabric.Numerics.slnx`.
- Use the latest C# version supported by the pinned .NET 10 SDK and target `net8.0`; nullable reference types and implicit usings are enabled.
- All compiler and analyzer warnings are errors. Resolve diagnostics rather than suppressing them.
- Constrain generic numeric algorithms with `System.Numerics` interfaces such as `INumber<T>` and `IFloatingPoint<T>`.
- Prefer static abstract interface members and the numeric type's static methods over type-specific overloads or `Math`/`MathF`.
- Add shared project-wide namespaces as `<Using>` items in the owning project file.
- Add XML documentation to new or changed public and protected APIs; library projects generate documentation files.
- Keep unsafe code limited to measured, performance-critical SIMD paths.
- Respect project analyzers, including `ErrorProne.NET.Structs` and `NetFabric.Hyperlinq.Analyzer` where referenced.

## Testing

- Tests use xUnit and FluentAssertions.
- Add or update tests for every changed behavior and public API.
- Prefer `[Theory]` with `[InlineData]` or `[MemberData]` for related cases.
- Run a focused project or test while iterating, for example:

```bash
dotnet test src/NetFabric.Numerics.UnitTests/NetFabric.Numerics.UnitTests.csproj -c Release --filter "FullyQualifiedName~VectorTests"
```

- Finish with the full format, build, and test sequence above.

## Security

- This is a pure-computation library. Do not add credentials, network calls, or connection strings.
- Never commit secrets, API keys, or tokens.
- Do not hand-edit generated artifacts under `obj/` or `bin/`, or package outputs (`*.nupkg`, `*.snupkg`).
- Do not hand-edit `apm.lock.yaml`; regenerate it through APM tooling.
- Do not run destructive Git commands, publish packages, or deploy documentation without explicit user approval.

## Maintenance

- Update this file in the same change as build, test, formatter, or repository-layout changes.
- Keep root guidance shared; put package-specific exceptions in the nearest nested `AGENTS.md`.
