namespace NetFabric.Numerics.UnitTests;

public class DegreesTests
{
    [Fact]
    public void Constants_Should_Succeed()
    {
        Angle<Degrees, double>.Zero.Value.Should().Be(0.0);
        Angle<Degrees, double>.Right.Value.Should().Be(90.0);
        Angle<Degrees, double>.Straight.Value.Should().Be(180.0);
        Angle<Degrees, double>.Full.Value.Should().Be(360.0);
        Angle<Degrees, double>.MinValue.Value.Should().Be(double.MinValue);
        Angle<Degrees, double>.MaxValue.Value.Should().Be(double.MaxValue);
    }

    public static TheoryData<double> ConstructorData => new()
        {
            { -360.0 },
            { 0.0 },
            { 90.0 },
            { 180.0 },
            { 360.0 },
            { 720.0 },
        };

    [Theory]
    [MemberData(nameof(ConstructorData))]
    public void Constructor_Should_Succeed(double value)
    {
        // arrange

        // act
        var resultFloat = new Angle<Degrees, float>((float)value);
        var resultDouble = new Angle<Degrees, double>(value);
        var resultDecimal = new Angle<Degrees, decimal>((decimal)value);

        // assert
        resultFloat.Value.Should().Be((float)value);
        resultDouble.Value.Should().Be(value);
        resultDecimal.Value.Should().Be((decimal)value);
    }
}
