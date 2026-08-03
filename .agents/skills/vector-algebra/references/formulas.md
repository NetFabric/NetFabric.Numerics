# Vector Algebra — Formulas

## Scalar (Dot) Projection

Projection of **a** onto **b**:
```
proj_b(a) = (a · b / ‖b‖²) · b        (vector projection)
scalar    = a · b / ‖b‖                (signed scalar length)
```

## Vector Rejection

Component of **a** perpendicular to **b**:
```
rej_b(a) = a − proj_b(a)
```

## Scalar Triple Product (3D)

```
a · (b × c) = det([a b c])
```

| Value | Meaning |
|-------|---------|
| > 0 | a, b, c form right-handed triple |
| = 0 | Vectors are coplanar |
| < 0 | Left-handed triple |

## Distance Between Points

```
d(P, Q)  = ‖Q − P‖  = √((Qₓ−Pₓ)² + (Qᵧ−Pᵧ)²[+ (Q_z−P_z)²])
d²(P, Q) = (Qₓ−Pₓ)² + (Qᵧ−Pᵧ)²[+ (Q_z−P_z)²]   (avoid sqrt for comparison)
```

## Cross Product Properties

```
a × a = 0
a × b = −(b × a)                    (anti-commutative)
a × (b + c) = a×b + a×c            (distributive)
(ka) × b = k(a × b)                 (scalar associative)
‖a × b‖² = ‖a‖²‖b‖² − (a·b)²       (Lagrange identity)
```

Right-hand rule: curl fingers from **a** toward **b**; thumb points in direction of **a**×**b**.

## Cross Product for Parallel Test

`a × b = 0` iff **a** and **b** are parallel (or one is zero).

## 2D Pseudo-Cross Product

```
a ⊗ b = aₓbᵧ − aᵧbₓ     (z-component of 3D cross product)
```

Sign interpretation:
- Positive: **b** is CCW from **a** (left turn)
- Negative: **b** is CW from **a** (right turn)
- Zero: **b** is parallel to **a**

## Angle Between Vectors (Full Formula)

The arccos formula gives ∈ [0, π] and cannot distinguish CCW from CW.

For a signed angle in 2D: `θ = atan2(a⊗b, a·b)` gives ∈ (−π, π].

## Normalization

```
v̂ = v / ‖v‖
```

Pre-condition: `‖v‖ ≠ 0`. Post-condition: `‖v̂‖ = 1` (within floating-point tolerance).

`IsNormalized(v, ε)`: `|‖v‖ − 1| < ε`

## Component-wise Clamp

```
clamp(v, min, max) = (clamp(vₓ, minₓ, maxₓ), clamp(vᵧ, minᵧ, maxᵧ)[, clamp(v_z, min_z, max_z)])
```

This is NOT the same as clamping the magnitude.
