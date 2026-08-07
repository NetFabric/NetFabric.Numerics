# Async Enumeration Usage (IAsyncEnumerable\<T\>)

Consuming and cancelling async streams — concerns that have no sync-`foreach` equivalent. Interface shapes: [enumerable-interfaces.md](enumerable-interfaces.md#iasyncenumerablet--iasyncenumeratort). Writing an async iterator with `yield return`: [custom-iterators.md](custom-iterators.md#async-iterators).

## `await foreach` Desugaring

```csharp
var enumerator = source.GetAsyncEnumerator();
try
{
    while (await enumerator.MoveNextAsync())
        Use(enumerator.Current);
}
finally { if (enumerator is not null) await enumerator.DisposeAsync(); }
```

`await foreach` binds by the same pattern-based rules as `foreach`: a directly callable `GetAsyncEnumerator()` returning a type with `Current` and an awaitable `MoveNextAsync()` is enough — `IAsyncEnumerable<T>` isn't strictly required, matching how `foreach` doesn't strictly require `IEnumerable<T>`.

`foreach`/`await foreach` are not interchangeable — using the wrong one is a compile error naming the correct one to use:

| Code | Meaning |
|---|---|
| CS8414 / CS9353 | `foreach` used on a type that only implements the async-enumerable pattern — use `await foreach` |
| CS8415 | `await foreach` used on a type that only implements the sync-enumerable pattern — use `foreach` |
| CS8412 | `GetAsyncEnumerator`'s return type doesn't have a public `Current`/`MoveNextAsync` matching the pattern |

## Cancellation

Two ways to get a token into an async iterator:

```csharp
async IAsyncEnumerable<T> QueryAsync<T>(
    [EnumeratorCancellation] CancellationToken cancellationToken = default)
{
    while (...)
    {
        cancellationToken.ThrowIfCancellationRequested();
        yield return item;
    }
}

// caller
await foreach (var item in QueryAsync<T>().WithCancellation(token))
{
    ...
}
```

- `[EnumeratorCancellation]` on a `CancellationToken` parameter tells the compiler to route the token passed to the generated `GetAsyncEnumerator(CancellationToken)` into that parameter's value.
- `.WithCancellation(token)` (on the caller's side) is how that token actually gets supplied — it's what ends up calling `GetAsyncEnumerator(token)` instead of `GetAsyncEnumerator(default)`.
- Diagnostics: **CS8424** (attribute used on a non-token parameter, or outside an async-iterator returning `IAsyncEnumerable<T>`) fails to compile; **CS8425** (a token parameter exists but isn't attributed) compiles but silently ignores the caller's `WithCancellation` token; **CS8426** (attribute on more than one parameter) fails to compile.

## ConfigureAwait

Each `MoveNextAsync()` await captures the current `SynchronizationContext`/`TaskScheduler` by default, same as any other `await`. Disable capturing on the whole stream with `.ConfigureAwait(false)` (an extension on `IAsyncEnumerable<T>`, not per-await inside the iterator):

```csharp
await foreach (var item in source.WithCancellation(token).ConfigureAwait(false))
{
    ...
}
```

## IAsyncEnumerable\<T\> vs Task\<IReadOnlyList\<T\>\>

| Return | Use when |
|---|---|
| `IAsyncEnumerable<T>` | Items are produced incrementally (paged API, DB cursor, file stream) and the caller can start processing before the full sequence is available |
| `Task<IReadOnlyList<T>>` / `Task<T[]>` | The whole result must be materialized anyway, or the caller always needs random access/count |

Don't wrap an already-materializing `Task<List<T>>`-returning method as `IAsyncEnumerable<T>` purely for API consistency — that forces full materialization before the first `yield return` fires, losing the incremental-processing benefit while adding state-machine overhead for nothing.
