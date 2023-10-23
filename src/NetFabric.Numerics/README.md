# NetFabric.Numerics

## Overview

**NetFabric.Numerics** is a C# library that specializes in delivering strongly-typed implementations for various coordinate systems, making it well-suited for precise numeric and geometric operations. This README will introduce you to the library's key features and how to use it effectively.

## Key Features

- **Strong Typing:** The library places a strong emphasis on type safety, ensuring that your code remains robust without the need for excessive memory allocation.

- **Universal Compatibility:** **NetFabric.Numerics** harnesses the latest generic math features, and it requires .NET 7 or a more recent version.

- **Mathematical Concepts:** The library is founded on the mathematical concepts of points and vectors. Subtracting two points results in a vector, and adding a point and a vector results in a point.

- **Support for Multiple Coordinate Systems:** **NetFabric.Numerics** supports multiple coordinate systems, including Cartesian, Polar, and Spherical coordinates. This allows you to work with various coordinate systems seamlessly, expanding the library's versatility for your numeric and geometric operations.

## Usage

To make the most of **NetFabric.Numerics**, follow these steps:

1. **Installation:** The library is available as a NuGet package. You can install it using your preferred method, such as Package Manager Console, .NET CLI, or Visual Studio.

```shell
dotnet add package NetFabric.Numerics
```

2. **Add Namespace:** Import the necessary namespaces in your C# code to access the library's functionality.

```csharp
using NetFabric.Numerics;
using NetFabric.Numerics.Cartesian2;
using NetFabric.Numerics.Polar;
using NetFabric.Numerics.Spherical; // Include the Spherical coordinate system
```

When using points and vectors from multiple coordinate systems, you can use the following namespace:

```csharp
using NetFabric.Numerics;
```

Precede the type by the coordinate system name to avoid ambiguity:

```csharp
var point2D = new Cartesian2.Point<int>(10, 20);
var point3D = new Cartesian3.Point<int>(10, 20, 30);
var pointPolar = new Polar.Point<Degrees, float>(10, 20);
var pointSpherical = new Spherical.Point<Degrees, float>(10, 20, 30);
```

3. **Start Using the Library:** Once the library is installed and namespaces are imported, you can begin using it for your strongly-typed numeric and geometric operations. For detailed information and examples on how to use different features and coordinate systems, refer to the library's documentation.

## Strong Typing in Coordinates

**NetFabric.Numerics** excels in providing a strongly-typed approach to coordinate systems, built on the mathematical concepts of points and vectors. Subtracting two points results in a vector, while adding a point and a vector results in a point. This approach is showcased in various examples:

```csharp
using NetFabric.Numerics.Cartesian2;

var integerPoint = new Point<int>(0, 0);                // Point using integers
var doublePrecisionPoint = new Point<double>(0.0, 0.0); // Point using double precision
var singlePrecisionPoint = new Point<float>(0.0, 0.0);  // Point using single precision
```

### Quaternions Made Simple

Quaternions are essential for orientation and rotation. With **NetFabric.Numerics**, you can work with them efficiently:

```csharp
// Strongly-typed Quaternion using single precision
var quaternionFloat = new Quaternion<float>(1.0f, 2.0f, 3.0f, 4.0f);
```

### Efficient Math Operations

**NetFabric.Numerics** simplifies numeric operations while maintaining precision and strong typing:

```csharp
// Subtract two 3D points, resulting in a vector
var vector3DDouble = point3DDouble - new Point<double>(1.0, 1.0, 1.0);

// Transform a 3D point with a vector, resulting in a new point
var point3DTransformed = point3DDouble + vector3DDouble;
```

### Simple Conversions

The library simplifies converting between different numeric types while preserving strong typing:

```csharp
// Convert a 3D point to single precision, checking for overflow
var convertToFloatChecked = Point<float>.CreateChecked(point3DDouble);

// Convert a 3D point to single precision, saturating on overflow
var convertToFloatSaturated = Point<float>.CreateSaturating(point3DDouble);

// Convert a 3D point to single precision, truncating on overflow
var convertToFloatTruncated = Point<float>.CreateTruncating(point3DDouble);
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
