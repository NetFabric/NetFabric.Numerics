using System.Linq;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class SumPairsTests
{
    public static TheoryData<int> SumData 
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumPairs_Short_Should_Succeed(int count)
    {
        // arrange
        var source = Enumerable.Range(0, count).Select(value => ((short)value, (short)(value + 1))).ToArray();
        var expected = source.Aggregate((0, 0), (sum, value) => (sum.Item1 + value.Item1, sum.Item2 + value.Item2));

        // act
        var result = Tensor.SumPairs<short>(MemoryMarshal.Cast<ValueTuple<short, short>, short>(source));

        // assert
        result.Item1.Should().Be((short)expected.Item1);
        result.Item2.Should().Be((short)expected.Item2);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumPairs_Int_Should_Succeed(int count)
    {
        // arrange
        var source = Enumerable.Range(0, count).Select(value => (value, value + 1)).ToArray();
        var expected = source.Aggregate((0, 0), (sum, value) => (sum.Item1 + value.Item1, sum.Item2 + value.Item2));

        // act
        var result = Tensor.SumPairs<int>(MemoryMarshal.Cast<ValueTuple<int, int>, int>(source));

        // assert
        result.Item1.Should().Be(expected.Item1);
        result.Item2.Should().Be(expected.Item2);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumPairs_Long_Should_Succeed(int count)
    {
        // arrange
        var source = Enumerable.Range(0, count).Select(value => ((long)value, (long)(value + 1))).ToArray();
        var expected = source.Aggregate((0l, 0l), (sum, value) => (sum.Item1 + value.Item1, sum.Item2 + value.Item2));

        // act
        var result = Tensor.SumPairs<long>(MemoryMarshal.Cast<ValueTuple<long, long>, long>(source));

        // assert
        result.Item1.Should().Be(expected.Item1);
        result.Item2.Should().Be(expected.Item2);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumPairs_Float_Should_Succeed(int count)
    {
        // arrange
        var source = Enumerable.Range(0, count).Select(value => ((float)value, (float)(value + 1))).ToArray();
        var expected = source.Aggregate((0.0f, 0.0f), (sum, value) => (sum.Item1 + value.Item1, sum.Item2 + value.Item2));

        // act
        var result = Tensor.SumPairs<float>(MemoryMarshal.Cast<ValueTuple<float, float>, float>(source));

        // assert
        result.Item1.Should().Be(expected.Item1);
        result.Item2.Should().Be(expected.Item2);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumPairs_Double_Should_Succeed(int count)
    {
        // arrange
        var source = Enumerable.Range(0, count).Select(value => ((double)value, (double)(value + 1))).ToArray();
        var expected = source.Aggregate((0.0, 0.0), (sum, value) => (sum.Item1 + value.Item1, sum.Item2 + value.Item2));

        // act
        var result = Tensor.SumPairs<double>(MemoryMarshal.Cast<ValueTuple<double, double>, double>(source));

        // assert
        result.Item1.Should().Be(expected.Item1);
        result.Item2.Should().Be(expected.Item2);
    }
}
