---
name: geodetic-coordinate-bounds
description: Abstract math for geodetic coordinate constraints: valid ranges, wrap semantics, and coordinate type distinctions. USE FOR: validating latitude ∈ [−90°, +90°] and longitude ∈ (−180°, +180°]; understanding why latitude is clamped but longitude wraps; difference between geodetic and geocentric latitude; ellipsoidal height vs. orthometric (MSL) height; antimeridian handling; normalizing out-of-range coordinates. DO NOT USE FOR: reference ellipsoid parameters (use reference-ellipsoids); datum transformations (use helmert-datum-transformation); coordinate system conversions (use coordinate-system-conversions).
---

# Geodetic Coordinate Bounds

## Valid Ranges

| Coordinate | Range | Boundary type |
|-----------|-------|--------------|
| Latitude φ | [−90°, +90°] | Closed both ends (poles are valid points) |
| Longitude λ | (−180°, +180°] | Open at −180° (the antimeridian is a single line) |
| Ellipsoidal height h | (−∞, +∞) | No geometric bound (can be below ellipsoid) |

## Why Latitude is Clamped, Not Wrapped

The poles are distinct point-entities: there is no "86°N wraps to 94°N". Latitude has physical poles at ±90°.

Values outside [−90°, +90°] indicate an input error — clamp or reject.

## Why Longitude Wraps

Longitude is a circular coordinate: 180°E and 180°W are the same meridian (antimeridian).
Wrap formula: `λ = ((λ + 180°) mod 360°) − 180°` → maps to (−180°, +180°].

`−180°` is excluded to prevent both −180° and +180° representing the antimeridian.
`+180°` is included as the canonical form for the antimeridian.

## Geodetic vs. Geocentric Latitude

| Type | Symbol | Definition |
|------|--------|-----------|
| Geodetic | φ | Angle of ellipsoid normal to equatorial plane |
| Geocentric | φ' | Angle of radius vector to equatorial plane |
| Reduced (parametric) | β | Auxiliary latitude for ellipse parameterization |

For WGS84: `φ − φ' ≤ 11.5 arcmin` at 45°.

Conversion (geodetic → geocentric):
```
tan φ' = (1 − e²) · tan φ = (b/a)² · tan φ
```

## Antimeridian Handling

A geographic polygon crossing the antimeridian requires special treatment:
- Do NOT test if all longitudes are < 180° (a segment from 170°E to −170°E crosses the antimeridian)
- Detect crossing: `|λ₂ − λ₁| > 180°`
- Split the polygon at the antimeridian for Cartesian bounding box calculations

## Height Types

| Type | Reference surface | Use |
|------|-----------------|-----|
| Ellipsoidal height h | Reference ellipsoid (WGS84) | GPS, geometric geodesy |
| Orthometric height H | Geoid (mean sea level) | Topographic, engineering |
| Relationship | h = H + N (N = geoid undulation) | N varies by location (−107 to +85 m) |

## Reference Files

| File | Load When |
|------|-----------|
| [references/formulas.md](references/formulas.md) | Longitude wrap formula, geodetic→geocentric derivation, height conversion |
| [references/numerical-stability.md](references/numerical-stability.md) | Floating-point boundary tests, antimeridian edge cases, pole singularity |
