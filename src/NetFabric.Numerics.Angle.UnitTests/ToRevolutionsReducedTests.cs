namespace NetFabric.Numerics.UnitTests;

public class ToRevolutionsReducedTests
{
    public static TheoryData<AngleReduced<Degrees, double>, AngleReduced<Revolutions, double>> DegreesToRevolutionsData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Revolutions, double>.Zero },
            { Angle<Degrees, double>.Right, Angle<Revolutions, double>.Right },
            { Angle<Degrees, double>.Straight, Angle<Revolutions, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToRevolutionsData))]
    public void ToRevolutions_From_Degrees_Should_Succeed(
        AngleReduced<Degrees, double> angle, 
        AngleReduced<Revolutions, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToRevolutions(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<AngleReduced<Radians, double>, AngleReduced<Revolutions, double>> RadiansToRevolutionsData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Revolutions, double>.Zero },
            { Angle<Radians, double>.Right, Angle<Revolutions, double>.Right },
            { Angle<Radians, double>.Straight, Angle<Revolutions, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToRevolutionsData))]
    public void ToRevolutions_From_Radians_Should_Succeed(
        AngleReduced<Radians, double> angle, 
        AngleReduced<Revolutions, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToRevolutions(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<AngleReduced<Gradians, double>, AngleReduced<Revolutions, double>> GradiansToRevolutionsData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Revolutions, double>.Zero },
            { Angle<Gradians, double>.Right, Angle<Revolutions, double>.Right },
            { Angle<Gradians, double>.Straight, Angle<Revolutions, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToRevolutionsData))]
    public void ToRevolutions_From_Gradians_Should_Succeed(
        AngleReduced<Gradians, double> angle, 
        AngleReduced<Revolutions, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToRevolutions(angle);

        // assert
        result.Should().Be(expected);
    }
}
