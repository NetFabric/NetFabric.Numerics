# Reference Ellipsoids — Formulas

## Parameter Relationships

All ellipsoid parameters can be derived from any two independent ones. Most common choices:

| Given | Derive |
|-------|--------|
| a, f | b = a(1−f);  e² = f(2−f) |
| a, b | f = (a−b)/a;  e² = 1−(b/a)² |
| a, e | b = a√(1−e²);  f = 1−√(1−e²) |

## Eccentricity Derivation

```
e² = 1 − (b/a)² = 1 − (1−f)² = f(2−f) = 2f − f²
```

Since 0 ≤ f < 1: 0 ≤ e < 1. For Earth, e ≈ 0.0818.

## Radii of Curvature Derivation

The ellipse in the meridional plane: `x²/a² + z²/b² = 1`.

**Meridional radius M** (curvature in the north-south direction at latitude φ):
```
M(φ) = a(1−e²) / W³      where W = √(1 − e²sin²φ)
```

**Prime-vertical radius N** (curvature in the east-west direction):
```
N(φ) = a / W              where W = √(1 − e²sin²φ)
```

Note: N(φ) ≥ M(φ) always; N = M only for a sphere (e = 0).

Mean radius of curvature at latitude φ: `R(φ) = √(M(φ) · N(φ))`

## Surface Area Formula

```
S = 2πb²/e · ln((1+e)/(1−e)) + 2πb · a/e · arcsin(e)
  = 2πa² ( 1 + (1−e²)/e · atanh(e) )
```

For a sphere (e=0): `atanh(e)/e → 1`, so `S = 4πa²` — as expected.

## Volume

```
V = (4/3)π a² b
```

For a sphere (b=a): V = (4/3)πa³ — as expected.

## Third Flattening n (Helmert Series)

```
n = (a−b)/(a+b) = f/(2−f) ≈ f/2  for small f
```

Used in series expansions for arc lengths. For WGS84: n ≈ 1/600.

## Arc Length Along Meridian

Exact arc length from equator to latitude φ:
```
m(φ) = a(1−e²) ∫₀^φ dφ' / (1−e²sin²φ')^(3/2)
```

Approximated as a series in n (third flattening):
```
m(φ) ≈ a/(1+n) · (A₀φ − A₂sin(2φ) + A₄sin(4φ) − ...)
```
where A₀, A₂, ... are functions of n (Helmert 1880 series).

## Arithmetic Mean Radius

```
R₁ = (2a + b) / 3     (equal-surface approximation of mean Earth radius)
```

For WGS84: R₁ ≈ 6 371 008.8 m
