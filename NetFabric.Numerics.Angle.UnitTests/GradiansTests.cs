
using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class GradiansTests
{
    public static TheoryData<Gradians<double>, Radians<float>> ToRadiansData => new()
        {
            { Gradians<double>.Zero, Radians<float>.Zero },
            { Gradians<double>.Right, Radians<float>.Right },
            { Gradians<double>.Straight, Radians<float>.Straight },
            { Gradians<double>.Full, Radians<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToRadiansData))]
    public void ToRadians_Should_Succeed(Gradians<double> angle, Radians<float> expected)
    {
        // arrange

        // act
        var result = angle.ToRadians<float>();

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Gradians<double>, Degrees<float>> ToDegreesData => new()
        {
            { Gradians<double>.Zero, Degrees<float>.Zero },
            { Gradians<double>.Right, Degrees<float>.Right },
            { Gradians<double>.Straight, Degrees<float>.Straight },
            { Gradians<double>.Full, Degrees<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToDegreesData))]
    public void ToDegrees_Should_Succeed(Gradians<double> angle, Degrees<float> expected)
    {
        // arrange

        // act
        var result = angle.ToDegrees<float>();

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Gradians<double>, Gradians<float>> ToGradiansData => new()
        {
            { Gradians<double>.Zero, Gradians<float>.Zero },
            { Gradians<double>.Right, Gradians<float>.Right },
            { Gradians<double>.Straight, Gradians<float>.Straight },
            { Gradians<double>.Full, Gradians<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToGradiansData))]
    public void ToGradians_Should_Succeed(Gradians<double> angle, Gradians<float> expected)
    {
        // arrange

        // act
        var result = angle.ToGradians<float>();

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Gradians<double>, Revolutions<float>> ToRevolutionsData => new()
        {
            { Gradians<double>.Zero, Revolutions<float>.Zero },
            { Gradians<double>.Right, Revolutions<float>.Right },
            { Gradians<double>.Straight, Revolutions<float>.Straight },
            { Gradians<double>.Full, Revolutions<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToRevolutionsData))]
    public void ToRevolutions_Should_Succeed(Gradians<double> angle, Revolutions<float> expected)
    {
        // arrange

        // act
        var result = angle.ToRevolutions<float>();

        // assert
        result.Should().Be(expected);
    }
}
