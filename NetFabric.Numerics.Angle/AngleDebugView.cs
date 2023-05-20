using NetFabric.Numerics;
using System.Numerics;

namespace NetFabric
{
    class AngleDebugView<TUnits, T>
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        readonly Angle<TUnits, T> angle;

        public AngleDebugView(Angle<TUnits, T> angle)
        {
            this.angle = angle;
        }

        public AngleDebugView(AngleReduced<TUnits, T> angle)
        {
            this.angle = angle;
        }

        public string? Degrees
            => ToDegrees()?.ToString();

        public string? DegreesMinutes
        {
            get
            {
                var value = ToDegrees();
                if (value is null)
                    return default;

                var (degrees, minutes) = Angle.ToDegreesMinutes<T, long, float>(value.Value);
                return $"{degrees}° {minutes}'";
            }
        }

        public string? DegreesMinutesSeconds
        {
            get
            {
                var value = ToDegrees();
                if (value is null)
                    return default;

                var (degrees, minutes, seconds) = Angle.ToDegreesMinutesSeconds<T, long, byte, float>(value.Value);
                return $"{degrees}° {minutes}' {seconds}''";
            }
        }

        public string? Radians
            => ToRadians()?.ToString();

        public string? Gradians
            => ToGradians()?.ToString();

        public string? Revolutions
            => ToRevolutions()?.ToString();

        Angle<Degrees, T>? ToDegrees()
            => angle switch
            {
                Angle<Degrees, T> degrees => degrees,
                Angle<Radians, T> radians => Angle.ToDegrees(radians),
                Angle<Gradians, T> gradians => Angle.ToDegrees(gradians),
                Angle<Revolutions, T> revolutions => Angle.ToDegrees(revolutions),
                _ => default,
            };
        Angle<Radians, T>? ToRadians()
            => angle switch
            {
                Angle<Degrees, T> degrees => Angle.ToRadians(degrees),
                Angle<Radians, T> radians => radians,
                Angle<Gradians, T> gradians => Angle.ToRadians(gradians),
                Angle<Revolutions, T> revolutions => Angle.ToRadians(revolutions),
                _ => default,
            };

        Angle<Gradians, T>? ToGradians()
            => angle switch
            {
                Angle<Degrees, T> degrees => Angle.ToGradians(degrees),
                Angle<Radians, T> radians => Angle.ToGradians(radians),
                Angle<Gradians, T> gradians => gradians,
                Angle<Revolutions, T> revolutions => Angle.ToGradians(revolutions),
                _ => default,
            };

        Angle<Revolutions, T>? ToRevolutions()
            => angle switch
            {
                Angle<Degrees, T> degrees => Angle.ToRevolutions(degrees),
                Angle<Radians, T> radians => Angle.ToRevolutions(radians),
                Angle<Gradians, T> gradians => Angle.ToRevolutions(gradians),
                Angle<Revolutions, T> revolutions => revolutions,
                _ => default,
            };
    }
}