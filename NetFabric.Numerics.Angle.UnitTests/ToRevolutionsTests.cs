
using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class ToRevolutionsTests
{
    public static TheoryData<Angle<Degrees, double>, Angle<Revolutions, double>, Angle<Revolutions, float>> DegreesToRevolutionsData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Revolutions, double>.Zero, Angle<Revolutions, float>.Zero },
            { Angle<Degrees, double>.Right, Angle<Revolutions, double>.Right, Angle<Revolutions, float>.Right },
            { Angle<Degrees, double>.Straight, Angle<Revolutions, double>.Straight, Angle<Revolutions, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToRevolutionsData))]
    public void ToRevolutions_From_Degrees_Should_Succeed(
        Angle<Degrees, double> angle, 
        Angle<Revolutions, double> expectedDouble, 
        Angle<Revolutions, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRevolutions(angle);
        var resultFloat = Angle.ToRevolutions<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<Angle<Radians, double>, Angle<Revolutions, double>, Angle<Revolutions, float>> RadiansToRevolutionsData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Revolutions, double>.Zero, Angle<Revolutions, float>.Zero },
            { Angle<Radians, double>.Right, Angle<Revolutions, double>.Right, Angle<Revolutions, float>.Right },
            { Angle<Radians, double>.Straight, Angle<Revolutions, double>.Straight, Angle<Revolutions, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToRevolutionsData))]
    public void ToRevolutions_From_Radians_Should_Succeed(
        Angle<Radians, double> angle, 
        Angle<Revolutions, double> expectedDouble, 
        Angle<Revolutions, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRevolutions(angle);
        var resultFloat = Angle.ToRevolutions<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<Angle<Gradians, double>, Angle<Revolutions, double>, Angle<Revolutions, float>> GradiansToRevolutionsData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Revolutions, double>.Zero, Angle<Revolutions, float>.Zero },
            { Angle<Gradians, double>.Right, Angle<Revolutions, double>.Right, Angle<Revolutions, float>.Right },
            { Angle<Gradians, double>.Straight, Angle<Revolutions, double>.Straight, Angle<Revolutions, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToRevolutionsData))]
    public void ToRevolutions_From_Gradians_Should_Succeed(
        Angle<Gradians, double> angle, 
        Angle<Revolutions, double> expectedDouble, 
        Angle<Revolutions, float> expectedFloat)
    {
        // arrange

        // act
        var resultDouble = Angle.ToRevolutions(angle);
        var resultFloat = Angle.ToRevolutions<double, float>(angle);

        // assert
        resultDouble.Should().Be(expectedDouble);
        resultFloat.Should().Be(expectedFloat);
    }

    public static TheoryData<Angle<Revolutions, double>, Angle<Revolutions, double>, Angle<Revolutions, float>> RevolutionsToRevolutionsData 
        => new()
        {
            { Angle<Revolutions, double>.Zero, Angle<Revolutions, double>.Zero, Angle<Revolutions, float>.Zero },
            { Angle<Revolutions, double>.Right, Angle<Revolutions, double>.Right, Angle<Revolutions, float>.Right },
            { Angle<Revolutions, double>.Straight, Angle<Revolutions, double>.Straight, Angle<Revolutions, float>.Straight },
        };

    [Theory]
    [MemberData(nameof(RevolutionsToRevolutionsData))]
    public void ToRevolutions_From_Revolutions_Should_Succeed(
        Angle<Revolutions, double> angle, 
        Angle<Revolutions, double> expectedDouble, 
        Angle<Revolutions, float> expectedFloat)
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
