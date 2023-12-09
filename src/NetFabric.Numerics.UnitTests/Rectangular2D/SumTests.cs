namespace NetFabric.Numerics.Rectangular2D.UnitTests;

public class SumTests
{
    /*

    public static TheoryData<Vector<double>[], Vector<double>> SumData 
        => new()
        {
            {
                Array.Empty<Vector<double>>(), 
                new Vector<double>(0, 0)
            },
            {
                new Vector<double>[] { new(1.0, 2.0) }, 
                new Vector<double>(1.0, 2.0)
            },
            {
                new Vector<double>[] { new(1.0, 2.0), new(11.0, 12.0) }, 
                new Vector<double>(12.0, 14.0)
            },
            { 
                Enumerable.Range(0, 97).Select(value => new Vector<double>(value, value + 1)).ToArray(), 
                new Vector<double>(Enumerable.Range(0, 97).Sum(), Enumerable.Range(0, 97).Select(value => value + 1).Sum())
            },
        };

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_For_Enumerable_Should_Succeed(Vector<double>[] source, Vector<double> expected)
    {
        // arrange
        var enumerable = new ReadOnlyCollection<Vector<double>>(source);

        // act
        var result = enumerable.Sum();

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_For_Array_Should_Succeed(Vector<double>[] source, Vector<double> expected)
    {
        // arrange

        // act
        var result = source.Sum();

        // assert
        result.Should().Be(expected);
    }

    */
}
