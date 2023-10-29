namespace NetFabric.Numerics.Polar;

/// <summary>
/// Represents a Polar coordinate system with specific radius and angle units.
/// </summary>
/// <typeparam name="TAngleUnits">The units used for the angles.</typeparam>
/// <typeparam name="T">The type of the point coordinates.</typeparam>
/// <remarks>
/// In a Polar coordinate system, coordinates are represented using a radius and an azimuth to specify the position
/// of a point in 2D space. The radius represents the distance from the origin, and the azimuth represents the angle
/// measured counterclockwise from a reference direction. The choice of angle units is determined by the specified angle
/// units type, TAngleUnits.
/// </remarks>
public abstract class CoordinateSystem<TAngleUnits, T>
    : ICoordinateSystem
    where TAngleUnits : IAngleUnits
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Gets the list of coordinates in the polar coordinate system.
    /// </summary>
    /// <remarks>
    /// Each coordinate contains information about its name and type.
    /// </remarks>
    public static IReadOnlyList<Coordinate> Coordinates { get; } 
        = new[] {
            new Coordinate("Radius", typeof(T)),
            new Coordinate("Azimuth", typeof(Angle<TAngleUnits, T>)),
        };
}
