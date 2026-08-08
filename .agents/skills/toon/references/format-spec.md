# Format Spec

Read-only reference for understanding/reviewing TOON text. Don't hand-encode from this — use [cli-and-libraries.md](cli-and-libraries.md).

## The Four Forms

| Form | When | Example |
|------|------|---------|
| Inline | Array of primitives | `tags[3]: admin,ops,dev` |
| List | Array that isn't uniform objects | `items[2]:` then `- ` per element |
| Tabular | Array of uniform objects (same keys, primitive/uniform-nested values) | `items[2]{sku,qty}:` then one row per element |
| Keyed tabular | Object whose values are uniform objects | `users[2:]{age,city}:` then one row per entry, keyed |

## Root Forms

A document root can be an object (most common, fields at depth 0), an array (`[N]:` or `[N]{fields}:` at depth 0), or a single primitive.

## Objects

```yaml
id: 123
name: Ada
user:
  id: 123          # nested object: one extra indent level (default 2 spaces)
```

- Indentation replaces braces; one space after `:`.
- Empty object at root → empty document; nested empty object → `key:` alone.
- **Keyed tabular**: object with ≥2 entries whose values are uniform objects collapses:
  ```yaml
  users[2:]{age,city}:
    alice: 30,Berlin
    bob: 25,Oslo
  ```
  `[2:]` = keyed header (colon marks it keyed) + entry count; each row is `entrykey: cell,cell,…`.

## Arrays

Arrays always declare length: `[N]`.

- **Inline** (primitives): `tags[3]: admin,ops,dev`
- **Tabular** (uniform objects):
  ```yaml
  items[2]{sku,qty,price}:
    A1,2,9.99
    B2,1,14.5
  ```
  Requires identical field sets, ≥1 key, every column primitive or uniform-nested. Empty `{}` elements or mixed shapes fall back to list form.
- **Nested field groups** (uniform sub-object column folds into header):
  ```yaml
  orders[2]{id,customer{name,country},total}:
    1,Ada,DK,99
    2,Bob,UK,149
  ```
- **List** (mixed/non-uniform), hyphen per element one indent deeper:
  ```yaml
  items[3]:
    - 1
    - a: 1
    - text
  ```
- **Arrays of arrays**: `- [2]: 1,2` per inner array on the hyphen line.
- **Empty arrays**: `items: []` (fields) or `[]` (root); legacy `items[0]:` still decodes.

## Array Headers

`key[N<delimiter?>]<{fields}>:` — `N` = non-negative length (helps detect truncation); delimiter absent → comma, `\t` → tab, `|` → pipe; `{fields}` for tabular arrays only. Delimiter is scoped to the header that declares it; object `key: value` lines always use the document delimiter regardless of a surrounding array's delimiter.

## Comments

Decoders strip any line whose first non-space char is `#` before parsing. Full-line only (mid-line `#` is ordinary content). Encoders never emit comments; a string starting with `#` is always quoted so encoder output never reads as a comment.

## Quoting

A string must be quoted if it: is empty; has leading/trailing whitespace; equals `true`/`false`/`null` (case-sensitive); looks numeric (`"42"`, `"-3.14"`, `"05"`); contains `:`, `"`, `\`, brackets/braces, or a control char; contains the active delimiter; equals/starts with `-` or `#`. Otherwise unquoted — Unicode, emoji, and internal spaces are safe unquoted.

## Escape Sequences (quoted strings/keys only)

| Char | Escape |
|------|--------|
| `\` | `\\` |
| `"` | `\"` |
| newline | `\n` |
| CR | `\r` |
| tab | `\t` |
| other U+0000–U+001F | `\uXXXX` |

`\x`, `\0`, `\b` and lone-surrogate `\uXXXX` are always rejected.

## Type Conversions

Numbers emit in canonical decimal form (`-0` → `0`); exponent form allowed outside a small carve-out range. Non-JSON types (`NaN`, `Infinity`, dates, `Set`/`Map`, etc.) are normalized per-implementation before encoding — see [cli-and-libraries.md](cli-and-libraries.md) for the per-language table. Decoders accept both decimal and exponent input; a forbidden leading zero (`"05"`) decodes as a string.

## Full Spec

[github.com/toon-format/spec](https://github.com/toon-format/spec/blob/main/SPEC.md) is normative; this file is a condensed reading aid.
