# Copilot CLI and Claude Code

## GitHub Copilot CLI

| Surface | How to use it |
| --- | --- |
| Process logs | Inspect `$COPILOT_HOME/logs/`; `COPILOT_HOME` defaults to `~/.copilot` in current versions |
| Sessions | Inspect `$COPILOT_HOME/session-state/`; older versions may use `history-session-state/` |
| Diagnostics | Run `/diagnose` in the CLI and review the generated collection before sharing |
| Log verbosity | Use the current CLI's `log_level`/`logLevel` setting; confirm accepted values for the installed version |
| Telemetry | Current releases support OTEL aligned with GenAI semantic conventions; confirm exporter settings against the installed release/docs |

The CLI prunes old process logs in current releases. Session state and operational logs answer different questions. Search sessions by ID and modification time; search process logs by timestamp, severity, request ID, model, and tool name.

Version caveat: `--config-dir` existed in earlier releases and migrated toward `COPILOT_HOME`. Record `copilot --version` before applying a path rule.

## Claude Code

| Surface | How to use it |
| --- | --- |
| Debug mode | `claude --debug` or category filters such as `claude --debug='mcp,startup'` |
| Explicit debug file | `claude --debug-file /path/to/claude-debug.log` |
| Debug directory override | `CLAUDE_CODE_DEBUG_LOGS_DIR`; `--debug-file` takes precedence |
| Transcripts | `~/.claude/projects/*/*.jsonl` |
| Background sessions | `claude logs <id>` |
| Read-only health check | `claude doctor`; use `/doctor` inside an interactive session |
| Remove local project state | Preview with `claude project purge <path> --dry-run` before any deletion |

Transcript entries are internal and can change. Claude's documented OTEL event fields may correlate to transcript data, but such joins are version-specific.

### Claude OpenTelemetry

Enable with `CLAUDE_CODE_ENABLE_TELEMETRY=1`. Choose `OTEL_METRICS_EXPORTER` and/or `OTEL_LOGS_EXPORTER`; configure standard `OTEL_EXPORTER_OTLP_PROTOCOL`, `OTEL_EXPORTER_OTLP_ENDPOINT`, and headers. Tracing is beta and additionally requires `CLAUDE_CODE_ENHANCED_TELEMETRY_BETA=1` plus `OTEL_TRACES_EXPORTER`.

Useful stable event families include `claude_code.user_prompt`, `assistant_response`, `tool_result`, `tool_decision`, `api_request`, `api_error`, `mcp_server_connection`, and `compaction`. Correlate with `session.id`, `prompt.id`, `message.uuid`, `request_id`, `client_request_id`, and `tool_use_id` when present.

Content is mostly gated off by default. Treat these flags as explicit sensitivity escalations:

- `OTEL_LOG_USER_PROMPTS=1`
- `OTEL_LOG_ASSISTANT_RESPONSES=1`
- `OTEL_LOG_TOOL_DETAILS=1`
- `OTEL_LOG_TOOL_CONTENT=1`
- `OTEL_LOG_RAW_API_BODIES=1` or `file:<dir>`

Raw body files contain conversation history and tool results. Heap snapshots can contain conversations and credentials. Never attach either to a public report.
