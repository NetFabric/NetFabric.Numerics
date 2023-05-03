using System;
using System.Numerics;

namespace NetFabric.Numerics.Angle;

public readonly record struct Radians<TRadians>(TRadians Value) 
    : IAngle<Radians<TRadians>>,
      ITrigonometricFunctions<TRadians>,
      IAdditionOperators<Radians<TRadians>, Radians<TRadians>, Radians<TRadians>>
    where TRadians: IFloatingRadians<TRadians>
{
    const TRadians HalfRevolution = TRadians.CreateChecked(180.0);

    public static Radians<TRadians> operator +(Radians<TRadians> left, Radians<TRadians> right)
        => new (left.Value + right.Value);

        
    public static Radians<TRadians> op_CheckedAddition(Radians<TRadians> left, Radians<TRadians> right)
        => checked(new (left.Value + right.Value));

    public static Radians<TRadians> Acos(Radians<TRadians> x) 
        => new (Math.Acos(Value));

    static Radians<TRadians> ITrigonometricFunctions<TRadians>.AcosPi(Radians<TRadians> x)
        => new (Math.AcosPi(Value / HalfRevolution));
        
    public static Radians<TRadians> Asin(Radians<TRadians> x)
        => new (Math.Asin(Value));
        
    static Radians<TRadians> ITrigonometricFunctions<TRadians>.AsinPi(Radians<TRadians> x)
        => new (Math.AsinPi(Value / HalfRevolution));
        
    public static Radians<TRadians> Atan(Radians<TRadians> x)
        => new (Math.Atan(Value));
        
    static Radians<TRadians> ITrigonometricFunctions<TRadians>.AtanPi(Radians<TRadians> x)
        => new (Math.AtanPi(Value / HalfRevolution));
        
    public static Radians<TRadians> Cos(Radians<TRadians> x)
        => new (Math.Cos(Value));
        
    static Radians<TRadians> ITrigonometricFunctions<TRadians>.CosPi(Radians<TRadians> x)
        => new (Math.CosPi(Value / HalfRevolution));
        
    public static Radians<TRadians> Sin(Radians<TRadians> x)
        => new (Math.Sin(Value));
        
    static Radians<TRadians> ITrigonometricFunctions<TRadians>.SinPi(Radians<TRadians> x)
        => new (Math.SinPi(Value / HalfRevolution));
        
    public static (Radians<TRadians> Sin, Radians<TRadians> Cos) SinCos(Radians<TRadians> x)
        => (new (Math.Sin(Value), new (Math.Cos(Value))));
        
    static (Radians<TRadians> SinPi, Radians<TRadians> CosPi) ITrigonometricFunctions<TRadians>.SinCosPi(Radians<TRadians> x)
    {
        var value = Value / HalfRevolution;
        return (new (Math.SinPi(Value), new (Math.CosPi(Value))));
    }
        
    public static Radians<TRadians> Tan(Radians<TRadians> x)
        => new (Math.Tan(Value));
        
    static Radians<TRadians> ITrigonometricFunctions<TRadians>.TanPi(Radians<TRadians> x)
        => new (Math.TanPi(Value / HalfRevolution));
        
}