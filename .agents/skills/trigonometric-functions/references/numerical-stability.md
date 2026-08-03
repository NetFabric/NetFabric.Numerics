# Trigonometric Functions — Numerical Stability

## arcsin/arccos Near ±1 (Cancellation)

When x ≈ ±1, `arcsin(x)` and `arccos(x)` change very rapidly:
```
d/dx arcsin(x) = 1 / √(1 − x²)  → ∞ as x → ±1
```

**Effect**: a small floating-point error in x causes large error in the result.
For x values known to be exactly ±1 (e.g., dot product of unit vectors), **clamp before calling**:
```csharp
x = Math.Clamp(x, -1.0, 1.0);
```

Without clamping, `arccos(1.0000000000000002)` returns NaN on most platforms.

## tan Near π/2 (Overflow)

`tan(π/2)` is undefined; the floating-point value of π/2 is slightly less than the mathematical value, so `Math.Tan(Math.PI / 2)` returns a very large finite number (~1.633e16), not ∞.

Do not expect ±Infinity; expect very large magnitude.

## atan2 vs atan: Precision Loss

`atan(y/x)` loses quadrant information and degrades near x ≈ 0:
- Division `y/x` overflows if |x| is tiny
- `atan2(y, x)` handles all quadrants and avoids the division

Always prefer `atan2` when both x and y are available.

## sin/cos Catastrophic Cancellation in Identities

`cos(2θ) = cos²θ − sin²θ` is unstable near θ ≈ π/4 where both terms are ≈ 0.5.
Use `cos(2θ) = 1 − 2sin²θ` or `2cos²θ − 1` to reduce cancellation depending on sign.

## SinCos Simultaneous Computation

Most platforms expose `sincos(θ)` or equivalent, computing both in one instruction.
Use it when both sin and cos of the same angle are needed (e.g., rotation matrix construction).

## Near-Zero tan for Large Arguments

For very large |θ| (millions of radians), floating-point representations of θ lose low-order
bits, making `sin(θ)` and `cos(θ)` meaningless. Reduce the angle before calling.

## arctan(1/x) for acot/asec/acsc

```
acot(x) = atan(1/x)    — breaks at x = 0 (acot(0) = π/2, not handled by atan(1/0) = ±Inf)
asec(x) = acos(1/x)    — x = 0 is undefined; |x| < 1 must be rejected
acsc(x) = asin(1/x)    — same constraints
```

Special-case x = 0 separately for `acot`.

## Expected Precision

| Function | Ulp error (typical) |
|----------|---------------------|
| sin, cos | ≤ 1 ulp (correctly rounded on most platforms) |
| tan | ≤ 1–2 ulp |
| atan2 | ≤ 1 ulp |
| arcsin, arccos near ±1 | can be many ulps if input has noise |
