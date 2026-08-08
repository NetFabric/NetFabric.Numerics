namespace NetFabric.Numerics.Polar.UnitTests;

public class SpanVectorSubtractMultiplyDivideTests
{
    public static TheoryData<Vector<Degrees, double>[], Vector<Degrees, double>[], Vector<Degrees, double>[]> SubtractVectorData
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
                new[] { new Vector<Degrees, double>(-2.0, new Angle<Degrees, double>(-90.0)) }
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
                    new Vector<Degrees, double>(-2.0, Angle<Degrees, double>.Zero),
                    new Vector<Degrees, double>(9.0, new Angle<Degrees, double>(135.0)),
                }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Vector<Degrees, double>(value + 10.0, new Angle<Degrees, double>(value + 90.0))).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Vector<Degrees, double>(value * 0.5, new Angle<Degrees, double>(value * 0.5))).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Vector<Degrees, double>((value + 10.0) - (value * 0.5), new Angle<Degrees, double>((value + 90.0) - (value * 0.5)))).ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(SubtractVectorData))]
    public void Subtract_Vector_Should_Succeed(Vector<Degrees, double>[] left, Vector<Degrees, double>[] right, Vector<Degrees, double>[] expected)
    {
        var result = new Vector<Degrees, double>[left.Length];

        Vector.Subtract(left, right, result);

        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(SubtractVectorData))]
    public void Subtract_Vector_Inplace_Should_Succeed(Vector<Degrees, double>[] left, Vector<Degrees, double>[] right, Vector<Degrees, double>[] expected)
    {
        Vector.Subtract(left, right, left);

        left.Should().Equal(expected);
    }
}