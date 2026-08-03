---
description: >
  Expert in .NET 7+ generic math concepts and the System.Numerics interfaces.
  Guides implementation of numeric types that leverage static virtual methods in interfaces,
  arithmetic operators, and math function interfaces to write generic, type-safe mathematical code.
---

# Generic Math in .NET 7+

This skill covers the generic math concept introduced in .NET 7 and C# 11, which enables writing
generic algorithms that work with any numeric type. This is the foundation of the NetFabric.Numerics
library's strongly-typed, generic implementations.

## Core Concept

Generic math leverages C# 11's **static virtual members in interfaces** to define arithmetic operators
and mathematical functions at the interface level. This allows writing generic algorithms over any type
that implements the appropriate numeric interface.

## Key Interfaces in System.Numerics

### Operator Interfaces
- `IAdditionOperators<TSelf, TOther, TResult>` — defines the `+` operator
- `ISubtractionOperators<TSelf, TOther, TResult>` — defines the `-` operator
- `IMultiplicationOperators<TSelf, TOther, TResult>` — defines the `*` operator
- `IDivisionOperators<TSelf, TDivisor, TResult>` — defines the `/` operator
- `IModulusOperators<TSelf, TModulus, TResult>` — defines the `%` operator
- `IUnaryNegationOperators<TSelf, TResult>` — defines unary `-`
- `IUnaryPlusOperators<TSelf, TResult>` — defines unary `+`
- `IComparisonOperators<TSelf, TOther, TResult>` — defines `<`, `>`, `<=`, `>=`
- `IEqualityOperators<TSelf, TOther, TResult>` — defines `==`, `!=`

### Identity Interfaces
- `IAdditiveIdentity<TSelf, TResult>` — provides the additive identity (zero)
- `IMultiplicativeIdentity<TSelf, TResult>` — provides the multiplicative identity (one)

### Number Hierarchy Interfaces
- `INumber<TSelf>` — any numeric type
- `IFloatingPoint<TSelf>` — floating-point numeric type
- `IFloatingPointConstants<TSelf>` — provides `E`, `Pi`, `Tau` constants
- `IBinaryInteger<TSelf>` — binary integer type
- `ISignedNumber<TSelf>` — signed numeric type
- `IUnsignedNumber<TSelf>` — unsigned numeric type

### Math Function Interfaces
- `ITrigonometricFunctions<TSelf>` — `Sin`, `Cos`, `Tan`, `Asin`, `Acos`, `Atan`, etc.
- `IExponentialFunctions<TSelf>` — `Exp`, `Exp2`, `Exp10`
- `ILogarithmicFunctions<TSelf>` — `Log`, `Log2`, `Log10`
- `IPowerFunctions<TSelf>` — `Pow`
- `IRootFunctions<TSelf>` — `Sqrt`, `Cbrt`, `Hypot`
- `IHyperbolicFunctions<TSelf>` — `Sinh`, `Cosh`, `Tanh`, `Asinh`, `Acosh`, `Atanh`

### Conversion Interfaces
- `IMinMaxValue<TSelf>` — provides `MinValue` and `MaxValue`
- `INumberBase<TSelf>` — base interface for all number types, includes `CreateChecked`, `CreateSaturating`, `CreateTruncating`

## Guidance

### Writing Generic Algorithms

Constrain generic type parameters to the minimum required interfaces. Prefer specific operator
interfaces over the broad `INumber<T>` when possible:

```csharp
// Prefer this (minimal constraints):
static T Sum<T>(IEnumerable<T> source)
    where T : IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    var sum = T.AdditiveIdentity;
    foreach (var value in source)
        sum += value;
    return sum;
}

// Over this (overly broad constraints):
static T Sum<T>(IEnumerable<T> source) where T : INumber<T> { ... }
```

### Implementing Custom Numeric Types

When implementing custom numeric types, implement the relevant `System.Numerics` interfaces so
that the type integrates with generic algorithms:

```csharp
public readonly struct MyNumber
    : IAdditionOperators<MyNumber, MyNumber, MyNumber>,
      IAdditiveIdentity<MyNumber, MyNumber>,
      IMultiplicationOperators<MyNumber, MyNumber, MyNumber>,
      IMultiplicativeIdentity<MyNumber, MyNumber>
{
    public static MyNumber AdditiveIdentity => new(0);
    public static MyNumber MultiplicativeIdentity => new(1);
    public static MyNumber operator +(MyNumber left, MyNumber right) => new(left.Value + right.Value);
    public static MyNumber operator *(MyNumber left, MyNumber right) => new(left.Value * right.Value);
    // ...
}
```

### Replacing System.Math and System.MathF

In .NET 7+, prefer calling math functions directly on the type instead of using `System.Math`
or `System.MathF`:

```csharp
// Preferred in .NET 7+:
var sin = double.Sin(double.Pi);
var sqrt = float.Sqrt(2.0f);

// Avoid:
var sin = Math.Sin(Math.PI);
var sqrt = MathF.Sqrt(2.0f);
```

### Type Conversion

Use `CreateChecked`, `CreateSaturating`, or `CreateTruncating` from `INumberBase<T>` for
numeric conversions in generic code:

```csharp
static TResult Convert<TSource, TResult>(TSource value)
    where TSource : INumberBase<TSource>
    where TResult : INumberBase<TResult>
    => TResult.CreateChecked(value);
```

## Relevance to NetFabric.Numerics

The NetFabric.Numerics library uses generic math extensively:

- `Angle<TUnits, T>` uses `IFloatingPoint<T>` constraint so angles work with `float`, `double`, `Half`, or any future floating-point type.
- `Point<T>` and `Vector<T>` types use operator interfaces for arithmetic.
- Conversion methods like `CreateChecked`, `CreateSaturating`, and `CreateTruncating` follow the `INumberBase<T>` pattern.
- All implementations avoid `System.Math` / `System.MathF` in favor of calling methods on the constrained type parameter directly.

## References

- [Generic Math in .NET 7](https://learn.microsoft.com/en-us/dotnet/standard/generics/math)
- [System.Numerics namespace](https://learn.microsoft.com/en-us/dotnet/api/system.numerics)
- [Static abstract members in interfaces (C# 11)](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#static-abstract-members-in-interfaces)
- [docs/articles/GenericMath.md](../../docs/articles/GenericMath.md)
