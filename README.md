# NetFabric.Numerics

Welcome to the documentation for **NetFabric.Numerics**, a powerful C# library that provides strongly-typed implementations of multiple coordinate systems, including rectangular, polar, spherical, and geodetic. Whether you're working with angles, points, or geographic coordinates, this library has you covered!

> **Note:** The `NetFabric.Numerics` library utilizes advanced generic math features, which are only available in .NET 7 and C# 11. 
> Make sure you are using a compatible version of the framework before using this library.

## Installation

To get started with **NetFabric.Numerics**, you need to install the library's NuGet packages. The library is divided into three packages, each serving a specific purpose:

- [NetFabric.Numerics.Angle](https://www.nuget.org/packages/NetFabric.Numerics.Angle/): Provides strongly-typed angle implementations.
- [NetFabric.Numerics](https://www.nuget.org/packages/NetFabric.Numerics/): Provides strongly-typed rectangular, polar and spherical coordinate implementations.
- [NetFabric.Numerics.Geography](https://www.nuget.org/packages/NetFabric.Numerics.Geography/): Provides strongly-typed geodetic coordinate implementations.

Make sure to include the appropriate package(s) in your project, depending on your specific needs.

## Strongly-Typed Approach

**NetFabric.Numerics** adopts a robust, strongly-typed methodology, harnessing the capabilities of generics to guarantee both type safety and adaptability. Through the utilization of generics, you have the ability to define the internal data type and the characteristics of the coordinate system without the need for additional memory allocation.

Let's take a look at some examples to illustrate this:

### Strongly-Typed Points

When working with points in different coordinate systems, **NetFabric.Numerics** allows you to specify the data type used internally. Here's an example:

``` csharp
using NetFabric.Numerics.Rectangular2D;

var integerPoint = new Point<int>(0, 0);                // Point using integers
var doublePrecisionPoint = new Point<double>(0.0, 0.0); // Point using double precision
var singlePrecisionPoint = new Point<float>(0.0, 0.0);  // Point using single precision

var checkedConversion = Point<float>.CreateChecked(doublePrecisionPoint);      // Throws if overflow occurs
var saturatedConversion = Point<float>.CreateSaturating(doublePrecisionPoint); // Saturates if overflow occurs
var truncatedConversion = Point<float>.CreateTruncating(doublePrecisionPoint); // Truncates if overflow occurs
```

By specifying the desired data type, you have control over the precision and memory usage of your points.

### Strongly-Typed Angles

**NetFabric.Numerics** also supports strongly-typed angles, with support for various units of measurement. Here's an example:

``` csharp
using NetFabric.Numerics;

var degreesAngle = new Angle<Degrees, double>(0.0);            // Angle using degrees
var radiansAngle = new Angle<Radians, double>(0.0);            // Angle using radians
var gradiansAngle = new Angle<Gradians, double>(0.0);          // Angle using gradians
var revolutionsAngle = new Angle<Revolutions, double>(0.0);    // Angle using revolutions

var doublePrecisionAngle = new Angle<Degrees, double>(0.0);    // Angle with double precision
var singlePrecisionAngle = new Angle<Degrees, float>(0.0);     // Angle with single precision
```

You can choose the unit of measurement for your angles and specify the desired precision.

### Strongly-Typed Polar Coordinates

**NetFabric.Numerics** provides strongly-typed polar coordinate implementations. Here's an example:

``` csharp
using NetFabric.Numerics.Polar;

var degreesPoint = new Point<Degrees, double>(0.0, 0.0);            // Polar point using degrees
var radiansPoint = new Point<Radians, double>(0.0, 0.0);            // Polar point using radians
var gradiansPoint = new Point<Gradians, double>(0.0, 0.0);          // Polar point using gradians
var revolutionsPoint = new Point<Revolutions, double>(0.0, 0.0);    // Polar point using revolutions

var doublePrecisionPoint = new Point<Degrees, double>(0.0, 0.0);    // Polar point with double precision
var singlePrecisionPoint = new Point<Degrees, float>(0.0, 0.0);     // Polar point with single precision
```

With **NetFabric.Numerics**, you can work with polar coordinates using different units of measurement and specify the desired precision for the azimuth and radius.

### Strongly-Typed Geodetic Coordinates

**NetFabric.Numerics** also supports strongly-typed geodetic coordinate implementations, specifically latitude and longitude. Here's an example:

``` csharp
using NetFabric.Numerics.Geography.Geodetic2;

var wgs84Point = new Point<WGS84, Degrees, double>(new(0.0), new(0.0));                    // Geodetic point using WGS84 datum
var wgs1972Point = new Point<WGS1972, Degrees, double>(new(0.0), new(0.0));                // Geodetic point using WGS1972 datum
var nad83Point = new Point<NAD83, Degrees, double>(new(0.0), new(0.0));                    // Geodetic point using NAD83 datum
var nad1927ConusPoint = new Point<NAD1927CONUS, Degrees, double>(new(0.0), new(0.0));      // Geodetic point using NAD1927CONUS datum

var doublePrecisionPoint = new Point<WGS84, Degrees, double>(new(0.0), new(0.0));          // Geodetic point with double precision
var singlePrecisionPoint = new Point<WGS84, Degrees, float>(new(0.0f), new(0.0f));           // Geodetic point with single precision

var minutesPoint = new Point<WGS84, Degrees, double>(Angle.ToDegrees<double>(0, 0.0), Angle.ToDegrees<double>(0, 0.0));               // Geodetic point using degrees and minutes
var minutesSecondsPoint = new Point<WGS84, Degrees, double>(Angle.ToDegrees<double>(0, 0, 0.0), Angle.ToDegrees<double>(0, 0, 0.0));  // Geodetic point using degrees, minutes and seconds

var (degreesLatitude, minutesLatitude) = Angle.ToDegreesMinutes<double, int, double>(wgs84Point.Latitude);                                // Convert latitude to degrees and minutes
var (degreesLatitude2, minutesLatitude2

, secondsLatitude) = Angle.ToDegreesMinutesSeconds<double, int, int, double>(wgs84Point.Latitude); // Convert latitude to degrees, minutes, and seconds
```

**NetFabric.Numerics** enables you to work with geodetic coordinates using various datums, allowing you to specify the desired precision and units of measurement for latitude and longitude.

This addition provides clarity about the usage of generic types for specifying data types and properties within the `NetFabric.Numerics` library, enhancing the documentation's value. If you have any further suggestions or updates, please feel free to let me know.

## Credits

The following open-source projects are used to build and test this project:

- [.NET](https://github.com/dotnet)
- [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet)
- [coverlet](https://github.com/coverlet-coverage/coverlet)
- [Fluent Assertions](https://github.com/fluentassertions/fluentassertions)
- [NetFabric.Hyperlinq.Analyzer](https://github.com/NetFabric/NetFabric.Hyperlinq.Analyzer)
- [xUnit](https://github.com/xunit/xunit)

## License

This project is licensed under the MIT license. See the [LICENSE](LICENSE) file for more info.