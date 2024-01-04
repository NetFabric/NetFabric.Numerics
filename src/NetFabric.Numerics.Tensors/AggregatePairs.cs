using System;

namespace NetFabric.Numerics
{
    public static partial class Tensor
    {

        /// <summary>
        /// Aggregates pairs of elements in a <see cref="ReadOnlySpan{T}"/> using the specified <typeparamref name="TOperator"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <typeparam name="TOperator">The type of the aggregation operator.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> containing the elements to aggregate.</param>
        /// <returns>A <see cref="ValueTuple{T1, T2}"/> representing the aggregated pairs.</returns>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="source"/> span does not have an even size.</exception>        
        public static ValueTuple<T, T> AggregatePairs<T, TOperator>(ReadOnlySpan<T> source)
            where T : struct
            where TOperator : struct, IAggregationPairsOperator<T>
        {
            if (source.Length % 2 is not 0)
                Throw.ArgumentException(nameof(source), "source span must have an even size.");

            var result = TOperator.Identity;
            ref var sourceRef = ref MemoryMarshal.GetReference(source);

            if (Vector.IsHardwareAccelerated && Vector<T>.IsSupported)
            {
                var resultVector = GetVector(TOperator.Identity);

                nint index = 0;

                if (Vector<T>.Count > 2 &&
                    Vector<T>.Count % 2 is 0 &&
                    source.Length >= Vector<T>.Count)
                {
                    var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);

                    ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                    for (nint indexVector = 0; indexVector < sourceVectors.Length; indexVector++)
                        resultVector = TOperator.Invoke(resultVector, Unsafe.Add(ref sourceVectorsRef, indexVector));

                    index = source.Length - (source.Length % Vector<T>.Count);
                }

                for (; index < source.Length; index += 2)
                {
                    result.Item1 = TOperator.Invoke(result.Item1, Unsafe.Add(ref sourceRef, index));
                    result.Item2 = TOperator.Invoke(result.Item2, Unsafe.Add(ref sourceRef, index + 1));
                }

                return TOperator.ResultSelector(result, resultVector);
            }
            else
            {
                for (nint index = 0; index < source.Length; index += 2)
                {
                    result.Item1 = TOperator.Invoke(result.Item1, Unsafe.Add(ref sourceRef, index));
                    result.Item2 = TOperator.Invoke(result.Item2, Unsafe.Add(ref sourceRef, index + 1));
                }

                return result;
            }
        }
    }
}