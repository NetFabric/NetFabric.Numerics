# AGENTS.md — NetFabric.Numerics.Angle

Delta from [root AGENTS.md](../../AGENTS.md). Build, test, and code-style rules apply unchanged.

## Package scope

Strongly-typed angle representation. Core types:

| Type | Purpose |
| --- | --- |
| `Angle<TUnits, T>` | General angle parameterized by unit (`Degrees`, `Radians`, `Gradians`, `Revolutions`) and numeric type `T` |
| `AngleReduced<TUnits, T>` | Angle guaranteed to be in the canonical range for its unit (e.g., `[0°, 360°)`) |
| `IAngleUnits<TSelf>` | Static abstract interface defining the range constants for a unit type |

## Key patterns

- Both the unit type and the numeric type must always be parameterized — avoid hardcoding either.
- `AngleReduced<TUnits, T>` is only produced by `Angle.Reduce()`; never construct it directly without calling `Reduce` first.
- Point and vector families in `NetFabric.Numerics` delegate angle-unit conversion to this package. When adding a unit conversion, preserve non-angle components and add the matching polar and spherical coverage where applicable.
- Coordinate-system trigonometric conversions evaluate in radians. Convert through the existing `Angle.ToRadians` helpers instead of duplicating unit factors in geometry code.
- Span operations (`AngleSpanOperations.cs`) use `System.Numerics.Tensors.TensorPrimitives` for SIMD acceleration — keep tensor operations in that file.
- Trigonometry helpers (`AngleTrigonometry.cs`) delegate to the static methods on `T` (e.g., `T.Sin`) — do not add `Math`/`MathF` calls.

## Test project

`../NetFabric.Numerics.Angle.UnitTests/` — run with:

```sh
dotnet test src/NetFabric.Numerics.Angle.UnitTests -c Release
```
