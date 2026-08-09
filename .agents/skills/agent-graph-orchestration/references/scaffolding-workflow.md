# Scaffolding Workflow (Copilot CLI Example)

Concrete steps to turn a graph design into working agent files, delegating every node to whichever agent-authoring skill matches the harness the user selected. This assumes the user has already confirmed the target harness (see [harness-implementation.md](harness-implementation.md#always-ask-first-never-assume) — always ask before reaching this point, and never assume it's Copilot CLI just because this file uses it as its running example). Every step below that names `copilot-cli-custom-agents` means "whichever agent-authoring skill matches the confirmed harness" — swap in `claude-code-custom-agents` for Claude Code, or that harness's own equivalent skill for anything else (see [harness-implementation.md](harness-implementation.md#delegating-node-authoring-by-harness)); if no agent-authoring skill exists for the confirmed harness, say so explicitly rather than defaulting to Copilot CLI's shape.

For Claude Code specifically, substitute `claude-code-custom-agents` for `copilot-cli-custom-agents`, `.claude/agents/*.md` for `.agent.md`, and the `Agent` tool for `task` throughout this file — but three things do **not** carry over as-is (see [harness-implementation.md](harness-implementation.md#copilot-cli-vs-claude-code-what-doesnt-carry-over) for the first two): step 2's `user-invocable: false` instruction (no equivalent field there; approximate visibility instead via `tools: Agent(<squad>-a, <squad>-b, ...)` on the coordinator plus the naming/description convention, and say explicitly that this doesn't stop a user from `@`-mentioning an internal node directly); the naming table's "display label" row below — on Claude Code the frontmatter `name:` field *is* the dispatch identifier (what `Agent(...)` and hooks match on), not a free-form label, so it must equal the prefixed kebab-case ID (e.g. `name: <squad>-orchestrator`), never a human-friendly title; and every `list_agents` mention below — it's a Copilot CLI tool with no Claude Code equivalent, so drop it from a Claude Code node's `tools:` entirely rather than substituting anything in its place.

## 1. Choose a squad prefix (multiple squads coexist in one project)

A project can host more than one graph-pattern squad at once. Pick a short, project-unique `<squad>` prefix *before* authoring anything, and check `.github/agents/` (or `.claude/agents/`, or the confirmed harness's own agent directory) for an existing collision first. Apply that prefix to every resource the squad owns, not just agent filenames:

| Resource | Convention | Reason |
| --- | --- | --- |
| Orchestrator agent ID/filename | `<squad>-orchestrator` | Obvious in directory listings |
| Planner agent ID/filename | `<squad>-planner` | Mandatory node present in every squad (topologies.md#mandatory-baseline-squad-shape) |
| Node/specialist agent ID/filename | `<squad>-<node-id>` (node-id from the design's node table) | Traceable back to the graph diagram |
| Quality gate step ID | `<squad>-quality-gate` | Only when the design includes one; may not be a separate `.agent.md` at all (see step 6) |
| Publisher step ID | `<squad>-publisher` | Only when the design includes one (topologies.md#mandatory-baseline-squad-shape); a concrete post-review action (open a PR, update a tracking ticket, publish/deploy), not a validation step like the quality gate |
| Tribunal review orchestrator | `<squad>-review-orchestrator` | Distinguishes it from the outer graph's orchestrator |
| Tribunal reviewers | `<squad>-reviewer-a`, `<squad>-reviewer-b` | Pairs with the provider-diversity constraint in model-selection.md |
| Agent frontmatter `name:` (display label) | `"<Squad> Orchestrator"`, `"<Squad> Reviewer A"`, etc. | `name:` is a separate, human-facing label from the filename/ID on Copilot CLI (see that harness's own agent-authoring skill's `frontmatter-reference.md` — this does not carry over to Claude Code, see this file's intro) — prefixing it too avoids two squads showing indistinguishable entries anywhere it's rendered |
| Custom `mcp-servers:` keys declared in a node's frontmatter | `<squad>-<server-name>` | Two squads each declaring a generic key like `search` would otherwise collide when both merge into the same MCP config |
| Shared scratch/plan files on disk (the hand-off mechanism documented in the target harness's own agent-authoring skill, e.g. its `delegation-and-squads.md`) | `/memories/session/<squad>-plan.md` | Two orchestrators writing an unprefixed `plan.md` would overwrite each other |

`~/.copilot/settings.json`'s `subagents.agents.<name>` overrides are keyed by agent filename/ID, so prefixing filenames already prevents collisions there for free.

## 2. Author each node — enforce least privilege and visibility

For every node in the design, invoke the target harness's agent-authoring skill to create its agent file — `copilot-cli-custom-agents` (→ `.agent.md`) for Copilot CLI, `claude-code-custom-agents` (→ `.claude/agents/*.md`) for Claude Code, or that harness's own equivalent skill for anything else (see [harness-implementation.md](harness-implementation.md#delegating-node-authoring-by-harness)). Never hand-write the frontmatter/prompt body yourself regardless of harness. The mandatory planner node is authored the same way as any other specialist here — it's a regular LLM agent, unlike the quality gate (step 6), which is not. Two requirements are non-negotiable, not just conventions:

- **`user-invocable: false` on every node except the orchestrator.** This is the field Copilot CLI provides for "hide from users, keep dispatchable" (see `copilot-cli-custom-agents`' own `frontmatter-reference.md`) — it hides the node from the `/agent` picker while leaving it reachable via `task(agent_type=...)`. Never use `disable-model-invocation` for this: it also blocks the orchestrator's own `task()` dispatch, since the CLI has no "only orchestrator X" exception. Claude Code has no equivalent field at all — see this file's intro paragraph for the approximation there.
- **`tools:` scoped to the minimum that node's role needs** — no `task`/`list_agents` on a leaf specialist unless it is itself a nested orchestrator (e.g. a tribunal's review-orchestrator), no `edit`/`shell`/`create` on a pure reviewer/classifier node.

Even with both applied, remember the documented limitation in the target harness's own agent-authoring skill (e.g. `copilot-cli-custom-agents`' or `claude-code-custom-agents`' `delegation-and-squads.md`): there's no hard allowlist on Copilot CLI, so any agent elsewhere in the project with `task` in its `tools:` could still name an internal node directly (Claude Code's `tools: Agent(...)` allowlist is the one harness here that actually enforces this). Keep specialist `description`s scoped to their role in this squad (not generic user-facing phrasing) to minimize the chance of unrelated auto-delegation picking them up.

## 3. Author the orchestrator

Invoke the same target-harness agent-authoring skill once more for the orchestrator itself:

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

When the planner's decomposition produces multiple implementer subtasks, apply the **Fan-out** and **AND join** rows above between them by default — dispatch every implementer with no dependency on another implementer's output in parallel, and don't gate the quality gate/reviewer dispatch until all of them return. Only fall back to the **Simple** row's serial pattern for an implementer pair where one's task genuinely needs the other's output (see [topologies.md](topologies.md#implementer-stage-one-or-more-agents-parallel-when-independent)).

## 5. Scaffold a tribunal review node (if the design calls for one)

When a node in the design is a review step, treat it as a nested tribunal (see [topologies.md](topologies.md#5-tribunal-review-adversarial-cross-provider)) instead of a single reviewer agent:

1. Invoke the target harness's agent-authoring skill for `<squad>-review-orchestrator`: `tools:` limited to `read`, `search`, `task`, `list_agents` (no `edit`/`shell`); prompt body instructs it to dispatch the same artifact to both reviewers in parallel, then merge findings (dedupe, keep single-reviewer findings, flag disagreements for escalation)
2. Invoke the same skill twice more for `<squad>-reviewer-a` and `<squad>-reviewer-b`: identical review-criteria prompt body, `tools:` limited to `read`/`search`, `user-invocable: false`
3. Assign each reviewer's `model:` per [model-selection.md](model-selection.md#cross-provider-diversity-for-tribunal-review) — different provider from each other and from whichever agent implemented the artifact under review
4. In the outer graph, wire the review-orchestrator as a single opaque node; its two reviewer children are invisible to the outer graph's edges

## 6. Scaffold a quality gate node (if the design calls for one)

A quality gate is not an LLM agent — do not invoke the target harness's agent-authoring skill (`copilot-cli-custom-agents`, `claude-code-custom-agents`, or another harness's equivalent) to author reasoning/prompt content for it. It validates deterministically: it runs existing tools (test runner, linter, type-checker, schema validator, build) and reports pass/fail plus the raw tool output, with no model reasoning about the result. Realize it one of two ways:

1. **Orchestrator-inline step** (preferred): the orchestrator's own prompt body includes an instruction to run the tool directly (e.g. "run `<test command>`; if it introduces a regression, dispatch back to `<squad>-implementer` with the failure output") — no separate agent file at all.
2. **Minimal wrapper agent** (only when the harness needs every dispatch target to be an agent): author `<squad>-quality-gate` via the harness's agent-authoring skill with `tools:` restricted to exactly the deterministic check tool(s) — no `edit`, and a prompt body limited to "run the tool(s), report pass/fail and the raw output verbatim, do not editorialize or judge quality subjectively."

Either way, wire it between the last implementation node and the adversarial review stage. Capture a pre-edit baseline when repository-wide checks can already fail, distinguish new failure signatures from unchanged baseline failures, and route regressions through a bounded repair loop. After the bound is exhausted, dispatch the mandatory reviewer with the unresolved gate evidence; a quality gate may block approval or publishing, but must not suppress review after implementation.

## 7. Scaffold a publisher node (if the design calls for one)

Unlike the quality gate, a publisher commonly does need reasoning — drafting a PR description, summarizing what changed for a ticket comment — alongside tool calls (git/gh, an issue-tracker API/MCP server, a deploy command). Unless its job is a single fixed command with no drafting involved, author it via the target harness's agent-authoring skill like any other specialist, not as a deterministic-only step like the quality gate. Concrete responsibilities to scope into its prompt/tools, individually or combined:

- Open a pull request (commit, push, `gh pr create`/equivalent, write the PR description from the approved artifact and upstream context)
- Update a tracking ticket (Jira, GitHub Issues, Linear, ...) — status transition, a summary comment, linking the PR
- Publish/deploy to an environment (run a deploy command/pipeline, or dispatch to an existing deployment tool)

Scope its `tools:` to exactly what these actions need (e.g. `edit`/`shell` for git operations, the relevant MCP server for the ticket tracker) — no broader than the quality gate's or a reviewer's tools, and specifically no dispatch tool (`task`/`Agent`) unless it must itself delegate part of its own work. Wire it as the final node after the adversarial review passes; a quality-gate or review failure must never reach the publisher.

## 8. Respect the stateless-subagent constraint

Every subagent dispatch starts with empty context — the orchestrator's own conversation history is invisible to the node. Each dispatch's prompt must therefore restate the original task plus every upstream node's output the target node needs, exactly like the prompt-is-the-only-channel rule in the target harness's own agent-authoring skill (`copilot-cli-custom-agents`, `claude-code-custom-agents`, or equivalent). This replaces the automatic input-propagation that native `Graph` SDKs perform for you.

## 9. Validate

1. Use the target harness's agent-listing mechanism to confirm every node and the orchestrator are visible (e.g. `list_agents` on Copilot CLI; Claude Code has no equivalent listing tool — check the `.claude/agents/` and `~/.claude/agents/` directories directly instead)
2. Reload the node files per the target harness's own rules before testing — don't assume a blanket restart is always needed: Copilot CLI needs a CLI restart (or new session); Claude Code picks up new/edited files in an already-existing `.claude/agents/`/`~/.claude/agents/` within seconds with no restart, and only needs one if that directory didn't exist yet when the session started
3. Run the orchestrator on a sample task and confirm dispatch order matches the designed edges
4. Confirm the squad includes an orchestrator, a planner, and an adversarial (tribunal) review stage — all three are mandatory even if a quality gate is absent
5. If the design has multiple implementer nodes, confirm independent ones are actually dispatched in parallel (not serialized by default) and that the quality gate/reviewer dispatch waits on all of them (AND join)
6. For cyclic graphs, confirm the iteration bound actually terminates the loop
7. For tribunal review nodes, confirm the two reviewers' `model:` values resolve to genuinely different providers, not just different model names on the same provider
8. For a quality gate, confirm it spends no reasoning/tokens (tool-execution only), runs before the adversarial reviewer, uses a baseline when incoming failures are possible, and cannot terminate an implementation run before mandatory review
9. If a publisher is present, confirm it only dispatches after the quality gate (if any) and the adversarial review both pass, and that its `tools:` are scoped to its concrete post-review actions (PR, ticket update, deploy) with no dispatch tool unless it genuinely delegates
10. Confirm every node has an explicit `model:` ([model-selection.md](model-selection.md)); no node's cost tier exceeds the orchestrator's, unless a tribunal-diversity exception was deliberately surfaced to the user
11. Check the target harness's agent picker (e.g. Copilot CLI's `/agent`) and confirm only the orchestrator appears there — every other node must be absent, to whatever degree the harness supports that distinction (see [harness-implementation.md](harness-implementation.md#copilot-cli-vs-claude-code-what-doesnt-carry-over) for harnesses like Claude Code with no enforced equivalent)
12. If another squad already exists in the project, confirm no filename, `mcp-servers:` key, or `/memories/session/` file collides between the two prefixes
