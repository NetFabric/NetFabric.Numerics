# NetFabric.Numerics.Angle

**NetFabric.Numerics.Angle** provides a strongly-typed implementation of an angle. 

> WARNING: 
> **NetFabric.Numerics.Angle** makes use of [generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math) features only available in .NET 7 and C# 11.
> For older versions of .NET, please use [NetFabric.Angle](https://github.com/NetFabric/NetFabric.Angle) instead.

``` csharp
using NetFabric.Numerics.Angle;

// Create angles
var degreesAngle = new Angle<Degrees, double>(45.0);
var radiansAngle = new Angle<Radians, float>(1.57f);
var gradiansAngle = new Angle<Gradians, decimal>(200.0m);
var revolutionsAngle = new Angle<Revolutions, double>(0.25);

// Perform angle operations
var sum = degreesAngle + Angle.ToDegrees<float, double>(radiansAngle);
var difference = Angle.ToRadians<decimal, float>(gradiansAngle) - Angle.ToRadians<double, float>(revolutionsAngle);
var product = 2.0 * degreesAngle;
var quotient = gradiansAngle / 100.0m;
var remainder = degreesAngle % 180.0;

// Compare angles
var areEqual = degreesAngle.Equals(Angle.ToDegrees<float, double>(radiansAngle));
var isGreater = gradiansAngle > Angle.ToGradians<double, decimal>(revolutionsAngle);

// Convert angles
var convertedToRadians = Angle.ToRadians(degreesAngle);
var convertedToDegrees = Angle.ToDegrees(radiansAngle);
var convertedToRevolution = Angle.ToRevolution(degreesAngle);

var convertToFloat = Angle<Degrees, float>.CreateChecked(degreesAngle); // throws if value is out of range

// Perform trigonometric calculations
var sineValue = Angle.Sin(radiansAngle);
var cosineValue = Angle.Cos(Angle.ToRadians(degreesAngle));
var tangentValue = Angle.Tan(radiansAngle);
var arcsineRadiansAngle = Angle.Asin(180.0);

// Reduce angles
var reducedAngle = Angle.Reduce(degreesAngle);
var quadrant = Angle.GetQuadrant(reducedAngle);
var reference = Angle.GetReference(reducedAngle);

// Classify angles
var isZeroAngle = Angle.IsZero(reducedAngle);
var isAcuteAngle = Angle.IsAcute(reducedAngle);
var isRightAngle = Angle.IsRight(reducedAngle);
var isObtuseAngle = Angle.IsObtuse(reducedAngle);
var isStraightAngle = Angle.IsStraight(reducedAngle);

// Calculate collection operations
var angleCollection = new List<Angle<Degrees, double>> { degreesAngle, Angle.ToDegrees<float, double>(radiansAngle), Angle.ToDegrees<decimal, double>(gradiansAngle) };
var collectionSum = angleCollection.Sum();
var collectionAverage = angleCollection.Average();
```

## Angle<TUnits, T> and AngleReduced<TUnits, T>

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

These operations use SIMD instructions when available, ensuring high-performance calculations.

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