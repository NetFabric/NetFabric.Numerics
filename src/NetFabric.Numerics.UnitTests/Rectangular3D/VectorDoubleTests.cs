namespace NetFabric.Numerics.Rectangular3D.UnitTests;

public class VectorDoubleTests
{
    public static TheoryData<Vector<double>, Vector<double>> Data => new()
    {
        {new(0.0, 0.0, 0.0), new (0.0, 0.0, 0.0)},
        {new(1.0, 0.0, 0.0), new (0.0, 0.0, 0.0)},
        {new(0.0, 1.0, 0.0), new (0.0, 0.0, 0.0)},
        {new(0.0, 0.0, 1.0), new (0.0, 0.0, 0.0)},
        {new(0.0, 0.0, 0.0), new (1.0, 0.0, 0.0)},
        {new(0.0, 0.0, 0.0), new (0.0, 1.0, 0.0)},
        {new(0.0, 0.0, 0.0), new (0.0, 0.0, 1.0)},
        {new(1.0, 1.0, 1.0), new (1.0, 1.0, 1.0)},
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void Equals_Should_Succeed(Vector<double> left, Vector<double> right)
    {
        // arrange
        var expected = new System.Numerics.Vector3((float)left.X, (float)left.Y, (float)left.Z)
            .Equals(new System.Numerics.Vector3((float)right.X, (float)right.Y, (float)right.Z));

        // act
        var result = left.Equals(right);

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void Add_Should_Succeed(Vector<double> left, Vector<double> right)
    {
        // arrange
        var expected = System.Numerics.Vector3.Add(
            new System.Numerics.Vector3((float)left.X, (float)left.Y, (float)left.Z),
            new System.Numerics.Vector3((float)right.X, (float)right.Y, (float)right.Z));

        // act
        var result = left + right;

        // assert
        result.GetType().Should().Be(typeof(Vector<double>));
        result.X.Should().Be(expected.X);
        result.Y.Should().Be(expected.Y);
        result.Z.Should().Be(expected.Z);
    }
}
