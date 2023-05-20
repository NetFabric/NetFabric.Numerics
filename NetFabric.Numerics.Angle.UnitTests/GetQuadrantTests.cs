
using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class GetQuadrantTests
{
    static readonly double AcuteDegrees = Angle<Degrees, double>.Right.Value / 4.0;

    public static TheoryData<AngleReduced<Degrees, double>, Quadrant> GetQuadrantDegreesData 
        => new()
        {
            {new AngleReduced<Degrees, double>(0.0), Quadrant.PositiveX},
            {new AngleReduced<Degrees, double>(45.0), Quadrant.First},
            {new AngleReduced<Degrees, double>(90.0), Quadrant.PositiveY},
            {new AngleReduced<Degrees, double>(135.0), Quadrant.Second},
            {new AngleReduced<Degrees, double>(180.0), Quadrant.NegativeX},
            {new AngleReduced<Degrees, double>(225.0), Quadrant.Third},
            {new AngleReduced<Degrees, double>(270.0), Quadrant.NegativeY},
            {new AngleReduced<Degrees, double>(315.0), Quadrant.Fourth},
        };

    [Theory]
    [MemberData(nameof(GetQuadrantDegreesData))]
    public void GetQuadrant_Degrees_Should_Succeed(AngleReduced<Degrees, double> angle, Quadrant expected)
    {
        // arrange

        // act
        var result = Angle.GetQuadrant(angle);

        // assert
        result.Should().Be(expected);
    }
}
