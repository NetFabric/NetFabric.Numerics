namespace NetFabric.Numerics.UnitTests;

public class SpanAddTests
{
    public static TheoryData<Angle<Degrees, double>[], Angle<Degrees, double>[], Angle<Degrees, double>[]> AddData
        => new()
        {
            {
                Array.Empty<Angle<Degrees, double>>(),
                Array.Empty<Angle<Degrees, double>>(),
                Array.Empty<Angle<Degrees, double>>()
            },
            {
                new Angle<Degrees, double>[] { new(1.0) },
                new Angle<Degrees, double>[] { new(3.0) },
                new Angle<Degrees, double>[] { new(4.0) }
            },
            {
                new Angle<Degrees, double>[] { new(1.0), new(11.0) },
                new Angle<Degrees, double>[] { new(12.0), new(13.0) },
                new Angle<Degrees, double>[] { new(13.0), new(24.0) }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value)).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value + 1.0)).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value * 2.0 + 1.0)).ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Should_Succeed(Angle<Degrees, double>[] left, Angle<Degrees, double>[] right, Angle<Degrees, double>[] expected)
    {
        // arrange
        var result = new Angle<Degrees, double>[left.Length];

        // act
        Angle.Add<Degrees, double>(left, right, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Inplace_Should_Succeed(Angle<Degrees, double>[] left, Angle<Degrees, double>[] right, Angle<Degrees, double>[] expected)
    {
        // arrange

        // act
        Angle.Add<Degrees, double>(left, right, left);

        // assert
        left.Should().Equal(expected);
    }

    public static TheoryData<Angle<Degrees, double>[], Angle<Degrees, double>[], Angle<Degrees, double>[]> SubtractData
        => new()
        {
            {
                Array.Empty<Angle<Degrees, double>>(),
                Array.Empty<Angle<Degrees, double>>(),
                Array.Empty<Angle<Degrees, double>>()
            },
            {
                new Angle<Degrees, double>[] { new(1.0) },
                new Angle<Degrees, double>[] { new(3.0) },
                new Angle<Degrees, double>[] { new(-2.0) }
            },
            {
                new Angle<Degrees, double>[] { new(1.0), new(11.0) },
                new Angle<Degrees, double>[] { new(12.0), new(13.0) },
                new Angle<Degrees, double>[] { new(-11.0), new(-2.0) }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value * 2.0 + 5.0)).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value + 7.0)).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value - 2.0)).ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(SubtractData))]
    public void Subtract_Should_Succeed(Angle<Degrees, double>[] left, Angle<Degrees, double>[] right, Angle<Degrees, double>[] expected)
    {
        // arrange
        var result = new Angle<Degrees, double>[left.Length];

        // act
        Angle.Subtract(left, right, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(SubtractData))]
    public void Subtract_Inplace_Should_Succeed(Angle<Degrees, double>[] left, Angle<Degrees, double>[] right, Angle<Degrees, double>[] expected)
    {
        // arrange

        // act
        Angle.Subtract(left, right, left);

        // assert
        left.Should().Equal(expected);
    }

    public static TheoryData<Angle<Degrees, float>[], Angle<Degrees, float>[], Angle<Degrees, float>[]> AddDataFloat
        => new()
        {
            {
                Array.Empty<Angle<Degrees, float>>(),
                Array.Empty<Angle<Degrees, float>>(),
                Array.Empty<Angle<Degrees, float>>()
            },
            {
                new Angle<Degrees, float>[] { new(1f), new(-11f) },
                new Angle<Degrees, float>[] { new(2f), new(1f) },
                new Angle<Degrees, float>[] { new(3f), new(-10f) }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, float>(value - 48f)).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, float>(value / 2f)).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, float>((value - 48f) + (value / 2f))).ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(AddDataFloat))]
    public void Add_Float_Should_Succeed(Angle<Degrees, float>[] left, Angle<Degrees, float>[] right, Angle<Degrees, float>[] expected)
    {
        // arrange
        var result = new Angle<Degrees, float>[left.Length];

        // act
        Angle.Add(left, right, result);

        // assert
        result.Should().Equal(expected);
    }

    public static TheoryData<Angle<Degrees, float>[], Angle<Degrees, float>[], Angle<Degrees, float>[]> SubtractDataFloat
        => new()
        {
            {
                Array.Empty<Angle<Degrees, float>>(),
                Array.Empty<Angle<Degrees, float>>(),
                Array.Empty<Angle<Degrees, float>>()
            },
            {
                new Angle<Degrees, float>[] { new(1f), new(-11f) },
                new Angle<Degrees, float>[] { new(2f), new(1f) },
                new Angle<Degrees, float>[] { new(-1f), new(-12f) }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, float>(value - 48f)).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, float>(value / 2f)).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, float>((value - 48f) - (value / 2f))).ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(SubtractDataFloat))]
    public void Subtract_Float_Should_Succeed(Angle<Degrees, float>[] left, Angle<Degrees, float>[] right, Angle<Degrees, float>[] expected)
    {
        // arrange
        var result = new Angle<Degrees, float>[left.Length];

        // act
        Angle.Subtract(left, right, result);

        // assert
        result.Should().Equal(expected);
    }
}