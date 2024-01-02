using System.Linq;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddValuePairsTests
{
    public static readonly ValueTuple<short, short> constValue = (42, 24);

    public static TheoryData<int> AddData 
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Short_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((short)value, (short)(value + 1))).ToArray();
        var result = new ValueTuple<short, short>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((short)(value + constValue.Item1), (short)(value + 1 + constValue.Item2))).ToArray();

        // act
        Tensor.Add(MemoryMarshal.Cast<ValueTuple<short, short>, short>(x), constValue, MemoryMarshal.Cast<ValueTuple<short, short>, short>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Int_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (value, value + 1)).ToArray();
        var result = new ValueTuple<int, int>[count];
        var expected = Enumerable.Range(0, count).Select(value => (value + constValue.Item1, value + 1 + constValue.Item2)).ToArray();

        // act
        Tensor.Add(MemoryMarshal.Cast<ValueTuple<int, int>, int>(x), constValue, MemoryMarshal.Cast<ValueTuple<int, int>, int>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Long_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((long)value, (long)(value + 1))).ToArray();
        var result = new ValueTuple<long, long>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((long)(value + constValue.Item1), (long)(value + 1 + constValue.Item2))).ToArray();

        // act
        Tensor.Add(MemoryMarshal.Cast<ValueTuple<long, long>, long>(x), constValue, MemoryMarshal.Cast<ValueTuple<long, long>, long>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Float_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((float)value, (float)(value + 1))).ToArray();
        var result = new ValueTuple<float, float>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((float)(value + constValue.Item1), (float)(value + 1 + constValue.Item2))).ToArray();

        // act
        Tensor.Add(MemoryMarshal.Cast<ValueTuple<float, float>, float>(x), constValue, MemoryMarshal.Cast<ValueTuple<float, float>, float>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Double_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((double)value, (double)(value + 1))).ToArray();
        var result = new ValueTuple<double, double>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((double)(value + constValue.Item1), (double)(value + 1 + constValue.Item2))).ToArray();

        // act
        Tensor.Add(MemoryMarshal.Cast<ValueTuple<double, double>, double>(x), constValue, MemoryMarshal.Cast<ValueTuple<double, double>, double>(result));

        // assert
        result.Should().Equal(expected);
    }

}
