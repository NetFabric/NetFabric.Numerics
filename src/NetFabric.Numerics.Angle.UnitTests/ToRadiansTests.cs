namespace NetFabric.Numerics.UnitTests;

public class ToRadiansTests
{
    public static TheoryData<Angle<Degrees, double>, Angle<Radians, double>, Angle<Radians, float>> DegreesToRadiansData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Radians, double>.Zero, Angle<Radians, float>.Zero },
            { Angle<Degrees, double>.Right, Angle<Radians, double>.Right, Angle<Radians, float>.Right },
            { Angle<Degrees, double>.Straight, Angle<Radians, double>.Straight, Angle<Radians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToRadiansData))]
    public void ToRadians_From_Degrees_Should_Succeed(
        Angle<Degrees, double> angle, 
        Angle<Radians, double> expectedDouble, 
        Angle<Radians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRadians(angle);
        var resultFloat = Angle.ToRadians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<Angle<Radians, double>, Angle<Radians, double>, Angle<Radians, float>> RadiansToRadiansData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Radians, double>.Zero, Angle<Radians, float>.Zero },
            { Angle<Radians, double>.Right, Angle<Radians, double>.Right, Angle<Radians, float>.Right },
            { Angle<Radians, double>.Straight, Angle<Radians, double>.Straight, Angle<Radians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToRadiansData))]
    public void ToRadians_From_Radians_Should_Succeed(
        Angle<Radians, double> angle, 
        Angle<Radians, double> expectedDouble, 
        Angle<Radians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRadians(angle);
        var resultFloat = Angle.ToRadians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<Angle<Gradians, double>, Angle<Radians, double>, Angle<Radians, float>> GradiansToRadiansData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Radians, double>.Zero, Angle<Radians, float>.Zero },
            { Angle<Gradians, double>.Right, Angle<Radians, double>.Right, Angle<Radians, float>.Right },
            { Angle<Gradians, double>.Straight, Angle<Radians, double>.Straight, Angle<Radians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToRadiansData))]
    public void ToRadians_From_Gradians_Should_Succeed(
        Angle<Gradians, double> angle, 
        Angle<Radians, double> expectedDouble, 
        Angle<Radians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRadians(angle);
        var resultFloat = Angle.ToRadians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<Angle<Revolutions, double>, Angle<Radians, double>, Angle<Radians, float>> RevolutionsToRadiansData 
        => new()
        {
            { Angle<Revolutions, double>.Zero, Angle<Radians, double>.Zero, Angle<Radians, float>.Zero },
            { Angle<Revolutions, double>.Right, Angle<Radians, double>.Right, Angle<Radians, float>.Right },
            { Angle<Revolutions, double>.Straight, Angle<Radians, double>.Straight, Angle<Radians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RevolutionsToRadiansData))]
    public void ToRadians_From_Revolutions_Should_Succeed(
        Angle<Revolutions, double> angle, 
        Angle<Radians, double> expectedDouble, 
        Angle<Radians, float> expectedFloat)
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
