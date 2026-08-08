namespace NetFabric.Numerics.Spherical.UnitTests;

public class SpanVectorAddTests
{
    public static TheoryData<Vector<Degrees, double>[], Vector<Degrees, double>[], Vector<Degrees, double>[]> AddVectorData
        => new()
        {
            {
                Array.Empty<Vector<Degrees, double>>(),
                Array.Empty<Vector<Degrees, double>>(),
                Array.Empty<Vector<Degrees, double>>()
            },
            {
                new[] { new Vector<Degrees, double>(1.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Right) },
                new[] { new Vector<Degrees, double>(3.0, Angle<Degrees, double>.Straight, Angle<Degrees, double>.Straight) },
                new[] { new Vector<Degrees, double>(4.0, new Angle<Degrees, double>(270.0), new Angle<Degrees, double>(270.0)) }
            },
            {
                new[]
                {
                    new Vector<Degrees, double>(1.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Right),
                    new Vector<Degrees, double>(11.0, Angle<Degrees, double>.Straight, Angle<Degrees, double>.Straight),
                },
                new[]
                {
                    new Vector<Degrees, double>(3.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Right),
                    new Vector<Degrees, double>(2.0, new Angle<Degrees, double>(45.0), new Angle<Degrees, double>(45.0)),
                },
                new[]
                {
                    new Vector<Degrees, double>(4.0, Angle<Degrees, double>.Straight, Angle<Degrees, double>.Straight),
                    new Vector<Degrees, double>(13.0, new Angle<Degrees, double>(225.0), new Angle<Degrees, double>(225.0)),
                }
            },
            {
                Enumerable.Range(0, 97)
                    .Select(value => new Vector<Degrees, double>(
                        value + 10.0,
                        new Angle<Degrees, double>(value + 90.0),
                        new Angle<Degrees, double>(value + 120.0)))
                    .ToArray(),
                Enumerable.Range(0, 97)
                    .Select(value => new Vector<Degrees, double>(
                        value * 0.5,
                        new Angle<Degrees, double>(value * 0.5),
                        new Angle<Degrees, double>(value * 0.25)))
                    .ToArray(),
                Enumerable.Range(0, 97)
                    .Select(value => new Vector<Degrees, double>(
                        (value + 10.0) + (value * 0.5),
                        new Angle<Degrees, double>((value + 90.0) + (value * 0.5)),
                        new Angle<Degrees, double>((value + 120.0) + (value * 0.25))))
                    .ToArray()
            },
        };

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