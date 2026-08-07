# AGENTS.md — NetFabric.Numerics

Delta from [root AGENTS.md](../../AGENTS.md). Build, test, and code-style rules apply unchanged.

## Package scope

Strongly-typed geometric primitives for four coordinate systems:

| Namespace | Coordinate system | Types |
|-----------|------------------|-------|
| `NetFabric.Numerics.Rectangular2D` | 2D Cartesian | `Point<T>`, `Vector<T>` |
| `NetFabric.Numerics.Rectangular3D` | 3D Cartesian | `Point<T>`, `Vector<T>` |
| `NetFabric.Numerics.Polar` | 2D polar | `Point<TAngleUnits, T>` |
| `NetFabric.Numerics.Spherical` | 3D spherical | `Point<TAngleUnits, T>` |

## Key patterns

- `ICoordinateSystem` tags a coordinate system type; `IPoint<TSelf, TCoordinateSystem>` and `IVector<TSelf, TCoordinateSystem>` constrain point and vector types respectively.
- Span/aggregate operations (`VectorSpanOperations.cs`, `VectorSum.cs`, `VectorAverage.cs`) mirror the pattern in the Angle package — SIMD paths belong there.
- Do not add mutable state to structs; `ErrorProne.NET.Structs` will flag it.

## Test project

`../NetFabric.Numerics.UnitTests/` — run with:

```sh
dotnet test src/NetFabric.Numerics.UnitTests -c Release
```
