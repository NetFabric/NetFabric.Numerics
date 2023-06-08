using NetFabric.Numerics.Cartesian3;

namespace NetFabric.Numerics.UnitsTests.Cartesian3;

public class QuaternionLerpTests
{
    static readonly Quaternion<float> start = Quaternion.Normalize(new Quaternion<float>(1.0f, 2.0f, 3.0f, 4.0f));
    static readonly Quaternion<float> end = Quaternion.Normalize(new Quaternion<float>(4.0f, 3.0f, 2.0f, 1.0f));

    public static TheoryData<Quaternion<float>, Quaternion<float>, float, Quaternion<float>> Data
        => new()
        {
            {start, end, 0.0f, start},
            {start, end, 1.0f, end},
        };

    [Theory]
    [MemberData(nameof(Data))]
    public void Lerp_Should_Succeed(Quaternion<float> start, Quaternion<float> end, float factor, Quaternion<float> expected)
    {
        // arrange

        // act
        var result = Quaternion.Lerp(start, end, factor);

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void LerpShortestPath_Should_Succeed(Quaternion<float> start, Quaternion<float> end, float factor, Quaternion<float> expected)
    {
        // arrange

        // act
        var result = Quaternion.LerpShortestPath(start, end, factor);

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void Slerp_Should_Succeed(Quaternion<float> start, Quaternion<float> end, float factor, Quaternion<float> expected)
    {
        // arrange

        // act
        var result = Quaternion.Slerp(start, end, factor);

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void SlerpShortestPath_Should_Succeed(Quaternion<float> start, Quaternion<float> end, float factor, Quaternion<float> expected)
    {
        // arrange

        // act
        var result = Quaternion.SlerpShortestPath(start, end, factor);

        // assert
        result.Should().Be(expected);
    }

}
