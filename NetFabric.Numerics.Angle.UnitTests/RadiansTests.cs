
using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class RadiansTests
{
    public static TheoryData<Radians<double>, Radians<float>> ToRadiansData => new()
        {
            { Radians<double>.Zero, Radians<float>.Zero },
            { Radians<double>.Right, Radians<float>.Right },
            { Radians<double>.Straight, Radians<float>.Straight },
            { Radians<double>.Full, Radians<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToRadiansData))]
    public void ToRadians_Should_Succeed(Radians<double> angle, Radians<float> expected)
    {
        // arrange

        // act
        var result = angle.ToRadians<float>();

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Radians<double>, Degrees<float>> ToDegreesData => new()
        {
            { Radians<double>.Zero, Degrees<float>.Zero },
            { Radians<double>.Right, Degrees<float>.Right },
            { Radians<double>.Straight, Degrees<float>.Straight },
            { Radians<double>.Full, Degrees<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToDegreesData))]
    public void ToDegrees_Should_Succeed(Radians<double> angle, Degrees<float> expected)
    {
        // arrange

        // act
        var result = angle.ToDegrees<float>();

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Radians<double>, Gradians<float>> ToGradiansData => new()
        {
            { Radians<double>.Zero, Gradians<float>.Zero },
            { Radians<double>.Right, Gradians<float>.Right },
            { Radians<double>.Straight, Gradians<float>.Straight },
            { Radians<double>.Full, Gradians<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToGradiansData))]
    public void ToGradians_Should_Succeed(Radians<double> angle, Gradians<float> expected)
    {
        // arrange

        // act
        var result = angle.ToGradians<float>();

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Radians<double>, Revolutions<float>> ToRevolutionsData => new()
        {
            { Radians<double>.Zero, Revolutions<float>.Zero },
            { Radians<double>.Right, Revolutions<float>.Right },
            { Radians<double>.Straight, Revolutions<float>.Straight },
            { Radians<double>.Full, Revolutions<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToRevolutionsData))]
    public void ToRevolutions_Should_Succeed(Radians<double> angle, Revolutions<float> expected)
    {
        // arrange

        // act
        var result = angle.ToRevolutions<float>();

        // assert
        result.Should().Be(expected);
    }
}
