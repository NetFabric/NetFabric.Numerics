namespace NetFabric.Numerics.UnitTests;

public class ToGradiansTests
{
    public static TheoryData<Angle<Degrees, double>, Angle<Gradians, double>, Angle<Gradians, float>> DegreesToGradiansData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Gradians, double>.Zero, Angle<Gradians, float>.Zero },
            { Angle<Degrees, double>.Right, Angle<Gradians, double>.Right, Angle<Gradians, float>.Right },
            { Angle<Degrees, double>.Straight, Angle<Gradians, double>.Straight, Angle<Gradians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToGradiansData))]
    public void ToGradians_From_Degrees_Should_Succeed(
        Angle<Degrees, double> angle, 
        Angle<Gradians, double> expectedDouble, 
        Angle<Gradians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToGradians(angle);
        var resultFloat = Angle.ToGradians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<Angle<Radians, double>, Angle<Gradians, double>, Angle<Gradians, float>> RadiansToGradiansData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Gradians, double>.Zero, Angle<Gradians, float>.Zero },
            { Angle<Radians, double>.Right, Angle<Gradians, double>.Right, Angle<Gradians, float>.Right },
            { Angle<Radians, double>.Straight, Angle<Gradians, double>.Straight, Angle<Gradians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToGradiansData))]
    public void ToGradians_From_Radians_Should_Succeed(
        Angle<Radians, double> angle, 
        Angle<Gradians, double> expectedDouble, 
        Angle<Gradians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToGradians(angle);
        var resultFloat = Angle.ToGradians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<Angle<Gradians, double>, Angle<Gradians, double>, Angle<Gradians, float>> GradiansToGradiansData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Gradians, double>.Zero, Angle<Gradians, float>.Zero },
            { Angle<Gradians, double>.Right, Angle<Gradians, double>.Right, Angle<Gradians, float>.Right },
            { Angle<Gradians, double>.Straight, Angle<Gradians, double>.Straight, Angle<Gradians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToGradiansData))]
    public void ToGradians_From_Gradians_Should_Succeed(
        Angle<Gradians, double> angle, 
        Angle<Gradians, double> expectedDouble, 
        Angle<Gradians, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToGradians(angle);
        var resultFloat = Angle.ToGradians<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<Angle<Revolutions, double>, Angle<Gradians, double>, Angle<Gradians, float>> RevolutionsToGradiansData 
        => new()
        {
            { Angle<Revolutions, double>.Zero, Angle<Gradians, double>.Zero, Angle<Gradians, float>.Zero },
            { Angle<Revolutions, double>.Right, Angle<Gradians, double>.Right, Angle<Gradians, float>.Right },
            { Angle<Revolutions, double>.Straight, Angle<Gradians, double>.Straight, Angle<Gradians, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RevolutionsToGradiansData))]
    public void ToGradians_From_Revolutions_Should_Succeed(
        Angle<Revolutions, double> angle, 
        Angle<Gradians, double> expectedDouble, 
        Angle<Gradians, float> expectedFloat)
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
