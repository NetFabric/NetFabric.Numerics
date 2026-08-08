# Vector&lt;T&gt; and Fixed-Shape System.Numerics Types

## Vector&lt;T&gt; — Portable, Variable-Width

`Vector<T>` (namespace `System.Numerics`) is a variable-width vector: its length is fixed for the process lifetime, but `Vector<T>.Count` depends on the CPU running the code. It's **simpler to write** than `Vector128`/`256`/`512` — one generic type, no per-width branching — at the cost of not knowing the width at compile time and less fine-grained control over lane layout.

```csharp
// Illustrative only — prefer TensorPrimitives.Add, which already covers this and is
// optimized for every type Vector<T> supports.
int lastVectorStart = left.Length - Vector<double>.Count;
int i = 0;
for (; i <= lastVectorStart; i += Vector<double>.Count)
{
    var v1 = Vector.Create(left.AsSpan(i));
    var v2 = Vector.Create(right.AsSpan(i));
    (v1 + v2).CopyTo(result, i);
}
for (; i < left.Length; i++) result[i] = left[i] + right[i];   // scalar remainder
```

Use `Vector.IsHardwareAccelerated` to check acceleration (JIT-time constant, don't cache) and `Vector<T>.IsSupported` for the element type.

**When to reach for it over `Vector128`/`256`/`512`:** a quick, simple portable loop where you don't need control over the exact width, lane-crossing behavior, or the wider `System.Runtime.Intrinsics` op set. For anything performance-critical enough to hand-write in the first place, [Vector128](vector128-vectorization.md) is the officially recommended starting point — but `Vector<T>`'s simplicity is a legitimate reason to pick it for straightforward cases.

### MaxVectorTBitWidth

The system default for how wide `Vector<T>` grows can be narrower than the hardware fully supports — e.g. `Vector512<T>.IsHardwareAccelerated` can be `true` while `Vector<T>` stays 256-bit. Set `DOTNET_MaxVectorTBitWidth=512` to opt `Vector<T>` into the wider width (diagnostic/testing knob — see [testing-and-benchmarking.md](testing-and-benchmarking.md)).

## Fixed-Shape Types (Graphics/Geometry)

`System.Numerics` also provides SIMD-accelerated fixed-shape types for 2D/3D/4D graphics and geometry math — a different use case from bulk span vectorization:

- `Vector2`, `Vector3`, `Vector4` — 2, 3, and 4-element `float` vectors.
- `Matrix3x2`, `Matrix4x4` — matrices.
- `Plane`, `Quaternion` — plane and 3D-rotation types.

```csharp
Vector2 v1 = Vector2.Create(0.1f, 0.2f);
Vector2 v2 = Vector2.Create(1.1f, 2.2f);
Vector2 sum = v1 + v2;
float dot = Vector2.Dot(v1, v2);
Vector2 clamped = Vector2.Clamp(v1, Vector2.Zero, Vector2.One);

Matrix4x4 transposed = Matrix4x4.Transpose(m1);
Matrix4x4 product = Matrix4x4.Multiply(m1, m2);
```

Use these for graphics/geometry math, not as a substitute for `TensorPrimitives`/`Vector128` when vectorizing bulk numeric loops over spans.
