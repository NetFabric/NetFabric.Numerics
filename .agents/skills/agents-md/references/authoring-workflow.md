# Authoring Workflow

## Section Catalog

| Section | Purpose | Include When |
|---------|---------|--------------|
| Project overview | One paragraph: what the repo does, high-level architecture | Always |
| Setup / dev environment | Install deps, start dev server, env vars | Always |
| Build & test commands | Exact commands to build, run, and test | Always — these get executed by agents |
| Code style | Formatting, linting, naming conventions actually enforced | Once conventions exist (tooling or established patterns) |
| Testing instructions | How to run the full suite, a single test, and required coverage/checks | Once a test suite exists |
| PR / commit guidelines | Title format, required checks before merge, changelog rules | Once a contribution process exists |
| Security considerations | Secrets handling, destructive-command guardrails, sandbox limits | Always if any risk exists (most repos) |
| Directory / architecture map | Where key modules live, non-obvious structure | Large or non-standard layouts |

Use a mermaid diagram instead of prose wherever a section describes structure or flow a table/list can't capture cleanly: directory/module maps, service or data-flow architecture, CI/release pipelines, branch/PR decision trees. Keep tables for flat facts (commands, conventions) — reserve diagrams for topology and process.

## Minimal Template

```markdown
# AGENTS.md

## Project overview
<one paragraph>

## Setup commands
- Install deps: `<command>`
- Start dev server: `<command>`

## Build & test commands
- Build: `<command>`
- Test: `<command>`
- Lint: `<command>`

## Code style
- <convention 1>
- <convention 2>

## PR instructions
- Title format: `<format>`
- Always run `<lint cmd>` and `<test cmd>` before committing.
```

## Greenfield Workflow

For a repo with little or no code yet:

1. **Start minimal.** Write Project overview + intended stack + setup commands only; skip sections with nothing real to say yet.
2. **Commit instructions with the scaffolding that makes them true.** When you add `package.json` scripts or a CI workflow, update `AGENTS.md` in the same commit — never describe a command before it exists.
3. **Don't invent conventions.** Omit Code style / Testing instructions until a linter, formatter, or test framework is actually wired up; a guessed convention is worse than no section.
4. **Add nesting only at the second package.** A single-package repo needs no nested files; split into per-package `AGENTS.md` once the monorepo actually has ≥2 packages with diverging commands.
5. **Ask if Claude compatibility is needed.** If yes, pair every `AGENTS.md` (root and nested) with a sibling `CLAUDE.md` containing only `@AGENTS.md` (see [anatomy-and-discovery.md](anatomy-and-discovery.md#migrating-legacy-files)).
6. **Re-verify after each milestone.** Re-run every listed command after major scaffolding changes (framework upgrade, build tool swap) before considering the file done.

## Brownfield Workflow

For a repo with an existing codebase, README, CI, and possibly legacy agent files:

1. **Mine existing sources first** — README.md, CONTRIBUTING.md, `.github/workflows/*`, `Makefile`/`package.json` scripts, and any legacy files (`CLAUDE.md`, `.cursor/rules`, `copilot-instructions.md`). Reconcile overlaps into one canonical `AGENTS.md` (see [anatomy-and-discovery.md](anatomy-and-discovery.md#migrating-legacy-files)).
2. **Verify every command by running it** on a clean checkout — CI YAML and docs frequently drift from what actually works.
3. **Document observed conventions, not aspirational ones.** Derive Code style from the actual linter config and existing code, not a style guide nobody enforces.
4. **Nest per package for monorepos**, covering only what differs from root (see [anatomy-and-discovery.md](anatomy-and-discovery.md#monorepo-nesting)).
5. **Migrate legacy files.** Rename + symlink `AGENT.md`. Ask if Claude compatibility is needed; if yes, pair every `AGENTS.md` (root and nested) with a sibling `CLAUDE.md` containing only `@AGENTS.md` instead of symlinking it (see [anatomy-and-discovery.md](anatomy-and-discovery.md#migrating-legacy-files)).
6. **Treat it as living documentation.** Update `AGENTS.md` in the same PR as any change to build/test/lint tooling; a stale instruction is a bug.

## Staying Project-Agnostic

This skill authors the *process* for writing an `AGENTS.md`, not its content. Never bake a specific language, framework, or package manager into the generated file — discover the target repo's actual stack (or, for greenfield, its stated intended stack) and reflect only that.
