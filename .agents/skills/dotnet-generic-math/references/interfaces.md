# Interface Reference

Source: [learn.microsoft.com/dotnet/standard/generics/math](https://learn.microsoft.com/dotnet/standard/generics/math)

## Numeric Interfaces

| Interface | Description | Key APIs |
|-----------|-------------|---------|
| `INumberBase<T>` | Base for all number types | Zero, One, Radix; Create*; IsNaN, IsInfinity, IsInteger, IsZero |
| `INumber<T>` | Real number-like (signed & unsigned) | Clamp, CopySign, Max, Min, Sign |
| `ISignedNumber<T>` | Signed types | NegativeOne |
| `IUnsignedNumber<T>` | Unsigned types | — |
| `IBinaryNumber<T>` | Binary representation | AllBitsSet, IsPow2, Log2 |
| `IBinaryInteger<T>` | Binary integers | DivRem, LeadingZeroCount, PopCount, RotateLeft/Right, TrailingZeroCount |
| `IFloatingPoint<T>` | Floating-point | Ceiling, Floor, Round, Truncate |
| `IFloatingPointConstants<T>` | Float constants | E, Pi, Tau |
| `IFloatingPointIeee754<T>` | IEEE-754 float | Epsilon, NaN, ±Infinity, ±Zero; all function interfaces |
| `IBinaryFloatingPointIeee754<T>` | Binary IEEE-754 | `float`, `double`, `Half` only |
| `IAdditiveIdentity<T,R>` | Additive zero | AdditiveIdentity |
| `IMultiplicativeIdentity<T,R>` | Multiplicative one | MultiplicativeIdentity |
| `IMinMaxValue<T>` | Value range | MinValue, MaxValue |

Hierarchy (simplified):

```
INumberBase<T>
  ├─ INumber<T>
  │    ├─ ISignedNumber<T>     (float, double, Half, int, …)
  │    └─ IUnsignedNumber<T>   (byte, uint, …)
  └─ IBinaryNumber<T>
       └─ IBinaryInteger<T>    (byte, short, int, long, …)

IFloatingPoint<T>             (float, double, Half, decimal)
  └─ IFloatingPointIeee754<T> (float, double, Half)
       └─ IBinaryFloatingPointIeee754<T>
```

## Operator Interfaces

Each accepts generic `TOther` and `TResult` — input/result types may differ (e.g. `Angle<T> / T → Angle<T>`).

| Interface | Operator(s) |
|-----------|------------|
| `IAdditionOperators<T,O,R>` | `x + y` |
| `ISubtractionOperators<T,O,R>` | `x - y` |
| `IMultiplyOperators<T,O,R>` | `x * y` |
| `IDivisionOperators<T,O,R>` | `x / y` |
| `IModulusOperators<T,O,R>` | `x % y` |
| `IUnaryNegationOperators<T,R>` | `-x` |
| `IUnaryPlusOperators<T,R>` | `+x` |
| `IIncrementOperators<T>` | `++x`, `x++` |
| `IDecrementOperators<T>` | `--x`, `x--` |
| `IComparisonOperators<T,O,R>` | `<`, `>`, `<=`, `>=` |
| `IEqualityOperators<T,O,R>` | `==`, `!=` |
| `IBitwiseOperators<T,O,R>` | `&`, `\|`, `^`, `~` |
| `IShiftOperators<T,O,R>` | `<<`, `>>` |

Checked variants (e.g. `ISubtractionOperators` exposes `op_CheckedSubtraction`) apply in `checked` contexts; implement both if you implement either.

## Function Interfaces

All are implemented by `IFloatingPointIeee754<T>` (`float`, `double`, `Half`). May be used as standalone constraints for custom types.

| Interface | Functions |
|-----------|-----------|
| `ITrigonometricFunctions<T>` | Sin, Cos, Tan, Asin, Acos, Atan, Atan2, SinCos |
| `IExponentialFunctions<T>` | Exp, ExpM1, Exp2, Exp2M1, Exp10, Exp10M1 |
| `ILogarithmicFunctions<T>` | Log, Log2, Log10, LogP1, Log2P1, Log10P1 |
| `IPowerFunctions<T>` | Pow |
| `IRootFunctions<T>` | Sqrt, Cbrt, Hypot, RootN |
| `IHyperbolicFunctions<T>` | Sinh, Cosh, Tanh, Asinh, Acosh, Atanh |

Prefer `T.Sin(angle)` over `Math.Sin((double)angle)` — works with `Half`, avoids precision loss.

## Parsing & Formatting Interfaces

| Interface | Static members | Direction |
|-----------|---------------|-----------|
| `IParsable<T>` | `T.Parse(string, IFormatProvider)` / `TryParse` | string → T |
| `ISpanParsable<T>` | `T.Parse(ReadOnlySpan<char>, …)` / `TryParse` | span → T (zero-alloc) |
| `IFormattable` | `value.ToString(format, provider)` | T → string |
| `ISpanFormattable` | `value.TryFormat(Span<char>, …)` | T → span (zero-alloc) |

`ISpanParsable<T>` / `ISpanFormattable` are preferred in library code — no heap allocation.

## CRTP Requirement (CA2260)

All `INumber<T>`-family interfaces use the *curiously recurring template pattern*: `TSelf` must be the implementing type itself.

```csharp
// ✅ correct
public readonly struct MyAngle
    : IComparisonOperators<MyAngle, MyAngle, bool>,
      IParsable<MyAngle>

// ❌ CA2260 — wrong TSelf; static abstract members become inaccessible
public readonly struct MyAngle : IParsable<DateOnly>
```

Compiler emits CS0315 on use sites when TSelf is wrong; CA2260 catches it at definition.

## Built-in Type Coverage

| Type | `INumber` | `IFloatingPoint` | `IFloatingPointIeee754` | `IBinaryInteger` |
|------|:---------:|:----------------:|:-----------------------:|:----------------:|
| `int`, `long`, `short`, `byte`, … | ✓ | — | — | ✓ |
| `float`, `double`, `Half` | ✓ | ✓ | ✓ | — |
| `decimal` | ✓ | ✓ | — | — |
| `char`, `DateTime`, `TimeSpan` | partial | — | — | — |

`Half` supports full `IFloatingPointIeee754<T>` including trig functions since .NET 7.
