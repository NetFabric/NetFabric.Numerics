
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
        public static T Aggregate<T, TOperator>(ReadOnlySpan<T> source)
            where T : struct
            where TOperator : struct, IAggregationOperator<T>
        {
            var result = TOperator.Seed;
            var resultVector = new Vector<T>(TOperator.Seed);
            nint index = 0;

            if (Vector.IsHardwareAccelerated &&
                Vector<T>.IsSupported &&
                source.Length >= Vector<T>.Count)
            {
                var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);

                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                for (nint indexVector = 0; indexVector < sourceVectors.Length; indexVector++)
                    resultVector = TOperator.Invoke(resultVector, Unsafe.Add(ref sourceVectorsRef, indexVector));

                index = source.Length - source.Length % Vector<T>.Count;
            }

            ref var sourceRef = ref MemoryMarshal.GetReference(source);
            for (; index < source.Length; index++)
                result = TOperator.Invoke(result, Unsafe.Add(ref sourceRef, index));

            return TOperator.ResultSelector(result, resultVector);
        }
    }
}