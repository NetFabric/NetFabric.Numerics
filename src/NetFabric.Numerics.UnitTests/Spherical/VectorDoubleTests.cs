namespace NetFabric.Numerics.Spherical.UnitTests;

public class VectorDoubleTests
{
    public static TheoryData<Vector<Degrees, double>, Vector<Degrees, double>> Data => new()
    {
        { new(0.0, Angle<Degrees, double>.Zero, Angle<Degrees, double>.Zero), new(0.0, Angle<Degrees, double>.Zero, Angle<Degrees, double>.Zero) },
        { new(1.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Straight), new(0.0, Angle<Degrees, double>.Zero, Angle<Degrees, double>.Zero) },
        { new(0.0, Angle<Degrees, double>.Zero, Angle<Degrees, double>.Zero), new(1.0, Angle<Degrees, double>.Right, Angle<Degrees, double>.Straight) },
        { new(1.0, new Angle<Degrees, double>(45.0), new Angle<Degrees, double>(60.0)), new(1.0, new Angle<Degrees, double>(45.0), new Angle<Degrees, double>(60.0)) },
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void Equals_Should_Succeed(Vector<Degrees, double> left, Vector<Degrees, double> right)
    {
        // arrange
        var expected = left.Radius.Equals(right.Radius)
            && left.Azimuth.Equals(right.Azimuth)
            && left.Polar.Equals(right.Polar);

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
            left.Azimuth + right.Azimuth,
            left.Polar + right.Polar);

        // act
        var result = left + right;

        // assert
        result.GetType().Should().Be(typeof(Vector<Degrees, double>));
        result.Should().Be(expected);
    }
}