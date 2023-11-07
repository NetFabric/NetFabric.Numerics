using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Tensor
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool SpansOverlapAndAreNotSame<T>(ReadOnlySpan<T> span1, ReadOnlySpan<T> span2)
        => !Unsafe.AreSame(ref MemoryMarshal.GetReference(span1), ref MemoryMarshal.GetReference(span2)) && span1.Overlaps(span2);

    [SkipLocalsInit]
    public static void Apply<T, TOperation>(ReadOnlySpan<T> source, ref TOperation operation)
        where T : struct
        where TOperation : struct, IUnaryTensorOperation<T>
    {
        // Initialize the index to 0.
        nint index = 0;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the source is greater than the Vector<T>.Count.
        if (Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported &&
            source.Length > Vector<T>.Count)
        {
            // Cast the source span to vectors for hardware acceleration.
            var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);

            // Iterate through the source vectors and accumulate their sum.
            ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
            for (nint indexVector = 0; indexVector < sourceVectors.Length; indexVector++)
                operation.Apply(in Unsafe.Add(ref sourceVectorsRef, indexVector));

            // Update the index to the end of the last complete vector.
            index = source.Length - source.Length % Vector<T>.Count;
        }

        // Iterate through the remaining elements of the source span.
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        for (; index < source.Length; index++)
            operation.Apply(in Unsafe.Add(ref sourceRef, index));
    }

    [SkipLocalsInit]
    public static void Apply2D<T, TOperation>(ReadOnlySpan<T> source, ref TOperation operation)
        where T : struct
        where TOperation : struct, IUnary2DTensorOperation<T>
    {
        if (source.Length % 2 != 0)
            Throw.ArgumentException(nameof(source), "Source doesn't have an even length.");

        // Initialize the index to 0.
        nint index = 0;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the source is greater than the Vector<T>.Count.
        if (Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported &&
            source.Length > Vector<T>.Count)
        {
            // Cast the source span to vectors for hardware acceleration.
            var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);

            // Iterate through the source vectors and accumulate their sum.
            ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
            for (nint indexVector = 0; indexVector < sourceVectors.Length; indexVector++)
                operation.Apply(in Unsafe.Add(ref sourceVectorsRef, indexVector));

            // Update the index to the end of the last complete vector.
            index = source.Length - source.Length % Vector<T>.Count;
        }

        // Iterate through the remaining elements of the source span.
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        for (; index < source.Length; index += 2)
        {
            operation.Apply(in Unsafe.Add(ref sourceRef, index), in Unsafe.Add(ref sourceRef, index + 1));
        }
    }

    [SkipLocalsInit]
    public static void Apply<T, TOperation>(ReadOnlySpan<T> source, Span<T> destination, ref TOperation operation)
        where T : struct
        where TOperation : struct, IBinaryTensorOperation<T>
    {
        if (source.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");
        if(SpansOverlapAndAreNotSame(source, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with source.");
                    
        // Initialize the index to 0.
        nint index = 0;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the source is greater than the Vector<T>.Count.
        if (Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported &&
            source.Length > Vector<T>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);
            var destinationVectors = MemoryMarshal.Cast<T, Vector<T>>(destination);

            // Iterate through the vectors.
            ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (var indexVector = 0; indexVector < sourceVectors.Length; indexVector++)
            {                
                operation.Apply(
                    in Unsafe.Add(ref sourceVectorsRef, indexVector),
                    ref Unsafe.Add(ref destinationVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            index = source.Length - source.Length % Vector<T>.Count;
        }

        // Iterate through the remaining elements.
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; index < source.Length; index++)
        {
            operation.Apply(
                in Unsafe.Add(ref sourceRef, index),
                ref Unsafe.Add(ref destinationRef, index));
        }
    }

    [SkipLocalsInit]
    public static void Apply2D<T, TOperation>(ReadOnlySpan<T> source, Span<T> destination, ref TOperation operation)
        where T : struct
        where TOperation : struct, IBinary2DTensorOperation<T>
    {
        if (source.Length % 2 != 0)
            Throw.ArgumentException(nameof(source), "Source doesn't have an even length.");
        if (source.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");
        if(SpansOverlapAndAreNotSame(source, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with source.");
                    
        // Initialize the index to 0.
        nint index = 0;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the source is greater than the Vector<T>.Count.
        if (Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported &&
            source.Length > Vector<T>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);
            var destinationVectors = MemoryMarshal.Cast<T, Vector<T>>(destination);

            // Iterate through the vectors.
            ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (var indexVector = 0; indexVector < sourceVectors.Length; indexVector++)
            {                
                operation.Apply(
                    in Unsafe.Add(ref sourceVectorsRef, indexVector),
                    ref Unsafe.Add(ref destinationVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            index = source.Length - source.Length % Vector<T>.Count;
        }

        // Iterate through the remaining elements.
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; index < source.Length; index += 2)
        {
            operation.Apply(
                in Unsafe.Add(ref sourceRef, index),
                in Unsafe.Add(ref sourceRef, index + 1),
                ref Unsafe.Add(ref destinationRef, index),
                ref Unsafe.Add(ref destinationRef, index + 1));
        }
    }

    [SkipLocalsInit]
    public static void Apply<T, TOperation>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination, ref TOperation operation)
        where T : struct
        where TOperation : struct, ITernaryTensorOperation<T>
    {
        if(left.Length != right.Length)
            Throw.ArgumentException(nameof(right), "Source and values spans must have the same length.");
        if (left.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");
        if(SpansOverlapAndAreNotSame(left, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with source.");
        if(SpansOverlapAndAreNotSame(right, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with values.");
                    
        // Initialize the index to 0.
        nint index = 0;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the source is greater than the Vector<T>.Count.
        if (Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported &&
            left.Length > Vector<T>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var leftVectors = MemoryMarshal.Cast<T, Vector<T>>(left);
            var rightVectors = MemoryMarshal.Cast<T, Vector<T>>(right);
            var destinationVectors = MemoryMarshal.Cast<T, Vector<T>>(destination);

            // Iterate through the vectors.
            ref var leftVectorsRef = ref MemoryMarshal.GetReference(leftVectors);
            ref var rightVectorsRef = ref MemoryMarshal.GetReference(rightVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (var indexVector = 0; indexVector < leftVectors.Length; indexVector++)
            {                
                operation.Apply(
                    in Unsafe.Add(ref leftVectorsRef, indexVector),
                    in Unsafe.Add(ref rightVectorsRef, indexVector),
                    ref Unsafe.Add(ref destinationVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            index = left.Length - left.Length % Vector<T>.Count;
        }

        // Iterate through the remaining elements.
        ref var leftRef = ref MemoryMarshal.GetReference(left);
        ref var rightRef = ref MemoryMarshal.GetReference(right);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; index < left.Length; index++)
        {
            operation.Apply(
                in Unsafe.Add(ref leftRef, index),
                in Unsafe.Add(ref rightRef, index),
                ref Unsafe.Add(ref destinationRef, index));
        }
    }
}