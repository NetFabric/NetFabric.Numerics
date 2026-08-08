---
name: quaternion-algebra
description: "Abstract math for quaternion algebra (hypercomplex numbers ℍ). USE FOR: implementing quaternion multiplication (Hamilton product, non-commutative); conjugate, norm, and inverse; unit quaternions on S³ as double cover of SO(3); rotating 3D vectors with quaternions; understanding why q and −q represent the same rotation; normalizing after operations; constructing rotation quaternions from axis-angle. DO NOT USE FOR: converting between rotation representations (use 3d-rotation-theory); slerp/lerp interpolation (use interpolation-on-manifolds); basic vector cross/dot products (use vector-algebra)."
---

# Quaternion Algebra

## Structure

A quaternion **q** = (x, y, z, w) = xi + yj + zk + w where i, j, k are imaginary units.

Also written as **q** = (**v**, w) with vector part **v** = (x, y, z) and scalar part w.

```
i² = j² = k² = ijk = −1
ij = k,  jk = i,  ki = j
ji = −k, kj = −i, ik = −j
```

## Core Operations

| Operation | Formula |
|-----------|---------|
| Addition | (x₁+x₂, y₁+y₂, z₁+z₂, w₁+w₂) — component-wise |
| Conjugate | **q*** = (−x, −y, −z, w) |
| Norm | ‖**q**‖ = √(x²+y²+z²+w²) |
| Norm squared | ‖**q**‖² = x²+y²+z²+w² |
| Scalar multiply | k**q** = (kx, ky, kz, kw) |

## Hamilton Product (Non-Commutative)

```
q₁ · q₂ = (w₁w₂ − x₁x₂ − y₁y₂ − z₁z₂,
            w₁x₂ + x₁w₂ + y₁z₂ − z₁y₂,
            w₁y₂ − x₁z₂ + y₁w₂ + z₁x₂,
            w₁z₂ + x₁y₂ − y₁x₂ + z₁w₂)
```

**q₁q₂ ≠ q₂q₁** in general. Order matters for composed rotations.

## Inverse & Division

```
q⁻¹ = q* / ‖q‖²
```

For unit quaternions: `q⁻¹ = q*` (conjugate = inverse).

## Unit Quaternions

‖**q**‖ = 1. Lives on S³ ⊂ ℝ⁴. Every unit quaternion represents a 3D rotation.
Map S³ → SO(3) is 2-to-1: **q** and **−q** encode the same rotation.

## Rotating a Vector

`p' = q · (0, p) · q⨉`  (pure quaternion sandwich; extract x,y,z from result)

## Reference Files

| File | Load When |
|------|-----------|
| [references/formulas.md](references/formulas.md) | Full Hamilton product derivation, double-cover proof, rotation construction |
| [references/numerical-stability.md](references/numerical-stability.md) | Normalization drift, dot product sign, re-normalization strategies |
