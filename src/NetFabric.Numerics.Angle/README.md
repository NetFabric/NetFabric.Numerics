Certainly, let's restructure the README.md for **NetFabric.Numerics.Angle** following the same structure as the previous READMEs:

---

# NetFabric.Numerics.Angle

A Strongly-Typed Angle Implementation in C#.

## Key Features

- **Strongly-typed** angle implementation.
- Compatibility with **.NET 7** and **C# 11**.
- Comprehensive angular representations with **Angle** and **AngleReduced**.

## Introduction

Welcome to **NetFabric.Numerics.Angle** – a robust C# library that simplifies angle calculations with a strong typing approach.

## Getting Started

Ensure that you are working in a .NET 7 or higher environment to fully leverage the capabilities of **NetFabric.Numerics.Angle**. Once you're set up, explore the world of strongly-typed angles.

### Strongly-Typed Angle Creation

**NetFabric.Numerics.Angle** provides robust angle creation with strong typing:

```csharp
var degreesDoubleAngle = new Angle<Degrees, double>(45.0);
var radiansFloatAngle = new Angle<Radians, float>(1.57f);
var gradiansDecimalAngle = new Angle<Gradians, decimal>(200.0m);
var revolutionsHalfAngle = new Angle<Revolutions, Half>((Half)0.25);
```

### Essential Constants

Easily access essential angle constants:

```csharp
var zeroDegreesDoubleAngle = Angle<Degrees, double>.Zero;
var rightDegreesFloatAngle = Angle<Degrees, float>.Right;
var straightRadiansDecimalAngle = Angle<Radians, decimal>.Straight;
var fullRevolutionsHalfAngle = Angle<Revolutions, Half>.Full;
```

### Angle Operations

Perform various angle operations with precision:

```csharp
var sum = degreesDoubleAngle + Angle<Degrees, double>.Right;
var difference = gradiansDecimalAngle - Angle<Gradians, decimal>.Right;
var product = 2.0 * degreesDoubleAngle;
var quotient = gradiansDecimalAngle / 100.0m;
var remainder = degreesDoubleAngle % 180.0;
```

### Angle Comparison

Easily compare angles for your calculations:

```csharp
var areEqual = degreesDoubleAngle.Equals(Angle<Gradians, double>.Right);
var isGreater = gradiansDecimalAngle > Angle<Gradians, decimal>.Right;
```

### Angle Conversion

Convert angles to different units and data types:

```csharp
var convertedToRadians = Angle.ToRadians(degreesDoubleAngle);
var convertedToDegrees = Angle.ToDegrees(radiansFloatAngle);
var convertedToRevolution = Angle.ToRevolutions(degreesDoubleAngle);

var convertToFloatChecked = Angle<Degrees, float>.CreateChecked(degreesDoubleAngle);
var convertToFloatSaturating = Angle<Degrees, float>.CreateSaturating(degreesDoubleAngle);
var convertToFloatTruncating = Angle<Degrees, float>.CreateTruncating(degreesDoubleAngle);
```

### Trigonometric Calculations

Leverage trigonometric functions for your angle calculations:

```csharp
var sineValue = Angle.Sin(radiansFloatAngle);
var cosineValue = Angle.Cos(Angle.ToRadians(degreesDoubleAngle));
var tangentValue = Angle.Tan(radiansFloatAngle);
var arcSineRadiansAngle = Angle.Asin(sineValue);
```

### Angle Reduction

Understand the significance of angle reduction in your calculations:

```csharp
var reducedAngle = Angle.Reduce(degreesDoubleAngle);
var quadrant = Angle.GetQuadrant(reducedAngle);
var reference = Angle.GetReference(reducedAngle);
```

### Classifying Angles

Classify angles with methods like `IsZero`, `IsAcute`, `IsRight`, `IsObtuse`, `IsStraight`, and more:

```csharp
var isZeroAngle = Angle.IsZero(reducedAngle);
var isAcuteAngle = Angle.IsAcute(reducedAngle);
var isRightAngle = Angle.IsRight(reducedAngle);
var isObtuseAngle = Angle.IsObtuse(reducedAngle);
var isStraightAngle = Angle.IsStraight(reducedAngle);
```

### Collection Support

Optimized operations on collections of angles:

