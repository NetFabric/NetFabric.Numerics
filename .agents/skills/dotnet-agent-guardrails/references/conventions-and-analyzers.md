# Conventions & Analyzers

## .editorconfig + dotnet format

`.editorconfig` defines indentation, spacing, naming conventions, `using` ordering, and analyzer severities — enforceable identically by every editor, IDE, and CLI. It removes the ambiguity that produces inconsistent agent output.

[`dotnet format`](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format) applies the rules in `.editorconfig` across the whole solution:

```bash
dotnet format               # fix whitespace, style, and analyzer-fixable issues
dotnet format --verify-no-changes   # CI check: fails if anything would change
```

- Anything it can't auto-fix surfaces as a diagnostic that must be resolved manually.
- Exit code `0` means the codebase fully complies with the conventions — use this as the literal "done" signal in AGENTS.md/CLAUDE.md.
- Run `dotnet format --verify-no-changes` in CI so drift never merges.

## Warnings as Errors

[`TreatWarningsAsErrors`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/errors-warnings#treatwarningsaserrors) turns every compiler warning into a build failure. Set it once, repo-wide, in `Directory.Build.props`:

```xml
<PropertyGroup>
  <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
</PropertyGroup>
```

`Directory.Build.props` centralizes MSBuild properties for every project under the directory tree — one place instead of repeating settings across dozens of `.csproj` files. Full property list (Nullable, ImplicitUsings, EnforceCodeStyleInBuild, EnableNETAnalyzers, etc.) → [dotnet-solution-setup/references/directory-build-props.md](../../../../apm_modules/netfabric/intelligentium/plugins/dotnet/.apm/skills/dotnet-solution-setup/references/directory-build-props.md).

Why it matters for agents: a permissive compiler leaves room for interpretation; a strict one reports nullability, unreachable code, unused variables, and API misuse the same way every time. That determinism is what an agent (or reviewer) can react to reliably.

## Roslyn Analyzers as Architecture Enforcement

[Roslyn analyzers](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/overview?tabs=net-10) run at compile time and report diagnostics when code violates a rule — deterministic, since the same code always produces the same diagnostics.

- The .NET SDK ships built-in analyzers for correctness/performance/style; NuGet adds framework-specific ones (ASP.NET Core, EF Core, security).
- When neither covers a project-specific architecture or domain constraint, write a custom analyzer.

**Scaffolding a custom analyzer project:**

1. Create a standard C# class library referencing the Roslyn SDK (`Microsoft.CodeAnalysis.CSharp`, `Microsoft.CodeAnalysis.Analyzers`).
2. Implement rules against syntax trees or the semantic model.
3. Reference it from the analyzed project as an analyzer, not a runtime dependency:

```xml
<ProjectReference Include="..\BookStore.ApiService.Analyzers\BookStore.ApiService.Analyzers.csproj"
                   OutputItemType="Analyzer"
                   ReferenceOutputAssembly="false" />
```

`OutputItemType="Analyzer"` + `ReferenceOutputAssembly="false"` tells MSBuild to load the analyzer during compilation without adding a runtime reference to its assembly.

Once wired, an agent that introduces a violation fails the build on every subsequent compile until the code is fixed — the analyzer enforces the architecture rather than relying on the agent remembering it.

### Agents Can Help Build Analyzers

Agents can propose rule definitions from recurring codebase patterns, scaffold the initial analyzer + code-fix, refine rules against failing diagnostics, and update analyzers as the architecture evolves. This is a feedback loop: analyzers guide agents, agents help maintain analyzers.
