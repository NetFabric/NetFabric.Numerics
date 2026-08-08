---
name: agent-harness-logs
description: "Find, inspect, query, redact, and troubleshoot logs, transcripts, sessions, and OpenTelemetry data from coding-agent harnesses on Windows, macOS, Linux, and WSL. USE FOR: GitHub Copilot in VS Code; Copilot CLI; Claude Code; OpenAI Codex; Gemini CLI; OpenCode; Cursor, Windsurf, Kiro, or other VS Code-family editors; locating log files; diagnosing tool or model failures; correlating turns, subagents, and tool calls; querying JSONL, plaintext, or SQLite without spending model tokens; preparing sanitized diagnostics. DO NOT USE FOR: application logs unrelated to coding agents; uploading raw logs; treating unstable transcript schemas as public APIs."
---

# Agent Harness Logs

Harnesses expose three different evidence surfaces. Identify the surface before querying it.

| Surface | Typical content | Best use |
| --- | --- | --- |
| Operational/debug log | Startup, extensions, network, retries, crashes | Why the harness failed |
| Session/transcript | Prompts, responses, tools, turn graph | What happened in one interaction |
| OpenTelemetry | Structured events, metrics, traces | Fleet analysis and durable observability |

## Workflow

1. Read [references/concepts-and-safety.md](references/concepts-and-safety.md); never upload or paste an unreviewed log.
2. Identify the harness and artifact in [references/path-matrix.md](references/path-matrix.md).
3. Load the matching reference: [VS Code and editors](references/vscode-and-editors.md), [Copilot CLI and Claude Code](references/copilot-and-claude.md), or [Codex, Gemini CLI, and OpenCode](references/codex-gemini-opencode.md).
4. Run `python3 scripts/harness_logs.py discover --toon`, then inventory the smallest relevant root with `--toon`.
5. Query locally using [references/query-recipes.md](references/query-recipes.md). Use `--toon` for model-bound output; stream source files and never read a large log wholesale.
6. Correlate with stable IDs when available: session, turn, prompt, request, tool-call, span, and parent-span IDs.
7. Redact to a new file, inspect the result manually, then share only the minimum slice needed.
8. State harness version, OS, artifact type, time window, and whether a path is documented or discovered.

## Script

`scripts/harness_logs.py` uses the Python standard library for queries and the official `@toon-format/cli` through `npx` when `--toon` is requested. It discovers candidate roots, inventories files, streams regex searches, summarizes JSONL fields, runs read-only SQLite queries, and creates best-effort redacted copies. Structured commands retain JSON/JSONL when `--toon` is omitted. The script never invokes a model.

## References

| File | Load when |
| --- | --- |
| [references/concepts-and-safety.md](references/concepts-and-safety.md) | Handling sensitive data, large files, or unstable schemas |
| [references/path-matrix.md](references/path-matrix.md) | Locating artifacts across operating systems |
| [references/vscode-and-editors.md](references/vscode-and-editors.md) | Troubleshooting Copilot in VS Code or VS Code-family editors |
| [references/copilot-and-claude.md](references/copilot-and-claude.md) | Inspecting Copilot CLI or Claude Code |
| [references/codex-gemini-opencode.md](references/codex-gemini-opencode.md) | Inspecting Codex, Gemini CLI, or OpenCode |
| [references/query-recipes.md](references/query-recipes.md) | Running deterministic, zero-token queries |
| [references/sources.md](references/sources.md) | Verifying claims or updating version-sensitive details |