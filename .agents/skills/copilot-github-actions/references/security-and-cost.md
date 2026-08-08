# Security and Cost

## Security considerations

Copilot CLI is agentic — it can read and modify repository contents. A compromised or misconfigured workflow can cause unintended changes, independent of which auth method you use.

To reduce risk:

* Prefer [GitHub Agentic Workflows](gh-aw-vs-direct.md) over raw `copilot` steps — they're designed with guardrails for automated use.
* Follow least privilege on `permissions:` — grant only `contents: read` (or whatever the task needs) plus `copilot-requests: write`; avoid `contents: write` unless the job actually commits/pushes.
* Scope tool access narrowly with `--allow-tool`/`--deny-tool` (see [cli-invocation-patterns.md](cli-invocation-patterns.md)) instead of `--allow-all`/`--yolo`.
* **Review triggers carefully.** Workflows that run on `pull_request` events from forks are at the highest risk: the PR author fully controls the diff/commit messages/issue text the CLI will read, making prompt injection possible if the CLI is invoked directly in that step. Prefer `pull_request_target` combined with strict guardrails, or `gh-aw`'s built-in fork protections, over a naive `pull_request` + direct CLI invocation.
* Invoking Copilot CLI directly in workflow steps gives it broad access to the workflow environment (secrets in scope, filesystem, network) — treat that step like any other privileged CI step.

## Billing and cost

| Auth method | Attribution |
|---|---|
| PAT | Billed to the PAT owner's individual Copilot seat/license |
| `GITHUB_TOKEN`, personally-owned repo | Billed to the repo owner's seat |
| `GITHUB_TOKEN`, org-owned repo | Metered directly to the organization (requires the org policy); **no per-user budget applies** |

Because org-attributed usage bypasses individual user budgets, manage spend at the org level instead:

* Configure **cost centers** to attribute and cap spend across groups of organizations.
* Monitor consumption via the organization's billing and usage dashboards.

## Fork PR risk in one line

If a workflow step runs `copilot -p "..."` against content an external contributor controls (a fork's PR title/body/diff) with write-capable tools allowed, that contributor can potentially steer the agent (prompt injection) into taking unwanted actions with the workflow's credentials — scope permissions and tools accordingly, or avoid direct CLI invocation on fork-triggered events entirely.
