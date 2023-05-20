using System.Numerics;

namespace NetFabric.Numerics
{
    class AngleReducedDebugView<TUnits, T>
        : AngleDebugView<TUnits, T>
        where TUnits : IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        public AngleReducedDebugView(AngleReduced<TUnits, T> angle)
            : base(angle)
        {
        }
    }
}
