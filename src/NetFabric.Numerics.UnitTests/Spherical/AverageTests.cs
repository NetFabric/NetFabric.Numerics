namespace NetFabric.Numerics.Spherical.UnitTests;

public class AverageTests
{
    public static TheoryData<Vector<Degrees, double>[], Vector<Degrees, double>?> AverageData
        => new()
        {
            {
                Array.Empty<Vector<Degrees, double>>(),
                null
            },
            {
                new[] { new Vector<Degrees, double>(1.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Straight) },
                new Vector<Degrees, double>(1.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Straight)
            },
            {
                new[]
                {
                    new Vector<Degrees, double>(1.0, Angle<Degrees, double>.Straight, Angle<Degrees, double>.Right),
                    new Vector<Degrees, double>(11.0, Angle<Degrees, double>.Straight, Angle<Degrees, double>.Right),
                },
                new Vector<Degrees, double>(6.0, Angle<Degrees, double>.Straight, Angle<Degrees, double>.Right)
            },
            {
                Enumerable.Range(0, 97)
                    .Select(value => new Vector<Degrees, double>(
                        value,
                        new Angle<Degrees, double>(value + 1),
                        new Angle<Degrees, double>(value + 2)))
                    .ToArray(),
                new Vector<Degrees, double>(48.0, new Angle<Degrees, double>(49.0), new Angle<Degrees, double>(50.0))
            },
        };

    [Theory]
    [MemberData(nameof(AverageData))]
    public void Average_For_Enumerable_Should_Succeed(Vector<Degrees, double>[] source, Vector<Degrees, double>? expected)
    {
        // arrange
        var enumerable = new ReadOnlyCollection<Vector<Degrees, double>>(source);

        // act
        var result = enumerable.Average();

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(AverageData))]
    public void Average_For_Array_Should_Succeed(Vector<Degrees, double>[] source, Vector<Degrees, double>? expected)
    {
        // arrange

        // act
        var result = source.Average();

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(AverageData))]
    public void Average_For_Span_Should_Succeed(Vector<Degrees, double>[] source, Vector<Degrees, double>? expected)
    {
        // arrange
        var span = source.AsSpan();

        // act
        var result = span.Average();

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(AverageData))]
    public void Average_For_ReadOnlySpan_Should_Succeed(Vector<Degrees, double>[] source, Vector<Degrees, double>? expected)
    {
        // arrange
        ReadOnlySpan<Vector<Degrees, double>> readOnlySpan = source;

        // act
        var result = readOnlySpan.Average();

        // assert
        result.Should().Be(expected);
    }
}