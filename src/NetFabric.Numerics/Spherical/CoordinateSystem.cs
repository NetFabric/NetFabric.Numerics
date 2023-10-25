namespace NetFabric.Numerics.Spherical;

/// <summary>
/// Represents a Spherical coordinate system with specific radius and angle units.
/// </summary>
/// <typeparam name="TAngleUnits">The units used for the angles.</typeparam>
/// <typeparam name="T">The type of the point coordinates.</typeparam>
/// <remarks>
/// In a Spherical coordinate system, coordinates are represented using a radius, an azimuth, and a polar angle to specify
/// the position of a point in 3D space. The radius represents the distance from the origin, the azimuth represents
/// the angle measured counterclockwise from a reference direction in the XY plane, and the polar angle represents
/// the angle measured from the positive Z-axis. The choice of angle units is determined by the specified angle units type,
/// TAngleUnits.
/// </remarks>
public readonly record struct CoordinateSystem<TAngleUnits, T>
    : ICoordinateSystem
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    static readonly IReadOnlyList<Coordinate> coordinates
        = new[] {
            new Coordinate("Radius", typeof(T)),
            new Coordinate("Azimuth", typeof(Angle<TAngleUnits, T>)),
            new Coordinate("Polar", typeof(Angle<TAngleUnits, T>)),
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
