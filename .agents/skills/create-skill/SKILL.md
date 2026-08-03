---
name: create-skill
description: Create or update VS Code Copilot agent skills (SKILL.md + reference files). Use when: authoring a new skill from scratch, improving or restructuring an existing skill, deciding how to split content across files, writing skill frontmatter, choosing trigger phrases for the description field, organizing reference files. Covers skill architecture, compact writing rules, frontmatter constraints, and research workflow via context7/microsoftdocs MCPs. DO NOT USE FOR: general coding tasks, VS Code extension development, non-skill documentation.
---

# Create Skill

## Anatomy

| File | Purpose | Target Size |
|------|---------|-------------|
| `SKILL.md` | Entry point — facts, quick-ref, reference table | <100 lines |
| `references/*.md` | Deep detail, examples, edge cases | <200 lines each |
| `scripts/` | Runnable helpers invoked by skill instructions | — |
| `assets/` | Templates, data files, images referenced by skill | — |

## Frontmatter

```yaml
---
name: <kebab-case>          # matches folder name
description: <≤1024 chars>  # trigger phrases; relevance context; exclusions
---
```

**Description rules:** keyword-rich; list trigger phrases; state exclusions with "DO NOT USE FOR:"; ≤1024 chars.

## Workflow: New Skill

1. Define scope — subject, users, trigger phrases, exclusions
2. Research — load [references/research-workflow.md](references/research-workflow.md)
3. Draft SKILL.md skeleton (tables, code blocks, ref table)
4. Extract detail → reference files
5. Validate frontmatter length (`echo -n "..." | wc -c`)

## Workflow: Update Skill

1. Re-fetch affected docs via context7 / microsoftdocs
2. Edit only the reference file(s) that changed
3. Update SKILL.md summary + reference table if needed
4. Verify description ≤1024 chars

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
