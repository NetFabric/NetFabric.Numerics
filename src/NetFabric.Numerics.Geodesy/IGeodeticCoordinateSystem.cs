using System.Numerics;

namespace NetFabric.Numerics.Geodesy;

/// <summary>
/// Represents an interface for a geodetic coordinate system with type-specific operations.
/// </summary>
/// <typeparam name="T">The floating-point type used in the coordinate system.</typeparam>
/// <remarks>
/// This interface provides functionality related to geodetic coordinate systems for a specific floating-point type 'T'.
/// </remarks>
public interface IGeodeticCoordinateSystem<T> : ICoordinateSystem
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Gets the datum associated with this geodetic coordinate system.
    /// </summary>
    static abstract Datum<T> Datum { get; }
}

/// <summary>
/// Represents an interface for a geodetic coordinate system with type-specific operations and datum.
/// </summary>
/// <typeparam name="TCoordinateSystem">The type of coordinate system.</typeparam>
/// <typeparam name="TDatum">The type representing the datum.</typeparam>
/// <typeparam name="T">The floating-point type used in the coordinate system.</typeparam>
/// <remarks>
/// This interface extends the base 'IGeodeticCoordinateSystem' with type-specific operations and datum.
/// </remarks>
public interface IGeodeticCoordinateSystem<TCoordinateSystem, TDatum, T>
    : IGeodeticCoordinateSystem<T>
    , ICoordinateSystem
    where TCoordinateSystem : ICoordinateSystem
    where TDatum : IDatum<T>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Gets the datum associated with this geodetic coordinate system with type-specific datum.
    /// </summary>
    new static Datum<TDatum, T> Datum
        => Datum<TDatum, T>.Instance;

    /// <summary>
    /// Gets the datum associated with this geodetic coordinate system.
    /// </summary>
    static Datum<T> IGeodeticCoordinateSystem<T>.Datum
        => Datum;
}
