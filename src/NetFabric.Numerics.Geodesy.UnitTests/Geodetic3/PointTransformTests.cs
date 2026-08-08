namespace NetFabric.Numerics.Geodesy.Geodetic3.UnitTests;

public class PointTransformTests
{
    [Theory]
    [InlineData(38.8895, -77.0352, 10.0, 1115266.078149, -4844324.358206, 3982782.539890)]
    public void ToEcef_Should_Match_Known_Wgs84_Reference(
        double latitude,
        double longitude,
        double height,
        double expectedX,
        double expectedY,
        double expectedZ)
    {
        // arrange
        var source = new Point<WGS84<double>, Degrees, double>(new(latitude), new(longitude), height);

        // act
        var result = Point.ToEcef(source);

        // assert
        result.X.Should().BeApproximately(expectedX, 0.01);
        result.Y.Should().BeApproximately(expectedY, 0.01);
        result.Z.Should().BeApproximately(expectedZ, 0.01);
    }

    [Theory]
    [InlineData(38.8895, -77.0352, 10.0)]
    [InlineData(-33.8688, 151.2093, 58.0)]
    public void ToEcef_FromEcef_RoundTrip_Should_Return_Original(
        double latitude,
        double longitude,
        double height)
    {
        // arrange
        var source = new Point<WGS84<double>, Degrees, double>(new(latitude), new(longitude), height);

        // act
        var ecef = Point.ToEcef(source);
        var result = Point.FromEcef<WGS84<double>, Degrees, double>(ecef.X, ecef.Y, ecef.Z);

        // assert
        result.Latitude.Value.Should().BeApproximately(latitude, 0.00000001);
        result.Longitude.Value.Should().BeApproximately(longitude, 0.00000001);
        result.Height.Should().BeApproximately(height, 0.0001);
    }

    [Theory]
    // EPSG-consistent direction for NAD1927CONUS.Offset = (-8, +160, +176):
    // NAD27 -> WGS84 is the published forward transform, so WGS84 -> NAD27 uses the inverse.
    // Independently derived reference for this test point:
    // latitude=38.889467094408, longitude=-77.035523871211, height=46.447388255969 m
    [InlineData(38.8895, -77.0352, 10.0, 38.889467094408, -77.035523871211, 46.447388255969)]
    public void TransformDatum_Wgs84_To_Nad1927Conus_Should_Match_Known_Reference(
        double latitude,
        double longitude,
        double height,
        double expectedLatitude,
        double expectedLongitude,
        double expectedHeight)
    {
        // arrange
        var source = new Point<WGS84<double>, Degrees, double>(new(latitude), new(longitude), height);

        // act
        var result = Point.TransformDatum<WGS84<double>, NAD1927CONUS<double>, Degrees, double>(source);

        // assert
        result.Latitude.Value.Should().BeApproximately(expectedLatitude, 0.00002);
        result.Longitude.Value.Should().BeApproximately(expectedLongitude, 0.00002);
        result.Height.Should().BeApproximately(expectedHeight, 1.0);
    }

    [Fact]
    public void TransformDatum_Should_Apply_CoordinateFrame_Rotation_And_Scale_Convention()
    {
        // arrange
        var source = new Point<WGS84<double>, Degrees, double>(new(12.3), new(45.6), 789.0);
        var sourceEcef = Point.ToEcef(source);

        var expectedEcef = ApplyCoordinateFrameHelmert(sourceEcef, SyntheticHelmertDatum.Offset, inverse: true);

        // act
        var transformed = Point.TransformDatum<WGS84<double>, SyntheticHelmertDatum, Degrees, double>(source);
        var transformedEcef = Point.ToEcef(transformed);

        // assert
        transformedEcef.X.Should().BeApproximately(expectedEcef.X, 0.001);
        transformedEcef.Y.Should().BeApproximately(expectedEcef.Y, 0.001);
        transformedEcef.Z.Should().BeApproximately(expectedEcef.Z, 0.001);
    }

    [Fact]
    public void FromEcef_On_Antimeridian_With_Negative_Zero_Y_Should_Normalize_Longitude_To_Positive_180()
    {
        // arrange
        Point<WGS84<double>, Degrees, double> result = default;
        var act = () => result = Point.FromEcef<WGS84<double>, Degrees, double>(-6378137.0, -0.0, 0.0);

        // act/assert
        act.Should().NotThrow();
        result.Longitude.Value.Should().Be(180.0);
    }

    [Theory]
    [InlineData(38.8895, -77.0352, 10.0)]
    [InlineData(-33.8688, 151.2093, 58.0)]
    public void TransformDatum_Wgs84_To_Nad1927Conus_To_Wgs84_Should_RoundTrip(
        double latitude,
        double longitude,
        double height)
    {
        // arrange
        var source = new Point<WGS84<double>, Degrees, double>(new(latitude), new(longitude), height);

        // act
        var transformed = Point.TransformDatum<WGS84<double>, NAD1927CONUS<double>, Degrees, double>(source);
        var roundTrip = Point.TransformDatum<NAD1927CONUS<double>, WGS84<double>, Degrees, double>(transformed);

        // assert
        roundTrip.Latitude.Value.Should().BeApproximately(latitude, 0.0000001);
        roundTrip.Longitude.Value.Should().BeApproximately(longitude, 0.0000001);
        roundTrip.Height.Should().BeApproximately(height, 0.001);
    }

    static Rectangular3D.Point<double> ApplyCoordinateFrameHelmert(Rectangular3D.Point<double> source, Offset<double> offset, bool inverse)
    {
        var sign = inverse ? -1.0 : 1.0;
        var arcSecToRad = Math.PI / (180.0 * 3600.0);
        var rx = sign * offset.RX * arcSecToRad;
        var ry = sign * offset.RY * arcSecToRad;
        var rz = sign * offset.RZ * arcSecToRad;
        var s = 1.0 + sign * offset.SC * 1e-6;
        var dx = sign * offset.XYZOffset.X;
        var dy = sign * offset.XYZOffset.Y;
        var dz = sign * offset.XYZOffset.Z;

        // Coordinate-frame convention (as documented in SKILL.md)
        var x = dx + s * (source.X + rz * source.Y - ry * source.Z);
        var y = dy + s * (-rz * source.X + source.Y + rx * source.Z);
        var z = dz + s * (ry * source.X - rx * source.Y + source.Z);

        return new(x, y, z);
    }

    private abstract class SyntheticHelmertDatum
        : IDatum<double>
    {
        public static string Name => "Synthetic test datum";

        public static Offset<double> Offset => new(
            new(1.0, -2.0, 3.0),
            0.2,   // RX arc-seconds
            -0.3,  // RY arc-seconds
            1.5,   // RZ arc-seconds
            2.0);  // scale ppm

        public static Ellipsoid<double> Ellipsoid => Ellipsoid<double>.WGS1984;
    }
}