# Writing Style

`AGENTS.md` is re-read into context on every task, same as a skill — apply the same compact, table-driven style, with zero loss of accuracy.

## Core Rules

| Rule | Do | Don't |
| --- | --- | --- |
| Executable over descriptive | "Run `pnpm test`" | "Make sure tests pass" |
| Tables > prose | Bullet/table a list of commands or conventions | Multi-sentence paragraphs for scannable facts |
| No preamble | Start with the first section heading | "This file describes how to work in this repo..." |
| Active voice, short sentences | "Run `X` before committing" | "`X` should be run by contributors before changes are committed" |
| Link, don't duplicate | Link to README/CONTRIBUTING for human-facing content | Copy full README prose into `AGENTS.md` |
| Concrete over aspirational | Document what the repo's tooling actually does today | Describe a style guide nobody enforces |
| Concise over exhaustive | Short, scannable sections | A wall of prose an agent must re-parse every run |
| Nest instead of bloat | Per-package `AGENTS.md` for monorepos | One giant root file covering every package |
| Mermaid diagrams | Flow/sequence/graph when structure is clearer and more compact than prose or a table | Diagrams for data that fits cleanly in a table |

Compactness never trims a command, a flag, or a caveat that changes behavior — cut words, not facts.

## Section Length Guidance

| Section | Target |
| --- | --- |
| Project overview | 1 paragraph |
| Each list section (setup, build, code style) | 3-8 bullets |
| Whole file (single-package repo) | Fits on one screen; split into nested files before it doesn't |

## Anti-Patterns

- ❌ Multi-sentence paragraphs where a table or bullet list would do
- ❌ Preamble ("This file describes...", "Note that...") before the first heading
- ❌ Copying the entire README/CONTRIBUTING.md verbatim instead of linking to it
- ❌ Describing a convention aspirationally instead of what the tooling actually enforces today
- ❌ One monolithic root file for a large monorepo instead of nested per-package files
