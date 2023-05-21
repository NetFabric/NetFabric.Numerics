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
    public void GetReference_Degrees_Should_Succeed(AngleReduced<Degrees, double> angle, AngleReduced<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Angle.GetReference(angle);

        // assert
        result.Should().Be(expected);
    }
}
