# Query Recipes

Run from the skill directory. These commands are local and do not invoke a model.

## Portable Script

```bash
python3 scripts/harness_logs.py discover --toon
python3 scripts/harness_logs.py inventory ~/.copilot/logs --limit 20 --toon
python3 scripts/harness_logs.py search ~/.claude 'error|timeout|429|5[0-9]{2}' --ignore-case --redact --limit 50 --toon
python3 scripts/harness_logs.py jsonl-summary session.jsonl --field type --toon
python3 scripts/harness_logs.py sqlite state.sqlite 'SELECT name FROM sqlite_master WHERE type = "table"' --limit 50 --toon
python3 scripts/harness_logs.py redact input.log sanitized.log
```

Use `--toon` by default when returning results to a model; it uses the official `@toon-format/cli@4.1.1` package through `npx`. Omit the option for JSON/JSONL consumed by existing automation. The first TOON invocation may download the package into the npm cache and therefore requires Node.js, `npx`, and network access; later cached invocations remain local.

`jsonl-summary --field` accepts flattened paths such as `message.role` or `attributes.event.name`. First summarize likely discriminator fields, then search the resulting event names. The SQLite command opens the database read-only and permits only `SELECT`, `PRAGMA`, `WITH`, or `EXPLAIN`.

## Shell Tools

```bash
# Inventory without reading content
find LOG_ROOT -type f -print0 | xargs -0 stat -f '%m %z %N' | sort -nr | head -50  # macOS
find LOG_ROOT -type f -printf '%T@ %s %p\n' | sort -nr | head -50                 # Linux

# Stream JSONL event counts and selected failures
jq -r '.type // .event.name // "<missing>"' session.jsonl | sort | uniq -c | sort -nr
jq -c 'select((.level? == "error") or (.success? == false))' session.jsonl | head -100

# Search text with file names and line numbers
rg -n -i --max-count 100 'error|timeout|rate.?limit|429|5[0-9]{2}' LOG_ROOT

# Inspect SQLite schema before querying rows
sqlite3 -readonly state.sqlite '.tables'
sqlite3 -readonly state.sqlite '.schema target_table'
```

On Windows PowerShell:

```powershell
Get-ChildItem LOG_ROOT -File -Recurse |
  Sort-Object LastWriteTime -Descending |
  Select-Object -First 50 FullName, Length, LastWriteTime

Get-ChildItem LOG_ROOT -File -Recurse |
  Select-String -Pattern 'error|timeout|rate.?limit|429|5\d\d' |
  Select-Object -First 100 Path, LineNumber, Line
```

## Correlation Order

1. Session or conversation ID
2. Turn, prompt, or interaction ID
3. Request/client-request ID
4. Tool-use/call ID
5. Trace/span and parent-span IDs
6. Timestamp only as a fallback

Use exact IDs before broad text. Clock skew, retries, parallel tools, and subagents make timestamp-only reconstruction unreliable.

## SQLite Safety

Copy a live database only when the harness is stopped or use its supported query/export command. SQLite WAL files may hold recent rows not present in the main file. Never mutate a harness database to investigate it.
