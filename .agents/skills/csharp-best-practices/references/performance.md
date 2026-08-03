# Performance

## Span\<T\> and Memory\<T\>

| Type | Heap? | Async? | Use |
|------|-------|--------|-----|
| `Span<T>` | ❌ (stack only) | ❌ | Synchronous slice/parse/copy |
| `ReadOnlySpan<T>` | ❌ | ❌ | Read-only slice; string → `ReadOnlySpan<char>` |
| `Memory<T>` | ✅ | ✅ | Async-safe slice |
| `ReadOnlyMemory<T>` | ✅ | ✅ | Async-safe read-only slice |

```csharp
// Slice without allocation
ReadOnlySpan<char> trimmed = input.AsSpan().Trim();

// Parse without substring
bool ok = int.TryParse(line.AsSpan(start, length), out int value);

// Stack-allocate small buffers
Span<byte> buf = stackalloc byte[128];   // use only for ≤1 KB
```

## ArrayPool\<T\>

```csharp
var pool = ArrayPool<byte>.Shared;
byte[] rented = pool.Rent(minimumLength: 4096);
try
{
    Process(rented.AsSpan(0, actualLength));
}
finally
{
    pool.Return(rented, clearArray: false);   // set clearArray: true for secrets
}
```

Never store a rented array beyond the `finally` block. Never use `rented.Length` — it may be larger than requested; track actual length separately.

## Avoiding Boxing

| Cause | Fix |
|-------|-----|
| `object` parameter receiving value type | Generic `<T>` parameter |
| `IComparable` / non-generic interface | `IComparable<T>` / generic interface |
| `Enum` in `Dictionary<object,…>` | `Dictionary<MyEnum,…>` |
| `string.Format("{0}", intVal)` | String interpolation (no box in modern JIT) |
| `ArrayList`, `Hashtable` | Generic `List<T>`, `Dictionary<K,V>` |

## readonly struct

```csharp
// Without readonly: JIT makes defensive copies on every in/ref call
public readonly struct Vector3<T>(T X, T Y, T Z) where T : struct, INumber<T>
{
    // All members implicitly pure; no copy on in-parameter
    public T LengthSquared() => X * X + Y * Y + Z * Z;
}
```

Add `readonly` to any struct whose members don't mutate state. Cost: zero at runtime; gain: no hidden copies.

## FrozenDictionary / FrozenSet (.NET 8+)

```csharp
// Build once; lookups are faster than Dictionary for read-heavy workloads
FrozenDictionary<string, int> codes = new Dictionary<string, int>
{
    ["US"] = 1, ["UK"] = 44, ["DE"] = 49
}.ToFrozenDictionary();

// Typical hot path — no thread safety overhead, no mutation
int dialCode = codes["US"];
```

Use `Dictionary<K,V>` when content changes; `FrozenDictionary<K,V>` when populated once and read many times.

## LINQ vs Loops

| Context | Recommendation |
|---------|---------------|
| Readable one-off queries | LINQ fine |
| Hot loop (called millions of times) | `for`/`foreach` + `Span<T>` |
| Allocation-sensitive path | Avoid LINQ; use `foreach` on `IEnumerable<T>` or index loop |
| Filtering + projecting once | LINQ with `ToArray()` / `ToList()` at end only |

`IEnumerable<T>` LINQ chains defer execution — call `.ToArray()` or `.ToList()` once to avoid re-enumeration.

## String Performance

```csharp
// Avoid in loops:
result += part;              // O(n²) allocations

// Use instead:
var sb = new StringBuilder();
foreach (var part in parts) sb.Append(part);
string result = sb.ToString();

// Or for known format:
string s = string.Create(totalLength, state, (span, st) =>
{
    st.WriteTo(span);
});

// SearchValues<char> for repeated char-set scanning (.NET 8+)
private static readonly SearchValues<char> Delimiters =
    SearchValues.Create([',', ';', '\t']);

int idx = line.AsSpan().IndexOfAny(Delimiters);
```

## Vectorization (SIMD)

```csharp
// System.Numerics.Vector<T> — auto-vectorized by JIT
static void Add(float[] a, float[] b, float[] result)
{
    int i = 0;
    int vecLen = Vector<float>.Count;
    for (; i <= a.Length - vecLen; i += vecLen)
        (new Vector<float>(a, i) + new Vector<float>(b, i)).CopyTo(result, i);
    for (; i < a.Length; i++)
        result[i] = a[i] + b[i];   // scalar tail
}
```

For more control use `System.Runtime.Intrinsics` (`Vector128<T>`, `Vector256<T>`, `Vector512<T>`).

## Key Annotations

| Attribute | Effect |
|-----------|--------|
| `[MethodImpl(MethodImplOptions.AggressiveInlining)]` | Force inline for small hot methods |
| `[SkipLocalsInit]` | Skip zero-init of locals (unsafe; use only in perf-critical, audited code) |
| `[InlineArray(N)]` | Fixed-size inline buffer in a struct (C# 12) |
