---
name: csharp-best-practices
description: "Modern .NET and C# best practices for code quality, performance, and correctness. USE FOR: choosing struct/class/record/readonly-record-struct; nullable reference types; pattern matching; primary constructors; collection expressions; required members; init-only properties; file-scoped namespaces; global usings; async/await patterns; ConfigureAwait; ValueTask vs Task; CancellationToken propagation; IAsyncEnumerable; Span<T>/Memory<T> usage; avoiding boxing; readonly struct; ArrayPool<T>; FrozenDictionary/FrozenSet; LINQ vs loops in hot paths; ArgumentNullException.ThrowIfNull; ThrowIfNegative; ThrowIfZero; IReadOnlyList return types; error handling patterns; GeneratedRegex; LoggerMessage; source generators for regex and logging; avoiding Math/MathF in favor of static methods on the numeric type. DO NOT USE FOR: generic math interfaces (use dotnet-generic-math); ASP.NET Core middleware; EF Core modeling."
---

# C# Best Practices

Targets .NET 8+ / C# 12+ unless noted.

## Type Selection

| Scenario | Preferred type |
|----------|---------------|
| Small immutable value (<4 fields) | `readonly record struct` |
| Value type, mutated in place | `struct` (avoid) or `ref struct` |
| Mutable reference type | `class` |
| Immutable data transfer | `record class` |
| Stack-only lifetime | `ref struct` |

## Language Features Quick-Ref

| Feature | Version | Note |
|---------|---------|------|
| `required` members | C# 11 | enforce init in object initializer |
| Primary constructors | C# 12 | classes & structs; params are in-scope everywhere |
| Collection expressions `[1, 2, 3]` | C# 12 | replaces `new List<>`, `new[]`, `Array.Empty<>` |
| Raw string literals `"""..."""` | C# 11 | no escape sequences needed |
| Pattern matching (`switch` expr) | C# 8+ | prefer over `if/else` chains on type/value |
| `is` type patterns | C# 9 | `if (x is Foo { Bar: > 0 } f)` |
| `nameof()` | C# 6 | always use for param names in exceptions |
| File-scoped namespace | C# 10 | `namespace Foo;` — one per file |
| Global usings | C# 10 | put in `GlobalUsings.cs` |
| `init`-only setters | C# 9 | immutable after construction |

Full examples → [references/language-features.md](references/language-features.md)

## Nullability

- Enable project-wide: `<Nullable>enable</Nullable>` in `.csproj`
- Use `!` only when null is provably impossible; prefer null checks
- `ArgumentNullException.ThrowIfNull(param)` — validates & throws in one line
- Return `T?` from "not found" lookups; never return `null` for empty collections (return empty)

## Error Handling

| Validator | .NET version |
|-----------|-------------|
| `ArgumentNullException.ThrowIfNull(x)` | .NET 6 |
| `ArgumentOutOfRangeException.ThrowIfNegative(x)` | .NET 8 |
| `ArgumentOutOfRangeException.ThrowIfZero(x)` | .NET 8 |
| `ArgumentOutOfRangeException.ThrowIfGreaterThan(x, max)` | .NET 8 |
| `ObjectDisposedException.ThrowIf(condition, this)` | .NET 7 |

## Source Generators

| Pattern | Attribute | Benefit |
|---------|-----------|--------|
| Regex | `[GeneratedRegex("...")]` on `static partial` method | Compile-time; no runtime compilation; faster |
| Logging | `[LoggerMessage(...)]` on `static partial` method | Zero-alloc structured logging; no boxing |

Full examples → [references/source-generators.md](references/source-generators.md)

## API Design

- Return `IReadOnlyList<T>` / `IReadOnlyDictionary<K,V>` for collections
- Accept `IEnumerable<T>` for input (widest contract)
- Accept `ReadOnlySpan<T>` overloads for hot paths
- Use `CancellationToken` as **last** parameter; default to `default`

## Math APIs

- Never call `Math.*` or `MathF.*` — use the equivalent static method on the operand's own type instead (`double.Sqrt(x)`, `float.Pow(x, y)`, `int.Abs(x)`), available since .NET 7 via `INumber<T>`/`IFloatingPointIeee754<T>`
- Same rule applies generically: `T.Sqrt(x)` when `T : IRootFunctions<T>` — avoids picking the wrong precision and works uniformly across `float`/`double`/custom numeric types
- Full generic-math interface usage (`INumber<T>`, `IFloatingPointIeee754<T>`, etc.) → use the `dotnet-generic-math` skill

## Reference Files

| File | Load When |
|------|-----------|
| [references/language-features.md](references/language-features.md) | Records, pattern matching, primary constructors, collection expressions, raw strings |
| [references/performance.md](references/performance.md) | Span, Memory, ArrayPool, stackalloc, boxing, FrozenDictionary, LINQ vs loops |
| [references/async.md](references/async.md) | async/await, ValueTask, ConfigureAwait, IAsyncEnumerable, CancellationToken |
| [references/source-generators.md](references/source-generators.md) | GeneratedRegex, LoggerMessage |
