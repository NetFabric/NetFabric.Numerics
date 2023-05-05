
using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class LerpTests
{
    static readonly Degrees<double> AcuteDegrees = Degrees<double>.Right / 2.0;
    static readonly Radians<double> AcuteRadians = Radians<double>.Right / 2.0;
    static readonly Gradians<double> AcuteGradians = Gradians<double>.Right / 2.0;
    static readonly Revolutions<double> AcuteRevolutions = Revolutions<double>.Right / 2.0;

    public static TheoryData<Degrees<double>, Degrees<double>, float, Degrees<double>> LerpDegreesData => new()
    {
            {AcuteDegrees, Degrees<double>.Right + AcuteDegrees, -0.5f, Degrees<double>.Zero},
            {AcuteDegrees, Degrees<double>.Right + AcuteDegrees, 0.0f, AcuteDegrees},
            {AcuteDegrees, Degrees<double>.Right + AcuteDegrees, 0.5f, Degrees<double>.Right},
            {AcuteDegrees, Degrees<double>.Right + AcuteDegrees, 1.0f, Degrees<double>.Right + AcuteDegrees},
            {AcuteDegrees, Degrees<double>.Right + AcuteDegrees, 1.5f, Degrees<double>.Straight},

            {-AcuteDegrees, -Degrees<double>.Right - AcuteDegrees, -0.5f, Degrees<double>.Zero},
            {-AcuteDegrees, -Degrees<double>.Right - AcuteDegrees, 0.0f, -AcuteDegrees},
            {-AcuteDegrees, -Degrees<double>.Right - AcuteDegrees, 0.5f, -Degrees<double>.Right},
            {-AcuteDegrees, -Degrees<double>.Right - AcuteDegrees, 1.0f, -Degrees<double>.Right - AcuteDegrees},
            {-AcuteDegrees, -Degrees<double>.Right - AcuteDegrees, 1.5f, -Degrees<double>.Straight},

            {Degrees<double>.Right + AcuteDegrees, AcuteDegrees, -0.5f, Degrees<double>.Straight},
            {Degrees<double>.Right + AcuteDegrees, AcuteDegrees, 0.0f, Degrees<double>.Right + AcuteDegrees},
            {Degrees<double>.Right + AcuteDegrees, AcuteDegrees, 0.5f, Degrees<double>.Right},
            {Degrees<double>.Right + AcuteDegrees, AcuteDegrees, 1.0f, AcuteDegrees},
            {Degrees<double>.Right + AcuteDegrees, AcuteDegrees, 1.5f, Degrees<double>.Zero},
        };

    public static TheoryData<Radians<double>, Radians<double>, float, Radians<double>> LerpRadiansData => new()
    {
            {AcuteRadians, Radians<double>.Right + AcuteRadians, -0.5f, Radians<double>.Zero},
            {AcuteRadians, Radians<double>.Right + AcuteRadians, 0.0f, AcuteRadians},
            {AcuteRadians, Radians<double>.Right + AcuteRadians, 0.5f, Radians<double>.Right},
            {AcuteRadians, Radians<double>.Right + AcuteRadians, 1.0f, Radians<double>.Right + AcuteRadians},
            {AcuteRadians, Radians<double>.Right + AcuteRadians, 1.5f, Radians<double>.Straight},

            {-AcuteRadians, -Radians<double>.Right - AcuteRadians, -0.5f, Radians<double>.Zero},
            {-AcuteRadians, -Radians<double>.Right - AcuteRadians, 0.0f, -AcuteRadians},
            {-AcuteRadians, -Radians<double>.Right - AcuteRadians, 0.5f, -Radians<double>.Right},
            {-AcuteRadians, -Radians<double>.Right - AcuteRadians, 1.0f, -Radians<double>.Right - AcuteRadians},
            {-AcuteRadians, -Radians<double>.Right - AcuteRadians, 1.5f, -Radians<double>.Straight},

            {Radians<double>.Right + AcuteRadians, AcuteRadians, -0.5f, Radians<double>.Straight},
            {Radians<double>.Right + AcuteRadians, AcuteRadians, 0.0f, Radians<double>.Right + AcuteRadians},
            {Radians<double>.Right + AcuteRadians, AcuteRadians, 0.5f, Radians<double>.Right},
            {Radians<double>.Right + AcuteRadians, AcuteRadians, 1.0f, AcuteRadians},
            {Radians<double>.Right + AcuteRadians, AcuteRadians, 1.5f, Radians<double>.Zero},
        };

    public static TheoryData<Gradians<double>, Gradians<double>, float, Gradians<double>> LerpGradiansData => new()
    {
            {AcuteGradians, Gradians<double>.Right + AcuteGradians, -0.5f, Gradians<double>.Zero},
            {AcuteGradians, Gradians<double>.Right + AcuteGradians, 0.0f, AcuteGradians},
            {AcuteGradians, Gradians<double>.Right + AcuteGradians, 0.5f, Gradians<double>.Right},
            {AcuteGradians, Gradians<double>.Right + AcuteGradians, 1.0f, Gradians<double>.Right + AcuteGradians},
            {AcuteGradians, Gradians<double>.Right + AcuteGradians, 1.5f, Gradians<double>.Straight},

            {-AcuteGradians, -Gradians<double>.Right - AcuteGradians, -0.5f, Gradians<double>.Zero},
            {-AcuteGradians, -Gradians<double>.Right - AcuteGradians, 0.0f, -AcuteGradians},
            {-AcuteGradians, -Gradians<double>.Right - AcuteGradians, 0.5f, -Gradians<double>.Right},
            {-AcuteGradians, -Gradians<double>.Right - AcuteGradians, 1.0f, -Gradians<double>.Right - AcuteGradians},
            {-AcuteGradians, -Gradians<double>.Right - AcuteGradians, 1.5f, -Gradians<double>.Straight},

            {Gradians<double>.Right + AcuteGradians, AcuteGradians, -0.5f, Gradians<double>.Straight},
            {Gradians<double>.Right + AcuteGradians, AcuteGradians, 0.0f, Gradians<double>.Right + AcuteGradians},
            {Gradians<double>.Right + AcuteGradians, AcuteGradians, 0.5f, Gradians<double>.Right},
            {Gradians<double>.Right + AcuteGradians, AcuteGradians, 1.0f, AcuteGradians},
            {Gradians<double>.Right + AcuteGradians, AcuteGradians, 1.5f, Gradians<double>.Zero},
        };

    public static TheoryData<Revolutions<double>, Revolutions<double>, float, Revolutions<double>> LerpRevolutionsData => new()
    {
            {AcuteRevolutions, Revolutions<double>.Right + AcuteRevolutions, -0.5f, Revolutions<double>.Zero},
            {AcuteRevolutions, Revolutions<double>.Right + AcuteRevolutions, 0.0f, AcuteRevolutions},
            {AcuteRevolutions, Revolutions<double>.Right + AcuteRevolutions, 0.5f, Revolutions<double>.Right},
            {AcuteRevolutions, Revolutions<double>.Right + AcuteRevolutions, 1.0f, Revolutions<double>.Right + AcuteRevolutions},
            {AcuteRevolutions, Revolutions<double>.Right + AcuteRevolutions, 1.5f, Revolutions<double>.Straight},

            {-AcuteRevolutions, -Revolutions<double>.Right - AcuteRevolutions, -0.5f, Revolutions<double>.Zero},
            {-AcuteRevolutions, -Revolutions<double>.Right - AcuteRevolutions, 0.0f, -AcuteRevolutions},
            {-AcuteRevolutions, -Revolutions<double>.Right - AcuteRevolutions, 0.5f, -Revolutions<double>.Right},
            {-AcuteRevolutions, -Revolutions<double>.Right - AcuteRevolutions, 1.0f, -Revolutions<double>.Right - AcuteRevolutions},
            {-AcuteRevolutions, -Revolutions<double>.Right - AcuteRevolutions, 1.5f, -Revolutions<double>.Straight},

            {Revolutions<double>.Right + AcuteRevolutions, AcuteRevolutions, -0.5f, Revolutions<double>.Straight},
            {Revolutions<double>.Right + AcuteRevolutions, AcuteRevolutions, 0.0f, Revolutions<double>.Right + AcuteRevolutions},
            {Revolutions<double>.Right + AcuteRevolutions, AcuteRevolutions, 0.5f, Revolutions<double>.Right},
            {Revolutions<double>.Right + AcuteRevolutions, AcuteRevolutions, 1.0f, AcuteRevolutions},
            {Revolutions<double>.Right + AcuteRevolutions, AcuteRevolutions, 1.5f, Revolutions<double>.Zero},
        };

    [Theory]
    [MemberData(nameof(LerpDegreesData))]
    public void Lerp_Degrees_Should_Succeed(Degrees<double> a1, Degrees<double> a2, float t, Degrees<double> expected)
    {
        // arrange

        // act
        var result = Angle.Lerp(a1, a2, t);

        // assert
        result.Value.Should().BeApproximately(expected.Value, 0.00001);
    }

    [Theory]
    [MemberData(nameof(LerpRadiansData))]
    public void Lerp_Radians_Should_Succeed(Radians<double> a1, Radians<double> a2, float t, Radians<double> expected)
    {
        // arrange

        // act
        var result = Angle.Lerp(a1, a2, t);

        // assert
        result.Value.Should().BeApproximately(expected.Value, 0.00001);
    }

    [Theory]
    [MemberData(nameof(LerpGradiansData))]
    public void Lerp_Gradians_Should_Succeed(Gradians<double> a1, Gradians<double> a2, float t, Gradians<double> expected)
    {
        // arrange

        // act
        var result = Angle.Lerp(a1, a2, t);

        // assert
        result.Value.Should().BeApproximately(expected.Value, 0.00001);
    }

    [Theory]
    [MemberData(nameof(LerpRevolutionsData))]
    public void Lerp_Revolutions_Should_Succeed(Revolutions<double> a1, Revolutions<double> a2, float t, Revolutions<double> expected)
    {
        // arrange

        // act
        var result = Angle.Lerp(a1, a2, t);

        // assert
        result.Value.Should().BeApproximately(expected.Value, 0.00001);
    }
}
