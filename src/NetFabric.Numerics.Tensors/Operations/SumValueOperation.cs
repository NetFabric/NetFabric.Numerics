using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

[StructLayout(LayoutKind.Auto)]
public struct SumOperation<T> 
    : IUnaryTensorOperation<T>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    T sum;
    Vector<T> sumVector;

    public SumOperation()
    {
        sum = T.AdditiveIdentity;
        sumVector = Vector<T>.Zero;
    }

    public readonly T Result
        => Vector.Sum(sumVector) + sum;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IUnaryTensorOperation<T>.Apply(ref readonly T item) 
        => sum += item;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IUnaryTensorOperation<T>.Apply(ref readonly Vector<T> vector)
        => sumVector += vector;
}

[StructLayout(LayoutKind.Auto)]
public struct SumOperation2D<T> 
    : IUnary2DTensorOperation<T>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    T sum1;
    T sum2;
    Vector<T> sumVector;

    public SumOperation2D()
    {
        sum1 = T.AdditiveIdentity;
        sum2 = T.AdditiveIdentity;
        sumVector = Vector<T>.Zero;
    }

    public readonly (T, T) Result
    {
        get
        {
            var sumX = sum1;
            var sumY = sum2;

            // Add the sum of the vector elements to the overall result.
            ref var sumVectorRef = ref Unsafe.As<Vector<T>, T>(ref Unsafe.AsRef(in sumVector));
            for (nint index = 0; index < Vector<T>.Count; index += 2)
            {
                sumX += Unsafe.Add(ref sumVectorRef, index);
                sumY += Unsafe.Add(ref sumVectorRef, index + 1);
            } 

            return (sumX, sumY);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IUnary2DTensorOperation<T>.Apply(ref readonly T item1, ref readonly T item2) 
    {
        sum1 += item1;
        sum2 += item2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IUnary2DTensorOperation<T>.Apply(ref readonly Vector<T> vector)
        => sumVector += vector;
}