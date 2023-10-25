# Strongly Typed Angles with NetFabric.Numerics

In the world of geometric calculations and spatial mathematics, angles play a crucial role. Whether you're working on graphics, physics simulations, or any application involving angular measurements, dealing with angles can be a challenging task. NetFabric.Numerics, a .NET library, addresses these challenges with its strongly typed Angle implementation.

## The Challenge of Angles

Angles are unique in that they are periodic, wrapping around after reaching a full circle, typically represented as 360 degrees or 2π radians. When comparing angles in a programming context, it's essential to ensure they are correctly reduced to a standardized form. This reduction process involves calculations that can impact performance, which is a critical concern for many software applications.

NetFabric.Numerics offers a solution by introducing a `Reduce` method that returns an `AngleReduced` instance. This approach allows the compiler to understand whether the angle is reduced or not, enabling programmers to call methods that only accept reduced angles. This prevents runtime errors and promises more reliable and maintainable code.

```csharp
// sum of a right (90º) and full (360º) angles
Angle<Degrees, float> angle = Angle<Degrees, float>.Right + Angle<Degrees, float>.Full;

// reduce the angle
AngleReduced<Degrees, float> reduced = Angle.Reduce(angle);

// the following methods require the angle to be reduced
Quadrant quadrant = Angle.GetQuadrant(reduced);
bool isRight = Angle.IsRight(reduced);
```

### Strongly Typed Angles

NetFabric.Numerics uses generics to specify units of measurement for angles, including degrees, radians, gradians, and revolutions. This strict typing ensures that you only compare angles that use the same units, reducing the risk of erroneous calculations and enhancing code clarity. It also allows for compile-time optimizations, ensuring not only safety but also efficiency and performance.

```csharp
Angle<Degrees, double> degreesAngle = new Angle<Degrees, double>(45.0f);
Angle<Radians, double> radiansAngle = new Angle<Radians, double>(double.Pi)

// convert to single precision
Angle<Radians, float> singleRadiansAngle = Angle.CreateChecked<float>(radiansAngle);

// convert to revolutions
Angle<Revolutions, float> singleRevolutionsAngle = Angle.ToRevolutions(singleRadiansAngle);
```

This is accomplished through the C# compiler's use of generics. This means that for each angle, there are no additional memory allocations necessary beyond the size of the inner data type used.

### Leveraging .NET 7 Generics Math

NetFabric.Numerics leverages .NET 7 generics math, enabling the use of numeric types provided by the .NET framework. This includes fundamental types such as `int`, `float`, `double`, and more. It also allows integration with third-party implementations that adhere to .NET requirements, providing flexibility in choosing the numeric type that best suits your application's needs.

## Practical Applications

The applications of NetFabric.Numerics' strongly typed Angle implementation are numerous. From computer graphics to physics simulations, this library empowers developers to work with angles efficiently and error-resistantly.

- **Computer Graphics**: In rendering and manipulating objects on a screen, precise angle calculations are vital. NetFabric.Numerics ensures that you're working with correctly reduced angles, preventing graphical artifacts and glitches.

- **Physics Simulations**: In simulations of real-world physics, accurate angle measurements are essential. NetFabric.Numerics' strongly typed angles guarantee the use of consistent units and avoid the pitfalls of unmanaged angle calculations.

- **Engineering and CAD**: In engineering and computer-aided design (CAD) software, precise angle measurements are the norm. This library simplifies angle handling and improves code readability.

## Conclusion

NetFabric.Numerics' innovative approach to strongly typed Angle implementations and angle reduction simplifies the handling of angles in your code. It enforces unit compatibility and eliminates runtime errors. In software development, precision and performance matter, and NetFabric.Numerics ensures that your angle calculations are accurate and efficient. Whether you're developing a game, a physics simulator, or a CAD tool, NetFabric.Numerics is a valuable addition to your toolkit, providing reliability and precision in angle calculations.