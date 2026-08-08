# Links, Images & Whitespace

## Links

- Correct syntax is `[text](url)` — never reversed as `(text)[url]`.
- No spaces just inside the brackets: `[text](url)`, not `[ text ](url)`.
- Give every link a real destination. `[text]()` and `[text](#)` are empty links that go nowhere.
- Write descriptive link text instead of generic text — screen readers often list links out of context, so "click here" or "link" gives no information on their own:

```markdown
<!-- Wrong -->
See [here](setup.md) for setup instructions.

<!-- Right -->
See the [setup instructions](setup.md).
```

- A same-document anchor link (`[text](#section)`) must match the target heading's generated anchor: lowercase the heading text, drop punctuation, and replace spaces with `-` (e.g. `## Setup Guide` → `#setup-guide`).
- Reference-style links (`[text][label]` with a `[label]: url` defined elsewhere) need the label defined exactly once. Delete unused reference definitions and don't leave a label undefined.
- Wrap bare URLs and email addresses in angle brackets or use proper link syntax — a naked URL isn't reliably turned into a clickable link by every renderer:

```markdown
<!-- Wrong -->
Visit https://example.com for details.

<!-- Right -->
Visit <https://example.com> for details.
```

## Images

Always include descriptive alt text — never leave it empty:

```markdown
<!-- Wrong -->
![](diagram.png)

<!-- Right -->
![Architecture diagram showing the request flow](diagram.png)
```

## Whitespace

- No trailing spaces at the end of a line, except exactly two spaces when a hard line break is intended.
- Use spaces for indentation, never hard tabs.
- Collapse multiple consecutive blank lines down to a single blank line.
- End every file with exactly one trailing newline — no trailing blank lines, and never omit the final newline.
- Don't pad emphasis markers with spaces: `**bold**`, not `** bold **`.

## Emphasis & Rules

- Pick one emphasis marker (`*italic*` or `_italic_`) and one strong marker (`**bold**` or `__bold__`), and use each consistently throughout a document.
- If a document uses horizontal rules, pick one style — `---` is the most portable — and use it consistently rather than mixing `---`, `***`, and `___`.
