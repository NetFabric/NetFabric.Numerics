using System.Linq;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class SquareTests
{
    public static TheoryData<int> SquareData 
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    [Theory]
    [MemberData(nameof(SquareData))]
    public void Square_Short_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (short)value).ToArray();
        var result = new short[count];
        var expected = Enumerable.Range(0, count).Select(value => (short)(value * value)).ToArray();

        // act
        Tensor.Square<short>(x, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(SquareData))]
    public void Square_Int_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).ToArray();
        var result = new int[count];
        var expected = Enumerable.Range(0, count).Select(value => value * value).ToArray();

        // act
        Tensor.Square<int>(x, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(SquareData))]
    public void Square_Long_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (long)value).ToArray();
        var y = Enumerable.Range(0, count).Select(value => (long)(value + 1)).ToArray();
        var result = new long[count];
        var expected = Enumerable.Range(0, count).Select(value => (long)(value * value)).ToArray();

        // act
        Tensor.Square<long>(x, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(SquareData))]
    public void Square_Float_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (float)value).ToArray();
        var result = new float[count];
        var expected = Enumerable.Range(0, count).Select(value => (float)(value * value)).ToArray();

        // act
        Tensor.Square<float>(x, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(SquareData))]
    public void Square_Double_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (double)value).ToArray();
        var result = new double[count];
        var expected = Enumerable.Range(0, count).Select(value => (double)(value * value)).ToArray();

        // act
        Tensor.Square<double>(x, result);

        // assert
        result.Should().Equal(expected);
    }

}
