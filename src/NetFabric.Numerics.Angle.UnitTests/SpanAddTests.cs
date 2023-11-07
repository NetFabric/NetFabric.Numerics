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
}
