# NetFabric.Numerics.Angle

`NetFabric.Numerics.Angle` implements a strongly-typed implementation of an angle. 

> WARNING: 
> `NetFabric.Numerics.Angle` makes use of [generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math) features only available in .NET 7 and C# 11.
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
var convertedToRadians = Angle.ToRadians<double, float>(degreesAngle);
var convertedToDegrees = Angle.ToDegrees<float, double>(radiansAngle);
var convertedToRevolution = Angle.ToDegrees(degreesAngle);

// Perform trigonometric calculations
var sineValue = Angle.Sin(radiansAngle);
var cosineValue = Angle.Cos(degreesAngle);
var tangentValue = Angle.Tan(radiansAngle);
var arcsineRadiansAngle = Angle.Asin(180.0);

// Reduce angles
var reducedAngle = Angle.Reduce(degreesAngle);
var quadrant = Angle.GetQuadrant(reducedAngle);

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
- The `T` type can be any of the following types: `float`, `double`, or `decimal`.
- The `TUnits` type can be any of the following types: `Degrees`, `Radians`, `Gradians`, or `Revolutions`.

`Angle<TUnits, T>` can represent any angle value in the range `[T.MinValue, T.MaxValue]` in the specified `TUnits` unit. Some operations require the angle to be reduced to `[TUnits.Zero, TUnits.Full[`. 
To avoid the cost of reducing the angle every time, `AngleReduced<TUnits, T>` can be used instead. It guarantees that the angle is already reduced. 

An `Angle<TUnits, T>` can be converted to an `AngleReduced<TUnits, T>` using the `Angle.Reduce()` method.
`AngleReduced<TUnits, T>` can be implicitly converted to an `Angle<TUnits, T>`.

## Angle classification

`NetFabric.Numerics.Angle` provides a large number of methods to classify an angle: `IsZero`, `IsAcute`, `IsRight`, `IsObtuse`, `IsStraight`, `IsReflex`, `IsOblique`, `AreComplementary`, `AreSupplementary`

These methods are only available for `AngleReduced<TUnits, T>`. If you need to classify an `Angle<TUnits, T>`, you can use the `Angle.Reduce()` method to convert it to an `AngleReduced<TUnits, T>`.

## Trigonometry

`NetFabric.Numerics.Angle` provides a large number of trigonometric methods: `Sin`, `Cos`, `Tan`, `Sec`, `Csc`, `Cot`, `Sinh`, `Cosh`, `Tanh`, `Sech`, `Csch`, `Coth`, `Asin`, `Acos`, `Atan`, `Acot`, `Asec`, `Acsc`

These methods are only available for angles in radians. If you need to use them with angles in degrees, gradians, or revolutions, you can use the `Angle.ToRadians()` method to convert them to radians.

## Collections support

`NetFabric.Numerics.Angle` provides optimized operations on collections of angles: `Sum`, `Average`.

These operations are available for `IEnumerable<Angle<TUnits, T>>`, arrays, `Memory<Angle<TUnits, T>>`, `ReadOnlyMemory<Angle<TUnits, T>>`, `Span<Angle<TUnits, T>>`, and `ReadOnlySpan<Angle<TUnits, T>>`.

These operations use SIMD instructions when available, ensuring high-performance calculations.
