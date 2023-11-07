namespace NetFabric.Numerics;

public interface IUnaryTensorOperation<T>
{
    void Apply(ref readonly T item);
    void Apply(ref readonly Vector<T> vector);
}

public interface IUnary2DTensorOperation<T>
{
    void Apply(ref readonly T item1, ref readonly T item2);
    void Apply(ref readonly Vector<T> vector);
}

public interface IBinaryTensorOperation<T>
{
    void Apply(ref readonly T sourceItem, ref T destinationItem);
    void Apply(ref readonly Vector<T> sourceVector, ref Vector<T> destinationVector);
}

public interface IBinary2DTensorOperation<T>
{
    void Apply(ref readonly T sourceItem1, ref readonly T sourceItem2, ref T destinationItem1, ref T destinationItem2);
    void Apply(ref readonly Vector<T> sourceVector, ref Vector<T> destinationVector);
}

public interface ITernaryTensorOperation<T>
{
    void Apply(ref readonly T leftItem, ref readonly T rightItem, ref T destinationItem);
    void Apply(ref readonly Vector<T> leftVector, ref readonly Vector<T> rightVector, ref Vector<T> destinationVector);
}