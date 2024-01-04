using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddMultiplyPairsTests
{
    public static TheoryData<int> AddData 
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void AddMultiply_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = Enumerable.Range(0, count);
        var x = source
            .Select(value => new MyVector2<T>(T.CreateChecked(value), T.CreateChecked(value + 1)))
            .ToArray();
        var y = source
            .Select(value => new MyVector2<T>(T.CreateChecked(value + 2), T.CreateChecked(value + 3)))
            .ToArray();
        var z = source
            .Select(value => new MyVector2<T>(T.CreateChecked(value + 4), T.CreateChecked(value + 5)))
            .ToArray();
        var result = new MyVector2<T>[count];
        var expected = source
            .Select(value => new MyVector2<T>(T.CreateChecked((value + value + 2) * (value + 4)), T.CreateChecked((value + value + 4) * (value + 5))))
            .ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<MyVector2<T>, T>(x),
            MemoryMarshal.Cast<MyVector2<T>, T>(y),
            MemoryMarshal.Cast<MyVector2<T>, T>(z),
            MemoryMarshal.Cast<MyVector2<T>, T>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Short_Should_Succeed(int count)
        => AddMultiply_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Int_Should_Succeed(int count)
        => AddMultiply_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Long_Should_Succeed(int count)
        => AddMultiply_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Half_Should_Succeed(int count)
        => AddMultiply_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Float_Should_Succeed(int count)
        => AddMultiply_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Double_Should_Succeed(int count)
        => AddMultiply_Should_Succeed<double>(count);

}
