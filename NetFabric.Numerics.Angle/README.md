# NetFabric.Numerics.Angle

NetFabric.Numerics.Angle implements a strongly-typed implementation of an angle. 

> WARNING: 
> NetFabric.Numerics makes use of [generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math) features only available in .NET 7 and C# 11.
> For older versions of .NET, please check [NetFabric.Angle](https://github.com/NetFabric/NetFabric.Angle) instead.

This library provides:
- Floating point precision (`float`, `double`, and `decimal`)
- Angle units (`Degrees`, `Radians`, `Gradians`, and `Revolutions`)
- Angle operations (addition, subtraction, multiplication, division, and remainder)
- Angle comparison (equality, greater than, greater than or equal, less than, less than or equal, and compare)
- Angle conversions (to degrees, to radians, to gradians, and to revolutions)
- Angle trigonometry (`Sin`, `Cos`, `Tan`, `Sec`, `Csc`, `Cot`, `Sinh`, `Cosh`, `Tanh`, `Sech`, `Csch`, `Coth`, `Asin`, `Acos`, `Atan`, `Acot`, `Asec`, `Acsc`)
- Angle reduction (`Reduce`, `GetQuadrant`, `GetReference`)
- Angle classification (`IsZero`, `IsAcute`, `IsRight`, `IsObtuse`, `IsStraight`, `IsReflex`, `IsOblique`, `AreComplementary`, `AreSupplementary`)
- Angle linear interpolation (`Lerp`)
- Angle parsing (from string)
- Angle formatting (to string)

## Angle<TUnits, T> and AngleReduced<TUnits, T>

- `Angle<TUnits, T>` represents an angle as a value of type `T` in the specified `TUnits` unit. 
- `AngleReduced<TUnits, T>` represents an angle as a value of type `T` in the specified `TUnits` unit, reduced to the range `[0, 2π[`.
- The `T` type can be any of the following types: `float`, `double`, or `decimal`.
- The `TUnits` type can be any of the following types: `Degrees`, `Radians`, `Gradians`, or `Revolutions`.

`Angle<TUnits, T>` can represent any angle value in the range `[T.MinValue, T.MaxValue]` in the specified `TUnits` unit. Some operations require the angle to be reduced to `[Angle<TUnits, T>.Zero, Angle<TUnits, T>.Full[`. 
To avoid the cost of reducing the angle every time, `AngleReduced<TUnits, T>` can be used instead. It guarantees that the angle is already reduced. 

An `Angle<TUnits, T>` can be converted to an `AngleReduced<TUnits, T>` using the `Angle.Reduce()` method.
`AngleReduced<TUnits, T>` can be implicitly converted to an `Angle<TUnits, T>`.








