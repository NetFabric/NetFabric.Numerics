# Squad-Specific Model Selection Overlay

This file covers only what's specific to a multi-agent squad's node roles. For the generic mechanics — capability tiers, efficiency-first vs. capability-first, effort tuning, cadence-vs-cache-TTL tables, cost-tier ranking, and per-harness field syntax — use the `model-selection` skill instead; this overlay assumes that skill's guidance has already been applied per node and adds only the squad-topology-specific rules below.

## Model selection is mandatory

Every node in the squad — orchestrator, planner, every implementer, every tribunal reviewer, and a quality-gate wrapper agent if one exists — must have an explicit `model:` value chosen for its task, never left unset on the assumption that an inherited default is adequate for a specialist role. Pick it using the `model-selection` skill, then apply the squad-specific overlay below (cost ceiling, tribunal diversity) before finalizing the design.

## Node role → call cadence

Map each node role to the cadence guidance in the `model-selection` skill's cache-and-cost reference:

| Node role | Call cadence |
| --- | --- |
| Orchestrator | Infrequent — one dispatch per node, long idle gaps waiting on subagents |
| Worker/specialist in parallel fan-out | Frequent — dispatched together, often returns quickly |
| Reviewer/writer in a tight feedback loop | Frequent — each cycle iteration re-invokes it within seconds |
| Rarely-invoked conditional branch (e.g. an escalation path taken occasionally) | Infrequent, unpredictable timing — treat like the orchestrator |

## Cross-provider diversity for tribunal review

Cadence-based selection (above) optimizes cost/latency for a single node's own call pattern. Tribunal review nodes (see [topologies.md](topologies.md#5-tribunal-review-adversarial-cross-provider)) add a second, independent selection axis: **diversity**, not cache lifetime. The general principle lives in the `model-selection` skill's cache-and-cost reference; this section is the squad-specific application of it.

| Role | Selection axis | Guidance |
| --- | --- | --- |
| Reviewer A | Provider diversity | Pick a different provider than Reviewer B and the implementer |
| Reviewer B | Provider diversity | Pick a different provider than Reviewer A and the implementer |
| Review orchestrator (merge step) | Cache-TTL axis | Infrequent, single merge call per review round — apply the orchestrator guidance above |

A node can need both axes at once — the review-orchestrator is itself an orchestrator (infrequent calls → longer-TTL provider preferred) while also enforcing the diversity constraint on its two reviewer children. Pick the orchestrator's own model by cadence/TTL; pick the two reviewers' models by pairwise provider difference from each other and from the implementer, independent of TTL.

Three-provider minimum for a full tribunal: implementer, reviewer A, and reviewer B each on a distinct provider. If only two providers are available, prioritize `provider(reviewer-A) != provider(reviewer-B)` over separating either from the implementer — the adversarial pair catching each other's mistakes is the primary value; catching the implementer's own bias is secondary.

### Claude Code constraint: no cross-vendor diversity via `model:`

Claude Code's subagent `model:` field only accepts Anthropic aliases/IDs (`sonnet`, `opus`, `haiku`, `fable`, a full `claude-*` model ID, or `inherit`) — every subagent on Claude Code runs a Claude model regardless of infra provider (direct API, Bedrock, Vertex, Foundry all serve Claude models only). A tribunal built entirely on Claude Code subagents therefore can't satisfy true cross-vendor provider diversity through `model:` alone. Fall back to **cross-model-family diversity** instead (e.g. reviewer A on `opus`, reviewer B on `sonnet`) and say explicitly that this is a weaker independence guarantee than a genuine cross-vendor tribunal — the two reviewers still share the same base training lineage.

## Cost ceiling on Copilot CLI

Copilot CLI has no orchestrator-level spend limit or subagent cost policy — the orchestrator's own `model:` is the only cost signal the user set when invoking the squad, so treat it as the squad's cost ceiling:

- No specialist node (planner, implementer, quality-gate wrapper agent) may be assigned a `model:` with a higher cost tier than the orchestrator's — rank candidates using the `model-selection` skill's cost-tier ranking guidance.
- A tribunal reviewer's cross-provider diversity requirement (above) can conflict with this ceiling when the only cross-provider option costs more than the orchestrator. When that happens, surface the tradeoff to the user explicitly (diversity vs. cost ceiling) rather than silently picking one side; don't default to quietly violating the ceiling.
- Pick the orchestrator's own model first, before any other node — every other node's ceiling depends on that choice.

This constraint is Copilot-CLI-specific because the CLI has no native per-node cost limit to enforce it for you. A harness with its own cost/budget controls (e.g. an org-level spend cap) doesn't need this heuristic — defer to that control instead.

## Quality gate: no model selection needed

A quality gate node (see [topologies.md](topologies.md#mandatory-baseline-squad-shape)) runs deterministic tools, not an LLM call — skip model selection for it entirely. When scaffolded as an orchestrator-inline step it has no `model:` field to set at all; even the minimal-wrapper-agent variant ([scaffolding-workflow.md](scaffolding-workflow.md#6-scaffold-a-quality-gate-node-if-the-design-calls-for-one)) should not be assigned a `model:` chosen for reasoning quality, since it isn't meant to reason about the artifact — only the adversarial reviewer stage spends tokens on reasoning.

## Applying this per node

Use the `model-selection` skill (its `harness-mapping.md` reference) to write the actual `model:` field for the target harness — this is a required step for every node, not an optional optimization. On Copilot CLI, pick the orchestrator's model first since it sets the squad's cost ceiling (above), then every other node's. Beyond the cost ceiling, cadence/TTL selection is a latency/diversity heuristic, not a correctness requirement — measure actual cache hit rates via the provider's usage fields (`cached_tokens`/`cache_read_input_tokens`) before over-optimizing a low-traffic graph.
