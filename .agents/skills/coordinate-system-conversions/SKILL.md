---
name: coordinate-system-conversions
description: Abstract math for converting between 2D and 3D coordinate systems. USE FOR: Cartesian to/from polar (r, θ); Cartesian to/from spherical (r, azimuth, polar/colatitude); azimuth vs. elevation vs. colatitude conventions; canonical range enforcement after conversion; precision near the origin and poles; choosing azimuth reference direction. DO NOT USE FOR: angle unit conversions (use angles-and-circular-arithmetic); geodetic (lat/lon) coordinates (use geodetic-coordinate-bounds); rotation representations (use 3d-rotation-theory).
---

# Coordinate System Conversions

## 2D: Cartesian ↔ Polar

| Direction | Formulas |
|-----------|---------|
| Cartesian → Polar | r = √(x²+y²);  θ = atan2(y, x) |
| Polar → Cartesian | x = r cos θ;  y = r sin θ |

- r ≥ 0 always; r = 0 at origin (θ undefined)
- θ ∈ (−π, π] from `atan2`; reduce to [0, 2π) for canonical polar form
- Azimuth measured CCW from positive x-axis

## 3D: Cartesian ↔ Spherical

**Physics convention** (ISO 80000-2): r, polar angle θ from z-axis (colatitude), azimuth φ from x-axis

| Direction | Formulas |
|-----------|---------|
| Cartesian → Spherical | r = √(x²+y²+z²);  θ = arccos(z/r);  φ = atan2(y, x) |
| Spherical → Cartesian | x = r sin θ cos φ;  y = r sin θ sin φ;  z = r cos θ |

**Canonical ranges**: r ≥ 0,  θ ∈ [0, π],  φ ∈ [0, 2π) or (−π, π]

## Convention Table (3D)

| Name | 1st angle | 2nd angle | Used in |
|------|-----------|-----------|---------|
| Physics (ISO) | θ = colatitude from z | φ = azimuth from x | Math, physics |
| Geography | lat = elevation from equator | lon = azimuth from meridian | Maps, geodesy |
| This codebase | polar = colatitude from z | azimuth = CCW from x | Spherical coords |

⚠ **Azimuth and polar are always named explicitly** — never assume which is which.

## Singular Points

| Point | Condition | Issue |
|-------|-----------|-------|
| Origin | r = 0 | θ and φ undefined |
| North/South pole | θ = 0 or θ = π | φ undefined (infinite azimuths map to same point) |

## 3D: Cartesian ↔ Cylindrical (reference)

```
ρ = √(x²+y²);   φ = atan2(y, x);   z = z
x = ρ cos φ;    y = ρ sin φ;        z = z
```

## Reference Files

| File | Load When |
|------|-----------|
| [references/formulas.md](references/formulas.md) | Derivations, Jacobians, differential area/volume elements |
| [references/numerical-stability.md](references/numerical-stability.md) | Near-origin, near-pole precision, atan2 edge cases, r near zero |
