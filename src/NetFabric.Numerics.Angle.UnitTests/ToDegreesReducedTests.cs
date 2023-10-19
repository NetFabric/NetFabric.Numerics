namespace NetFabric.Numerics.UnitTests;

public class ToDegreesReducedTests
{
    public static TheoryData<AngleReduced<Radians, double>, AngleReduced<Degrees, double>> RadiansToDegreesData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Degrees, double>.Zero },
            { Angle<Radians, double>.Right, Angle<Degrees, double>.Right },
            { Angle<Radians, double>.Straight, Angle<Degrees, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToDegreesData))]
    public void ToDegrees_From_Radians_Should_Succeed(
        AngleReduced<Radians, double> angle, 
        AngleReduced<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToDegrees(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<AngleReduced<Gradians, double>, AngleReduced<Degrees, double>> GradiansToDegreesData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Degrees, double>.Zero },
            { Angle<Gradians, double>.Right, Angle<Degrees, double>.Right },
            { Angle<Gradians, double>.Straight, Angle<Degrees, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToDegreesData))]
    public void ToDegrees_From_Gradians_Should_Succeed(
        AngleReduced<Gradians, double> angle, 
        AngleReduced<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToDegrees(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<AngleReduced<Revolutions, double>, AngleReduced<Degrees, double>> RevolutionsToDegreesData 
        => new()
        {
            { Angle<Revolutions, double>.Zero, Angle<Degrees, double>.Zero },
            { Angle<Revolutions, double>.Right, Angle<Degrees, double>.Right },
            { Angle<Revolutions, double>.Straight, Angle<Degrees, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(RevolutionsToDegreesData))]
    public void ToDegrees_From_Revolutions_Should_Succeed(
        AngleReduced<Revolutions, double> angle, 
        AngleReduced<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToDegrees(angle);

        // assert
        result.Should().Be(expected);
    }
}
