# Interpolation on Manifolds — Formulas

## Lerp Properties

```
lerp(a, b, 0) = a
lerp(a, b, 1) = b
lerp(a, a, t) = a           (same point)
lerp(a, b, t) = lerp(b, a, 1−t)    (reversible)
```

Extrapolation (t outside [0,1]) is mathematically valid; implementations may choose to clamp.

## Slerp Derivation

On the unit sphere, the geodesic (shortest path) between unit vectors **a** and **b** is the
great-circle arc. Parameterize by arc fraction t ∈ [0,1]:

```
Ω = arccos(a · b)           (total arc angle)

slerp(a, b, t) = sin((1−t)Ω) / sin(Ω) · a  +  sin(tΩ) / sin(Ω) · b
```

This follows from the spherical law of sines applied to the arc.

**Verification**: at t=0: `sin(Ω)/sin(Ω)·a + 0 = a`. At t=1: `0 + sin(Ω)/sin(Ω)·b = b`.

## Slerp Degenerate Cases

| Condition | Ω | Behavior |
|-----------|---|----------|
| a ≈ b | Ω ≈ 0 | sin(Ω) ≈ 0; fall back to lerp(a,b,t) |
| a ≈ −b | Ω ≈ π | Result undefined — infinitely many great circles |

For the antipodal case, choose an arbitrary orthogonal vector to define the interpolation plane.

## Nlerp vs Slerp: Error Bound

The angle error of Nlerp vs Slerp is bounded by:
```
|θ_nlerp − θ_slerp| ≤ C · Ω²
```
where C ≈ 0.125 and Ω is the arc angle. For Ω < 5°, the error is < 0.05° — acceptable for animation.

## Angular Lerp: Shortest-Difference Method

```
delta = b − a
// Reduce delta to (−Full/2, Full/2] — the signed shortest arc
if delta > Full/2:   delta -= Full
if delta < −Full/2:  delta += Full
result = a + t · delta
```

Example (degrees): a=350°, b=10°, t=0.5
- delta = 10 − 350 = −340
- Reduce: −340 + 360 = 20
- result = 350 + 0.5 · 20 = 360° → reduces to 0°  ✓

## Squad (Smooth Quaternion Spline, reference)

For smooth quaternion sequences (not constant velocity but smooth), squad uses:
```
squad(q₀, q₁, s₀, s₁, t) = slerp(slerp(q₀,q₁,t), slerp(s₀,s₁,t), 2t(1−t))
```
where s₀ and s₁ are auxiliary control quaternions computed from neighboring keyframes.
