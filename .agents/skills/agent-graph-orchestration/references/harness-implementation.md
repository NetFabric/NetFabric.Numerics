# Harness Implementation Strategies

A designed graph (nodes, edges, topology) executes differently depending on whether the target harness has a native multi-agent graph runtime or not. This skill only produces the design — always delegate node authoring to the harness's own agent-authoring skill.

## Always ask first, never assume

Before scaffolding a single file, ask the user which harness the generated squad/swarm must target. Do not infer it from whatever tool is currently running this skill — a workspace's active assistant and its target deployment harness are frequently different (e.g. designing a squad while using one assistant, but shipping it for a teammate who runs Copilot CLI). If the user names a harness with no known agent-authoring skill installed, say so explicitly rather than silently defaulting to Copilot CLI.

| Question to ask | Why it matters |
| --- | --- |
| Which harness will run this squad? (Copilot CLI, Claude Code, Strands Agents SDK, LangGraph, ...) | Determines native-graph vs. orchestrator+delegation (below) and which agent-authoring skill to invoke |
| Is there an existing agent-authoring skill installed for that harness? | If none exists, say so instead of guessing at a file format |

## Native-graph harnesses

| Harness | Graph construct | Node authoring |
| --- | --- | --- |
| Strands Agents SDK | `Graph({ nodes, edges, sources, maxSteps, ... })` — code-defined, executes deterministically | Nodes are `Agent`/`AgentBase` instances constructed in code; no separate agent-authoring skill needed |
| LangGraph | `StateGraph` with `add_node`/`add_edge` | Nodes are Python/TS callables or LLM-backed runnables defined in code |

For these, the graph *is* the implementation — write the node objects and edges directly; this skill's design output maps almost 1:1 onto the constructor call.

## No-native-graph harnesses: orchestrator + delegation

Copilot CLI, Claude Code, and most chat-first coding-agent harnesses have no `Graph` class. Realize the topology as:

1. One **orchestrator/coordinator custom agent** whose prompt body encodes the edge order and any conditions in plain instructions (see [scaffolding-workflow.md](scaffolding-workflow.md))
2. One **specialist custom agent per node**, authored via that harness's agent-authoring skill
3. The orchestrator dispatches nodes via that harness's subagent-dispatch tool, in the order the edges require, passing each node's output forward explicitly in the next node's prompt

### Copilot CLI equivalents

| Graph concept | Copilot CLI equivalent |
| --- | --- |
| Node | A custom agent (`.agent.md`), authored via `copilot-cli-custom-agents` |
| Edge | An instruction in the orchestrator's prompt body: "after X completes, dispatch to Y with X's output" |
| Source | The orchestrator itself receives the user's task first |
| AND join | Orchestrator instruction: "wait for tasks A, B, and C to all return before dispatching to D" |
| Conditional edge | Orchestrator instruction: "if A's result mentions `<condition>`, dispatch to B; otherwise dispatch to C" |
| Cycle + bound | Orchestrator instruction with an explicit max-iteration count (Copilot CLI has no orchestrator-level `maxSteps`) |
| Nested graph/swarm as node | A specialist agent that is itself a mini orchestrator delegating to its own sub-specialists — e.g. a tribunal review node (see [topologies.md](topologies.md#5-tribunal-review-adversarial-cross-provider)) |

### Claude Code equivalents

| Graph concept | Claude Code equivalent |
| --- | --- |
| Node | A subagent (`.claude/agents/*.md`), authored via `claude-code-custom-agents` |
| Edge | An instruction in the coordinator's prompt body dispatching via the `Agent` tool: "after X completes, dispatch to Y with X's output" |
| Source | The coordinator itself receives the user's task first |
| AND join | Coordinator instruction: "wait for A, B, and C to all return before dispatching to D" |
| Conditional edge | Coordinator instruction: "if A's result mentions `<condition>`, dispatch to B; otherwise dispatch to C" |
| Cycle + bound | Coordinator instruction with an explicit max-iteration count — Claude Code has no orchestrator-level cycle-count field either; `maxTurns` only bounds a single subagent invocation's own turns, not how many times the coordinator re-dispatches to it |
| Nested graph/swarm as node | A subagent that is itself a coordinator delegating to its own children, up to the nesting depth limit (`CLAUDE_CODE_MAX_SUBAGENT_SPAWN_DEPTH`, default 3) |

### Copilot CLI vs. Claude Code: what doesn't carry over

| Aspect | Copilot CLI | Claude Code |
| --- | --- | --- |
| Dispatch tool | `task(agent_type=..., prompt=...)` | `Agent` tool (renamed from `Task` in v2.1.63) |
| What identifies a node for dispatch | The **filename** (minus extension) — `name:` is only an optional cosmetic display label shown in the `/agent` picker | The frontmatter **`name:`** field itself (required) — this is what `Agent(...)`/hooks match on; the filename is cosmetic and doesn't have to match |
| Restricting which nodes a coordinator may dispatch | Convention only — no enforced allowlist | `tools: Agent(node-a, node-b)` on the coordinator is an actually-enforced allowlist |
| Hiding internal nodes from users | `user-invocable: false` (hides from the `/agent` picker, stays dispatchable) | No equivalent field — every subagent is `@`-mentionable by any user; rely on the `Agent(...)` allowlist plus naming/description convention instead |
| Default execution | Foreground | Background by default (v2.1.198+), with a reduced tool set (see `claude-code-custom-agents` → undocumented-and-gotchas.md) |

Because Claude Code has no field equivalent to `user-invocable`, the "only the orchestrator is user-selectable" requirement (see [scaffolding-workflow.md](scaffolding-workflow.md)) can only be approximated there, not enforced — say this explicitly rather than implying parity with Copilot CLI. And because `name:` carries the dispatch identity on Claude Code (unlike Copilot CLI, where the filename does), every node's `name:` must itself be the prefixed kebab-case squad ID (e.g. `name: <squad>-orchestrator`) — never a free-form human-friendly title there.

For each node, delegate authoring by harness — never hand-write frontmatter/prompt bodies directly.

## Delegating node authoring by harness

| Target harness | Agent-authoring skill to invoke per node |
| --- | --- |
| GitHub Copilot CLI | `copilot-cli-custom-agents` — produces `.agent.md` files |
| Claude Code | `claude-code-custom-agents` — produces `.claude/agents/*.md` subagent files |
| Any harness with its own agent-file format | That harness's equivalent agent-authoring skill, if one exists in the installed marketplace |

Never hand-write a node's frontmatter/prompt body directly in this skill's workflow — always call the target skill so its frontmatter rules, tool restrictions, and writing-style guidance are applied consistently.
