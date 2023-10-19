namespace NetFabric.Numerics.UnitTests;

public class ToRevolutionsTests
{
    public static TheoryData<Angle<Degrees, double>, Angle<Revolutions, double>> DegreesToRevolutionsData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Revolutions, double>.Zero },
            { Angle<Degrees, double>.Right, Angle<Revolutions, double>.Right },
            { Angle<Degrees, double>.Straight, Angle<Revolutions, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToRevolutionsData))]
    public void ToRevolutions_From_Degrees_Should_Succeed(
        Angle<Degrees, double> angle, 
        Angle<Revolutions, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToRevolutions(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Angle<Radians, double>, Angle<Revolutions, double>> RadiansToRevolutionsData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Revolutions, double>.Zero },
            { Angle<Radians, double>.Right, Angle<Revolutions, double>.Right },
            { Angle<Radians, double>.Straight, Angle<Revolutions, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToRevolutionsData))]
    public void ToRevolutions_From_Radians_Should_Succeed(
        Angle<Radians, double> angle, 
        Angle<Revolutions, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToRevolutions(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Angle<Gradians, double>, Angle<Revolutions, double>> GradiansToRevolutionsData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Revolutions, double>.Zero },
            { Angle<Gradians, double>.Right, Angle<Revolutions, double>.Right },
            { Angle<Gradians, double>.Straight, Angle<Revolutions, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToRevolutionsData))]
    public void ToRevolutions_From_Gradians_Should_Succeed(
        Angle<Gradians, double> angle, 
        Angle<Revolutions, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToRevolutions(angle);

        // assert
        result.Should().Be(expected);
    }
}
