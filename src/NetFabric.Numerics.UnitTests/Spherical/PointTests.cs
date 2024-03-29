namespace NetFabric.Numerics.Spherical.UnitTests;

public class PointTests
{
    [Fact]
    public void CoordinateSystem_Should_Succeed()
    {
        // arrange
        IGeometricBase point = Point<Degrees, double>.Zero;

        // act
        var result = point.CoordinateSystem;

        // assert
        result.Coordinates[0].Should().Be(new Coordinate("Radius", typeof(double)));
        result.Coordinates[1].Should().Be(new Coordinate("Azimuth", typeof(Angle<Degrees, double>)));
        result.Coordinates[2].Should().Be(new Coordinate("Polar", typeof(Angle<Degrees, double>)));
    }

    const double radius = 2.0;
    static readonly double radiusCos45 = radius * Angle.Cos(Angle.ToRadians(new Angle<Degrees, double>(45.0)));

    public static TheoryData<Point<Degrees, double>, Rectangular3D.Point<double>> ToRectangularData => new()
        {
            { new(0.0, new(0.0), new(0.0)), new(0.0, 0.0, 0.0) },

            { new(2.0, new(0.0),  new(0.0)),  new(0.0,   0.0,   2.0) },
            { new(2.0, new(60.0),  new(0.0)),  new(0.0,   0.0,   2.0) },
            { new(2.0, new(90.0),  new(0.0)),  new(0.0,   0.0,   2.0) },

            { new(2.0, new(0.0),  new(180.0)),  new(0.0,   0.0,   -2.0) },
            { new(2.0, new(60.0),  new(180.0)),  new(0.0,   0.0,   -2.0) },
            { new(2.0, new(90.0),  new(180.0)),  new(0.0,   0.0,   -2.0) },

            { new(2.0, new(45.0), new(30.0)), new(0.7071067811865475, 0.7071067811865475, 1.7320508075688774) },
            { new(3.5, new(90.0), new(60.0)), new(1.856006667768587E-16, 3.031088913245535, 1.7500000000000004) },

            { new(radius, new(0.0),   new(90.0)), new(radius,       0.0,          0.0) },
            { new(radius, new(45.0),  new(90.0)), new(radiusCos45,  radiusCos45,  0.0) },
            { new(radius, new(90.0),  new(90.0)), new(0.0,          radius,       0.0) },
            { new(radius, new(135.0), new(90.0)), new(-radiusCos45, radiusCos45,  0.0) },
            { new(radius, new(180.0), new(90.0)), new(-radius,      0.0,          0.0) },
            { new(radius, new(225.0), new(90.0)), new(-radiusCos45, -radiusCos45, 0.0) },
            { new(radius, new(270.0), new(90.0)), new(0.0,          -radius,      0.0) },
            { new(radius, new(315.0), new(90.0)), new(radiusCos45,  -radiusCos45, 0.0) },
            { new(radius, new(360.0), new(90.0)), new(radius,       0.0,          0.0) },
        };

    [Theory]
    [MemberData(nameof(ToRectangularData))]
    public void ToRectangular_Should_Succeed(Point<Degrees, double> point, Rectangular3D.Point<double> expected)
    {
        // arrange

        // act
        var result = Point.ToRectangular(Point.ToRadians(point));

        // assert
        result.Should().BeOfType<Rectangular3D.Point<double>>();
        result.X.Should().BeApproximately(expected.X, 0.0001);
        result.Y.Should().BeApproximately(expected.Y, 0.0001);
        result.Z.Should().BeApproximately(expected.Z, 0.0001);
    }

    public static TheoryData<Point<Degrees, double>, Point<Degrees, double>> ReduceData => new()
        {
            { new(2.0, new(0.0), new(0.0)),     new(2.0, new(0.0), new(0.0)) },
            { new(2.0, new(0.0), new(45.0)),   new(2.0, new(0.0), new(45.0)) },
            { new(2.0, new(0.0), new(90.0)),   new(2.0, new(0.0), new(90.0)) },
            { new(2.0, new(0.0), new(135.0)),   new(2.0, new(0.0), new(135.0)) },
            { new(2.0, new(0.0), new(180.0)),   new(2.0, new(0.0), new(180.0)) },
            { new(2.0, new(0.0), new(225.0)),   new(2.0, new(0.0), new(135.0)) },
            { new(2.0, new(0.0), new(270.0)),   new(2.0, new(0.0), new(90.0)) },
            { new(2.0, new(0.0), new(315.0)),   new(2.0, new(0.0), new(45.0)) },
            { new(2.0, new(0.0), new(360.0)),   new(2.0, new(0.0), new(0.0)) },
        };

    [Theory]
    [MemberData(nameof(ReduceData))]
    public void Reduce_Should_Succeed(Point<Degrees, double> point, Point<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Point.Reduce(point);

        // assert
        result.Radius.Should().BeApproximately(expected.Radius, 0.0001);
        result.Azimuth.Value.Should().BeApproximately(expected.Azimuth.Value, 0.0001);
        result.Polar.Value.Should().BeApproximately(expected.Polar.Value, 0.0001);
    }

}