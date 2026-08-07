# Undocumented & Version-Specific Behaviors

Facts from `code.claude.com/docs/en/sub-agents` that are easy to miss on a first read, current as of Claude Code v2.1.222.

## Background execution by default

Since v2.1.198, subagents run in the **background** by default (Claude decides; a foreground run happens only when Claude needs the result immediately to continue). Background subagents get a **smaller built-in tool set** than foreground ones (see [frontmatter-reference.md](frontmatter-reference.md#available-tools)) — the same definition can therefore resolve to different available tools depending on how it happens to run. Set `background: true` in frontmatter to force background always, or set `CLAUDE_CODE_DISABLE_BACKGROUND_TASKS=1` session-wide to force foreground everywhere.

## Model resolution order

When a subagent runs, its actual model is resolved in this order (highest wins):

1. `CLAUDE_CODE_SUBAGENT_MODEL` environment variable (if set to a real alias/ID; `inherit` here is a no-op, resolution continues)
2. A per-invocation `model` parameter Claude passes when spawning
3. The subagent definition's `model` frontmatter
4. The main conversation's current model

An org's `availableModels` allowlist is checked against all of these; a blocked family alias (e.g. `opus`) substitutes the newest allowed version of that family rather than falling through.

## Permission-mode inheritance

A subagent's `permissionMode` is overridden by the parent's mode when the parent is already `bypassPermissions` or `acceptEdits` — those parent modes take precedence and can't be relaxed by the child. If the parent is in `auto` mode, the child's `permissionMode` frontmatter is ignored entirely and the classifier evaluates the child's tool calls with the parent's own rules.

## Subagent output scanning (prompt-injection defense)

Claude Code scans each subagent's final report before the parent reads it, because a subagent may have read attacker-controlled files/web pages/command output containing text shaped like conversation control tokens. The scan never removes content; it only inserts a backslash into imitation control tokens (e.g. a fake `<system-reminder>` tag) and prepends a `[harness: subagent output matched instruction-shaped pattern(s):` marker line when it detects one. **This is not a substitute for restricting what a subagent can reach** — a tool call the report leads Claude to make still goes through normal permission checks.

## Forks bypass the usual subagent isolation

A fork (see [delegation-and-squads.md](delegation-and-squads.md#forks-vs-named-subagents)) inherits the full parent conversation and skips both subagent tool filters — this is a deliberate exception, not a bug, since a fork is meant to behave like "the same session, running a side task."

## Plugin subagents drop three fields

For security, subagents loaded from a plugin's `agents/` directory ignore `hooks`, `mcpServers`, and `permissionMode` even if present in the file — these are silently no-ops on that load path. Copy the file into `.claude/agents/` or `~/.claude/agents/` if you need them.

## API errors don't masquerade as findings

A subagent whose run is cut off by a rate limit/overload/server error reports that failure explicitly back to the parent (with any partial output it had produced) instead of the error text being mistaken for the subagent's actual analysis (fixed as of v2.1.199).
