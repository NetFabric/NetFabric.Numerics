using System;
using System.Numerics;

namespace NetFabric.Numerics.Angle;

public readonly record struct DegreesMinutes<TDegrees, TMinutes>(TDegrees Value, TMinutes Minutes) 
    : IAngle<DegreesMinutes<TDegrees, TMinutes>>,
      IAdditionOperators<DegreesMinutes<TDegrees, TMinutes>, DegreesMinutes<TDegrees, TMinutes>, DegreesMinutes<TDegrees, TMinutes>>
    where TDegrees: IBinaryInteger<TDegrees>
    where TDegrees: IFloatingDegrees<TDegrees>
{
    public static DegreesMinutes<TDegrees, TMinutes> operator +(DegreesMinutes<TDegrees, TMinutes> left, DegreesMinutes<TDegrees, TMinutes> right)
        => new (left.Value + right.Value);

    public static DegreesMinutes<TDegrees, TMinutes> op_CheckedAddition(DegreesMinutes<TDegrees, TMinutes> left, DegreesMinutes<TDegrees, TMinutes> right)
        => checked(new (left.Value + right.Value));
}