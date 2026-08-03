# Interpolation on Manifolds — Numerical Stability

## Slerp: sin(Ω) Near Zero

When Ω ≈ 0, `sin(Ω) ≈ 0` → division by near-zero in:
```
sin((1−t)Ω) / sin(Ω)
```

**Fix**: when `Ω < ε` (e.g., ε = 1e-4 rad), fall back to Lerp (optionally with normalization):
```csharp
if (Omega < 1e-4)
    return Normalize(Lerp(a, b, t));
```

Threshold ε = 1e-4 rad ≈ 0.006°; below this, Lerp error is < 1 ulp.

## Antipodal Quaternions (Ω ≈ π)

When `a · b ≈ −1`, slerp involves `sin(Ω) ≈ 0` again (Ω ≈ π).
There is no unique shortest path: both hemispheres are equally short.

Standard practice: choose one axis (e.g., `UnitX × a` or `UnitZ × a`, picking whichever isn't parallel) and slerp through that auxiliary quaternion.

## Hemisphere Consistency

Before Slerp or Nlerp, always apply the shortest-path flip:
```
if (a · b < 0):  b = −b
```

Not doing this causes 330°-arc interpolation instead of 30°-arc. The result is still valid
(same endpoint), but the path is wrong and `t = 0.5` gives a 90° rotation away from the midpoint.

## Lerp Result Not on Manifold

`lerp(a, b, t)` for unit vectors produces vectors with ‖result‖ < 1 (always shorter than 1 except at endpoints).

If a unit-length result is required (e.g., for direction vectors), use Nlerp or Slerp.

## t Outside [0, 1]

Extrapolation (t < 0 or t > 1) is mathematically valid for Lerp and Slerp.
For Slerp with t > 1, the great-circle arc continues past **b** — this is correct but unusual.
Validate t range at the API boundary based on the use case.

## Angular Lerp Wrap Precision

When computing `delta = b − a` in floating-point, the reduction step:
```
if delta > Full/2:   delta -= Full
```
can accumulate round-off if Full is not exactly representable (e.g., 2π is irrational).

For degree/gradian/revolution units, Full is an exact integer, so this is not a problem.
For radians, Full = 2π ≈ 6.28318...; the reduction introduces rounding at the threshold boundary.
