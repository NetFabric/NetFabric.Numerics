# AGENTS.md — NetFabric.Numerics

Delta from [root AGENTS.md](../../AGENTS.md). Build, test, and code-style rules apply unchanged.

## Package scope

Generic, strongly-typed geometric primitives and conversions for four coordinate systems:

| Namespace | Coordinate system | Types |
| --- | --- | --- |
| `NetFabric.Numerics.Rectangular2D` | 2D Cartesian | `Point<T>`, `Vector<T>` |
| `NetFabric.Numerics.Rectangular3D` | 3D Cartesian | `Point<T>`, `Vector<T>` |
| `NetFabric.Numerics.Polar` | 2D polar | `Point<TAngleUnits, T>` |
| `NetFabric.Numerics.Spherical` | 3D spherical | `Point<TAngleUnits, T>` |

Shared abstractions in the package root provide the generic contract:

| Abstraction | Responsibility |
| --- | --- |
| `ICoordinateSystem` and `CoordinateSystem<T>` | Static coordinate metadata exposed through a singleton adapter |
| `IGeometricBase<TSelf, TCoordinateSystem>` | Typed coordinate system, indexed coordinate access, and a generic zero value |
| `IPoint<TSelf, TCoordinateSystem>` / `IVector<TSelf, TCoordinateSystem, T>` | Point and vector contracts, generic-math operators, and min/max values |
| `Utils` | Shared generic magnitude, square, and approximate-equality helpers |

## Key patterns

- `ICoordinateSystem` tags a coordinate system type; `IPoint<TSelf, TCoordinateSystem>` and `IVector<TSelf, TCoordinateSystem>` constrain point and vector types respectively.
- Keep entities as immutable `readonly struct` values. Every coordinate-system implementation must expose static `Coordinates` metadata in the same order as its indexed coordinate access.
- Preserve geometric arithmetic: `Point - Point` returns the matching `Vector`; `Point +/- Vector` returns a `Point`. Keep scalar vector operators and aggregate operations type-preserving.
- Place coordinate-system conversions in the static `Point` or `Vector` helper for the source family. Rectangular 2D and polar conversions use `Point.ToPolar` and `Point.ToRectangular`; convert angular values to radians before trigonometric evaluation.
- Polar uses `Radius` and counterclockwise `Azimuth`; spherical uses `Radius`, XY-plane `Azimuth`, and `Polar` measured from the positive Z-axis. Keep these conventions consistent in constructors, conversions, and XML documentation.
- `PointReduced<TAngleUnits, T>` represents reduced angular point values. Use the existing reduction path rather than bypassing its canonical-angle guarantees.
- Span/aggregate operations (`VectorSpanOperations.cs`, `VectorSum.cs`, `VectorAverage.cs`) mirror the pattern in the Angle package — SIMD paths belong there.
- Do not add mutable state to structs; `ErrorProne.NET.Structs` will flag it.

## Tests

- Keep tests under `../NetFabric.Numerics.UnitTests/` in the coordinate-family directory that owns the behavior.
- Cover coordinate metadata, basic point/vector arithmetic, numeric conversions, and both directions of every coordinate-system conversion. Use approximate assertions for floating-point trigonometric results.
- Keep span and aggregate tests separate from scalar-operation tests, following the existing `Span*Tests`, `SumTests`, and `AverageTests` layout.

## Test project

`../NetFabric.Numerics.UnitTests/` — run with:

```sh
dotnet test src/NetFabric.Numerics.UnitTests -c Release
```
