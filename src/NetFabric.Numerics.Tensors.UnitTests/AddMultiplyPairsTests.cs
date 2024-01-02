using System.Linq;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddMultiplyPairsTests
{
    public static TheoryData<int> AddData 
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Short_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((short)value, (short)(value + 1))).ToArray();
        var y = Enumerable.Range(0, count).Select(value => ((short)(value + 2), (short)(value + 3))).ToArray();
        var z = Enumerable.Range(0, count).Select(value => ((short)(value + 4), (short)(value + 5))).ToArray();
        var result = new ValueTuple<short, short>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((short)((value + value + 2) * (value + 4)), (short)((value + value + 4) * (value + 5)))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<short, short>, short>(x),
            MemoryMarshal.Cast<ValueTuple<short, short>, short>(y),
            MemoryMarshal.Cast<ValueTuple<short, short>, short>(z),
            MemoryMarshal.Cast<ValueTuple<short, short>, short>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Int_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (value, value + 1)).ToArray();
        var y = Enumerable.Range(0, count).Select(value => (value + 2, value + 3)).ToArray();
        var z = Enumerable.Range(0, count).Select(value => (value + 4, value + 5)).ToArray();
        var result = new ValueTuple<int, int>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((value + value + 2) * (value + 4), (value + value + 4) * (value + 5))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<int, int>, int>(x),
            MemoryMarshal.Cast<ValueTuple<int, int>, int>(y),
            MemoryMarshal.Cast<ValueTuple<int, int>, int>(z),
            MemoryMarshal.Cast<ValueTuple<int, int>, int>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Long_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((long)value, (long)(value + 1))).ToArray();
        var y = Enumerable.Range(0, count).Select(value => ((long)(value + 2), (long)(value + 3))).ToArray();
        var z = Enumerable.Range(0, count).Select(value => ((long)(value + 4), (long)(value + 5))).ToArray();
        var result = new ValueTuple<long, long>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((long)((value + value + 2) * (value + 4)), (long)((value + value + 4) * (value + 5)))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<long, long>, long>(x),
            MemoryMarshal.Cast<ValueTuple<long, long>, long>(y),
            MemoryMarshal.Cast<ValueTuple<long, long>, long>(z),
            MemoryMarshal.Cast<ValueTuple<long, long>, long>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Float_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((float)value, (float)(value + 1))).ToArray();
        var y = Enumerable.Range(0, count).Select(value => ((float)(value + 2), (float)(value + 3))).ToArray();
        var z = Enumerable.Range(0, count).Select(value => ((float)(value + 4), (float)(value + 5))).ToArray();
        var result = new ValueTuple<float, float>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((float)((value + value + 2) * (value + 4)), (float)((value + value + 4) * (value + 5)))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<float, float>, float>(x),
            MemoryMarshal.Cast<ValueTuple<float, float>, float>(y),
            MemoryMarshal.Cast<ValueTuple<float, float>, float>(z),
            MemoryMarshal.Cast<ValueTuple<float, float>, float>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Double_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((double)value, (double)(value + 1))).ToArray();
        var y = Enumerable.Range(0, count).Select(value => ((double)(value + 2), (double)(value + 3))).ToArray();
        var z = Enumerable.Range(0, count).Select(value => ((double)(value + 4), (double)(value + 5))).ToArray();
        var result = new ValueTuple<double, double>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((double)((value + value + 2) * (value + 4)), (double)((value + value + 4) * (value + 5)))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<double, double>, double>(x),
            MemoryMarshal.Cast<ValueTuple<double, double>, double>(y),
            MemoryMarshal.Cast<ValueTuple<double, double>, double>(z),
            MemoryMarshal.Cast<ValueTuple<double, double>, double>(result));

        // assert
        result.Should().Equal(expected);
    }

}
