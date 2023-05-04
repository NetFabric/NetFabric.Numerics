using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NetFabric.Numerics.Angle.UnitTests;

public class RadiansTests
{
    public static TheoryData<Radians<double>> TrigonometryDoubleData = new()
    {
        -Radians<double>.Full,
        -Radians<double>.Straight,
        -Radians<double>.Right,
        Radians<double>.Zero,
        Radians<double>.Right,
        Radians<double>.Straight,
        Radians<double>.Full,
    };

    [Theory]
    [MemberData(nameof(TrigonometryDoubleData))]
    public void Cos_Should_Succeed(Radians<double> angle)
    {
        // arrange
        var expected = Math.Cos(angle.Value);

        // act
        var result = Angle.Cos(angle);

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(TrigonometryDoubleData))]
    public void ACos_Should_Succeed(Radians<double> angle)
    {
        // arrange
        var cos = Math.Cos(angle.Value);
        var expected = new Radians<double>(Math.Acos(cos));

        // act
        var result = Angle.Acos(cos);

        // assert
        result.Should().Be(expected);
    }
}
