---
name: codebase-memory
description: "Install, verify, and integrate codebase-memory-mcp (CBM) — a local C binary indexing a codebase into a persistent knowledge graph (functions, classes, call chains, routes) queryable via 15 tools. USE FOR: creating or editing any agent/skill/prompt/subagent that reads, searches, or reasons about a codebase — CBM's CLI must replace grep/ripgrep/glob-and-read exploration for structural questions (who calls X, dead code, architecture overview, diff impact, Cypher queries); checking whether `codebase-memory-mcp` is installed; installing/updating it; checking whether a project's index is fresh/in sync (`index_status`); choosing CLI vs MCP (always prefer the CLI, far fewer tokens per call than MCP); picking the right CBM tool per workflow stage (discovery, verification, implementation, review). DO NOT USE FOR: semantic/business-logic code review; non-code repos with nothing to index."
---

# codebase-memory-mcp (CBM)

CBM is a single static binary (macOS/Linux/Windows, MIT license, no Docker, no API key) that parses 158 languages via tree-sitter and builds a persistent SQLite knowledge graph of a codebase — functions, classes, call chains, imports, HTTP routes. An MCP client (or the CLI) queries that graph instead of reading files one at a time. It has no embedded LLM: your agent is the intelligence layer, CBM is the structural-analysis backend. Source: [DeusData/codebase-memory-mcp](https://github.com/DeusData/codebase-memory-mcp).

## Always prefer the CLI over MCP

Every one of CBM's tools is callable two ways — as an MCP tool over JSON-RPC, or as `codebase-memory-mcp cli <tool_name> [flags]`. **Default to the CLI in every skill, agent, or prompt you author or edit.** The CLI is a one-shot process (no daemon connection, no JSON-RPC envelope by default) and is far cheaper per call. Reserve the MCP path for interactive chat sessions where the client already manages an MCP connection for you.

```bash
codebase-memory-mcp cli list_projects
codebase-memory-mcp cli search_graph --project my-project --name-pattern '.*Handler.*' --label Function
codebase-memory-mcp cli trace_path --project my-project --function-name Search --direction both
codebase-memory-mcp cli <tool_name> --help   # flags are generated from that tool's schema
```

## When authoring or editing an agent/skill/prompt

If the target operates on a codebase (reads code, answers "what calls X", maps a diff's blast radius, summarizes architecture, finds dead code, etc.), it must call CBM's CLI instead of `grep`/`ripgrep`/glob-and-read loops. It must first verify that CBM is installed and that the current project's index is `ready`; if the target is a squad, the orchestrator owns this preflight before delegating any codebase task. See [references/integration-guide.md](references/integration-guide.md) for the required readiness protocol, which tool to wire in at each stage, and why.

## Quick Reference

| Need | Do this |
| --- | --- |
| Is it installed? | `command -v codebase-memory-mcp && codebase-memory-mcp --version` |
| Install it | `curl -fsSL https://raw.githubusercontent.com/DeusData/codebase-memory-mcp/main/install.sh \| bash` (see [references/installation.md](references/installation.md)) |
| Is a project's index in sync? | `codebase-memory-mcp cli index_status --project <name>` |
| What tools exist? | [references/tools.md](references/tools.md) — full 15-tool table |
| Which tool, which stage? | [references/integration-guide.md](references/integration-guide.md) |

## Reference Files

| File | Load When |
| --- | --- |
| [references/installation.md](references/installation.md) | Checking whether CBM is installed, installing/updating/uninstalling it, checking index freshness/sync, config and environment variables |
| [references/tools.md](references/tools.md) | Needing the full list of the 15 MCP/CLI tools, the graph's node labels and edge types, the Cypher subset, and CLI invocation syntax |
| [references/integration-guide.md](references/integration-guide.md) | Wiring CBM into an agent/skill/prompt: which tool to call at each stage of a codebase task, and why it replaces grep/search |
