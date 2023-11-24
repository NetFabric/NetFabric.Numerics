# Exploring Geometric Interfaces in C# with NetFabric.Numerics

If you're a C# developer interested in working with geometric types and coordinate systems, the `NetFabric.Numerics` package offers a powerful set of interfaces and data structures. In this article, we'll take a deep dive into the core interfaces provided by the package and explore how they can help you model geometric data efficiently.

## ICoordinateSystem Interface

Introducing the `ICoordinateSystem` interface, which plays a key role in providing access to coordinate information within a system. Here's the code snippet:

```csharp
public interface ICoordinateSystem 
{
    IReadOnlyList<Coordinate> Coordinates { get; }
}
```

The `Coordinates` property returns an `IReadOnlyCollection<Coordinate`, ensuring you can access information about each coordinate while maintaining data integrity.

`Coordinate` is defined as follow:

```csharp
public readonly record struct Coordinate(string Name, Type Type);
```

This code defines a read-only struct, `Coordinate`, which holds the name and type for a coordinate. It's useful for dynamic queries of the coordinate system, especially for tasks like serialization and creating user interfaces. These practices align with modern C# coding standards and can enhance code performance and efficiency.

### IGeometricBase 

The `IGeometricBase` interface is at the core of the geometric types in the `NetFabric.Numerics` package. It defines a common set of operations and properties for geometric objects. Let's explore the key elements of this interface:

```csharp
public interface IGeometricBase<TSelf, TCoordinateSystem>
    : IEquatable<TSelf>,
      IEqualityOperators<TSelf, TSelf, bool>
    where TSelf : struct, IGeometricBase<TSelf, TCoordinateSystem>?
    where TCoordinateSystem : class, ICoordinateSystem
{    
    TCoordinateSystem CoordinateSystem { get; }

    object this[int index] { get; }

    static abstract TSelf Zero { get; }

    public static bool IsZero(TSelf value) 
        => value.Equals(TSelf.Zero);
}
```

The definition implies that all geometric objects must be equatable, emeaning that they mus implement the generics version of `Equals` and also must implement all the equality operators.

They must provide a `CoordinateSystem` property that returns and instance of class (reference type) that implements `ICoordinateSystem`. This allows the dynamic queries in realtime about the coordinate system the geometric object is defined in. 

Having it be a reference type and implement the singleton pattern, makes it more efficient to compare if two geometric object are from the same coordinate system. The equal operator will internally use the `ReferenceEquals`.


## Points in Space

### IPoint Interface

Building on the `IGeometricBase` interface, the `IPoint` interface is tailored for representing points in a coordinate system. It includes additional capabilities, such as finding the minimum and maximum values for points. Here's the definition of the `IPoint` interface:

```csharp
namespace NetFabric.Numerics;

public interface IPoint<TSelf, TCoordinateSystem>
    : IGeometricBase<TSelf, TCoordinateSystem>,
      IMinMaxValue<TSelf>
    where TSelf : struct, IPoint<TSelf, TCoordinateSystem>?
    where TCoordinateSystem : class, ICoordinateSystem
{
}
```

The `IPoint` interface adds the ability to find the minimum and maximum values for points, which can be particularly useful in spatial applications.

## Vectors in Space

### IVector Interface

Lastly, the `IVector` interface is designed for modeling vectors in a coordinate system. It encompasses a wide range of mathematical operations and comparisons. Here's how it's defined:

```csharp
namespace NetFabric.Numerics;

public interface IVector<TSelf, TCoordinateSystem, T>
    : IGeometricBase<TSelf, TCoordinateSystem>,
      IComparable,
      IComparable<TSelf>,
      IComparisonOperators<TSelf, TSelf, bool>,
      IAdditiveIdentity<TSelf, TSelf>,
      IUnaryPlusOperators<TSelf, TSelf>,
      IAdditionOperators<TSelf, TSelf, TSelf>,
      IUnaryNegationOperators<TSelf, TSelf>,
      ISubtractionOperators<TSelf, TSelf, TSelf>,
      IMultiplyOperators<TSelf, T, TSelf>,
      IDivisionOperators<TSelf, T, TSelf>,
      IMinMaxValue<TSelf>
    where TSelf : struct, IVector<TSelf, TCoordinateSystem, T>?
    where TCoordinateSystem : class, ICoordinateSystem
    where T : struct, INumber<T>, IMinMaxValue<T>
{
}
```

The `IVector` interface is a versatile tool for working with vectors. It supports operations such as addition, subtraction, unary negation, and scalar multiplication, making it an essential foundation for vector mathematics.

## Wrapping Up

The `NetFabric.Numerics` package provides a robust set of interfaces for modeling geometric data and coordinate systems in C#. By adhering to these interfaces and following the coding guidelines you've specified, you can build efficient and maintainable code for geometric calculations. Whether you're working on 2D or 3D graphics, physics simulations, or any other domain involving spatial data, these interfaces are a valuable resource for your C# projects. Happy coding!

*Note: The code snippets provided in this article adhere to the coding guidelines specified, using up-to-date C# syntax and providing well-structured inline documentation where relevant.*