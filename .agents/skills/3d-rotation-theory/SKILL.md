---
name: 3d-rotation-theory
description: Abstract math for 3D rotation representations and conversions. USE FOR: understanding axis-angle representation; Euler angles (yaw/pitch/roll, convention choices); rotation matrices (SO(3)); quaternion-based rotation; converting between representations; identifying and avoiding gimbal lock; composing rotations; half-angle formula; right-hand rule. DO NOT USE FOR: quaternion arithmetic details (use quaternion-algebra); slerp/lerp interpolation (use interpolation-on-manifolds); coordinate system definitions (use coordinate-system-conversions).
---

# 3D Rotation Theory

SO(3) = group of 3×3 orthogonal matrices with det = +1. Every rotation has an inverse (transpose).

## Representations

| Representation | Parameters | Strengths | Weaknesses |
|----------------|-----------|-----------|-----------|
| Axis-angle | axis **n̂** + angle θ | Intuitive, minimal | Not closed under composition |
| Rotation matrix | 3×3, 9 numbers | Fast multi-vector transform | 9 params, orthogonality drift |
| Euler angles | 3 angles (many conventions) | Human-readable | Gimbal lock, convention ambiguity |
| Quaternion | 4 numbers, unit | Stable interpolation, fast compose | Double-cover ambiguity |

## Axis-Angle (Rodrigues Formula)

Rotation by θ around unit **n̂** = (nₓ, nᵧ, n_z):
```
v' = v cos θ + (n̂ × v) sin θ + n̂(n̂·v)(1 − cos θ)
```
Right-hand rule: thumb along **n̂**, fingers curl in positive rotation direction.

## Euler Angles (ZYX Intrinsic = Yaw-Pitch-Roll)

Yaw ψ (Z), Pitch φ (Y), Roll ρ (X) — most common aerospace/robotics convention:

```
R = Rz(ψ) · Ry(φ) · Rx(ρ)        applied right-to-left: roll first, yaw last
```

## Gimbal Lock

When pitch = ±90° (second rotation hits a pole), yaw and roll become degenerate —
one degree of freedom is lost. Avoid for systems requiring full orientation control.

Quaternions and rotation matrices do NOT suffer gimbal lock.

## Quaternion ↔ Axis-Angle

Axis-angle → quaternion (load [quaternion-algebra](../quaternion-algebra/SKILL.md)):
```
q = (sin(θ/2)·nₓ, sin(θ/2)·nᵧ, sin(θ/2)·n_z, cos(θ/2))
```

Quaternion → axis-angle:
```
θ = 2 arccos(w)
n̂ = (x, y, z) / sin(θ/2)        (undefined when θ = 0; use n̂ = (0,0,1) by convention)
```

## Composition Order (Right-to-Left)

`R_total = R_second · R_first` applies R_first first, then R_second.
For quaternions: `q_total = q_second · q_first` (same convention).

## Reference Files

| File | Load When |
|------|-----------|
| [references/formulas.md](references/formulas.md) | Full rotation matrix for each Euler sequence, Rodrigues formula expansion, quaternion↔matrix conversion |
| [references/numerical-stability.md](references/numerical-stability.md) | Orthogonality drift, gimbal lock detection, near-zero axis extraction |
