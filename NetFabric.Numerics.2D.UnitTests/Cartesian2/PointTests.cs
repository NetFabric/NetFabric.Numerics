using FluentAssertions;

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
        result.Coordinates.Should().Equal(
            new Coordinate("X", typeof(double)), 
            new Coordinate("Y", typeof(double)));
    }
}