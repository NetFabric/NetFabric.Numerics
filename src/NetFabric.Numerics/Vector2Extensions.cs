using System.Runtime.Intrinsics;

namespace NetFabric.Numerics;

static class Vector2Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<uint> AsVector64(this Vector2<uint> vector)
        => Unsafe.As<Vector2<uint>, Vector64<uint>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<int> AsVector64(this Vector2<int> vector)
        => Unsafe.As<Vector2<int>, Vector64<int>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> AsVector64(this Vector2<float> vector)
        => Unsafe.As<Vector2<float>, Vector64<float>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<uint> AsVector128(this Vector2<uint> vector)
        => new Vector4<uint>(vector.X, vector.Y, 0, 0).AsVector128();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> AsVector128(this Vector2<int> vector)
        => new Vector4<int>(vector.X, vector.Y, 0, 0).AsVector128();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> AsVector128(this Vector2<float> vector)
        => new Vector4<float>(vector.X, vector.Y, 0.0f, 0.0f).AsVector128();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<ulong> AsVector128(this Vector2<ulong> vector)
        => Unsafe.As<Vector2<ulong>, Vector128<ulong>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<long> AsVector128(this Vector2<long> vector)
        => Unsafe.As<Vector2<long>, Vector128<long>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<double> AsVector128(this Vector2<double> vector)
        => Unsafe.As<Vector2<double>, Vector128<double>>(ref vector);

    // -----------------------------------------------------

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<uint> AsVector2(this Vector64<uint> vector)
        => Unsafe.As<Vector64<uint>, Vector2<uint>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<int> AsVector2(this Vector64<int> vector)
        => Unsafe.As<Vector64<int>, Vector2<int>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<float> AsVector2(this Vector64<float> vector)
        => Unsafe.As<Vector64<float>, Vector2<float>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<uint> AsVector2(this Vector128<uint> vector)
    {
        ref byte address = ref Unsafe.As<Vector128<uint>, byte>(ref vector);
        return Unsafe.ReadUnaligned<Vector2<uint>>(ref address);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<int> AsVector2(this Vector128<int> vector)
    {
        ref byte address = ref Unsafe.As<Vector128<int>, byte>(ref vector);
        return Unsafe.ReadUnaligned<Vector2<int>>(ref address);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<float> AsVector2(this Vector128<float> vector)
    {
        ref byte address = ref Unsafe.As<Vector128<float>, byte>(ref vector);
        return Unsafe.ReadUnaligned<Vector2<float>>(ref address);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<ulong> AsVector2(this Vector128<ulong> vector)
    => Unsafe.As<Vector128<ulong>, Vector2<ulong>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<long> AsVector2(this Vector128<long> vector)
        => Unsafe.As<Vector128<long>, Vector2<long>>(ref vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<double> AsVector2(this Vector128<double> vector)
        => Unsafe.As<Vector128<double>, Vector2<double>>(ref vector);
}
