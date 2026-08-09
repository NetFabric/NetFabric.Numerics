---
name: ripgrep
description: "Search files and code with ripgrep (`rg`) after preferring available code-indexing tools. Use when: finding literal text or regex matches; listing searchable files; filtering by glob or file type; checking whether `rg` is installed; installing ripgrep on macOS, Linux, or Windows; replacing legacy text-search commands in agent workflows. ALWAYS prefer an available fresh code index for symbols, references, call paths, architecture, and impact analysis, then use `rg` for text search. NEVER use `grep`. DO NOT USE FOR: structural code questions already answered by an available index; modifying matched files; searching binary formats without a text preprocessor."
---

# Ripgrep

## Anatomy

| File | Purpose | Target Size |
| --- | --- | --- |
| `SKILL.md` | Search policy, availability check, quick reference | <100 lines |
| [references/installation.md](references/installation.md) | Install and verify `rg` across platforms | <200 lines |
| [references/usage.md](references/usage.md) | Search patterns, filtering, output, and diagnostics | <200 lines |

## Search Policy

Use the first applicable route:

1. Use an available, fresh code index for symbols, definitions, references, implementations, call paths, architecture, dependencies, dead code, or change impact. Examples: codebase-memory, language-server, IDE symbol, or repository index tools.
2. Use `rg` for exact text, regex, filenames, configuration values, logs, generated text, or when the index cannot answer the question.
3. If `rg` is unavailable, install it or report the blocker. Never fall back to `grep`.

For codebase-memory, prefer its one-shot CLI and verify index freshness before querying:

```bash
command -v codebase-memory-mcp && codebase-memory-mcp --version
codebase-memory-mcp cli index_status --project <project-name>
```

## Availability

```bash
if command -v rg >/dev/null 2>&1; then
  rg --version
else
  printf '%s\n' 'ripgrep (rg) is not installed or is not on PATH.'
fi
```

See [installation](references/installation.md) when the check fails.

## Quick Reference

| Need | Command |
| --- | --- |
| Search recursively | `rg 'pattern' [path]` |
| Search literal text | `rg -F 'literal text' [path]` |
| Ignore case / smart case | `rg -i 'pattern'` / `rg -S 'pattern'` |
| Match whole words | `rg -w 'pattern'` |
| Include / exclude globs | `rg 'pattern' -g '*.cs' -g '!bin/**'` |
| Include / exclude types | `rg 'pattern' -tcs` / `rg 'pattern' -Tjson` |
| List searchable files | `rg --files [path]` |
| List matching files only | `rg -l 'pattern' [path]` |
| Show context | `rg -n -C 2 'pattern' [path]` |
| Search hidden files | `rg --hidden 'pattern'` |
| Explain skipped files | `rg --debug 'pattern' [path]` |
| Show supported types | `rg --type-list` |
| Full command help | `rg --help` |

Quote patterns and globs so the shell does not expand them. Ripgrep respects `.gitignore`, `.ignore`, and `.rgignore` and skips hidden and binary files by default.

## Reference Files

| File | Load When |
| --- | --- |
| [references/installation.md](references/installation.md) | Checking availability; installing, upgrading, or verifying ripgrep |
| [references/usage.md](references/usage.md) | Building searches; filtering paths; handling ignores, regex, output, or exit status |
