
using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics
{
    public static partial class Tensor
    {
        /// <summary>
        /// Aggregates the elements in the specified <see cref="ReadOnlySpan{T}"/> using the specified <see cref="IAggregationOperator{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the span.</typeparam>
        /// <typeparam name="TOperator">The type of the aggregation operator.</typeparam>
        /// <param name="source">The source span.</param>
        /// <returns>The aggregated value.</returns>
        public static ValueTuple<T, T> AggregatePairs<T, TOperator>(ReadOnlySpan<T> source)
            where T : struct
            where TOperator : struct, IAggregationPairsOperator<T>
        {
            if (source.Length % 2 is not 0)
                Throw.ArgumentException(nameof(source), "source span must have an even size.");

            var result = TOperator.Seed;
            var resultVector = Vector<T>.Zero;

            nint index = 0;

            if (Vector.IsHardwareAccelerated &&
                Vector<T>.IsSupported &&
                Vector<T>.Count > 2 &&
                Vector<T>.Count % 2 is 0 &&
                source.Length >= Vector<T>.Count)
            {
                var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);
                resultVector = GetVector(TOperator.Seed);

                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                for (nint indexVector = 0; indexVector < sourceVectors.Length; indexVector++)
                    resultVector = TOperator.Invoke(resultVector, Unsafe.Add(ref sourceVectorsRef, indexVector));

                index = source.Length - source.Length % Vector<T>.Count;
            }

            ref var sourceRef = ref MemoryMarshal.GetReference(source);
            for (; index < source.Length; index += 2)
            {
                result.Item1 = TOperator.Invoke(result.Item1, Unsafe.Add(ref sourceRef, index));
                result.Item2 = TOperator.Invoke(result.Item2, Unsafe.Add(ref sourceRef, index + 1));
            }

            return TOperator.ResultSelector(result, resultVector);
        }
    }
}