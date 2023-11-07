using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

[StructLayout(LayoutKind.Auto)]
public readonly struct SubtractValueOperation<T> 
    : IBinaryTensorOperation<T>
    where T : struct, ISubtractionOperators<T, T, T>
{
    readonly T value;
    readonly Vector<T> valueVector;

    public SubtractValueOperation(T value) 
    {
        this.value = value;
        valueVector = new Vector<T>(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IBinaryTensorOperation<T>.Apply(ref readonly T sourceItem, ref T destinationItem) 
        => destinationItem = sourceItem - value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IBinaryTensorOperation<T>.Apply(ref readonly Vector<T> sourceVector, ref Vector<T> destinationVector)
        => destinationVector = sourceVector - valueVector;
}

[StructLayout(LayoutKind.Auto)]
public readonly struct SubtractValueOperation2D<T> 
    : IBinary2DTensorOperation<T>
    where T : struct, ISubtractionOperators<T, T, T>
{
    readonly T value1;
    readonly T value2;
    readonly Vector<T> valueVector;

    public SubtractValueOperation2D(T value1, T value2) 
    {
        this.value1 = value1;
        this.value2 = value2;

        valueVector = new Vector<T>();
        valueVector.Fill(value1, value2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IBinary2DTensorOperation<T>.Apply(ref readonly T sourceItem1, ref readonly T sourceItem2, ref T destinationItem1, ref T destinationItem2) 
    {
        destinationItem1 = sourceItem1 - value1;
        destinationItem2 = sourceItem2 - value2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IBinary2DTensorOperation<T>.Apply(ref readonly Vector<T> sourceVector, ref Vector<T> destinationVector)
        => destinationVector = sourceVector - valueVector;
}

public readonly struct SubtractOperation<T> 
    : ITernaryTensorOperation<T>
    where T : struct, ISubtractionOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void ITernaryTensorOperation<T>.Apply(ref readonly T leftItem, ref readonly T rightItem, ref T destinationItem) 
        => destinationItem = leftItem - rightItem;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void ITernaryTensorOperation<T>.Apply(ref readonly Vector<T> leftVector, ref readonly Vector<T> rightVector, ref Vector<T> destinationVector)
        => destinationVector = leftVector - rightVector;
}