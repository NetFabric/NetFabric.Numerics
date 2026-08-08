# Angles — Formulas & Derivations

## Unit Conversion Derivation

All conversions pass through revolutions as a normalized intermediate:

```
revolutions = θ_source / Full_source
θ_target    = revolutions × Full_target
```

Combined: `θ_target = θ_source × (Full_target / Full_source)`

This gives the conversion table in SKILL.md. Example: degrees → radians = 360/2π is wrong;
correct factor is 2π/360 = π/180.

## Modular Reduction — Why Floor Division

The canonical range is [0, Full) (closed at 0, open at Full).

Using truncating division (C `%`, many languages) gives wrong results for negatives:
- `−30 % 360 = −30`  ← wrong; should be 330

Using floor division (Python `%`, or explicit formula) is correct:
```
reduced = θ − Full × floor(θ / Full)
```

Mathematical basis: θ mod Full using the floor function maps every real θ to [0, Full).

Proof that result is in [0, Full):
- Let k = floor(θ / Full), then k ≤ θ/Full < k+1
- Multiply by Full: k·Full ≤ θ < (k+1)·Full
- Subtract k·Full: 0 ≤ θ − k·Full < Full ✓

## Degrees-Minutes-Seconds (DMS) Format

```
d° m' s"  where  0 ≤ m < 60,  0 ≤ s < 60
```

Conversion to decimal degrees:
```
decimal = d + m/60 + s/3600
```

Conversion from decimal degrees:
```
d = trunc(decimal)
m = trunc((decimal − d) × 60)
s = ((decimal − d) × 60 − m) × 60
```

Validation constraints: m ∈ [0, 60), s ∈ [0, 60). Any value outside this range is invalid.

## Reference Angle — Full Derivation

Given reduced angle θ ∈ [0°, 360°):

| Quadrant | θ range | Reference = shortest arc to x-axis |
|----------|---------|-------------------------------------|
| First | [0°, 90°) | θ itself |
| PositiveY | 90° | 90° (on axis) |
| Second | (90°, 180°) | 180° − θ |
| NegativeX | 180° | 0° (on axis) |
| Third | (180°, 270°) | θ − 180° |
| NegativeY | 270° | 90° (on axis) |
| Fourth | (270°, 360°) | 360° − θ |

Reference angle is always in [0°, 90°].

## Angle Arithmetic

Addition is defined by closure on the unit: `θ₁ + θ₂` stays in the same unit.
The result is NOT automatically reduced; reduction is a separate explicit step.

`θ_sum = θ₁ + θ₂`  (unrestricted, may exceed Full or be negative)

Negation: `−θ` is valid (represents opposite direction).

Scalar multiplication: `k × θ` scales the angle magnitude; unit is preserved.

Subtraction: `θ₁ − θ₂` computes the signed difference (may be negative).
