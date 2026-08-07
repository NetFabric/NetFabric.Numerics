# Model Selection by Call Cadence and Prompt-Cache TTL

Providers cache prompt prefixes (system prompt, tools, static instructions) to cut latency and cost on repeated calls, but each provider's cache has a different lifetime. A node's call cadence in the graph determines whether that lifetime matters.

## Why cadence matters

A cache entry is evicted after its TTL elapses since the last hit. Frequently-called nodes (parallel workers, tight feedback loops) refresh the cache on nearly every call, so a short TTL never becomes a problem. Infrequently-called nodes — chiefly an **orchestrator**, which dispatches once per node and then sits idle while every subagent runs — can easily leave the cache idle longer than a short TTL, forcing a full-price cache write on the orchestrator's next call.

## Provider cache TTLs (verify against current provider docs before relying on exact figures)

| Provider | Default cache lifetime | Longer option |
| --- | --- | --- |
| Anthropic (Claude) | 5 minutes, refreshed on each hit | 1-hour TTL at 2× the cache-write price |
| OpenAI (pre-GPT-5.6 models) | ~5–10 minutes of inactivity, up to ~1 hour off-peak (automatic, not configurable) | Extended retention up to 24 hours (`prompt_cache_retention: "24h"`) on specific models (e.g. `gpt-5.5`, `gpt-5.1` family, `gpt-5`, `gpt-4.1`) |
| OpenAI (GPT-5.6+ models) | `prompt_cache_options.ttl` minimum lifetime, default `30m` | Same 30 m minimum; provider may retain longer |

## Selection guidance

| Node role | Call cadence | Preference |
| --- | --- | --- |
| Orchestrator | Infrequent — one dispatch per node, long idle gaps waiting on subagents | Favor a model/provider with a longer or configurable cache retention (e.g. OpenAI extended retention) so its system prompt and tool definitions stay cached across the whole graph run |
| Worker/specialist in parallel fan-out | Frequent — dispatched together, often returns quickly | Cache-TTL-agnostic; any provider's default TTL is refreshed well within its lifetime |
| Reviewer/writer in a tight feedback loop | Frequent — each cycle iteration re-invokes it within seconds | Cache-TTL-agnostic, same reasoning |
| Rarely-invoked conditional branch (e.g. an escalation path taken occasionally) | Infrequent, unpredictable timing | Treat like the orchestrator — prefer longer retention if the branch has a large static prompt |

## Cross-provider diversity for tribunal review

Cache-TTL selection (above) optimizes cost/latency for a single node's own call pattern. Tribunal review nodes (see [topologies.md](topologies.md#5-tribunal-review-adversarial-cross-provider)) add a second, independent selection axis: **diversity**, not cache lifetime.

| Role | Selection axis | Guidance |
| --- | --- | --- |
| Reviewer A | Provider diversity | Pick a different provider than Reviewer B and the implementer |
| Reviewer B | Provider diversity | Pick a different provider than Reviewer A and the implementer |
| Review orchestrator (merge step) | Cache-TTL axis | Infrequent, single merge call per review round — apply the orchestrator guidance above |

A node can need both axes at once — the review-orchestrator is itself an orchestrator (infrequent calls → longer-TTL provider preferred) while also enforcing the diversity constraint on its two reviewer children. Pick the orchestrator's own model by cadence/TTL; pick the two reviewers' models by pairwise provider difference from each other and from the implementer, independent of TTL.

Three-provider minimum for a full tribunal: implementer, reviewer A, and reviewer B each on a distinct provider. If only two providers are available, prioritize `provider(reviewer-A) != provider(reviewer-B)` over separating either from the implementer — the adversarial pair catching each other's mistakes is the primary value; catching the implementer's own bias is secondary.

### Claude Code constraint: no cross-vendor diversity via `model:`

Claude Code's subagent `model:` field only accepts Anthropic aliases/IDs (`sonnet`, `opus`, `haiku`, `fable`, a full `claude-*` model ID, or `inherit`) — every subagent on Claude Code runs a Claude model regardless of infra provider (direct API, Bedrock, Vertex, Foundry all serve Claude models only). A tribunal built entirely on Claude Code subagents therefore can't satisfy true cross-vendor provider diversity through `model:` alone. Fall back to **cross-model-family diversity** instead (e.g. reviewer A on `opus`, reviewer B on `sonnet`) and say explicitly that this is a weaker independence guarantee than a genuine cross-vendor tribunal — the two reviewers still share the same base training lineage.

## Applying this in Copilot CLI and Claude Code

Set the `model:` frontmatter field (see `copilot-cli-custom-agents`/`claude-code-custom-agents` → frontmatter-reference.md) per node based on the tables above. This is a cost/latency/diversity heuristic, not a correctness requirement — measure actual cache hit rates via the provider's usage fields (`cached_tokens`/`cache_read_input_tokens`) before over-optimizing a low-traffic graph.
