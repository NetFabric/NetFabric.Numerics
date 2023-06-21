namespace NetFabric.Numerics;

/// <summary>
/// Represents a base type.
/// </summary>
/// <typeparam name="TSelf">The type that implements the interface.</typeparam>
public interface INumericBase<TSelf>
    : IEquatable<TSelf>,
      IEqualityOperators<TSelf, TSelf, bool>
    where TSelf : struct, INumericBase<TSelf>?
{
    /// <summary>Gets the value <c>0</c> for the type.</summary>
    static abstract TSelf Zero { get; }

    /// <summary>
    /// Determines whether <paramref name="value"/> is zero.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if the value is a zero vector; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(TSelf value) 
        => value.Equals(TSelf.Zero);
}
