# Sources

Prefer installed-version help and current official documentation. Repository source links support implementation details that public docs omit; those details are version-sensitive.

| Harness | Authoritative sources |
| --- | --- |
| VS Code and Copilot | [GitHub Copilot troubleshooting](https://docs.github.com/en/copilot/troubleshooting-github-copilot/troubleshooting-github-copilot-chat-in-your-ide), [VS Code diagnostics](https://github.com/microsoft/vscode/wiki/Native-Crash-Issues), bundled VS Code Copilot Chat `troubleshoot` skill for internal JSONL schema |
| Copilot CLI | [github/copilot-cli](https://github.com/github/copilot-cli), installed `copilot --help`, CLI changelog/release notes |
| Claude Code | [CLI reference](https://code.claude.com/docs/en/cli-reference), [Monitoring](https://code.claude.com/docs/en/monitoring-usage), [Sessions](https://code.claude.com/docs/en/sessions), [Troubleshooting](https://code.claude.com/docs/en/troubleshooting) |
| Codex | [openai/codex](https://github.com/openai/codex), [Codex configuration](https://learn.chatgpt.com/docs/config-file/config-reference), installed `codex --help` |
| Gemini CLI | [Configuration](https://geminicli.com/docs/reference/configuration/), [Telemetry](https://geminicli.com/docs/cli/telemetry/), [Session management](https://geminicli.com/docs/cli/session-management/), [google-gemini/gemini-cli](https://github.com/google-gemini/gemini-cli) |
| OpenCode | [Troubleshooting](https://opencode.ai/docs/troubleshooting/), [CLI reference](https://opencode.ai/docs/cli/), [anomalyco/opencode](https://github.com/anomalyco/opencode) |

## Updating This Skill

1. Check the installed harness version and built-in help.
2. Verify paths and flags against official docs or the tagged source for that version.
3. Label repository-only behavior as source-backed and version-sensitive.
4. Keep proprietary editor paths as discovery guidance unless the vendor publishes them.
5. Retest the script using synthetic plaintext, JSONL, and SQLite fixtures.
