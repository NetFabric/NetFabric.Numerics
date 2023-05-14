
using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class GetQuadrantTests
{
    static readonly Angle<Degrees, double> AcuteDegrees = Angle<Degrees, double>.Right / 4.0;

    public static TheoryData<Angle<Degrees, double>, Quadrant> GetQuadrantDegreesData => new()
        {
            {Angle<Degrees, double>.Zero, Quadrant.PositiveX},
            {Angle<Degrees, double>.Zero + AcuteDegrees, Quadrant.First},
            {Angle<Degrees, double>.Right - AcuteDegrees, Quadrant.First},
            {Angle<Degrees, double>.Right, Quadrant.PositiveY},
            {Angle<Degrees, double>.Right + AcuteDegrees, Quadrant.Second},
            {Angle<Degrees, double>.Straight - AcuteDegrees, Quadrant.Second},
            {Angle<Degrees, double>.Straight, Quadrant.NegativeX},
            {Angle<Degrees, double>.Straight + AcuteDegrees, Quadrant.Third},
            {Angle<Degrees, double>.Straight + Angle<Degrees, double>.Right - AcuteDegrees, Quadrant.Third},
            {Angle<Degrees, double>.Straight + Angle<Degrees, double>.Right, Quadrant.NegativeY},
            {Angle<Degrees, double>.Straight + Angle<Degrees, double>.Right + AcuteDegrees, Quadrant.Fourth},
            {Angle<Degrees, double>.Full - AcuteDegrees, Quadrant.Fourth},

            {Angle<Degrees, double>.Full, Quadrant.PositiveX},
            {Angle<Degrees, double>.Full + AcuteDegrees, Quadrant.First},

            {-Angle<Degrees, double>.Full, Quadrant.PositiveX},
            {-Angle<Degrees, double>.Full + AcuteDegrees, Quadrant.First},
            {-Angle<Degrees, double>.Full - AcuteDegrees, Quadrant.Fourth},
        };

    [Theory]
    [MemberData(nameof(GetQuadrantDegreesData))]
    public void GetQuadrant_Degrees_Should_Succeed(Angle<Degrees, double> angle, Quadrant expected)
    {
        // arrange

        // act
        var result = Angle.GetQuadrant(angle);

        // assert
        result.Should().Be(expected);
    }
}
