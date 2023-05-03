using System.Numerics;

namespace NetFabric.Numerics.Angle;

public readonly record struct Degrees<TDegrees>(TDegrees Value) 
    : IAngle<Degrees<TDegrees>>
    where TDegrees: struct, IFloatingPoint<TDegrees>
{
    public static Degrees<TDegrees> operator +(Degrees<TDegrees> left, Degrees<TDegrees> right)
        => new (left.Value + right.Value);

        
    public static Degrees<TDegrees> op_CheckedAddition(Degrees<TDegrees> left, Degrees<TDegrees> right)
        => checked(new (left.Value + right.Value));
}