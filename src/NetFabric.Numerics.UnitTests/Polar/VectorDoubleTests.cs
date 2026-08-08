namespace NetFabric.Numerics.Polar.UnitTests;

public class VectorDoubleTests
{
    public static TheoryData<Vector<Degrees, double>, Vector<Degrees, double>> Data => new()
    {
        { new(0.0, new(0.0)), new(0.0, new(0.0)) },
        { new(1.0, new(0.0)), new(0.0, new(0.0)) },
        { new(0.0, new(1.0)), new(0.0, new(0.0)) },
        { new(0.0, new(0.0)), new(1.0, new(0.0)) },
        { new(0.0, new(0.0)), new(0.0, new(1.0)) },
        { new(1.0, new(1.0)), new(1.0, new(1.0)) },
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void Equals_Should_Succeed(Vector<Degrees, double> left, Vector<Degrees, double> right)
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
    public void Add_Should_Succeed(Vector<Degrees, double> left, Vector<Degrees, double> right)
    {
        // arrange
        var expected = new Vector<Degrees, double>(
            left.Radius + right.Radius,
            left.Azimuth + right.Azimuth);

        // act
        var result = left + right;

        // assert
        result.GetType().Should().Be(typeof(Vector<Degrees, double>));
        result.Radius.Should().Be(expected.Radius);
        result.Azimuth.Should().Be(expected.Azimuth);
    }
}