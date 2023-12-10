using System.Linq;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddValueTests
{
    public const int constValue = 42;

    public static TheoryData<int> AddData 
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Value_Short_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (short)value).ToArray();
        var result = new short[count];
        var expected = Enumerable.Range(0, count).Select(value => (short)(value + constValue)).ToArray();

        // act
        Tensor.Add<short>(x, constValue, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Value_Int_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).ToArray();
        var result = new int[count];
        var expected = Enumerable.Range(0, count).Select(value => value + constValue).ToArray();

        // act
        Tensor.Add<int>(x, constValue, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Value_Long_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (long)value).ToArray();
        var result = new long[count];
        var expected = Enumerable.Range(0, count).Select(value => (long)(value + constValue)).ToArray();

        // act
        Tensor.Add<long>(x, constValue, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Value_Float_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (float)value).ToArray();
        var result = new float[count];
        var expected = Enumerable.Range(0, count).Select(value => (float)(value + constValue)).ToArray();

        // act
        Tensor.Add<float>(x, constValue, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Value_Double_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (double)value).ToArray();
        var result = new double[count];
        var expected = Enumerable.Range(0, count).Select(value => (double)(value + constValue)).ToArray();

        // act
        Tensor.Add<double>(x, constValue, result);

        // assert
        result.Should().Equal(expected);
    }

}
