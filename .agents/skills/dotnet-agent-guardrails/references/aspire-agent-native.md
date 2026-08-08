# Aspire as the Agent-Native Application Model

[Aspire](https://aspire.dev/) is a model for building and running distributed applications. Though it started .NET-focused, it now orchestrates polyglot systems — giving an agent one consistent view of an entire system, not just the .NET parts.

## Capabilities That Matter for Agents

| Capability | What it gives the agent |
|-------------|--------------------------|
| Service orchestration | services always start, configure, and wire together the same way — a stable environment to reason about |
| Service discovery & connection management | Aspire wires connection strings, URLs, ports, credentials automatically — removes a whole class of manual-config errors |
| Unified configuration | one structured place to read/update config, instead of guessing where a value comes from |
| Observability & telemetry | logs, metrics, traces, dependency maps out of the box |
| Local-first development | the full distributed system runs locally with full fidelity — no cloud dependency to validate a change |
| Agent integration | agents are first-class services: they appear in the dependency graph, receive configuration, and are observable like any other service |

## Why Telemetry Access Reduces Token Usage

Instead of inferring system behavior by reading source, an agent with Aspire telemetry can inspect traces, analyze logs, view the dependency graph, check metrics, and detect failures directly. This cuts the amount of source code that must be loaded into context just to understand what a running system is doing — precise, low-cost signal instead of speculative code reading.

## Why Local-First Matters

Running the entire system locally (services, dependencies, and all) lets an agent run it, observe behavior, run tests, inspect logs, and validate changes without provisioning cloud infrastructure per iteration. Local determinism means every run produces the same signals, which is what makes agent self-correction reliable across turns.

## Agent Integration

Aspire treats agents as first-class services: they participate in the dependency graph, receive configuration the same way other services do, and show up in observability/governance alongside APIs and databases they call.
