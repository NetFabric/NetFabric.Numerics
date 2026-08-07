# Collection Interfaces

## IEnumerable / IEnumerator

```csharp
namespace System.Collections
{
    public interface IEnumerable { IEnumerator GetEnumerator(); }
    public interface IEnumerator { object Current { get; } bool MoveNext(); void Reset(); }
}
```

`IEnumerable` is a factory of `IEnumerator` instances. `IEnumerator` performs the traversal: `MoveNext()` advances and returns `false` when exhausted; `Current` reads the value at the current position. `Reset()` exists for COM interop and may throw `NotSupportedException`.

Consequences: no mutation, no random access, no size/other metadata beyond enumeration.

> Each call to `GetEnumerator()` must return an independent enumerator with its own position. Some third-party providers violate this (shared state across enumerators) — enumerating more than once then yields different results. Verify before relying on multi-enumeration.

## IEnumerable\<T\> / IEnumerator\<T\>

```csharp
namespace System.Collections.Generic
{
    public interface IEnumerable<out T> : IEnumerable { new IEnumerator<T> GetEnumerator(); }
    public interface IEnumerator<out T> : IDisposable, IEnumerator { new T Current { get; } }
}
```

Strongly typed `Current` (no cast, no `InvalidCastException` risk) and derives from `IDisposable` — `foreach` always wraps the loop in a `try`/`finally` calling `Dispose()`.

## IAsyncEnumerable\<T\> / IAsyncEnumerator\<T\>

```csharp
namespace System.Collections.Generic
{
    public interface IAsyncEnumerable<out T> { IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken ct = default); }
    public interface IAsyncEnumerator<out T> : IAsyncDisposable { ValueTask<bool> MoveNextAsync(); T Current { get; } }
}
```

Use `await foreach (var item in source.WithCancellation(token))`. `MoveNextAsync()` and `DisposeAsync()` let the app do other work while waiting for the next item (e.g. I/O-bound sources). Writing async iterators with `yield return`, cancellation propagation, and `ConfigureAwait`: [async-enumeration.md](async-enumeration.md).

## IReadOnlyCollection\<T\> / IReadOnlyList\<T\>

```csharp
public interface IReadOnlyCollection<out T> : IEnumerable<T> { int Count { get; } }
public interface IReadOnlyList<out T> : IReadOnlyCollection<T> { T this[int index] { get; } }
```

`Count` is O(1) — backed by a field or cheap calculation, never a full traversal. `IReadOnlyList<T>` adds an indexer: zero-based random access without allocating an enumerator (one method call vs the two — `MoveNext()` + `Current` — needed by enumeration). `foreach` still uses the enumerator on these types; use an explicit `for` loop to get the indexer's benefit (exceptions: arrays, `Span<T>`, and — since .NET 8 JIT improvements — `ImmutableArray<T>`, see [iteration-performance.md](iteration-performance.md)).

```csharp
public static double? Average(this IReadOnlyCollection<int> source)
{
    if (source.Count == 0) return null;   // O(1), no enumerator allocated
    var sum = 0;
    foreach (var value in source) sum += value;
    return sum / (double)source.Count;
}
```

## ICollection\<T\> / IList\<T\>

Add mutation (`Add`, `Remove`, `Clear`, indexer setter, ...) on top of the read-only pair, but **don't derive from `IReadOnlyCollection<T>`/`IReadOnlyList<T>`** — for backwards compatibility, the hierarchy is awkward: a collection implementing both must implement each set independently, and passing an `ICollection<T>` where `IReadOnlyCollection<T>` is expected requires an explicit cast (ambiguity between the two `Count` members otherwise).

An immutable interface is simply one whose methods can't mutate state — casting a mutable `List<T>` to `IEnumerable<T>` makes it "immutable" through that reference, but the underlying object is still mutable elsewhere. `System.Collections.Immutable` types (e.g. `ImmutableArray<T>`) are genuinely immutable: "mutating" operations return a new instance.

## Which Interface to Use

| Role | Guidance |
|---|---|
| Method **return type** | Expose the most-capable interface the implementation genuinely provides — `IReadOnlyList<T>` lets callers choose enumerator or indexer; `IEnumerable<T>` only allows enumeration |
| Method **parameter type** | Consume the least-capable interface the algorithm needs — don't require `IList<T>` if you never mutate or index |
| Implementing a **new collection** | Implement as many of these interfaces as the data structure legitimately supports, to maximize compatibility with existing methods (LINQ only needs `IEnumerable<T>`; other libraries may need random access or mutation) |
| Items produced **incrementally over time** (I/O-bound source: paged API, DB cursor, file stream) | Return `IAsyncEnumerable<T>` instead of any sync interface — pair with `await foreach` on the consumer side, see [async-enumeration.md](async-enumeration.md) |

`IReadOnlyList<T>` is a historically bad name — random access isn't limited to list-like structures, but the name stuck.

Official guidance: [Guidelines for Collections](https://learn.microsoft.com/dotnet/standard/design-guidelines/guidelines-for-collections) — prefer `Collection<T>`/`ReadOnlyCollection<T>` for public read/write and read-only properties respectively; implement `IEnumerable<T>` at minimum for any custom collection, adding `ICollection<T>`/`IList<T>` only where genuinely supported.
