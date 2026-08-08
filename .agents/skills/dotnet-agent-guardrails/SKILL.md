---
name: dotnet-agent-guardrails
description: "Deterministic guardrails for defining the review/validation process in .NET/C# development agents and agent squads: .editorconfig + dotnet format, TreatWarningsAsErrors, custom Roslyn analyzers, full test suites, Aspire telemetry, and Copilot/Claude Code hooks as the gates a change must pass. USE FOR: defining what 'done' means and which deterministic checks gate an agent-generated .NET change before merge, for a single agent or a squad of agents/subagents sharing a repo. DO NOT USE FOR: general AGENTS.md authoring for non-.NET repos (agents-md); C# style rules (csharp-best-practices); non-agent solution scaffolding (dotnet-solution-setup); BenchmarkDotNet usage (dotnet-benchmarking)."
---

# .NET Development in the Code Agent Era

Code agents are non-deterministic — the same prompt can yield different code. The .NET toolchain is deterministic by design; wire it up as guardrails instead of trusting agent output alone.

## Anatomy

| File | Purpose |
|------|---------|
| `SKILL.md` | Guardrail overview + AGENTS.md wiring |
| [references/conventions-and-analyzers.md](references/conventions-and-analyzers.md) | `.editorconfig`, `dotnet format`, `TreatWarningsAsErrors`, custom Roslyn analyzer projects |
| [references/testing-contract.md](references/testing-contract.md) | Test tiers as the behavioral contract, agent failure-triage pattern |
| [references/aspire-agent-native.md](references/aspire-agent-native.md) | Aspire orchestration, telemetry, and agent-as-service integration |
| [references/hooks-governance.md](references/hooks-governance.md) | Hooks as a governance layer, hook categories, file-based C# scripts |

## The Six Guardrails

| Guardrail | Tool | Enforces | Config |
|-----------|------|----------|--------|
| Conventions | `dotnet format` + `.editorconfig` | style, naming, import order | `.editorconfig` |
| Compiler strictness | `TreatWarningsAsErrors` | nullability, unreachable code, API misuse | `Directory.Build.props` |
| Architecture | custom Roslyn analyzer project | domain/architecture rules with no built-in analyzer | `ProjectReference OutputItemType="Analyzer"` |
| Behavior | full test suite (unit/integration/e2e) | correctness, regressions | CI + AGENTS.md instruction |
| Runtime | Aspire AppHost | service wiring, config, telemetry | AppHost project |
| Actions | agent hooks | intercept/validate agent edits before they land | hook scripts (file-based C#) |

Each is deterministic: same input, same result, every run. That stability is what lets an agent self-correct across turns instead of drifting.

## AGENTS.md Wiring

Encode "done" in operational, checkable terms — not "write clean code":

```markdown
Run `dotnet format` to ensure code style compliance. Issues not automatically fixed
must be resolved manually. A feature is not complete until `dotnet format` exits
with code 0 and the full test suite passes.
```

## Directory.Build.props — Minimum

```xml
<PropertyGroup>
  <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
</PropertyGroup>
```

Set once in `Directory.Build.props` at the repo root so every project inherits it — see [dotnet-solution-setup](../../../apm_modules/netfabric/intelligentium/plugins/dotnet/.apm/skills/dotnet-solution-setup/SKILL.md) for the full shared-properties list.

## Practical Example

[BookStore](https://aalmada.github.io/BookStore/) applies all six guardrails (conventions, analyzers, tests, Aspire, hooks) in one open-source .NET solution.

## Reference Files

| File | Load When |
|------|-----------|
| [references/conventions-and-analyzers.md](references/conventions-and-analyzers.md) | Wiring `.editorconfig`/`dotnet format`/`TreatWarningsAsErrors`, or scaffolding a custom Roslyn analyzer project |
| [references/testing-contract.md](references/testing-contract.md) | Deciding test tiers, writing AGENTS.md test instructions, triaging agent-caused test failures |
| [references/aspire-agent-native.md](references/aspire-agent-native.md) | Using Aspire orchestration/telemetry so an agent can reason about a running distributed system |
| [references/hooks-governance.md](references/hooks-governance.md) | Writing or categorizing Copilot/Claude Code hooks that gate agent actions |
