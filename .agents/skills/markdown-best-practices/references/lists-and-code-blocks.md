# Lists & Code Blocks

## List Markers

Pick one unordered-list marker (`-`, `*`, or `+`) and use it for every list item at the same level throughout a document — don't mix them:

```markdown
<!-- Wrong -->
* Item 1
- Item 2
+ Item 3

<!-- Right -->
- Item 1
- Item 2
- Item 3
```

For ordered lists, either number every item `1.` (renders as an auto-incrementing list — easiest to maintain, since inserting an item never requires renumbering) or number them truly sequentially (`1.`, `2.`, `3.`). Don't skip or restart numbers.

## Indentation

- Indent nested list items by exactly 2 spaces under their parent marker.
- Sibling items at the same list level must share identical indentation — a stray extra space breaks how the parser groups them:

```markdown
<!-- Wrong: misaligned sibling -->
- Item 1
  - Nested item
   - Misaligned nested item

<!-- Right -->
- Item 1
  - Nested item
  - Nested item
```

## Spacing After Markers

Exactly one space between a list marker and its text:

```markdown
<!-- Wrong -->
-  Two spaces
-No space

<!-- Right -->
- One space
```

## Blank Lines Around Blocks

Lists and fenced code blocks each need a blank line before and after them, except at the very start/end of a document or when a code block is nested inside a list item's own content:

```markdown
<!-- Wrong -->
Some text
- List item
- List item
Some more text

<!-- Right -->
Some text

- List item
- List item

Some more text
```

## Fenced Code Blocks

- Always specify a language after the opening fence: `` ```bash ``, `` ```json ``, `` ```yaml ``, `` ```python ``. Use `` ```text `` for plain text, pseudocode, or sample output that isn't real code — never leave the fence untagged.
- Prefer fenced code blocks (triple backticks) over indented code blocks — fences are unambiguous, easy to nest inside lists, and clearly mark where code starts and ends.
- Use backtick fences (`` ``` ``) consistently. Reserve tilde fences (`~~~`) only for the rare case where the code content itself needs to contain a backtick fence.
- Don't pad inline code spans with extra spaces: `` `code` ``, not `` ` code ` ``.
