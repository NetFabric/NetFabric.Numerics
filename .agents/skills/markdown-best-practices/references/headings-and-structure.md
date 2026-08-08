# Headings & Structure

## Document Title

- The first line of a file SHOULD be a single H1 that serves as the document's title. An `<h1>`-equivalent image or badge is also acceptable for a README that leads with a logo.
- Use exactly one H1 per document — it's the title. Every subsequent heading is H2 or deeper.

## Heading Hierarchy

Increment by exactly one level at a time. Never skip a level.

```markdown
<!-- Wrong: skips H2 -->
# Heading 1
### Heading 3

<!-- Right: increments by one -->
# Heading 1
## Heading 2
### Heading 3
```

## Heading Style

- Use ATX style (`#`, `##`, `###`, ...) consistently. Avoid Setext style (underlining text with `===`/`---`) — it only supports two levels and reads worse in a diff.
- Exactly one space between the `#` markers and the heading text:

```markdown
<!-- Wrong -->
#Heading
##  Heading

<!-- Right -->
# Heading
## Heading
```

- Headings must start at column 0 (not indented), except inside a blockquote (`> # Heading`) — an indented heading is parsed as regular text, not a heading.

## Spacing

Every heading needs a blank line before it and a blank line after it, except at the very start or end of the file:

```markdown
<!-- Wrong -->
Some text
## Heading
More text

<!-- Right -->
Some text

## Heading

More text
```

Some parsers render a heading with no blank line before it as plain text instead — this isn't just a style preference.

## Content Rules

- Don't repeat identical heading text at the same level — parsers generate anchors from heading text, and duplicates collide. Repeating a heading under different parents is fine (e.g., a recurring "### Notes" subheading under each "## 1.0.0" / "## 2.0.0" release in a changelog).
- Don't end a heading with punctuation: `# Setup.` → `# Setup`. A trailing `?` is fine for FAQ-style headings.
- Don't fake a heading with bold or italic text used as a section break — use a real heading so tools can parse the document's structure:

```markdown
<!-- Wrong: emphasis instead of a heading -->
**Setup**

Steps go here.

<!-- Right -->
## Setup

Steps go here.
```
