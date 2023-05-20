
using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class ToRadiansReducedTests
{
    public static TheoryData<AngleReduced<Degrees, double>, AngleReduced<Radians, double>, AngleReduced<Radians, float>> DegreesToRadiansData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Radians, double>.Zero, Angle<Radians, float>.Zero },
            { Angle<Degrees, double>.Right, Angle<Radians, double>.Right, Angle<Radians, float>.Right },
            { Angle<Degrees, double>.Straight, Angle<Radians, double>.Straight, Angle<Radians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToRadiansData))]
    public void ToRadians_From_Degrees_Should_Succeed(
        AngleReduced<Degrees, double> angle, 
        AngleReduced<Radians, double> expectedDouble, 
        AngleReduced<Radians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRadians(angle);
        var resultFloat = Angle.ToRadians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<AngleReduced<Radians, double>, AngleReduced<Radians, double>, AngleReduced<Radians, float>> RadiansToRadiansData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Radians, double>.Zero, Angle<Radians, float>.Zero },
            { Angle<Radians, double>.Right, Angle<Radians, double>.Right, Angle<Radians, float>.Right },
            { Angle<Radians, double>.Straight, Angle<Radians, double>.Straight, Angle<Radians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToRadiansData))]
    public void ToRadians_From_Radians_Should_Succeed(
        AngleReduced<Radians, double> angle, 
        AngleReduced<Radians, double> expectedDouble, 
        AngleReduced<Radians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRadians(angle);
        var resultFloat = Angle.ToRadians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<AngleReduced<Gradians, double>, AngleReduced<Radians, double>, AngleReduced<Radians, float>> GradiansToRadiansData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Radians, double>.Zero, Angle<Radians, float>.Zero },
            { Angle<Gradians, double>.Right, Angle<Radians, double>.Right, Angle<Radians, float>.Right },
            { Angle<Gradians, double>.Straight, Angle<Radians, double>.Straight, Angle<Radians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToRadiansData))]
    public void ToRadians_From_Gradians_Should_Succeed(
        AngleReduced<Gradians, double> angle, 
        AngleReduced<Radians, double> expectedDouble, 
        AngleReduced<Radians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRadians(angle);
        var resultFloat = Angle.ToRadians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<AngleReduced<Revolutions, double>, AngleReduced<Radians, double>, AngleReduced<Radians, float>> RevolutionsToRadiansData 
        => new()
        {
            { Angle<Revolutions, double>.Zero, Angle<Radians, double>.Zero, Angle<Radians, float>.Zero },
            { Angle<Revolutions, double>.Right, Angle<Radians, double>.Right, Angle<Radians, float>.Right },
            { Angle<Revolutions, double>.Straight, Angle<Radians, double>.Straight, Angle<Radians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RevolutionsToRadiansData))]
    public void ToRadians_From_Revolutions_Should_Succeed(
        AngleReduced<Revolutions, double> angle, 
        AngleReduced<Radians, double> expectedDouble, 
        AngleReduced<Radians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRadians(angle);
        var resultFloat = Angle.ToRadians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }
}
