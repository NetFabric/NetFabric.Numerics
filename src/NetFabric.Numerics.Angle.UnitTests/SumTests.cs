namespace NetFabric.Numerics.UnitTests;

public class SumTests
{
    public static TheoryData<Angle<Degrees, double>[], Angle<Degrees, double>> SumData
        => new()
        {
            {
                Array.Empty<Angle<Degrees, double>>(),
                new Angle<Degrees, double>(0)
            },
            {
                new Angle<Degrees, double>[] { new(1.0) },
                new Angle<Degrees, double>(1.0)
            },
            {
                new Angle<Degrees, double>[] { new(1.0), new(11.0) },
                new Angle<Degrees, double>(12.0)
            },
            {
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value)).ToArray(),
                new Angle<Degrees, double>(Enumerable.Range(0, 97).Sum())
            },
        };

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_For_Enumerable_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double> expected)
    {
        // arrange

        // act
        var result = source.AsEnumerable().Sum();

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_For_Array_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double> expected)
    {
        // arrange

        // act
        var result = source.Sum();

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_For_ReadOnlySpan_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double> expected)
    {
        // arrange

        // act
        var result = source.AsSpan().Sum();

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_For_NonSpan_Enumerable_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double> expected)
    {
        // arrange
        var enumerable = source.Select(item => item);

        // act
        var result = enumerable.Sum();

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Angle<Degrees, double>[], double, double> SumDataPrecisionSensitiveDouble
        => new()
        {
            {
                Enumerable.Repeat(new Angle<Degrees, double>(0.1), 97).ToArray(),
                9.7,
                0.000_000_000_001
            },
        };

    [Theory]
    [MemberData(nameof(SumDataPrecisionSensitiveDouble))]
    public void Sum_For_Array_Double_PrecisionSensitive_Should_Succeed(Angle<Degrees, double>[] source, double expected, double tolerance)
    {
        // arrange

        // act
        var result = source.Sum();

        // assert
        result.Value.Should().BeApproximately(expected, tolerance);
    }

    public static TheoryData<Angle<Degrees, float>[], Angle<Degrees, float>> SumDataFloat
        => new()
        {
            {
                Array.Empty<Angle<Degrees, float>>(),
                new Angle<Degrees, float>(0f)
            },
            {
                new Angle<Degrees, float>[] { new(1f) },
                new Angle<Degrees, float>(1f)
            },
            {
                new Angle<Degrees, float>[] { new(1f), new(11f) },
                new Angle<Degrees, float>(12f)
            },
            {
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, float>(value)).ToArray(),
                new Angle<Degrees, float>(Enumerable.Range(0, 97).Sum())
            },
        };

    [Theory]
    [MemberData(nameof(SumDataFloat))]
    public void Sum_For_Array_Float_Should_Succeed(Angle<Degrees, float>[] source, Angle<Degrees, float> expected)
    {
        // arrange

        // act
        var result = source.Sum();

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(SumDataFloat))]
    public void Sum_For_NonSpan_Enumerable_Float_Should_Succeed(Angle<Degrees, float>[] source, Angle<Degrees, float> expected)
    {
        // arrange
        var enumerable = source.Select(item => item);

        // act
        var result = enumerable.Sum();

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Angle<Degrees, float>[], float, float> SumDataPrecisionSensitiveFloat
        => new()
        {
            {
                Enumerable.Repeat(new Angle<Degrees, float>(0.1f), 97).ToArray(),
                9.7f,
                0.0001f
            },
        };

    [Theory]
    [MemberData(nameof(SumDataPrecisionSensitiveFloat))]
    public void Sum_For_Array_Float_PrecisionSensitive_Should_Succeed(Angle<Degrees, float>[] source, float expected, float tolerance)
    {
        // arrange

        // act
        var result = source.Sum();

        // assert
        result.Value.Should().BeApproximately(expected, tolerance);
    }
}