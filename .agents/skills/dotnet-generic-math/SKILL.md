---
name: dotnet-generic-math
description: C# 11+ generic math with static abstract interface members in System.Numerics. USE FOR: writing generic numeric algorithms; constraining type parameters to INumber<T>/IFloatingPoint<T>/ITrigonometricFunctions<T>; implementing custom numeric types with operator overloads; defining static abstract interfaces (IAngleUnits pattern); CreateChecked/Saturating/Truncating cross-type conversions; AdditiveIdentity/MultiplicativeIdentity; singleton bridge pattern for static abstract at runtime; ISpanFormattable/ISpanParsable; generic Sum/Average/Lerp methods; Half/float/double/decimal compatibility. DO NOT USE FOR: Unity math; pre-.NET 7 System.Math/MathF workarounds; Python/JavaScript numerics.
---

# .NET Generic Math

Requires .NET 7+ / C# 11+. `using System.Numerics;`

## Constraint Selection

| Constraint | T must support | Example built-in types |
|-----------|----------------|----------------------|
| `INumber<T>` | +, -, *, /, %, compare, Clamp, Sign | `int`, `float`, `double`, `decimal`, `Half` |
| `IFloatingPoint<T>` | Ceiling, Floor, Round, Truncate | `float`, `double`, `Half`, `decimal` |
| `IFloatingPointIeee754<T>` | Pi, E, Tau, NaN, ±Infinity | `float`, `double`, `Half` |
| `ITrigonometricFunctions<T>` | Sin, Cos, Tan, Asin, Acos, Atan2 | `float`, `double`, `Half` |
| `IBinaryInteger<T>` | bit ops, DivRem, PopCount, rotate | `byte`–`long`, `nint`, `uint`–`ulong` |
| `IMinMaxValue<T>` | `T.MinValue` / `T.MaxValue` | all built-in numerics |
| `ISignedNumber<T>` | `T.NegativeOne` | signed int & float types |

`INumber<T>` includes `decimal` & integers; `IFloatingPointIeee754<T>` is float-only and implies `ITrigonometricFunctions<T>`.

## CreateChecked / CreateSaturating / CreateTruncating

| Method | On overflow |
|--------|------------|
| `T.CreateChecked(x)` | Throws `OverflowException` |
| `T.CreateSaturating(x)` | Clamps to `T.MinValue` / `T.MaxValue` |
| `T.CreateTruncating(x)` | Wraps (lowest bits) |

For IEEE-754 float types, all three clamp to `±Infinity` (never throw).

## Static Abstract Access Pattern

```csharp
static T Sum<T>(ReadOnlySpan<T> values)
    where T : IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    var sum = T.AdditiveIdentity;       // access via T, not via the interface directly (CS8926)
    foreach (var v in values) sum += v;
    return sum;
}
```

## Custom Type (minimum viable)

```csharp
public readonly record struct Vec2<T>(T X, T Y)
    : IAdditiveIdentity<Vec2<T>, Vec2<T>>,
      IAdditionOperators<Vec2<T>, Vec2<T>, Vec2<T>>
    where T : struct, INumber<T>
{
    public static Vec2<T> AdditiveIdentity => new(T.AdditiveIdentity, T.AdditiveIdentity);
    public static Vec2<T> operator +(Vec2<T> l, Vec2<T> r) => new(l.X + r.X, l.Y + r.Y);
}
```

## Prefer Type Members Over Math / MathF

**Never use `Math` or `MathF`.** Since .NET 7, all constants and methods are available as static members on the numeric types themselves, which also work in generic contexts.

| Instead of | Use |
|------------|-----|
| `Math.PI` | `double.Pi` |
| `MathF.PI` | `float.Pi` |
| `Math.Floor(x)` | `double.Floor(x)` |
| `MathF.Floor(x)` | `float.Floor(x)` |
| `Math.Abs(x)` | `T.Abs(x)` (generic) |
| `Math.Sin(x)` | `T.Sin(x)` (generic) |
| `Math.Sqrt(x)` | `T.Sqrt(x)` (generic) |
| `Math.Max(a, b)` | `T.Max(a, b)` (generic) |

In generic code, call the method on `T` (e.g., `T.Sin(x)`); for concrete `double`/`float` code, call on the type (`double.Sin(x)`).

**Always match the constant or method to the type of the value being computed.** Mixing types causes silent implicit conversions.

| `double` | `float` | `int` / `long` / … |
|----------|---------|---------------------|
| `double.Pi` | `float.Pi` | — |
| `double.Tau` | `float.Tau` | — |
| `double.E` | `float.E` | — |
| `double.MinValue` | `float.MinValue` | `int.MinValue`, `long.MinValue` |
| `double.MaxValue` | `float.MaxValue` | `int.MaxValue`, `long.MaxValue` |
| `double.Pow(x, y)` | `float.Pow(x, y)` | — |
| `double.Sqrt(x)` | `float.Sqrt(x)` | — |
| `double.Log(x)` | `float.Log(x)` | — |
| `double.Abs(x)` | `float.Abs(x)` | `int.Abs(x)`, `long.Abs(x)` |
| `double.Max(a, b)` | `float.Max(a, b)` | `int.Max(a, b)`, `long.Max(a, b)` |
| `double.Clamp(v,min,max)` | `float.Clamp(…)` | `int.Clamp(…)`, `long.Clamp(…)` |

In generic code use `T.Pi`, `T.Abs(x)`, `T.Max(a, b)`, etc. — the compiler resolves to the correct type automatically.

## Reference Files

| File | Load When |
|------|-----------|
| [references/interfaces.md](references/interfaces.md) | Full operator/function/parsing interface tables; CRTP rule; built-in type coverage |
| [references/patterns.md](references/patterns.md) | Custom type recipe; static abstract interface definition; singleton bridge; performance annotations |
