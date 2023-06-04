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
        //result.Coordinates.Span[0].Should().Equal(new Coordinate("X", typeof(double)));
        //result.Coordinates.Span[1].Should().Equal(new Coordinate("Y", typeof(double)));
    }
}