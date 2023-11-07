using FluentAssertions;

namespace NetFabric.Numerics.Rectangular3D.UnitTests;

public class QuaternionTests
{
    [Fact]
    public void Constructor_Should_Succeed()
    {
        // arrange
        var x = 1.0;
        var y = 2.0;
        var z = 3.0;
        var w = 4.0;

        // act
        var result = new Quaternion<double>(x, y, z, w);

        // assert
        result.X.Should().Be(x);
        result.Y.Should().Be(y);
        result.Z.Should().Be(z);
        result.W.Should().Be(w);
    }

    public static TheoryData<Quaternion<float>> UnaryData
        => new()
        {
            {Quaternion<float>.Zero},
            {Quaternion<float>.Identity},
            {new(1.0f, 2.0f, 3.0f, 4.0f)},
            {new(-1.0f, -2.0f, -3.0f, -4.0f)},
        };

    public static TheoryData<Quaternion<float>, Quaternion<float>> DualData
        => new()
        {
            {Quaternion<float>.Zero, new(1.0f, 2.0f, 3.0f, 4.0f) },
            {Quaternion<float>.Identity, new(1.0f, 2.0f, 3.0f, 4.0f) },
            {new(1.0f, 2.0f, 3.0f, 4.0f), new(10.0f, 20.0f, 30.0f, 40.0f)},
            {new(-1.0f, -2.0f, -3.0f, -4.0f), new(10.0f, 20.0f, 30.0f, 40.0f)},
            {new(-1.0f, -2.0f, -3.0f, -4.0f), new(-10.0f, -20.0f, -30.0f, -40.0f)},
        };

    [Theory]
    [MemberData(nameof(UnaryData))]
    public void Plus_Should_Succeed(Quaternion<float> right)
    {
        // arrange

        // act
        var result = +right;

        // assert
        result.Should().Be(right);
    }

    [Theory]
    [MemberData(nameof(DualData))]
    public void Addition_Should_Succeed(Quaternion<float> left, Quaternion<float> right)
    {
        // arrange
        var expected = System.Numerics.Quaternion.Add(new System.Numerics.Quaternion(left.X, left.Y, left.Z, left.W), new System.Numerics.Quaternion(right.X, right.Y, right.Z, right.W));

        // act
        var result = left + right;

        // assert
        result.X.Should().Be(expected.X);
        result.Y.Should().Be(expected.Y);
        result.Z.Should().Be(expected.Z);
        result.W.Should().Be(expected.W);
    }

    [Theory]
    [MemberData(nameof(UnaryData))]
    public void Negate_Should_Succeed(Quaternion<float> right)
    {
        // arrange
        var expected = System.Numerics.Quaternion.Negate(new System.Numerics.Quaternion(right.X, right.Y, right.Z, right.W));

        // act
        var result = -right;

        // assert
        result.X.Should().Be(expected.X);
        result.Y.Should().Be(expected.Y);
        result.Z.Should().Be(expected.Z);
        result.W.Should().Be(expected.W);
    }

    [Theory]
    [MemberData(nameof(DualData))]
    public void Subtraction_Should_Succeed(Quaternion<float> left, Quaternion<float> right)
    {
        // arrange
        var expected = System.Numerics.Quaternion.Subtract(new System.Numerics.Quaternion(left.X, left.Y, left.Z, left.W), new System.Numerics.Quaternion(right.X, right.Y, right.Z, right.W));

        // act
        var result = left - right;

        // assert
        result.X.Should().Be(expected.X);
        result.Y.Should().Be(expected.Y);
        result.Z.Should().Be(expected.Z);
        result.W.Should().Be(expected.W);
    }

    [Theory]
    [MemberData(nameof(DualData))]
    public void Multiply_Should_Succeed(Quaternion<float> left, Quaternion<float> right)
    {
        // arrange
        var expected = System.Numerics.Quaternion.Multiply(new System.Numerics.Quaternion(left.X, left.Y, left.Z, left.W), new System.Numerics.Quaternion(right.X, right.Y, right.Z, right.W));

        // act
        var result = left * right;

        // assert
        result.X.Should().Be(expected.X);
        result.Y.Should().Be(expected.Y);
        result.Z.Should().Be(expected.Z);
        result.W.Should().Be(expected.W);
    }

    public static TheoryData<Quaternion<float>, float> MultiplyScalarOperatorData
        => new()
        {
            {Quaternion<float>.Zero, 10.0f },
            {Quaternion<float>.Identity, 10.0f },
            {new(1.0f, 2.0f, 3.0f, 4.0f), 10.0f},
            {new(1.0f, 2.0f, 3.0f, 4.0f), -10.0f},
        };

    [Theory]
    [MemberData(nameof(MultiplyScalarOperatorData))]
    public void Multiply_Scalar_Should_Succeed(Quaternion<float> left, float right)
    {
        // arrange
        var expected = System.Numerics.Quaternion.Multiply(new System.Numerics.Quaternion(left.X, left.Y, left.Z, left.W), right);
        // act
        var result = left * right;

        // assert
        result.X.Should().Be(expected.X);
        result.Y.Should().Be(expected.Y);
        result.Z.Should().Be(expected.Z);
        result.W.Should().Be(expected.W);
    }

