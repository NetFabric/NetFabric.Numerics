using System.Numerics;

namespace NetFabric.Numerics.Geodesy;

/// <summary>
/// Represents a geodetic datum.
/// </summary>
/// <remarks>
/// This interface defines the basic properties of a geodetic datum, including its name,
/// offset, and associated ellipsoid.
/// </remarks>
public interface IDatum
{
    /// <summary>
    /// Gets the name of the datum.
    /// </summary>
    static abstract string Name { get; }
}

/// <summary>
/// Represents a geodetic datum.
/// </summary>
/// <remarks>
/// This interface defines the basic properties of a geodetic datum, including its name,
/// offset, and associated ellipsoid.
/// </remarks>
public interface IDatum<T>
    : IDatum
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Gets the offset for the datum.
    /// </summary>
    static abstract Offset<T> Offset { get; }

    /// <summary>
    /// Gets the ellipsoid associated with the datum.
    /// </summary>
    static abstract Ellipsoid<T> Ellipsoid { get; }
}

/// <summary>
/// Represents the WGS 84 geodetic datum.
/// </summary>
/// <remarks>
/// This class represents the World Geodetic System 1984 (WGS 84) datum, which is widely used in
/// GPS and global mapping systems.
/// </remarks>
public abstract class WGS84<T> 
    : IDatum<T>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    /// <inheritdoc/>
    public static string Name => "World Geodetic System 1984 (WGS 84)";

    /// <inheritdoc/>
    public static Offset<T> Offset => Offset<T>.Zero;

    /// <inheritdoc/>
    public static Ellipsoid<T> Ellipsoid => Ellipsoid<T>.WGS1984;
}

/// <summary>
/// Represents the WGS 1972 geodetic datum.
/// </summary>
/// <remarks>
/// This class represents the World Geodetic System 1972 (WGS 1972) datum, which was used in
/// earlier GPS systems and geospatial applications.
/// </remarks>
public abstract class WGS1972<T> 
    : IDatum<T>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    /// <inheritdoc/>
    public static string Name => "World Geodetic System 1972 (WGS 1972)";

    /// <inheritdoc/>
    public static Offset<T> Offset => Offset<T>.Zero;

    /// <inheritdoc/>
    public static Ellipsoid<T> Ellipsoid => Ellipsoid<T>.WGS1972;
}

/// <summary>
/// Represents the NAD 83 geodetic datum.
/// </summary>
/// <remarks>
/// This class represents the North American Datum 1983 (NAD 83), a widely used datum in North
/// America for mapping and geospatial applications.
/// </remarks>
public abstract class NAD83<T> 
    : IDatum<T>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    /// <inheritdoc/>
    public static string Name => "North American Datum 1983 (NAD 83)";

    /// <inheritdoc/>
    public static Offset<T> Offset => Offset<T>.Zero;

    /// <inheritdoc/>
    public static Ellipsoid<T> Ellipsoid => Ellipsoid<T>.Grs1980;
}

/// <summary>
/// Represents the NAD 27 CONUS geodetic datum.
/// </summary>
/// <remarks>
/// This class represents the North American Datum 1927 (NAD 27) for the contiguous United States (CONUS).
/// It uses the Clarke 1866 ellipsoid and has a non-zero offset.
/// </remarks>
public abstract class NAD1927CONUS<T> 
    : IDatum<T>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    /// <inheritdoc/>
    public static string Name => "North American Datum 1927 (NAD 27)";

    /// <inheritdoc/>
    public static Offset<T> Offset => new(
        new(T.CreateChecked(-8.0), T.CreateChecked(160.0), T.CreateChecked(176.0)), 
        T.Zero, T.Zero, T.Zero, T.Zero);

    /// <inheritdoc/>
    public static Ellipsoid<T> Ellipsoid => Ellipsoid<T>.Clarke1866;
}
