# Hooks as the Governance Layer

[Hooks](https://docs.github.com/en/copilot/how-tos/copilot-cli/customize-copilot/use-hooks) let engineering workflows intercept agent actions, enforce rules, and collect telemetry — a programmable layer on top of the deterministic build-time guardrails (analyzers, `dotnet format`, tests).

## What Hooks Enforce

Hooks run scripts that validate behavior, enforce conventions, or gate a step before letting an agent continue. Implement them with [file-based C#](https://learn.microsoft.com/en-us/dotnet/core/sdk/file-based-apps) — no project scaffolding, run directly with `dotnet run script.cs`, easy to version and evolve alongside the rules they enforce.

## Hook Categories

| Category | Purpose |
|----------|---------|
| Governance | ensure generated code respects conventions, analyzers, and architectural boundaries |
| Telemetry | insight into how agents are used and where they struggle |
| Security | prevent unsafe patterns or accidental secret/data leaks |
| Workflow | integrate agents into formatting, testing, documentation, and other automated steps |
| Domain-specific | inject business vocabulary and invariants into the agent's reasoning |

## Division of Labor: Hooks vs Aspire

Hooks provide deterministic enforcement at **development time** (before/during an agent's edit). Aspire provides deterministic orchestration at **runtime** (once the system is running). Together they bound agent behavior across the whole lifecycle, not just at the point of code generation.

## Agents Can Help Build Hooks

Agents can propose hook logic from recurring codebase patterns, generate the initial file-based C# script, refine a script when it starts flagging false positives/negatives, and evolve hook behavior as conventions change — the same guide/maintain feedback loop analyzers have with agents (see [conventions-and-analyzers.md](conventions-and-analyzers.md)).
