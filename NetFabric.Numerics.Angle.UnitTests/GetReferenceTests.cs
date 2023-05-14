
using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class GetReferenceTests
{
    static readonly Angle<Degrees, double> AcuteDegrees = Angle<Degrees, double>.Right / 4.0;

    public static TheoryData<Angle<Degrees, double>, Angle<Degrees, double>> GetReferenceDegreesData => new()
        {
            {Angle<Degrees, double>.Zero, Angle<Degrees, double>.Zero},
            {Angle<Degrees, double>.Zero + AcuteDegrees, AcuteDegrees},
            {Angle<Degrees, double>.Right - AcuteDegrees, Angle<Degrees, double>.Right - AcuteDegrees},
            {Angle<Degrees, double>.Right, Angle<Degrees, double>.Right},
            {Angle<Degrees, double>.Right + AcuteDegrees, Angle<Degrees, double>.Right - AcuteDegrees},
            {Angle<Degrees, double>.Straight - AcuteDegrees, AcuteDegrees},
            {Angle<Degrees, double>.Straight, Angle<Degrees, double>.Zero},
            {Angle<Degrees, double>.Straight + AcuteDegrees, AcuteDegrees},
            {Angle<Degrees, double>.Straight + Angle<Degrees, double>.Right - AcuteDegrees, Angle<Degrees, double>.Right - AcuteDegrees},
            {Angle<Degrees, double>.Straight + Angle<Degrees, double>.Right, Angle<Degrees, double>.Right},
            {Angle<Degrees, double>.Straight + Angle<Degrees, double>.Right + AcuteDegrees, Angle<Degrees, double>.Right - AcuteDegrees},
            {Angle<Degrees, double>.Full - AcuteDegrees, AcuteDegrees},

            {Angle<Degrees, double>.Full, Angle<Degrees, double>.Zero},
            {Angle<Degrees, double>.Full + AcuteDegrees, AcuteDegrees},

            {-Angle<Degrees, double>.Full, Angle<Degrees, double>.Zero},
            {-Angle<Degrees, double>.Full + AcuteDegrees, AcuteDegrees},
        };

    [Theory]
    [MemberData(nameof(GetReferenceDegreesData))]
    public void GetReference_Degrees_Should_Succeed(Angle<Degrees, double> angle, Angle<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Angle.GetReference(angle);

        // assert
        result.Should().Be(expected);
    }
}
