namespace NetFabric.Numerics.UnitTests;

public class ToRevolutionsReducedTests
{
    public static TheoryData<AngleReduced<Degrees, double>, AngleReduced<Revolutions, double>, AngleReduced<Revolutions, float>> DegreesToRevolutionsData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Revolutions, double>.Zero, Angle<Revolutions, float>.Zero },
            { Angle<Degrees, double>.Right, Angle<Revolutions, double>.Right, Angle<Revolutions, float>.Right },
            { Angle<Degrees, double>.Straight, Angle<Revolutions, double>.Straight, Angle<Revolutions, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToRevolutionsData))]
    public void ToRevolutions_From_Degrees_Should_Succeed(
        AngleReduced<Degrees, double> angle, 
        AngleReduced<Revolutions, double> expectedDouble, 
        AngleReduced<Revolutions, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRevolutions(angle);
        var resultFloat = Angle.ToRevolutions<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<AngleReduced<Radians, double>, AngleReduced<Revolutions, double>, AngleReduced<Revolutions, float>> RadiansToRevolutionsData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Revolutions, double>.Zero, Angle<Revolutions, float>.Zero },
            { Angle<Radians, double>.Right, Angle<Revolutions, double>.Right, Angle<Revolutions, float>.Right },
            { Angle<Radians, double>.Straight, Angle<Revolutions, double>.Straight, Angle<Revolutions, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToRevolutionsData))]
    public void ToRevolutions_From_Radians_Should_Succeed(
        AngleReduced<Radians, double> angle, 
        AngleReduced<Revolutions, double> expectedDouble, 
        AngleReduced<Revolutions, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRevolutions(angle);
        var resultFloat = Angle.ToRevolutions<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<AngleReduced<Gradians, double>, AngleReduced<Revolutions, double>, AngleReduced<Revolutions, float>> GradiansToRevolutionsData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Revolutions, double>.Zero, Angle<Revolutions, float>.Zero },
            { Angle<Gradians, double>.Right, Angle<Revolutions, double>.Right, Angle<Revolutions, float>.Right },
            { Angle<Gradians, double>.Straight, Angle<Revolutions, double>.Straight, Angle<Revolutions, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToRevolutionsData))]
    public void ToRevolutions_From_Gradians_Should_Succeed(
        AngleReduced<Gradians, double> angle, 
        AngleReduced<Revolutions, double> expectedDouble, 
        AngleReduced<Revolutions, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRevolutions(angle);
        var resultFloat = Angle.ToRevolutions<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<AngleReduced<Revolutions, double>, AngleReduced<Revolutions, double>, AngleReduced<Revolutions, float>> RevolutionsToRevolutionsData 
        => new()
        {
            { Angle<Revolutions, double>.Zero, Angle<Revolutions, double>.Zero, Angle<Revolutions, float>.Zero },
            { Angle<Revolutions, double>.Right, Angle<Revolutions, double>.Right, Angle<Revolutions, float>.Right },
            { Angle<Revolutions, double>.Straight, Angle<Revolutions, double>.Straight, Angle<Revolutions, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RevolutionsToRevolutionsData))]
    public void ToRevolutions_From_Revolutions_Should_Succeed(
        AngleReduced<Revolutions, double> angle, 
        AngleReduced<Revolutions, double> expectedDouble, 
        AngleReduced<Revolutions, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRevolutions(angle);
        var resultFloat = Angle.ToRevolutions<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }
}
