using System.Linq;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddTests
{
    public static TheoryData<int> AddData 
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Short_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (short)value).ToArray();
        var y = Enumerable.Range(0, count).Select(value => (short)(value + 1)).ToArray();
        var result = new short[count];
        var expected = Enumerable.Range(0, count).Select(value => (short)(value + value + 1)).ToArray();

        // act
        Tensor.Add<short>(x, y, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Int_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).ToArray();
        var y = Enumerable.Range(0, count).Select(value => value + 1).ToArray();
        var result = new int[count];
        var expected = Enumerable.Range(0, count).Select(value => value + value + 1).ToArray();

        // act
        Tensor.Add<int>(x, y, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Long_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (long)value).ToArray();
        var y = Enumerable.Range(0, count).Select(value => (long)(value + 1)).ToArray();
        var result = new long[count];
        var expected = Enumerable.Range(0, count).Select(value => (long)(value + value + 1)).ToArray();

        // act
        Tensor.Add<long>(x, y, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Float_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (float)value).ToArray();
        var y = Enumerable.Range(0, count).Select(value => (float)(value + 1)).ToArray();
        var result = new float[count];
        var expected = Enumerable.Range(0, count).Select(value => (float)(value + value + 1)).ToArray();

        // act
        Tensor.Add<float>(x, y, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Double_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (double)value).ToArray();
        var y = Enumerable.Range(0, count).Select(value => (double)(value + 1)).ToArray();
        var result = new double[count];
        var expected = Enumerable.Range(0, count).Select(value => (double)(value + value + 1)).ToArray();

        // act
        Tensor.Add<double>(x, y, result);

        // assert
        result.Should().Equal(expected);
    }

}
