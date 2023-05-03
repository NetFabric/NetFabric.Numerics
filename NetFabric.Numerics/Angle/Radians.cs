using System;
using System.Numerics;

namespace NetFabric.Numerics.Angle;

public readonly record struct Radians<TRadians>(TRadians Value) 
    : IAngle<Radians<TRadians>>
    where TRadians: struct, IFloatingPoint<TRadians>
{
    public static Radians<TRadians> operator +(Radians<TRadians> left, Radians<TRadians> right)
        => new (left.Value + right.Value);

        
    public static Radians<TRadians> op_CheckedAddition(Radians<TRadians> left, Radians<TRadians> right)
        => checked(new (left.Value + right.Value));

    public static Radians<TRadians> Acos<TCos>(TCos cos) 
        where TCos: IFloatingPoint<TCos>
        => new (TRadians.CreateChecked(Math.Acos(double.CreateChecked(cos))));

    public static Radians<TRadians> Asin<TSin>(TSin sin)
        where TSin: IFloatingPoint<TSin>
        => new (TRadians.CreateChecked(Math.Asin(double.CreateChecked(sin))));
        
    public static Radians<TRadians> Atan<TTan>(TTan tan)
        where TTan: IFloatingPoint<TTan>
        => new (TRadians.CreateChecked(Math.Atan(double.CreateChecked(double.CreateChecked(tan)))));
        
    public static double Cos(Radians<TRadians> radians)
        => Math.Cos(double.CreateChecked(radians.Value));
        
    public static double Sin(Radians<TRadians> radians)
        => Math.Sin(double.CreateChecked(radians.Value));
        
    public static double Tan(Radians<TRadians> radians)
        => Math.Tan(double.CreateChecked(radians.Value));
        
}