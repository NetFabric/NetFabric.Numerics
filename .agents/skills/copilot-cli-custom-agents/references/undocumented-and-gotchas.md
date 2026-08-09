# Undocumented Features & Doc Gotchas

Sourced from `github/copilot-cli`'s own `changelog.md` — details not (yet) reflected in the official `cli-command-reference#custom-agents-reference` page. Verified current as of CLI v1.0.79 (2026-08-05).

## `infer` vs. `disable-model-invocation`

- The CLI added `disable-model-invocation`/`user-invocable` and made `infer` a **backward-compatible legacy alias** (changelog: "Custom agents use `disable-model-invocation` instead of `infer` (backward compatible)", shipped 0.0.411).
- The official CLI reference table still lists `infer` as a live field without any deprecation note.
- **Practical guidance**: both fields work on the CLI today; use `disable-model-invocation` in new agents, but don't be surprised to see `infer` in older examples or docs.

## Subagent max-depth: two different defaults documented

- `cli-command-reference`'s "Subagent limits" section states a default max depth of `6` (max `256`).
- The same page's "Environment variables" table lists `COPILOT_SUBAGENT_MAX_DEPTH` with a default of `4` (range `1`–`128`).
- The changelog resolves this: depth was **explicitly lowered from 6 to 4** to curb runaway recursive delegation, with usage-based billing users able to raise it up to `128` via `subagents.maxDepth`. Treat **4 / up to 128** as current; `6`/`256` is a stale figure left in one part of the docs.

## Fields/behaviors missing from the official frontmatter table

| Item | Evidence |
| --- | --- |
| `deferred-tool-loading` (boolean) | Changelog: "Custom agents support opt-in deferred tool loading via `deferred-tool-loading` in agent frontmatter, enabling tool-search discovery for agents with large tool lists." |
| `skills` (array) | Changelog: "Custom agents can now declare a `skills` field to eagerly load skill content into agent context at startup." |
| `reasoning-effort` | Changelog: "Let custom agents set reasoning effort in their definitions" (exact YAML key inferred from naming convention shared with `--reasoning-effort`/`disable-model-invocation`; verify against your installed CLI version if pinning behavior on it). |
| `sidekick` block (`triggers`, `behavior`, `maxSendsPerTurn`) | Documented in `cli-command-reference`'s "Sidekick agents" section, but absent from the main "Custom agent frontmatter fields" table — easy to miss. |
| `configure-copilot` built-in agent | Changelog only: "Add configure-copilot sub-agent for managing MCP servers, custom agents, and skills via the task tool." Absent from the official built-in-agents table. |
| Plugin-provided agents (`<plugin>/agents/`) | Changelog: "Plugins can provide custom agents"; "Plugin agents respect the model specified in their frontmatter"; "Plugins loaded via `--plugin-dir` now correctly register their agents as available `task(agent_type=...)` subagents in prompt mode." |
| `model` accepts display names | Changelog: "Custom agent model field now accepts display names and vendor suffixes from VS Code (e.g., `Claude Sonnet 4.5`, `GPT-5.4 (copilot)`)". Treat this as compatibility input, not authoring guidance: generate the canonical lowercase CLI ID (`claude-sonnet-4.6`, `gpt-5.4`, `gpt-5.3-codex`) whenever one exists. Never generate a bare human-readable label such as `GPT-5.4`; if compatibility requires display-name syntax, preserve the complete picker value, including a suffix such as `(copilot)`. |
| Comma-separated `tools:` | Changelog: "Support comma-separated tools in custom agent frontmatter" — in addition to a YAML array. |
| Unknown fields warn, don't block | Changelog: "Custom agents with unknown fields load with warnings instead of errors". |
| Malformed frontmatter now surfaces a real error | Changelog: "Surface the real load error for malformed custom agents" / "Show the real parse error when `--agent` selects a malformed custom agent" (earlier versions failed more silently). |
| Nested/subdirectory discovery | Changelog: "Custom agents and skills are now discovered recursively in subdirectories" and "Custom agents in nested `.github/agents` and `.claude/agents` directories are now discovered when the session is started from a subdirectory of the repository root." |
| `subagentStart` hook context injection | Changelog: "Add subagentStart hook that fires when a subagent is spawned, with support for injecting additional context into the subagent's prompt" — relevant if you pair custom agents with hooks for guardrails. |
| `~/.claude/` agents are NOT loaded | Changelog: "Custom agents, skills and commands from `~/.claude/` are no longer loaded by the Copilot CLI" — only project-level `.claude/agents/` (relative to repo root) counts, not the user's global Claude directory. |
