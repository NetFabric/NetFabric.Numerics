---
name: helmert-datum-transformation
description: "Abstract math for the 7-parameter (Helmert) similarity transformation used to convert coordinates between geodetic datums. USE FOR: implementing datum-to-datum 3D coordinate transformations; understanding the 7 Helmert parameters (3 translations, 3 rotations, 1 scale); applying the linearized vs. full rotation matrix form; composing transformations; transforming to/from WGS84. DO NOT USE FOR: geodetic coordinate validation (use geodetic-coordinate-bounds); reference ellipsoid parameters (use reference-ellipsoids); map projections."
---

# Helmert Datum Transformation

## Definition

The 7-parameter Helmert transformation converts geocentric Cartesian coordinates (X, Y, Z)
from one datum to another. Also called similarity transformation or Bursa-Wolf model.

Parameters:
| Symbol | Name | Unit |
|--------|------|------|
| ΔX, ΔY, ΔZ | Translation (origin shift) | metres |
| Rₓ, Rᵧ, R_z | Rotation (small angles) | arcseconds (or radians) |
| ΔS | Scale factor change | ppm (parts per million) |

## Linearized Formula (Small Rotations)

When Rₓ, Rᵧ, R_z << 1 radian (typically < 10 arcsec for datum shifts):

```
⎡X'⎤   ⎡ΔX⎤          ⎡  1    R_z  −Rᵧ⎤ ⎡X⎤
⎢Y'⎥ = ⎢ΔY⎥ + (1+ΔS) ⎢−R_z   1    Rₓ ⎥ ⎢Y⎥
⎣Z'⎦   ⎣ΔZ⎦          ⎣  Rᵧ  −Rₓ   1  ⎦ ⎣Z⎦
```

ΔS is the fractional scale: ΔS = 0 means no scale change. Applied as `(1 + ΔS × 10⁻⁶)` when given in ppm.

## Inverse Transformation

To reverse the transformation (source ← target), negate all 7 parameters:
```
(−ΔX, −ΔY, −ΔZ, −Rₓ, −Rᵧ, −R_z, −ΔS)
```

This is exact for the linearized form. For the full rotation matrix form, invert the rotation matrix.

## WGS84 as Reference

WGS84 geocentric coordinates are the common hub. Typical workflow:
```
Source datum → WGS84 → Target datum
```

WGS84 ↔ WGS72 parameters are defined in the WGS84 standard. For national grids, parameters are published by the national geodetic authority (e.g., EPSG registry).

## Sign Convention

Two conventions exist — confirm which is in use:
1. **Coordinate Frame** (used by EPSG): rotations rotate the coordinate frame
2. **Position Vector** (Bursa-Wolf): rotations rotate the position vector

The rotation matrix entries swap sign between conventions. The parameters are numerically identical but with opposite rotation signs.

## Reference Files

| File | Load When |
|------|-----------|
| [references/formulas.md](references/formulas.md) | Full rotation matrix form, ppm conversion, composition, inverse |
| [references/numerical-stability.md](references/numerical-stability.md) | Angle unit conversion, scale near 1, ppm precision |
