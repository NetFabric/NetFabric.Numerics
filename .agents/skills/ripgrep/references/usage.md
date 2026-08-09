# Usage

## Core Searches

| Goal | Command |
| --- | --- |
| Regex in current tree | `rg 'class\s+Widget'` |
| Literal punctuation | `rg -F 'CreateChecked<T>(' src` |
| Multiple patterns | `rg -e 'first' -e 'second'` |
| Invert matches | `rg -v 'pattern'` |
| Whole line | `rg -x 'exact line'` |
| Match only | `rg -o 'pattern'` |
| Count matching lines | `rg -c 'pattern'` |
| Multiline match | `rg -U 'first\nsecond'` |
| PCRE2 features | `rg -P '(?<=prefix)value'` |

Use `-F` when the input is literal. Ripgrep patterns use regular expressions by default; PCRE2 look-around and backreferences require a build with PCRE2 support and `-P` or `--engine pcre2`.

## Scope and Filtering

```bash
rg 'pattern' src tests
rg 'pattern' -g '*.cs' -g '!**/obj/**'
rg 'pattern' -tcs -Tjson
rg --files -g '*.md'
rg --type-list
```

Later glob rules override earlier rules. Quote every glob. Use `-t<type>` to include a built-in type and `-T<type>` to exclude one.

## Ignore Behavior

By default, recursive searches respect `.gitignore`, `.ignore`, and `.rgignore`; skip hidden files and directories; skip binary files; and do not follow symbolic links.

| Need | Flag |
| --- | --- |
| Include hidden paths | `--hidden` |
| Ignore ignore-files | `--no-ignore` |
| Follow symbolic links | `--follow` |
| Treat binary as text | `--text` |
| Disable all filtering progressively | `-u`, `-uu`, `-uuu` |
| Diagnose filtering | `--debug` |

Use the narrowest override. Avoid `-uuu` unless ignored, hidden, and binary content are all intentionally in scope.

## Output for Humans and Tools

| Need | Command |
| --- | --- |
| Line and column | `rg -n --column 'pattern'` |
| Before / after context | `rg -B 2 -A 3 'pattern'` |
| Stable path ordering | `rg --sort path 'pattern'` |
| Null-delimited paths | `rg -l -0 'pattern'` |
| Structured event stream | `rg --json 'pattern'` |
| Cap long output lines | `rg -M 200 'pattern'` |

Sorting disables parallelism and may be slower. Prefer `--json` for programmatic consumers and null-delimited paths when filenames may contain whitespace or newlines.

## Exit Status

| Code | Meaning |
| --- | --- |
| `0` | At least one match found |
| `1` | No matches found |
| `2` | Error occurred |

Treat exit code `1` as a valid empty result, not a command failure.

## Diagnostics

1. Run `rg --debug 'pattern' path` when expected files are skipped.
2. Check shell quoting and use `-F` if regex metacharacters should be literal.
3. Add one narrow filter override such as `--hidden` or `--no-ignore`.
4. Run `rg --no-config ...` if `RIPGREP_CONFIG_PATH` may be changing behavior.
5. Run `rg --help` for the installed version's authoritative option list.

## Sources

- [Official ripgrep user guide](https://github.com/BurntSushi/ripgrep/blob/master/GUIDE.md)
- [Official ripgrep command reference](https://github.com/BurntSushi/ripgrep/blob/master/doc/rg.1.txt)
