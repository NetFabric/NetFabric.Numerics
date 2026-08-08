namespace NetFabric.Numerics.Rectangular2D.UnitTests;

public class SpanVectorSubtractTests
{
    public static TheoryData<Vector<double>[], Vector<double>[], Vector<double>[]> SubtractVectorData
        => new()
        {
            {
                Array.Empty<Vector<double>>(),
                Array.Empty<Vector<double>>(),
                Array.Empty<Vector<double>>()
            },
            {
                new[] { new Vector<double>(1.0, 2.0) },
                new[] { new Vector<double>(3.0, 4.0) },
                new[] { new Vector<double>(-2.0, -2.0) }
            },
            {
                new[] { new Vector<double>(1.0, 2.0), new Vector<double>(11.0, 12.0) },
                new[] { new Vector<double>(3.0, 4.0), new Vector<double>(2.0, 3.0) },
                new[] { new Vector<double>(-2.0, -2.0), new Vector<double>(9.0, 9.0) }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Vector<double>(value + 10.0, value - 5.0)).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Vector<double>(value * 0.5, -value)).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Vector<double>((value + 10.0) - (value * 0.5), (value - 5.0) + value)).ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(SubtractVectorData))]
    public void Subtract_Vector_Should_Succeed(Vector<double>[] left, Vector<double>[] right, Vector<double>[] expected)
    {
        var result = new Vector<double>[left.Length];

        Vector.Subtract(left, right, result);

        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(SubtractVectorData))]
    public void Subtract_Vector_Inplace_Should_Succeed(Vector<double>[] left, Vector<double>[] right, Vector<double>[] expected)
    {
        Vector.Subtract(left, right, left);

        left.Should().Equal(expected);
    }
}