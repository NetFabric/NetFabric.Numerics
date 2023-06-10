# NetFabric.Numerics.Geography

**NetFabric.Numerics.Geography** provides a strongly-typed implementation of geography coordinates and calculations.

> WARNING: 
> **NetFabric.Numerics.Geography** makes use of [generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math) features only available in .NET 7 and C# 11.
> Make sure you are using a compatible version of the framework before using this library.

``` csharp
using NetFabric.Numerics.Geography.Geodetic2;

var wgs84Point = new Point<WGS84, Degrees, double>(new(0.0), new(0.0));                 // LatLon point using WGS84 datum
var wgs1972Point = new Point<WGS1972, Degrees, double>(new(0.0), new(0.0));             // LatLon point using WGS1972 datum
var nad83Point = new Point<NAD83, Degrees, double>(new(0.0), new(0.0));                 // LatLon point using NAD83 datum
var nad1927ConusPoint = new Point<NAD1927CONUS, Degrees, double>(new(0.0), new(0.0));   // LatLon point using NAD1927CONUS datum

var doublePrecisionPoint = new Point<WGS84, Degrees, double>(new(0.0), new(0.0));       // LatLon point with double precision
var singlePrecisionPoint = new Point<WGS84, Degrees, float>(new(0.0f), new(0.0f));      // LatLon point with single precision

var minutesPoint = new Point<WGS84, Degrees, double>(Angle.ToDegrees<double>(0, 0.0), Angle.ToDegrees<double>(0, 0.0));               // LatLon point using degrees and minutes
var minutesSecondsPoint = new Point<WGS84, Degrees, double>(Angle.ToDegrees<double>(0, 0, 0.0), Angle.ToDegrees<double>(0, 0, 0.0));  // LatLon point using degrees, minutes and seconds

var (degreesLatitude, minutesLatitude) = Angle.ToDegreesMinutes<double, int, double>(wgs84Point.Latitude);                                // Convert latitude to degrees and minutes
var (degreesLatitude2, minutesLatitude2, secondsLatitude) = Angle.ToDegreesMinutesSeconds<double, int, int, double>(wgs84Point.Latitude); // Convert latitude to degrees, minutes, and seconds
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