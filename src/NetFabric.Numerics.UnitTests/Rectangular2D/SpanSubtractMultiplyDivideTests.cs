namespace NetFabric.Numerics.Rectangular2D.UnitTests;

public class SpanVectorSubtractMultiplyDivideTests
{
    public static TheoryData<Vector<double>[], Vector<double>, Vector<double>[]> SubtractValueData
        => new()
        {
            {
                Array.Empty<Vector<double>>(),
                new Vector<double>(1.0, 1.0),
                Array.Empty<Vector<double>>()
            },
            {
                new[] { new Vector<double>(1.0, 2.0) },
                new Vector<double>(3.0, 4.0),
                new[] { new Vector<double>(-2.0, -2.0) }
            },
            {
                new[] { new Vector<double>(1.0, 2.0), new Vector<double>(11.0, 12.0) },
                new Vector<double>(3.0, 4.0),
                new[] { new Vector<double>(-2.0, -2.0), new Vector<double>(8.0, 8.0) }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Vector<double>(value + 0.5, (value * 2.0) - 10.0)).ToArray(),
                new Vector<double>(1.25, -3.5),
                Enumerable.Range(0, 97).Select(value => new Vector<double>((value + 0.5) - 1.25, ((value * 2.0) - 10.0) + 3.5)).ToArray()
            },
        };

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

    public static TheoryData<Vector<double>[], Vector<double>, Vector<double>[]> MultiplyValueData
        => new()
        {
            {
                Array.Empty<Vector<double>>(),
                new Vector<double>(2.0, 3.0),
                Array.Empty<Vector<double>>()
            },
            {
                new[] { new Vector<double>(2.0, -3.0) },
                new Vector<double>(-2.0, 0.5),
                new[] { new Vector<double>(-4.0, -1.5) }
            },
            {
                new[] { new Vector<double>(1.0, 2.0), new Vector<double>(11.0, 12.0) },
                new Vector<double>(3.0, 4.0),
                new[] { new Vector<double>(3.0, 8.0), new Vector<double>(33.0, 48.0) }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Vector<double>(value - 20.0, value + 4.0)).ToArray(),
                new Vector<double>(-2.0, 0.25),
                Enumerable.Range(0, 97).Select(value => new Vector<double>((value - 20.0) * -2.0, (value + 4.0) * 0.25)).ToArray()
            },
        };

    public static TheoryData<Vector<double>[], Vector<double>, Vector<double>[]> DivideValueData
        => new()
        {
            {
                Array.Empty<Vector<double>>(),
                new Vector<double>(2.0, 4.0),
                Array.Empty<Vector<double>>()
            },
            {
                new[] { new Vector<double>(8.0, -6.0) },
                new Vector<double>(2.0, -3.0),
                new[] { new Vector<double>(4.0, 2.0) }
            },
            {
                new[] { new Vector<double>(8.0, 10.0), new Vector<double>(18.0, 24.0) },
                new Vector<double>(2.0, 4.0),
                new[] { new Vector<double>(4.0, 2.5), new Vector<double>(9.0, 6.0) }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Vector<double>(value + 20.0, (value + 1.0) * 3.0)).ToArray(),
                new Vector<double>(2.0, 3.0),
                Enumerable.Range(0, 97).Select(value => new Vector<double>((value + 20.0) / 2.0, value + 1.0)).ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(SubtractValueData))]
    public void Subtract_Value_Should_Succeed(Vector<double>[] source, Vector<double> value, Vector<double>[] expected)
    {
        var result = new Vector<double>[source.Length];

        Vector.Subtract(source, value, result);

        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(SubtractValueData))]
    public void Subtract_Value_Inplace_Should_Succeed(Vector<double>[] source, Vector<double> value, Vector<double>[] expected)
    {
        Vector.Subtract(source, value, source);

        source.Should().Equal(expected);
    }

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

    [Theory]
    [MemberData(nameof(MultiplyValueData))]
    public void Multiply_Value_Should_Succeed(Vector<double>[] source, Vector<double> value, Vector<double>[] expected)
    {
        var result = new Vector<double>[source.Length];

        Vector.Multiply(source, value, result);

        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(MultiplyValueData))]
    public void Multiply_Value_Inplace_Should_Succeed(Vector<double>[] source, Vector<double> value, Vector<double>[] expected)
    {
        Vector.Multiply(source, value, source);

        source.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(DivideValueData))]
    public void Divide_Value_Should_Succeed(Vector<double>[] source, Vector<double> value, Vector<double>[] expected)
    {
        var result = new Vector<double>[source.Length];

        Vector.Divide(source, value, result);

        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(DivideValueData))]
    public void Divide_Value_Inplace_Should_Succeed(Vector<double>[] source, Vector<double> value, Vector<double>[] expected)
    {
        Vector.Divide(source, value, source);

        source.Should().Equal(expected);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Value_Operation_With_Undersized_Destination_Should_Throw_And_Not_Modify_Destination(int operation)
    {
        // arrange
        var source = new[] { new Vector<double>(10.0, 20.0), new Vector<double>(30.0, 40.0) };
        var value = new Vector<double>(2.0, 3.0);
        var destination = new[] { new Vector<double>(1234.0, 5678.0) };
        var expected = destination.ToArray();

        // act
        var act = () => InvokeValueOperation(operation, source, value, destination);

        // assert
        act.Should().Throw<ArgumentException>();
        destination.Should().Equal(expected);
    }

    static void InvokeValueOperation(int operation, Vector<double>[] source, Vector<double> value, Vector<double>[] destination)
    {
        switch (operation)
        {
            case 0:
                Vector.Add(source, value, destination);
                break;
            case 1:
                Vector.Subtract(source, value, destination);
                break;
            case 2:
                Vector.Multiply(source, value, destination);
                break;
            case 3:
                Vector.Divide(source, value, destination);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(operation));
        }
    }
}