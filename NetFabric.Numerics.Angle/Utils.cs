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
    }
}