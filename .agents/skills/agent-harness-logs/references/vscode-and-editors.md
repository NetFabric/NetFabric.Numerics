# VS Code and Editors

## GitHub Copilot in VS Code

Start with supported UI surfaces because VS Code profile, remote-host, web, and Insiders layouts differ.

1. Open **View: Toggle Output** and select **GitHub Copilot**, **GitHub Copilot Chat**, or the relevant extension-host channel.
2. Run **Developer: Open Logs Folder** for the current VS Code instance.
3. Run **Developer: Show Logs...** and inspect **Extension Host**, **Window**, and **Shared Process** when startup or activation failed.
4. Reproduce once, note the timestamp, then search only the newest matching log directory.
5. For extension failures, use **Help: Report Issue** and choose the extension when appropriate.

Stable VS Code desktop discovery candidates are listed in [path-matrix.md](path-matrix.md). Remote SSH, WSL, dev containers, Codespaces, browser clients, profiles, and portable mode can move or split logs.

## Direct Copilot Chat Debug Sessions

Current VS Code Copilot Chat builds can produce session-scoped JSONL diagnostics under workspace/profile storage. This is an internal troubleshooting surface, not a stable public schema. A session may include:

| File or event | Typical role |
| --- | --- |
| `main.jsonl` | Main event stream |
| `models.json` | Model metadata |
| `system_prompt_*.json` | Captured system prompts; highly sensitive |
| `tools_*.json` | Tool definitions or details |
| Child JSONL files | Subagent event streams |
| `discovery`, `tool_call`, `llm_request`, `agent_response`, `user_message`, `subagent` | Common event categories in current builds |
| `spanId`, `parentSpanId` | Parent-child correlation when present |

Always inspect the actual keys first. Build-specific event names and files can change.

## Cursor, Windsurf, Kiro, and Other Editors

Use a product-supported sequence:

1. Open its Output/Logs panel and choose the agent or extension channel.
2. Use its **Open Logs Folder**, diagnostics bundle, issue reporter, or support command.
3. Separate editor renderer/main-process logs from extension-host and remote-agent logs.
4. Check whether the agent is a bundled service, a VS Code extension, or a separate CLI; each owns different files.
5. If documentation does not publish a path, report the path as discovered for that product version and OS.

Do not present a VS Code-derived directory as authoritative for another editor. Proprietary products can relocate, rename, rotate, or sanitize logs without a public compatibility promise.
