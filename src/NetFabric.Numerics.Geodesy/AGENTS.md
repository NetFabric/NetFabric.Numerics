# AGENTS.md — NetFabric.Numerics.Geodesy

Delta from [root AGENTS.md](../../AGENTS.md). Build, test, and code-style rules apply unchanged.

## Package scope

Strongly-typed geodetic coordinates built on top of `NetFabric.Numerics` and `NetFabric.Numerics.Angle`. Core types:

| Type | Purpose |
|------|---------|
| `Ellipsoid<T>` | Reference ellipsoid parameters (semi-major axis, flattening) |
| `Datum<T>` | Geodetic datum binding a coordinate system to an ellipsoid |
| `GeodeticCoordinateSystem<TDatum>` | Coordinate system type parameterized by datum |
| `IGeodeticPoint<TSelf, TDatum>` | Interface for geodetic point types (latitude, longitude, height) |

## Key patterns

- Latitude is always in `[-90°, +90°]`; longitude wraps in `(-180°, +180°]` — validate at construction and document in XML docs.
- Ellipsoid math uses `T`'s static methods (e.g., `T.Sqrt`, `T.Pow`) — no `Math`/`MathF`.
- `Geodetic2/` contains 2D (lat/lon) types; `Geodetic3/` adds ellipsoidal height — keep them in their respective subdirectories.

## Test project

`../NetFabric.Numerics.Geodesy.UnitTests/` — run with:

```sh
dotnet test src/NetFabric.Numerics.Geodesy.UnitTests -c Release
```
