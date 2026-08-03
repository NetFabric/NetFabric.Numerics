---
name: angles-and-circular-arithmetic
description: Abstract math for angle measurement, unit systems, and circular arithmetic. USE FOR: implementing angle types; converting between degrees/radians/gradians/revolutions; reducing angles to canonical range [0, Full); computing reference angles and quadrants; implementing safe modular arithmetic with negative inputs; angle addition and subtraction; classifying angles by sign and size. DO NOT USE FOR: trigonometric functions (use trigonometric-functions); coordinate conversions (use coordinate-system-conversions); interpolation (use interpolation-on-manifolds).
---

# Angles & Circular Arithmetic

## Unit Systems

| Unit | Full (2π equiv) | Right | Straight |
|------|----------------|-------|---------|
| Degrees | 360 | 90 | 180 |
| Radians | 2π | π/2 | π |
| Gradians | 400 | 100 | 200 |
| Revolutions | 1 | 0.25 | 0.5 |

## Conversion Factors (multiply source by factor)

| From \ To | Degrees | Radians | Gradians | Revolutions |
|-----------|---------|---------|----------|-------------|
| Degrees | 1 | π/180 | 10/9 | 1/360 |
| Radians | 180/π | 1 | 200/π | 1/(2π) |
| Gradians | 9/10 | π/200 | 1 | 1/400 |
| Revolutions | 360 | 2π | 400 | 1 |

## Canonical Reduction to [0, Full)

```
reduced = θ − Full × ⌊θ / Full⌋
```

| Input (degrees) | Result |
|-----------------|--------|
| −30° | 330° |
| 0° | 0° |
| 360° | 0° |
| 720° | 0° |
| −360° | 0° |

## Quadrant Classification (after reduction to [0°, 360°))

| Range | Name |
|-------|------|
| θ = 0° | PositiveX axis |
| 0° < θ < 90° | First |
| θ = 90° | PositiveY axis |
| 90° < θ < 180° | Second |
| θ = 180° | NegativeX axis |
| 180° < θ < 270° | Third |
| θ = 270° | NegativeY axis |
| 270° < θ < 360° | Fourth |

## Reference Angle (acute angle to nearest x-axis)

| Quadrant | Formula |
|----------|---------|
| First | θ |
| Second | 180° − θ |
| Third | θ − 180° |
| Fourth | 360° − θ |

## Reference Files

| File | Load When |
|------|-----------|
| [references/formulas.md](references/formulas.md) | Unit conversion derivations, reduction algorithm, DMS format, arithmetic proofs |
| [references/numerical-stability.md](references/numerical-stability.md) | Mod with negatives, precision loss in conversion, large-angle reduction |
