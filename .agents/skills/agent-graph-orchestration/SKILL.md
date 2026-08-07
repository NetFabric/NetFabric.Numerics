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

## Mandatory squad shape

Every squad this skill scaffolds — whatever topology its implementation stages take below — must include three roles: an **orchestrator**, a **planner** (decomposes the task into the stage-specific nodes/edges before any implementation work runs), and an **adversarial review** stage (the tribunal pattern, not a single reviewer agent). The stage between planner and reviewer may be one **implementer** agent or several; dispatch independent implementer subtasks in parallel rather than serially (see [Core topologies](#core-topologies-quick-reference) below), only serializing them when one genuinely depends on another's output. A **quality gate** node is optional but common: when present it runs between the last implementation node and the adversarial reviewer and validates *deterministically* — executing tools (tests, linters, schema/build checks) that spend no model tokens — while the adversarial reviewer validates by *reasoning* over the artifact, which does spend tokens. A **publisher** node is also optional: when present it runs after the adversarial review passes and *acts* on the approved artifact rather than validating it — opening a pull request, updating a tracking ticket (Jira, GitHub Issues, ...), publishing/deploying to an environment, or a combination. Full baseline shape and the gate/reviewer/publisher distinctions → [references/topologies.md](references/topologies.md#mandatory-baseline-squad-shape).

## Core topologies (quick reference)

| Topology | Shape | Use when |
| --- | --- | --- |
| Sequential pipeline | A → B → C | Each stage strictly needs the prior stage's output |
| Parallel fan-out + aggregation | Coordinator → {W1, W2, W3} → Aggregator | Independent subtasks can run concurrently, then merge (AND/OR join semantics vary by SDK — always require AND explicitly) |
| Branching | Classifier → conditional edge → {branch A, branch B} | Routing depends on runtime content; branches must be exhaustive or the graph stalls with no fired successor |
| Feedback loop | Writer ↔ Reviewer → Publisher | Iterative refinement; must bound iterations (cycle limit) |
| Tribunal review | Implementer → review-orchestrator → {Reviewer A, Reviewer B} → merged findings | A review step needs adversarial, cross-provider independence instead of a single reviewer agent |
| Quality gate + adversarial review | Implementer → Quality Gate (deterministic, no tokens) → Tribunal Review (reasoning, tokens) → Publisher | An implementation node's output needs cheap mechanical checks (tests/lint/build) run before spending reasoning tokens on adversarial review |

Full topology diagrams, conditional-edge semantics, and cycle-safety rules → [references/topologies.md](references/topologies.md).

## Workflow

1. **Interview the user before designing or scaffolding anything** — never assume any of these from the current session's own tool or from what seems "obvious" for the task; ask them in order and wait for real answers:
   - Which harness must the squad target? → [references/harness-implementation.md](references/harness-implementation.md#always-ask-first-never-assume)
   - Should the squad include a **quality gate**, and if so, what should it check? → [references/topologies.md](references/topologies.md#mandatory-baseline-squad-shape)
   - Should the squad include a **publisher**, and if so, what should it do? → [references/topologies.md](references/topologies.md#mandatory-baseline-squad-shape)
2. **Propose a graph design from the answers, then stop and get explicit approval before proceeding further** — start from the mandatory orchestrator + planner + adversarial-review baseline shape, include (or omit) the quality gate/publisher exactly per the user's answers, then layer in the task-specific nodes, edges, topology, entry points, and cycle bounds; present the proposed graph (nodes, edges, which optional roles are included and what they do) back to the user and wait for their confirmation — do not move on to harness-execution strategy, scaffolding, or model selection until they approve or request changes → [references/topologies.md](references/topologies.md)
3. **Ground planning/coordination tradeoffs** in agent-coordination-graph (ACG) and task-dependency-graph (TDG) theory when the topology isn't obvious → [references/graph-concepts.md](references/graph-concepts.md)
4. **Pick how the graph executes** for the answered harness — native `Graph` construct vs. orchestrator + delegation → [references/harness-implementation.md](references/harness-implementation.md)
5. **Realize every node** per the step-4 answer: for a **native-graph harness** (Strands Agents SDK, LangGraph), write each node directly as code (`Agent`/`AgentBase` instances or callables) — there is no agent-authoring skill to delegate to and no `.agent.md`-style file to scaffold; for a **no-native-graph harness** (Copilot CLI, Claude Code, VS Code custom agents, or another chat-first harness), scaffold every node by delegating, never hand-authoring: Copilot CLI nodes → invoke `copilot-cli-custom-agents` for each `.agent.md`; Claude Code nodes → invoke `claude-code-custom-agents` for each `.claude/agents/*.md` subagent; other harnesses → their own agent-authoring skill if one exists, otherwise say so explicitly rather than reusing a mismatched one (e.g. VS Code custom agents share the `.agent.md` extension with Copilot CLI but are a distinct target with no dedicated skill yet). Either way, prefix with the squad's name, keep least-privilege `tools:`, and hide internal nodes from users as best the harness allows (`user-invocable: false` on Copilot CLI; convention-only on Claude Code). The mandatory planner is authored the same way as any other specialist, and so is an optional publisher node (open a PR, update a tracking ticket, publish/deploy) when the design includes one. A quality gate node is the one exception on any harness — it's a deterministic tool-execution step, not an LLM agent, so don't delegate reasoning/prompt content for it → [references/scaffolding-workflow.md](references/scaffolding-workflow.md)
6. **Pick each node's model** — mandatory for every node, not an optional optimization: use the `model-selection` skill for the generic mechanics (capability tier, call cadence vs. provider prompt-cache TTL, cost-tier ranking, per-harness field syntax and fallback-list support), then apply this squad's own overlay — the orchestrator's model is the squad's cost ceiling (Copilot CLI: no other node's cost tier may exceed it) and tribunal reviewers need cross-provider diversity → [references/model-selection.md](references/model-selection.md)
7. **Validate**: every squad includes an orchestrator, a planner, and an adversarial (tribunal) review stage; any quality gate present runs deterministically (no reasoning/tokens) before, not instead of, the adversarial reviewer; any publisher present only runs after the quality gate and adversarial review both pass; every node has an explicit, harness-appropriate `model:` (on Copilot CLI, no node's cost tier exceeds the orchestrator's, aside from a deliberately surfaced tribunal-diversity exception); graph is acyclic unless a feedback loop is intentional (and bounded); every conditional edge's branches are exhaustive; every delegation prompt is self-contained since subagents start with empty context; tribunal reviewers resolve to genuinely different providers (or model families, on harnesses locked to one vendor); only the orchestrator is user-selectable to whatever degree the harness allows it (e.g. absent from Copilot CLI's `/agent` picker); no resource name collides with another squad in the project
8. **Run `markdown-best-practices`** over every generated Markdown artifact (shared plan files, design notes) before finishing — the delegated agent files are already covered by their own harness skill's workflow

## Reference Files

| File | Load When |
| --- | --- |
| [references/graph-concepts.md](references/graph-concepts.md) | Grounding a topology choice in agent-coordination-graph (ACG) / task-dependency-graph (TDG) theory, or explaining why the graph pattern helps planning, execution, memory, or coordination |
| [references/topologies.md](references/topologies.md) | Designing nodes/edges, entry points, conditional routing, AND/OR dependency semantics, bounding a cyclic graph, modeling a review node as an adversarial tribunal, or confirming the mandatory orchestrator + planner + adversarial-review baseline shape and where an optional deterministic quality gate or optional publisher node fits |
| [references/harness-implementation.md](references/harness-implementation.md) | Confirming which harness to target before doing anything else, or deciding how the designed graph actually executes for that harness (native SDK `Graph` vs. orchestrator + `task` delegation) |
| [references/scaffolding-workflow.md](references/scaffolding-workflow.md) | Turning a graph design into concrete agent files by delegating to `copilot-cli-custom-agents` or `claude-code-custom-agents` (or another harness's agent-authoring skill); scaffolding the mandatory planner node, an optional deterministic quality gate node, or an optional publisher node (PR/ticket/deploy actions); applying the squad naming prefix and least-privilege/visibility rules |
| [references/model-selection.md](references/model-selection.md) | The squad-specific overlay on top of the `model-selection` skill: mapping node roles (orchestrator, worker, reviewer-in-loop, rare branch) to call cadence, assigning cross-provider diversity across tribunal reviewers, and applying the Copilot CLI cost ceiling relative to the orchestrator |
