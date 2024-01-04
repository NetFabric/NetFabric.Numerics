namespace NetFabric.Numerics;

public static partial class Tensor
{

    /// <summary>
    /// Aggregates the elements of a <see cref="ReadOnlySpan{T}"/> using the specified <typeparamref name="TOperator"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <typeparam name="TOperator">The type of the aggregation operator.</typeparam>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to aggregate.</param>
    /// <returns>The aggregated value.</returns>
    public static T Aggregate<T, TOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TOperator : struct, IAggregationOperator<T>
    {
        var result = TOperator.Identity;
        ref var sourceRef = ref MemoryMarshal.GetReference(source);

        if (Vector.IsHardwareAccelerated && Vector<T>.IsSupported)
        {
            var resultVector = new Vector<T>(TOperator.Identity);
            nint index = 0;

            if (source.Length >= Vector<T>.Count)
            {
                var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);

                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                for (nint indexVector = 0; indexVector < sourceVectors.Length; indexVector++)
                    resultVector = TOperator.Invoke(resultVector, Unsafe.Add(ref sourceVectorsRef, indexVector));

                index = source.Length - (source.Length % Vector<T>.Count);
            }

            for (; index < source.Length; index++)
                result = TOperator.Invoke(result, Unsafe.Add(ref sourceRef, index));

            return TOperator.ResultSelector(result, resultVector);
        }
        else
        {
            for (nint index = 0; index < source.Length; index++)
                result = TOperator.Invoke(result, Unsafe.Add(ref sourceRef, index));

            return result;
        }
    }
}