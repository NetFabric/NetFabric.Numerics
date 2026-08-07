# Custom Iterators with yield

## Mechanics

`yield return`/`yield break` inside a method returning `IEnumerable<T>`/`IEnumerator<T>` make the compiler generate a hidden state-machine class implementing the interface. Each call to `MoveNext()` resumes execution right after the last `yield return`, runs until the next `yield return`/`yield break`/end of method, and pauses again — no work happens until something calls `MoveNext()`. The async counterpart (`async` method + `yield return` returning `IAsyncEnumerable<T>`) follows the same pausing model, driven by `MoveNextAsync()` instead — see below.

```csharp
public static IEnumerable<int> Countdown(int from)
{
    for (var i = from; i >= 0; i--)
        yield return i;
    // implicit yield break at the end
}
```

`yield break` exits the iterator early, equivalent to `MoveNext()` returning `false` from that point on.

## Async Iterators

Combining `async` and `yield return` in the same method produces an async iterator — the method is declared `async` and returns `IAsyncEnumerable<T>` directly (never `Task<IAsyncEnumerable<T>>`):

```csharp
async IAsyncEnumerable<string> ReadWordsAsync(TextReader reader)
{
    string? line;
    while ((line = await reader.ReadLineAsync()) is not null)
    {
        foreach (var word in line.Split(' '))
            yield return word;
    }
}
```

The compiler generates a single state machine combining both the `async` suspension points and the `yield return` pause points. Nothing runs until the caller's first `MoveNextAsync()` — same lazy-evaluation caveat as the sync case below. One extra restriction versus sync iterators: `ref struct` locals (e.g. `Span<T>`) can't be held across an `await` or `yield return` inside this state machine (**CS4007**) — the compiler hoists such locals onto the heap-allocated state machine, and `ref struct` types are stack-only by design.

Consuming an async iterator, cancellation propagation, and `ConfigureAwait`: [async-enumeration.md](async-enumeration.md).

## Enumerable vs Enumerator Return Type

A method can be declared to return `IEnumerable<T>` (most common — caller gets a fresh enumerator each time `GetEnumerator()` is called, supports multiple concurrent enumerations) or `IEnumerator<T>` directly (single-shot, caller drives `MoveNext()`/`Current` without a `foreach`-friendly factory). Prefer `IEnumerable<T>` unless the single-shot, already-positioned semantics are specifically wanted (e.g. manually implementing `GetEnumerator()` inside a custom collection type, where returning `IEnumerator<T>` from a `yield`-based helper avoids an extra allocation). Same reasoning applies to `IAsyncEnumerable<T>` vs `IAsyncEnumerator<T>` — return the enumerable unless single-shot semantics are specifically wanted.

## Lazy Evaluation & the Validation Pitfall

Nothing in an iterator method's body runs until the first `MoveNext()` call (or `MoveNextAsync()` for async iterators) — including parameter validation written directly in a `yield`-containing method:

```csharp
// Bug: ArgumentOutOfRangeException is thrown lazily, only when
// the caller starts enumerating (e.g. inside a foreach), not when
// Range(...) is called — surprising and hard to diagnose.
public static IEnumerable<int> Range(int start, int count)
{
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
    for (var i = 0; i < count; i++)
        yield return start + i;
}
```

Fix: split eager validation (in a normal, non-iterator method) from the lazy sequence (in a nested local function using `yield`):

```csharp
public static IEnumerable<int> Range(int start, int count)
{
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
    return GetEnumerable(start, count);

    static IEnumerable<int> GetEnumerable(int start, int count)
    {
        var end = start + count;
        for (var value = start; value < end; value++)
            yield return value;
    }
}
```

Now `Range(...)` throws immediately on the bad argument, while iteration itself stays lazy.

## Coroutines

`IEnumerator`/`IEnumerator<T>` are a natural fit for cooperative coroutines (popularized by Unity): a method with `yield return null;` (or a wait/condition object) pauses at that point; an external driver calls `MoveNext()` once per frame/tick to resume it. This reuses the compiler-generated state machine as a general-purpose "pausable function" mechanism, not just for producing sequences of data.

```csharp
IEnumerator Coroutine()
{
    Console.WriteLine("start");
    yield return null;              // pause here until next MoveNext()
    Console.WriteLine("resumed");
}
```

## Behavior Trees

The same mechanism models AI behavior trees: each node's `Tick()` is an `IEnumerator<BehaviorStatus>` (`Running`/`Success`/`Failure`), and composite nodes drive their children's enumerators:

| Node kind | Pattern |
|---|---|
| Leaf (`Condition`, `Action`) | Simple iterator yielding `Running` until done, then `Success`/`Failure` |
| `Invert` (decorator) | Wraps a child, swaps `Success`\<->`Failure`, passes through `Running` |
| `Repeat` / `RepeatUntilFail` | Re-invokes the child's enumerator after it completes |
| `Sequence` | Runs children in order, stopping (with `Failure`) at the first child that fails; `Success` only if all succeed |
| `Select` (a.k.a. `Selector`/fallback) | Runs children in order, stopping (with `Success`) at the first child that succeeds; `Failure` only if all fail |
| `ParallelAny` / `ParallelAll` | Advances **all** children's enumerators once per tick (round-robin `MoveNext()` calls), aggregating status once any/all finish |

```csharp
IEnumerator<BehaviorStatus> Sequence(IEnumerable<IEnumerator<BehaviorStatus>> children)
{
    foreach (var child in children)
    {
        while (child.MoveNext())
        {
            if (child.Current == BehaviorStatus.Running) yield return BehaviorStatus.Running;
            else if (child.Current == BehaviorStatus.Failure) { yield return BehaviorStatus.Failure; yield break; }
            else break;   // Success: move to next child
        }
    }
    yield return BehaviorStatus.Success;
}
```

Composite nodes hold their children's `IEnumerator<BehaviorStatus>` instances directly (not `IEnumerable`), so each tick resumes exactly where the last one left off — no restart cost, and no allocation beyond the one-time `GetEnumerator()` call per node.
