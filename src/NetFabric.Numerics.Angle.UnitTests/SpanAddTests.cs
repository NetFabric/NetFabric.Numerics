namespace NetFabric.Numerics.UnitTests;

public class SpanAddTests
{
    public static TheoryData<Angle<Degrees, double>[], Angle<Degrees, double>, Angle<Degrees, double>[]> AddValueData
        => new()
        {
            {
                Array.Empty<Angle<Degrees, double>>(),
                new Angle<Degrees, double>(1),
                Array.Empty<Angle<Degrees, double>>()
            },
            {
                new Angle<Degrees, double>[] { new(1.0) },
                new Angle<Degrees, double>(2.0),
                new Angle<Degrees, double>[] { new(3.0) }
            },
            {
                new Angle<Degrees, double>[] { new(1.0), new(11.0) },
                new Angle<Degrees, double>(12.0),
                new Angle<Degrees, double>[] { new(13.0), new(23.0) }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value)).ToArray(),
                new Angle<Degrees, double>(3.0),
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value + 3.0)).ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(AddValueData))]
    public void Add_Value_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double> value, Angle<Degrees, double>[] expected)
    {
        // arrange
        var result = new Angle<Degrees, double>[source.Length];

        // act
        Angle.Add(source, value, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddValueData))]
    public void Add_Value_Inplace_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double> value, Angle<Degrees, double>[] expected)
    {
        // arrange

        // act
        Angle.Add(source, value, source);

        // assert
        source.Should().Equal(expected);
    }

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

    public static TheoryData<Angle<Degrees, double>[], Angle<Degrees, double>, Angle<Degrees, double>[]> SubtractValueData
        => new()
        {
            {
                Array.Empty<Angle<Degrees, double>>(),
                new Angle<Degrees, double>(1),
                Array.Empty<Angle<Degrees, double>>()
            },
            {
                new Angle<Degrees, double>[] { new(1.0) },
                new Angle<Degrees, double>(2.0),
                new Angle<Degrees, double>[] { new(-1.0) }
            },
            {
                new Angle<Degrees, double>[] { new(1.0), new(11.0) },
                new Angle<Degrees, double>(12.0),
                new Angle<Degrees, double>[] { new(-11.0), new(-1.0) }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value - 48.0)).ToArray(),
                new Angle<Degrees, double>(5.5),
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value - 53.5)).ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(SubtractValueData))]
    public void Subtract_Value_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double> value, Angle<Degrees, double>[] expected)
    {
        // arrange
        var result = new Angle<Degrees, double>[source.Length];

        // act
        Angle.Subtract(source, value, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(SubtractValueData))]
    public void Subtract_Value_Inplace_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double> value, Angle<Degrees, double>[] expected)
    {
        // arrange

        // act
        Angle.Subtract(source, value, source);

        // assert
        source.Should().Equal(expected);
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

    public static TheoryData<Angle<Degrees, double>[], Angle<Degrees, double>, Angle<Degrees, double>[]> MultiplyValueData
        => new()
        {
            {
                Array.Empty<Angle<Degrees, double>>(),
                new Angle<Degrees, double>(2),
                Array.Empty<Angle<Degrees, double>>()
            },
            {
                new Angle<Degrees, double>[] { new(1.5) },
                new Angle<Degrees, double>(2.0),
                new Angle<Degrees, double>[] { new(3.0) }
            },
            {
                new Angle<Degrees, double>[] { new(1.0), new(-11.0) },
                new Angle<Degrees, double>(-3.0),
                new Angle<Degrees, double>[] { new(-3.0), new(33.0) }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value - 48.0)).ToArray(),
                new Angle<Degrees, double>(-1.5),
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>((value - 48.0) * -1.5)).ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(MultiplyValueData))]
    public void Multiply_Value_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double> value, Angle<Degrees, double>[] expected)
    {
        // arrange
        var result = new Angle<Degrees, double>[source.Length];

        // act
        Angle.Multiply(source, value, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(MultiplyValueData))]
    public void Multiply_Value_Inplace_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double> value, Angle<Degrees, double>[] expected)
    {
        // arrange

        // act
        Angle.Multiply(source, value, source);

        // assert
        source.Should().Equal(expected);
    }

    public static TheoryData<Angle<Degrees, double>[], Angle<Degrees, double>, Angle<Degrees, double>[]> DivideValueData
        => new()
        {
            {
                Array.Empty<Angle<Degrees, double>>(),
                new Angle<Degrees, double>(2),
                Array.Empty<Angle<Degrees, double>>()
            },
            {
                new Angle<Degrees, double>[] { new(3.0) },
                new Angle<Degrees, double>(2.0),
                new Angle<Degrees, double>[] { new(1.5) }
            },
            {
                new Angle<Degrees, double>[] { new(3.0), new(-12.0) },
                new Angle<Degrees, double>(-3.0),
                new Angle<Degrees, double>[] { new(-1.0), new(4.0) }
            },
            {
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>(value - 48.0)).ToArray(),
                new Angle<Degrees, double>(2.5),
                Enumerable.Range(0, 97).Select(value => new Angle<Degrees, double>((value - 48.0) / 2.5)).ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(DivideValueData))]
    public void Divide_Value_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double> value, Angle<Degrees, double>[] expected)
    {
        // arrange
        var result = new Angle<Degrees, double>[source.Length];

        // act
        Angle.Divide(source, value, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(DivideValueData))]
    public void Divide_Value_Inplace_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double> value, Angle<Degrees, double>[] expected)
    {
        // arrange

        // act
        Angle.Divide(source, value, source);

        // assert
        source.Should().Equal(expected);
    }

    public static TheoryData<Angle<Degrees, float>[], Angle<Degrees, float>, Angle<Degrees, float>[]> AddValueDataFloat
        => new()
        {
            {
                Array.Empty<Angle<Degrees, float>>(),
                new Angle<Degrees, float>(1f),
                Array.Empty<Angle<Degrees, float>>()
            },
            {
                new Angle<Degrees, float>[] { new(1f), new(-11f) },
                new Angle<Degrees, float>(2f),
                new Angle<Degrees, float>[] { new(3f), new(-9f) }
            },
        };

    [Theory]
    [MemberData(nameof(AddValueDataFloat))]
    public void Add_Value_Float_Should_Succeed(Angle<Degrees, float>[] source, Angle<Degrees, float> value, Angle<Degrees, float>[] expected)
    {
        // arrange
        var result = new Angle<Degrees, float>[source.Length];

        // act
        Angle.Add(source, value, result);

        // assert
        result.Should().Equal(expected);
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

    public static TheoryData<Angle<Degrees, float>[], Angle<Degrees, float>> FloatValueData
        => new()
        {
            {
                Array.Empty<Angle<Degrees, float>>(),
                new Angle<Degrees, float>(1f)
            },
            {
                new Angle<Degrees, float>[] { new(1f), new(-11f) },
                new Angle<Degrees, float>(2f)
            },
        };

    [Theory]
    [MemberData(nameof(FloatValueData))]
    public void Subtract_Value_Float_Should_Succeed(Angle<Degrees, float>[] source, Angle<Degrees, float> value)
    {
        // arrange
        var expected = source
            .Select(item => new Angle<Degrees, float>(item.Value - value.Value))
            .ToArray();
        var result = new Angle<Degrees, float>[source.Length];

        // act
        Angle.Subtract(source, value, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(FloatValueData))]
    public void Multiply_Value_Float_Should_Succeed(Angle<Degrees, float>[] source, Angle<Degrees, float> value)
    {
        // arrange
        var expected = source
            .Select(item => new Angle<Degrees, float>(item.Value * value.Value))
            .ToArray();
        var result = new Angle<Degrees, float>[source.Length];

        // act
        Angle.Multiply(source, value, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(FloatValueData))]
    public void Divide_Value_Float_Should_Succeed(Angle<Degrees, float>[] source, Angle<Degrees, float> value)
    {
        // arrange
        var expected = source
            .Select(item => new Angle<Degrees, float>(item.Value / value.Value))
            .ToArray();
        var result = new Angle<Degrees, float>[source.Length];

        // act
        Angle.Divide(source, value, result);

        // assert
        result.Should().Equal(expected);
    }
}