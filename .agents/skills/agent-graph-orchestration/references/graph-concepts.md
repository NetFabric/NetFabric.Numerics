# Graph Concepts for Multi-Agent Systems

Grounded in "Graphs Meet AI Agents" (arXiv:2506.18019): graphs organize agent planning, execution, memory, and coordination by representing entities as nodes and their relationships as edges — structurization that lets agents reason over explicit dependencies instead of unstructured text.

## Task Dependency Graph (TDG)

Used at design time to decompose a complex task into subtasks with dependencies.

| Element | Meaning |
| --- | --- |
| Node | A subtask decomposed from the overall task |
| Edge | A dependency — one subtask's output feeds another's input |
| Shape | Typically a DAG, so no subtask waits on its own (indirect) result |

A TDG is the blueprint for graph topology: turn each subtask node into an agent node, and each dependency edge into a graph edge.

## Agent Coordination Graph (ACG)

Used at runtime to model how multiple agents communicate once the TDG (or another topology) is chosen.

| Element | Meaning |
| --- | --- |
| Node | An agent (its features/role) |
| Edge | A communication path — who passes messages/results to whom |
| Task-specific relationship | Edges mirror the TDG's subtask dependencies (this skill's default) |
| Environment-specific relationship | Edges mirror a shared environment's structure (e.g., agents near each other in a simulated space) rather than task dependencies |

## Why graphs over ad-hoc multi-agent chat

| Capability | Without a graph | With a graph |
| --- | --- | --- |
| Planning | Implicit ordering inferred per turn | Explicit dependency order, reusable across runs |
| Execution | Tool/agent calls interleaved unpredictably | Deterministic dispatch order from edges |
| Memory | Flat conversation history | Structured per-node results addressable by node ID |
| Coordination | Every agent sees every message | Each node only receives what its edges deliver |

## DAG vs. cyclic graphs

Most TDGs are acyclic (a subtask can't depend on its own output). Cyclic graphs are valid for iterative patterns (draft → review → revise) but require an explicit exit condition and an iteration bound — an unbounded cyclic graph can run indefinitely. See [topologies.md](topologies.md#4-feedback-loop-cyclic) for the bounding mechanism.

## When topology optimization matters

The survey also covers *learning* optimal graph topology (edge weights, GNN-based topology search) for large agent populations. That is out of scope for hand-scaffolded squads of a handful of agents — pick a topology from [topologies.md](topologies.md) directly instead of trying to learn one.
