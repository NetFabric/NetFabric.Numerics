﻿namespace NetFabric.Numerics;

/// <summary>
/// Represents a geometric type.
/// </summary>
public interface IGeometricBase
{        
    /// <summary>
    /// Gets the value for a given coordinate of the geometric object.
    /// </summary>
    /// <param name="index">The index of the coordinate to get the value.</param>
    /// <value>The value of the coordinate indexed by index.</value>
    /// <remarks>
    /// <para>
    /// The maximum value for the index is the number of coordinates minus one.
    /// </para>
    /// <para>
    /// The number of coordinates can be obtained from the <see cref="ICoordinateSystem.Coordinates"/> property.
    /// </para>
    /// </remarks>
    object this[int index] { get; }

    /// <summary>
    /// Gets the coordinate system of the geometric object.
    /// </summary>
    /// <value>The coordinate system of the geometric object.</value>
    CoordinateSystem CoordinateSystem { get; }
}

/// <summary>
/// Represents a geometric type.
/// </summary>
public interface IGeometricBase<TCoordinateSystem>
    : IGeometricBase
    where TCoordinateSystem : ICoordinateSystem
{    
    /// <summary>
    /// Gets the coordinate system of the geometric object.
    /// </summary>
    /// <value>The coordinate system of the geometric object.</value>
    new CoordinateSystem<TCoordinateSystem> CoordinateSystem 
        => CoordinateSystem<TCoordinateSystem>.Instance;

    CoordinateSystem IGeometricBase.CoordinateSystem
        => CoordinateSystem;
}
    
/// <summary>
/// Represents a geometric type.
/// </summary>
/// <typeparam name="TSelf">The type implementing the interface.</typeparam>
/// <typeparam name="TCoordinateSystem">The type representing the coordinate system.</typeparam>
public interface IGeometricBase<TSelf, TCoordinateSystem>
    : IGeometricBase<TCoordinateSystem>
    , IEquatable<TSelf>
    , IEqualityOperators<TSelf, TSelf, bool>
    where TSelf : struct, IGeometricBase<TSelf, TCoordinateSystem>?
    where TCoordinateSystem : ICoordinateSystem
{    
    /// <summary>
    /// Gets the zero value for the type.
    /// </summary>
    static abstract TSelf Zero { get; }

    /// <summary>
    /// Determines whether a value is zero.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if the value is a zero vector; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(TSelf value) 
        => value.Equals(TSelf.Zero);
}

