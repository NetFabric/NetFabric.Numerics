namespace NetFabric.Numerics.Spherical
{
    /// <summary>
    /// Represents a spherical coordinate system with specific radius and angle units.
    /// </summary>
    /// <typeparam name="TUnits">The type representing the angle units.</typeparam>
    /// <typeparam name="T">The type of the point coordinates.</typeparam>
    public readonly record struct CoordinateSystem<TUnits, T>
        : ICoordinateSystem
        where TUnits : struct, IAngleUnits<TUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        static readonly IReadOnlyList<Coordinate> coordinates
            = new[] {
                new Coordinate("Radius", typeof(T)),
                new Coordinate("Azimuth", typeof(Angle<TUnits, T>)),
                new Coordinate("Polar", typeof(Angle<TUnits, T>)),
            };

        /// <summary>
        /// Gets the list of coordinates in the spherical coordinate system.
        /// </summary>
        /// <remarks>
        /// Each coordinate contains information about its name and type.
        /// </remarks>
        public IReadOnlyList<Coordinate> Coordinates
            => coordinates;
    }
}
