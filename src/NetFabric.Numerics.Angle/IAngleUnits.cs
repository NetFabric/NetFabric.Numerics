namespace NetFabric.Numerics;

/// <summary>
/// Defines the units of an angle.
/// </summary>
/// <typeparam name="TSelf">The type that implements the interface.</typeparam>
public interface IAngleUnits<TSelf>
    where TSelf: IAngleUnits<TSelf>
{
    /// <summary>
    /// Gets the symbol of the units.
    /// </summary>
    static abstract string Symbol { get; }

    /// <summary>
    /// Gets the value of a zero angle.
    /// </summary>
    static abstract double Zero { get; }

    /// <summary>
    /// Gets the value of a right angle.
    /// </summary>
    static abstract double Right { get; }

    /// <summary>
    /// Gets the value of a straight angle.
    /// </summary>
    static abstract double Straight { get; }

    /// <summary>
    /// Gets the value of a full angle.
    /// </summary>
    static abstract double Full { get; }
}

/// <summary>
/// Defines the units of an angle in degrees.
/// </summary>
public struct Degrees
    : IAngleUnits<Degrees>
{
    /// <summary>
    /// Gets the symbol of the units.
    /// </summary>
    public static string Symbol => "º";

    /// <summary>
    /// Gets the value in degrees of a zero angle.
    /// </summary>
    public static double Zero => 0.0;

    /// <summary>
    /// Gets the value in degrees of a right angle.
    /// </summary>
    public static double Right => 90.0;

    /// <summary>
    /// Gets the value in degrees of a straight angle.
    /// </summary>
    public static double Straight => 180.0;

    /// <summary>
    /// Gets the value in degrees of a full angle.
    /// </summary>
    public static double Full => 360.0;
}

/// <summary>
/// Defines the units of an angle in radians.
/// </summary>
public struct Radians
    : IAngleUnits<Radians>
{
    /// <summary>
    /// Gets the symbol of the units.
    /// </summary>
    public static string Symbol => " rad";

    /// <summary>
    /// Gets the value in radians of a zero angle.
    /// </summary>
    public static double Zero => 0.0;

    /// <summary>
    /// Gets the value in radians of a right angle.
    /// </summary>
    public static double Right => Math.PI / 2.0;

    /// <summary>
    /// Gets the value in radians of a straight angle.
    /// </summary>
    public static double Straight => Math.PI;

    /// <summary>
    /// Gets the value in radians of a full angle.
    /// </summary>
    public static double Full => Math.PI * 2.0;
}

/// <summary>
/// Defines the units of an angle in gradians.
/// </summary>
public struct Gradians
    : IAngleUnits<Gradians>
{
    /// <summary>
    /// Gets the symbol of the units.
    /// </summary>
    public static string Symbol => " grad";

    /// <summary>
    /// Gets the value in gradians of a zero angle.
    /// </summary>
    public static double Zero => 0.0;

    /// <summary>
    /// Gets the value in gradians of a right angle.
    /// </summary>
    public static double Right => 100.0;

    /// <summary>
    /// Gets the value in gradians of a straight angle.
    /// </summary>
    public static double Straight => 200.0;

    /// <summary>
    /// Gets the value in gradians of a full angle.
    /// </summary>
    public static double Full => 400.0;
}

/// <summary>
/// Defines the units of an angle in revolutions.
/// </summary>
public struct Revolutions
    : IAngleUnits<Revolutions>
{
    /// <summary>
    /// Gets the symbol of the units.
    /// </summary>
    public static string Symbol => " rev";

    /// <summary>
    /// Gets the value in revolutions of a zero angle.
    /// </summary>
    public static double Zero => 0.0;

    /// <summary>
    /// Gets the value in revolutions of a right angle.
    /// </summary>
    public static double Right => 0.25;

    /// <summary>
    /// Gets the value in revolutions of a straight angle.
    /// </summary>
    public static double Straight => 0.5;

    /// <summary>
    /// Gets the value in revolutions of a full angle.
    /// </summary>
    public static double Full => 1.0;
}
