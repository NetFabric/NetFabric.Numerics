namespace NetFabric.Numerics.UnitTests;

public class ReduceTests
{
    static readonly AngleReduced<Degrees, double> AcuteDegrees = new(Angle<Degrees, double>.Right.Value / 4.0);

    public static TheoryData<Angle<Degrees, double>, AngleReduced<Degrees, double>> ReduceData => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Degrees, double>.Zero },
            { Angle<Degrees, double>.Right, Angle<Degrees, double>.Right },
            { Angle<Degrees, double>.Straight, Angle<Degrees, double>.Straight },

            { Angle<Degrees, double>.Zero + Angle<Degrees, double>.Full, Angle<Degrees, double>.Zero },
            { Angle<Degrees, double>.Right + Angle<Degrees, double>.Full, Angle<Degrees, double>.Right },
            { Angle<Degrees, double>.Straight + Angle<Degrees, double>.Full, Angle<Degrees, double>.Straight },

            { AcuteDegrees + Angle<Degrees, double>.Full, AcuteDegrees },
            { AcuteDegrees - Angle<Degrees, double>.Full, AcuteDegrees },
            { -AcuteDegrees + Angle<Degrees, double>.Full, new(Angle<Degrees, double>.Full.Value - AcuteDegrees.Value) },

            { AcuteDegrees + (2 * Angle<Degrees, double>.Full), AcuteDegrees },
            { AcuteDegrees - (2 * Angle<Degrees, double>.Full), AcuteDegrees },
            { -AcuteDegrees + (2 * Angle<Degrees, double>.Full), new(Angle<Degrees, double>.Full.Value - AcuteDegrees.Value) },
        };

    [Theory]
    [MemberData(nameof(ReduceData))]
    public void Reduce_Should_Succeed(Angle<Degrees, double> angle, AngleReduced<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Angle.Reduce(angle);

        // assert
        result.Should().Be(expected);
    }
}
