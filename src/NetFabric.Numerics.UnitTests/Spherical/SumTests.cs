namespace NetFabric.Numerics.Spherical.UnitTests;

public class SumTests
{
    public static TheoryData<Vector<Degrees, double>[], Vector<Degrees, double>> SumData
        => new()
        {
            {
                Array.Empty<Vector<Degrees, double>>(),
                new Vector<Degrees, double>(0.0, Angle<Degrees, double>.Zero, Angle<Degrees, double>.Zero)
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
                new Vector<Degrees, double>(12.0, Angle<Degrees, double>.Full, Angle<Degrees, double>.Straight)
            },
            {
                Enumerable.Range(0, 97)
                    .Select(value => new Vector<Degrees, double>(
                        value,
                        new Angle<Degrees, double>(value + 1),
                        new Angle<Degrees, double>(value + 2)))
                    .ToArray(),
                new Vector<Degrees, double>(
                    Enumerable.Range(0, 97).Sum(),
                    Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value + 1)).Sum(),
                    Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value + 2)).Sum())
            },
        };

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_For_Enumerable_Should_Succeed(Vector<Degrees, double>[] source, Vector<Degrees, double> expected)
    {
        // arrange
        var enumerable = new ReadOnlyCollection<Vector<Degrees, double>>(source);

        // act
        var result = enumerable.Sum();

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_For_Array_Should_Succeed(Vector<Degrees, double>[] source, Vector<Degrees, double> expected)
    {
        // arrange

        // act
        var result = source.Sum();

        // assert
        result.Should().Be(expected);
    }
}
