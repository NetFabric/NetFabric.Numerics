namespace NetFabric.Numerics.Geography;

/// <summary>
/// Represents a geodetic datum.
/// </summary>
public interface IDatum<TSelf>
    where TSelf : IDatum<TSelf>?
{
    static abstract string Name { get; }
    static abstract Offset Offset { get; }
    static abstract Ellipsoid Ellipsoid { get; }
}

public class WGS84
    : IDatum<WGS84>
{
    public static string Name => "WGS 84";
    public static Offset Offset => Offset.Zero;
    public static Ellipsoid Ellipsoid => Ellipsoid.WGS1984;
};

public class WGS1972
    : IDatum<WGS1972>
{
    public static string Name => "WGS 1972";
    public static Offset Offset => Offset.Zero;
    public static Ellipsoid Ellipsoid => Ellipsoid.WGS1972;
};

public class NAD83
    : IDatum<NAD83>
{
    public static string Name => "NAD 83";
    public static Offset Offset => Offset.Zero;
    public static Ellipsoid Ellipsoid => Ellipsoid.Grs1980;
};

public class NAD1927CONUS
    : IDatum<NAD1927CONUS>
{
    public static string Name => "NAD 27 CONUS";
    public static Offset Offset => new(new(-8.0, 160.0, 176.0), 0.0, 0.0, 0.0, 0.0);
    public static Ellipsoid Ellipsoid => Ellipsoid.Clarke1866;
};