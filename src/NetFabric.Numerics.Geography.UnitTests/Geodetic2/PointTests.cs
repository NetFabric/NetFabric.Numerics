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

var wgs84Point = new Point<WGS84, Degrees, double>(new(0.0), new(0.0));                    // Geodetic point using WGS84 datum
var wgs1972Point = new Point<WGS1972, Degrees, double>(new(0.0), new(0.0));                // Geodetic point using WGS1972 datum
var nad83Point = new Point<NAD83, Degrees, double>(new(0.0), new(0.0));                    // Geodetic point using NAD83 datum
var nad1927ConusPoint = new Point<NAD1927CONUS, Degrees, double>(new(0.0), new(0.0));      // Geodetic point using NAD1927CONUS datum

var doublePrecisionPoint = new Point<WGS84, Degrees, double>(new(0.0), new(0.0));          // Geodetic point with double precision
var singlePrecisionPoint = new Point<WGS84, Degrees, float>(new(0.0f), new(0.0f));           // Geodetic point with single precision

var minutesPoint = new Point<WGS84, Degrees, double>(Angle.ToDegrees(0, 0.0), Angle.ToDegrees(0, 0.0));               // Geodetic point using degrees and minutes
var minutesSecondsPoint = new Point<WGS84, Degrees, double>(Angle.ToDegrees(0, 0, 0.0), Angle.ToDegrees(0, 0, 0.0));  // Geodetic point using degrees, minutes and seconds

var (degreesLatitude, minutesLatitude) = Angle.ToDegreesMinutes<double, int, double>(wgs84Point.Latitude);                                // Convert latitude to degrees and minutes
var (degreesLatitude2, minutesLatitude2, secondsLatitude) = Angle.ToDegreesMinutesSeconds<double, int, int, double>(wgs84Point.Latitude); // Convert latitude to degrees, minutes, and seconds

    }
}
