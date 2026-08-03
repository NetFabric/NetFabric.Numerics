---
name: trigonometric-functions
description: Abstract math for trigonometric functions: definitions, domains, ranges, key identities, and inverse functions. USE FOR: implementing sin/cos/tan/cot/sec/csc; implementing arcsin/arccos/arctan/atan2/acot/asec/acsc; validating input domains for inverse trig; applying Pythagorean and angle-sum identities; choosing atan vs atan2; precision near domain boundaries. DO NOT USE FOR: angle unit conversions (use angles-and-circular-arithmetic); coordinate system conversions (use coordinate-system-conversions); rotation representations (use 3d-rotation-theory).
---

# Trigonometric Functions

## Primary Functions

| Function | Definition | Domain | Range |
|----------|-----------|--------|-------|
| sin θ | opposite / hypotenuse | ℝ | [−1, 1] |
| cos θ | adjacent / hypotenuse | ℝ | [−1, 1] |
| tan θ | sin θ / cos θ | ℝ \ {π/2 + kπ} | ℝ |
| cot θ | cos θ / sin θ | ℝ \ {kπ} | ℝ |
| sec θ | 1 / cos θ | ℝ \ {π/2 + kπ} | (−∞,−1] ∪ [1,∞) |
| csc θ | 1 / sin θ | ℝ \ {kπ} | (−∞,−1] ∪ [1,∞) |

## Inverse Functions

| Function | Principal Range | Domain | Notes |
|----------|----------------|--------|-------|
| arcsin x | [−π/2, π/2] | [−1, 1] | Odd function |
| arccos x | [0, π] | [−1, 1] | arccos = π/2 − arcsin |
| arctan x | (−π/2, π/2) | ℝ | Odd; atan(0) = 0 |
| atan2(y,x) | (−π, π] | ℝ² \ {0,0} | Signs of both args determine quadrant |
| acot x | (0, π) | ℝ | acot(x) = arctan(1/x) for x > 0 |
| asec x | [0,π] \ {π/2} | \|x\| ≥ 1 | asec(x) = arccos(1/x) |
| acsc x | [−π/2,π/2] \ {0} | \|x\| ≥ 1 | acsc(x) = arcsin(1/x) |

## Key Identities

| Identity | Formula |
|----------|---------|
| Pythagorean | sin²θ + cos²θ = 1 |
| Pythagorean (tan) | 1 + tan²θ = sec²θ |
| Pythagorean (cot) | 1 + cot²θ = csc²θ |
| Angle sum (sin) | sin(α+β) = sin α cos β + cos α sin β |
| Angle sum (cos) | cos(α+β) = cos α cos β − sin α sin β |
| Double angle (sin) | sin(2θ) = 2 sin θ cos θ |
| Double angle (cos) | cos(2θ) = cos²θ − sin²θ = 1 − 2sin²θ |
| Reflection | sin(π−θ) = sin θ;  cos(π−θ) = −cos θ |
| Parity | sin(−θ) = −sin θ (odd);  cos(−θ) = cos θ (even) |

## atan2 vs atan

| Scenario | Use |
|----------|-----|
| Need full 4-quadrant angle from (y, x) | `atan2(y, x)` |
| x guaranteed positive | `atan(y/x)` |
| Computing angle between two vectors | `atan2(cross, dot)` |

## Reference Files

| File | Load When |
|------|-----------|
| [references/formulas.md](references/formulas.md) | Full identity table, reciprocal derivations, composition rules |
| [references/numerical-stability.md](references/numerical-stability.md) | Precision near ±1, atan2 singularities, cancellation in identities |
