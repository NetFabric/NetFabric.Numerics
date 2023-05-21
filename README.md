# NetFabric.Numerics

NetFabric.Numerics provides strongly-typed implementations of multiple coordinate systems, i.e. cartesian, polar, spherical, and geodetic.

> WARNING: 
> `NetFabric.Numerics.Angle` makes use of [generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math) features only available in .NET 7 and C# 11.

Please use the "discussions" in this repository for feedback and suggestions. 

## Installation

This repository is published over three NuGet packages:
- [NetFabric.Numerics.Angle](https://www.nuget.org/packages/NetFabric.Numerics.Angle/) - provides strongly-typed angle implementations.
- [NetFabric.Numerics](https://www.nuget.org/packages/NetFabric.Numerics/) - provides strongly-typed cartesian and polar coordinate implementations.
- [NetFabric.Numerics.Geography](https://www.nuget.org/packages/NetFabric.Numerics.Geography/) - provides strongly-typed geodetic coordinate implementations.

## Strongly-typed

NetFabric.Numerics makes strong use of generics to make it fully strongly-typed. 

The generics can be used to specify the data type used internally:

``` csharp
using NetFabric.Numerics.Cartesian2;

var integerPoint = new Point<int>(0, 0);
var doublePrecisionPoint = new Point<double>(0.0, 0.0);
var singlePrecisionPoint = new Point<float>(0.0, 0.0);
```

The generics can also be used to specify coordinate system features:

Angles
``` csharp
using NetFabric.Numerics;

// supports multiple units: degrees, radians, gradians, and revolutions
var degreesAngle = new Angle<Degrees, double>(0.0);
var radiansAngle = new Angle<Radians, double>(0.0);
var gradiansAngle = new Angle<Gradians, double>(0.0);
var revolutionsAngle = new Angle<Revolutions, double>(0.0);

// supports any floating point precision
var doublePrecisionAngle = new Angle<Degrees, double>(0.0);
var singlePrecisionAngle = new Angle<Degrees, float>(0.0);
```

Polar coordinates
``` csharp
using NetFabric.Numerics.Polar;

// supports multiple units: degrees, radians, gradians, and revolutions
var degreesPoint = new Point<Degrees, double, double>(0.0, 0.0);
var radiansPoint = new Point<Radians, double, double>(0.0, 0.0);
var gradiansPoint = new Point<Gradians, double, double>(0.0, 0.0);
var revolutionsPoint = new Point<Revolutions, double, double>(0.0, 0.0);

// supports any floating point precision for azimuth and radius
var doublePrecisionPoint = new Point<Degrees, double, double>(0.0, 0.0);
var singlePrecisionPoint = new Point<Degrees, float, float>(0.0, 0.0)
var mixedPrecisionPoint = new Point<Degrees, float, double>(0.0, 0.0)
```

Geodetic coordinates (latitude and longitude)
``` csharp
using NetFabric.Numerics.Geography.Geodetic2;

// supports definition of datum
var wgs84Point = new Point<WGS84, double>(new(0.0), new(0.0));
var wgs1972Point = new Point<WGS1972, double>(new(0.0), new(0.0));
var nad83Point = new Point<NAD83, double>(new(0.0), new(0.0));
var nad1927ConusPoint = new Point<NAD1927CONUS, double>(new(0.0), new(0.0));

// supports any floating point precision
var doublePrecisionPoint = new Point<WGS84, double>(new(0.0), new(0.0));
var singlePrecisionPoint = new Point<WGS84, float>(new(0.0), new(0.0));

// supports minutes and seconds
var minutesPoint = new Point<WGS84, double>(Angle.FromDegreesMinutes(0, 0.0), Angle.FromDegreesMinutes(0, 0.0));
var minutesSecondsPoint = new Point<WGS84, double>(Angle.FromDegreesMinutesSeconds(0, 0, 0.0), Angle.FromDegreesMinutesSeconds(0, 0, 0.0));

var (degreesLatitude, minutesLatitude) = Angle.ToDegreesMinutes(wgs84Point.Latitude);
var (degreesLatitude, minutesLatitude, secondsLatitude) = Angle.ToDegreesMinutesSeconds(wgs84Point.Latitude);
```

The use of generics prevents the use of incompatible types when coding. Overloaded operators and other functions only support compatible types. 

Conversion functions can be used:

``` csharp
using NetFabric.Numerics;

var doublePrecisionDegreesAngle = new Angle<Degrees, double>(0.0);

// convert precision
var singlePrecisionDegreesAngle = Angle.ToDegrees<double, float>(doublePrecisionDegreesAngle);

// convert units
var doublePrecisionRadiansAngle = Angle.ToRadians(doublePrecisionDegreesAngle);

// convert units and precision
var singlePrecisionRadiansAngle = Angle.ToRadians<double, float>(doublePrecisionDegreesAngle);
```




