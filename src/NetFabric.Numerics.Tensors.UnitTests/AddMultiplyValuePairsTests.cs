using System.Linq;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddMultiplyValuePairsTests
{
    public static TheoryData<int> AddData 
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Short_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((short)value, (short)(value + 1))).ToArray();
        var y = ((short)24, (short)25);
        var z = Enumerable.Range(0, count).Select(value => ((short)(value + 2), (short)(value + 3))).ToArray();
        var result = new ValueTuple<short, short>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((short)((value + 24) * (value + 2)), (short)((value + 26) * (value + 3)))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<short, short>, short>(x),
            y,
            MemoryMarshal.Cast<ValueTuple<short, short>, short>(z),
            MemoryMarshal.Cast<ValueTuple<short, short>, short>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Short_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((short)value, (short)(value + 1))).ToArray();
        var y = Enumerable.Range(0, count).Select(value => ((short)(value + 2), (short)(value + 3))).ToArray();
        var z = ((short)42, (short)43);
        var result = new ValueTuple<short, short>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((short)((value + value + 2) * 42), (short)((value + value + 4) * 43))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<short, short>, short>(x),
            MemoryMarshal.Cast<ValueTuple<short, short>, short>(y),
            z,
            MemoryMarshal.Cast<ValueTuple<short, short>, short>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Short_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((short)value, (short)(value + 1))).ToArray();
        var y = ((short)24, (short)25);
        var z = ((short)42, (short)43);
        var result = new ValueTuple<short, short>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((short)((value + 24) * 42), (short)((value + 26) * 43))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<short, short>, short>(x),
            y,
            z,
            MemoryMarshal.Cast<ValueTuple<short, short>, short>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Int_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (value, value + 1)).ToArray();
        var y = (24, 25);
        var z = Enumerable.Range(0, count).Select(value => (value + 2, value + 3)).ToArray();
        var result = new ValueTuple<int, int>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((value + 24) * (value + 2), (value + 26) * (value + 3))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<int, int>, int>(x),
            y,
            MemoryMarshal.Cast<ValueTuple<int, int>, int>(z),
            MemoryMarshal.Cast<ValueTuple<int, int>, int>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Int_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (value, value + 1)).ToArray();
        var y = Enumerable.Range(0, count).Select(value => (value + 2, value + 3)).ToArray();
        var z = (42, 43);
        var result = new ValueTuple<int, int>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((value + value + 2) * 42, (value + value + 4) * 43)).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<int, int>, int>(x),
            MemoryMarshal.Cast<ValueTuple<int, int>, int>(y),
            z,
            MemoryMarshal.Cast<ValueTuple<int, int>, int>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Int_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => (value, value + 1)).ToArray();
        var y = (24, 25);
        var z = (42, 43);
        var result = new ValueTuple<int, int>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((value + 24) * 42, (value + 26) * 43)).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<int, int>, int>(x),
            y,
            z,
            MemoryMarshal.Cast<ValueTuple<int, int>, int>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Long_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((long)value, (long)(value + 1))).ToArray();
        var y = (24l, 25l);
        var z = Enumerable.Range(0, count).Select(value => ((long)(value + 2), (long)(value + 3))).ToArray();
        var result = new ValueTuple<long, long>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((long)((value + 24) * (value + 2)), (long)((value + 26) * (value + 3)))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<long, long>, long>(x),
            y,
            MemoryMarshal.Cast<ValueTuple<long, long>, long>(z),
            MemoryMarshal.Cast<ValueTuple<long, long>, long>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Long_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((long)value, (long)(value + 1))).ToArray();
        var y = Enumerable.Range(0, count).Select(value => ((long)(value + 2), (long)(value + 3))).ToArray();
        var z = (42l, 43l);
        var result = new ValueTuple<long, long>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((long)((value + value + 2) * 42), (long)((value + value + 4) * 43))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<long, long>, long>(x),
            MemoryMarshal.Cast<ValueTuple<long, long>, long>(y),
            z,
            MemoryMarshal.Cast<ValueTuple<long, long>, long>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Long_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((long)value, (long)(value + 1))).ToArray();
        var y = (24l, 25l);
        var z = (42l, 43l);
        var result = new ValueTuple<long, long>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((long)((value + 24) * 42), (long)((value + 26) * 43))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<long, long>, long>(x),
            y,
            z,
            MemoryMarshal.Cast<ValueTuple<long, long>, long>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Float_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((float)value, (float)(value + 1))).ToArray();
        var y = (24f, 25f);
        var z = Enumerable.Range(0, count).Select(value => ((float)(value + 2), (float)(value + 3))).ToArray();
        var result = new ValueTuple<float, float>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((float)((value + 24) * (value + 2)), (float)((value + 26) * (value + 3)))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<float, float>, float>(x),
            y,
            MemoryMarshal.Cast<ValueTuple<float, float>, float>(z),
            MemoryMarshal.Cast<ValueTuple<float, float>, float>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Float_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((float)value, (float)(value + 1))).ToArray();
        var y = Enumerable.Range(0, count).Select(value => ((float)(value + 2), (float)(value + 3))).ToArray();
        var z = (42f, 43f);
        var result = new ValueTuple<float, float>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((float)((value + value + 2) * 42), (float)((value + value + 4) * 43))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<float, float>, float>(x),
            MemoryMarshal.Cast<ValueTuple<float, float>, float>(y),
            z,
            MemoryMarshal.Cast<ValueTuple<float, float>, float>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Float_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((float)value, (float)(value + 1))).ToArray();
        var y = (24f, 25f);
        var z = (42f, 43f);
        var result = new ValueTuple<float, float>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((float)((value + 24) * 42), (float)((value + 26) * 43))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<float, float>, float>(x),
            y,
            z,
            MemoryMarshal.Cast<ValueTuple<float, float>, float>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Double_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((double)value, (double)(value + 1))).ToArray();
        var y = (24d, 25d);
        var z = Enumerable.Range(0, count).Select(value => ((double)(value + 2), (double)(value + 3))).ToArray();
        var result = new ValueTuple<double, double>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((double)((value + 24) * (value + 2)), (double)((value + 26) * (value + 3)))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<double, double>, double>(x),
            y,
            MemoryMarshal.Cast<ValueTuple<double, double>, double>(z),
            MemoryMarshal.Cast<ValueTuple<double, double>, double>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Double_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((double)value, (double)(value + 1))).ToArray();
        var y = Enumerable.Range(0, count).Select(value => ((double)(value + 2), (double)(value + 3))).ToArray();
        var z = (42d, 43d);
        var result = new ValueTuple<double, double>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((double)((value + value + 2) * 42), (double)((value + value + 4) * 43))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<double, double>, double>(x),
            MemoryMarshal.Cast<ValueTuple<double, double>, double>(y),
            z,
            MemoryMarshal.Cast<ValueTuple<double, double>, double>(result));

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Double_Should_Succeed(int count)
    {
        // arrange
        var x = Enumerable.Range(0, count).Select(value => ((double)value, (double)(value + 1))).ToArray();
        var y = (24d, 25d);
        var z = (42d, 43d);
        var result = new ValueTuple<double, double>[count];
        var expected = Enumerable.Range(0, count).Select(value => ((double)((value + 24) * 42), (double)((value + 26) * 43))).ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<ValueTuple<double, double>, double>(x),
            y,
            z,
            MemoryMarshal.Cast<ValueTuple<double, double>, double>(result));

        // assert
        result.Should().Equal(expected);
    }

}
