# NetFabric.Numerics.Angle

**NetFabric.Numerics.Angle** provides a strongly-typed implementation of an angle. 

> WARNING: 
> **NetFabric.Numerics.Angle** makes use of [generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math) features only available in .NET 7 and C# 11.
> Make sure you are using a compatible version of the framework before using this library.
> For older versions of .NET, please use [NetFabric.Angle](https://github.com/NetFabric/NetFabric.Angle) instead.

``` csharp
using NetFabric.Numerics.Angle;

// Create angles
var degreesDoubleAngle = new Angle<Degrees, double>(45.0);
var radiansFloatAngle = new Angle<Radians, float>(1.57f);
var gradiansDecimalAngle = new Angle<Gradians, decimal>(200.0m);
var revolutionsDoubleAngle = new Angle<Revolutions, Half>((Half)0.25);

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
var areEqual = degreesDoubleAngle.Equals(Angle.ToDegrees(revolutionsDoubleAngle));
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
var arcsineRadiansAngle = Angle.Asin(sineValue);

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

## Angle<TUnits, T> vs. AngleReduced<TUnits, T>

- `Angle<TUnits, T>` represents an angle as a value of type `T` in the specified `TUnits` unit. 
- `AngleReduced<TUnits, T>` represents an angle as a value of type `T` in the specified `TUnits` unit, reduced to the range `[TUnits.Zero, TUnits.Full[`.
- The `T` type can be any of the following types: `float`, `double`, `decimal`, or any other implementation of `IFloatingPoint<TSelf>`.
- The `TUnits` type can be any of the following types: `Degrees`, `Radians`, `Gradians`, `Revolutions`, or any other implementation of `IAngleUnits<TSelf>`.

`Angle<TUnits, T>` can represent any angle value in the range `[T.MinValue, T.MaxValue]` in the specified `TUnits` unit. Some operations require the angle to be reduced to `[TUnits.Zero, TUnits.Full[`. 
To avoid the cost of reducing the angle every time, `AngleReduced<TUnits, T>` can be used instead. It guarantees that the angle is already reduced. 

An `Angle<TUnits, T>` can be converted to an `AngleReduced<TUnits, T>` using the `Angle.Reduce()` method.
`AngleReduced<TUnits, T>` can be implicitly converted to an `Angle<TUnits, T>`.

## Angle classification

**NetFabric.Numerics.Angle** provides a large number of methods to classify an angle: `IsZero`, `IsAcute`, `IsRight`, `IsObtuse`, `IsStraight`, `IsReflex`, `IsOblique`, `AreComplementary`, `AreSupplementary`

These methods are only available for `AngleReduced<TUnits, T>`. When classifying a `Angle<TUnits, T>`, reduce it first by using `Angle.Reduce()`.

## Trigonometry

**NetFabric.Numerics.Angle** provides a large number of trigonometric methods: `Sin`, `Cos`, `Tan`, `Sec`, `Csc`, `Cot`, `Sinh`, `Cosh`, `Tanh`, `Sech`, `Csch`, `Coth`, `Asin`, `Acos`, `Atan`, `Acot`, `Asec`, `Acsc`

These methods are only available for angles in radians. When using an angle on any other unit, convert it first by using `Angle.ToRadians()`.

## Collections support

**NetFabric.Numerics.Angle** provides optimized operations on collections of angles: `Sum`, `Average`.

These operations are available for `IEnumerable<Angle<TUnits, T>>`, `Angle<TUnits, T>[]`, `Memory<Angle<TUnits, T>>`, `ReadOnlyMemory<Angle<TUnits, T>>`, `Span<Angle<TUnits, T>>`, and `ReadOnlySpan<Angle<TUnits, T>>`.

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