namespace NetFabric.Numerics.Polar.UnitTests;

public class VectorIntTests
{
    public static TheoryData<Vector<Degrees, decimal>, Vector<Degrees, decimal>> Data => new()
    {
        { new(0.0m, new(0.0m)), new(0.0m, new(0.0m)) },
        { new(1.0m, new(0.0m)), new(0.0m, new(0.0m)) },
        { new(0.0m, new(1.0m)), new(0.0m, new(0.0m)) },
        { new(0.0m, new(0.0m)), new(1.0m, new(0.0m)) },
        { new(0.0m, new(0.0m)), new(0.0m, new(1.0m)) },
        { new(1.0m, new(1.0m)), new(1.0m, new(1.0m)) },
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void Equals_Should_Succeed(Vector<Degrees, decimal> left, Vector<Degrees, decimal> right)
    {
        // arrange
        var expected = left.Radius == right.Radius
            && left.Azimuth == right.Azimuth;

        // act
        var result = left.Equals(right);

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void Add_Should_Succeed(Vector<Degrees, decimal> left, Vector<Degrees, decimal> right)
    {
        // arrange
        var expected = new Vector<Degrees, decimal>(
            left.Radius + right.Radius,
            left.Azimuth + right.Azimuth);

        // act
        var result = left + right;

        // assert
        result.GetType().Should().Be(typeof(Vector<Degrees, decimal>));
        result.Radius.Should().Be(expected.Radius);
        result.Azimuth.Should().Be(expected.Azimuth);
    }
}
