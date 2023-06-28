namespace NetFabric.Numerics.UnitTests;

public class Vector2DoubleTests
{
    public static TheoryData<Vector2<double>, Vector2<double>> Data => new()
    {
        {new(0.0, 0.0), new (0.0, 0.0)},
        {new(1.0, 0.0), new (0.0, 0.0)},
        {new(0.0, 1.0), new (0.0, 0.0)},
        {new(0.0, 0.0), new (1.0, 0.0)},
        {new(0.0, 0.0), new (0.0, 1.0)},
        {new(1.0, 1.0), new (1.0, 1.0)},
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void Equals_Should_Succeed(Vector2<double> left, Vector2<double> right)
    {
        // arrange
        var expected = new System.Numerics.Vector2((float)left.X, (float)left.Y)
            .Equals(new System.Numerics.Vector2((float)right.X, (float)right.Y));

        // act
        var result = left.Equals(right);

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void Add_Should_Succeed(Vector2<double> left, Vector2<double> right)
    {
        // arrange
        var expected = System.Numerics.Vector2.Add(
            new System.Numerics.Vector2((float)left.X, (float)left.Y), 
            new System.Numerics.Vector2((float)right.X, (float)right.Y));

        // act
        var result = left + right;

        // assert
        result.GetType().Should().Be(typeof(Vector2<double>));
        result.X.Should().Be(expected.X);
        result.Y.Should().Be(expected.Y);
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