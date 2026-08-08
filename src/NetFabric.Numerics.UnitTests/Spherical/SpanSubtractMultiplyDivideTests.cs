namespace NetFabric.Numerics.Spherical.UnitTests;

public class SpanVectorSubtractMultiplyDivideTests
{
    public static TheoryData<Vector<Degrees, double>[], Vector<Degrees, double>, Vector<Degrees, double>[]> SubtractValueData
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
                new[] { new Vector<Degrees, double>(-2.0, new Angle<Degrees, double>(-90.0), new Angle<Degrees, double>(-90.0)) }
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
                    new Vector<Degrees, double>(-2.0, Angle<Degrees, double>.Zero, Angle<Degrees, double>.Zero),
                    new Vector<Degrees, double>(8.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Right),
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
                        value - 3,
                        new Angle<Degrees, double>(value - 89),
                        new Angle<Degrees, double>(value - 178)))
                    .ToArray()
            },
        };

    public static TheoryData<Vector<Degrees, double>[], Vector<Degrees, double>, Vector<Degrees, double>[]> MultiplyValueData
        => new()
        {
            {
                Array.Empty<Vector<Degrees, double>>(),
                new Vector<Degrees, double>(3.0, new Angle<Degrees, double>(2.0), new Angle<Degrees, double>(4.0)),
                Array.Empty<Vector<Degrees, double>>()
            },
            {
                new[] { new Vector<Degrees, double>(1.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Straight) },
                new Vector<Degrees, double>(3.0, new Angle<Degrees, double>(2.0), new Angle<Degrees, double>(4.0)),
                new[] { new Vector<Degrees, double>(3.0, Angle<Degrees, double>.Straight, new Angle<Degrees, double>(720.0)) }
            },
            {
                new[]
                {
                    new Vector<Degrees, double>(1.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Straight),
                    new Vector<Degrees, double>(11.0, Angle<Degrees, double>.Straight, Angle<Degrees, double>.Right),
                },
                new Vector<Degrees, double>(3.0, new Angle<Degrees, double>(2.0), new Angle<Degrees, double>(4.0)),
                new[]
                {
                    new Vector<Degrees, double>(3.0, Angle<Degrees, double>.Straight, new Angle<Degrees, double>(720.0)),
                    new Vector<Degrees, double>(33.0, new Angle<Degrees, double>(360.0), new Angle<Degrees, double>(360.0)),
                }
            },
            {
                Enumerable.Range(0, 97)
                    .Select(value => new Vector<Degrees, double>(
                        value,
                        new Angle<Degrees, double>(value + 1),
                        new Angle<Degrees, double>(value + 2)))
                    .ToArray(),
                new Vector<Degrees, double>(3.0, new Angle<Degrees, double>(2.0), new Angle<Degrees, double>(4.0)),
                Enumerable.Range(0, 97)
                    .Select(value => new Vector<Degrees, double>(
                        value * 3,
                        new Angle<Degrees, double>((value + 1) * 2),
                        new Angle<Degrees, double>((value + 2) * 4)))
                    .ToArray()
            },
        };

    public static TheoryData<Vector<Degrees, double>[], Vector<Degrees, double>[], Vector<Degrees, double>[]> SubtractVectorData
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
                new[] { new Vector<Degrees, double>(-2.0, new Angle<Degrees, double>(-90.0), new Angle<Degrees, double>(-90.0)) }
            },
            {
                new[]
                {
                    new Vector<Degrees, double>(1.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Straight),
                    new Vector<Degrees, double>(11.0, Angle<Degrees, double>.Straight, Angle<Degrees, double>.Right),
                },
                new[]
                {
                    new Vector<Degrees, double>(3.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Right),
                    new Vector<Degrees, double>(2.0, new Angle<Degrees, double>(45.0), Angle<Degrees, double>.Straight),
                },
                new[]
                {
                    new Vector<Degrees, double>(-2.0, Angle<Degrees, double>.Zero, Angle<Degrees, double>.Right),
                    new Vector<Degrees, double>(9.0, new Angle<Degrees, double>(135.0), new Angle<Degrees, double>(-90.0)),
                }
            },
            {
                Enumerable.Range(0, 97)
                    .Select(value => new Vector<Degrees, double>(
                        value + 1,
                        new Angle<Degrees, double>(value + 2),
                        new Angle<Degrees, double>(value + 3)))
                    .ToArray(),
                Enumerable.Range(0, 97)
                    .Select(value => new Vector<Degrees, double>(
                        value,
                        new Angle<Degrees, double>(value + 1),
                        new Angle<Degrees, double>(value + 2)))
                    .ToArray(),
                Enumerable.Range(0, 97)
                    .Select(_ => new Vector<Degrees, double>(
                        1.0,
                        new Angle<Degrees, double>(1.0),
                        new Angle<Degrees, double>(1.0)))
                    .ToArray()
            },
        };

    public static TheoryData<Vector<Degrees, double>[], Vector<Degrees, double>, Vector<Degrees, double>[]> DivideValueData
        => new()
        {
            {
                Array.Empty<Vector<Degrees, double>>(),
                new Vector<Degrees, double>(2.0, new Angle<Degrees, double>(2.0), new Angle<Degrees, double>(4.0)),
                Array.Empty<Vector<Degrees, double>>()
            },
            {
                new[] { new Vector<Degrees, double>(8.0, Angle<Degrees, double>.Straight, Angle<Degrees, double>.Straight) },
                new Vector<Degrees, double>(2.0, new Angle<Degrees, double>(2.0), new Angle<Degrees, double>(4.0)),
                new[] { new Vector<Degrees, double>(4.0, Angle<Degrees, double>.Right, new Angle<Degrees, double>(45.0)) }
            },
            {
                new[]
                {
                    new Vector<Degrees, double>(8.0, Angle<Degrees, double>.Straight, Angle<Degrees, double>.Straight),
                    new Vector<Degrees, double>(18.0, new Angle<Degrees, double>(270.0), Angle<Degrees, double>.Right),
                },
                new Vector<Degrees, double>(2.0, new Angle<Degrees, double>(2.0), new Angle<Degrees, double>(4.0)),
                new[]
                {
                    new Vector<Degrees, double>(4.0, Angle<Degrees, double>.Right, new Angle<Degrees, double>(45.0)),
                    new Vector<Degrees, double>(9.0, new Angle<Degrees, double>(135.0), new Angle<Degrees, double>(22.5)),
                }
            },
            {
                Enumerable.Range(0, 97)
                    .Select(value => new Vector<Degrees, double>(
                        value + 1,
                        new Angle<Degrees, double>(value + 2),
                        new Angle<Degrees, double>(value + 4)))
                    .ToArray(),
                new Vector<Degrees, double>(2.0, new Angle<Degrees, double>(2.0), new Angle<Degrees, double>(4.0)),
                Enumerable.Range(0, 97)
                    .Select(value => new Vector<Degrees, double>(
                        (value + 1) / 2.0,
                        new Angle<Degrees, double>((value + 2) / 2.0),
                        new Angle<Degrees, double>((value + 4) / 4.0)))
                    .ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(SubtractValueData))]
    public void Subtract_Value_Should_Succeed(Vector<Degrees, double>[] source, Vector<Degrees, double> value, Vector<Degrees, double>[] expected)
    {
        // arrange
        var result = new Vector<Degrees, double>[source.Length];

        // act
        Vector.Subtract(source, value, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(SubtractValueData))]
    public void Subtract_Value_Inplace_Should_Succeed(Vector<Degrees, double>[] source, Vector<Degrees, double> value, Vector<Degrees, double>[] expected)
    {
        // arrange

        // act
        Vector.Subtract(source, value, source);

        // assert
        source.Should().Equal(expected);
    }

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

    [Theory]
    [MemberData(nameof(MultiplyValueData))]
    public void Multiply_Value_Should_Succeed(Vector<Degrees, double>[] source, Vector<Degrees, double> value, Vector<Degrees, double>[] expected)
    {
        // arrange
        var result = new Vector<Degrees, double>[source.Length];

        // act
        Vector.Multiply(source, value, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(MultiplyValueData))]
    public void Multiply_Value_Inplace_Should_Succeed(Vector<Degrees, double>[] source, Vector<Degrees, double> value, Vector<Degrees, double>[] expected)
    {
        // arrange

        // act
        Vector.Multiply(source, value, source);

        // assert
        source.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(DivideValueData))]
    public void Divide_Value_Should_Succeed(Vector<Degrees, double>[] source, Vector<Degrees, double> value, Vector<Degrees, double>[] expected)
    {
        // arrange
        var result = new Vector<Degrees, double>[source.Length];

        // act
        Vector.Divide(source, value, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(DivideValueData))]
    public void Divide_Value_Inplace_Should_Succeed(Vector<Degrees, double>[] source, Vector<Degrees, double> value, Vector<Degrees, double>[] expected)
    {
        // arrange

        // act
        Vector.Divide(source, value, source);

        // assert
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
        var source = new[]
        {
            new Vector<Degrees, double>(10.0, new Angle<Degrees, double>(20.0), new Angle<Degrees, double>(30.0)),
            new Vector<Degrees, double>(40.0, new Angle<Degrees, double>(50.0), new Angle<Degrees, double>(60.0)),
        };
        var value = new Vector<Degrees, double>(2.0, new Angle<Degrees, double>(3.0), new Angle<Degrees, double>(4.0));
        var destination = new[] { new Vector<Degrees, double>(1234.0, new Angle<Degrees, double>(5678.0), new Angle<Degrees, double>(9012.0)) };
        var expected = destination.ToArray();

        // act
        var act = () => InvokeValueOperation(operation, source, value, destination);

        // assert
        act.Should().Throw<ArgumentException>();
        destination.Should().Equal(expected);
    }

    static void InvokeValueOperation(
        int operation,
        Vector<Degrees, double>[] source,
        Vector<Degrees, double> value,
        Vector<Degrees, double>[] destination)
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