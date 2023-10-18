using System.Runtime.InteropServices;

namespace NetFabric.Numerics
{
    static class Utils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Reduce<TUnits, T>(Angle<TUnits, T> angle)
            where TUnits : IAngleUnits<TUnits>
            where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        {
            var reduced = angle.Value % Angle<TUnits, T>.Full.Value;
            return T.Sign(reduced) >= 0
                ? reduced
                : reduced + Angle<TUnits, T>.Full.Value;
        }   
                
        public static Quadrant GetQuadrant<TUnits, T>(AngleReduced<TUnits, T> angle)
            where TUnits : IAngleUnits<TUnits>
            where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        {
            var value = angle.Value;
            if(value == Angle<TUnits, T>.Zero.Value)
                return Quadrant.PositiveX;
            if (value < Angle<TUnits, T>.Right.Value)
                return Quadrant.First;
            if (value == Angle<TUnits, T>.Right.Value)
                return Quadrant.PositiveY;
            if (value < Angle<TUnits, T>.Straight.Value)
                return Quadrant.Second;
            if (value == Angle<TUnits, T>.Straight.Value)
                return Quadrant.NegativeX;
            if (value < Angle<TUnits, T>.Straight.Value + Angle<TUnits, T>.Right.Value)
                return Quadrant.Third;
            if (value == Angle<TUnits, T>.Straight.Value + Angle<TUnits, T>.Right.Value)
                return Quadrant.NegativeY;
            return Quadrant.Fourth;
        }

        public static T GetReference<TUnits, T>(AngleReduced<TUnits, T> angle)
            where TUnits : IAngleUnits<TUnits>
            where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        {
            var quadrant = GetQuadrant(angle);
            return quadrant switch
            {
                Quadrant.PositiveX => Angle<TUnits, T>.Zero.Value,
                Quadrant.First => angle.Value,
                Quadrant.PositiveY => Angle<TUnits, T>.Right.Value,
                Quadrant.Second => Angle<TUnits, T>.Straight.Value - angle.Value,
                Quadrant.NegativeX => Angle<TUnits, T>.Zero.Value,
                Quadrant.Third => angle.Value - Angle<TUnits, T>.Straight.Value,
                Quadrant.NegativeY => Angle<TUnits, T>.Right.Value,
                Quadrant.Fourth => Angle<TUnits, T>.Full.Value - angle.Value,
                _ => Throw.InvalidOperationException<T>(),
            };
        }

#if !NET8_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Lerp<T, TFactor>(T a1, T a2, TFactor factor)
            where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, TFactor, T> 
            where TFactor : struct, INumberBase<TFactor>
            => (a1 * (TFactor.One - factor)) + (a2 * factor);
#endif
    }
}