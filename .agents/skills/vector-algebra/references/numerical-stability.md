# Vector Algebra — Numerical Stability

## Near-Zero Normalization (Critical)

`v̂ = v / ‖v‖` is undefined when ‖v‖ = 0. Near zero:
- Division by a subnormal causes ‖v̂‖ >> 1
- Division by 0.0 produces ±Inf components

**Guard**: always check `‖v‖ > ε` before normalizing; return zero vector or signal error for degenerate input.

**Relative epsilon**: use `ε ≈ float.Epsilon * 4` or check `MagnitudeSquared > ε²`.

## Magnitude Comparison (Avoid Sqrt)

To compare two lengths: compare `MagnitudeSquared` values to avoid the cost and precision loss of `sqrt`.

```
‖a‖ < ‖b‖  ⟺  ‖a‖² < ‖b‖²      (all values ≥ 0, so squaring preserves order)
```

## Dot Product Cancellation

When **a** and **b** nearly parallel (same direction), `a · b ≈ ‖a‖‖b‖`; the identity `sin θ = ‖a×b‖ / (‖a‖‖b‖)` is more accurate for small angles.

When **a** ⊥ **b**, `a · b ≈ 0`; subtraction of nearly-equal numbers occurs in the component products. This is unavoidable but bounded by 1 ulp per term.

## arccos Argument Clamping (Angle Between)

`a · b / (‖a‖‖b‖)` may exceed [−1, 1] by tiny amounts due to rounding in magnitude computation.

Always clamp to [−1, 1] before calling arccos:
```csharp
double cosTheta = Math.Clamp(dot / (magA * magB), -1.0, 1.0);
```

## Catastrophic Cancellation in Cross Product

`aᵧb_z − a_z bᵧ`: when both products are nearly equal, up to N bits of precision can be lost.

For nearly-parallel vectors, use `‖a‖‖b‖ sin θ` formulation or higher-precision arithmetic.

## MagnitudeSquared Overflow

For components near `float.MaxValue / √2`, `vₓ² + vᵧ²` overflows.

If overflow is possible, scale the vector first: `v' = v / max(|vₓ|, |vᵧ|[, |v_z|])`, compute ‖v'‖, then scale back.

## IsNormalized Tolerance

Exact bit-equality `‖v‖ == 1.0` will fail for nearly all normalized vectors.
Use: `Math.Abs(MagnitudeSquared - 1.0) < ε` where ε is chosen per use case (e.g., 1e-6 for float, 1e-12 for double).

## NaN/Inf Propagation

- Any NaN component propagates through all operations.
- Addition of +Inf and −Inf components gives NaN.
- Normalizing a vector with Inf component: Inf/Inf = NaN.

Guard with `IsFinite` checks at system boundaries (inputs from users/files).
