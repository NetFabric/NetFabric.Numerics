---
name: agent-graph-orchestration
description: "Design and scaffold multi-agent squads/swarms using the graph orchestration pattern (nodes = agents, edges = dependencies/data flow). USE FOR: designing a coordination topology for a multi-agent squad (sequential pipeline, parallel fan-out/aggregation, branching, feedback loop, cyclic); building an orchestrator agent that delegates to specialist agents; setting up an adversarial/tribunal code or document review with independent reviewers; scaffolding a squad's agent files for a target coding-agent harness (e.g. Copilot CLI or Claude Code custom agents). DO NOT USE FOR: authoring a single agent's frontmatter/prompt body (use copilot-cli-custom-agents, claude-code-custom-agents, or the target harness's own skill); SKILL.md authoring (use create-skill); ad-hoc multi-agent chat with no explicit topology."
---

# Agent Graph Orchestration

A graph-pattern multi-agent system is a directed graph where nodes are agents (or nested graphs/swarms) and edges are dependencies and data flow. Execution follows edge order — output from one node becomes input for its dependents. This skill designs that graph and delegates each node's actual authoring to the target harness's own agent-authoring skill; it never hand-writes agent files itself.

## Anatomy

| Concept | Role |
| --- | --- |
| Node | An agent, a deterministic custom step, or a nested graph/swarm |
| Edge | `[source, target]` dependency, optionally with a condition for dynamic routing |
| Source | Entry node(s) receiving the original task; auto-detected as nodes with no incoming edges |
| Topology | Overall shape — sequential, parallel+aggregate, branching, feedback loop, cyclic |

A project can host more than one squad: prefix every node ID, display name, and any shared resource the squad declares (custom MCP-server keys, shared session files) with a project-unique `<squad>` string. Keep only the orchestrator user-selectable wherever the harness supports that distinction (e.g. `user-invocable: false` on Copilot CLI); the exact mechanism — and its limits — is harness-specific → [references/harness-implementation.md](references/harness-implementation.md).

## Core topologies (quick reference)

| Topology | Shape | Use when |
| --- | --- | --- |
| Sequential pipeline | A → B → C | Each stage strictly needs the prior stage's output |
| Parallel fan-out + aggregation | Coordinator → {W1, W2, W3} → Aggregator | Independent subtasks can run concurrently, then merge |
| Branching | Classifier → conditional edge → {branch A, branch B} | Routing depends on runtime content (AND/OR edge semantics vary by SDK) |
| Feedback loop | Writer ↔ Reviewer → Publisher | Iterative refinement; must bound iterations (cycle limit) |
| Tribunal review | Implementer → review-orchestrator → {Reviewer A, Reviewer B} → merged findings | A review step needs adversarial, cross-provider independence instead of a single reviewer agent |

Full topology diagrams, conditional-edge semantics, and cycle-safety rules → [references/topologies.md](references/topologies.md).

## Workflow

1. **Ask the user which harness to target** before designing or scaffolding anything — never assume from the current session's own tool → [references/harness-implementation.md](references/harness-implementation.md)
2. **Design the graph** — list nodes, edges, topology, entry points, and cycle bounds → [references/topologies.md](references/topologies.md)
3. **Ground planning/coordination tradeoffs** in agent-coordination-graph (ACG) and task-dependency-graph (TDG) theory when the topology isn't obvious → [references/graph-concepts.md](references/graph-concepts.md)
4. **Pick how the graph executes** for the answered harness — native `Graph` construct vs. orchestrator + delegation → [references/harness-implementation.md](references/harness-implementation.md)
5. **Scaffold every node by delegating**, never hand-authoring: Copilot CLI nodes → invoke `copilot-cli-custom-agents` for each `.agent.md`; Claude Code nodes → invoke `claude-code-custom-agents` for each `.claude/agents/*.md` subagent; either way, prefix with the squad's name, keep least-privilege `tools:`, and hide internal nodes from users as best the harness allows (`user-invocable: false` on Copilot CLI; convention-only on Claude Code); other harnesses → their own agent-authoring skill → [references/scaffolding-workflow.md](references/scaffolding-workflow.md)
6. **Pick each node's model** by call cadence vs. provider prompt-cache TTL, or by cross-provider diversity for tribunal reviewers → [references/model-selection.md](references/model-selection.md)
7. **Validate**: graph is acyclic unless a feedback loop is intentional (and bounded); every conditional edge's branches are exhaustive; every delegation prompt is self-contained since subagents start with empty context; tribunal reviewers resolve to genuinely different providers (or model families, on harnesses locked to one vendor); only the orchestrator is user-selectable to whatever degree the harness allows it (e.g. absent from Copilot CLI's `/agent` picker); no resource name collides with another squad in the project
8. **Run `markdown-best-practices`** over every generated Markdown artifact (shared plan files, design notes) before finishing — the delegated agent files are already covered by their own harness skill's workflow

## Reference Files

| File | Load When |
| --- | --- |
| [references/graph-concepts.md](references/graph-concepts.md) | Grounding a topology choice in agent-coordination-graph (ACG) / task-dependency-graph (TDG) theory, or explaining why the graph pattern helps planning, execution, memory, or coordination |
| [references/topologies.md](references/topologies.md) | Designing nodes/edges, entry points, conditional routing, AND/OR dependency semantics, bounding a cyclic graph, or modeling a review node as an adversarial tribunal |
| [references/harness-implementation.md](references/harness-implementation.md) | Confirming which harness to target before doing anything else, or deciding how the designed graph actually executes for that harness (native SDK `Graph` vs. orchestrator + `task` delegation) |
| [references/scaffolding-workflow.md](references/scaffolding-workflow.md) | Turning a graph design into concrete agent files by delegating to `copilot-cli-custom-agents` or `claude-code-custom-agents` (or another harness's agent-authoring skill); applying the squad naming prefix and least-privilege/visibility rules |
| [references/model-selection.md](references/model-selection.md) | Choosing each node's `model:` based on how often it's called and its provider's prompt-cache TTL, or by cross-provider diversity for tribunal reviewers |
