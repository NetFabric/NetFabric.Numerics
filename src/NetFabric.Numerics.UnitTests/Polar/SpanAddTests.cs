namespace NetFabric.Numerics.Polar.UnitTests;

public class SpanVectorAddTests
{
    public static TheoryData<Vector<Degrees, double>[], Vector<Degrees, double>, Vector<Degrees, double>[]> AddValueData
        => new()
        {
            {
                Array.Empty<Vector<Degrees, double>>(),
                new Vector<Degrees, double>(1.0, Angle<Degrees, double>.Right),
                Array.Empty<Vector<Degrees, double>>()
            },
            {
                new Vector<Degrees, double>[] { new(1.0, Angle<Degrees, double>.Right) },
                new Vector<Degrees, double>(3.0, Angle<Degrees, double>.Straight),
                new Vector<Degrees, double>[] { new(4.0, new Angle<Degrees, double>(270.0)) }
            },
            {
                new Vector<Degrees, double>[] { new(1.0, Angle<Degrees, double>.Right), new(11.0, Angle<Degrees, double>.Straight) },
                new Vector<Degrees, double>(3.0, Angle<Degrees, double>.Right),
                new Vector<Degrees, double>[] { new(4.0, Angle<Degrees, double>.Straight), new(14.0, new Angle<Degrees, double>(270.0)) }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Vector<Degrees, double>(value, new Angle<Degrees, double>(value + 1))).ToArray(),
                new Vector<Degrees, double>(3.0, Angle<Degrees, double>.Right),
                Enumerable.Range(0, 97).Select(value => new Vector<Degrees, double>(value + 3, new Angle<Degrees, double>(value + 91))).ToArray()
            },
        };

    public static TheoryData<Vector<Degrees, double>[], Vector<Degrees, double>[], Vector<Degrees, double>[]> AddVectorData
        => new()
        {
            {
                Array.Empty<Vector<Degrees, double>>(),
                Array.Empty<Vector<Degrees, double>>(),
                Array.Empty<Vector<Degrees, double>>()
            },
            {
                new[] { new Vector<Degrees, double>(1.0, Angle<Degrees, double>.Right) },
                new[] { new Vector<Degrees, double>(3.0, Angle<Degrees, double>.Straight) },
                new[] { new Vector<Degrees, double>(4.0, new Angle<Degrees, double>(270.0)) }
            },
            {
                new[]
                {
                    new Vector<Degrees, double>(1.0, Angle<Degrees, double>.Right),
                    new Vector<Degrees, double>(11.0, Angle<Degrees, double>.Straight),
                },
                new[]
                {
                    new Vector<Degrees, double>(3.0, Angle<Degrees, double>.Right),
                    new Vector<Degrees, double>(2.0, new Angle<Degrees, double>(45.0)),
                },
                new[]
                {
                    new Vector<Degrees, double>(4.0, Angle<Degrees, double>.Straight),
                    new Vector<Degrees, double>(13.0, new Angle<Degrees, double>(225.0)),
                }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Vector<Degrees, double>(value + 10.0, new Angle<Degrees, double>(value + 90.0))).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Vector<Degrees, double>(value * 0.5, new Angle<Degrees, double>(value * 0.5))).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Vector<Degrees, double>((value + 10.0) + (value * 0.5), new Angle<Degrees, double>((value + 90.0) + (value * 0.5)))).ToArray()
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

    [Theory]
    [MemberData(nameof(AddVectorData))]
    public void Add_Vector_Should_Succeed(Vector<Degrees, double>[] left, Vector<Degrees, double>[] right, Vector<Degrees, double>[] expected)
    {
        var result = new Vector<Degrees, double>[left.Length];

        Vector.Add(left, right, result);

        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddVectorData))]
    public void Add_Vector_Inplace_Should_Succeed(Vector<Degrees, double>[] left, Vector<Degrees, double>[] right, Vector<Degrees, double>[] expected)
    {
        Vector.Add(left, right, left);

        left.Should().Equal(expected);
    }
}