namespace NetFabric.Numerics.UnitTests;

public class ToDegreesTests
{
    public static TheoryData<Angle<Degrees, double>, Angle<Degrees, double>, Angle<Degrees, float>> DegreesToDegreesData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Degrees, double>.Zero, Angle<Degrees, float>.Zero },
            { Angle<Degrees, double>.Right, Angle<Degrees, double>.Right, Angle<Degrees, float>.Right },
            { Angle<Degrees, double>.Straight, Angle<Degrees, double>.Straight, Angle<Degrees, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToDegreesData))]
    public void ToDegrees_From_Degrees_Should_Succeed(
        Angle<Degrees, double> angle, 
        Angle<Degrees, double> expectedDouble, 
        Angle<Degrees, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToDegrees(angle);
        var resultFloat = Angle.ToDegrees<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<Angle<Radians, double>, Angle<Degrees, double>, Angle<Degrees, float>> RadiansToDegreesData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Degrees, double>.Zero, Angle<Degrees, float>.Zero },
            { Angle<Radians, double>.Right, Angle<Degrees, double>.Right, Angle<Degrees, float>.Right },
            { Angle<Radians, double>.Straight, Angle<Degrees, double>.Straight, Angle<Degrees, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToDegreesData))]
    public void ToDegrees_From_Radians_Should_Succeed(
        Angle<Radians, double> angle, 
        Angle<Degrees, double> expectedDouble, 
        Angle<Degrees, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToDegrees(angle);
        var resultFloat = Angle.ToDegrees<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<Angle<Gradians, double>, Angle<Degrees, double>, Angle<Degrees, float>> GradiansToDegreesData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Degrees, double>.Zero, Angle<Degrees, float>.Zero },
            { Angle<Gradians, double>.Right, Angle<Degrees, double>.Right, Angle<Degrees, float>.Right },
            { Angle<Gradians, double>.Straight, Angle<Degrees, double>.Straight, Angle<Degrees, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToDegreesData))]
    public void ToDegrees_From_Gradians_Should_Succeed(
        Angle<Gradians, double> angle, 
        Angle<Degrees, double> expectedDouble, 
        Angle<Degrees, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToDegrees(angle);
        var resultFloat = Angle.ToDegrees<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<Angle<Revolutions, double>, Angle<Degrees, double>, Angle<Degrees, float>> RevolutionsToDegreesData 
        => new()
        {
            { Angle<Revolutions, double>.Zero, Angle<Degrees, double>.Zero, Angle<Degrees, float>.Zero },
            { Angle<Revolutions, double>.Right, Angle<Degrees, double>.Right, Angle<Degrees, float>.Right },
            { Angle<Revolutions, double>.Straight, Angle<Degrees, double>.Straight, Angle<Degrees, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RevolutionsToDegreesData))]
    public void ToDegrees_From_Revolutions_Should_Succeed(
        Angle<Revolutions, double> angle, 
        Angle<Degrees, double> expectedDouble, 
        Angle<Degrees, float> expectedFloat)
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
