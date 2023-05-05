
using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class DegreesTests
{
    public static TheoryData<Degrees<double>, Radians<float>> ToRadiansData => new()
        {
            { Degrees<double>.Zero, Radians<float>.Zero },
            { Degrees<double>.Right, Radians<float>.Right },
            { Degrees<double>.Straight, Radians<float>.Straight },
            { Degrees<double>.Full, Radians<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToRadiansData))]
    public void ToRadians_Should_Succeed(Degrees<double> angle, Radians<float> expected)
    {
        // arrange

        // act
        var result = angle.ToRadians<float>();

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Degrees<double>, Degrees<float>> ToDegreesData => new()
        {
            { Degrees<double>.Zero, Degrees<float>.Zero },
            { Degrees<double>.Right, Degrees<float>.Right },
            { Degrees<double>.Straight, Degrees<float>.Straight },
            { Degrees<double>.Full, Degrees<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToDegreesData))]
    public void ToDegrees_Should_Succeed(Degrees<double> angle, Degrees<float> expected)
    {
        // arrange

        // act
        var result = angle.ToDegrees<float>();

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Degrees<double>, Gradians<float>> ToGradiansData => new()
        {
            { Degrees<double>.Zero, Gradians<float>.Zero },
            { Degrees<double>.Right, Gradians<float>.Right },
            { Degrees<double>.Straight, Gradians<float>.Straight },
            { Degrees<double>.Full, Gradians<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToGradiansData))]
    public void ToGradians_Should_Succeed(Degrees<double> angle, Gradians<float> expected)
    {
        // arrange

        // act
        var result = angle.ToGradians<float>();

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Degrees<double>, Revolutions<float>> ToRevolutionsData => new()
        {
            { Degrees<double>.Zero, Revolutions<float>.Zero },
            { Degrees<double>.Right, Revolutions<float>.Right },
            { Degrees<double>.Straight, Revolutions<float>.Straight },
            { Degrees<double>.Full, Revolutions<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToRevolutionsData))]
    public void ToRevolutions_Should_Succeed(Degrees<double> angle, Revolutions<float> expected)
    {
        // arrange

        // act
        var result = angle.ToRevolutions<float>();

        // assert
        result.Should().Be(expected);
    }
}
