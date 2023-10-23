namespace NetFabric.Numerics.Rectangular2D.UnitTests;

public class VectorIntTests
{
    public static TheoryData<Vector<int>, Vector<int>> Data => new()
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
    public void Equals_Should_Succeed(Vector<int> left, Vector<int> right)
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
    public void Add_Should_Succeed(Vector<int> left, Vector<int> right)
    {
        // arrange
        var expected = System.Numerics.Vector2.Add(
            new System.Numerics.Vector2(left.X, left.Y),
            new System.Numerics.Vector2(right.X, right.Y));

        // act
        var result = left + right;

        // assert
        result.GetType().Should().Be(typeof(Vector<int>));
        result.X.Should().Be((int)expected.X);
        result.Y.Should().Be((int)expected.Y);
    }
}