# Helmert Datum Transformation — Formulas

## Unit Conversions for Rotation Parameters

Rotation parameters are published in arcseconds; convert before use:
```
r_rad = R_arcsec × (π / (180 × 3600))
      = R_arcsec × 4.84814e-6  rad/arcsec
```

Scale in ppm → dimensionless:
```
s = 1 + ΔS × 10⁻⁶
```

## Full Rotation Matrix Form

For large rotations (not small-angle approximation), use exact rotation matrix R = Rz·Ry·Rx:

```
⎡X'⎤   ⎡ΔX⎤       ⎡X⎤
⎢Y'⎥ = ⎢ΔY⎥ + s·R ⎢Y⎥
⎣Z'⎦   ⎣ΔZ⎦       ⎣Z⎦
```

For datum shifts, rotations are always tiny (< 10 arcsec ≈ 5×10⁻⁵ rad), so the linearized form
is indistinguishable from the full form at double precision.

## Linearized Rotation Matrix

```
R ≈ I + [ω]×     where [ω]× = ⎡  0   R_z  −Rᵧ⎤
                               ⎢−R_z   0    Rₓ ⎥
                               ⎣  Rᵧ  −Rₓ   0  ⎦
```

(Position vector convention; coordinate frame convention negates the off-diagonal elements)

## Composition of Two Transformations

To go A → B → C using parameters (ΔX₁, ...) for A→B and (ΔX₂, ...) for B→C:

For small rotations:
```
ΔX_total = ΔX₁ + ΔX₂ (approximately; exact only if rotations commute)
R_total  ≈ R₁ + R₂   (linearized; higher-order cross-terms neglected)
s_total  = s₁ · s₂   (exact)
```

For practical datum work, go through WGS84 as a hub rather than composing large chains.

## Geocentric ↔ Geodetic

The Helmert transform operates on geocentric Cartesian (X, Y, Z) coordinates.
To apply it to geodetic (lat, lon, h):
1. Convert (lat, lon, h) → (X, Y, Z) using ellipsoid parameters
2. Apply Helmert transform
3. Convert (X', Y', Z') → (lat', lon', h') using target ellipsoid

Geocentric → Geodetic (Bowring iterative):
```
p = √(X²+Y²)
θ = atan2(Z·a, p·b)          (initial approximation)
lat = atan2(Z + e'²b·sin³θ, p − e²a·cos³θ)   (iterate until convergence)
lon = atan2(Y, X)
h = p/cos(lat) − N(lat)
```

## EPSG vs. Bursa-Wolf Sign Convention

EPSG "Coordinate Frame" rotation: the rotation matrix rotates the coordinate frame.
```
R_EPSG = I + [−ω]×   (opposite sign from position vector convention)
```

When applying parameters from EPSG registry, verify the method code:
- EPSG method 1032 = Coordinate Frame
- EPSG method 1033 = Position Vector (Bursa-Wolf)
