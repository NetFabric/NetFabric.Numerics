namespace NetFabric.Numerics.Rectangular3D.UnitTests;

public class VectorTests
{
    public static TheoryData<Vector<float>, Vector<float>> Data => new()
    {
        {new(0.0f, 0.0f, 0.0f), new (0.0f, 0.0f, 0.0f)},
        {new(1.0f, 0.0f, 0.0f), new (0.0f, 0.0f, 0.0f)},
        {new(0.0f, 1.0f, 0.0f), new (0.0f, 0.0f, 0.0f)},
        {new(0.0f, 0.0f, 1.0f), new (0.0f, 0.0f, 0.0f)},
        {new(0.0f, 0.0f, 0.0f), new (1.0f, 0.0f, 0.0f)},
        {new(0.0f, 0.0f, 0.0f), new (0.0f, 1.0f, 0.0f)},
        {new(0.0f, 0.0f, 0.0f), new (0.0f, 0.0f, 1.0f)},
        {new(1.0f, 1.0f, 1.0f), new (1.0f, 1.0f, 1.0f)},
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void Equals_Should_Succeed(Vector<float> left, Vector<float> right)
    {
        // arrange
        var expected = new System.Numerics.Vector3(left.X, left.Y, left.Z)
            .Equals(new System.Numerics.Vector3(right.X, right.Y, right.Z));

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
        var expected = System.Numerics.Vector3.Add(
            new System.Numerics.Vector3(left.X, left.Y, left.Z),
            new System.Numerics.Vector3(right.X, right.Y, right.Z));

        // act
        var result = left + right;

        // assert
        result.GetType().Should().Be<Vector<float>>();
        result.X.Should().Be(expected.X);
        result.Y.Should().Be(expected.Y);
        result.Z.Should().Be(expected.Z);
    }
}