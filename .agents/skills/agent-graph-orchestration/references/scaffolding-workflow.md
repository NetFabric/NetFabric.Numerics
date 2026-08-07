# Scaffolding Workflow (Copilot CLI Example)

Concrete steps to turn a graph design into working `.agent.md` files on Copilot CLI, delegating every node to `copilot-cli-custom-agents`. This assumes the user has already confirmed Copilot CLI as the target harness (see [harness-implementation.md](harness-implementation.md#always-ask-first-never-assume) — always ask before reaching this point). The same shape applies to any no-native-graph harness — substitute its own agent-authoring skill.

For Claude Code specifically, substitute `claude-code-custom-agents` for `copilot-cli-custom-agents`, `.claude/agents/*.md` for `.agent.md`, and the `Agent` tool for `task` throughout this file — but two things do **not** carry over as-is (see [harness-implementation.md](harness-implementation.md#copilot-cli-vs-claude-code-what-doesnt-carry-over) for both): step 2's `user-invocable: false` instruction (no equivalent field there; approximate visibility instead via `tools: Agent(<squad>-a, <squad>-b, ...)` on the coordinator plus the naming/description convention, and say explicitly that this doesn't stop a user from `@`-mentioning an internal node directly), and the naming table's "display label" row below — on Claude Code the frontmatter `name:` field *is* the dispatch identifier (what `Agent(...)` and hooks match on), not a free-form label, so it must equal the prefixed kebab-case ID (e.g. `name: <squad>-orchestrator`), never a human-friendly title.

## 1. Choose a squad prefix (multiple squads coexist in one project)

A project can host more than one graph-pattern squad at once. Pick a short, project-unique `<squad>` prefix *before* authoring anything, and check `.github/agents/` (or `.claude/agents/`) for an existing collision first. Apply that prefix to every resource the squad owns, not just agent filenames:

| Resource | Convention | Reason |
| --- | --- | --- |
| Orchestrator agent ID/filename | `<squad>-orchestrator` | Obvious in directory listings |
| Node/specialist agent ID/filename | `<squad>-<node-id>` (node-id from the design's node table) | Traceable back to the graph diagram |
| Tribunal review orchestrator | `<squad>-review-orchestrator` | Distinguishes it from the outer graph's orchestrator |
| Tribunal reviewers | `<squad>-reviewer-a`, `<squad>-reviewer-b` | Pairs with the provider-diversity constraint in model-selection.md |
| Agent frontmatter `name:` (display label) | `"<Squad> Orchestrator"`, `"<Squad> Reviewer A"`, etc. | `name:` is a separate, human-facing label from the filename/ID (frontmatter-reference.md) — prefixing it too avoids two squads showing indistinguishable entries anywhere it's rendered |
| Custom `mcp-servers:` keys declared in a node's frontmatter | `<squad>-<server-name>` | Two squads each declaring a generic key like `search` would otherwise collide when both merge into the same MCP config |
| Shared scratch/plan files on disk (the hand-off mechanism from delegation-and-squads.md) | `/memories/session/<squad>-plan.md` | Two orchestrators writing an unprefixed `plan.md` would overwrite each other |

`~/.copilot/settings.json`'s `subagents.agents.<name>` overrides are keyed by agent filename/ID, so prefixing filenames already prevents collisions there for free.

## 2. Author each node — enforce least privilege and visibility

For every node in the design, invoke `copilot-cli-custom-agents` to create its `.agent.md`. Two requirements are non-negotiable, not just conventions:

- **`user-invocable: false` on every node except the orchestrator.** This is the field the CLI actually provides for "hide from users, keep dispatchable" (frontmatter-reference.md) — it hides the node from the `/agent` picker while leaving it reachable via `task(agent_type=...)`. Never use `disable-model-invocation` for this: it also blocks the orchestrator's own `task()` dispatch, since the CLI has no "only orchestrator X" exception.
- **`tools:` scoped to the minimum that node's role needs** — no `task`/`list_agents` on a leaf specialist unless it is itself a nested orchestrator (e.g. a tribunal's review-orchestrator), no `edit`/`shell`/`create` on a pure reviewer/classifier node.

Even with both applied, remember the documented limitation from delegation-and-squads.md: there's no hard allowlist, so any agent elsewhere in the project with `task` in its `tools:` could still name an internal node directly. Keep specialist `description`s scoped to their role in this squad (not generic user-facing phrasing) to minimize the chance of unrelated auto-delegation picking them up.

## 3. Author the orchestrator

Invoke `copilot-cli-custom-agents` once more for the orchestrator itself:

- `tools:` excludes `edit`/`shell`/`bash` so it can't do the work itself, only `read`, `search`, `task`, `list_agents`
- Leave `user-invocable` at its default (`true`) — it is the one node in the squad users should select directly
- Prompt body must state, in order, every edge as an instruction — see the translation table below

## 4. Translate edges into orchestrator instructions

| Graph edge type | Orchestrator prompt instruction pattern |
| --- | --- |
| Simple `[A, B]` | "Dispatch to `<squad>-a`. Take its result and dispatch to `<squad>-b`, including A's full output in B's prompt." |
| Fan-out `[A,B]`, `[A,C]` | "After A completes, dispatch to `<squad>-b` and `<squad>-c` in parallel, each with A's output." |
| AND join `[B,D]`, `[C,D]` | "Do not dispatch to `<squad>-d` until both B and C have returned. Include both outputs in D's prompt." |
| Conditional `{source, target, handler}` | "If A's result contains/implies `<condition>`, dispatch to B; otherwise dispatch to C." (spell out the actual condition text) |
| Cyclic `[Reviewer, Writer]` bounded | "If the reviewer's result says revision is needed, dispatch back to the writer with the reviewer's feedback. Repeat at most 3 times, then dispatch to the publisher regardless." |

## 5. Scaffold a tribunal review node (if the design calls for one)

When a node in the design is a review step, treat it as a nested tribunal (see [topologies.md](topologies.md#5-tribunal-review-adversarial-cross-provider)) instead of a single reviewer agent:

1. Invoke `copilot-cli-custom-agents` for `<squad>-review-orchestrator`: `tools:` limited to `read`, `search`, `task`, `list_agents` (no `edit`/`shell`); prompt body instructs it to dispatch the same artifact to both reviewers in parallel, then merge findings (dedupe, keep single-reviewer findings, flag disagreements for escalation)
2. Invoke `copilot-cli-custom-agents` twice more for `<squad>-reviewer-a` and `<squad>-reviewer-b`: identical review-criteria prompt body, `tools:` limited to `read`/`search`, `user-invocable: false`
3. Assign each reviewer's `model:` per [model-selection.md](model-selection.md#cross-provider-diversity-for-tribunal-review) — different provider from each other and from whichever agent implemented the artifact under review
4. In the outer graph, wire the review-orchestrator as a single opaque node; its two reviewer children are invisible to the outer graph's edges

## 6. Respect the stateless-subagent constraint

Every `task` dispatch starts with empty context — the orchestrator's own conversation history is invisible to the node. Each dispatch's `prompt` must therefore restate the original task plus every upstream node's output the target node needs, exactly like the `prompt`-is-the-only-channel rule in `copilot-cli-custom-agents`. This replaces the automatic input-propagation that native `Graph` SDKs perform for you.

## 7. Validate

1. `list_agents` to confirm every node and the orchestrator are visible
2. Restart the CLI (or start a new session) so new/edited agent files load
3. Run the orchestrator on a sample task and confirm dispatch order matches the designed edges
4. For cyclic graphs, confirm the iteration bound actually terminates the loop
5. For tribunal review nodes, confirm the two reviewers' `model:` values resolve to genuinely different providers, not just different model names on the same provider
6. Open `/agent` and confirm only the orchestrator appears — every other node must be absent from the picker
7. If another squad already exists in the project, confirm no filename, `mcp-servers:` key, or `/memories/session/` file collides between the two prefixes
