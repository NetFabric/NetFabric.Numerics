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

        // Source: https://github.com/dotnet/dotnet/blob/f20eddc465ad40b7620e848b88a43fe6f6741e59/src/runtime/src/libraries/System.Linq/src/System/Linq/Enumerable.cs#L27
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // fast type checks that don't add a lot of overhead
        public static bool TryGetSpan<TSource>(this IEnumerable<TSource> source, out ReadOnlySpan<TSource> span)
            // This constraint isn't required, but the overheads involved here can be more substantial when TSource
            // is a reference type and generic implementations are shared.  So for now we're protecting ourselves
            // and forcing a conscious choice to remove this in the future, at which point it should be paired with
            // sufficient performance testing.
            where TSource : struct
        {
            // Use `GetType() == typeof(...)` rather than `is` to avoid cast helpers.  This is measurably cheaper
            // but does mean we could end up missing some rare cases where we could get a span but don't (e.g. a uint[]
            // masquerading as an int[]).  That's an acceptable tradeoff.  The Unsafe usage is only after we've
            // validated the exact type; this could be changed to a cast in the future if the JIT starts to recognize it.
            // We only pay the comparison/branching costs here for super common types we expect to be used frequently
            // with LINQ methods.

            bool result = true;
            if (source.GetType() == typeof(TSource[]))
            {
                span = Unsafe.As<TSource[]>(source);
            }
            else if (source.GetType() == typeof(List<TSource>))
            {
                span = CollectionsMarshal.AsSpan(Unsafe.As<List<TSource>>(source));
            }
            else
            {
                span = default;
                result = false;
            }

            return result;
        }
    }
}