using System.Linq;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddMultiplyValueTests
{
    public static TheoryData<int> AddData 
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Short_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (short)value).ToArray();
        var y = (short)24;
        var z = Enumerable.Range(0, count).Select(value => (short)(value + 2)).ToArray();
        var result = new short[count];
        var expected = Enumerable.Range(0, count).Select(value => (short)((value + 24) * (value + 2))).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Short_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (short)value).ToArray();
        var y = Enumerable.Range(0, count).Select(value => (short)(value + 1)).ToArray();
        var z = (short)42;
        var result = new short[count];
        var expected = Enumerable.Range(0, count).Select(value => (short)((value + value + 1) * 42)).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Short_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (short)value).ToArray();
        var y = (short)24;
        var z = (short)42;
        var result = new short[count];
        var expected = Enumerable.Range(0, count).Select(value => (short)((value + 24) * 42)).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Int_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).ToArray();
        var y = 24;
        var z = Enumerable.Range(0, count).Select(value => value + 2).ToArray();
        var result = new int[count];
        var expected = Enumerable.Range(0, count).Select(value => (value + 24) * (value + 2)).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Int_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).ToArray();
        var y = Enumerable.Range(0, count).Select(value => value + 1).ToArray();
        var z = 42;
        var result = new int[count];
        var expected = Enumerable.Range(0, count).Select(value => (value + value + 1) * 42).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Int_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).ToArray();
        var y = 24;
        var z = 42;
        var result = new int[count];
        var expected = Enumerable.Range(0, count).Select(value => (value + 24) * 42).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Long_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (long)value).ToArray();
        var y = 24l;
        var z = Enumerable.Range(0, count).Select(value => (long)(value + 2)).ToArray();
        var result = new long[count];
        var expected = Enumerable.Range(0, count).Select(value => (long)((value + 24) * (value + 2))).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Long_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (long)value).ToArray();
        var y = Enumerable.Range(0, count).Select(value => (long)(value + 1)).ToArray();
        var z = 42l;
        var result = new long[count];
        var expected = Enumerable.Range(0, count).Select(value => (long)((value + value + 1) * 42)).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Long_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (long)value).ToArray();
        var y = 24l;
        var z = 42l;
        var result = new long[count];
        var expected = Enumerable.Range(0, count).Select(value => (long)((value + 24) * 42)).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Float_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (float)value).ToArray();
        var y = 24f;
        var z = Enumerable.Range(0, count).Select(value => (float)(value + 2)).ToArray();
        var result = new float[count];
        var expected = Enumerable.Range(0, count).Select(value => (float)((value + 24) * (value + 2))).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Float_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (float)value).ToArray();
        var y = Enumerable.Range(0, count).Select(value => (float)(value + 1)).ToArray();
        var z = 42f;
        var result = new float[count];
        var expected = Enumerable.Range(0, count).Select(value => (float)((value + value + 1) * 42)).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Float_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (float)value).ToArray();
        var y = 24f;
        var z = 42f;
        var result = new float[count];
        var expected = Enumerable.Range(0, count).Select(value => (float)((value + 24) * 42)).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Double_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (double)value).ToArray();
        var y = 24d;
        var z = Enumerable.Range(0, count).Select(value => (double)(value + 2)).ToArray();
        var result = new double[count];
        var expected = Enumerable.Range(0, count).Select(value => (double)((value + 24) * (value + 2))).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Double_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (double)value).ToArray();
        var y = Enumerable.Range(0, count).Select(value => (double)(value + 1)).ToArray();
        var z = 42d;
        var result = new double[count];
        var expected = Enumerable.Range(0, count).Select(value => (double)((value + value + 1) * 42)).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Double_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (double)value).ToArray();
        var y = 24d;
        var z = 42d;
        var result = new double[count];
        var expected = Enumerable.Range(0, count).Select(value => (double)((value + 24) * 42)).ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        result.Should().Equal(expected);
    }

}
