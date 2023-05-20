
using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class ToDegreesReducedTests
{
    public static TheoryData<AngleReduced<Degrees, double>, AngleReduced<Degrees, double>, AngleReduced<Degrees, float>> DegreesToDegreesData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Degrees, double>.Zero, Angle<Degrees, float>.Zero },
            { Angle<Degrees, double>.Right, Angle<Degrees, double>.Right, Angle<Degrees, float>.Right },
            { Angle<Degrees, double>.Straight, Angle<Degrees, double>.Straight, Angle<Degrees, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToDegreesData))]
    public void ToDegrees_From_Degrees_Should_Succeed(
        AngleReduced<Degrees, double> angle, 
        AngleReduced<Degrees, double> expectedDouble, 
        AngleReduced<Degrees, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToDegrees(angle);
        var resultFloat = Angle.ToDegrees<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<AngleReduced<Radians, double>, AngleReduced<Degrees, double>, AngleReduced<Degrees, float>> RadiansToDegreesData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Degrees, double>.Zero, Angle<Degrees, float>.Zero },
            { Angle<Radians, double>.Right, Angle<Degrees, double>.Right, Angle<Degrees, float>.Right },
            { Angle<Radians, double>.Straight, Angle<Degrees, double>.Straight, Angle<Degrees, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToDegreesData))]
    public void ToDegrees_From_Radians_Should_Succeed(
        AngleReduced<Radians, double> angle, 
        AngleReduced<Degrees, double> expectedDouble, 
        AngleReduced<Degrees, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToDegrees(angle);
        var resultFloat = Angle.ToDegrees<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<AngleReduced<Gradians, double>, AngleReduced<Degrees, double>, AngleReduced<Degrees, float>> GradiansToDegreesData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Degrees, double>.Zero, Angle<Degrees, float>.Zero },
            { Angle<Gradians, double>.Right, Angle<Degrees, double>.Right, Angle<Degrees, float>.Right },
            { Angle<Gradians, double>.Straight, Angle<Degrees, double>.Straight, Angle<Degrees, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToDegreesData))]
    public void ToDegrees_From_Gradians_Should_Succeed(
        AngleReduced<Gradians, double> angle, 
        AngleReduced<Degrees, double> expectedDouble, 
        AngleReduced<Degrees, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToDegrees(angle);
        var resultFloat = Angle.ToDegrees<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<AngleReduced<Revolutions, double>, AngleReduced<Degrees, double>, AngleReduced<Degrees, float>> RevolutionsToDegreesData 
        => new()
        {
            { Angle<Revolutions, double>.Zero, Angle<Degrees, double>.Zero, Angle<Degrees, float>.Zero },
            { Angle<Revolutions, double>.Right, Angle<Degrees, double>.Right, Angle<Degrees, float>.Right },
            { Angle<Revolutions, double>.Straight, Angle<Degrees, double>.Straight, Angle<Degrees, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RevolutionsToDegreesData))]
    public void ToDegrees_From_Revolutions_Should_Succeed(
        AngleReduced<Revolutions, double> angle, 
        AngleReduced<Degrees, double> expectedDouble, 
        AngleReduced<Degrees, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToDegrees(angle);
        var resultFloat = Angle.ToDegrees<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }
}
