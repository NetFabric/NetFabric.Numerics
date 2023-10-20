# NetFabric.Numerics.Angle: Strongly-Typed Angle Implementation

**NetFabric.Numerics.Angle** offers a robust, strongly-typed angle implementation.

> **Important:**
> Please be aware that **NetFabric.Numerics.Angle** leverages [generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math) capabilities, which are exclusive to .NET 7 and C# 11. Ensure that you are using a compatible version of the framework before integrating this library. If you're working with older versions of .NET, consider using [NetFabric.Angle](https://github.com/NetFabric/NetFabric.Angle) instead.

``` csharp
using NetFabric.Numerics.Angle;

// Create angles
var degreesDoubleAngle = new Angle<Degrees, double>(45.0);
var radiansFloatAngle = new Angle<Radians, float>(1.57f);
var gradiansDecimalAngle = new Angle<Gradians, decimal>(200.0m);
var revolutionsHalfAngle = new Angle<Revolutions, Half>((Half)0.25);

// Constants
var zeroDegreesDoubleAngle = Angle<Degrees, double>.Zero; // 0.0 degrees
var rightDegreesFloatAngle = Angle<Degrees, float>.Right; // 90.0f degrees
var straightRadiansDecimalAngle = Angle<Radians, decimal>.Straight; // π radians
var fullRevolutionsHalfAngle = Angle<Revolutions, Half>.Full; // 1 revolution

// Perform angle operations
var sum = degreesDoubleAngle + Angle<Degrees, double>.Right;
var difference = gradiansDecimalAngle - Angle<Gradians, decimal>.Right;
var product = 2.0 * degreesDoubleAngle;
var quotient = gradiansDecimalAngle / 100.0m;
var remainder = degreesDoubleAngle % 180.0;

// Compare angles
var areEqual = degreesDoubleAngle.Equals(Angle<Gradians, double>.Right);
var isGreater = gradiansDecimalAngle > Angle<Gradians, decimal>.Right;

// Convert angles
var convertedToRadians = Angle.ToRadians(degreesDoubleAngle);
var convertedToDegrees = Angle.ToDegrees(radiansFloatAngle);
var convertedToRevolution = Angle.ToRevolutions(degreesDoubleAngle);

var convertToFloatChecked = Angle<Degrees, float>.CreateChecked(degreesDoubleAngle); // throws if value is out of range
var convertToFloatSaturating = Angle<Degrees, float>.CreateSaturating(degreesDoubleAngle); // saturates if value is out of range
var convertToFloatTruncating = Angle<Degrees, float>.CreateTruncating(degreesDoubleAngle); // truncates if value is out of range

// Perform trigonometric calculations
var sineValue = Angle.Sin(radiansFloatAngle);
var cosineValue = Angle.Cos(Angle.ToRadians(degreesDoubleAngle));
var tangentValue = Angle.Tan(radiansFloatAngle);
var arcSineRadiansAngle = Angle.Asin(sineValue);

// Reduce angles
var reducedAngle = Angle.Reduce(degreesDoubleAngle);
var quadrant = Angle.GetQuadrant(reducedAngle);
var reference = Angle.GetReference(reducedAngle);

// Classify angles
var isZeroAngle = Angle.IsZero(reducedAngle);
var isAcuteAngle = Angle.IsAcute(reducedAngle);
var isRightAngle = Angle.IsRight(reducedAngle);
var isObtuseAngle = Angle.IsObtuse(reducedAngle);
var isStraightAngle = Angle.IsStraight(reducedAngle);

// Calculate collection operations
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

### Key Distinctions:

- **Angle<TUnits, T** represents an angle using a value of type **T** within the chosen **TUnits** unit. The **T** type can encompass various data types, such as **Half**, **float**, **double**, **decimal**, or any other implementation of **IFloatingPoint<TSelf>**.
- **AngleReduced<TUnits, T** represents an angle with a value of type **T** within the same **TUnits** unit but reduced to the range **[TUnits.Zero, TUnits.Full[**.

### Range Considerations

- **Angle<TUnits, T** has the capacity to represent any angle value within the range of **[T.MinValue, T.MaxValue]** within the specified **TUnits** unit. However, certain operations may necessitate reducing the angle to the range of **[TUnits.Zero, TUnits.Full[**. To optimize performance in such scenarios, consider employing **AngleReduced<TUnits, T**, which guarantees that the angle is already in a reduced form.

### Unit Conversion

To facilitate the versatile usage of angle values in different units, you can utilize methods like **ToDegrees()**, **ToGradians()**, **ToRadians()**, and **ToRevolutions()**. These methods offer a convenient way to convert angles from one unit to another, enabling you to adapt angle values to the specific requirements of your calculations.

### Data Type Conversion

Additionally, the angle library provides methods like **CreateChecked()**, **CreateSaturating()**, and **CreateTruncating()** to manage data type conversion. These methods cater to diverse scenarios, allowing you to choose between checked, saturating, or truncating conversions based on your specific requirements for data type conversion.

In conclusion, the choice between **Angle** and **AngleReduced** depends on the nature of your angle-related computations. Be mindful of their distinctions and use the one that best aligns with your specific needs for representing and comparing angles, managing units, handling data type conversions, and reducing the frequency of angle reduction operations.

## Angle classification

**NetFabric.Numerics.Angle** provides a large number of methods to classify an angle: `IsZero`, `IsAcute`, `IsRight`, `IsObtuse`, `IsStraight`, `IsReflex`, `IsOblique`, `AreComplementary`, `AreSupplementary`

These methods are only available for `AngleReduced<TUnits, T>`. When classifying a `Angle<TUnits, T>`, reduce it first by using `Angle.Reduce()`.

## Trigonometry

**NetFabric.Numerics.Angle** provides a large number of trigonometric methods: `Sin`, `Cos`, `Tan`, `Sec`, `Csc`, `Cot`, `Sinh`, `Cosh`, `Tanh`, `Sech`, `Csch`, `Coth`, `Asin`, `Acos`, `Atan`, `Acot`, `Asec`, `Acsc`

These methods are only available for angles in radians. When using an angle on any other unit, convert it first by using `Angle.ToRadians()`.

## Collections support

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