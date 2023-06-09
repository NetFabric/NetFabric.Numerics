namespace NetFabric.Numerics.UnitTests;

public class ToDegreesTests
{
    public static TheoryData<Angle<Degrees, double>, Angle<Degrees, double>> DegreesToDegreesData 
        => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Degrees, double>.Zero },
            { Angle<Degrees, double>.Right, Angle<Degrees, double>.Right },
            { Angle<Degrees, double>.Straight, Angle<Degrees, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(DegreesToDegreesData))]
    public void ToDegrees_From_Degrees_Should_Succeed(
        Angle<Degrees, double> angle, 
        Angle<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToDegrees(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Angle<Radians, double>, Angle<Degrees, double>> RadiansToDegreesData 
        => new()
        {
            { Angle<Radians, double>.Zero, Angle<Degrees, double>.Zero },
            { Angle<Radians, double>.Right, Angle<Degrees, double>.Right },
            { Angle<Radians, double>.Straight, Angle<Degrees, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(RadiansToDegreesData))]
    public void ToDegrees_From_Radians_Should_Succeed(
        Angle<Radians, double> angle, 
        Angle<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToDegrees(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Angle<Gradians, double>, Angle<Degrees, double>> GradiansToDegreesData 
        => new()
        {
            { Angle<Gradians, double>.Zero, Angle<Degrees, double>.Zero },
            { Angle<Gradians, double>.Right, Angle<Degrees, double>.Right },
            { Angle<Gradians, double>.Straight, Angle<Degrees, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(GradiansToDegreesData))]
    public void ToDegrees_From_Gradians_Should_Succeed(
        Angle<Gradians, double> angle,
        Angle<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToDegrees(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Angle<Revolutions, double>, Angle<Degrees, double>> RevolutionsToDegreesData 
        => new()
        {
            { Angle<Revolutions, double>.Zero, Angle<Degrees, double>.Zero },
            { Angle<Revolutions, double>.Right, Angle<Degrees, double>.Right },
            { Angle<Revolutions, double>.Straight, Angle<Degrees, double>.Straight },
        };

    [Theory]
    [MemberData(nameof(RevolutionsToDegreesData))]
    public void ToDegrees_From_Revolutions_Should_Succeed(
        Angle<Revolutions, double> angle, 
        Angle<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToDegrees(angle);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<int, double, Angle<Degrees, double>> DegreesMinutesData
        => new()
        {
            { 0, 0.0, Angle<Degrees, double>.Zero },
            { 90, 0.0, Angle<Degrees, double>.Right },
            { 90, 30.0, new Angle<Degrees, double>(90.5) },
            { 10, 11.0, new Angle<Degrees, double>(10.18333333333333) },
            { -10, 11.0, new Angle<Degrees, double>(-10.18333333333333) },
        };

    [Theory]
    [MemberData(nameof(DegreesMinutesData))]
    public void ToDegrees_From_DegreesMinutes_Should_Succeed(
        int degrees,
        double minutes,
        Angle<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToDegrees<double>(degrees, minutes);

        // assert
        result.Value.Should().BeApproximately(expected.Value, 0.000001);
    }

    [Theory]
    [MemberData(nameof(DegreesMinutesData))]
    public void ToDegreesMinutes_Should_Succeed(
        int expectedDegrees,
        double expectedMinutes,
        Angle<Degrees, double> angle)
    {
        // arrange

        // act
        var (degrees, minutes) = Angle.ToDegreesMinutes<double, int, double>(angle);

        // assert
        degrees.Should().Be(expectedDegrees);
        minutes.Should().BeApproximately(expectedMinutes, 0.000001);
    }

    public static TheoryData<int, int, double, Angle<Degrees, double>> DegreesMinutesSecondsData
        => new()
        {
            { 0, 0, 0.0, Angle<Degrees, double>.Zero },
            { 90, 0, 0.0, Angle<Degrees, double>.Right },
            { 90, 30, 0.0, new Angle<Degrees, double>(90.5) },
            { 90, 0, 30.0, new Angle<Degrees, double>(90.00833333) },
            { 90, 30, 30.0, new Angle<Degrees, double>(90.50833333) },
            { 10, 11, 12.0, new Angle<Degrees, double>(10.18666666666667) },
            { -10, 11, 12.0, new Angle<Degrees, double>(-10.18666666666667) },
        };

    [Theory]
    [MemberData(nameof(DegreesMinutesSecondsData))]
    public void ToDegrees_From_DegreesMinutesSeconds_Should_Succeed(
        int degrees,
        int minutes,
        double seconds,
        Angle<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Angle.ToDegrees<double>(degrees, minutes, seconds);

        // assert
        result.Value.Should().BeApproximately(expected.Value, 0.000001);
    }

    [Theory]
    [MemberData(nameof(DegreesMinutesSecondsData))]
    public void ToDegreesMinutesSeconds_Should_Succeed(
        int expectedDegrees,
        int expectedMinutes,
        double expectedSeconds,
        Angle<Degrees, double> angle)
    {
        // arrange

        // act
        var (degrees, minutes, seconds) = Angle.ToDegreesMinutesSeconds<double, int, int, double>(angle);

        // assert
        degrees.Should().Be(expectedDegrees);
        minutes.Should().Be(expectedMinutes);
        seconds.Should().BeApproximately(expectedSeconds, 0.0001);
    }

}
