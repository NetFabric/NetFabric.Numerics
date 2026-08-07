---
name: model-selection
description: "Choose and tune which AI model a custom agent, subagent, or squad node should use. USE FOR: picking a model by task capability tier (general-purpose coding/writing, fast/cheap, deep reasoning/debugging, multimodal); efficiency-first vs. capability-first starting strategies; tuning an effort/reasoning-effort parameter instead of switching models; call cadence vs. provider prompt-cache TTL; cost-tier ranking and cost ceilings; cross-provider or cross-model-family diversity for independent/adversarial review; per-harness model field syntax and resolution order (GitHub Copilot CLI, Claude Code, VS Code custom agents); extending this framework to a new harness. DO NOT USE FOR: authoring the rest of an agent's frontmatter or prompt body (use copilot-cli-custom-agents, claude-code-custom-agents, or the target harness's own skill); designing squad topology (use agent-graph-orchestration); SKILL.md authoring (use create-skill)."
---

# Model Selection

Every custom agent, subagent, or squad node needs an explicit model choice — never leave it unset on the assumption that an inherited default is adequate for a specialist role. The right model balances five criteria, then gets mapped onto whatever field syntax the target harness actually accepts.

## Anatomy

| Criterion | Question it answers |
| --- | --- |
| Capability tier | How complex is the reasoning this agent's task requires? |
| Speed | How latency-sensitive is whoever is waiting on this agent? |
| Cost | What's the budget, and is this agent bounded by a caller's own cost ceiling? |
| Context & modality | Does the task need a large context window, or image/visual input? |
| Effort | Can the *same* model be tuned cheaper or faster instead of switching models entirely? |

## Workflow

1. **Establish task criteria** for this specific agent — capability need, latency sensitivity, cost budget, context/multimodal needs → [references/selection-criteria.md](references/selection-criteria.md)
2. **Pick a starting strategy**: efficiency-first (start cheap/fast, upgrade only if evals show a gap) or capability-first (start at the top, optimize down later) → [references/selection-criteria.md](references/selection-criteria.md)
3. **Prefer tuning an effort/reasoning-effort parameter over switching models** when the harness and model support it — it trades intelligence for latency/cost within the same model family, a cheaper lever than a full model swap → [references/selection-criteria.md](references/selection-criteria.md)
4. **If this agent is one node of a multi-agent squad, or needs independent/adversarial review**, apply call-cadence vs. provider prompt-cache TTL, cost-tier ranking/ceilings, and cross-provider diversity → [references/cache-and-cost.md](references/cache-and-cost.md)
5. **Ask which harness targets this agent** (if not already established), then apply that harness's exact model field name, value shape, and resolution order → [references/harness-mapping.md](references/harness-mapping.md)
6. **Verify the chosen model name is still current** against the provider's live docs before finalizing — model lineups rotate every few months; never trust a memorized or previously-seen model name as still accurate → [references/selection-criteria.md](references/selection-criteria.md#verify-against-live-docs)
7. **Run `markdown-best-practices`** over any new or edited Markdown produced while documenting the choice (design notes, agent files' surrounding docs)

## Reference Files

| File | Load When |
| --- | --- |
| [references/selection-criteria.md](references/selection-criteria.md) | Picking a capability tier for a task, choosing an efficiency-first vs. capability-first starting point, tuning an effort/reasoning-effort parameter, deciding whether to upgrade/downgrade after benchmarking, or verifying a model name is still current |
| [references/cache-and-cost.md](references/cache-and-cost.md) | An agent's call cadence vs. its provider's prompt-cache TTL, ranking candidate models by cost tier, applying a cost ceiling inherited from a caller, or picking genuinely independent models/providers for adversarial or second-opinion review |
| [references/harness-mapping.md](references/harness-mapping.md) | Writing the actual `model:`-equivalent field for GitHub Copilot CLI, Claude Code, or VS Code custom agents, understanding each harness's resolution order and fallback-list support, or extending this skill's guidance to a harness not yet covered |