```csharp
var angleCollection = new[] { degreesDoubleAngle, Angle<Degrees, double>.Right, Angle<Degrees, double>.Straight };
var collectionSum = angleCollection.Sum();
var collectionAverage = angleCollection.Average();
```

## Angle vs. AngleReduced: Comprehensive Angular Representations

In the world of angular measurements, distinguishing between **Angle** and **AngleReduced** is pivotal. These two types serve unique purposes, offering a comprehensive approach to angular representation, units, and data type conversion.

### The Nature of Angles

Angles possess a periodic nature, cycling back to their original value after a full revolution, akin to a complete circle. When comparing two **Angle** instances, it's important to understand that, for performance reasons, this periodicity is not inherently considered. However, if your comparison demands accounting for this periodic nature, the angles should be reduced.

### AngleReduced: Embracing Periodicity and Unit Restrictions

**AngleReduced** is meticulously crafted to embrace the periodicity of angles while enforcing specific unit restrictions. However, one of its most significant advantages is the minimization of angle reduction:

- It represents an angle as a value of type **T** within the chosen **TUnits** unit.
- Crucially, **AngleReduced** ensures that the angle remains within the range of **[TUnits.Zero, TUnits.Full[**. This range restriction guarantees that two **AngleReduced** instances with the same value are considered equivalent. Notably, it's important to mention that **AngleReduced** can be implicitly converted to **Angle** when needed, offering flexibility in your angular computations.

### Reducing Reduction Frequency

A prominent benefit of **AngleReduced** is its ability to reduce the frequency of angle reduction operations. Reduction has to be explicitly performed only when required. The **AngleReduced** type also allows the compiler to know if reduction has already been performed, providing a clear indicator of the angle's status.

### Comparing Reduced Angles

When comparing angles, it's essential to choose the appropriate angle representation, whether **Angle** or **AngleReduced**, depending on the specific requirements of your calculations.

In conclusion, the choice between **Angle** and **AngleReduced** depends on the nature of your angle-related computations. Be mindful of their distinctions and use the one that best aligns with your specific needs for representing and comparing angles, managing units, handling data type conversions, and reducing the frequency of angle reduction operations.

## Angle Classification

**NetFabric.Numerics.Angle** provides a large number of methods to classify an angle: `IsZero`, `IsAcute`, `IsRight`, `IsObtuse`, `IsStraight`, `IsReflex`, `IsOblique`, `AreComplementary`, `AreSupplementary`

These methods are only available for `AngleReduced<TUnits, T>`. When classifying a `Angle<TUnits, T>`, reduce it first by using `Angle.Reduce()`.

## Trigonometry

**NetFabric.Numerics.Angle** provides a large number of trigonometric methods: `Sin`,

 `Cos`, `Tan`, `Sec`, `Csc`, `Cot`, `Sinh`, `Cosh`, `Tanh`, `Sech`, `Csch`, `Coth`, `Asin`, `Acos`, `Atan`, `Acot`, `Asec`, `Acsc`

These methods are only available for angles in radians. When using an angle on any other unit, convert it first by using `Angle.ToRadians()`.

## Collections Support

**NetFabric.Numerics.Angle** provides optimized operations on collections of angles: `Sum`, `Average`.

These operations are available for `IEnumerable<Angle<TUnits, T>>`, `Angle<TUnits, T>[]`, `Memory<Angle<TUnits, T>>`, `IReadOnlyList<Angle<TUnits, T>>`, `Span<Angle<TUnits, T>>`, and `ReadOnlySpan<Angle<TUnits, T>>`.

These operations use SIMD instructions when possible, ensuring high-performance calculations.

## Credits

The following open-source projects are used to build and test this project:

- [.NET](https://github.com/dotnet)
- [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet)
- [coverlet](https://github.com/coverlet-coverage/coverlet)
- [Fluent Assertions](https://github.com/fluentassertions/fluentassertions)
- [NetFabric.Hyperlinq.Analyzer](https://github.com/NetFabric/NetFabric.Hyperlinq.Analyzer)
- [xUnit](https://github.com/xunit/xunit)

## License

This project is licensed under the MIT license. See the [LICENSE](https://github.com/NetFabric/NetFabric.Numerics/blob/main/README.md) file for more info.