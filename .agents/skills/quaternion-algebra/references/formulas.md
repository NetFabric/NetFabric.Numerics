# Quaternion Algebra — Formulas

## Hamilton Product — Compact Vector Form

Given **q₁** = (**v₁**, w₁) and **q₂** = (**v₂**, w₂):

```
q₁q₂ = (w₁v₂ + w₂v₁ + v₁×v₂,  w₁w₂ − v₁·v₂)
```

The cross product term `v₁×v₂` is what makes multiplication non-commutative.

## Algebraic Properties

```
q₁(q₂q₃) = (q₁q₂)q₃            (associative)
q₁q₂ ≠ q₂q₁                    (NOT commutative in general)
(q₁q₂)* = q₂* q₁*               (conjugate reverses order)
‖q₁q₂‖ = ‖q₁‖ · ‖q₂‖            (norm is multiplicative)
```

## Norm Preservation of Product

Because `‖q₁q₂‖ = ‖q₁‖·‖q₂‖`, the product of two unit quaternions is a unit quaternion.
This means unit quaternions are **closed** under multiplication — the set of rotations is a group.

## Inverse Formula

```
q⁻¹ = q* / ‖q‖²
```

Derivation: `q · q* = ‖q‖²` (scalar), so `q · (q*/‖q‖²) = 1`.

For unit q: `‖q‖ = 1`, so `q⁻¹ = q* = (−x, −y, −z, w)`.

## Double-Cover of SO(3)

The map `φ: S³ → SO(3)` defined by `φ(q)(p) = qpq*` is:
- **Surjective**: every rotation is represented
- **2-to-1**: `φ(q) = φ(−q)` — negating all four components gives the same rotation

Consequence: when interpolating or composing rotations, choose the hemisphere consistently (dot product check).

## Rotation Construction from Axis-Angle

Given unit axis **n** = (nₓ, nᵧ, n_z) and angle θ:

```
q = (sin(θ/2) · nₓ,  sin(θ/2) · nᵧ,  sin(θ/2) · n_z,  cos(θ/2))
```

Derivation: from the exponential map `exp(θ/2 · n̂)` in the Lie algebra of quaternions.

Note: **n** must be a unit vector before applying this formula.

## Rotation from Yaw-Pitch-Roll (Euler Angles)

Yaw (ψ around Z), Pitch (φ around Y), Roll (ρ around X), intrinsic ZYX order:

```
qψ = (0, 0, sin(ψ/2), cos(ψ/2))
qφ = (0, sin(φ/2), 0, cos(φ/2))
qρ = (sin(ρ/2), 0, 0, cos(ρ/2))

q = qψ · qφ · qρ         (apply roll first, then pitch, then yaw)
```

## Dot Product of Quaternions

```
q₁ · q₂ = x₁x₂ + y₁y₂ + z₁z₂ + w₁w₂
```

For unit quaternions: `q₁·q₂ = cos(Ω/2)` where Ω is the angle between the two rotations.

If `q₁·q₂ < 0`, then **−q₂** represents the same rotation but with a shorter interpolation arc.

## Rotating a Vector — Sandwich Product

```
p_rotated = q · (0, pₓ, pᵧ, p_z) · q*
```

Result is a pure quaternion (w = 0); extract (x, y, z) as the rotated vector.

This requires two quaternion multiplications. Converting to a rotation matrix is faster
if the same rotation is applied to many vectors.
