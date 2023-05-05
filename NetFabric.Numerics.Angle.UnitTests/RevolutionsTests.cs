
using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class RevolutionsTests
{
    public static TheoryData<Revolutions<double>, Radians<float>> ToRadiansData => new()
        {
            { Revolutions<double>.Zero, Radians<float>.Zero },
            { Revolutions<double>.Right, Radians<float>.Right },
            { Revolutions<double>.Straight, Radians<float>.Straight },
            { Revolutions<double>.Full, Radians<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToRadiansData))]
    public void ToRadians_Should_Succeed(Revolutions<double> angle, Radians<float> expected)
    {
        // arrange

        // act
        var result = angle.ToRadians<float>();

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Revolutions<double>, Degrees<float>> ToDegreesData => new()
        {
            { Revolutions<double>.Zero, Degrees<float>.Zero },
            { Revolutions<double>.Right, Degrees<float>.Right },
            { Revolutions<double>.Straight, Degrees<float>.Straight },
            { Revolutions<double>.Full, Degrees<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToDegreesData))]
    public void ToDegrees_Should_Succeed(Revolutions<double> angle, Degrees<float> expected)
    {
        // arrange

        // act
        var result = angle.ToDegrees<float>();

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Revolutions<double>, Gradians<float>> ToGradiansData => new()
        {
            { Revolutions<double>.Zero, Gradians<float>.Zero },
            { Revolutions<double>.Right, Gradians<float>.Right },
            { Revolutions<double>.Straight, Gradians<float>.Straight },
            { Revolutions<double>.Full, Gradians<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToGradiansData))]
    public void ToGradians_Should_Succeed(Revolutions<double> angle, Gradians<float> expected)
    {
        // arrange

        // act
        var result = angle.ToGradians<float>();

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Revolutions<double>, Revolutions<float>> ToRevolutionsData => new()
        {
            { Revolutions<double>.Zero, Revolutions<float>.Zero },
            { Revolutions<double>.Right, Revolutions<float>.Right },
            { Revolutions<double>.Straight, Revolutions<float>.Straight },
            { Revolutions<double>.Full, Revolutions<float>.Full },
        };

    [Theory]
    [MemberData(nameof(ToRevolutionsData))]
    public void ToRevolutions_Should_Succeed(Revolutions<double> angle, Revolutions<float> expected)
    {
        // arrange

        // act
        var result = angle.ToRevolutions<float>();

        // assert
        result.Should().Be(expected);
    }
}
