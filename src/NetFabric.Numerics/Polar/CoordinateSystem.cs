namespace NetFabric.Numerics.Polar
{
    /// <summary>
    /// Represents a polar coordinate system with specific radius and angle units.
    /// </summary>
    /// <typeparam name="TAngleUnits">The type representing the angle units.</typeparam>
    /// <typeparam name="T">The type of the point coordinates.</typeparam>
    public readonly record struct CoordinateSystem<TAngleUnits, T>
        : ICoordinateSystem
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        static readonly IReadOnlyList<Coordinate> coordinates
            = new[] {
                new Coordinate("Radius", typeof(T)),
                new Coordinate("Azimuth", typeof(Angle<TAngleUnits, T>)),
            };

        /// <summary>
        /// Gets the list of coordinates in the polar coordinate system.
        /// </summary>
        /// <remarks>
        /// Each coordinate contains information about its name and type.
        /// </remarks>
        public IReadOnlyList<Coordinate> Coordinates
            => coordinates;
    }
}
