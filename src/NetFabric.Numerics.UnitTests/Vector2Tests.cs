namespace NetFabric.Numerics.UnitTests;

public class Vector2Tests
{
    public static TheoryData<Vector2<int>, Vector2<int>, Angle<Radians, double>> AngleData => new()
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
    public void Angle_Should_Succeed(Vector2<int> from, Vector2<int> to, Angle<Radians, double> expected)
    {
        // arrange

        // act
        var result = Vector2.AngleBetween<int, double>(from, to);
        //var resultSigned = Vector2.AngleSignedBetween<int, double>(from, to);

        // assert
        result.Value.Should().BeApproximately(double.Abs(expected.Value), 0.00001);
        //resultSigned.Value.Should().BeApproximately(expected.Value, 0.00001);
    }
}