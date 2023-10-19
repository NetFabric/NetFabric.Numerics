namespace NetFabric.Numerics.Cartesian3.UnitTests;

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
        result.Coordinates[2].Should().Be(new Coordinate("Z", typeof(double)));
    }

    static readonly double sqr2 = double.Sqrt(2.0);

    public static TheoryData<Point<double>, Spherical.Point<Degrees, double, double>> ToSphericalData => new()
        {
            { new(0.0,  0.0, 0.0),  new(0.0,  new(0.0),   new(0.0)) },

            { new(0.0,  0.0, 1.0),   new(1.0,  new(0.0),   new(0.0)) },
            { new(0.0,  0.0, -1.0),  new(1.0,  new(0.0),   new(180.0)) },

            { new(1.0,  0.0, 0.0),  new(1.0,  new(0.0),   new(90.0)) },
            { new(1.0,  1.0, 0.0),  new(sqr2, new(45.0),  new(90.0)) },
            { new(0.0,  1.0, 0.0),  new(1.0,  new(90.0),  new(90.0)) },
            { new(-1.0, 1.0, 0.0),  new(sqr2, new(135.0), new(90.0)) },
            { new(-1.0, 0.0, 0.0),  new(1.0,  new(180.0), new(90.0)) },
            { new(-1.0, -1.0, 0.0), new(sqr2, new(225.0), new(90.0)) },
            { new(0.0,  -1.0, 0.0), new(1.0,  new(270.0), new(90.0)) },
            { new(1.0,  -1.0, 0.0), new(sqr2, new(315.0), new(90.0)) },

            { new(0.7071067811865475, 0.7071067811865475, 1.7320508075688774), new(2.0, new(45.0), new(30.0)) },
            { new(1.856006667768587E-16, 3.031088913245535, 1.7500000000000004), new(3.5, new(90.0), new(60.0)) },
        };

    [Theory]
    [MemberData(nameof(ToSphericalData))]
    public void ToSpherical_Should_Succeed(Point<double> point, Spherical.Point<Degrees, double, double> expected)
    {
        // arrange

        // act
        var result = Spherical.Point.Reduce(Spherical.Point.ToDegrees(Point.ToSpherical(point)));

        // assert
        result.Radius.Should().BeApproximately(expected.Radius, 0.0001);
        result.Azimuth.Value.Should().BeApproximately(expected.Azimuth.Value, 0.0001);
        result.Polar.Value.Should().BeApproximately(expected.Polar.Value, 0.0001);
    }
}