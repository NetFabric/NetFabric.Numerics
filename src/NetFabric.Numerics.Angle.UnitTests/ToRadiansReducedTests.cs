namespace NetFabric.Numerics.UnitTests;

public class ToRadiansReducedTests
{
    public static TheoryData<AngleReduced<Degrees, double>, AngleReduced<Radians, double>> DegreesToRadiansData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Radians, double>.Zero },
            { Angle<Degrees, double>.Right, Angle<Radians, double>.Right },
            { Angle<Degrees, double>.Straight, Angle<Radians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToRadiansData))]
    public void ToRadians_From_Degrees_Should_Succeed(
        AngleReduced<Degrees, double> angle, 
        AngleReduced<Radians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToRadians(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<AngleReduced<Radians, double>, AngleReduced<Radians, double>> RadiansToRadiansData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Radians, double>.Zero},
            { Angle<Radians, double>.Right, Angle<Radians, double>.Right },
            { Angle<Radians, double>.Straight, Angle<Radians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToRadiansData))]
    public void ToRadians_From_Radians_Should_Succeed(
        AngleReduced<Radians, double> angle, 
        AngleReduced<Radians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToRadians(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<AngleReduced<Gradians, double>, AngleReduced<Radians, double>> GradiansToRadiansData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Radians, double>.Zero },
            { Angle<Gradians, double>.Right, Angle<Radians, double>.Right },
            { Angle<Gradians, double>.Straight, Angle<Radians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToRadiansData))]
    public void ToRadians_From_Gradians_Should_Succeed(
        AngleReduced<Gradians, double> angle, 
        AngleReduced<Radians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToRadians(angle);

        // assert
        result.Value.Should().BeApproximately(expected.Value, 0.000000001);
    }

    public static TheoryData<AngleReduced<Revolutions, double>, AngleReduced<Radians, double>> RevolutionsToRadiansData 
        => new()
        {
            { Angle<Revolutions, double>.Zero, Angle<Radians, double>.Zero },
            { Angle<Revolutions, double>.Right, Angle<Radians, double>.Right },
            { Angle<Revolutions, double>.Straight, Angle<Radians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(RevolutionsToRadiansData))]
    public void ToRadians_From_Revolutions_Should_Succeed(
        AngleReduced<Revolutions, double> angle, 
        AngleReduced<Radians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToRadians(angle);

        // assert
        result.Should().Be(expected);
    }
}
