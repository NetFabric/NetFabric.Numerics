using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics
{
    static class Utils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Lerp<T, TFactor>(T a1, T a2, TFactor t)
            where T : struct, IFloatingPoint<T>
            where TFactor : struct, IFloatingPoint<TFactor>
            => T.CreateChecked(((TFactor.One - t) * TFactor.CreateChecked(a1)) + (t * TFactor.CreateChecked(a2)));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Reduce<T>(T angle, T fullAngle)
            where T: struct, INumber<T>
        {
            var reduced = angle % fullAngle;
            if (reduced < T.Zero)
                reduced += fullAngle;
            return reduced;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quadrant GetQuadrant<T>(T angle, T rightAngle, T straightAngle, T fullAngle)
            where T: struct, INumber<T>
            => GetQuadrant(Reduce(angle, fullAngle), rightAngle, straightAngle);

        public static T GetReference<T>(T angle, T rightAngle, T straightAngle, T fullAngle)
            where T: struct, INumber<T>
        {
            var reduced = Reduce(angle, fullAngle);
            var quadrant = GetQuadrant(reduced, rightAngle, straightAngle);
            return quadrant switch
            {
                Quadrant.First => reduced,
                Quadrant.Second => straightAngle - reduced,
                Quadrant.Third => reduced - straightAngle,
                Quadrant.Fourth => fullAngle - reduced,
                _ => Throw.InvalidOperationException<T>(),
            };
        }

        static Quadrant GetQuadrant<T>(T angle, T rightAngle, T straightAngle)
            where T: struct, INumber<T>
        {
            if (angle < rightAngle)
                return Quadrant.First;

            if (angle < straightAngle)
                return Quadrant.Second;

            if (angle < straightAngle + rightAngle)
                return Quadrant.Third;

            return Quadrant.Fourth;
        }

    }
}