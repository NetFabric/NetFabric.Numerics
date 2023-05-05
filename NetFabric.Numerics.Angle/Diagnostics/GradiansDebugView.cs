using System.Numerics;

namespace NetFabric.Numerics
{
    class GradiansDebugView<T>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        readonly Gradians<T> angle;

        public GradiansDebugView(Gradians<T> angle) 
        {
            this.angle = angle;
        }

        public string? Radians
            => angle.ToRadians<T>().Value.ToString();

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
            => angle.Value.ToString();

        public string? Revolutions
            => angle.ToRevolutions<T>().Value.ToString();
    }
}