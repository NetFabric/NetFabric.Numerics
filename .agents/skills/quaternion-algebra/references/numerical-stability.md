# Quaternion Algebra — Numerical Stability

## Normalization Drift

Every floating-point Hamilton product introduces tiny rounding errors. After repeated multiplications,
‖q‖ drifts away from 1. The drift is ≈ ε per multiplication.

**Fix**: renormalize periodically:
```csharp
q = q / q.Norm();
```

For rotation-only workloads, renormalize every N multiplications (e.g., N = 64–256).
Avoid renormalizing every operation — it is expensive and unnecessary.

## Hemisphere Consistency (Double-Cover)

q and −q represent the same rotation, but they are on opposite hemispheres of S³.
Mixing hemispheres causes:
- Lerp: interpolation takes the long path (330° instead of 30°)
- Slerp: abrupt flip at the midpoint

**Fix**: before interpolating or composing, ensure the quaternions are on the same hemisphere:
```
if (q₁ · q₂ < 0)  q₂ = −q₂;
```

## Construction Precision: sin(θ/2) Near 0

When θ ≈ 0, `sin(θ/2) ≈ θ/2` and the axis components are tiny. The axis direction is
unreliable if θ is very small. Near-identity rotations are safely represented as q ≈ (0,0,0,1).

## Norm Computation Overflow

For components with magnitude ~1e154 (double), x²+y²+z²+w² overflows.
Normalize each component by max before computing norm:
```
m = max(|x|, |y|, |z|, |w|)
‖q‖ = m · √((x/m)² + (y/m)² + (z/m)² + (w/m)²)
```

## Inverse for Non-Unit Quaternion

`q⁻¹ = q* / ‖q‖²` — computing `‖q‖²` avoids the sqrt in `‖q‖`.
If ‖q‖² ≈ 0 (near-zero quaternion), the inverse is numerically unstable.

## IsIdentity Check

`q == (0,0,0,1)` fails with floating-point. Check:
```
|x| < ε  &&  |y| < ε  &&  |z| < ε  &&  |w − 1| < ε
```

Or equivalently: `|q·q_identity − 1| < ε` where `q_identity = (0,0,0,1)`.

## Composition Order Matters

Quaternion multiplication is associative but not commutative.
`q_total = q_second · q_first` applies `q_first` rotation first, then `q_second`.
Getting the order wrong is silent — the type system cannot catch it.

Document the convention (local-to-world vs. world-to-local) explicitly.