    [Theory]
    [MemberData(nameof(DualData))]
    public void Divide_Should_Succeed(Quaternion<float> left, Quaternion<float> right)
    {
        // arrange
        var expected = System.Numerics.Quaternion.Divide(new System.Numerics.Quaternion(left.X, left.Y, left.Z, left.W), new System.Numerics.Quaternion(right.X, right.Y, right.Z, right.W));

        // act
        var result = left / right;

        // assert
        result.X.Should().Be(expected.X);
        result.Y.Should().Be(expected.Y);
        result.Z.Should().Be(expected.Z);
        result.W.Should().Be(expected.W);
    }

    [Fact]
    public void Sum_Zero_Should_Return_Same()
    {
        // arrange
        var value = new Quaternion<float>(1.0f, 2.0f, 3.0f, 4.0f);

        // act
        var result = Quaternion<float>.Zero + value;

        // assert
        result.Should().Be(value);
    }

    [Fact]
    public void Multiply_Identity_Should_Return_Same()
    {
        // arrange
        var value = new Quaternion<float>(1.0f, 2.0f, 3.0f, 4.0f);

        // act
        var result = Quaternion<float>.Identity * value;

        // assert
        result.Should().Be(value);
    }

    public static TheoryData<Vector<float>, Angle<Radians, float>> FromAxisAngleData
        => new()
        {
                {Vector<float>.UnitX, Angle<Radians, float>.Zero},
                {Vector<float>.UnitX, Angle<Radians, float>.Right},
                {Vector<float>.UnitX, -Angle<Radians, float>.Right},

                {Vector<float>.UnitY, Angle<Radians, float>.Zero},
                {Vector<float>.UnitY, Angle<Radians, float>.Right},
                {Vector<float>.UnitY, -Angle<Radians, float>.Right},

                {Vector<float>.UnitZ, Angle<Radians, float>.Zero},
                {Vector<float>.UnitZ, Angle<Radians, float>.Right},
                {Vector<float>.UnitZ, -Angle<Radians, float>.Right},
        };

    [Theory]
    [MemberData(nameof(FromAxisAngleData))]
    public void FromAxisAngle_Should_Succeed(Vector<float> axis, Angle<Radians, float> angle)
    {
        // arrange
        var expected = System.Numerics.Quaternion.CreateFromAxisAngle(new System.Numerics.Vector3(axis.X, axis.Y, axis.Z), angle.Value);
        // act
        var result = Quaternion.FromAxisAngle(in axis, angle);

        // assert
        result.X.Should().Be(expected.X);
        result.Y.Should().Be(expected.Y);
        result.Z.Should().Be(expected.Z);
        result.W.Should().Be(expected.W);
    }

    //public static TheoryData<Angle<Radians, float>, Angle<Radians, float>, Angle<Radians, float>> FromYawPitchRollData
    //    => new()
    //    {
    //        {Angle<Radians, float>.Zero, Angle<Radians, float>.Zero, Angle<Radians, float>.Zero},
    //        {Angle<Radians, float>.Right, Angle<Radians, float>.Zero, Angle<Radians, float>.Zero},
    //        {Angle<Radians, float>.Zero, Angle<Radians, float>.Right, Angle<Radians, float>.Zero},
    //        {Angle<Radians, float>.Zero, Angle<Radians, float>.Zero, Angle<Radians, float>.Right},
    //        {Angle<Radians, float>.Right, Angle<Radians, float>.Zero, Angle<Radians, float>.Right},
    //    };

    //[Theory]
    //[MemberData(nameof(FromYawPitchRollData))]
    //public void FromYawPitchRoll_Should_Succeed(Angle<Radians, float> yaw, Angle<Radians, float> pitch, Angle<Radians, float> roll)
    //{
    //    // arrange
    //    var expected = System.Numerics.Quaternion.CreateFromYawPitchRoll(yaw.Value, pitch.Value, roll.Value);
    //    // act
    //    var result = Quaternion.FromYawPitchRoll(yaw, pitch, roll);

    //    // assert
    //    result.X.Should().Be(expected.X);
    //    result.Y.Should().Be(expected.X);
    //    result.Z.Should().Be(expected.X);
    //    result.W.Should().Be(expected.X);
    //}

    [Theory]
    [MemberData(nameof(UnaryData))]
    public void Normalize_Should_Succeed(Quaternion<float> quaternion)
    {
        // arrange
        if(quaternion == Quaternion<float>.Zero)
            return;
        var expected = System.Numerics.Quaternion.Normalize(new System.Numerics.Quaternion(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W));

        // act
        var result = Quaternion.Normalize(in quaternion);

        // assert
        result.X.Should().BeApproximately(expected.X, 1e-7f);
        result.Y.Should().BeApproximately(expected.Y, 1e-7f);
        result.Z.Should().BeApproximately(expected.Z, 1e-7f);
        result.W.Should().BeApproximately(expected.W, 1e-7f);
    }

    [Theory]
    [MemberData(nameof(DualData))]
    public void DotProduct_Should_Succeed(Quaternion<float> left, Quaternion<float> right)
    {
        // arrange
        var expected = System.Numerics.Quaternion.Dot(new System.Numerics.Quaternion(left.X, left.Y, left.Z, left.W), new System.Numerics.Quaternion(right.X, right.Y, right.Z, right.W));

        // act
        var result = Quaternion.DotProduct(in left, in right);

        // assert
        result.Should().Be(expected);
    }

}
