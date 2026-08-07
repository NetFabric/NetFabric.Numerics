# Harness Field Mapping

The criteria in [selection-criteria.md](selection-criteria.md) and [cache-and-cost.md](cache-and-cost.md) apply everywhere; how you *express* the chosen model differs by harness. Always confirm which harness targets the agent before writing the field — never assume from the current session's own tool.

## Comparison table

| Harness | Model field(s) | Value shape | Resolution order (highest wins) | Fallback-list support |
| --- | --- | --- | --- | --- |
| GitHub Copilot CLI (`.agent.md`) | `model`, `reasoning-effort` | Single string only — a short model ID or a display name with vendor suffix (e.g. `"GPT-5.4 (copilot)"`) | Session model set to `Auto` overrides every agent's own `model:` field | None — an inline array is silently ignored. Approximate resilience via `~/.copilot/settings.json` → `subagents.agents.<name>.model` instead |
| Claude Code (`.claude/agents/*.md`) | `model`, `effort` | Single value only — alias (`sonnet`/`opus`/`haiku`/`fable`), a full model ID, or `inherit` (defaults to `inherit`) | `CLAUDE_CODE_SUBAGENT_MODEL` env var > per-invocation `model` parameter > the subagent's own frontmatter `model` > the main conversation's model | None — single value only |
| VS Code custom agents (`.agent.md`, `target: vscode`) | `model` | Either a single string, or an array tried in order until an available model is found | The array is walked top-to-bottom at invocation time; no session-level override documented | Yes — the one harness among these three with native fallback-list support |

Details and exact syntax always live in that harness's own agent-authoring skill (`copilot-cli-custom-agents`, `claude-code-custom-agents`, or the relevant other harness skill) — treat the table above as a quick comparison, not the authoritative reference for writing the field.

## Extending to a new harness

When authoring for a harness not in the table above, work out these before writing a `model:`-equivalent field:

1. **Single value or list?** Does the field accept only one model, or a prioritized array with automatic fallback?
2. **Separate effort/reasoning field?** Is intelligence-vs-latency tuning a distinct field from the model choice itself, or bundled into model selection only?
3. **Session/parent-level override?** Can a broader setting (a session-wide model, an `Auto`-style mode, an org policy) override or ignore the per-agent field regardless of what's written there?
4. **Native cost/budget field?** Does the harness enforce its own per-agent or per-session cost ceiling, making the cost-ceiling heuristic in [cache-and-cost.md](cache-and-cost.md) unnecessary?
5. **Cross-vendor reach?** Is the field restricted to one vendor's models (like Claude Code), or can it name any provider the harness integrates with?

Record the answers as a new row in the comparison table above and cite that harness's own docs/agent-authoring skill as the source of truth for exact field syntax.
