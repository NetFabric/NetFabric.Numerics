# NetFabric.Numerics

Welcome to the documentation for **NetFabric.Numerics**, a powerful C# library that provides strongly-typed implementations of multiple coordinate systems, including cartesian, polar, spherical, and geodetic. Whether you're working with angles, points, or geographic coordinates, this library has you covered!

> **Note:** The `NetFabric.Numerics.Angle` class in this library utilizes advanced generic math features, which are only available in .NET 7 and C# 11. Make sure you are using a compatible version of the framework before using this library.

## Installation

To get started with **NetFabric.Numerics**, you need to install the library's NuGet packages. The library is divided into three packages, each serving a specific purpose:

- [NetFabric.Numerics.Angle](https://www.nuget.org/packages/NetFabric.Numerics.Angle/): Provides strongly-typed angle implementations.
- [NetFabric.Numerics](https://www.nuget.org/packages/NetFabric.Numerics/): Provides strongly-typed cartesian and polar coordinate implementations.
- [NetFabric.Numerics.Geography](https://www.nuget.org/packages/NetFabric.Numerics.Geography/): Provides strongly-typed geodetic coordinate implementations.

Make sure to include the appropriate package(s) in your project, depending on your specific needs.

## Strongly-Typed Approach

**NetFabric.Numerics** embraces a strongly-typed approach, leveraging the power of generics to ensure type safety and flexibility. By utilizing generics, you can specify both the data type used internally and the features of the coordinate system.

Let's take a look at some examples to illustrate this:

### Strongly-Typed Points

When working with points in different coordinate systems, **NetFabric.Numerics** allows you to specify the data type used internally. Here's an example:

``` csharp
using NetFabric.Numerics.Cartesian2;

var integerPoint = new Point<int>(0, 0);                      // Point using integers
var doublePrecisionPoint = new Point<double>(0.0, 0.0);       // Point using double precision
var singlePrecisionPoint = new Point<float>(0.0, 0.0);        // Point using single precision
```

By specifying the desired data type, you have control over the precision and memory usage of your points.

## Strongly-Typed Angles ##

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

## Strongly-Typed Polar Coordinates ##

**NetFabric.Numerics** provides strongly-typed polar coordinate implementations. Here's an example:

``` csharp
using NetFabric.Numerics.Polar;

var degreesPoint = new Point<Degrees, double, double>(0.0, 0.0);            // Polar point using degrees
var radiansPoint = new Point<Radians, double, double>(0.0, 0.0);            // Polar point using radians
var gradiansPoint = new Point<Gradians, double, double>(0.0, 0.0);          // Polar point using gradians
var revolutionsPoint = new Point<Revolutions, double, double>(0.0, 0.0);    // Polar point using revolutions

var doublePrecisionPoint = new Point<Degrees, double, double>(0.0, 0.0);    // Polar point with double precision
var singlePrecisionPoint = new Point<Degrees, float, float>(0.0, 0.0);      // Polar point with single precision
var mixedPrecisionPoint = new Point<Degrees, float, double>(0.0, 0.0);      // Polar point with mixed precision
```

With **NetFabric.Numerics**, you can work with polar coordinates using different units of measurement and specify the desired precision for the azimuth and radius.

## Strongly-Typed Geodetic Coordinates ##

**NetFabric.Numerics** also supports strongly-typed geodetic coordinate implementations, specifically latitude and longitude. Here's an example:

``` csharp
using NetFabric.Numerics.Geography.Geodetic2;

var wgs84Point = new Point<WGS84, double>(new Angle<Degrees, double>(0.0), new Angle<Degrees, double>(0.0));                    // Geodetic point using WGS84 datum
var wgs1972Point = new Point<WGS1972, double>(new Angle<Degrees, double>(0.0), new Angle<Degrees, double>(0.0));                // Geodetic point using WGS1972 datum
var nad83Point = new Point<NAD83, double>(new Angle<Degrees, double>(0.0), new Angle<Degrees, double>(0.0));                    // Geodetic point using NAD83 datum
var nad1927ConusPoint = new Point<NAD1927CONUS, double>(new Angle<Degrees, double>(0.0), new Angle<Degrees, double>(0.0));      // Geodetic point using NAD1927CONUS datum

var doublePrecisionPoint = new Point<WGS84, double>(new Angle<Degrees, double>(0.0), new Angle<Degrees, double>(0.0));          // Geodetic point with double precision
var singlePrecisionPoint = new Point<WGS84, float>(new Angle<Degrees, double>(0.0), new Angle<Degrees, double>(0.0));           // Geodetic point with single precision

var minutesPoint = new Point<WGS84, double>(Angle.FromDegreesMinutes(0, 0.0), Angle.FromDegreesMinutes(0, 0.0));                          // Geodetic point using minutes
var minutesSecondsPoint = new Point<WGS84, double>(Angle.FromDegreesMinutesSeconds(0, 0, 0.0), Angle.FromDegreesMinutesSeconds(0, 0, 0.0));  // Geodetic point using minutes and seconds

var (degreesLatitude, minutesLatitude) = Angle.ToDegreesMinutes(wgs84Point.Latitude);                                             // Convert latitude to degrees and minutes
var (degreesLatitude, minutesLatitude, secondsLatitude) = Angle.ToDegreesMinutesSeconds(wgs84Point.Latitude);                     // Convert latitude to degrees, minutes, and seconds
```

**NetFabric.Numerics** enables you to work with geodetic coordinates using various datums, allowing you to specify the desired precision and units of measurement for latitude and longitude.

## Conclusion ##

**NetFabric.Numerics** provides a comprehensive set of strongly-typed implementations for different coordinate systems, offering flexibility, type safety, and precise control over precision and units of measurement. Whether you're working with points, angles, or geodetic coordinates, this library simplifies your code and ensures accuracy in your calculations. Explore the library's packages and unleash the power of strongly-typed numerical computations in your C# projects!
