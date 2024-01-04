using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class SumTripletsTests
{
    public static TheoryData<int> SumData 
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void SumTriplets_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = Enumerable.Range(0, count)
            .Select(value => new MyVector3<T>(T.CreateChecked(value), T.CreateChecked(value + 1), T.CreateChecked(value + 2)))
            .ToArray();
        var expected = source
            .Aggregate(MyVector3<T>.AdditiveIdentity, (sum, value) => sum + value);

        // act
        var result = new MyVector3<T>(Tensor.SumTriplets<T>(MemoryMarshal.Cast<MyVector3<T>, T>(source)));

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumTriplets_Short_Should_Succeed(int count)
        => SumTriplets_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumTriplets_Int_Should_Succeed(int count)
        => SumTriplets_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumTriplets_Long_Should_Succeed(int count)
        => SumTriplets_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumTriplets_Half_Should_Succeed(int count)
        => SumTriplets_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumTriplets_Float_Should_Succeed(int count)
        => SumTriplets_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumTriplets_Double_Should_Succeed(int count)
        => SumTriplets_Should_Succeed<double>(count);
}
