# Skill Structure Reference

Skills are a cross-harness convention, not a single-tool feature — Copilot, Claude Code, Cursor, Codex, Gemini CLI, Windsurf, Kiro, OpenCode, and others all discover skills the same way.

## Folder Layout

```text
.agents/skills/<skill-name>/     # harness-neutral (preferred); every harness above reads this path
  SKILL.md
  references/

~/.agents/skills/<skill-name>/   # user-global (all workspaces)
```

Some harnesses also accept their own native path alongside `.agents/skills/`: `.github/skills/<name>/` (Copilot), `.claude/skills/<name>/` (Claude Code). When authoring through APM, source lives at `.apm/skills/<name>/SKILL.md`; `apm install`/`apm compile` deploys the harness-neutral copy automatically.

## SKILL.md Frontmatter Spec

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

## YAML Safety in Descriptions

**Always wrap the `description` value in double quotes** (`description: "..."`). Trigger-phrase descriptions routinely contain colons ("USE FOR:", "DO NOT USE FOR:", "Use when:"), and an unquoted YAML plain scalar treats `: ` (colon + space) mid-value as the start of a new mapping key — this throws a real parse error (`bad indentation of a mapping entry` / `mapping values are not allowed in this context`) in any strict YAML parser, including tools that load SKILL.md frontmatter directly. Earlier guidance in this repo assumed that was "tolerated" because apm's own frontmatter reader was regex-based — that assumption was wrong: other tooling (e.g. editor extensions) does strict-parse this frontmatter and fails loudly. Quoting is the only reliable fix; don't rely on rewording to dodge colons.

Inside a double-quoted scalar, only two characters need escaping: `\"` and `\\`. Everything else — colons, single quotes/apostrophes, backticks, parentheses — is safe as-is.

| Sequence | Effect | Fix |
|----------|--------|-----|
| `: ` (colon + space) mid-value | Starts a new mapping key in an unquoted scalar — parse error | Wrap the whole value in double quotes |
| ` #` (space + hash) | Starts a YAML comment — silently drops everything after it, with no error, even when quoted incorrectly | Keep the value inside the quotes; never let a `#` fall outside them |
| Literal `"` inside the value | Would end the quoted scalar early | Escape as `\"` |
| Leading `- ? : , [ ] { } # & * ! \| > ' " % @` \` (if ever unquoted) | A scalar can't start with these unquoted | Moot once the whole value is quoted |

Verify: `node -e "const yaml=require('js-yaml'); const fm=require('fs').readFileSync('SKILL.md','utf8').match(/^---\n([\s\S]*?)\n---/)[1]; console.log(yaml.load(fm))"` — this must succeed without throwing and print the full, untruncated description.

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

A `scripts/` script isn't limited to pure deterministic code — it can embed the [copilot-sdk](../../../../apm_modules/netfabric/intelligentium/plugins/agent-authoring/.apm/skills/copilot-sdk/SKILL.md) to call out to Copilot for one bounded, non-deterministic step (e.g. classify input, summarize a diff) and then resume deterministic control flow. Use this when only part of the pipeline needs judgment; keep the AI call scoped to that step so the rest of the script stays testable and reproducible.

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
