# Coordinate System Conversions — Formulas

## Polar: Derivation

Point (x, y) in Cartesian ≡ point at distance r from origin at angle θ from +x axis:
```
x = r cos θ,  y = r sin θ    (unit circle definition)
r = √(x²+y²)                  (distance from origin)
θ = atan2(y, x)               (four-quadrant angle)
```

## Spherical: Full Derivation

Starting from the unit sphere and projecting:
```
z = r cos θ                        (height = radius × cos(polar angle))
x = r sin θ cos φ                  (horizontal = projected radius × cos(azimuth))
y = r sin θ sin φ                  (horizontal = projected radius × sin(azimuth))
```

Inverse:
```
r = √(x²+y²+z²)
θ = arccos(z/r)                    (polar angle from z; undefined at r=0)
φ = atan2(y, x)                    (azimuth; undefined at x=y=0)
```

Intermediate: `ρ = √(x²+y²)` (cylindrical radius), so `φ = atan2(y, x)` and `θ = atan2(ρ, z)`.

## Canonical Range Enforcement

After conversion, angles may need reduction:

| Angle | Canonical range | Method |
|-------|----------------|--------|
| 2D azimuth φ | [0, 2π) or (−π, π] | floor-based reduction or atan2 sign |
| Spherical azimuth φ | [0, 2π) | reduction |
| Spherical polar θ | [0, π] | arccos always gives this; no reduction needed |

## Differential Area Element

Spherical: `dA = r² sin θ dθ dφ`  — the sin θ factor is why area vanishes at the poles.

Polar: `dA = r dr dθ`

## Relation: Spherical ↔ Geographic

Geographic uses elevation (latitude) measured from equatorial plane, not from z-axis:
```
lat = π/2 − θ         (elevation = 90° − colatitude)
lon = φ               (same azimuth)
```

So `θ = 0` (north pole) corresponds to `lat = 90°`, and `θ = π` (south pole) to `lat = −90°`.

## Round-Trip Property

Spherical → Cartesian → Spherical recovers the original angles only if:
- r > 0 (not at origin)
- The azimuth is not at a pole (θ ≠ 0 and θ ≠ π)
- The reduced canonical form is compared (angles modulo 2π)

At poles: any azimuth maps to the same point, so round-trip recovers a canonical azimuth (typically 0), not the original.
