using System.Numerics;
using System.Runtime.CompilerServices;

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
                
        public static Quadrant GetQuadrant<TUnits, T>(Angle<TUnits, T> angle, out T reduced)
            where TUnits : IAngleUnits<TUnits>
            where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        {
            reduced = Reduce(angle);
            return reduced < Angle<TUnits, T>.Right.Value
                ? Quadrant.First
                : reduced < Angle<TUnits, T>.Straight.Value
                    ? Quadrant.Second
                    : reduced < Angle<TUnits, T>.Straight.Value + Angle<TUnits, T>.Right.Value
                        ? Quadrant.Third
                        : Quadrant.Fourth;
        }

        public static T GetReference<TUnits, T>(Angle<TUnits, T> angle)
            where TUnits : IAngleUnits<TUnits>
            where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        {
            var quadrant = GetQuadrant(angle, out var reduced);
            return quadrant switch
            {
                Quadrant.First => reduced,
                Quadrant.Second => Angle<TUnits, T>.Straight.Value - reduced,
                Quadrant.Third => reduced - Angle<TUnits, T>.Straight.Value,
                Quadrant.Fourth => Angle<TUnits, T>.Full.Value - reduced,
                _ => Throw.InvalidOperationException<T>(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Lerp<T, TFactor>(T a1, T a2, TFactor t)
            where T : struct, IFloatingPoint<T>
            where TFactor : struct, IFloatingPoint<TFactor>
            => T.CreateChecked(((TFactor.One - t) * TFactor.CreateChecked(a1)) + (t * TFactor.CreateChecked(a2)));
    }
}