namespace NetFabric.Numerics.Polar.UnitTests;

public class PointTests
{
    [Fact]
    public void CoordinateSystem_Should_Succeed()
    {
        // arrange

        // act
        var result = Point<double, Degrees, double>.Zero.CoordinateSystem;

        // assert
        result.Coordinates[0].Should().Be(new Coordinate("Radius", typeof(double)));
        result.Coordinates[1].Should().Be(new Coordinate("Azimuth", typeof(Angle<Degrees, double>)));
    }

    const double radius = 2.0;
    static readonly double radiusCos45 = radius * Angle.Cos(Angle.ToRadians(new Angle<Degrees, double>(45.0)));

    public static TheoryData<Point<double, Degrees, double>, Cartesian2.Point<double>> ToCartesianData => new()
        {
            { new(0.0, new(0.0)), new(0.0, 0.0) },

            { new(radius, new(0.0)),   new(radius,       0.0) },
            { new(radius, new(45.0)),  new(radiusCos45,  radiusCos45) },
            { new(radius, new(90.0)),  new(0.0,          radius) },
            { new(radius, new(135.0)), new(-radiusCos45, radiusCos45) },
            { new(radius, new(180.0)), new(-radius,      0.0) },
            { new(radius, new(225.0)), new(-radiusCos45, -radiusCos45) },
            { new(radius, new(270.0)), new(0.0,          -radius) },
            { new(radius, new(315.0)), new(radiusCos45,  -radiusCos45) },
            { new(radius, new(360.0)), new(radius,       0.0) },
        };

    [Theory]
    [MemberData(nameof(ToCartesianData))]
    public void ToCartesian_Should_Succeed(Point<double, Degrees, double> point, Cartesian2.Point<double> expected)
    {
        // arrange

        // act
        var result = Point.ToCartesian(Point.ToRadians(point));

        // assert
        result.Should().BeOfType<Cartesian2.Point<double>>();
        result.X.Should().BeApproximately(expected.X, 0.0001);
        result.Y.Should().BeApproximately(expected.Y, 0.0001);
    }

    [Theory]
    [MemberData(nameof(ToCartesianData))]
    public void ToCartesian_With_Conversion_Should_Succeed(Point<double, Degrees, double> point, Cartesian2.Point<double> expected)
    {
        // arrange

        // act
        var result = Point.ToCartesian<double, double, float>(Point.ToRadians(point));

        // assert
        result.Should().BeOfType<Cartesian2.Point<float>>();
        ((double)result.X).Should().BeApproximately(expected.X, 0.0001);
        ((double)result.Y).Should().BeApproximately(expected.Y, 0.0001);
    }
}