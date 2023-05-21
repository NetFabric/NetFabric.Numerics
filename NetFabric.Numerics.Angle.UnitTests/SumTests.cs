namespace NetFabric.Numerics.UnitTests;

public class SumTests
{
    public static TheoryData<Angle<Degrees, double>[], Angle<Degrees, double>> SumData 
        => new()
        {
            {Array.Empty<Angle<Degrees, double>>(), new Angle<Degrees, double>(0)},
            {new Angle<Degrees, double>[] { new(1.0), new(11.0) }, new Angle<Degrees, double>(12.0)},
            {new Angle<Degrees, double>[] { new(1.0), new(-11.0) }, new Angle<Degrees, double>(-10.0)},
            { Enumerable.Range(0, 1_000).Select(value => new Angle<Degrees, double>(1.0)).ToArray(), new Angle<Degrees, double>(1_000.0)},
        };

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_For_Enumerable_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double> expected)
    {
        // arrange

        // act
        var result = source.AsEnumerable().Sum();

        // assert
        result.Value.Should().Be(expected.Value);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_For_Array_Should_Succeed(Angle<Degrees, double>[] source, Angle<Degrees, double> expected)
    {
        // arrange

        // act
        var result = source.Sum();

        // assert
        result.Value.Should().Be(expected.Value);
    }
}
