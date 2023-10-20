# NetFabric.Numerics

Your Comprehensive Strongly-Typed Toolkit for Numeric and Geometric Operations in C#!

## Key Features

- **Strongly-typed** implementations for cartesian, polar, and spherical coordinates.
- Seamless operations on 2D and 3D vectors and quaternions.
- Leveraging the latest [generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math) features, available in .NET 7 and C# 11.

## Introduction

Welcome to **NetFabric.Numerics** – a versatile library designed to simplify numeric and geometric calculations in C#.

## Getting Started

To harness the full capabilities of **NetFabric.Numerics**, ensure you have a compatible .NET 7 or higher environment. Once you're set up, you can unlock the potential of various coordinate systems and efficient operations.

### Strongly-Typed Coordinates

**NetFabric.Numerics** shines with its **strongly-typed** approach to coordinate systems. Whether you're dealing with 2D, 3D, polar, or spherical coordinates, our library offers precision with strong typing:

```csharp
var point2DInteger = new Cartesian2.Point<int>(10, 20);
var point2DFloat = new Cartesian2.Point<float>(10.0f, 20.0f);
var point3DDouble = new Cartesian3.Point<double>(10.0, 20.0, 30.0);

var pointPolarDegreesFloat = new Polar.Point<Degrees, float, float>(Angle<Degrees, float>.Right, 10.0f);
var pointPolarRadiansDouble = new Polar.Point<Radians, double, double>(new Angle<Radians, double>(Math.PI), 10.0f);

var pointSphericalDegreesFloat = new Spherical.Point<Degrees, float, float>(
    Angle<Degrees, float>.Zero, // Azimuth: 0 degrees
    Angle<Degrees, float>.Right, // Zenith: 90 degrees
    10.0f); // Radius
```

### Quaternions Made Easy

Quaternions are crucial for orientation and rotation. With **NetFabric.Numerics**, you can create them with strong typing and ease:

```csharp
var quaternionFloat = new Cartesian3.Quaternion<float>(1.0f, 2.0f, 3.0f, 4.0f);
var quaternionDouble = Cartesian3.Quaternion.FromYawPitchRoll<double>(Angle<Radians, double>.Zero, Angle<Radians, double>.Right);
```

### Efficient Math Operations

Elevate your numeric operations with our library. Enjoy simple, efficient, and accurate calculations with strong typing:

```csharp
var vector3DDouble = point3DDouble - new Cartesian3.Point<double>(1.0, 1.0, 1.0);
var point3DTransformed = point3DDouble + vector3DDouble;
```

### Simple Conversions

**NetFabric.Numerics** simplifies converting between different numeric types while preserving strong typing. You choose how to handle out-of-range values:

```csharp
var convertToFloatChecked = Cartesian3.Point<float>.CreateChecked(point3DDouble);
var convertToFloatSaturated = Cartesian3.Point<float>.CreateSaturating(point3DDouble);
var convertToFloatTruncated = Cartesian3.Point<float>.CreateTruncating(point3DDouble);
```

Discover how **NetFabric.Numerics** can simplify your numeric and geometric challenges today.

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