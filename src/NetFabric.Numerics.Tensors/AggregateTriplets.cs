namespace NetFabric.Numerics
{
    public static partial class Tensor  
    {
        /// <summary>
        /// Aggregates triplets of elements from a <see cref="ReadOnlySpan{T}"/> using the specified <typeparamref name="TOperator"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <typeparam name="TOperator">The type of the aggregation operator.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to aggregate triplets from.</param>
        /// <returns>A <see cref="ValueTuple{T1, T2, T3}"/> containing the aggregated triplets.</returns>
        /// <exception cref="ArgumentException">Thrown when the size of the <paramref name="source"/> span is not a multiple of 3.</exception>
        public static ValueTuple<T, T, T> AggregateTriplets<T, TOperator>(ReadOnlySpan<T> source)
            where T : struct
            where TOperator : struct, IAggregationTripletsOperator<T>
        {
            if (source.Length % 3 is not 0)
                Throw.ArgumentException(nameof(source), "source span must have a size multiple of 3.");

            var result = TOperator.Seed;

            ref var sourceRef = ref MemoryMarshal.GetReference(source);
            for (nint index = 0; index < source.Length; index += 3)
            {
                result.Item1 = TOperator.Invoke(result.Item1, Unsafe.Add(ref sourceRef, index));
                result.Item2 = TOperator.Invoke(result.Item2, Unsafe.Add(ref sourceRef, index + 1));
                result.Item3 = TOperator.Invoke(result.Item3, Unsafe.Add(ref sourceRef, index + 2));
            }

            return result;
        }
    }
}