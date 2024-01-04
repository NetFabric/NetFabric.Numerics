using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddMultiplyValuePairsTests
{
    public static TheoryData<int> AddData 
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void AddMultiply_First_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = Enumerable.Range(0, count);
        var x = source
            .Select(value => new MyVector2<T>(T.CreateChecked(value), T.CreateChecked(value + 1)))
            .ToArray();
        var y = (T.CreateChecked(24), T.CreateChecked(25));
        var z = source
            .Select(value => new MyVector2<T>(T.CreateChecked(value + 2), T.CreateChecked(value + 3)))
            .ToArray();
        var result = new MyVector2<T>[count];
        var expected = source
            .Select(value => new MyVector2<T>(T.CreateChecked((value + 24) * (value + 2)), T.CreateChecked((value + 26) * (value + 3))))
            .ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<MyVector2<T>, T>(x),
            y,
            MemoryMarshal.Cast<MyVector2<T>, T>(z),
            MemoryMarshal.Cast<MyVector2<T>, T>(result));

        // assert
        result.Should().Equal(expected);
    }


    static void AddMultiply_Second_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = Enumerable.Range(0, count);
        var x = source
            .Select(value => new MyVector2<T>(T.CreateChecked(value), T.CreateChecked(value + 1)))
            .ToArray();
        var y = source
            .Select(value => new MyVector2<T>(T.CreateChecked(value + 2), T.CreateChecked(value + 3)))
            .ToArray();
        var z = (T.CreateChecked(42), T.CreateChecked(43));
        var result = new MyVector2<T>[count];
        var expected = source
            .Select(value => new MyVector2<T>(T.CreateChecked((value + value + 2) * 42), T.CreateChecked((value + value + 4) * 43)))
            .ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<MyVector2<T>, T>(x),
            MemoryMarshal.Cast<MyVector2<T>, T>(y),
            z,
            MemoryMarshal.Cast<MyVector2<T>, T>(result));

        // assert
        result.Should().Equal(expected);
    }

    static void AddMultiply_Both_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = Enumerable.Range(0, count);
        var x = source
            .Select(value => new MyVector2<T>(T.CreateChecked(value), T.CreateChecked(value + 1)))
            .ToArray();
        var y = (T.CreateChecked(24), T.CreateChecked(25));
        var z = (T.CreateChecked(42), T.CreateChecked(43));
        var result = new MyVector2<T>[count];
        var expected = source
            .Select(value => new MyVector2<T>(T.CreateChecked((value + 24) * 42), T.CreateChecked((value + 26) * 43)))
            .ToArray();

        // act
        Tensor.AddMultiply(
            MemoryMarshal.Cast<MyVector2<T>, T>(x),
            y,
            z,
            MemoryMarshal.Cast<MyVector2<T>, T>(result));

        // assert
        result.Should().Equal(expected);
    }


    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Short_Should_Succeed(int count)
        => AddMultiply_First_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Short_Should_Succeed(int count)
        => AddMultiply_Second_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Short_Should_Succeed(int count)
        => AddMultiply_Both_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Int_Should_Succeed(int count)
        => AddMultiply_First_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Int_Should_Succeed(int count)
        => AddMultiply_Second_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Int_Should_Succeed(int count)
        => AddMultiply_Both_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Long_Should_Succeed(int count)
        => AddMultiply_First_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Long_Should_Succeed(int count)
        => AddMultiply_Second_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Long_Should_Succeed(int count)
        => AddMultiply_Both_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Half_Should_Succeed(int count)
        => AddMultiply_First_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Half_Should_Succeed(int count)
        => AddMultiply_Second_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Half_Should_Succeed(int count)
        => AddMultiply_Both_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Float_Should_Succeed(int count)
        => AddMultiply_First_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Float_Should_Succeed(int count)
        => AddMultiply_Second_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Float_Should_Succeed(int count)
        => AddMultiply_Both_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Double_Should_Succeed(int count)
        => AddMultiply_First_Should_Succeed<double>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Double_Should_Succeed(int count)
        => AddMultiply_Second_Should_Succeed<double>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Double_Should_Succeed(int count)
        => AddMultiply_Both_Should_Succeed<double>(count);

}
