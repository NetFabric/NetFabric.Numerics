using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddValuePairsTests
{
    public static TheoryData<int> AddData 
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void Add_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = Enumerable.Range(0, count);
        var x = source
            .Select(value => new MyVector2<T>(T.CreateChecked(value), T.CreateChecked(value + 1)))
            .ToArray();
        var y = (T.CreateChecked(42), T.CreateChecked(24));
        var result = new MyVector2<T>[count];
        var expected = source
            .Select(value => new MyVector2<T>(T.CreateChecked(value) + y.Item1, T.CreateChecked(value + 1) + y.Item2))
            .ToArray();

        // act
        Tensor.Add(MemoryMarshal.Cast<MyVector2<T>, T>(x), y, MemoryMarshal.Cast<MyVector2<T>, T>(result));

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
