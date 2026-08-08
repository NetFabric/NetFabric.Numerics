# Language Features

## Records & Structs

```csharp
// readonly record struct — best for small immutable value types
public readonly record struct Point(double X, double Y);

// record class — immutable reference type with value equality
public record class Person(string Name, int Age);

// with-expression works on both
var p2 = p1 with { X = 3.0 };
```

**Struct guidelines:**
- Add `readonly` to prevent defensive copies when passed as `in`
- Keep ≤4 fields; larger structs are slower to copy than references
- Implement `IEquatable<T>` explicitly for `struct` (auto-generated for records)

## Primary Constructors (C# 12)

```csharp
// Parameters are captured as fields implicitly — do NOT re-declare them
public class Service(ILogger<Service> logger, IRepository repo)
{
    public async Task DoWork() => await repo.SaveAsync();
    // logger and repo are in scope everywhere in the class
}

// readonly record struct with primary ctor — canonical pattern
public readonly record struct Angle<TUnits, T>(T Value)
    where TUnits : IAngleUnits<TUnits>
    where T : struct, INumber<T>;
```

## Required Members (C# 11)

```csharp
public class Config
{
    public required string ConnectionString { get; init; }
    public required int MaxRetries { get; init; }
    public int TimeoutMs { get; init; } = 5000;   // optional, has default
}

// Enforced at call site:
var cfg = new Config { ConnectionString = "...", MaxRetries = 3 };
```

## Collection Expressions (C# 12)

```csharp
// Prefer over new List<>(), new int[], Array.Empty<>(), Enumerable.Empty<>()
int[] nums = [1, 2, 3];
List<string> names = ["Alice", "Bob"];
ReadOnlySpan<byte> empty = [];

// Spread operator
int[] combined = [..a, ..b];
```

## Pattern Matching

```csharp
// Switch expression — prefer over switch statement for mapping
string Describe(Shape s) => s switch
{
    Circle { Radius: > 10 } c  => $"large circle r={c.Radius}",
    Circle c                   => $"small circle r={c.Radius}",
    Rectangle { Width: var w, Height: var h } when w == h => "square",
    Rectangle r                => $"rect {r.Width}x{r.Height}",
    _                          => "unknown"
};

// is-pattern with property pattern
if (result is { IsSuccess: true, Value: var value })
    Process(value);

// List patterns (C# 11)
if (args is [var first, ..])
    Console.WriteLine(first);
```

## Raw String Literals (C# 11)

```csharp
// No escaping needed; leading indent stripped
var json = """
    {
        "name": "Alice",
        "path": "C:\\Users\\Alice"
    }
    """;

// Interpolated raw strings
var query = $"""
    SELECT *
    FROM {tableName}
    WHERE id = {id}
    """;
```

## Nullable Reference Types

```csharp
// Project-wide in .csproj:
// <Nullable>enable</Nullable>

// Return null only for "not found"; never for empty collections
public string? FindById(int id) => _dict.GetValueOrDefault(id);
public IReadOnlyList<Item> GetAll() => _items;   // never null

// Null-coalescing assignment
_cache ??= new Dictionary<string, object>();

// Null-conditional with index
var first = list?.Count > 0 ? list[0] : null;
```

## Using Declarations & Disposal

```csharp
// Prefer using declaration (C# 8) over using block when scope is clear
using var stream = File.OpenRead(path);
using var reader = new StreamReader(stream);
// disposed at end of enclosing scope

// IAsyncDisposable
await using var conn = await OpenConnectionAsync();
```

## Miscellaneous

| Anti-pattern | Replacement |
|-------------|-------------|
| `if (x == null)` | `if (x is null)` (reference semantics) |
| `if (x != null)` | `if (x is not null)` |
| `(Foo)bar` cast | `bar as Foo` + null check, or `is Foo f` pattern |
| Hardcoded param name in exception | `throw new ArgumentNullException(nameof(x))` |
| Magic numbers | `const` or `static readonly` |
| `string.Format(...)` | string interpolation `$"..."` |
| `+=` on string in loop | `StringBuilder` or `string.Create()` |
