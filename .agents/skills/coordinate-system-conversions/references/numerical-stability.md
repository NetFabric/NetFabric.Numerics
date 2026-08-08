# Coordinate System Conversions — Numerical Stability

## Near-Origin (r → 0)

When x ≈ 0 and y ≈ 0:
- `r = √(x²+y²)` underflows to 0 for subnormal inputs
- `θ = arccos(z/r)` → division by zero → NaN
- `φ = atan2(y, x)` at (0, 0) is implementation-defined (typically 0 or platform-specific)

Guard: check `r > ε` before computing angles. Return zero-angle representation for origin.

## Near-Pole (θ → 0 or θ → π)

Near the poles, `sin θ ≈ 0`, making the azimuth φ unreliable:
- `x = r sin θ cos φ ≈ 0`, `y = r sin θ sin φ ≈ 0` regardless of φ
- Converting back: `φ = atan2(y, x)` ≈ `atan2(0, 0)` — undefined

This is the coordinate singularity of spherical coordinates.
Round-trip through poles does NOT preserve azimuth.

## arccos Argument for Polar Angle

`θ = arccos(z/r)`: the ratio `z/r` can exceed [−1, 1] due to rounding, especially when z ≈ r:
```csharp
double polar = Math.Acos(Math.Clamp(z / r, -1.0, 1.0));
```

## atan2 Precision Near Axes

`atan2(y, x)` near the +x axis (y ≈ 0, x > 0) returns values near 0 accurately.
Near the −x axis (y ≈ 0, x < 0), the result is near ±π; small perturbations in y cause
the sign to flip. This is a genuine discontinuity in the canonical range (−π, π] at the −x axis.

For [0, 2π) canonical range, the discontinuity moves to the +x axis (0 and 2π boundary).

## Squared Radius Overflow

`x²+y²+z²` overflows for components near float.MaxValue / √3.
Scale by `max(|x|, |y|, |z|)` first, then scale the radius back.

## Polar → Cartesian Precision for Small θ

`x = r sin θ cos φ` when θ ≈ 0: `sin θ ≈ θ` — use Taylor approximation `sin θ ≈ θ − θ³/6` for θ < 1e-4 to avoid `sin` precision loss.

## IEEE 754 Sign of Zero

`atan2(−0.0, −1.0) = −π` but `atan2(+0.0, −1.0) = +π`.
When computing angles from exact-zero coordinates, the sign of zero matters.
Normalize −0.0 to 0.0 when canonicalizing.
