namespace NetFabric.Numerics
{
    static class Utils
    {
#if !NET8_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Lerp<T, TFactor>(T a1, T a2, TFactor factor)
            where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, TFactor, T> 
            where TFactor : struct, INumberBase<TFactor>
            => (a1 * (TFactor.One - factor)) + (a2 * factor);
#endif
    }
}