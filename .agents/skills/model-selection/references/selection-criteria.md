# Selection Criteria

## Task-based capability tiers

Providers group their models into task-oriented tiers rather than a single quality ranking — pick by what the agent actually does, not by "the newest model." The four tiers below recur across every major provider's own model-comparison guidance (verified against GitHub Copilot's and Anthropic's current docs at time of writing):

| Tier | Use when the agent's task is... | Typical traits |
| --- | --- | --- |
| General-purpose coding/writing | Writing or reviewing functions, short files, diffs; generating docs/comments/summaries; explaining errors | Balanced quality, speed, and cost — the default when nothing below applies |
| Fast/cheap | Small utility functions, quick syntax questions, prototyping, repetitive edits, high-volume simple lookups | Lowest latency and cost tier; accept a shallower reasoning depth |
| Deep reasoning/debugging | Multi-file debugging, large refactors, architecture/design tradeoffs, analyzing logs or performance data | Highest capability tier; highest cost and often highest latency |
| Multimodal/visual | Diagrams, screenshots, UI components, visual QA | Requires a model that accepts image input — a capability, not just a tier |

An agent scoped narrowly (see the harness's own agent-authoring skill for scoping guidance) usually maps cleanly to one tier. An agent with a broad, mixed workload may need the deep-reasoning tier even if most of its individual calls are simple, since the occasional hard case sets the floor.

## Two starting strategies

Anthropic's own model-selection guidance frames the starting choice as one of two strategies; the same logic applies across providers:

| Strategy | Approach | Best for |
| --- | --- | --- |
| Efficiency-first | Start on the fast/cheap tier, test the actual use case, upgrade only if evals show a real capability gap | Prototyping, tight latency budgets, cost-sensitive or high-volume agents |
| Capability-first | Start on the deep-reasoning tier, then optimize down (lower effort, cheaper model) once the task is well understood | Complex reasoning, advanced agentic/coding work, tasks where correctness outweighs cost |

Neither strategy is universally correct — pick based on how well-understood the task is and how expensive a wrong answer would be.

## Prefer tuning effort over switching models

Several current model families expose an effort/reasoning-effort parameter that trades intelligence for latency and cost *within the same model*, independent of the harness's own `model:` field:

- This is usually a cheaper, faster lever than swapping to a different model entirely, since it keeps the same weights, training, and (often) prompt cache.
- Start at a model's documented default effort level; step up only for the hardest tasks in the agent's workload and down only once evals confirm quality holds.
- Exact field name and accepted values are harness/model-specific (e.g. a `reasoning-effort` frontmatter field on one harness, an `effort` field on another) → [harness-mapping.md](harness-mapping.md).

## Benchmark before upgrading or downgrading

Don't change an agent's model on intuition alone:

1. Build a small evaluation set specific to this agent's actual task (not a generic benchmark).
2. Run the candidate model(s) against it and compare accuracy, response quality, and edge-case handling.
3. Weigh the measured quality difference against the cost/latency difference before committing to a change.

## Verify against live docs

Model names, tiers, and pricing rotate every few months — a name that was current when a skill or design doc was written can be retired by the time it's read. Before finalizing a `model:` value:

- Check the target provider's current model list/comparison docs (e.g. the harness's own vendor's published model catalog) rather than relying on a memorized or previously-seen name.
- Treat any specific model name in this skill's own examples, or in any other skill's examples, as illustrative only, not as a guarantee that name is still offered.
- If a chosen model has a published retirement date, prefer its listed successor instead of the retiring name.
