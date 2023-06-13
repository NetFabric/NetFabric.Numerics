namespace NetFabric.Numerics.Cartesian2.UnitTests;

public class VectorTests
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

    public static TheoryData<Vector<int>, Vector<int>, Angle<Radians, double>> AngleData => new()
    {
        {new(1, 0), new (1, 1), Angle<Radians, double>.Right / 2.0},
        {new(1, 0), new (0, 1), Angle<Radians, double>.Right},
        {new(0, 1), new (-1, 0), Angle<Radians, double>.Right},
        {new(1, 0), new (-1, 0), -Angle<Radians, double>.Straight},
        {new(1, 1), new (0, 1), Angle<Radians, double>.Right / 2.0},
        {new(1, 0), new (1, -1), -Angle<Radians, double>.Right / 2.0},
        {new(1, 0), new (0, -1), Angle<Radians, double>.Right},
    };

    [Theory]
    [MemberData(nameof(AngleData))]
    public void Angle_Should_Succeed(Vector<int> from, Vector<int> to, Angle<Radians, double> expected)
    {
        // arrange

        // act
        var result = Vector.Angle<int, double>(from, to);
        var resultSigned = Vector.AngleSigned<int, double>(from, to);

        // assert
        result.Value.Should().BeApproximately(double.Abs(expected.Value), 0.00001);
        //resultSigned.Value.Should().BeApproximately(expected.Value, 0.00001);
    }
}