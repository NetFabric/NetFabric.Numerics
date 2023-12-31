namespace NetFabric.Numerics.Polar;

public static partial class Vector
{
    public static Vector<TAngleUnits, T>? Average<TAngleUnits, T>(this IEnumerable<Vector<TAngleUnits, T>> source)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        if (source.TryGetSpan(out var span))
            return span.Average();

        var sumRadius = T.Zero;
        var sumAzimuth = T.Zero;
        var count = T.Zero;
        foreach (var vector in source)
        {
            checked 
            {
                sumRadius += vector.Radius;
                sumAzimuth += vector.Azimuth.Value;
                count++;
            }
        }
        return T.IsZero(count) 
            ? null 
            : new Vector<TAngleUnits, T>(sumRadius / count, new Angle<TAngleUnits, T>(sumAzimuth / count));
    }

    public static Vector<TAngleUnits, T>? Average<TAngleUnits, T>(this Vector<TAngleUnits, T>[] source)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.AsSpan().Average();

    public static Vector<TAngleUnits, T>? Average<TAngleUnits, T>(this Span<Vector<TAngleUnits, T>> source)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => ((ReadOnlySpan<Vector<TAngleUnits, T>>)source).Average();

    public static Vector<TAngleUnits, T>? Average<TAngleUnits, T>(this ReadOnlySpan<Vector<TAngleUnits, T>> source)
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => source.Length is 0
            ? null
            : Sum(source) / T.CreateChecked(source.Length);
}
