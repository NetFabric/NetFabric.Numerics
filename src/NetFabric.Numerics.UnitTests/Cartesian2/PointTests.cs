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
}