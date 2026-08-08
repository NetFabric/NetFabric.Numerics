# 3D Rotation Theory — Formulas

## Elementary Rotation Matrices

```
Rx(ρ) = ⎡ 1    0      0   ⎤
         ⎢ 0  cos ρ  −sin ρ ⎥
         ⎣ 0  sin ρ   cos ρ ⎦

Ry(φ) = ⎡  cos φ  0  sin φ ⎤
         ⎢   0     1   0   ⎥
         ⎣ −sin φ  0  cos φ ⎦

Rz(ψ) = ⎡ cos ψ  −sin ψ  0 ⎤
         ⎢ sin ψ   cos ψ  0 ⎥
         ⎣  0       0     1 ⎦
```

## ZYX (Yaw-Pitch-Roll) Combined Matrix

R = Rz(ψ) · Ry(φ) · Rx(ρ):

```
R = ⎡ cψcφ   cψsφsρ−sψcρ   cψsφcρ+sψsρ ⎤
    ⎢ sψcφ   sψsφsρ+cψcρ   sψsφcρ−cψsρ ⎥
    ⎣ −sφ      cφsρ            cφcρ     ⎦
```
where c = cos, s = sin.

## Rotation Matrix ↔ Quaternion

Matrix to quaternion (Shepperd's method — numerically stable):
```
trace = R[0,0] + R[1,1] + R[2,2]

if trace > 0:
    s = 0.5 / √(trace + 1)
    w = 0.25 / s
    x = (R[2,1] − R[1,2]) · s
    y = (R[0,2] − R[2,0]) · s
    z = (R[1,0] − R[0,1]) · s
else: (branch on largest diagonal element to avoid near-zero divisor)
    ...
```

Quaternion to matrix:
```
R = ⎡ 1−2(y²+z²)   2(xy−wz)    2(xz+wy) ⎤
    ⎢ 2(xy+wz)    1−2(x²+z²)   2(yz−wx) ⎥
    ⎣ 2(xz−wy)    2(yz+wx)    1−2(x²+y²) ⎦
```
(valid only for unit quaternions)

## Quaternion → Euler Angles (ZYX)

```
ρ = atan2(2(wx + yz),  1 − 2(x² + y²))    (roll)
φ = arcsin(2(wy − zx))                     (pitch, clamped to [−π/2, π/2])
ψ = atan2(2(wz + xy),  1 − 2(y² + z²))    (yaw)
```

Gimbal lock occurs when `2(wy − zx) = ±1` (pitch = ±90°).

## Euler Angles → Quaternion (ZYX)

```
qψ = (0, 0, sin(ψ/2), cos(ψ/2))
qφ = (0, sin(φ/2), 0, cos(φ/2))
qρ = (sin(ρ/2), 0, 0, cos(ρ/2))
q  = qψ · qφ · qρ
```

Expanded:
```
x = cos(ψ/2)cos(φ/2)sin(ρ/2) − sin(ψ/2)sin(φ/2)cos(ρ/2)
y = cos(ψ/2)sin(φ/2)cos(ρ/2) + sin(ψ/2)cos(φ/2)sin(ρ/2)
z = sin(ψ/2)cos(φ/2)cos(ρ/2) − cos(ψ/2)sin(φ/2)sin(ρ/2)
w = cos(ψ/2)cos(φ/2)cos(ρ/2) + sin(ψ/2)sin(φ/2)sin(ρ/2)
```

## Rodrigues Rotation Formula

Rotate vector **v** by angle θ around unit axis **n̂**:
```
v' = v cos θ + (n̂ × v) sin θ + n̂(n̂·v)(1 − cos θ)
```

This is equivalent to the rotation matrix formula and does not require building a matrix.

## Inverse Rotation

- Matrix: R⁻¹ = Rᵀ (transpose, not general inverse)
- Quaternion: q⁻¹ = q* (conjugate, for unit q)
- Axis-angle: negate θ (or negate **n̂**)
- Euler (ZYX): negate each angle and apply in reverse order XYZ
