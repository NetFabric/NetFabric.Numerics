namespace NetFabric.Numerics;

public static partial class Tensor
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool SpansOverlapAndAreNotSame<T>(ReadOnlySpan<T> span1, ReadOnlySpan<T> span2)
        => !Unsafe.AreSame(ref MemoryMarshal.GetReference(span1), ref MemoryMarshal.GetReference(span2)) && span1.Overlaps(span2);

    static Vector<T> GetVector<T>(ValueTuple<T, T> tuple)
        where T : struct
    {
        var array = new T[Vector<T>.Count];
        ref var resultRef = ref MemoryMarshal.GetReference<T>(array);
        for (nint indexVector = 0; indexVector<array.Length; indexVector += 2)
        {
            Unsafe.Add(ref resultRef, indexVector) = tuple.Item1;
            Unsafe.Add(ref resultRef, indexVector + 1) = tuple.Item2;
        }
        return new Vector<T>(array);
    }

}