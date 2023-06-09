namespace NetFabric.Numerics.UnitTests;

public class ToRadiansTests
{
    public static TheoryData<Angle<Degrees, double>, Angle<Radians, double>> DegreesToRadiansData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Radians, double>.Zero },
            { Angle<Degrees, double>.Right, Angle<Radians, double>.Right },
            { Angle<Degrees, double>.Straight, Angle<Radians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToRadiansData))]
    public void ToRadians_From_Degrees_Should_Succeed(
        Angle<Degrees, double> angle, 
        Angle<Radians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToRadians(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Angle<Radians, double>, Angle<Radians, double>> RadiansToRadiansData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Radians, double>.Zero },
            { Angle<Radians, double>.Right, Angle<Radians, double>.Right },
            { Angle<Radians, double>.Straight, Angle<Radians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToRadiansData))]
    public void ToRadians_From_Radians_Should_Succeed(
        Angle<Radians, double> angle, 
        Angle<Radians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToRadians(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Angle<Gradians, double>, Angle<Radians, double>> GradiansToRadiansData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Radians, double>.Zero },
            { Angle<Gradians, double>.Right, Angle<Radians, double>.Right },
            { Angle<Gradians, double>.Straight, Angle<Radians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToRadiansData))]
    public void ToRadians_From_Gradians_Should_Succeed(
        Angle<Gradians, double> angle, 
        Angle<Radians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToRadians(angle);

        // assert
        result.Value.Should().BeApproximately(expected.Value, 0.000000001);
    }

    public static TheoryData<Angle<Revolutions, double>, Angle<Radians, double>> RevolutionsToRadiansData 
        => new()
        {
            { Angle<Revolutions, double>.Zero, Angle<Radians, double>.Zero },
            { Angle<Revolutions, double>.Right, Angle<Radians, double>.Right },
            { Angle<Revolutions, double>.Straight, Angle<Radians, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(RevolutionsToRadiansData))]
    public void ToRadians_From_Revolutions_Should_Succeed(
        Angle<Revolutions, double> angle, 
        Angle<Radians, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToRadians(angle);

        // assert
        result.Should().Be(expected);
    }
}
