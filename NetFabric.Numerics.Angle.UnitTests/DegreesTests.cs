
using FluentAssertions;

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
        var result = new Angle<Degrees, double>(value);

        // assert
        result.Value.Should().Be(value);
    }

    public static TheoryData<Angle<Degrees, double>, Angle<Degrees, float>> ToDegreesData => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Degrees, float>.Zero },
            { Angle<Degrees, double>.Right, Angle<Degrees, float>.Right },
            { Angle<Degrees, double>.Straight, Angle<Degrees, float>.Straight },
            { Angle<Degrees, double>.Full, Angle<Degrees, float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToDegreesData))]
    public void ToDegrees_Should_Succeed(Angle<Degrees, double> angle, Angle<Degrees, float> expected)
    {
        // arrange

        // act
        var result = Angle.ToDegrees<double, float>(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Angle<Degrees, double>, Angle<Radians, double>, Angle<Radians, float>> ToRadiansData => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Radians, double>.Zero, Angle<Radians, float>.Zero },
            { Angle<Degrees, double>.Right, Angle<Radians, double>.Right, Angle<Radians, float>.Right },
            { Angle<Degrees, double>.Straight, Angle<Radians, double>.Straight, Angle<Radians, float>.Straight },
            { Angle<Degrees, double>.Full, Angle<Radians, double>.Full, Angle<Radians, float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToRadiansData))]
    public void ToRadians_Should_Succeed(Angle<Degrees, double> angle, Angle<Radians, double> expectedDouble, Angle<Radians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRadians(angle);
        var resultFloat = Angle.ToRadians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

     public static TheoryData<Angle<Degrees, double>, Angle<Gradians, double>, Angle<Gradians, float>> ToGradiansData => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Gradians, double>.Zero, Angle<Gradians, float>.Zero },
            { Angle<Degrees, double>.Right, Angle<Gradians, double>.Right, Angle<Gradians, float>.Right },
            { Angle<Degrees, double>.Straight, Angle<Gradians, double>.Straight, Angle<Gradians, float>.Straight },
            { Angle<Degrees, double>.Full, Angle<Gradians, double>.Full, Angle<Gradians, float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToGradiansData))]
    public void ToGradians_Should_Succeed(Angle<Degrees, double> angle, Angle<Gradians, double> expectedDouble, Angle<Gradians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToGradians(angle);
        var resultFloat = Angle.ToGradians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<Angle<Degrees, double>, Angle<Revolutions, double>, Angle<Revolutions, float>> ToRevolutionsData => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Revolutions, double>.Zero, Angle<Revolutions, float>.Zero },
            { Angle<Degrees, double>.Right, Angle<Revolutions, double>.Right, Angle<Revolutions, float>.Right },
            { Angle<Degrees, double>.Straight, Angle<Revolutions, double>.Straight, Angle<Revolutions, float>.Straight },
            { Angle<Degrees, double>.Full, Angle<Revolutions, double>.Full, Angle<Revolutions, float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToRevolutionsData))]
    public void ToRevolutions_Should_Succeed(Angle<Degrees, double> angle, Angle<Revolutions, double> expectedDouble, Angle<Revolutions, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRevolutions(angle);
        var resultFloat = Angle.ToRevolutions<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }
}
