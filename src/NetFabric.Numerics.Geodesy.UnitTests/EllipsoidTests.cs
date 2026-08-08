namespace NetFabric.Numerics.Geodesy.UnitTests;

public class EllipsoidTests
{
    [Fact]
    public void PolarRadius_Should_Match_Wgs84_Reference()
    {
        // arrange
        var ellipsoid = Ellipsoid<double>.WGS1984;

        // act
        var result = Ellipsoid.PolarRadius(in ellipsoid);

        // assert
        result.Should().BeApproximately(6_356_752.314_245, 1e-6);
    }

    [Fact]
    public void EccentricitySquared_Should_Match_Wgs84_Reference()
    {
        // arrange
        var ellipsoid = Ellipsoid<double>.WGS1984;

        // act
        var result = Ellipsoid.EccentricitySquared(in ellipsoid);

        // assert
        result.Should().BeApproximately(0.006_694_379_990_14, 1e-14);
    }

    [Fact]
    public void SecondEccentricitySquared_Should_Match_Wgs84_Reference()
    {
        // arrange
        var ellipsoid = Ellipsoid<double>.WGS1984;

        // act
        var result = Ellipsoid.SecondEccentricitySquared(in ellipsoid);

        // assert
        result.Should().BeApproximately(0.006_739_496_742_28, 1e-14);
    }

    [Fact]
    public void ArithmeticMeanRadius_Should_Match_Wgs84_Reference()
    {
        // arrange
        var ellipsoid = Ellipsoid<double>.WGS1984;

        // act
        var result = Ellipsoid.ArithmeticMeanRadius(in ellipsoid);

        // assert
        result.Should().BeApproximately(6_371_008.771, 1e-3);
    }

    [Theory]
    [InlineData(0.0, 6_335_439.327_292_819_5)]
    [InlineData(0.785_398_163_397_448_3, 6_367_381.815_619_548)]
    [InlineData(1.570_796_326_794_896_6, 6_399_593.625_758_492)]
    public void RadiusOfCurvatureInMeridian_Should_Match_Wgs84_Reference(double latitude, double expected)
    {
        // arrange
        var ellipsoid = Ellipsoid<double>.WGS1984;

        // act
        var result = Ellipsoid.RadiusOfCurvatureInMeridian(in ellipsoid, latitude);

        // assert
        result.Should().BeApproximately(expected, 1e-3);
    }

    [Theory]
    [InlineData(0.0, 6_378_137.0)]
    [InlineData(0.785_398_163_397_448_3, 6_388_838.290_121_148)]
    [InlineData(1.570_796_326_794_896_6, 6_399_593.625_758_493)]
    public void RadiusOfCurvatureInPrimeVertical_Should_Match_Wgs84_Reference(double latitude, double expected)
    {
        // arrange
        var ellipsoid = Ellipsoid<double>.WGS1984;

        // act
        var result = Ellipsoid.RadiusOfCurvatureInPrimeVertical(in ellipsoid, latitude);

        // assert
        result.Should().BeApproximately(expected, 1e-3);
    }

    [Fact]
    public void SurfaceArea_Should_Match_Wgs84_Reference()
    {
        // arrange
        var ellipsoid = Ellipsoid<double>.WGS1984;

        // act
        var result = Ellipsoid.SurfaceArea(in ellipsoid);

        // assert
        result.Should().BeApproximately(510_065_621_724_088.5, 1e-1);
    }

    [Fact]
    public void Volume_Should_Match_Wgs84_Reference()
    {
        // arrange
        var ellipsoid = Ellipsoid<double>.WGS1984;

        // act
        var result = Ellipsoid.Volume(in ellipsoid);

        // assert
        result.Should().BeApproximately(1.083_207_319_801_408_1e21, 1e6);
    }
}