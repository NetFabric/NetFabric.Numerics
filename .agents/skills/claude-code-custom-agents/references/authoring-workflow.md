# Authoring Workflow

## Quickstart

1. **Ask Claude to write it**: "Create a project code-improver subagent in `.claude/agents/` that scans files for readability/performance issues, read-only, using Sonnet." Claude writes the frontmatter (`name`, `description`, `tools`, `model`) and the body.
2. **Or hand-author** the file directly — see the [minimal example](../SKILL.md#minimal-example) and [frontmatter-reference.md](frontmatter-reference.md).
3. **Review** the generated file for scope creep — confirm `tools` is actually least-privilege, not just "whatever Claude defaulted to."
4. **Try it**: "Use the code-improver agent to suggest improvements in this project," or `@agent-code-improver`.

As of v2.1.198, `/agents` no longer opens an interactive creation wizard — running it just prints a reminder to ask Claude or edit `.claude/agents/` directly. The file format and locations are unchanged.

## Choosing a scope

| Scope | Location | Use when |
| --- | --- | --- |
| Project | `.claude/agents/` | Specific to this codebase; check into version control so the team shares it |
| User | `~/.claude/agents/` | Personal, useful across every project on your machine |
| Managed | Org's managed-settings `.claude/agents/` | Org-wide deployment; overrides project/user with the same name |
| Session-only | `--agents '{...}'` CLI JSON | Quick testing/automation, never written to disk |
| Plugin | `<plugin>/agents/` | Distributed with a plugin; lowest priority, and plugin subagents ignore `hooks`/`mcpServers`/`permissionMode` |

Both `.claude/agents/` and `~/.claude/agents/` are scanned recursively — organize into subfolders like `agents/review/` freely; the path doesn't affect identity, only `name` does. Keep `name` unique across the whole tree in one scope: duplicate names in the same directory resolve by filesystem read order (undocumented), and `/doctor` flags and offers to fix them.

## Restart caveats

The file watcher covers directories that already existed when the session started. Two cases still need a restart:

- Creating the *first* agent file in a scope whose `agents/` directory didn't exist yet (e.g. first-ever `.claude/agents/`)
- Sessions started with `--disable-slash-commands` don't watch these directories at all

Otherwise, edits to an existing subagent file are picked up within seconds — the next delegation uses the updated definition, no restart needed.

## Troubleshooting

| Symptom | Likely cause | Fix |
| --- | --- | --- |
| Subagent never gets delegated to | `description` too vague, or missing "use proactively" for an eager-delegation role | Rewrite with concrete trigger phrases → [writing-style.md](writing-style.md) |
| New subagent invisible after creation | First agent file in a newly-created `agents/` directory | Restart Claude Code |
| "Agent would be spawned with zero tools" error | Every entry in `tools` is misspelled or names a tool unavailable to subagents | Check spelling against [Available tools](frontmatter-reference.md#available-tools) |
| Coordinator can't spawn a specific subagent | `Agent(...)` allowlist on the coordinator's `tools` omits that type | Add the type to the parenthesized list, or use bare `Agent` |
| Two subagents with the same `name` in one directory, only one works | Name collision, undocumented resolution order | Run `/doctor` to find and rename/remove the duplicate |
| Subagent frontmatter hooks never fire | Project folder not yet trusted (workspace-trust dialog) | Accept the workspace trust prompt for that folder |
