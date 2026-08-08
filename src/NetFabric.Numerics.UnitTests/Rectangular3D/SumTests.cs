namespace NetFabric.Numerics.Rectangular3D.UnitTests;

public class SumTests
{
    public static TheoryData<Vector<double>[], Vector<double>> SumData
        => new()
        {
            {
                Array.Empty<Vector<double>>(),
                new Vector<double>(0, 0, 0)
            },
            {
                new Vector<double>[] { new(1.0, 2.0, 3.0) },
                new Vector<double>(1.0, 2.0, 3.0)
            },
            {
                new Vector<double>[] { new(1.0, 2.0, 3.0), new(11.0, 12.0, 13.0) },
                new Vector<double>(12.0, 14.0, 16.0)
            },
            {
                Enumerable.Range(0, 97).Select(value => new Vector<double>(value, value + 1, value + 2)).ToArray(),
                new Vector<double>(
                    Enumerable.Range(0, 97).Sum(),
                    Enumerable.Range(0, 97).Select(value => value + 1).Sum(),
                    Enumerable.Range(0, 97).Select(value => value + 2).Sum())
            },
        };

    public static TheoryData<Vector<float>[], Vector<float>> SumFloatData
        => new()
        {
            {
                Array.Empty<Vector<float>>(),
                new Vector<float>(0f, 0f, 0f)
            },
            {
                new Vector<float>[] { new(1f, 2f, 3f) },
                new Vector<float>(1f, 2f, 3f)
            },
            {
                new Vector<float>[] { new(1f, 2f, 3f), new(11f, 12f, 13f) },
                new Vector<float>(12f, 14f, 16f)
            },
            {
                Enumerable.Range(0, 97).Select(value => new Vector<float>(value + 0.25f, value + 1.5f, value - 2f)).ToArray(),
                new Vector<float>(4680.25f, 4801.5f, 4462f)
            },
        };

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_For_Enumerable_Should_Succeed(Vector<double>[] source, Vector<double> expected)
    {
        // arrange
        var enumerable = new ReadOnlyCollection<Vector<double>>(source);

        // act
        var result = enumerable.Sum();

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_For_Array_Should_Succeed(Vector<double>[] source, Vector<double> expected)
    {
        // arrange

        // act
        var result = source.Sum();

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(SumFloatData))]
    public void Sum_Float_For_Enumerable_Should_Succeed(Vector<float>[] source, Vector<float> expected)
    {
        // arrange
        var enumerable = new ReadOnlyCollection<Vector<float>>(source);

        // act
        var result = enumerable.Sum();

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(SumFloatData))]
    public void Sum_Float_For_Array_Should_Succeed(Vector<float>[] source, Vector<float> expected)
    {
        // arrange

        // act
        var result = source.Sum();

        // assert
        result.Should().Be(expected);
    }
}