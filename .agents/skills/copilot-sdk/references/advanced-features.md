# Advanced Features

## Hooks

Hooks intercept session lifecycle points. Register via the `hooks` field on session config.

| Hook | Trigger | Typical use |
|---|---|---|
| `onPreToolUse` | Before a tool executes | Approve/deny/modify args, add context, suppress output |
| `onPostToolUse` | After a successful tool execution | Transform/redact results, logging |
| `onPostToolUseFailure` | After a tool execution that failed | Inject retry guidance, log failures |
| `onUserPromptSubmitted` | User sends a message | Modify or filter the prompt |
| `onUserPromptTransformed` | After runtime prompt transformation | Inspect/replace the model-facing content |
| `onSessionStart` | Session begins | Inject `additionalContext`, configure session |
| `onSessionEnd` | Session ends | Cleanup, analytics |
| `onErrorOccurred` | An error happens | Custom error handling |
| `onAgentStop` | Top-level agent naturally stops | Validate completion or request another turn |

`onPreToolUse` returns `{ permissionDecision: "allow" | "deny" | "ask", permissionDecisionReason?, modifiedArgs?, additionalContext?, suppressOutput? }` (or `null`/`undefined` to allow unchanged).

## Custom agents (sub-agent delegation)

Pass `customAgents` on session config; each needs `name` + `prompt`, plus optionally `displayName`, `description`, `tools` (scoped tool allowlist), and its own `mcpServers`. The runtime auto-delegates a user request to the best-matching agent, runs it in an isolated context, and streams `subagent.*` lifecycle events back to the parent session. For dispatching many sub-agents in parallel instead of one delegated match, see Fleet Mode below.

```typescript
customAgents: [{
    name: "researcher",
    tools: ["grep", "glob", "view"],
    prompt: "You are a research assistant. Analyze code and answer questions. Do not modify any files.",
}]
```

## MCP servers

Wire external tool providers via `mcpServers` on session config, keyed by name.

| Type | Transport | Fields |
|---|---|---|
| `local` (stdio) | Subprocess | `command`, `args`, `env`, `cwd`, `tools` (`["*"]`/`[]`/list), `timeout` |
| `http` | Remote HTTP/SSE | `url`, `headers`, `tools` |

```typescript
mcpServers: {
    "github": { type: "http", url: "https://api.githubcopilot.com/mcp/", headers: { Authorization: "Bearer ${TOKEN}" }, tools: ["*"] },
}
```

## Skill directories

`skillDirectories: ["./skills/code-review"]` loads every `SKILL.md`-containing folder into session context — same skill format this repo's `create-skill` skill teaches you to author, just consumed at runtime instead of by an IDE agent.

## Plugin directories

A plugin bundles skills + hooks + MCP servers + custom agents behind one manifest, loaded once instead of wired per-extension:

```text
my-plugin/
├── plugin.json          # manifest (or use a root SKILL.md alone)
├── hooks.json
├── .mcp.json
├── agents/<name>.md
└── skills/<name>/SKILL.md
```

Load with `--plugin-dir <path>` passed as an extra CLI arg via the language's `RuntimeConnection` (repeat the flag for multiple plugins). Use plugin directories once you have 3+ related extensions shipping together; for one-off additions just use `mcpServers`/`hooks`/`customAgents` directly. Manifest may also live at `.github/plugin.json`.

## Fleet mode

Experimental: dispatches multiple sub-agents **in parallel** for decomposable work (multi-file refactors, per-module reviews, independent research). Start via the session RPC namespace: `session.rpc.fleet.start({ prompt })`. Avoid for sequential/dependent steps or small tasks a single agent finishes quickly.

## Session persistence

Pass an explicit `sessionId` when creating a session to make it resumable later (`client.resumeSession(sessionId, ...)`); without one, the SDK assigns a random ID and the session can't be resumed. State (history, tool state, planning context) is persisted to disk automatically once a `sessionId` is set.

## Session limits

Set `sessionLimits: { maxAiCredits: N }` on create/resume to cap AI Credit spend for the session's current accounting window. Enforcement checks after each model call returns, so one response can still exceed the cap before the *next* call is blocked.

## Steering & queueing

`send(prompt, { mode: "immediate" })` steers the agent mid-turn (course-correct without aborting); `send(prompt, { mode: "enqueue" })` queues the message for after the current turn completes. Default `sendAndWait` has no mode option — it's for one-shot, non-overlapping prompts.
