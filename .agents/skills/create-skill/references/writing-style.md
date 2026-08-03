# Compact Writing Style

## Core Rules

| Rule | Do | Don't |
|------|----|-------|
| Tables > prose | `\| Key \| Value \|` grids | Multi-sentence comparisons |
| No preamble | Start with content | "This file covers..." |
| Active voice | "Run `X`" | "`X` should be run" |
| No restatement | Comments only when code hides intent | Restating what headers say |
| Compress lists | Semicolons between items | Bulleted single-word items |
| Symbols | `→`, `&`, `vs`, `≤`, `≥` | Spelled-out equivalents |
| Mermaid diagrams | Flow/sequence/graph when structure is clearer than prose or tables | Diagrams for data that fits in a table |

## Token Budget Targets

| File | Lines | Code blocks |
|------|-------|-------------|
| SKILL.md | <60 | Only non-obvious examples |
| Each reference | <200 | Essential patterns only |

## Section Pattern

```markdown
## Section Title
One-line context (omit if header is self-evident).

| Col | Col |
|-----|-----|
| ... | ... |
```

## Content Triage

Keep in SKILL.md:

- Anatomy/structure overview
- Workflow steps (brief)
- Quick-ref tables
- Reference file table

Move to references:

- Full API signatures
- Extended examples
- Edge cases and gotchas
- Background / motivation

## Anti-patterns

- ❌ "Note:", "Important:", "Remember:" for obvious facts
- ❌ Blank lines between every bullet
- ❌ Repeating the section title in the first sentence
- ❌ Code blocks for trivial one-liners already shown in prose
- ❌ History or motivation sections (unless critical to usage)
- ❌ Closing summary that restates the intro
