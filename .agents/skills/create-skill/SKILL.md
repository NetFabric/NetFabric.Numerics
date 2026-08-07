---
name: create-skill
description: "Create or update AI agent skills (SKILL.md + reference files) for any harness that supports the format: GitHub Copilot, Claude Code, Cursor, Codex, Gemini CLI, Windsurf, Kiro, OpenCode, and more. Use when: authoring a new skill from scratch, improving or restructuring an existing skill, deciding how to split content across files, writing skill frontmatter, choosing trigger phrases for the description field, organizing reference files. Covers skill architecture, compact writing rules, frontmatter constraints, and research workflow via context7/microsoftdocs MCPs. DO NOT USE FOR: general coding tasks, VS Code extension development, non-skill documentation."
---

# Create Skill

Skills built this way run in any harness that supports `SKILL.md` — Copilot, Claude Code, Cursor, Codex, Gemini CLI, Windsurf, Kiro, OpenCode, and more — not just one tool.

## Anatomy

A skill is a folder (`<skill-name>/`, kebab-case) containing:

| File | Purpose | Target Size |
|------|---------|-------------|
| `SKILL.md` | Entry point — facts, quick-ref, reference table | <100 lines |
| `references/*.md` | Deep detail, examples, edge cases | <200 lines each |
| `scripts/` | Runnable helpers invoked by skill instructions | — |
| `assets/` | Templates, data files, images referenced by skill | — |

## SKILL.md Frontmatter

```yaml
---
name: <skill-name>            # matches folder name
description: "<≤1024 chars>"  # double-quoted; trigger phrases; relevance context; exclusions
---
```

**Description rules:** always wrap the value in double quotes — descriptions routinely contain colons ("USE FOR:", "DO NOT USE FOR:") that break strict YAML parsers when left unquoted; keyword-rich; list trigger phrases; state exclusions with "DO NOT USE FOR:"; ≤1024 chars; never write a literal `#` preceded by whitespace — YAML treats it as a comment and silently truncates the rest of the value (reword instead, e.g. "colon-prefixed directives" not "`#:`"). Full rules → [references/structure.md](references/structure.md#yaml-safety-in-descriptions).

## Workflow: New Skill

1. Define scope — subject, users, trigger phrases, exclusions
2. Research — load [references/research-workflow.md](references/research-workflow.md)
3. Draft SKILL.md skeleton (tables, code blocks, ref table)
4. Extract detail → reference files
5. Validate frontmatter length (`echo -n "..." | wc -c`)
6. Run `markdown-best-practices` over every new/edited `.md` file — heading spacing, table/list consistency, fenced-code language tags

## Workflow: Update Skill

1. Re-fetch affected docs via context7 / microsoftdocs
2. Edit only the reference file(s) that changed
3. Update SKILL.md summary + reference table if needed
4. Verify description ≤1024 chars
5. Re-run `markdown-best-practices` over every file touched

## Writing Rules (summary)

- Tables > prose; omit preamble; active voice
- Comments only when code can't show intent
- No restatements, no obvious callouts
- Full rules → [references/writing-style.md](references/writing-style.md)

## Reference Files

| File | Load When |
|------|-----------|
| [references/structure.md](references/structure.md) | Designing folder layout, frontmatter, or reference conventions |
| [references/writing-style.md](references/writing-style.md) | Writing or reviewing content for token efficiency |
| [references/research-workflow.md](references/research-workflow.md) | Researching a new or updated skill topic |
