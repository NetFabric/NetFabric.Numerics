namespace NetFabric.Numerics.Spherical.UnitTests;

public class SpanVectorAddTests
{
    public static TheoryData<Vector<Degrees, double>[], Vector<Degrees, double>, Vector<Degrees, double>[]> AddValueData
        => new()
        {
            {
                Array.Empty<Vector<Degrees, double>>(),
                new Vector<Degrees, double>(1.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Straight),
                Array.Empty<Vector<Degrees, double>>()
            },
            {
                new[] { new Vector<Degrees, double>(1.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Right) },
                new Vector<Degrees, double>(3.0, Angle<Degrees, double>.Straight, Angle<Degrees, double>.Straight),
                new[] { new Vector<Degrees, double>(4.0, new Angle<Degrees, double>(270.0), new Angle<Degrees, double>(270.0)) }
            },
            {
                new[]
                {
                    new Vector<Degrees, double>(1.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Right),
                    new Vector<Degrees, double>(11.0, Angle<Degrees, double>.Straight, Angle<Degrees, double>.Straight),
                },
                new Vector<Degrees, double>(3.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Right),
                new[]
                {
                    new Vector<Degrees, double>(4.0, Angle<Degrees, double>.Straight, Angle<Degrees, double>.Straight),
                    new Vector<Degrees, double>(14.0, new Angle<Degrees, double>(270.0), new Angle<Degrees, double>(270.0)),
                }
            },
            {
                Enumerable.Range(0, 97)
                    .Select(value => new Vector<Degrees, double>(
                        value,
                        new Angle<Degrees, double>(value + 1),
                        new Angle<Degrees, double>(value + 2)))
                    .ToArray(),
                new Vector<Degrees, double>(3.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Straight),
                Enumerable.Range(0, 97)
                    .Select(value => new Vector<Degrees, double>(
                        value + 3,
                        new Angle<Degrees, double>(value + 91),
                        new Angle<Degrees, double>(value + 182)))
                    .ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(AddValueData))]
    public void Add_Value_Should_Succeed(Vector<Degrees, double>[] source, Vector<Degrees, double> value, Vector<Degrees, double>[] expected)
    {
        // arrange
        var result = new Vector<Degrees, double>[source.Length];

        // act
        Vector.Add(source, value, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddValueData))]
    public void Add_Value_Inplace_Should_Succeed(Vector<Degrees, double>[] source, Vector<Degrees, double> value, Vector<Degrees, double>[] expected)
    {
        // arrange

        // act
        Vector.Add(source, value, source);

        // assert
        source.Should().Equal(expected);
    }
}