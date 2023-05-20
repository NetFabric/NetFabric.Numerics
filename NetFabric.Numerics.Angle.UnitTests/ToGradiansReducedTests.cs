
using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class ToGradiansReducedTests
{
    public static TheoryData<AngleReduced<Degrees, double>, AngleReduced<Gradians, double>, AngleReduced<Gradians, float>> DegreesToGradiansData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Gradians, double>.Zero, Angle<Gradians, float>.Zero },
            { Angle<Degrees, double>.Right, Angle<Gradians, double>.Right, Angle<Gradians, float>.Right },
            { Angle<Degrees, double>.Straight, Angle<Gradians, double>.Straight, Angle<Gradians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToGradiansData))]
    public void ToGradians_From_Degrees_Should_Succeed(
        AngleReduced<Degrees, double> angle, 
        AngleReduced<Gradians, double> expectedDouble, 
        AngleReduced<Gradians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToGradians(angle);
        var resultFloat = Angle.ToGradians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<AngleReduced<Radians, double>, AngleReduced<Gradians, double>, AngleReduced<Gradians, float>> RadiansToGradiansData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Gradians, double>.Zero, Angle<Gradians, float>.Zero },
            { Angle<Radians, double>.Right, Angle<Gradians, double>.Right, Angle<Gradians, float>.Right },
            { Angle<Radians, double>.Straight, Angle<Gradians, double>.Straight, Angle<Gradians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToGradiansData))]
    public void ToGradians_From_Radians_Should_Succeed(
        AngleReduced<Radians, double> angle, 
        AngleReduced<Gradians, double> expectedDouble, 
        AngleReduced<Gradians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToGradians(angle);
        var resultFloat = Angle.ToGradians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<AngleReduced<Gradians, double>, AngleReduced<Gradians, double>, AngleReduced<Gradians, float>> GradiansToGradiansData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Gradians, double>.Zero, Angle<Gradians, float>.Zero },
            { Angle<Gradians, double>.Right, Angle<Gradians, double>.Right, Angle<Gradians, float>.Right },
            { Angle<Gradians, double>.Straight, Angle<Gradians, double>.Straight, Angle<Gradians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToGradiansData))]
    public void ToGradians_From_Gradians_Should_Succeed(
        AngleReduced<Gradians, double> angle, 
        AngleReduced<Gradians, double> expectedDouble, 
        AngleReduced<Gradians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToGradians(angle);
        var resultFloat = Angle.ToGradians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<AngleReduced<Revolutions, double>, AngleReduced<Gradians, double>, AngleReduced<Gradians, float>> RevolutionsToGradiansData 
        => new()
        {
            { Angle<Revolutions, double>.Zero, Angle<Gradians, double>.Zero, Angle<Gradians, float>.Zero },
            { Angle<Revolutions, double>.Right, Angle<Gradians, double>.Right, Angle<Gradians, float>.Right },
            { Angle<Revolutions, double>.Straight, Angle<Gradians, double>.Straight, Angle<Gradians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RevolutionsToGradiansData))]
    public void ToGradians_From_Revolutions_Should_Succeed(
        AngleReduced<Revolutions, double> angle, 
        AngleReduced<Gradians, double> expectedDouble, 
        AngleReduced<Gradians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToGradians(angle);
        var resultFloat = Angle.ToGradians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }
}
