namespace NetFabric.Numerics.UnitTests;

public class AverageTests
{
    public static TheoryData<Angle<Degrees, double>[], Angle<Degrees, double>?> AverageData 
        => new()
        {
            {Array.Empty<Angle<Degrees, double>>(), null},
            {new Angle<Degrees, double>[] { new(1.0) }, new Angle<Degrees, double>(1.0)},
            {new Angle<Degrees, double>[] { new(1.0), new(2.0) }, new Angle<Degrees, double>(1.5)},
            { Enumerable.Range(0, 1_000).Select(value => new Angle<Degrees, double>(1.0)).ToArray(), new Angle<Degrees, double>(1.0)},
        };

    [Theory]
    [MemberData(nameof(AverageData))]
    public void Average_For_Enumerable_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double>? expected)
    {
        // arrange

        // act
        var result = source.AsEnumerable().Average();

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(AverageData))]
    public void Average_For_Array_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double>? expected)
    {
        // arrange

        // act
        var result = source.Average();

        // assert
        result.Should().Be(expected);
    }
}
