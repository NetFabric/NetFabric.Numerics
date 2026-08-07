# Cache Cadence, Cost Ceilings, and Diversity

These three axes matter most once an agent isn't operating alone — it's one node of a multi-agent squad, or it needs a genuinely independent second opinion.

## Call cadence vs. prompt-cache TTL

Providers cache prompt prefixes (system prompt, tools, static instructions) to cut latency and cost on repeated calls, but each provider's cache has a different lifetime. A cache entry is evicted after its TTL elapses since the last hit.

- An agent invoked **frequently** (parallel workers, a tight review/revise loop) refreshes the cache on nearly every call, so a short TTL never becomes a problem — treat these as cache-TTL-agnostic.
- An agent invoked **infrequently** — dispatched once and then idle for a long stretch while other work happens, or a rarely-taken conditional/escalation branch — can easily leave the cache idle longer than a short TTL, forcing a full-price cache write on its next call. Favor a model/provider with a longer or configurable cache retention for these.

### Provider cache TTLs (verify against current provider docs before relying on exact figures)

| Provider | Default cache lifetime | Longer option |
| --- | --- | --- |
| Anthropic (Claude) | 5 minutes, refreshed on each hit | 1-hour TTL at 2× the cache-write price |
| OpenAI (pre-GPT-5.6-generation models) | ~5–10 minutes of inactivity, up to ~1 hour off-peak (automatic, not configurable) | Extended retention up to 24 hours (`prompt_cache_retention: "24h"`) on specific models |
| OpenAI (GPT-5.6-generation models and later) | `prompt_cache_options.ttl` minimum lifetime, default `30m` | Same 30 m minimum; provider may retain longer |

## Cost-tier ranking and cost ceilings

Rank candidate models by their provider's published cost tier (e.g. a premium-request multiplier or per-token price — verify current values against the provider's own pricing docs, they change over time) before assigning any agent's model.

When an agent is a child of some caller that itself has a cost signal — a parent orchestrator's own model choice, a session-level model, or an explicit budget field — treat that signal as a ceiling: no child agent should be assigned a model with a higher cost tier than its caller's, unless the tradeoff is deliberately surfaced to the user (see diversity below, which can conflict with a strict ceiling). Pick the caller's own model first, since every dependent's ceiling is relative to it.

This heuristic matters most on harnesses with no native per-agent cost/budget field to enforce it directly. A harness that does have its own cost/budget control (an org-level spend cap, a per-agent budget field) doesn't need this heuristic — defer to that control instead.

## Cross-provider/model-family diversity for independent review

A review, verification, or second-opinion role only adds value if it's genuinely independent from whatever it's reviewing — same-provider, same-model agreement can just mean both share the same blind spot.

- Pick a different **provider** for an independent reviewer than the agent(s) whose work it's reviewing, when the harness allows choosing across providers.
- When only one provider/vendor is available (a harness whose `model:` field is locked to a single vendor's models), fall back to **cross-model-family diversity** instead (e.g. a larger model from one family vs. a smaller model from a different family within that vendor's lineup) and treat this explicitly as a weaker independence guarantee than a genuine cross-vendor pairing — both models still share the same base training lineage.
- With three or more independent reviewers, prioritize maximizing pairwise diversity among the reviewers themselves over separating any one of them from the original implementer — reviewers catching each other's mistakes is the primary value; catching the original implementer's own bias is secondary.
- Check the target harness's own agent-authoring skill for whether its model field can even express cross-vendor diversity before designing around it → [harness-mapping.md](harness-mapping.md).
