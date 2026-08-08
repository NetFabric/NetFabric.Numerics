# Patterns & Recipes

## Constraint Selection Guide

| Goal | Minimum constraint |
|------|-------------------|
| Generic sum / accumulation | `IAdditiveIdentity<T,T>, IAdditionOperators<T,T,T>` |
| Generic average | + `IDivisionOperators<T,T,T>` |
| Generic lerp | `INumber<T>` |
| Trig on value | `IFloatingPoint<T>, ITrigonometricFunctions<T>` |
| Any real number | `INumber<T>` |
| Float with Pi/E/Tau + trig | `IFloatingPointIeee754<T>` (implies trig & exp & log) |
| Unit/conversion constants | custom `IXxxUnits` with `static abstract` properties |

## Generic Algorithm Examples

```csharp
// Prefers narrow constraints — works with vectors, quaternions, angles, not just scalars
static T Sum<T>(ReadOnlySpan<T> values)
    where T : IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    var sum = T.AdditiveIdentity;
    foreach (var v in values) sum += v;
    return sum;
}

// T.CreateChecked converts the literal 2 to any numeric type safely
static T Lerp<T>(T a, T b, T t)
    where T : INumber<T>
    => a + (b - a) * t;

// Trig — use T.Sin instead of Math.Sin; works for Half, float, double
static T Sin<T>(T radians)
    where T : IFloatingPoint<T>, ITrigonometricFunctions<T>
    => T.Sin(radians);
```

## Defining a Static Abstract Interface (IAngleUnits pattern)

Use static abstract properties to encode domain constants at the type level — zero runtime cost.

```csharp
public interface IAngleUnits
{
    static abstract string Name     { get; }
    static abstract double Right    { get; }
    static abstract double Straight { get; }
    static abstract double Full     { get; }
}

public abstract class Degrees : IAngleUnits
{
    public static string Name      => "Degrees";
    public static double Right     => 90.0;
    public static double Straight  => 180.0;
    public static double Full      => 360.0;
}
```

Used as `where TUnits : IAngleUnits` — the JIT resolves values at specialization time, no allocation or virtual dispatch.

## Custom Type — Full Recipe

```csharp
[SkipLocalsInit]
[DebuggerDisplay("{Value}")]
[DebuggerTypeProxy(typeof(MyTypeDebugView<>))]
public readonly struct Angle<TUnits, T>
    : IEquatable<Angle<TUnits, T>>,
      IComparable<Angle<TUnits, T>>,
      IEqualityOperators<Angle<TUnits, T>, Angle<TUnits, T>, bool>,
      IComparisonOperators<Angle<TUnits, T>, Angle<TUnits, T>, bool>,
      IAdditiveIdentity<Angle<TUnits, T>, Angle<TUnits, T>>,
      IAdditionOperators<Angle<TUnits, T>, Angle<TUnits, T>, Angle<TUnits, T>>,
      IUnaryNegationOperators<Angle<TUnits, T>, Angle<TUnits, T>>,
      ISubtractionOperators<Angle<TUnits, T>, Angle<TUnits, T>, Angle<TUnits, T>>,
      IMultiplicativeIdentity<Angle<TUnits, T>, Angle<TUnits, T>>,
      IDivisionOperators<Angle<TUnits, T>, T, Angle<TUnits, T>>,
      IModulusOperators<Angle<TUnits, T>, T, Angle<TUnits, T>>,
      IMinMaxValue<Angle<TUnits, T>>,
      ISpanFormattable,
      ISpanParsable<Angle<TUnits, T>>
    where TUnits : IAngleUnits
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    public T Value { get; }
    public Angle(T value) { Value = value; }

    // --- static constants from unit type (resolved at JIT specialization)
    public static readonly Angle<TUnits, T> Full = new(T.CreateChecked(TUnits.Full));
    static Angle<TUnits, T> IAdditiveIdentity<Angle<TUnits,T>,Angle<TUnits,T>>.AdditiveIdentity
        => new(T.AdditiveIdentity);

    // --- cross-type construction (implement all three)
    public static Angle<TUnits, T> CreateChecked<TOther>(in Angle<TUnits, TOther> a)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(T.CreateChecked(a.Value));

    public static Angle<TUnits, T> CreateSaturating<TOther>(in Angle<TUnits, TOther> a)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(T.CreateSaturating(a.Value));

    public static Angle<TUnits, T> CreateTruncating<TOther>(in Angle<TUnits, TOther> a)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(T.CreateTruncating(a.Value));

    // --- operators
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle<TUnits, T> operator +(Angle<TUnits, T> l, Angle<TUnits, T> r)
        => new(l.Value + r.Value);
}
```

## Singleton Bridge Pattern

Static abstract members are compile-time only — they can't be accessed through `object` at runtime. Bridge via a lazy singleton that delegates:

```csharp
public abstract class AngleUnits           // runtime-accessible base (no generics)
{
    public abstract double Full { get; }
}

public sealed class AngleUnits<TUnits> : AngleUnits
    where TUnits : IAngleUnits
{
    static readonly Lazy<AngleUnits<TUnits>> s_instance = new(() => new());
    private AngleUnits() { }
    internal static AngleUnits<TUnits> Instance => s_instance.Value;

    public override double Full => TUnits.Full;   // delegates static abstract → virtual
}
```

Use when you need to store `AngleUnits` as a field, pass it as a non-generic parameter, or expose it through debugger views / serialization.

## Conversion Factor Pattern

Cache per-`(TUnits, T)` conversion ratios in private nested generic classes — the CLR creates one instance per `T`, so values are computed once and reused:

```csharp
// inside the static Angle helper class
static class DegreesInRadians<T> where T : IFloatingPoint<T>
{
    internal static readonly T Value = T.CreateChecked(180.0 / Math.PI);
}

public static Angle<Degrees, T> ToDegrees<T>(Angle<Radians, T> angle)
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    => new(angle.Value * DegreesInRadians<T>.Value);
```

## Performance Annotations

```csharp
[SkipLocalsInit]                                      // skips zero-init of stack locals
public readonly struct MyType<T> { ... }              // readonly → no defensive copies on in/ref readonly

[MethodImpl(MethodImplOptions.AggressiveInlining)]    // on every operator and hot-path method
public static MyType<T> operator +(MyType<T> l, MyType<T> r) => new(l.X + r.X, l.Y + r.Y);
```

- `SkipLocalsInit` requires `<AllowUnsafeBlocks>true</AllowUnsafeBlocks>` in the `.csproj`.
- `readonly struct` eliminates defensive copies when the struct is passed as `in` / `ref readonly`.
- `AggressiveInlining` lets the JIT eliminate the wrapper method for arithmetic operators.
- Use `[DebuggerDisplay]` + `[DebuggerTypeProxy]` so IDE watches show meaningful values.
