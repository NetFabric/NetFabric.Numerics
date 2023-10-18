namespace NetFabric.Numerics.Cartesian2.UnitTests;

public class PointTests
{
    [Fact]
    public void CoordinateSystem_Should_Succeed()
    {
        // arrange

        // act
        var result = Point<double>.Zero.CoordinateSystem;

        // assert
        result.Coordinates[0].Should().Be(new Coordinate("X", typeof(double)));
        result.Coordinates[1].Should().Be(new Coordinate("Y", typeof(double)));
    }

    static readonly double sqr2 = double.Sqrt(2.0);

    public static TheoryData<Point<double>, Polar.Point<Degrees, double, double>> ToPolarData => new()
        {
            { new(0.0,  0.0),  new(0.0,  new(0.0)) },
            { new(1.0,  0.0),  new(1.0,  new(0.0)) },
            { new(1.0,  1.0),  new(sqr2, new(45.0)) },
            { new(0.0,  1.0),  new(1.0,  new(90.0)) },
            { new(-1.0, 1.0),  new(sqr2, new(135.0)) },
            { new(-1.0, 0.0),  new(1.0,  new(180.0)) },
            { new(-1.0, -1.0), new(sqr2, new(225.0)) },
            { new(0.0,  -1.0), new(1.0,  new(270.0)) },
            { new(1.0,  -1.0), new(sqr2, new(315.0)) },
        };

    [Theory]
    [MemberData(nameof(ToPolarData))]
    public void ToPolar_Should_Succeed(Point<double> point, Polar.Point<Degrees, double, double> expected)
    {
        // arrange

        // act
        var result = Polar.Point.Reduce(Polar.Point.ToDegrees(Point.ToPolar(point)));

        // assert
        result.Radius.Should().BeApproximately(expected.Radius, 0.0001);
        result.Azimuth.Value.Should().BeApproximately(expected.Azimuth.Value, 0.0001);
    }
}