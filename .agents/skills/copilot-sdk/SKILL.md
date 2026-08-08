---
name: copilot-sdk
description: "Embed the GitHub Copilot agent runtime into your own application using the Copilot SDK. USE FOR: CopilotClient/createSession setup, streaming responses, custom tools, permission handlers, hooks (pre/post-tool-use, session-start), custom agents and sub-agent delegation, MCP servers, skill and plugin directories, session persistence, fleet mode, steering/queueing, calling the SDK from skill scripts/ to run one bounded non-deterministic AI step inside an otherwise deterministic script, and choosing an auth/deployment path (CLI setup, backend services, multi-tenancy, GitHub OAuth, BYOK). Covers Node.js/TypeScript (@github/copilot-sdk), Python (github-copilot-sdk), Go (github.com/github/copilot-sdk/go), Rust (github-copilot-sdk crate), .NET (GitHub.Copilot.SDK), and Java (copilot-sdk-java). DO NOT USE FOR: authoring SKILL.md files consumed by VS Code Copilot (use create-skill instead); using the Copilot IDE extension or chat UI directly; general LLM/chat API integration unrelated to the Copilot SDK."
---

# Copilot SDK

Library for embedding the GitHub Copilot agent (via the `copilot` CLI over JSON-RPC) as the reasoning engine inside your own app, across six languages.

## Core concepts

| Concept | Description |
|---|---|
| `CopilotClient` | Top-level object; starts/stops the underlying CLI process |
| `Session` | One conversation; holds model, tools, hooks, agents, MCP servers |
| `sendAndWait` / `send` | Send a prompt and await the final reply, or fire-and-forget with event streaming |
| Custom tool | Function Copilot can call mid-conversation (name, JSON-schema params, handler) |
| Permission handler | Approves/denies tool calls (`approve-once`, `approve-for-session`, `deny`, `ask`) |
| Hook | Intercepts lifecycle points (`onPreToolUse`, `onPostToolUse`, `onSessionStart`, ...) |
| Custom agent | Named sub-agent with its own prompt + scoped tools, auto-delegated to |
| MCP server | External tool provider (stdio subprocess or HTTP) wired into a session |
| Skill directory | Folder of `SKILL.md` files injected into session context |
| Plugin directory | Bundle of skills/hooks/MCP servers/agents behind one manifest, loaded via `--plugin-dir` |

## Pattern: deterministic scripts, non-deterministic steps

A skill's `scripts/` automation doesn't have to be 100% deterministic. Have the script call the SDK for one bounded, ambiguous sub-step (classify input, pick a strategy, summarize a diff), then resume plain code for the rest — the AI call is scoped and its output validated/parsed like any other function result, so the surrounding pipeline stays testable and reproducible. See [create-skill](../../../apm_modules/netfabric/intelligentium/plugins/agent-authoring/.apm/skills/create-skill/SKILL.md)'s Scripts & Assets conventions for where this fits in a skill's folder layout.

## Language support

| Language | Package | Runtime | CLI required separately? |
|---|---|---|---|
| Node.js / TS | `@github/copilot-sdk` | Node 20+ | No (bundled) |
| Python | `github-copilot-sdk` | Python 3.11+ | No (bundled) |
| .NET | `GitHub.Copilot.SDK` | .NET 8+ | No (bundled) |
| Go | `github.com/github/copilot-sdk/go` | Go 1.24+ | Yes, unless app bundles it |
| Java | `com.github:copilot-sdk-java` | Java 17+ | Yes, unless app bundles it |
| Rust | `github-copilot-sdk` crate | Rust 1.94+ | Yes, unless app bundles it |

Verify any environment with `copilot --version` before writing code.

## Minimal session (Node.js; other languages in [language-quickstarts.md](references/language-quickstarts.md))

```typescript
import { CopilotClient } from "@github/copilot-sdk";

const client = new CopilotClient();
const session = await client.createSession({ model: "auto" });
const response = await session.sendAndWait({ prompt: "What is 2 + 2?" });
console.log(response?.data.content);
await client.stop();
```

## Workflow: build an app

1. Pick a deployment persona (hobbyist/internal/ISV/platform) and auth method → [setup-and-auth.md](references/setup-and-auth.md)
2. Install the SDK for your language, confirm `copilot --version` → [language-quickstarts.md](references/language-quickstarts.md)
3. Create client + session (`model`, `streaming`, `onPermissionRequest`), send prompts, subscribe to events → [core-session-api.md](references/core-session-api.md)
4. Layer in custom tools, hooks, custom agents, MCP servers, or skill/plugin directories as needed → [advanced-features.md](references/advanced-features.md)
5. For production, revisit auth/scaling (multi-tenancy, per-session tokens) → [setup-and-auth.md](references/setup-and-auth.md)

## Reference Files

| File | Load When |
|---|---|
| [references/setup-and-auth.md](references/setup-and-auth.md) | Choosing a setup path, configuring auth (OAuth, BYOK, server-to-server), scaling/multi-tenancy |
| [references/core-session-api.md](references/core-session-api.md) | Client/session lifecycle, sending messages, streaming events, custom tools, permission decisions |
| [references/advanced-features.md](references/advanced-features.md) | Hooks, custom agents, MCP servers, skill directories, plugin directories, fleet mode, persistence |
| [references/language-quickstarts.md](references/language-quickstarts.md) | Per-language install commands and hello-world snippets |
