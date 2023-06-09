namespace NetFabric.Numerics.UnitTests;

public class ToGradiansReducedTests
{
    public static TheoryData<AngleReduced<Degrees, double>, AngleReduced<Gradians, double>> DegreesToGradiansData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Gradians, double>.Zero },
            { Angle<Degrees, double>.Right, Angle<Gradians, double>.Right },
            { Angle<Degrees, double>.Straight, Angle<Gradians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToGradiansData))]
    public void ToGradians_From_Degrees_Should_Succeed(
        AngleReduced<Degrees, double> angle, 
        AngleReduced<Gradians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToGradians(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<AngleReduced<Radians, double>, AngleReduced<Gradians, double>> RadiansToGradiansData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Gradians, double>.Zero },
            { Angle<Radians, double>.Right, Angle<Gradians, double>.Right },
            { Angle<Radians, double>.Straight, Angle<Gradians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToGradiansData))]
    public void ToGradians_From_Radians_Should_Succeed(
        AngleReduced<Radians, double> angle, 
        AngleReduced<Gradians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToGradians(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<AngleReduced<Gradians, double>, AngleReduced<Gradians, double>> GradiansToGradiansData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Gradians, double>.Zero },
            { Angle<Gradians, double>.Right, Angle<Gradians, double>.Right },
            { Angle<Gradians, double>.Straight, Angle<Gradians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToGradiansData))]
    public void ToGradians_From_Gradians_Should_Succeed(
        AngleReduced<Gradians, double> angle, 
        AngleReduced<Gradians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToGradians(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<AngleReduced<Revolutions, double>, AngleReduced<Gradians, double>> RevolutionsToGradiansData 
        => new()
        {
            { Angle<Revolutions, double>.Zero, Angle<Gradians, double>.Zero },
            { Angle<Revolutions, double>.Right, Angle<Gradians, double>.Right },
            { Angle<Revolutions, double>.Straight, Angle<Gradians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(RevolutionsToGradiansData))]
    public void ToGradians_From_Revolutions_Should_Succeed(
        AngleReduced<Revolutions, double> angle, 
        AngleReduced<Gradians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToGradians(angle);

        // assert
        result.Should().Be(expected);
    }
}
