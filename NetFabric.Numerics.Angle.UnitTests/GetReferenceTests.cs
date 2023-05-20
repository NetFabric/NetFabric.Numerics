
using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class GetReferenceTests
{
    public static TheoryData<AngleReduced<Degrees, double>, AngleReduced<Degrees, double>> GetReferenceDegreesData
        => new()
        {
            {new AngleReduced<Degrees, double>(0.0), new AngleReduced<Degrees, double>(0.0)},
            {new AngleReduced<Degrees, double>(45.0), new AngleReduced<Degrees, double>(45.0)},
            {new AngleReduced<Degrees, double>(90.0), new AngleReduced<Degrees, double>(90.0)},
            {new AngleReduced<Degrees, double>(135.0), new AngleReduced<Degrees, double>(45.0)},
            {new AngleReduced<Degrees, double>(180.0), new AngleReduced<Degrees, double>(0.0)},
            {new AngleReduced<Degrees, double>(225.0), new AngleReduced<Degrees, double>(45.0)},
            {new AngleReduced<Degrees, double>(270.0), new AngleReduced<Degrees, double>(90.0)},
            {new AngleReduced<Degrees, double>(315.0), new AngleReduced<Degrees, double>(45.0)},
        };

    [Theory]
    [MemberData(nameof(GetReferenceDegreesData))]
    public void GetReference_Degrees_Should_Succeed(Angle<Degrees, double> angle, Angle<Degrees, double> expected)
    {
        // arrange
        var reduced = Angle.Reduce(angle);

        // act
        var result = Angle.GetReference(reduced);

        // assert
        result.Should().Be(expected);
    }
}
