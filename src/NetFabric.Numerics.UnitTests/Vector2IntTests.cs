namespace NetFabric.Numerics.UnitTests;

public class Vector2IntTests
{
    public static TheoryData<Vector2<int>, Vector2<int>> Data => new()
    {
        {new(0, 0), new (0, 0)},
        {new(1, 0), new (0, 0)},
        {new(0, 1), new (0, 0)},
        {new(0, 0), new (1, 0)},
        {new(0, 0), new (0, 1)},
        {new(1, 1), new (1, 1)},
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void Equals_Should_Succeed(Vector2<int> left, Vector2<int> right)
    {
        // arrange
        var expected = new System.Numerics.Vector2(left.X, left.Y).Equals(new System.Numerics.Vector2(right.X, right.Y));

        // act
        var result = left.Equals(right);

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void Add_Should_Succeed(Vector2<int> left, Vector2<int> right)
    {
        // arrange
        var expected = System.Numerics.Vector2.Add(
            new System.Numerics.Vector2(left.X, left.Y),
            new System.Numerics.Vector2(right.X, right.Y));

        // act
        var result = left + right;

        // assert
        result.GetType().Should().Be(typeof(Vector2<int>));
        result.X.Should().Be((int)expected.X);
        result.Y.Should().Be((int)expected.Y);
    }

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