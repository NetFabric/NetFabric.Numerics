using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

[StructLayout(LayoutKind.Auto)]
public readonly struct MultiplyValueOperation<T> 
    : IBinaryTensorOperation<T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    readonly T value;
    readonly Vector<T> valueVector;

    public MultiplyValueOperation(T value) 
    {
        this.value = value;
        valueVector = new Vector<T>(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IBinaryTensorOperation<T>.Apply(ref readonly T sourceItem, ref T destinationItem) 
        => destinationItem = sourceItem * value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IBinaryTensorOperation<T>.Apply(ref readonly Vector<T> sourceVector, ref Vector<T> destinationVector)
        => destinationVector = sourceVector * valueVector;
}

public readonly struct MultiplyOperation<T> 
    : ITernaryTensorOperation<T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void ITernaryTensorOperation<T>.Apply(ref readonly T leftItem, ref readonly T rightItem, ref T destinationItem) 
        => destinationItem = leftItem * rightItem;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void ITernaryTensorOperation<T>.Apply(ref readonly Vector<T> leftVector, ref readonly Vector<T> rightVector, ref Vector<T> destinationVector)
        => destinationVector = leftVector * rightVector;
}