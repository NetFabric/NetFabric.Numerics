using NetFabric.Numerics;

/// <summary>
/// Represents an abstract base class for defining angle units.
/// </summary>
public abstract class AngleUnits
{
    /// <summary>
    /// Gets the name of the angle unit.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Gets the symbol representing the angle unit.
    /// </summary>
    public abstract string Symbol { get; }

    /// <summary>
    /// Gets the value representing zero degrees in this angle unit.
    /// </summary>
    public abstract double Zero { get; }

    /// <summary>
    /// Gets the value representing a right angle (90 degrees) in this angle unit.
    /// </summary>
    public abstract double Right { get; }

    /// <summary>
    /// Gets the value representing a straight angle (180 degrees) in this angle unit.
    /// </summary>
    public abstract double Straight { get; }

    /// <summary>
    /// Gets the value representing a full angle (360 degrees) in this angle unit.
    /// </summary>
    public abstract double Full { get; }
}

/// <summary>
/// Represents a concrete implementation of angle units for a specific type 'TAngleUnits'.
/// </summary>
/// <typeparam name="TAngleUnits">The type of angle units to be represented.</typeparam>
public sealed class AngleUnits<TAngleUnits> : AngleUnits
    where TAngleUnits : IAngleUnits
{
    // Create a lazy singleton instance of the angle units.
    static readonly Lazy<AngleUnits<TAngleUnits>> lazyInstance = new(() => new());

    /// <summary>
    /// Private constructor to prevent direct instantiation.
    /// </summary>
    private AngleUnits() { }

    /// <summary>
    /// Gets the singleton instance of the angle units for the specified type 'TAngleUnits'.
    /// </summary>
    internal static AngleUnits<TAngleUnits> Instance
        => lazyInstance.Value;

    /// <inheritdoc/>
    public override string Name
        => TAngleUnits.Name;

    /// <inheritdoc/>
    public override string Symbol
        => TAngleUnits.Symbol;

    /// <inheritdoc/>
    public override double Zero
        => TAngleUnits.Zero;

    /// <inheritdoc/>
    public override double Right
        => TAngleUnits.Right;

    /// <inheritdoc/>
    public override double Straight
        => TAngleUnits.Straight;

    /// <inheritdoc/>
    public override double Full
        => TAngleUnits.Full;
}
