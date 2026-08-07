# Setup and Auth

## Architecture

Your app talks to the SDK client; the SDK talks to the `copilot` CLI over JSON-RPC (stdio or TCP). What changes per setup is **where the CLI runs**, **how users authenticate**, and **how sessions are isolated**.

## Choose a path by persona

| Persona | Start with |
|---|---|
| 💻 Hobbyist / side project | [Bundled CLI](https://github.com/github/copilot-sdk/tree/main/docs/setup/bundled-cli.md) — SDK ships the CLI, just install and go |
| 🏢 Internal app developer | [GitHub OAuth](https://github.com/github/copilot-sdk/tree/main/docs/setup/github-oauth.md) for employee sign-in, then [Backend Services](https://github.com/github/copilot-sdk/tree/main/docs/setup/backend-services.md) |
| 🚀 ISV / product for customers | [GitHub OAuth](https://github.com/github/copilot-sdk/tree/main/docs/setup/github-oauth.md) or [BYOK](https://github.com/github/copilot-sdk/tree/main/docs/auth/byok.md), plus [Backend Services](https://github.com/github/copilot-sdk/tree/main/docs/setup/backend-services.md) |
| 🏗 Platform developer | [Backend Services](https://github.com/github/copilot-sdk/tree/main/docs/setup/backend-services.md) + [Multi-tenancy](https://github.com/github/copilot-sdk/tree/main/docs/setup/multi-tenancy.md) + [Scaling](https://github.com/github/copilot-sdk/tree/main/docs/setup/scaling.md) |

## Decision matrix

| Need | Guide |
|---|---|
| Fastest path to "it works" | Bundled CLI (default; SDK manages the process) |
| Own CLI binary or already-running instance | Local CLI (`RuntimeConnection` pointed at an external process/TCP) |
| Users sign in with GitHub | GitHub OAuth flow |
| Your own model keys (OpenAI, Azure, Anthropic, ...) | BYOK |
| Azure BYOK with no stored API keys | Azure Managed Identity + Microsoft Foundry |
| Run headless on a server | Backend Services (CLI over TCP) |
| Many concurrent users/tenants | `mode: "empty"` + per-session `gitHubToken` + `sessionFs` (Multi-tenancy) |
| Horizontal scaling | Scaling & Multi-Tenancy guide (session isolation patterns) |

## Authentication priority

When multiple credentials are present, the SDK resolves in this order:

1. Explicit SDK token passed in code
2. Direct Copilot API environment authentication
3. Environment variable GitHub token
4. Stored Copilot CLI credentials
5. GitHub CLI (`gh`) credentials

Server-to-server installation tokens (GitHub Actions, GitHub App) use the environment-variable path. For multi-user server mode, pass a **per-session** `gitHubToken` so each session runs under the correct GitHub identity — don't rely on one process-wide credential.

## Auth methods

| Method | Use case |
|---|---|
| Signed-in CLI (local) | Local dev; single user shares the developer's own Copilot auth |
| GitHub OAuth | Multi-user apps where each end user authenticates with their own GitHub account |
| Server-to-server tokens | Organization-attributed automation via GitHub Actions or GitHub App installation tokens |
| BYOK | Bring your own model API keys (OpenAI, Azure OpenAI, Anthropic, and more); you manage identity |
| Azure Managed Identity | BYOK against Microsoft Foundry without storing API keys |

## CLI requirement per language

Node.js, Python, and .NET SDKs bundle/auto-manage the `copilot` CLI. Go, Java, and Rust require the CLI to be installed and authenticated separately (`copilot --version` to verify) unless the host app implements its own CLI-bundling.

## Multi-tenancy essentials

* Set `mode: "empty"` to avoid loading a signed-in user's local Copilot config into a shared server process.
* Pass a per-session `gitHubToken` (or BYOK key) rather than a single global credential.
* Use `sessionFs` / isolated working directories so concurrent sessions don't share filesystem state.
* See scaling guide for session-per-process vs. shared-CLI-process tradeoffs.
