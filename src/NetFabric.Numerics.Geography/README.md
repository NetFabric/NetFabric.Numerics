# NetFabric.Numerics.Geography

Your Strongly-Typed Solution for Geography Coordinates and Calculations in C#!

## Key Features

- **Strongly-typed** implementation of geography coordinates.
- Utilizes the latest [generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math) features, compatible with .NET 7 and C# 11.

## Introduction

Welcome to **NetFabric.Numerics.Geography** – a library tailored for precise geography calculations in C# with a focus on strong typing.

## Getting Started

To make the most of **NetFabric.Numerics.Geography**, ensure you're operating within a compatible .NET 7 or higher environment. Once ready, explore the world of geography with strong typing.

### Strongly-Typed Geography Coordinates

**NetFabric.Numerics.Geography** distinguishes itself with a **strongly-typed** approach to geography coordinates. It covers various datums, including WGS84, NAD83, and more, providing accuracy through strong typing:

```csharp
var wgs84Point = new Point<WGS84, Degrees, double>(new(0.0), new(0.0));                 // LatLon point using WGS84 datum
var wgs1972Point = new Point<WGS1972, Degrees, double>(new(0.0), new(0.0));             // LatLon point using WGS1972 datum
var nad83Point = new Point<NAD83, Degrees, double>(new(0.0), new(0.0));                 // LatLon point using NAD83 datum
var nad1927ConusPoint = new Point<NAD1927CONUS, Degrees, double>(new(0.0), new(0.0));   // LatLon point using NAD1927CONUS datum

var doublePrecisionPoint = new Point<WGS84, Degrees, double>(new(0.0), new(0.0));       // LatLon point with double precision
var singlePrecisionPoint = new Point<WGS84, Degrees, float>(new(0.0f), new(0.0f));      // LatLon point with single precision

var minutesPoint = new Point<WGS84, Degrees, double>(Angle.ToDegrees<double>(0, 0.0), Angle.ToDegrees<double>(0, 0.0));               // LatLon point using degrees and minutes
var minutesSecondsPoint = new Point<WGS84, Degrees, double>(Angle.ToDegrees<double>(0, 0, 0.0), Angle.ToDegrees<double>(0, 0, 0.0));  // LatLon point using degrees, minutes, and seconds

var (degreesLatitude, minutesLatitude) = Angle.ToDegreesMinutes<double, int, double>(wgs84Point.Latitude);                                // Convert latitude to degrees and minutes
var (degreesLatitude2, minutesLatitude2, secondsLatitude) = Angle.ToDegreesMinutesSeconds<double, int, int, double>(wgs84Point.Latitude); // Convert latitude to degrees, minutes, and seconds
```

### Geography with Strong Typing

Explore how **NetFabric.Numerics.Geography** can enhance your geography calculations with strong typing. Discover the precision, safety, and versatility it brings to C# geography operations.

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