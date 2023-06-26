namespace NetFabric.Numerics.Geography.Geodetic2.UnitTests;

public class PointTests
{
    [Fact]
    public void Zero_Should_Succeed()
    {
        // arrange

        // act
        var result = Point<WGS84, Degrees, double>.Zero;

        // assert
        result.Latitude.Should().Be(Angle<Degrees, double>.Zero);
        result.Longitude.Should().Be(Angle<Degrees, double>.Zero);
    }

    [Fact]
    public void CoordinateSystem_Should_Succeed()
    {
        // arrange

        // act
        var result = Point<WGS84, Degrees, double>.Zero.CoordinateSystem;

        // assert
        result.Datum.Name.Should().Be("WGS 84");
        //result.Coordinates.Should().Equal(
        //    new Coordinate("Latitude", typeof(Angle<Degrees, double>)),
        //    new Coordinate("Longitude", typeof(Angle<Degrees, double>)));
    }
}
