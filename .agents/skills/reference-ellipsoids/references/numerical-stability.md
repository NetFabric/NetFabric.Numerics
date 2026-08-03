# Reference Ellipsoids — Numerical Stability

## Near-Sphere (e → 0)

When e ≈ 0, the surface area formula `(1−e²)/e · atanh(e)` has a 0/0 form.
Using Taylor expansion: `atanh(e)/e = 1 + e²/3 + e⁴/5 + ...` — evaluate as a series or use l'Hôpital.

Similarly, `M(φ) = a(1−e²)/W³` and `N(φ) = a/W` both equal `a` when e=0. No special case needed.

## W = √(1−e²sin²φ) Near Poles

At φ = 90° (pole), `sin²φ = 1`, so `W = √(1−e²)`. This is well-defined and > 0.
No singularity at poles — both M and N are finite.

At φ = 0° (equator): `W = 1`, so `N = a`, `M = a(1−e²)`. Clean.

## Eccentricity Computation Precision

`e² = f(2−f)` vs. `e² = 1−(b/a)²`:
- For small f (Earth ≈ 1/298), `f(2−f)` keeps more significant digits in the result
- `1−(b/a)²` subtracts two numbers very close to 1; use `f(2−f)` form

## Reciprocal Flattening 1/f

Standard ellipsoid tables give `1/f` (e.g., 298.257...), not f. When storing:
```csharp
double f = 1.0 / inverseFlattening;
```
For `1/f = 298.257`, `f ≈ 3.35e-3` — the rounding to double is negligible.

## Surface Area: atanh Precision

`atanh(e) = 0.5 · ln((1+e)/(1−e))`. For small e: `atanh(e) ≈ e + e³/3 + e⁵/5`.
For Earth (e ≈ 0.0818), the full formula is well-conditioned; no special treatment needed.

## Large-Scale vs. Precision Tradeoffs

For WGS84: a = 6,378,137.0 m. Squaring: a² ≈ 4.07e13.
In double precision (~15 significant digits), `a²` is exact to 1–2 metres.
This limits absolute precision of curvature radii to ≈ 0.001 mm — adequate for geodesy.
For sub-millimetre accuracy, use `decimal` or `double` with Kahan summation.
