namespace NetFabric.Numerics.Rectangular2D.UnitTests;

public class VectorTests
{
    public static TheoryData<Vector<float>, Vector<float>> Data => new()
    {
        {new(0.0f, 0.0f), new (0.0f, 0.0f)},
        {new(1.0f, 0.0f), new (0.0f, 0.0f)},
        {new(0.0f, 1.0f), new (0.0f, 0.0f)},
        {new(0.0f, 0.0f), new (1.0f, 0.0f)},
        {new(0.0f, 0.0f), new (0.0f, 1.0f)},
        {new(1.0f, 1.0f), new (1.0f, 1.0f)},
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void Equals_Should_Succeed(Vector<float> left, Vector<float> right)
    {
        // arrange
        var expected = new System.Numerics.Vector2(left.X, left.Y)
            .Equals(new System.Numerics.Vector2(right.X, right.Y));

        // act
        var result = left.Equals(right);

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void Add_Should_Succeed(Vector<float> left, Vector<float> right)
    {
        // arrange
        var expected = System.Numerics.Vector2.Add(
            new System.Numerics.Vector2(left.X, left.Y), 
            new System.Numerics.Vector2(right.X, right.Y));

        // act
        var result = left + right;

        // assert
        result.GetType().Should().Be(typeof(Vector<float>));
        result.X.Should().Be(expected.X);
        result.Y.Should().Be(expected.Y);
    }
}