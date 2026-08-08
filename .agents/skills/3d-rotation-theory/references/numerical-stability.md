# 3D Rotation Theory — Numerical Stability

## Rotation Matrix Orthogonality Drift

After repeated floating-point matrix multiplications, R drifts away from SO(3):
- `RᵀR ≠ I` accumulates error
- `det(R) ≠ 1` can even become negative

**Fix**: periodically re-orthogonalize using Gram-Schmidt or SVD polar decomposition.
Quaternions are much cheaper to renormalize (4 values vs. 9).

## Gimbal Lock Detection

From the quaternion → Euler formula, the pitch singularity occurs when:
```
test = 2(wy − zx) ≈ ±1
```

When `|test| > 0.9999`, treat as gimbal-locked:
- Set roll = 0 (or preserve from context)
- Compute yaw = ±2 · atan2(x, w)

## Near-Zero Axis Extraction from Quaternion

When θ ≈ 0, `sin(θ/2) ≈ 0`, so dividing (x, y, z) by `sin(θ/2)` amplifies noise.
Guard:
```csharp
if (Math.Abs(1.0 - w) < ε)
    return (Vector3.UnitZ, 0.0);   // identity rotation; axis is arbitrary
```

## Matrix to Quaternion: Shepperd Branching

Naive `w = 0.5 · √(1 + trace)` becomes `√(near-zero)` when trace ≈ −1 (180° rotation).
Shepperd's method branches on the largest diagonal element to always use a well-conditioned square root.

## Pitch Clamp for arcsin

In quaternion → Euler: `φ = arcsin(2(wy − zx))`. Clamp the argument to [−1, 1]:
```csharp
double sinPitch = Math.Clamp(2.0 * (w*y - z*x), -1.0, 1.0);
```

Without clamping, rounding in unit quaternion multiplication can push the argument outside [−1, 1].

## Convention Ambiguity (Silent Correctness Bug)

Rotation representations have multiple incompatible conventions:
- Euler order: ZYX vs. XYZ vs. ZXZ — all are called "yaw/pitch/roll" in different fields
- Active vs. passive rotation
- Row-vector vs. column-vector matrix multiplication convention

Always document which convention is used; mixing conventions causes wrong-but-plausible results.

## Rotation Matrix Column/Row Major

`v' = R · v` (column vector) uses column-major matrix interpretation.
`v' = v · R` (row vector) uses row-major (transpose of above).

Most math literature uses column vectors; some graphics APIs use row vectors. This is a sign error.
