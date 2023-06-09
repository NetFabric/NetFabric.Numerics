﻿namespace NetFabric.Numerics.UnitTests;

public class LerpTests
{
    static readonly Angle<Degrees, double> AcuteDegrees = Angle<Degrees, double>.Right / 2.0;

    public static TheoryData<Angle<Degrees, double>, Angle<Degrees, double>, float, Angle<Degrees, double>> LerpData => new()
    {
            {AcuteDegrees, Angle<Degrees, double>.Right + AcuteDegrees, -0.5f, Angle<Degrees, double>.Zero},
            {AcuteDegrees, Angle<Degrees, double>.Right + AcuteDegrees, 0.0f, AcuteDegrees},
            {AcuteDegrees, Angle<Degrees, double>.Right + AcuteDegrees, 0.5f, Angle<Degrees, double>.Right},
            {AcuteDegrees, Angle<Degrees, double>.Right + AcuteDegrees, 1.0f, Angle<Degrees, double>.Right + AcuteDegrees},
            {AcuteDegrees, Angle<Degrees, double>.Right + AcuteDegrees, 1.5f, Angle<Degrees, double>.Straight},

            {-AcuteDegrees, -Angle<Degrees, double>.Right - AcuteDegrees, -0.5f, Angle<Degrees, double>.Zero},
            {-AcuteDegrees, -Angle<Degrees, double>.Right - AcuteDegrees, 0.0f, -AcuteDegrees},
            {-AcuteDegrees, -Angle<Degrees, double>.Right - AcuteDegrees, 0.5f, -Angle<Degrees, double>.Right},
            {-AcuteDegrees, -Angle<Degrees, double>.Right - AcuteDegrees, 1.0f, -Angle<Degrees, double>.Right - AcuteDegrees},
            {-AcuteDegrees, -Angle<Degrees, double>.Right - AcuteDegrees, 1.5f, -Angle<Degrees, double>.Straight},

            {Angle<Degrees, double>.Right + AcuteDegrees, AcuteDegrees, -0.5f, Angle<Degrees, double>.Straight},
            {Angle<Degrees, double>.Right + AcuteDegrees, AcuteDegrees, 0.0f, Angle<Degrees, double>.Right + AcuteDegrees},
            {Angle<Degrees, double>.Right + AcuteDegrees, AcuteDegrees, 0.5f, Angle<Degrees, double>.Right},
            {Angle<Degrees, double>.Right + AcuteDegrees, AcuteDegrees, 1.0f, AcuteDegrees},
            {Angle<Degrees, double>.Right + AcuteDegrees, AcuteDegrees, 1.5f, Angle<Degrees, double>.Zero},
        };

    [Theory]
    [MemberData(nameof(LerpData))]
    public void Lerp_Should_Succeed(Angle<Degrees, double> a1, Angle<Degrees, double> a2, float t, Angle<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Angle.Lerp(a1, a2, t);

        // assert
        result.Value.Should().BeApproximately(expected.Value, 0.00001);
    }
}
