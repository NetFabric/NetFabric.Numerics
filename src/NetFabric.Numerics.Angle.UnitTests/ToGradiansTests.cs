namespace NetFabric.Numerics.UnitTests;

public class ToGradiansTests
{
    public static TheoryData<Angle<Degrees, double>, Angle<Gradians, double>> DegreesToGradiansData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Gradians, double>.Zero },
            { Angle<Degrees, double>.Right, Angle<Gradians, double>.Right },
            { Angle<Degrees, double>.Straight, Angle<Gradians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToGradiansData))]
    public void ToGradians_From_Degrees_Should_Succeed(
        Angle<Degrees, double> angle, 
        Angle<Gradians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToGradians(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Angle<Radians, double>, Angle<Gradians, double>> RadiansToGradiansData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Gradians, double>.Zero },
            { Angle<Radians, double>.Right, Angle<Gradians, double>.Right },
            { Angle<Radians, double>.Straight, Angle<Gradians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToGradiansData))]
    public void ToGradians_From_Radians_Should_Succeed(
        Angle<Radians, double> angle, 
        Angle<Gradians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToGradians(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Angle<Revolutions, double>, Angle<Gradians, double>> RevolutionsToGradiansData 
        => new()
        {
            { Angle<Revolutions, double>.Zero, Angle<Gradians, double>.Zero },
            { Angle<Revolutions, double>.Right, Angle<Gradians, double>.Right },
            { Angle<Revolutions, double>.Straight, Angle<Gradians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(RevolutionsToGradiansData))]
    public void ToGradians_From_Revolutions_Should_Succeed(
        Angle<Revolutions, double> angle, 
        Angle<Gradians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToGradians(angle);

        // assert
        result.Should().Be(expected);
    }
}
