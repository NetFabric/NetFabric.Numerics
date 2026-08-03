---
name: vector-algebra
description: Abstract math for vector algebra in 2D and 3D Euclidean space. USE FOR: implementing vector operations (add, subtract, negate, scalar multiply/divide); dot product and geometric interpretation; 3D cross product; 2D pseudo-cross product (scalar z-component); vector magnitude and normalization; angle between two vectors; linear interpolation; checking zero/unit/finite vectors. 2D is a special case of 3D with z=0. DO NOT USE FOR: quaternion operations (use quaternion-algebra); rotation representations (use 3d-rotation-theory); coordinate system conversions (use coordinate-system-conversions).
---

# Vector Algebra

`Point − Point = Vector` (displacement).  `Point + Vector = Point` (translation).

## Operations (component-wise)

| Operation | Formula | Result type |
|-----------|---------|-------------|
| Addition | **a** + **b** = (aₓ+bₓ, aᵧ+bᵧ[, a_z+b_z]) | Vector |
| Subtraction | **a** − **b** | Vector |
| Scalar multiply | k**v** = (kvₓ, kvᵧ[, kv_z]) | Vector |
| Scalar divide | **v**/k | Vector |
| Negation | −**v** = (−vₓ, −vᵧ[, −v_z]) | Vector |

## Magnitude & Normalization

```
‖v‖  = √(vₓ² + vᵧ² [+ v_z²])        (Euclidean norm)
‖v‖² = vₓ² + vᵧ² [+ v_z²]           (avoid sqrt when comparing)
v̂    = v / ‖v‖                         (unit vector; undefined if ‖v‖ = 0)
```

## Dot Product

```
a · b = aₓbₓ + aᵧbᵧ [+ a_z b_z] = ‖a‖ ‖b‖ cos θ
```

`a·b = 0` → perpendicular;  `a·b = ‖a‖‖b‖` → parallel;  sign → acute/obtuse.

## Cross Product (3D only)

```
a × b = (aᵧb_z − a_z bᵧ,  a_z bₓ − aₓb_z,  aₓbᵧ − aᵧbₓ)
‖a × b‖ = ‖a‖ ‖b‖ sin θ
```

Direction: right-hand rule. Anti-commutative: **a** × **b** = −(**b** × **a**)

**2D pseudo-cross** (scalar z-component only): `aₓbᵧ − aᵧbₓ` — positive = CCW, negative = CW.

## Angle Between Vectors

`θ = arccos(clamp(a·b / (‖a‖‖b‖), −1, 1))`  result ∈ [0, π]

## Special Vectors

| Name | Condition |
|------|-----------|
| Zero | ‖v‖ = 0 |
| Unit (normalized) | ‖v‖ = 1 |
| Basis 2D | UnitX=(1,0), UnitY=(0,1) |
| Basis 3D | UnitX=(1,0,0), UnitY=(0,1,0), UnitZ=(0,0,1) |

## Reference Files

| File | Load When |
|------|-----------|
| [references/formulas.md](references/formulas.md) | Projection, triple product, distance formulas, point operations |
| [references/numerical-stability.md](references/numerical-stability.md) | Near-zero normalization, dot cancellation, magnitude comparison |
