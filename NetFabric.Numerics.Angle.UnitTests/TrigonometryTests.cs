﻿using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class TrigonometryTests
{
    public static TheoryData<Angle<Radians, double>> TrigonometryDoubleData = new()
    {
        -Angle<Radians, double>.Full,
        -Angle<Radians, double>.Straight,
        -Angle<Radians, double>.Right,
        Angle<Radians, double>.Zero,
        Angle<Radians, double>.Right,
        Angle<Radians, double>.Straight,
        Angle<Radians, double>.Full,
    };

    [Theory]
    [MemberData(nameof(TrigonometryDoubleData))]
    public void Cos_Double_Should_Succeed(Angle<Radians, double> angle)
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
    public void ACos_Double_Should_Succeed(Angle<Radians, double> angle)
    {
        // arrange
        var cos = Math.Cos(angle.Value);
        var expected = new Angle<Radians, double>(Math.Acos(cos));

        // act
        var result = Angle.Acos(cos);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Angle<Radians, float>> TrigonometryFloatData = new()
    {
        -Angle<Radians, float>.Full,
        -Angle<Radians, float>.Straight,
        -Angle<Radians, float>.Right,
        Angle<Radians, float>.Zero,
        Angle<Radians, float>.Right,
        Angle<Radians, float>.Straight,
        Angle<Radians, float>.Full,
    };

    [Theory]
    [MemberData(nameof(TrigonometryFloatData))]
    public void Cos_Float_Should_Succeed(Angle<Radians, float> angle)
    {
        // arrange
        var expected = Math.Cos(angle.Value);

        // act
        var result = Angle.Cos(angle);

        // assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(TrigonometryFloatData))]
    public void ACos_Float_Should_Succeed(Angle<Radians, float> angle)
    {
        // arrange
        var cos = Math.Cos(angle.Value);
        var expected = new Angle<Radians, float>((float)Math.Acos(cos));

        // act
        var result = Angle.Acos<double, float>(cos);

        // assert
        result.Should().Be(expected);
    }
}
