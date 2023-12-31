using System.Linq;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class SumTests
{
    public static TheoryData<int> SumData 
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Short_Should_Succeed(int count)
    {
        // arrange
        var source = Enumerable.Range(0, count).Select(value => (short)value).ToArray();
        var expected = source.Aggregate(0, (sum, value) => sum + value);

        // act
        var result = Tensor.Sum<short>(source);

        // assert
        result.Should().Be((short)expected);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Int_Should_Succeed(int count)
    {
        // arrange
        var source = Enumerable.Range(0, count).ToArray();
        var expected = source.Sum();

        // act
        var result = Tensor.Sum<int>(source);

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Long_Should_Succeed(int count)
    {
        // arrange
        var source = Enumerable.Range(0, count).Select(value => (long)value).ToArray();
        var expected = source.Sum();

        // act
        var result = Tensor.Sum<long>(source);

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Float_Should_Succeed(int count)
    {
        // arrange
        var source = Enumerable.Range(0, count).Select(value => (float)value).ToArray();
        var expected = source.Sum();

        // act
        var result = Tensor.Sum<float>(source);

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Double_Should_Succeed(int count)
    {
        // arrange
        var source = Enumerable.Range(0, count).Select(value => (double)value).ToArray();
        var expected = source.Sum();

        // act
        var result = Tensor.Sum<double>(source);

        // assert
        result.Should().Be(expected);
    }

}
