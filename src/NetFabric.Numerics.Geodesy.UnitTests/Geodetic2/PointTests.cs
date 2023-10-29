namespace NetFabric.Numerics.Geodesy.Geodetic2.UnitTests;

public class PointTests
{
    [Fact]
    public void Zero_Should_Succeed()
    {
        // arrange

        // act
        var result = Point<WGS84<double>, Degrees, double>.Zero;

        // assert
        result.Latitude.Should().Be(Angle<Degrees, double>.Zero);
        result.Longitude.Should().Be(Angle<Degrees, double>.Zero);
    }

    [Fact]
    public void CoordinateSystem_Should_Succeed()
    {
        // arrange
        IGeodeticBase<double> point = Point<WGS84<double>, Degrees, double>.Zero;

        // act
        var result = point.CoordinateSystem;

        // assert
        result.Datum.Name.Should().Be("World Geodetic System 1984 (WGS 84)");
        result.Coordinates.Should().Equal(
            new Coordinate("Latitude", typeof(Angle<Degrees, double>)),
            new Coordinate("Longitude", typeof(Angle<Degrees, double>)));
    }
}
