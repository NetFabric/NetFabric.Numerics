# Reflection & Roslyn Tooling for Enumerables

Generic/analyzer code (source generators, serializers, custom LINQ-like libraries) often needs to detect "is this type enumerable?" and extract its element type and enumerator shape — including duck-typed enumerables that only implement the `GetEnumerator()`/`MoveNext()`/`Current` pattern without the `IEnumerable`/`IEnumerator` interfaces (valid for `foreach`, since the C# compiler binds by shape, not by interface, when a directly-callable `GetEnumerator()` exists).

## NetFabric.Reflection

Runtime reflection over an arbitrary `Type`:

```csharp
using NetFabric.Reflection;

if (type.IsEnumerable(out var enumerableInfo))
{
    // enumerableInfo exposes the resolved GetEnumerator/MoveNext/Current
    // members, whether they're interface-based or duck-typed, and the
    // element type — even for ref-struct enumerators reflection can't
    // instantiate directly.
}

if (type.IsAsyncEnumerable(out var asyncInfo)) { /* IAsyncEnumerable-shaped types */ }
```

- `NetFabric.Expressions.ExpressionEx` (in the same package) builds `Expression` trees matching what Roslyn itself emits for the equivalent C# keyword — not just `foreach`:

| Method | Roslyn-equivalent to | Notes |
|---|---|---|
| `ExpressionEx.ForEach(enumerable, body)` | `foreach` | Shape (interface/class/struct/ref struct enumerator, disposable or not, array indexer vs `IEnumerable<>`) is resolved the same way `IsEnumerable` resolves it — no boxing for value-type enumerators |
| `ExpressionEx.For(init, condition, iterator, body)` | `for` | Doesn't declare the loop variable — wrap in `Expression.Block` with your own `ParameterExpression` |
| `ExpressionEx.While(condition, body)` | `while` | Plain `LoopExpression`; compose with `ExpressionEx.ForEach`'s internals when hand-rolling an enumerator-driven loop |
| `ExpressionEx.Using(instance, body)` | `using` | Requires `IDisposable` (class/struct) or a public parameterless `Dispose()` (`ref struct`); `IAsyncDisposable` isn't supported |

Use these when a source generator or runtime-codegen library (serializer, mapper) needs to emit iteration code without boxing a value-type enumerator to an interface — the same problem covered in [iteration-performance.md](iteration-performance.md), solved at the expression-tree level instead of by writing C# directly.
- `NetFabric.Assertive` adds test/assertion helpers that understand enumerable shapes (e.g. asserting two enumerables yield equal sequences without forcing `ToList()` first).

## NetFabric.CodeAnalysis (Roslyn)

Compile-time equivalent for source generators/analyzers, operating on `ITypeSymbol` instead of `Type`:

```csharp
using NetFabric.CodeAnalysis;

if (typeSymbol.IsEnumerable(compilation, out var enumerableSymbols))
{
    // enumerableSymbols exposes IMethodSymbol/IPropertySymbol for
    // GetEnumerator/MoveNext/Current, resolved the same way the C#
    // binder would (duck-typed first, interface-based fallback).
}

typeSymbol.IsAsyncEnumerable(compilation, out var asyncEnumerableSymbols);
```

Use this inside a Roslyn analyzer or source generator to validate/generate `foreach`-compatible code paths without instantiating any object — critical for analyzers, which only ever see symbols, never runtime instances.

## When to Reach for These

| Scenario | Tool |
|---|---|
| Runtime code (serializer, mapper) needs to iterate an arbitrary object without knowing its type ahead of time | `NetFabric.Reflection` |
| Source generator/analyzer needs to know if a type is enumerable, at compile time, from a symbol | `NetFabric.CodeAnalysis` |
| Want IDE warnings for the common LINQ performance traps in this skill | `NetFabric.Hyperlinq.Analyzer` |
| Need a `foreach`/`for`/`while`/`using`-equivalent expression tree generated dynamically | `ExpressionEx.ForEach`/`.For`/`.While`/`.Using` (`NetFabric.Reflection`) |
