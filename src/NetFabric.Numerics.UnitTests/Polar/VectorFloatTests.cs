namespace NetFabric.Numerics.Polar.UnitTests;

public class VectorFloatTests
{
    public static TheoryData<Vector<Degrees, float>, Vector<Degrees, float>> Data => new()
    {
        { new(0.0f, new(0.0f)), new(0.0f, new(0.0f)) },
        { new(1.0f, new(0.0f)), new(0.0f, new(0.0f)) },
        { new(0.0f, new(1.0f)), new(0.0f, new(0.0f)) },
        { new(0.0f, new(0.0f)), new(1.0f, new(0.0f)) },
        { new(0.0f, new(0.0f)), new(0.0f, new(1.0f)) },
        { new(1.0f, new(1.0f)), new(1.0f, new(1.0f)) },
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void Equals_Should_Succeed(Vector<Degrees, float> left, Vector<Degrees, float> right)
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
    public void Add_Should_Succeed(Vector<Degrees, float> left, Vector<Degrees, float> right)
    {
        // arrange
        var expected = new Vector<Degrees, float>(
            left.Radius + right.Radius,
            left.Azimuth + right.Azimuth);

        // act
        var result = left + right;

        // assert
        result.GetType().Should().Be(typeof(Vector<Degrees, float>));
        result.Radius.Should().Be(expected.Radius);
        result.Azimuth.Should().Be(expected.Azimuth);
    }
}
