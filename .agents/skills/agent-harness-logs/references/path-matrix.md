# Path Matrix

`~` means the current user's home directory. Environment variables and harness settings override defaults. A **discovery** path is a useful search candidate, not a promised public contract.

| Harness | Artifact | Default or discovery location | Confidence |
| --- | --- | --- | --- |
| Copilot CLI | Process logs | `$COPILOT_HOME/logs/`, default root `~/.copilot` | Documented/source-backed |
| Copilot CLI | Current session state | `$COPILOT_HOME/session-state/` | Documented/source-backed |
| Copilot CLI | Legacy session state | `$COPILOT_HOME/history-session-state/` | Version-specific legacy |
| Claude Code | State root | `~/.claude/` | Documented |
| Claude Code | Transcripts | `~/.claude/projects/*/*.jsonl` | Documented |
| Claude Code | Debug output | Use `--debug-file`; otherwise discover under the configured debug-log directory | Documented command |
| Codex | State root | `$CODEX_HOME`, default `~/.codex` | Documented |
| Codex | Sessions | `$CODEX_HOME/sessions/YYYY/MM/DD/*.jsonl` | Documented/source-backed |
| Codex | Prompt history | `$CODEX_HOME/history.jsonl` | Source-backed |
| Codex | Plaintext log | `<log_dir>/codex-tui.log`; configure `log_dir` | Documented/source-backed |
| Gemini CLI | State root | `$GEMINI_CLI_HOME/.gemini`, default `~/.gemini` | Documented |
| Gemini CLI | Project state | `~/.gemini/tmp/<project_hash>/` | Documented/source-backed |
| Gemini CLI | Session chats | Project state `chats/session-*.jsonl` | Source-backed |
| Gemini CLI | Local telemetry | Path set by `telemetry.outfile` or `GEMINI_TELEMETRY_OUTFILE` | Documented |
| OpenCode | Logs | `~/.local/share/opencode/log/` | Documented |
| OpenCode | Sessions/data | `~/.local/share/opencode/project/.../storage/` or database returned by `opencode db path` | Documented; version-sensitive |
| VS Code Copilot | Host/extension logs | Open with **Developer: Open Logs Folder** and inspect Copilot output channels | Discovery workflow |
| VS Code Copilot Chat | Direct agent debug sessions | Profile/workspace storage under a session-specific debug-log directory | Internal/version-specific |

## Platform Notes

| Platform | Notes |
| --- | --- |
| Windows | `~` resolves from the user profile. OpenCode documents `%USERPROFILE%\.local\share\opencode`. VS Code stable logs are commonly discoverable under `%APPDATA%\Code\logs`. |
| macOS | VS Code stable logs are commonly discoverable under `~/Library/Application Support/Code/logs`. CLI homes remain under `~` unless overridden. |
| Linux | Respect `XDG_DATA_HOME` and `XDG_CONFIG_HOME` where the harness supports XDG. VS Code stable logs are commonly discoverable under `${XDG_CONFIG_HOME:-~/.config}/Code/logs`. |
| WSL | A CLI launched inside WSL uses Linux paths. A Windows-hosted editor uses Windows paths. Determine which process owns the failing operation. |
| Containers/SSH | Inspect the machine or container where the harness process runs; editor UI logs and remote extension-host logs may live on different hosts. |

For VS Code derivatives, replace the product directory only after confirming it through the product's own **Open Logs Folder**, Output, or diagnostics command. Do not infer Cursor, Windsurf, Kiro, or VSCodium paths from branding alone.
