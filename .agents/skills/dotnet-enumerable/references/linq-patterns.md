# LINQ Patterns

## Count() vs Any()

`Count()` (extension on `IEnumerable<T>`) enumerates fully — O(n) — unless the source implements `ICollection`/`ICollection<T>`, in which case LINQ shortcuts to the `Count` property. **Don't rely on this**: many enumerables don't implement it, and a single upstream LINQ operator (e.g. `.Where(...)`) wraps the source in an iterator that doesn't, silently destroying the optimization even if the original collection had it.

`Any()` calls `MoveNext()` exactly once — O(1), regardless of source type.

```csharp
// Bad: enumerates 3 times (Count==0 check, sum loop, second Count() for the average)
if (source.Count() == 0) return null;

// Good: Any() is O(1); or better, track count while summing once
if (!source.Any()) return null;
```

## Count() vs Count Property

`Count()` (parens — LINQ extension method, O(n) worst case) vs `Count` (no parens — property on `IReadOnlyCollection<T>`/`ICollection<T>`, O(1) by contract). If a method can accept `IReadOnlyCollection<T>` instead of `IEnumerable<T>`, do it — it documents and guarantees O(1) count and empty-check without allocating an enumerator.

## First() / Single() (and OrDefault variants)

Both are O(1) parameterless (one or two `MoveNext()` calls) but O(n) with a predicate — worst case scanning the whole sequence. `Single()` is the expensive one: even when the first item matches, it must keep scanning to prove no second match exists. Reserve `Single()`/`SingleOrDefault()` for tests/dev-time validation, not production hot paths, if uniqueness isn't structurally guaranteed (e.g. by a dictionary lookup).

Use the **predicate overloads** instead of chaining `Where()`:

```csharp
// Two enumerators (Where's + First's)
source.Where(x => x.Id == id).Single();

// One enumerator — same result, faster
source.Single(x => x.Id == id);
```

`FirstOrDefault()`/`SingleOrDefault()` avoid a costly thrown-exception path when "not found" is an expected, not exceptional, outcome.

## ToList() / ToArray()

Both force immediate allocation + full copy — heap pressure, and if the source size is unknown ahead of time (e.g. after a `Where()`), one or more internal resizes are needed too. Use them only when:

- the result will be enumerated **more than once**, or
- the whole result is guaranteed to fit in memory and mutation-free reuse across calls is needed.

A method returning a query result should not call `ToList()`/`ToArray()` internally — let the caller decide whether/how to materialize.

```csharp
// Wasteful: forces allocation + full copy even though evenNumbers/oddNumbers
// are each only enumerated once below
var numbers = Enumerable.Range(0, count).ToList();
var evenNumbers = numbers.Where(n => (n & 1) == 0).ToList();

// Compose instead — stays lazy, same result
var numbers = Enumerable.Range(0, count);
var evenNumbers = numbers.Where(n => (n & 1) == 0);
```

## null vs Enumerable.Empty\<T\>()

`null` is an invalid/uninitialized state; empty is a valid, zero-item state. Returning `null` breaks `foreach` (`NullReferenceException`) and breaks method composition. Always return `Enumerable.Empty<T>()`, or `yield break` from an iterator method, never `null`.

## IQueryable\<T\>

`IQueryable<T>` derives from `IEnumerable<T>` but converts the LINQ expression tree into something a backing engine (SQL, Cassandra CQL, ...) executes remotely — filtering/ordering/grouping happen in the engine, not client-side. Push as much of the query as possible into the `IQueryable` chain; use `.AsEnumerable()` (not `.ToList()`/`.ToArray()`) to switch to client-side LINQ while keeping lazy evaluation once you need an operator the provider doesn't support.

## LINQ Operators and Async Streams

None of the patterns above apply directly to `IAsyncEnumerable<T>` — the BCL's `System.Linq` operators (`Where`, `Select`, `Count()`, `First()`, ...) are only defined for `IEnumerable<T>`/`IQueryable<T>`, not for async streams. There's no built-in `.Where()` you can chain on an `IAsyncEnumerable<T>`. Options: consume it with `await foreach` and filter/project by hand inside the loop, or take a dependency on the community `System.Linq.Async` package, which adds `IAsyncEnumerable<T>`-returning LINQ operators. This is unrelated to `IQueryable<T>`'s own async execution methods (`ToListAsync()`, `FirstOrDefaultAsync()`, from EF Core) — those materialize a `Task<T>`/`Task<List<T>>` from a still-synchronous `IQueryable<T>`, they don't produce an `IAsyncEnumerable<T>` stream. See [async-enumeration.md](async-enumeration.md) for writing/consuming async streams directly.
