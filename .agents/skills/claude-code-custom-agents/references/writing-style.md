# Writing Style: Descriptions and Prompt Bodies

## The `description` field drives delegation

Claude matches your request, the current context, and every subagent's `description` to decide whether to delegate — there is no separate routing config. A vague description ("Backend helper") rarely triggers; a specific one with concrete trigger conditions does.

| Weak | Strong |
| --- | --- |
| "Code helper" | "Expert code review specialist. Proactively reviews code for quality, security, and maintainability. Use immediately after writing or modifying code." |
| "Database agent" | "Execute read-only database queries. Use when analyzing data or generating reports." |

Include "**use proactively**" or "**use immediately after X**" when the subagent should be delegated to eagerly rather than only on an explicit request — this phrasing measurably changes delegation behavior, it isn't just documentation flavor.

## Prompt body structure

The body is the subagent's entire system prompt (plus environment details Claude Code appends) — it does not inherit the main conversation's system prompt, output style, or history. Write it as a self-contained role definition:

```markdown
You are a senior code reviewer ensuring high standards of code quality and security.

When invoked:
1. Run git diff to see recent changes
2. Focus on modified files
3. Begin review immediately

Review checklist:
- Code is clear and readable
- No exposed secrets or API keys
- Input validation implemented

Provide feedback organized by priority: critical issues (must fix), warnings
(should fix), suggestions (consider improving). Include specific examples.
```

| Section | Purpose |
| --- | --- |
| Role statement | One line establishing expertise/persona |
| Numbered "when invoked" steps | Concrete first actions — a subagent starts with no context, so tell it where to look |
| Domain checklist | The specific things this role must check/produce |
| Output format | How results should be organized (priority tiers, sections, etc.) — this is what actually reaches the parent conversation |

## Restating rules the subagent needs

A subagent doesn't see the main conversation's `CLAUDE.md`-derived understanding directly beyond the raw file content, and `Explore`/`Plan` skip `CLAUDE.md` and git status entirely. If a rule from your project conventions must reach the subagent (e.g. "ignore the `vendor/` directory"), restate it explicitly in the delegation prompt or the subagent's own body — don't assume it's inherited.

## Anti-patterns

- ❌ A description that only names a technology ("Python agent") instead of a task/trigger
- ❌ Omitting numbered steps and expecting the subagent to infer a workflow from a vague role description
- ❌ Assuming project conventions from `CLAUDE.md` reach the subagent automatically
- ❌ Writing an output format the parent conversation has to reformat before using it
