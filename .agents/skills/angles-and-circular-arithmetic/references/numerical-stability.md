# Angles — Numerical Stability

## Negative-Input Modulo (Critical)

In many languages, `%` is truncating remainder, not mathematical modulo:

| Language | `−30 % 360` | Correct? |
|----------|-------------|----------|
| C/C++/C# | −30 | No |
| Java | −30 | No |
| Python | 330 | Yes |
| JavaScript | −30 | No |

**Fix**: use floor-based formula:
```csharp
double reduced = angle - full * Math.Floor(angle / full);
```

Or equivalently: `((angle % full) + full) % full` — but this loses precision for large angles.

## Precision Loss in Unit Conversion

π is irrational; `Math.PI` is only a double approximation (~15 significant digits).

**Chain conversion amplifies error:** degrees → radians → degrees does NOT give the original value exactly.

**Round-trip rule**: convert directly between source and target unit; do not chain through radians as an intermediate unless required by the API.

**Example of accumulated error:**
```
360° → radians → gradians ≠ 400
```
The chain `degrees * (π/180) * (200/π)` has two floating-point multiplications vs. one for `degrees * (10/9)`. Use the direct factor.

## Large-Angle Reduction

When θ >> Full (e.g., angle accumulated over many rotations):

```csharp
// Catastrophic cancellation: θ = 1e15°, Full = 360
// floor(1e15 / 360) ≈ 2.78e12  — OK
// 1e15 − 360 * 2.78e12  — subtraction of nearly equal numbers loses low bits
```

For accumulated angles, carry a "full rotation counter" separately and reduce only the fractional part.

## Equality Comparison

`θ₁ == θ₂` using floating-point exact comparison fails at boundaries:
- 360.0 and 0.0 are mathematically equivalent after reduction but not bit-equal before it.
- Always reduce before comparing angles for angular equivalence.
- For approximate equality, use `|θ₁ − θ₂| < ε` on reduced values.

## IEEE 754 Special Values

| Input | Behavior |
|-------|---------|
| NaN | NaN propagates; reduction of NaN is NaN |
| +Inf / −Inf | `floor(Inf / Full)` is Inf; reduction of Inf is NaN |
| −0.0 | `floor(−0.0 / Full) = 0.0`; reduction gives 0.0 correctly |

Always guard for `IsFinite(θ)` before reducing.
