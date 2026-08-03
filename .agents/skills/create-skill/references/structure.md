# Skill Structure Reference

## Folder Layout

```text
~/.agents/skills/<skill-name>/   # user-global (all workspaces)
  SKILL.md
  references/

.agents/skills/<skill-name>/     # workspace-scoped (preferred)
  SKILL.md
  references/

# Fallback (legacy):
.github/copilot/skills/<skill-name>/
```

## Frontmatter Spec

| Field | Required | Constraint | Notes |
|-------|----------|-----------|-------|
| `name` | yes | kebab-case | Must match folder name |
| `description` | yes | ≤1024 chars | Semantic trigger for agent |

## Description Template

```text
<Primary action verb> <domain>. Use when: <trigger list>. [Covers: <key topics>.] [DO NOT USE FOR: <exclusions>.]
```

- Lead with verb: "Create", "Configure", "Debug", "Migrate"
- Trigger list: semicolons, no articles, keyword-dense
- Exclusions prevent false activations — include them
- Count chars: `echo -n "text" | wc -c`

## Reference File Conventions

| Convention | Rule |
|-----------|------|
| Naming | lowercase, hyphen-separated |
| Scope | one topic per file |
| Size | ≤200 lines; split if larger |
| Linking | use relative markdown links in SKILL.md table |
| Load hint | every ref file must have a row in SKILL.md table |

## Scripts & Assets

| Directory | Use For | Notes |
|-----------|---------|-------|
| `scripts/` | Automation run by skill (e.g. codegen, scaffolding) | Python preferred; any language permitted |
| `assets/` | Templates, sample data, images | Link from SKILL.md or reference files |

## SKILL.md Required Sections

1. Frontmatter
2. Anatomy table (files + purpose + size)
3. Core quick-reference (tables/code, ≤3 sections)
4. Reference file table (file | load when)

## Anti-patterns

- ❌ Single flat SKILL.md with all detail
- ❌ Description > 1024 chars
- ❌ Reference files not listed in SKILL.md table
- ❌ `name` field mismatching folder name
- ❌ Duplicate content across SKILL.md and references
