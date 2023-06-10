# NetFabric.Numerics

**NetFabric.Numerics** provides strongly-typed implementations for cartesian and polar coordinates, and operations for 2D and 3D vectors and quaternions.

> WARNING: 
> **NetFabric.Numerics** makes use of [generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math) features only available in .NET 7 and C# 11.
> Make sure you are using a compatible version of the framework before using this library.

``` csharp
using NetFabric.Numerics;

// Create points
var point2DInteger = new Cartesian2.Point<int>(10, 20);
var point2DFloat = new Cartesian2.Point<float>(10.0f, 20.0f);
var point2DDouble = new Cartesian2.Point<double>(10.0, 20.0);

var pointPolarDegreesFloat = new Polar.Point<Degrees, float, float>(
    Angle<Degrees, float>.Right, // 90 degrees azimuth
    10.0f); // radius
var pointPolarRadiansDouble = new Polar.Point<Radians, double, double>(
    new Angle<Radians, double>(double.Pi), // 180 degrees azimuth
    10.0f); // radius

var point3DInteger = new Cartesian3.Point<int>(10, 20, 30);
var point3DFloat = new Cartesian3.Point<float>(10.0f, 20.0f, 30.0f);
var point3DDouble = new Cartesian3.Point<double>(10.0, 20.0, 30.0);

var pointSphericalDegreesFloat = new Spherical.Point<Degrees, float, float>(
    Angle<Degrees, float>.Zero, // 0 degrees azimuth
    Angle<Degrees, float>.Right, // 90 degrees zenith
    10.0f); // 90 degrees

// Create quaternions
var quaternionFloat = new Cartesian3.Quaternion<float>(1.0f, 2.0f, 3.0f, 4.0f);
var quaternionDouble = Cartesian3.Quaternion.FromYawPitchRoll<double>(
    Angle<Radians, double>.Zero, // 0 degrees yaw
    Angle<Radians, double>.Zero, // 0 degrees pitch
    Angle<Radians, double>.Right); // 90 degrees roll

// Perform math operations
var vector3DDouble = point3DDouble - new Cartesian3.Point<double>(1.0, 1.0, 1.0);
var point3DTransformed = point3DDouble + vector3DDouble;

// Conversions
var convertToFloatChecked = Cartesian3.Point<float>.CreateChecked(point3DDouble); // throws if value is out of range
var convertToFloatSaturated = Cartesian3.Point<float>.CreateSaturating(point3DDouble); // saturate if value is out of range
var convertToFloatTruncated = Cartesian3.Point<float>.CreateTruncating(point3DDouble); // truncate if value is out of range
```

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