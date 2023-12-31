namespace NetFabric.Numerics.Rectangular2D;

public static partial class Vector
{
    public static Vector<T>? Average<T>(this IEnumerable<Vector<T>> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if(source.TryGetSpan(out var span))
            return span.Average();

        var sumX = T.Zero;
        var sumY = T.Zero;
        var count = T.Zero;
        foreach (var vector in source)
        {
            checked 
            { 
                sumX += vector.X;
                sumY += vector.Y;
                count++;
            }
        }
        return T.IsZero(count) 
            ? null 
            : new Vector<T>(sumX / count, sumY / count);
    }

    public static Vector<T>? Average<T>(this Vector<T>[] source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => source.AsSpan().Average();

    public static Vector<T>? Average<T>(this Span<Vector<T>> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => ((ReadOnlySpan<Vector<T>>)source).Average();

    public static Vector<T>? Average<T>(this ReadOnlySpan<Vector<T>> source)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => source.Length is 0
            ? null
            : Sum(source) / T.CreateChecked(source.Length);
}
