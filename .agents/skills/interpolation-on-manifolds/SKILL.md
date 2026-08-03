---
name: interpolation-on-manifolds
description: Abstract math for interpolating angles, vectors, and quaternions on curved manifolds. USE FOR: implementing Lerp (linear interpolation) for vectors; Nlerp (normalized lerp) for directions; Slerp (spherical linear interpolation) for quaternions and unit vectors; shortest-path sign flip when dot product is negative; choosing between Lerp/Nlerp/Slerp; angular lerp with wrap-around. DO NOT USE FOR: quaternion arithmetic (use quaternion-algebra); rotation representation conversions (use 3d-rotation-theory); angle unit conversions (use angles-and-circular-arithmetic).
---

# Interpolation on Manifolds

## Lerp (Linear Interpolation)

`lerp(a, b, t) = (1−t)·a + t·b`  Result NOT on unit sphere; constant Euclidean velocity.

## Nlerp (Normalized Lerp)

`nlerp(a, b, t) = normalize(lerp(a, b, t))`  Cheaper than Slerp; breaks down at antipodal inputs.

## Slerp (Spherical Linear Interpolation)

Great-circle arc at constant angular velocity:

```
Ω = arccos(a · b)
slerp(a, b, t) = sin((1−t)Ω)/sin(Ω) · a  +  sin(tΩ)/sin(Ω) · b
```

Fall back to Lerp when Ω ≈ 0. Undefined for antipodal inputs (Ω = π).

## Shortest-Path Flip

For quaternions, **q** and **−q** represent the same rotation.
If `a · b < 0`, interpolation takes the long path (> 180°). Fix:

```
if (a · b < 0):  b = −b    (before calling slerp/nlerp)
```

## Which to Use

| Situation | Method | Reason |
|-----------|--------|--------|
| Arbitrary vectors in ℝⁿ | Lerp | Correct; no manifold curvature |
| Unit vectors (approximate) | Nlerp | Fast; adequate for small angles |
| Unit quaternions (smooth) | Slerp | Constant angular velocity |
| Unit vectors (exact) | Slerp | Correct geodesic arc |
| Angles with wrapping | Angular lerp | Handles 350°↔10° via shortest arc |

## Angular Lerp (Wrap-Aware)

```
delta = reduce(b − a, Full)          // shortest signed difference ∈ [−Full/2, Full/2)
result = a + t · delta
```

This ensures the interpolation takes the short way around.

## Reference Files

| File | Load When |
|------|-----------|
| [references/formulas.md](references/formulas.md) | Slerp derivation, degenerate cases, angle lerp proof |
| [references/numerical-stability.md](references/numerical-stability.md) | Near-identical inputs, antipodal inputs, sin(Ω)→0 handling |
