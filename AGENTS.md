# AGENTS.md

## Project overview

NetFabric.Numerics is a multi-package .NET 8 extension of generic math from scalar numeric types to strongly typed coordinate systems. It models immutable angles, points, vectors, quaternions, and geodetic entities as generic mathematical values; implements their operations; and provides explicit conversions between rectangular, polar, spherical, and angle-unit representations. C# generic math (`System.Numerics` static abstract interface members) supplies numeric behavior, while coordinate-system, angle-unit, and datum type parameters preserve geometric meaning at compile time. See [README.md](README.md) and [docs/](docs/) for user-facing documentation.

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

## Architectural Rule

Treat this library as an extension of .NET generic math, not as a collection of numeric wrappers:

- Every interface, concept, and implementation must preserve the semantics, invariants, and valid operations of its mathematical counterpart. Do not introduce members, operators, conversions, or relationships that are mathematically invalid or misleading.
- Design public APIs to be easy and intuitive to use correctly, and make common mistakes difficult or impossible to express through types, constraints, constructors, and explicit conversions. Prefer compile-time enforcement of units, coordinate systems, datum, and point/vector semantics over documentation-only safeguards or runtime failure.
- Generic numeric interfaces define the valid algebra for a value type; implement the narrowest `System.Numerics` contract that accurately represents each operation.
- Follow the established `System.Numerics` value-type patterns for every supported entity: matching static identities, operators, conversions, comparison/equality contracts, parsing/formatting, and helper APIs when they are mathematically meaningful. Extend those patterns to coordinate-aware entities; do not invent incompatible alternatives.
- For floating-point `T`, public APIs must define and test their behavior for `NaN`, infinities, signed zero, overflow, underflow, and degenerate values such as zero-length vectors; never silently assume finite input.
- Coordinate-system, angle-unit, and datum type parameters are part of the mathematical type. Preserve them through APIs and conversions unless an explicit conversion changes the represented system.
- Every angle is represented by `Angle<TUnits, T>` or `AngleReduced<TUnits, T>`. Never represent degrees, radians, or any other angular quantity as a bare numeric component, parameter, return value, or public constant; the angle unit must remain in the type.
- Do not collapse geometry to untyped scalar values or introduce implicit coordinate-system conversions. An API must make both its numeric behavior and geometric semantics visible in its type signature.
- Points and vectors share coordinate-system metadata but represent different mathematical objects. Preserve affine semantics: `Point - Point` produces a `Vector`; `Point + Vector` and `Point - Vector` produce a `Point`; do not expose `Point + Point` or vector-only operations on points.
- Do not add a total ordering to points or vectors unless the ordering has a documented mathematical meaning that is consistent with equality; component equality alone does not justify `IComparable<TSelf>` or relational operators.
- Geometric values are immutable. Prefer `readonly record struct` for value entities so component equality, operators, and diagnostics follow .NET value semantics; use a plain `readonly struct` only when record semantics are unsuitable for the mathematical contract.
- New geometric entities should follow the established generic-math value-type pattern: immutable storage, static abstract/operator contracts where valid, checked/saturating/truncating numeric conversion, and focused algebraic tests.

## Geometry Architecture

| Layer | Responsibility |
| --- | --- |
| `NetFabric.Numerics.Angle` | Unit-tagged angles, reduction, trigonometry, and degree/radian/gradian/revolution conversion |
| `NetFabric.Numerics` | Rectangular 2D/3D, polar, and spherical points and vectors; coordinate-system metadata; quaternion operations; numeric and coordinate-system conversions |
| `NetFabric.Numerics.Geodesy` | Datum- and ellipsoid-aware geodetic coordinates built on the core and angle packages |

- Preserve coordinate-system and angle-unit generic parameters through APIs; do not replace them with untyped numeric values.
- Model point/vector arithmetic consistently: point minus point produces a vector, while point plus or minus vector produces a point. Points and vectors must remain separate types even when their component layouts match.
- Keep cross-system conversions in the static helpers for the source coordinate family. Use radians for trigonometric conversions and preserve documented component and angle conventions.
- Add or update focused tests for every public operation, conversion direction, and generic numeric conversion mode (`CreateChecked`, `CreateSaturating`, and `CreateTruncating`) changed by a task.
- Test conversion invariants as well as individual directions: angle-unit and coordinate-system round trips must preserve values within a documented, type-appropriate tolerance, and reduction must be idempotent. Cover singular cases such as the origin and spherical poles explicitly.

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

<!-- OPENWIKI:START -->

## OpenWiki

This repository has a generated `openwiki/` evidence index. It is optional just-in-time context, not required startup reading.

- Treat source code and tests as authoritative. A brief's unknowns and review items are verification gaps, not automatic requirements.
- Prefer the narrowest quiet validation that proves the changed behavior. Preserve complete failure output.

The scheduled OpenWiki GitHub Actions workflow refreshes the repository wiki. Do not hand-edit generated OpenWiki pages unless explicitly asked; prefer updating source code/docs and letting OpenWiki regenerate.

<!-- OPENWIKI:END -->
