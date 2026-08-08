# Async / Await

## Core Rules

| Rule | Rationale |
|------|-----------|
| Never `async void` (except event handlers) | Exceptions are unobservable; prefer `async Task` |
| Never `.Result` / `.Wait()` on a `Task` | Deadlocks in sync contexts (ASP.NET, UI) |
| Pass `CancellationToken` as last param | Propagate from caller to every I/O call |
| Await every `Task` returned | Suppress only with explicit `_ = FireAndForget()` and a reason |
| Don't `await` in a `finally` block | Use `IAsyncDisposable` instead |

## ConfigureAwait

```csharp
// Library code — always use ConfigureAwait(false) to avoid deadlocks
public async Task<Data> LoadAsync(CancellationToken ct = default)
{
    var raw = await _client.GetAsync(url, ct).ConfigureAwait(false);
    return await ParseAsync(raw, ct).ConfigureAwait(false);
}

// Application code (ASP.NET Core, console) — omit ConfigureAwait(false)
// ASP.NET Core has no SynchronizationContext so it doesn't matter, but
// omitting is conventional in app code.
```

## ValueTask vs Task

| Scenario | Use |
|----------|-----|
| Result is usually available synchronously (cache hit, memory) | `ValueTask<T>` |
| Always async (network, disk I/O) | `Task<T>` |
| Fire-and-forget | `Task` (never `void`) |
| Hot path, allocation-sensitive, sync fast-path | `ValueTask` |

```csharp
// ValueTask — avoid awaiting more than once or storing for later
public ValueTask<int> ReadCachedAsync(string key, CancellationToken ct = default)
{
    if (_cache.TryGetValue(key, out var val))
        return ValueTask.FromResult(val);   // no allocation on cache hit

    return new ValueTask<int>(FetchFromDbAsync(key, ct));
}
```

Never call `.Result` on a `ValueTask`; never await a `ValueTask` more than once.

## CancellationToken

```csharp
// Accept as last param with default
public async Task ProcessAsync(string input, CancellationToken ct = default)
{
    ct.ThrowIfCancellationRequested();              // fast check before work
    var result = await _service.CallAsync(input, ct).ConfigureAwait(false);
    await _store.SaveAsync(result, ct).ConfigureAwait(false);
}

// Link timeouts with cancellation
using var cts = CancellationTokenSource.CreateLinkedTokenSource(externalCt);
cts.CancelAfter(TimeSpan.FromSeconds(30));
await ProcessAsync(data, cts.Token);
```

## IAsyncEnumerable\<T\>

```csharp
// Producer — yield inside async method
public async IAsyncEnumerable<Record> StreamAsync(
    [EnumeratorCancellation] CancellationToken ct = default)
{
    await foreach (var batch in _db.ReadBatchesAsync(ct))
        foreach (var item in batch)
            yield return item;
}

// Consumer
await foreach (var record in source.StreamAsync(ct))
    await ProcessAsync(record, ct);
```

`[EnumeratorCancellation]` lets callers pass a token via `.WithCancellation(ct)` on the consumer side.

## Parallelism

```csharp
// Concurrent independent tasks — collect then await all
var taskA = _svc.GetA(ct);
var taskB = _svc.GetB(ct);
await Task.WhenAll(taskA, taskB);
var (a, b) = (taskA.Result, taskB.Result);   // safe after WhenAll

// Bounded parallel with Parallel.ForEachAsync (.NET 6+)
await Parallel.ForEachAsync(items, new ParallelOptions
{
    MaxDegreeOfParallelism = Environment.ProcessorCount,
    CancellationToken = ct
}, async (item, innerCt) =>
{
    await ProcessAsync(item, innerCt).ConfigureAwait(false);
});
```

## Common Anti-patterns

| Anti-pattern | Fix |
|-------------|-----|
| `async Task Foo() { return await Bar(); }` | `Task Foo() => Bar();` (drop async/await) |
| `Task.Run(() => Foo())` to go async | Expose truly async API; don't wrap sync code |
| `Task.Delay(0)` to yield | `await Task.Yield()` (explicit) |
| `new Task(...)` + `.Start()` | `Task.Run(...)` |
| Catching `TaskCanceledException` to swallow | Re-throw or handle intentionally |
| `async` lambda in `List.ForEach` | `foreach` loop with `await` |
