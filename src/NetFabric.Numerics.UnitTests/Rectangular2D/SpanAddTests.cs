namespace NetFabric.Numerics.Rectangular2D.UnitTests;

public class SpanVectorAddTests
{
    public static TheoryData<Vector<double>[], Vector<double>, Vector<double>[]> AddValueData 
        => new()
        {
            {
                Array.Empty<Vector<double>>(), 
                new Vector<double>(1.0, 1.0), 
                Array.Empty<Vector<double>>()
            },
            {
                new Vector<double>[] { new(1.0, 2.0) }, 
                new Vector<double>(3.0, 4.0), 
                new Vector<double>[] { new(4.0, 6.0) }
            },
            {
                new Vector<double>[] { new(1.0, 2.0), new(11.0, 12.0) }, 
                new Vector<double>(3.0, 4.0), 
                new Vector<double>[] { new(4.0, 6.0), new(14.0, 16.0) }
            },
            { 
                Enumerable.Range(0, 97).Select(value => new Vector<double>(value, value + 1)).ToArray(), 
                new Vector<double>(3.0, 4.0), 
                Enumerable.Range(0, 97).Select(value => new Vector<double>(value + 3, value + 5)).ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(AddValueData))]
    public void Add_Value_Should_Succeed(Vector<double>[] source, Vector<double> value, Vector<double>[] expected)
    {
        // arrange
        var result = new Vector<double>[source.Length];

        // act
        Vector.Add(source, value, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddValueData))]
    public void Add_Value_Inplace_Should_Succeed(Vector<double>[] source, Vector<double> value, Vector<double>[] expected)
    {
        // arrange

        // act
        Vector.Add(source, value, source);

        // assert
        source.Should().Equal(expected);
    }
}
