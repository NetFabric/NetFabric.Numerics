using System.Numerics;

namespace NetFabric.Numerics
{
    class RadiansDebugView<T>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        readonly Radians<T> angle;

        public RadiansDebugView(Radians<T> angle) 
        {
            this.angle = angle;
        }

        public string? Radians
            => angle.Value.ToString();

        public string Degrees
            => $"{angle.ToDegrees<T>().Value}°";

        public string DegreesMinutes
        {
            get
            {
                var (degrees, minutes) = angle.ToDegrees<T>().AsDegreesMinutes<long, float>();
                return $"{degrees}° {minutes}'";
            }
        }

        public string DegreesMinutesSeconds
        {
            get
            {
                var (degrees, minutes, seconds) = angle.ToDegrees<T>().AsDegreesMinutesSeconds<long, byte, float>();
                return $"{degrees}° {minutes}' {seconds}''";
            }
        }

        public string? Gradians
            => angle.ToGradians<T>().Value.ToString();

        public string? Revolutions
            => angle.ToRevolutions<T>().Value.ToString();
    }
}