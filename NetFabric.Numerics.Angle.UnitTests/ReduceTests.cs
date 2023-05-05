using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace NetFabric.Numerics.UnitTests;

public class ReduceTests
{
    public static TheoryData<Degrees<double>, Degrees<double>> CompareReducedData => new()
        {
            { Degrees<double>.Zero, Degrees<double>.Zero },
            { Degrees<double>.Right, Degrees<double>.Right },
            { Degrees<double>.Straight, Degrees<double>.Straight },

            { Degrees<double>.Zero + Degrees<double>.Full, Degrees<double>.Zero },
            { Degrees<double>.Right + Degrees<double>.Full, Degrees<double>.Right },
            { Degrees<double>.Straight + Degrees<double>.Full, Degrees<double>.Straight },

            { new Degrees<double>(10.0) + Degrees<double>.Full, new Degrees<double>(10.0) },
            { new Degrees<double>(10.0) - Degrees<double>.Full, new Degrees<double>(10.0) },
            { new Degrees<double>(-10.0) + Degrees<double>.Full, Degrees<double>.Full - new Degrees<double>(10.0) },

            { new Degrees<double>(10.0) + (2 * Degrees<double>.Full), new Degrees<double>(10.0) },
            { new Degrees<double>(10.0) - (2 * Degrees<double>.Full), new Degrees<double>(10.0) },
            { new Degrees<double>(-10.0) + (2 * Degrees<double>.Full), Degrees<double>.Full - new Degrees<double>(10.0) },
        };

    [Theory]
    [MemberData(nameof(CompareReducedData))]
    public void Reduce_Should_Succeed(Degrees<double> angle, Degrees<double> expected)
    {
        // arrange

        // act
        var result = Angle.Reduce(angle);

        // assert
        result.Should().Be(expected);
    }
}
