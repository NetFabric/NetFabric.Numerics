using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace NetFabric.Numerics.UnitTests;

public class ReduceTests
{
    public static TheoryData<Angle<Degrees, double>, Angle<Degrees, double>> CompareReducedData => new()
        {
            { Angle<Degrees, double>.Zero, Angle<Degrees, double>.Zero },
            { Angle<Degrees, double>.Right, Angle<Degrees, double>.Right },
            { Angle<Degrees, double>.Straight, Angle<Degrees, double>.Straight },

            { Angle<Degrees, double>.Zero + Angle<Degrees, double>.Full, Angle<Degrees, double>.Zero },
            { Angle<Degrees, double>.Right + Angle<Degrees, double>.Full, Angle<Degrees, double>.Right },
            { Angle<Degrees, double>.Straight + Angle<Degrees, double>.Full, Angle<Degrees, double>.Straight },

            { new Angle<Degrees, double>(10.0) + Angle<Degrees, double>.Full, new Angle<Degrees, double>(10.0) },
            { new Angle<Degrees, double>(10.0) - Angle<Degrees, double>.Full, new Angle<Degrees, double>(10.0) },
            { new Angle<Degrees, double>(-10.0) + Angle<Degrees, double>.Full, Angle<Degrees, double>.Full - new Angle<Degrees, double>(10.0) },

            { new Angle<Degrees, double>(10.0) + (2 * Angle<Degrees, double>.Full), new Angle<Degrees, double>(10.0) },
            { new Angle<Degrees, double>(10.0) - (2 * Angle<Degrees, double>.Full), new Angle<Degrees, double>(10.0) },
            { new Angle<Degrees, double>(-10.0) + (2 * Angle<Degrees, double>.Full), Angle<Degrees, double>.Full - new Angle<Degrees, double>(10.0) },
        };

    [Theory]
    [MemberData(nameof(CompareReducedData))]
    public void Reduce_Should_Succeed(Angle<Degrees, double> angle, Angle<Degrees, double> expected)
    {
        // arrange

        // act
        var result = Angle.Reduce(angle);

        // assert
        result.Should().Be(expected);
    }
}
