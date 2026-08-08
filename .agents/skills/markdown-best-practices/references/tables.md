# Tables

## Structure

Every row — header, separator, and each data row — must have the same number of cells:

```markdown
<!-- Wrong: second row has one cell too few, third has one too many -->
| Header | Header |
| --- | --- |
| Cell |
| Cell | Cell | Cell |

<!-- Right -->
| Header | Header |
| --- | --- |
| Cell | Cell |
| Cell | Cell |
```

## Pipe Style

Use a leading and trailing `|` on every row. It's the clearest, most portable form and avoids ambiguity with text immediately before or after the table.

## Column Padding — Pick One Style, Apply It To Every Row

Three padding styles all render identically. Pick one **per table** and use it consistently for the header, separator, and every data row:

| Style | Example row | When to use |
| --- | --- | --- |
| Tight | `\|Col\|Col\|` / `\|---\|---\|` | Short cell content, minimal diff noise |
| Compact | `\| Col \| Col \|` / `\| --- \| --- \|` | Default choice — one space of padding, easy to hand-edit even with long cell text |
| Aligned | cells padded so every `\|` lines up vertically down the column | Small tables with short, similar-length content you're willing to re-pad by hand on every edit |

Compact is the safest default: unlike Aligned, it never needs the whole table re-padded just because one cell's content got longer or shorter.

Never mix styles within one table — a padded header row with an unpadded separator row (or vice versa) is the most common way this breaks:

```markdown
<!-- Wrong: header is padded (Compact), separator is not (Tight) -->
| Skill | Description |
|---|---|
| name | text |

<!-- Right: Compact throughout -->
| Skill | Description |
| --- | --- |
| name | text |
```

## Spacing

A table needs a blank line before it and a blank line after it — text immediately following a table with no blank line is parsed as part of the table, not a new paragraph:

```markdown
<!-- Wrong -->
Some text
| Header | Header |
| --- | --- |
| Cell | Cell |
This line is swallowed into the table

<!-- Right -->
Some text

| Header | Header |
| --- | --- |
| Cell | Cell |

This line is a normal paragraph
```
