namespace NetFabric.Numerics.Geodesy.Geodetic3.UnitTests;

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
        result.Height.Should().Be(0.0);
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
            new Coordinate("Longitude", typeof(Angle<Degrees, double>)),
            new Coordinate("Height", typeof(double)));
    }

    [Fact]
    public void MinValue_Should_Succeed()
    {
        // arrange

        // act
        var result = Point<WGS84<double>, Degrees, double>.MinValue;

        // assert
        result.Latitude.Should().Be(-Angle<Degrees, double>.Right);
        result.Longitude.Value.Should().BeGreaterThan(-Angle<Degrees, double>.Straight.Value);
        result.Longitude.Value.Should().BeLessOrEqualTo(Angle<Degrees, double>.Straight.Value);
        result.Height.Should().Be(double.MinValue);
    }

    [Fact]
    public void MaxValue_Should_Succeed()
    {
        // arrange

        // act
        var result = Point<WGS84<double>, Degrees, double>.MaxValue;

        // assert
        result.Latitude.Should().Be(Angle<Degrees, double>.Right);
        result.Longitude.Should().Be(Angle<Degrees, double>.Straight);
        result.Height.Should().Be(double.MaxValue);
    }

    [Fact]
    public void MaxValue_Height_Should_Be_MaxValue()
    {
        // arrange

        // act
        var result = Point<WGS84<double>, Degrees, double>.MaxValue.Height;

        // assert
        result.Should().Be(double.MaxValue);
    }

    [Theory]
    [InlineData(-90.0)]
    [InlineData(0.0)]
    [InlineData(90.0)]
    public void Constructor_With_Valid_Latitude_Should_Succeed(double latitude)
    {
        // arrange

        // act
        var act = () => new Point<WGS84<double>, Degrees, double>(new(latitude), Angle<Degrees, double>.Zero, 0.0);

        // assert
        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(-90.000000001)]
    [InlineData(90.000000001)]
    public void Constructor_With_Invalid_Latitude_Should_ThrowArgumentOutOfRangeException(double latitude)
    {
        // arrange

        // act
        var act = () => new Point<WGS84<double>, Degrees, double>(new(latitude), Angle<Degrees, double>.Zero, 0.0);

        // assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(-179.999999)]
    [InlineData(0.0)]
    [InlineData(180.0)]
    public void Constructor_With_Valid_Longitude_Should_Succeed(double longitude)
    {
        // arrange

        // act
        var act = () => new Point<WGS84<double>, Degrees, double>(Angle<Degrees, double>.Zero, new(longitude), 0.0);

        // assert
        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(-180.0)]
    [InlineData(180.000000001)]
    public void Constructor_With_Invalid_Longitude_Should_ThrowArgumentOutOfRangeException(double longitude)
    {
        // arrange

        // act
        var act = () => new Point<WGS84<double>, Degrees, double>(Angle<Degrees, double>.Zero, new(longitude), 0.0);

        // assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void CreateChecked_Should_Succeed()
    {
        // arrange
        var source = new Point<WGS84<float>, Degrees, float>(new(10.5f), new(20.25f), 100.75f);

        // act
        var result = Point<WGS84<double>, Degrees, double>.CreateChecked(source);

        // assert
        result.Latitude.Value.Should().Be(10.5);
        result.Longitude.Value.Should().Be(20.25);
        result.Height.Should().Be(100.75);
    }

    [Fact]
    public void CreateSaturating_Should_Succeed()
    {
        // arrange
        var source = new Point<WGS84<float>, Degrees, float>(new(10.5f), new(20.25f), 100.75f);

        // act
        var result = Point<WGS84<double>, Degrees, double>.CreateSaturating(source);

        // assert
        result.Latitude.Value.Should().Be(10.5);
        result.Longitude.Value.Should().Be(20.25);
        result.Height.Should().Be(100.75);
    }

    [Fact]
    public void CreateTruncating_Should_Succeed()
    {
        // arrange
        var source = new Point<WGS84<float>, Degrees, float>(new(10.5f), new(20.25f), 100.75f);

        // act
        var result = Point<WGS84<double>, Degrees, double>.CreateTruncating(source);

        // assert
        result.Latitude.Value.Should().Be(10.5);
        result.Longitude.Value.Should().Be(20.25);
        result.Height.Should().Be(100.75);
    }
}
