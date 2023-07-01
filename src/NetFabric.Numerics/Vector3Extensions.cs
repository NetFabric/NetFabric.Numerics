namespace NetFabric.Numerics;

static class Vector3Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4<T> ToVector4<T>(this in Vector3<T> vector)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => new(vector.X, vector.Y, vector.Z, T.Zero);
}