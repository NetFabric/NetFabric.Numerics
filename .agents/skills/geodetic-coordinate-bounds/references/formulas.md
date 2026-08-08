# Geodetic Coordinate Bounds — Formulas

## Longitude Normalization

Map any λ to the canonical range (−180°, +180°]:
```
λ_norm = ((λ + 180°) mod 360°) − 180°
```

Using floor-based mod (handles negatives correctly):
```
λ_norm = λ − 360° · floor((λ + 180°) / 360°)
```

Edge case: when `λ_norm = −180°` (due to floating-point floor), correct to `+180°`.

## Geodetic ↔ Geocentric Latitude

Geodetic φ (from ellipsoid normal), geocentric φ' (from Earth's center):
```
tan φ' = (b/a)² · tan φ = (1 − e²) · tan φ
```

Difference (geodetic − geocentric):
```
Δφ = φ − φ' ≈ e² sin(2φ) / 2    (first-order approximation)
```

Maximum at φ = 45°: Δφ ≈ 11.48 arcmin for WGS84.

## Reduced (Parametric) Latitude β

Used to parameterize the ellipse:
```
tan β = (b/a) · tan φ = (1−f) · tan φ
```

Relationship: φ ≥ β ≥ φ'  for φ ∈ [0°, 90°].

## Geocentric Cartesian ↔ Geodetic

Geodetic to geocentric Cartesian:
```
X = (N + h) cos φ cos λ
Y = (N + h) cos φ sin λ
Z = (N(1−e²) + h) sin φ
```
where N = a / √(1−e²sin²φ) is the prime vertical radius of curvature.

Cartesian to geodetic (Bowring iterative, converges in 2–3 iterations):
```
p = √(X²+Y²)
θ₀ = atan2(Z·a, p·b)        (initial estimate)
φ  = atan2(Z + e'²·b·sin³θ, p − e²·a·cos³θ)   (iterate by updating θ = atan2(Z+e²·N·sinφ, p))
λ  = atan2(Y, X)
h  = p/cos φ − N(φ)
```

## Geoid Height Relationship

```
h = H + N_geoid
```
- h: ellipsoidal (GPS) height above ellipsoid surface
- H: orthometric (elevation above mean sea level)
- N_geoid: geoid undulation (EGM2008 model, ≈ −107 to +85 m globally)

This requires a geoid model (e.g., EGM96, EGM2008) — cannot be computed from ellipsoid alone.

## Antimeridian Crossing Detection

For a segment (λ₁, λ₂) in canonical form:
```
crosses_antimeridian = |λ₂ − λ₁| > 180°
```

If crossing:
```
// Normalize so segment goes east:
if λ₂ < λ₁:  λ₂ += 360°    (bring to [−180°, 540°])
```
