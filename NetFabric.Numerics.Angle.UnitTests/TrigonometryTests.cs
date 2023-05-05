using FluentAssertions;

namespace NetFabric.Numerics.UnitTests;

public class TrigonometryTests
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
    public void Cos_Double_Should_Succeed(Radians<double> angle)
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
    public void ACos_Double_Should_Succeed(Radians<double> angle)
    {
        // arrange
        var cos = Math.Cos(angle.Value);
        var expected = new Radians<double>(Math.Acos(cos));

        // act
        var result = Angle.Acos(cos);

        // assert
        result.Should().Be(expected);
    }

    public static TheoryData<Radians<float>> TrigonometryFloatData = new()
    {
        -Radians<float>.Full,
        -Radians<float>.Straight,
        -Radians<float>.Right,
        Radians<float>.Zero,
        Radians<float>.Right,
        Radians<float>.Straight,
        Radians<float>.Full,
    };

    [Theory]
    [MemberData(nameof(TrigonometryFloatData))]
    public void Cos_Float_Should_Succeed(Radians<float> angle)
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
    public void ACos_Float_Should_Succeed(Radians<float> angle)
    {
        // arrange
        var cos = Math.Cos(angle.Value);
        var expected = new Radians<float>((float)Math.Acos(cos));

        // act
        var result = Angle.Acos<double, float>(cos);

        // assert
        result.Should().Be(expected);
    }
}
