---
name: markdown-best-practices
description: "ALWAYS use this skill whenever creating, editing, or reviewing any Markdown (.md) file — README.md, SKILL.md, AGENTS.md, CONTRIBUTING.md, changelogs, or any other Markdown documentation — even for small edits. Write and review Markdown files for consistent, correctly-rendering formatting: heading hierarchy and spacing, list marker and indentation consistency, fenced code block languages and blank-line spacing, table pipe and column consistency, link and image conventions, and whitespace hygiene. USE FOR: authoring or reviewing README.md, SKILL.md, AGENTS.md, or any Markdown documentation; fixing inconsistent headings, lists, code fences, or tables; fixing issues reported by Markdown linters such as markdownlint; choosing link/image/emphasis styles; general Markdown formatting conventions. DO NOT USE FOR: non-Markdown formats (reStructuredText, AsciiDoc); prose tone or content-quality review; the SKILL.md authoring workflow itself (see create-skill); AGENTS.md content structure (see agents-md)."
---

# Markdown Best Practices

Markdown renders differently across parsers when formatting is inconsistent or incomplete. These conventions keep documents portable across GitHub, static-site generators, and editor previews.

## Anatomy

| File | Purpose |
| --- | --- |
| SKILL.md | Top pitfalls, workflow, reference table |
| [references/headings-and-structure.md](references/headings-and-structure.md) | Heading hierarchy, spacing, single-title rule |
| [references/lists-and-code-blocks.md](references/lists-and-code-blocks.md) | List markers/indentation, fenced code languages and spacing |
| [references/tables.md](references/tables.md) | Table pipe/column consistency, blank-line spacing |
| [references/links-images-and-whitespace.md](references/links-images-and-whitespace.md) | Link/image conventions, whitespace hygiene |

## Top Pitfalls

| Pitfall | Fix |
| --- | --- |
| Fenced code block with no language tag | Always tag it: `` ```bash ``, `` ```json ``, `` ```text `` for plain/pseudocode |
| Heading with no blank line above/below | Blank line before *and* after every heading |
| Heading levels skip (H1 → H3) | Increment by exactly one level at a time |
| More than one H1 in a document | Exactly one H1 as the title; everything else H2+ |
| Table rows padded inconsistently | Pick one column style and use it for every row, including the separator |
| Mixed list markers (`-`, `*`, `+`) in one document | Pick one marker and use it throughout |
| List/table/code block missing surrounding blank lines | Blank line before and after each block |
| Bare URL with no link syntax | Wrap it: `<https://...>` or `[text](https://...)` |
| Trailing whitespace or hard tabs | Strip trailing spaces (except an intentional 2-space line break); use spaces, not tabs |
| Generic link text ("click here", "link") | Use descriptive text, e.g. "the setup guide" |

## Workflow: Writing New Markdown

1. Start with a single H1 title; every other heading is H2 or deeper, incrementing by one level at a time.
2. Add a blank line before and after every heading, list, table, and fenced code block.
3. Tag every fenced code block with a language (`bash`, `json`, `yaml`, `text`, ...).
4. Pick one style each for list markers, emphasis (`*`/`_`), and table pipe padding — apply it document-wide.
5. Give links descriptive text and images alt text; wrap bare URLs.
6. End the file with exactly one trailing newline; no trailing whitespace.

## Workflow: Reviewing Existing Markdown

1. Scan headings top to bottom: one H1, no level skips, no duplicates, blank lines around each.
2. Scan lists and tables for mixed markers, ragged indentation, or inconsistent pipe padding.
3. Scan fenced code blocks for a missing language tag.
4. Scan links for bare URLs, empty destinations, and generic text.
5. Strip trailing whitespace and collapse multiple blank lines to one.

## Reference Files

| File | Load When |
| --- | --- |
| [references/headings-and-structure.md](references/headings-and-structure.md) | Structuring or fixing a document's heading hierarchy |
| [references/lists-and-code-blocks.md](references/lists-and-code-blocks.md) | Writing/reviewing lists or fenced code blocks |
| [references/tables.md](references/tables.md) | Writing/reviewing Markdown tables |
| [references/links-images-and-whitespace.md](references/links-images-and-whitespace.md) | Writing/reviewing links, images, or general whitespace |
