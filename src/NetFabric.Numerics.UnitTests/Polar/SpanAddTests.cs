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
