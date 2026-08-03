# Trigonometric Functions — Formulas & Identities

## Reciprocal Definitions

```
cot θ = 1/tan θ = cos θ / sin θ
sec θ = 1/cos θ
csc θ = 1/sin θ
```

These are NOT independent functions — implement via their primary definitions to avoid divergence.

## Even/Odd Symmetry

| Function | Symmetry | Consequence |
|----------|----------|-------------|
| sin | Odd | sin(−θ) = −sin(θ) |
| cos | Even | cos(−θ) = cos(θ) |
| tan | Odd | tan(−θ) = −tan(θ) |
| cot | Odd | cot(−θ) = −cot(θ) |

## Periodicity

| Function | Period |
|----------|--------|
| sin, cos, sec, csc | 2π |
| tan, cot | π |

## Quadrant Sign Table

| Quadrant | sin | cos | tan |
|----------|-----|-----|-----|
| I (0, π/2) | + | + | + |
| II (π/2, π) | + | − | − |
| III (π, 3π/2) | − | − | + |
| IV (3π/2, 2π) | − | + | − |

Mnemonic: "All Students Take Calculus" — All positive, then Sin, Tan, Cos.

## Pythagorean Identities

```
sin²θ + cos²θ = 1
tan²θ + 1     = sec²θ       (divide first by cos²θ)
1 + cot²θ     = csc²θ       (divide first by sin²θ)
```

## Sum and Difference Formulas

```
sin(α ± β) = sin α cos β ± cos α sin β
cos(α ± β) = cos α cos β ∓ sin α sin β
tan(α ± β) = (tan α ± tan β) / (1 ∓ tan α tan β)
```

## Double Angle Formulas

```
sin(2θ) = 2 sin θ cos θ
cos(2θ) = cos²θ − sin²θ
        = 2cos²θ − 1
        = 1 − 2sin²θ
tan(2θ) = 2 tan θ / (1 − tan²θ)
```

## Half Angle Formulas

```
sin²(θ/2) = (1 − cos θ) / 2
cos²(θ/2) = (1 + cos θ) / 2
tan(θ/2)  = sin θ / (1 + cos θ)  =  (1 − cos θ) / sin θ
```

Half-angle formulas appear in quaternion construction: `sin(θ/2)` and `cos(θ/2)`.

## Inverse Trig: Input Domain Validation

| Function | Valid input range | What to do if outside |
|----------|------------------|-----------------------|
| arcsin(x) | [−1, 1] | Clamp or throw |
| arccos(x) | [−1, 1] | Clamp or throw |
| atan2(y, x) | x²+y² > 0 | Undefined at (0,0) — handle separately |

## atan2 Sign Behavior

```
atan2( y,  x)  →  (0, π]      when y ≥ 0
atan2( y,  x)  →  (−π, 0)     when y < 0
atan2(+0,  x) = 0   for x > 0
atan2(+0, −x) = +π  for x > 0
atan2(−0,  x) = −0  for x > 0
atan2(−0, −x) = −π  for x > 0
atan2( y,  0) = ±π/2  (sign matches y)
```

## Composition Rules

```
sin(arccos x) = √(1 − x²)     for x ∈ [−1, 1]
cos(arcsin x) = √(1 − x²)     for x ∈ [−1, 1]
arcsin(x) + arccos(x) = π/2   for x ∈ [−1, 1]
arctan(x) + arctan(1/x) = π/2 for x > 0
```
