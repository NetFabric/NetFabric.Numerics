# Frontmatter Reference

Source: [code.claude.com/docs/en/sub-agents](https://code.claude.com/docs/en/sub-agents), verified current as of Claude Code v2.1.222.

## Core fields

| Field | Required | Default | Notes |
| --- | --- | --- | --- |
| `name` | Yes | — | Lowercase letters and hyphens only, unique within its directory tree. Can't contain `:` (reserved for plugin-scoped IDs like `my-plugin:reviewer`). Hooks receive this as `agent_type`. Filename doesn't have to match |
| `description` | Yes | — | When Claude should delegate to this subagent — the sole signal for automatic delegation |
| `tools` | No | inherits every tool available to subagents | Allowlist. See [Available tools](#available-tools) |
| `disallowedTools` | No | — | Denylist, removed from the inherited/specified pool. Applied *before* `tools` is resolved; a tool in both is removed |
| `model` | No | `inherit` | `sonnet`, `opus`, `haiku`, `fable`, a full model ID (`claude-opus-5`), or `inherit` (same model as main conversation) |
| `permissionMode` | No | inherits parent | `default`, `acceptEdits`, `auto`, `dontAsk`, `bypassPermissions`, `plan`, `manual` (alias for `default`). Ignored for plugin subagents. If the parent uses `bypassPermissions`/`acceptEdits`/`auto`, the parent takes precedence |
| `maxTurns` | No | unlimited | Maximum agentic turns before the subagent stops |
| `skills` | No | — | Preloads full skill content into context at startup (not just the description); the subagent can still invoke unlisted skills via the `Skill` tool |
| `mcpServers` | No | — | MCP servers scoped to this subagent — a server-name string (reuses an already-configured server) or an inline config (connects only for this subagent). Ignored for plugin subagents |
| `hooks` | No | — | Lifecycle hooks scoped to this subagent (`PreToolUse`, `PostToolUse`, `Stop`→`SubagentStop`). Ignored for plugin subagents |
| `memory` | No | — | `user`, `project`, or `local` — persistent cross-session memory directory (part of auto memory; no-op if auto memory is disabled) |
| `background` | No | Claude decides (background by default since v2.1.198) | `true` forces background execution even when Claude needs the result immediately |
| `effort` | No | inherits session | `low`/`medium`/`high`/`xhigh`/`max`, model-dependent |
| `isolation` | No | — | `worktree` runs the subagent in a temporary git worktree, auto-cleaned up if it makes no changes |
| `color` | No | — | `red`/`blue`/`green`/`yellow`/`purple`/`orange`/`pink`/`cyan` — display only |
| `initialPrompt` | No | — | Auto-submitted first user turn when this agent runs as the *main session* (via `--agent`), prepended to any user prompt |

## Available tools

Subagents inherit built-in + MCP tools from the main conversation, minus two filters:

**Removed from every subagent** (even if listed in `tools`): `Agent` (only at the depth limit), `AskUserQuestion`, `EndConversation`, `EnterPlanMode`, `ExitPlanMode` (unless `permissionMode: plan`), `ScheduleWakeup`, `TaskOutput`, `WaitForMcpServers`, `Workflow`.

**Removed additionally when running in the background** (the default): everything except `Read`, `Grep`, `Glob`, `Bash`, `PowerShell`, `Edit`, `Write`, `NotebookEdit`, `WebFetch`, `WebSearch`, `TodoWrite`, `Skill`, `ToolSearch`, `EnterWorktree`, `ExitWorktree`, `Monitor`, `TaskStop`, `SendMessage`, `Artifact`, and all MCP tools. A [fork](delegation-and-squads.md#forks-vs-named-subagents) skips both filters and gets the parent's exact tool pool.

MCP server-level patterns work in both fields: `mcp__<server>` or `mcp__<server>__*` grants/removes every tool from that server; `mcp__*` in `disallowedTools` removes every MCP tool. When nothing in `tools` resolves to a real tool, the subagent usually fails to launch with a named-entries error (v2.1.208+).

## Restrict which subagents a coordinator can spawn

`Agent(name1, name2)` in `tools` allowlists exactly those subagent types for an agent running as the *main thread* (`claude --agent coordinator`). `Agent` with no parentheses allows spawning any type. Omitting `Agent` entirely blocks spawning altogether. Inside a subagent definition (not the main thread), `Agent` in `tools` just lets that subagent spawn its own children — any parenthesized type list there is ignored.

## Locations & priority

| Scope | Location | Priority |
| --- | --- | --- |
| Managed settings | Org-wide, deployed centrally | 1 (highest) |
| `--agents` CLI flag | Current session only, JSON, not saved to disk | 2 |
| Project | `.claude/agents/`, walked upward to the Git root; closest to cwd wins among nested directories | 3 |
| User | `~/.claude/agents/` | 4 |
| Plugin | `<plugin>/agents/` (subfolders become part of the ID, e.g. `my-plugin:review:security`) | 5 (lowest) |

Same-directory name collisions resolve by filesystem read order, not a documented rule — `/doctor` flags duplicates. Both `.claude/agents/` and `~/.claude/agents/` are watched live for edits to *existing* directories; creating a scope's first-ever agent file needs a restart to be picked up.
