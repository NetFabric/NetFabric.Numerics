# Core Session API

## Client and session lifecycle

1. Construct a `CopilotClient` (optionally with `connection`, `mode`, auth options).
2. `start()` the client (spawns/attaches to the `copilot` CLI); some languages start implicitly on first use.
3. `createSession(config)` — one session per conversation.
4. Send prompts; read replies or subscribe to streaming events.
5. `stop()` the client / `disconnect()` the session when done (or use `await using` / `try-with-resources` / context managers where the language supports it).

## Session config fields

| Field | Purpose |
|---|---|
| `model` | Model ID, or `"auto"` to let the runtime choose |
| `streaming` | Emit incremental `*_delta` events as the reply is generated |
| `tools` | Custom tools available to this session (see below) |
| `availableTools` | Restrict which built-in/custom tools a session may use (multi-tenant safety) |
| `onPermissionRequest` | Callback deciding whether a tool call is allowed |
| `customAgents` | Sub-agent definitions (see advanced-features.md) |
| `mcpServers` | MCP server configs (see advanced-features.md) |
| `skillDirectories` | Directories of `SKILL.md` files to load into context |
| `hooks` | Lifecycle interceptors (see advanced-features.md) |
| `gitHubToken` | Per-session GitHub identity (multi-tenant deployments) |
| `sessionId` | Explicit ID for resuming/looking up a session later |

## Sending messages

| Method | Behavior |
|---|---|
| `sendAndWait(prompt)` | Sends and blocks until the final assistant reply; simplest for one-shot use |
| `send(prompt)` | Fire-and-forget; returns a message ID immediately, reply arrives via events |
| `send(prompt, { mode: "immediate" })` | **Steering** — injects into the turn the agent is currently running |
| `send(prompt, { mode: "enqueue" })` | **Queueing** — buffers the message until the current turn finishes |

Use `send` (not `sendAndWait`) whenever you also subscribe to streaming events, since `sendAndWait` already waits for completion internally.

## Permission handling

Every tool call (built-in or custom) can require a permission decision. Provide `onPermissionRequest` (or a hook's `permissionDecision` output) returning one of:

| Decision kind | Effect |
|---|---|
| `approve-once` | Allow this single call |
| `approve-for-session` | Allow this tool for the rest of the session, no more prompts |
| `approve-for-location` | Allow this tool for a specific path/scope |
| `approve-permanently` | Persist approval beyond the session (stored config) |
| `deny` | Reject the call |
| `ask` | Defer to a human-facing prompt (interactive CLIs) |

For unattended/server use, most samples pass an "approve all" convenience (`PermissionHandler.approve_all` in Python, `PermissionHandler.ApproveAll` in .NET/Java, `ApproveAllHandler` in Rust) — only safe when `availableTools` already scopes what the session can call.

## Streaming events

Every action emits a session event with a common envelope: `id`, `timestamp`, `parentId` (links to the previous event), `agentId` (set for sub-agent events), `ephemeral` (transient vs. persisted/replayable), `type`, `data`.

| Event type | Meaning |
|---|---|
| `assistant.message_delta` | Ephemeral streaming text chunk; accumulate for full content |
| `assistant.message` | Persisted, complete assistant message |
| `assistant.turn_start` / `assistant.turn_end` | Bounds of one agent turn |
| `tool.execution_start` / `tool.execution_complete` | A tool call began/finished |
| `session.idle` | Session finished processing and is waiting for input |
| `subagent.completed` | A custom-agent sub-agent finished (see advanced-features.md) |

Subscription methods: `session.on(handler)` for all events; `session.on(eventType, handler)` for one type (Node.js/TS only, with narrowed payload types); `session.subscribe()` returns a channel/stream to filter manually (Rust). All return an unsubscribe handle/function except where the language uses `try/finally`-style disposal.

## Custom tools

Define a tool with a name, JSON-schema parameters, and a handler; pass it in `tools` on session creation. Node.js uses a `defineTool(name, { description, parameters, handler })` helper; other languages provide an equivalent builder or attribute/derive macro (Rust uses `schemars`-derived structs, Java/.NET use POJOs/records with the SDK's tool-definition API). The model calls the tool by name with JSON args; your handler runs and its return value is sent back as the tool result.
