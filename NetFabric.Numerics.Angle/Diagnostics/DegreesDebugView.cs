using System.Numerics;

namespace NetFabric.Numerics
{
    class DegreesDebugView<T>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        readonly Degrees<T> angle;

        public DegreesDebugView(Degrees<T> angle) 
        {
            this.angle = angle;
        }

        public string? Radians
            => angle.ToRadians<T>().Value.ToString();

        public string Degrees
            => $"{angle.Value}°";

        public string DegreesMinutes
        {
            get
            {
                var (degrees, minutes) = angle.AsDegreesMinutes<long, float>();
                return $"{degrees}° {minutes}'";
            }
        }

        public string DegreesMinutesSeconds
        {
            get
            {
                var (degrees, minutes, seconds) = angle.AsDegreesMinutesSeconds<long, byte, float>();
                return $"{degrees}° {minutes}' {seconds}''";
            }
        }

        public string? Gradians
            => angle.ToGradians<T>().Value.ToString();

        public string? Revolutions
            => angle.ToRevolutions<T>().Value.ToString();
    }
}