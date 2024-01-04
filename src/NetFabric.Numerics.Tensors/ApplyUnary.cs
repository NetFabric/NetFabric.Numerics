namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct
        where TOperator : struct, IUnaryOperator<T>
    {
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");
        if(SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
                    
        // Initialize the index to 0.
        nint index = 0;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported &&
            x.Length >= Vector<T>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(x);
            var destinationVectors = MemoryMarshal.Cast<T, Vector<T>>(destination);

            // Iterate through the vectors.
            ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (nint indexVector = 0; indexVector < sourceVectors.Length; indexVector++)
            {                
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    Unsafe.Add(ref sourceVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            index = x.Length - (x.Length % Vector<T>.Count);
        }

        // Iterate through the remaining elements.
        ref var sourceRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; index < x.Length; index++)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(
                Unsafe.Add(ref sourceRef, index));
        }
    }

}