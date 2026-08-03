# Helmert Datum Transformation — Numerical Stability

## Scale Factor Near 1.0

The scale factor `s = 1 + ΔS × 10⁻⁶` is very close to 1 (|ΔS| typically < 10 ppm for geodetic datums).
Multiplying coordinates by s vs. 1.0 changes results by a few millimetres at Earth scale (6×10⁶ m).

**Pitfall**: storing s as float instead of double loses the ppm precision entirely.
Use double (or higher) for all scale and translation parameters.

## Rotation Angle Precision

Rotation parameters are often given to 4 decimal places in arcseconds:
```
1 arcsec = π/(180×3600) ≈ 4.848e-6 rad
0.001 arcsec ≈ 4.848e-9 rad
```

Double precision has ~15 significant digits, so 0.001 arcsec is representable accurately.
Float (7 digits) loses the last 2 decimal places — not suitable for geodetic work.

## Linearized vs. Full: When It Matters

For Earth datums: max rotation ≈ 5 arcsec = 2.4×10⁻⁵ rad.
```
sin(2.4e-5) ≈ 2.4e-5    (linearized)
cos(2.4e-5) ≈ 1 − 2.9e-10  (linearized ≈ 1)
```
At Earth radius (6×10⁶ m), the difference between linearized and full rotation matrix is:
`6×10⁶ × (2.4e-5)² / 2 ≈ 1.7 mm`

For sub-millimetre geodesy, use the full rotation matrix. For metre-level, linearized is fine.

## Inverse: Parameter Negation

Negating parameters gives the exact inverse only for the linearized form.
For the full matrix: `R⁻¹ = Rᵀ` (orthogonal matrix), not `−R`.

## Convention Error (Silent Correctness Bug)

Using EPSG Coordinate Frame parameters with Bursa-Wolf formula (or vice versa) produces
errors up to ~2× the rotation magnitude. For 5 arcsec rotation: ~3 cm error.
This is within-range plausible and hard to detect without a test point.

Always document the convention used with the parameter set.

## Translation Significance

Translations ΔX, ΔY, ΔZ are in metres. For typical datum shifts:
- Regional datums: 100–1000 m translations are common
- WGS84 origin is Earth's centre of mass; national datums can be offset by hundreds of metres

At double precision, metre-level translations on 6×10⁶ m coordinates cause no precision loss
(15 significant digits >> 7 significant digits needed for sub-mm accuracy).
