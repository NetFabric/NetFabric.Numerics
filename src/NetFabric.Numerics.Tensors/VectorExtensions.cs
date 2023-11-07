namespace NetFabric.Numerics;

static class VectorExtensions
{
    public static void Fill<T>(this ref Vector<T> vector, T value1, T value2)
        where T : struct
    {
        ref var valueArrayRef = ref Unsafe.As<Vector<T>, T>(ref Unsafe.AsRef(in vector));
        for (nint index = 0; index < Vector<T>.Count; index += 2)
        {
            Unsafe.Add(ref valueArrayRef, index) = value1;
            Unsafe.Add(ref valueArrayRef, index + 1) = value2;
        }
    }
}