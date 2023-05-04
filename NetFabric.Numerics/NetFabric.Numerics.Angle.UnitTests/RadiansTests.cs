using FluentAssertions;

namespace NetFabric.Numerics.Angle.UnitTests
{
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
            var result = Radians<double>.Cos(angle);

            // assert
            result.Should().Be(expected);
        }
    }
}
