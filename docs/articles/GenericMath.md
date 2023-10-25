# The Evolution of Math in .NET 7 - Harnessing the Power of Generics

## Before .NET 7

In C#, arithmetic operators like +, -, *, and / are essential for performing mathematical operations. They make your code more intuitive by removing the need for calling specific methods like `Add`, `Subtract`, `Multiply`, or `Divide`.

For more advanced math, especially when working with trigonometric functions, C# offers the [`System.Math`](https://learn.microsoft.com/en-us/dotnet/api/system.math) static class. It's important to note that most methods in this class work with double precision (`double`) values. Still, in some cases, single precision (`float`) can offer better performance. To address this, .NET Core 2 introduced the [`System.MathF`](https://learn.microsoft.com/en-us/dotnet/api/system.mathf) class for single precision operations.

While you can define custom arithmetic operators for your types in C#, it's challenging with generics. Generics are designed to work with known methods, and since operators are essentially static methods, they can't be defined within an interface. This complexity often leads to libraries defining specific `Vector` types for only one or two numerical data types, simplifying the implementation.

## What .NET 7 Brings

.NET 7, coupled with C# 11, introduces exciting features that eliminate previous limitations. One notable advancement is the ability to declare static virtual methods in interfaces, including those for arithmetic operators.

In the [`System.Numerics`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics) namespace, you'll find various interfaces that define mathematical operations for native numerical .NET types. For example, if a type implements [`IAdditionOperators<TSelf, TOther, TResult>`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.iadditionoperators-3), it automatically includes the `+` operator.

This opens the door to a powerful generic `Sum` method:

```csharp
static T Sum<T>(IEnumerable<T> source)
    where T: IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    var sum = T.AdditiveIdentity; // initialize to zero
    foreach(var value in source)
        sum += value; // add value to sum
    return sum;
}
```

This method lets you sum a collection of any type that defines the additive identity (zero) and the `+` operator, which is used by the `+=` operator. It works not only for all .NET native numerical types but also extends to other types like vectors, quaternions, matrices, and more. It's a versatile addition to the C# language.

Alternatively, you could limit `T` to [`INumber<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.inumber-1) for any numeric type or restrict it to [`IFloatingPoint<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.ifloatingpoint-1) for floating-point native types. However, these require the implementation by the numeric type of many other interfaces not required by this operation.

In [`System.Numerics`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics), you'll find new interfaces for various mathematical operations beyond basic arithmetic operators. One such interface is [`ITrigonometricFunctions<TSelf`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.itrigonometricfunctions-1). This enables usage like:

```csharp
// Half-precision floating-point
var sinHalf = Half.Sin(Half.Pi);
var arcSinHalf = Half.Asin(sinHalf);

// Single-precision floating-point
var sinFloat = float.Sin(float.Pi);
var arcSinFloat = float.Asin(sinFloat);

// Double-precision floating-point
var sinDouble = double.Sin(double.Pi);
var arcSinDouble = double.Asin(sinDouble);
```

These constants like `Pi`, as well as the `Sin` and `Asin` methods, are now defined for the floating-point types `Half`, `float`, and `double`. This makes it unnecessary and even discouraged to use `System.Math` or `System.MathF`, expanding support for various other numeric types.

`System.Numerics` offers a range of similar interfaces, including [`IExponentialFunctions<TSelf>`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.iexponentialfunctions-1), [`IHyperbolicFunctions<TSelf>`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.ihyperbolicfunctions-1), [`ILogarithmicFunctions<TSelf>`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.ilogarithmicfunctions-1), [`IPowerFunctions<TSelf>`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.ipowerfunctions-1), [`IRootFunctions<TSelf>`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.irootfunctions-1), and more. Notably, the constants `E`, `Pi`, and `Tau` are defined within the [`IFloatingPointConstants<TSelf>`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.ifloatingpointconstants-1) interface.

## Conclusion

- Generics have become more versatile, simplifying code and enhancing maintainability.
- It's advisable to transition away from `System.Math` or `System.MathF` and use the inherent math capabilities of each data type.
- If you're developing custom numeric types, implementing the interfaces provided in `System.Numerics` is crucial to ensure compatibility with third-party methods.

Additionally, it's worth noting that the NetFabrics.Numerics library leverages these new features to implement strongly-typed solutions for angles and geometric primitives across multiple coordinate systems, including rectangular 2D and 3D, polar, and spherical. This library showcases the power and flexibility of the .NET 7 and C# 11 features, providing robust support for various mathematical applications.

