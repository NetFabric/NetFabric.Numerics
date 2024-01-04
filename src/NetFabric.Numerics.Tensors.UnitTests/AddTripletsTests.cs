using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddTripletsTests
{
    public static TheoryData<int> AddData 
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void Add_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = Enumerable.Range(0, count);
        var x = source
            .Select(value => new MyVector3<T>(T.CreateChecked(value), T.CreateChecked(value + 1), T.CreateChecked(value + 2)))
            .ToArray();
        var y = source
            .Select(value => new MyVector3<T>(T.CreateChecked(value + 2), T.CreateChecked(value + 3), T.CreateChecked(value + 4)))
            .ToArray();
        var result = new MyVector3<T>[count];
        var expected = source
            .Select(value => new MyVector3<T>(T.CreateChecked(value + value + 2), T.CreateChecked(value + value + 4), T.CreateChecked(value + value + 6)))
            .ToArray();

        // act
        Tensor.Add(
            MemoryMarshal.Cast<MyVector3<T>, T>(x), 
            MemoryMarshal.Cast<MyVector3<T>, T>(y), 
            MemoryMarshal.Cast<MyVector3<T>, T>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Short_Should_Succeed(int count)
        => Add_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Int_Should_Succeed(int count)
        => Add_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Long_Should_Succeed(int count)
        => Add_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Half_Should_Succeed(int count)
        => Add_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Float_Should_Succeed(int count)
        => Add_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Double_Should_Succeed(int count)
        => Add_Should_Succeed<double>(count);
}
