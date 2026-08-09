---
name: copilot-cli-custom-agents
description: "Create and maintain GitHub Copilot CLI custom agents (.agent.md files). USE FOR: authoring .github/agents or ~/.copilot/agents profiles; frontmatter fields (description, target, name, model, tools, mcp-servers, disable-model-invocation, user-invocable, deferred-tool-loading, skills, reasoning-effort, sidekick); writing descriptions that drive auto-delegation (the CLI routes on description quality, like skills); building orchestrator + specialist squads with the task tool; /fleet parallel subagents; built-in agents (explore, task, research, code-review, rubber-duck, security-review, general-purpose); subagent depth/concurrency limits; list_agents and write_agent coordination. DO NOT USE FOR: VS Code-only fields beyond target (agents: allowlist, handoffs) or GitHub.com cloud agent configuration; MCP server authoring; SKILL.md authoring (use create-skill)."
---

# Copilot CLI Custom Agents

A custom agent is a Markdown file (`.agent.md` or `.md`) with YAML frontmatter plus a system-prompt body. The filename (minus extension) is the agent's ID. The CLI auto-delegates to it based on the `description` field — exactly like the skill/skill-invocation mechanism — so a vague description never gets invoked.

## Anatomy

| Part | Purpose |
| --- | --- |
| Frontmatter | `description` (required) + `target: github-copilot` (always set) + optional `name`, `model`, `tools`, `mcp-servers`, `disable-model-invocation`, `user-invocable`, `deferred-tool-loading`, `skills`, `reasoning-effort`, `sidekick` |
| Body | System prompt: role, protocol, constraints. Max 30,000 characters |
| Location | `.github/agents/` or `.claude/agents/` (project, walked to Git root, deepest wins) > `~/.copilot/agents/` (user) > `<plugin>/agents/` (plugin, lowest priority) |

## Minimal example

```markdown
---
description: Reviews code for OWASP Top 10 security issues. Use for security audits, "seccheck", or vulnerability review requests.
target: github-copilot
tools: ['read', 'search']
---

You are a security reviewer. Identify vulnerabilities following the OWASP Top 10
taxonomy. Report findings in a table with severity, location, and remediation.
Do NOT modify files.
```

## Invocation

| Method | Syntax |
| --- | --- |
| Auto-delegation | Main agent matches your prompt against every agent's `description` |
| Inline mention | `@agent-name your prompt` |
| Slash command | `/agent` (browse/select), then enter a prompt |
| CLI flag | `copilot --agent agent-name --prompt "..."` |
| From another agent | `task(agent_type="agent-name", prompt="...")` — the CLI's subagent-dispatch tool |

`task` takes exactly two parameters: `agent_type` (the target agent's ID) and `prompt` (the task to run). The subagent starts with a fresh, empty context — `prompt` is the *only* channel of information it receives, so the calling agent must write every fact the subagent needs (goal, constraints, file paths) directly into that string rather than assuming shared context. Details and examples → [references/delegation-and-squads.md](references/delegation-and-squads.md).

## Built-in agents

`explore`, `task`, `general-purpose`, `code-review`, `research` (only via `/research`), `rubber-duck`, `security-review` — plus the undocumented `configure-copilot` agent for managing MCP servers/agents/skills. Full table → [references/delegation-and-squads.md](references/delegation-and-squads.md).

## Workflow

1. Scope one focused role per agent — read [references/frontmatter-reference.md](references/frontmatter-reference.md) for every field
2. Write a specific, keyword-dense `description` and a constraint-driven prompt body → [references/writing-style.md](references/writing-style.md) — vague descriptions never get auto-invoked
3. Restrict `tools:` to the minimum the role needs; strip `edit` from orchestrators and normally strip `shell` so they delegate. Retain narrowly scoped shell access only for an explicit orchestrator-owned dependency readiness preflight (for example, CBM installation/index verification), and forbid all other shell work in the prompt body
4. Choose `model:` (and `reasoning-effort:`) using the `model-selection` skill before finalizing frontmatter — never leave it unset on the assumption an inherited default is adequate
5. For multi-agent squads, adopt a naming prefix and use `user-invocable: false` on internal specialists → [references/delegation-and-squads.md](references/delegation-and-squads.md)
6. Always set `target: github-copilot` in the frontmatter — the CLI ignores it, but it's needed if the same file is ever opened in VS Code → [references/frontmatter-reference.md](references/frontmatter-reference.md)
7. Restart the CLI (or start a new session) to load new/edited agent files
8. Check [references/authoring-workflow.md](references/authoring-workflow.md) if the agent doesn't load or isn't invoked
9. Check [references/undocumented-and-gotchas.md](references/undocumented-and-gotchas.md) for fields/behaviors missing from the official reference table
10. Run `markdown-best-practices` over the finished `.agent.md` — it's still a Markdown file (frontmatter fences, heading/list hygiene in the prompt body)

## Reference Files

| File | Load When |
| --- | --- |
| [references/frontmatter-reference.md](references/frontmatter-reference.md) | Writing or reviewing any frontmatter field, tool name/alias, or file location/priority rule |
| [references/writing-style.md](references/writing-style.md) | Writing or reviewing a `description` or prompt body — trigger phrasing, constraints, output format |
| [references/delegation-and-squads.md](references/delegation-and-squads.md) | Building an orchestrator, a specialist squad, using `task`/`list_agents`/`write_agent`, or `/fleet` |
| [references/authoring-workflow.md](references/authoring-workflow.md) | Creating an agent via the CLI wizard, testing it, or troubleshooting why it won't load/invoke |
| [references/undocumented-and-gotchas.md](references/undocumented-and-gotchas.md) | Hitting a field or behavior the official CLI reference table omits; reconciling CLI vs. VS Code vs. cloud-agent differences |
