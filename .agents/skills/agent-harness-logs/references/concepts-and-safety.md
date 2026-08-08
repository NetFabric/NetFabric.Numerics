# Concepts and Safety

## Choose the Right Artifact

| Question | Artifact |
| --- | --- |
| Did startup, auth, networking, an extension, or a subprocess fail? | Operational/debug log |
| Which prompt caused a tool call, response, or edit? | Session/transcript |
| How much time, cost, or token use occurs across users and sessions? | OpenTelemetry metrics/events |
| Where did latency occur in a turn? | OpenTelemetry traces or correlated request/tool timing |

A transcript is not merely a debug log. It can contain the full conversation, tool arguments and results, file contents, system prompts, and repository paths. An OTEL event stream may redact content by default but still include identity, model, timing, cost, and session identifiers.

## Safe Procedure

1. Record harness version and OS before interpreting a schema.
2. Inspect file names, sizes, and modification times before content.
3. Narrow by session ID, timestamp, event type, request ID, or error text.
4. Keep analysis local with `jq`, `rg`, PowerShell, SQLite, or the bundled script.
5. Redact into a different file. Never overwrite the only evidence copy.
6. Manually review the redacted result; pattern redaction is best effort, not proof of safety.
7. Share a minimal excerpt, not an entire state directory.

## Sensitive Fields

Assume these are sensitive even when a harness calls them diagnostics:

- Prompts, responses, system prompts, summaries, and hidden context
- Tool inputs/outputs, shell commands, file contents, diffs, and paths
- Authorization headers, cookies, API keys, OAuth tokens, and MCP credentials
- User email, account/organization IDs, installation IDs, and session IDs
- Repository names, remote URLs, issue/PR content, and environment variables
- Heap snapshots and raw API request/response bodies

## Schema Stability

Operational logs and transcript JSONL are often implementation details. Prefer documented CLI exports or OTEL schemas for automation. When querying internal JSONL:

- Probe keys before writing a filter.
- Tolerate missing and additional fields.
- Do not assume one harness's `type`, `role`, or `message` shape applies to another.
- Treat joins between transcript fields and telemetry IDs as version-specific unless documented.

## Large Files

Use streaming commands. Avoid `cat` on large files, loading complete JSONL into memory, opening SQLite as text, or passing raw logs to a model. Start with byte size, modification time, and line count; then inspect matching lines with a hard result limit.
