# Geodetic Coordinate Bounds — Numerical Stability

## Latitude Boundary Test

`φ == 90.0°` uses exact floating-point equality — reliable only for values that were never computed.
For computed latitudes, use: `Math.Abs(φ − 90.0) < ε` for "at north pole".

For clamping: `Math.Clamp(φ, −90.0, 90.0)` is correct in IEEE 754 (handles NaN → NaN, ±Inf → ±90).

## Longitude Wrap: −180° Case

After `((λ + 180) % 360) − 180`:
- Input `−180°` → output `−180°` if using truncating `%` in some languages
- But the canonical form is `+180°` for the antimeridian

Explicit fix:
```csharp
if (lon <= -180.0) lon += 360.0;
if (lon > 180.0)   lon -= 360.0;
```
(Handles the ±180 boundary correctly for in-range inputs; for large out-of-range, use floor-based reduction.)

## Pole Singularity in Geodetic Computations

At φ = ±90°:
- `cos(φ) = 0`, so X = Y = 0 (on the polar axis)
- Any longitude is valid — the point is the pole regardless of λ
- Longitude is undefined; store as 0 by convention

When converting between representations, special-case the poles:
```
if |φ| ≈ 90°:  return (φ = ±90°, λ = 0°, h = Z − b)   // b = polar radius
```

## Geocentric ↔ Geodetic Precision

The Bowring iteration converges to double precision in 3 iterations for all latitudes.
For the equator (φ = 0°), the formula reduces to:
```
φ = 0,  h = √(X²+Y²) − a
```

Near the poles (φ → 90°), the formula `h = Z/sin(φ) − N(1−e²)` is better conditioned
than `h = p/cos(φ) − N` (division by cos(φ) → 0).

## Height Precision

Ellipsoidal height h is computed as a difference of two large numbers at sea level:
```
h = p/cos(φ) − N(φ)     where both terms ≈ 6,378,000 m
```

For h ≈ 0: the subtraction cancels ~7 significant digits, leaving only ~8 digits of precision.
At h = 1000 m above sea level: full double precision is maintained for h.

## Antimeridian Floating-Point

A point at exactly λ = 180.0° is canonically +180° (not −180°).
Testing `λ == 180.0` is safe for this canonical value; it's an exact representable double.
Testing `λ == −180.0` after normalization should never be true if normalization is correct.
