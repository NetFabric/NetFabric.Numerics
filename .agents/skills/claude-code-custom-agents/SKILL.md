---
name: claude-code-custom-agents
description: "Create and maintain Claude Code custom subagents (.claude/agents/*.md files). USE FOR: authoring project (.claude/agents/) or user (~/.claude/agents/) subagent files; frontmatter fields (name, description, tools, disallowedTools, model, permissionMode, maxTurns, skills, mcpServers, hooks, memory, background, effort, isolation, color, initialPrompt); writing descriptions that drive automatic delegation and 'use proactively' phrasing; invoking subagents via natural language, @-mention, session-wide --agent, or the Agent tool; restricting which subagent types a coordinator can spawn with Agent(name1, name2); nested subagents and depth limits; built-in subagents (Explore, Plan, general-purpose, claude); troubleshooting a subagent that won't load or isn't invoked. DO NOT USE FOR: GitHub Copilot CLI custom agents (use copilot-cli-custom-agents); agent teams/teammates (a different, session-spanning feature); MCP server authoring; SKILL.md authoring (use create-skill)."
---

# Claude Code Custom Agents

A subagent is a Markdown file (`.md`) with YAML frontmatter plus a system-prompt body. Only `name` and `description` are required. Claude Code matches your request (and its own reasoning) against every subagent's `description` to decide when to delegate — a vague description rarely gets used.

## Anatomy

| Part | Purpose |
| --- | --- |
| Frontmatter | `name` + `description` (required) + optional `tools`, `disallowedTools`, `model`, `permissionMode`, `maxTurns`, `skills`, `mcpServers`, `hooks`, `memory`, `background`, `effort`, `isolation`, `color`, `initialPrompt` |
| Body | System prompt: role, numbered workflow, output format. The subagent receives only this plus basic environment details (CLAUDE.md, git status) — not the full Claude Code system prompt |
| Location | `.claude/agents/` (project, walked to Git root) and `~/.claude/agents/` (user) are watched live, no restart needed for edits to an *existing* directory; managed settings and `--agents` CLI JSON outrank both; plugin `agents/` is lowest priority |

## Minimal example

```markdown
---
name: code-reviewer
description: Reviews code for quality, security, and best practices. Use proactively after code changes.
tools: Read, Grep, Glob
model: sonnet
---

You are a senior code reviewer. When invoked, run `git diff`, focus on
modified files, and provide feedback organized by priority (critical,
warnings, suggestions) with specific fix examples.
```

## Invocation

| Method | Syntax |
| --- | --- |
| Automatic delegation | Claude matches your request against every subagent's `description` — include "use proactively" to encourage it |
| Natural language | Name the subagent in your prompt; Claude decides whether to delegate |
| @-mention | `@agent-<name>` — guarantees that specific subagent runs for one task |
| Session-wide | `claude --agent <name>` or `"agent": "<name>"` in `.claude/settings.json` — the whole session adopts that subagent's prompt, tools, and model |
| From another agent | The `Agent` tool (renamed from `Task` in v2.1.63) — nests up to 3 layers below the main conversation by default |

Full frontmatter field table, tool-availability filters, and location priority → [references/frontmatter-reference.md](references/frontmatter-reference.md).

## Built-in subagents

`Explore` (read-only search, model inherited/capped at Opus), `Plan` (plan-mode research), `general-purpose` (every subagent tool, for mixed research+edit tasks), `claude` (catch-all, also the default background-session agent), plus helper agents (`statusline-setup`, `claude-code-guide`) Claude invokes automatically. A user/project subagent named `Explore` overrides the built-in.

## Workflow

1. Scope one focused role per subagent; pick a scope (`.claude/agents/` project vs `~/.claude/agents/` user) → [references/authoring-workflow.md](references/authoring-workflow.md)
2. Write a specific `description` (add "use proactively" where delegation should be eager) and a numbered-workflow prompt body → [references/writing-style.md](references/writing-style.md)
3. Restrict `tools`/`disallowedTools` to the minimum the role needs; on a coordinator, allowlist spawnable subagent types with `tools: Agent(worker, researcher)` → [references/delegation-and-squads.md](references/delegation-and-squads.md)
4. Choose `model:` (and `effort:`) using the `model-selection` skill before finalizing frontmatter — never leave it on the `inherit` default without a deliberate reason
5. New agent files in an *existing* `.claude/agents/`/`~/.claude/agents/` load within seconds, no restart — restart only when that scope's `agents/` directory didn't exist when the session started
6. Check [references/authoring-workflow.md](references/authoring-workflow.md) if a subagent doesn't load or never gets invoked
7. Check [references/undocumented-and-gotchas.md](references/undocumented-and-gotchas.md) for version-specific behavior (background-by-default execution, model resolution order, output scanning) missing from a quick skim of the docs
8. Run `markdown-best-practices` over the finished subagent file — it's still Markdown (heading/list hygiene in the prompt body, no bare fences)

## Reference Files

| File | Load When |
| --- | --- |
| [references/frontmatter-reference.md](references/frontmatter-reference.md) | Writing or reviewing any frontmatter field, the available-tools list, or file location/priority rules |
| [references/writing-style.md](references/writing-style.md) | Writing or reviewing a `description` or prompt body — delegation phrasing, workflow structure, output format |
| [references/delegation-and-squads.md](references/delegation-and-squads.md) | Building a coordinator + specialist squad, nested subagents, `Agent(...)` allowlisting, or resuming a subagent |
| [references/authoring-workflow.md](references/authoring-workflow.md) | Creating a subagent (via Claude or by hand), choosing its scope, or troubleshooting why it won't load/invoke |
| [references/undocumented-and-gotchas.md](references/undocumented-and-gotchas.md) | Hitting a version-specific behavior the quickstart doesn't cover — background execution, permission-mode inheritance, output scanning, forks vs. subagents |
