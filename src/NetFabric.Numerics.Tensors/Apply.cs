using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Tensor
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool SpansOverlapAndAreNotSame<T>(ReadOnlySpan<T> span1, ReadOnlySpan<T> span2)
        => !Unsafe.AreSame(ref MemoryMarshal.GetReference(span1), ref MemoryMarshal.GetReference(span2)) && span1.Overlaps(span2);

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
            index = x.Length - x.Length % Vector<T>.Count;
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

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct
        where TOperator : struct, IBinaryOperator<T>
    {
        if(x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "x and y spans must have the same length.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");
        if(SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if(SpansOverlapAndAreNotSame(y, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with y.");
                    
        // Initialize the index to 0.
        nint index = 0;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported &&
            x.Length >= Vector<T>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T, Vector<T>>(x);
            var yVectors = MemoryMarshal.Cast<T, Vector<T>>(y);
            var destinationVectors = MemoryMarshal.Cast<T, Vector<T>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (nint indexVector = 0; indexVector < xVectors.Length; indexVector++)
            {                
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    Unsafe.Add(ref xVectorsRef, indexVector),
                    Unsafe.Add(ref yVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            index = x.Length - x.Length % Vector<T>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; index < x.Length; index++)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(
                Unsafe.Add(ref xRef, index),
                Unsafe.Add(ref yRef, index));
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct
        where TOperator : struct, IBinaryOperator<T>
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
            var xVectors = MemoryMarshal.Cast<T, Vector<T>>(x);
            var valueVector = new Vector<T>(y);
            var destinationVectors = MemoryMarshal.Cast<T, Vector<T>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (nint indexVector = 0; indexVector < xVectors.Length; indexVector++)
            {                
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    Unsafe.Add(ref xVectorsRef, indexVector),
                    valueVector);
            }

            // Update the index to the end of the last complete vector.
            index = x.Length - x.Length % Vector<T>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; index < x.Length; index++)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(
                Unsafe.Add(ref xRef, index),
                y);
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T>
    {
        if(x.Length != y.Length || x.Length != z.Length)
            Throw.ArgumentException(nameof(x), "x, y and z spans must have the same length.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");
        if(SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if(SpansOverlapAndAreNotSame(y, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with y.");
        if(SpansOverlapAndAreNotSame(z, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with z.");
                    
        // Initialize the index to 0.
        nint index = 0;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported &&
            x.Length >= Vector<T>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T, Vector<T>>(x);
            var yVectors = MemoryMarshal.Cast<T, Vector<T>>(y);
            var zVectors = MemoryMarshal.Cast<T, Vector<T>>(z);
            var destinationVectors = MemoryMarshal.Cast<T, Vector<T>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
            ref var zVectorsRef = ref MemoryMarshal.GetReference(zVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (nint indexVector = 0; indexVector < xVectors.Length; indexVector++)
            {                
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    Unsafe.Add(ref xVectorsRef, indexVector),
                    Unsafe.Add(ref yVectorsRef, indexVector),
                    Unsafe.Add(ref zVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            index = x.Length - x.Length % Vector<T>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        ref var zRef = ref MemoryMarshal.GetReference(z);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; index < x.Length; index++)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(
                Unsafe.Add(ref xRef, index),
                Unsafe.Add(ref yRef, index),
                Unsafe.Add(ref zRef, index));
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, T y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T>
    {
        if(x.Length != z.Length)
            Throw.ArgumentException(nameof(x), "x and z spans must have the same length.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");
        if(SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if(SpansOverlapAndAreNotSame(z, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with z.");
                    
        // Initialize the index to 0.
        nint index = 0;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported &&
            x.Length >= Vector<T>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T, Vector<T>>(x);
            var yVector = new Vector<T>(y);
            var zVectors = MemoryMarshal.Cast<T, Vector<T>>(z);
            var destinationVectors = MemoryMarshal.Cast<T, Vector<T>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var zVectorsRef = ref MemoryMarshal.GetReference(zVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (nint indexVector = 0; indexVector < xVectors.Length; indexVector++)
            {                
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    Unsafe.Add(ref xVectorsRef, indexVector),
                    yVector,
                    Unsafe.Add(ref zVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            index = x.Length - x.Length % Vector<T>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var zRef = ref MemoryMarshal.GetReference(z);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; index < x.Length; index++)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(
                Unsafe.Add(ref xRef, index),
                y,
                Unsafe.Add(ref zRef, index));
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, T z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T>
    {
        if(x.Length != y.Length)
            Throw.ArgumentException(nameof(x), "x and y spans must have the same length.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");
        if(SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if(SpansOverlapAndAreNotSame(y, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with y.");
                    
        // Initialize the index to 0.
        nint index = 0;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported &&
            x.Length >= Vector<T>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T, Vector<T>>(x);
            var yVectors = MemoryMarshal.Cast<T, Vector<T>>(y);
            var zVector = new Vector<T>(z);
            var destinationVectors = MemoryMarshal.Cast<T, Vector<T>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (nint indexVector = 0; indexVector < xVectors.Length; indexVector++)
            {                
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    Unsafe.Add(ref xVectorsRef, indexVector),
                    Unsafe.Add(ref yVectorsRef, indexVector),
                    zVector);
            }

            // Update the index to the end of the last complete vector.
            index = x.Length - x.Length % Vector<T>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; index < x.Length; index++)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(
                Unsafe.Add(ref xRef, index),
                Unsafe.Add(ref yRef, index),
                z);
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, T y, T z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T>
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
            var xVectors = MemoryMarshal.Cast<T, Vector<T>>(x);
            var yVector = new Vector<T>(y);
            var zVector = new Vector<T>(z);
            var destinationVectors = MemoryMarshal.Cast<T, Vector<T>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (nint indexVector = 0; indexVector < xVectors.Length; indexVector++)
            {                
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    Unsafe.Add(ref xVectorsRef, indexVector),
                    yVector,
                    zVector);
            }

            // Update the index to the end of the last complete vector.
            index = x.Length - x.Length % Vector<T>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; index < x.Length; index++)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(
                Unsafe.Add(ref xRef, index),
                y,
                z);
        }
    }

}