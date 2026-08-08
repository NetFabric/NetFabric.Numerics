# Codex, Gemini CLI, and OpenCode

## OpenAI Codex CLI

| Surface | Location or control |
| --- | --- |
| Home | `$CODEX_HOME`, default `~/.codex` |
| Rollout sessions | `$CODEX_HOME/sessions/YYYY/MM/DD/*.jsonl` |
| Archived sessions | `$CODEX_HOME/archived_sessions/` in current versions |
| Prompt history | `$CODEX_HOME/history.jsonl` when persistence is enabled |
| Plaintext log | Configure `log_dir`; output is `<log_dir>/codex-tui.log` |
| Rust verbosity | `RUST_LOG`, for example `RUST_LOG=codex_core=debug` |
| Login diagnostics | `codex-login.log` in affected/current login flows |
| TUI recording | `CODEX_TUI_RECORD_SESSION` and `CODEX_TUI_SESSION_LOG_PATH` in builds that expose them |

Do not assume `codex-tui.log` always exists. Current behavior requires an explicit `log_dir` for plaintext file logging. Newer versions also use a bounded SQLite log store and provide a `codex-state-logs` query client; discover the database/schema for the installed version and query read-only.

Codex supports OTEL logs, traces, and metrics through its configuration. Prefer the documented OTEL schema for durable automation and JSONL rollouts for session reconstruction. Treat rollout schemas as version-sensitive.

## Gemini CLI

| Surface | Location or control |
| --- | --- |
| Debug UI | `gemini --debug` or `-d`; F12 opens the debug console |
| Debug file | Set `GEMINI_DEBUG_LOG_FILE` in versions that expose it |
| Sessions | `~/.gemini/tmp/<project_hash>/chats/session-*.jsonl` by default |
| Session inventory | `gemini --list-sessions` |
| Local telemetry | Set `telemetry.target: "local"` and `telemetry.outfile`, or environment equivalents |
| Retention | `general.sessionRetention` settings; current default maximum age is 30 days |

`GEMINI_CLI_HOME` changes the user-level root. Local file telemetry is the recommended debugging target. Environment overrides include `GEMINI_TELEMETRY_ENABLED`, `GEMINI_TELEMETRY_TRACES_ENABLED`, `GEMINI_TELEMETRY_TARGET`, `GEMINI_TELEMETRY_OTLP_ENDPOINT`, `GEMINI_TELEMETRY_OTLP_PROTOCOL`, `GEMINI_TELEMETRY_OUTFILE`, and `GEMINI_TELEMETRY_LOG_PROMPTS`.

Telemetry can include `session.id`, `installation.id`, approval mode, and authenticated email. Current docs list `logPrompts` as enabled by default when telemetry itself is enabled, so explicitly disable it when content is unnecessary.

## OpenCode

| Surface | Location or control |
| --- | --- |
| Logs | `~/.local/share/opencode/log/`; current docs retain the newest 10 |
| Verbosity | `opencode --log-level DEBUG` |
| Terminal output | `opencode --print-logs` |
| Session list | `opencode session list --format json` |
| Session export | `opencode export --sanitize <sessionID>` |
| Database | `opencode db path`; query through `opencode db` or read-only SQLite |
| Data | `~/.local/share/opencode/`, including project session/message storage |

On Windows, official docs use `%USERPROFILE%\.local\share\opencode`. Source builds may honor XDG variables. Prefer `opencode db path` and CLI exports over hard-coded storage internals because OpenCode has changed storage implementations.

OpenCode Desktop adds UI/main-process logs and state separate from the CLI sidecar. Use its built-in debug-log export, then inspect the archive before sharing.
