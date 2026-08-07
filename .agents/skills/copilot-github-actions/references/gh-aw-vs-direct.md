# GitHub Agentic Workflows vs. Direct CLI Invocation

## Recommendation

GitHub's own docs recommend [GitHub Agentic Workflows](https://github.com/github/gh-aw) (`gh-aw`) over invoking `copilot` directly in a workflow `run:` step for most automation. Agentic Workflows use `GITHUB_TOKEN` by default and add guardrails designed for unattended/automated contexts that raw CLI steps don't have.

| | Direct CLI in `run:` | GitHub Agentic Workflows (`gh-aw`) |
|---|---|---|
| Setup | Install CLI + write prompt inline in YAML | `gh aw add-wizard <owner>/<repo>/<workflow>`, or author Markdown workflows |
| Source format | Raw `run: copilot -p "..."` steps | Markdown workflow (`.md`) + generated, committed `.lock.yml` compiled by `gh aw compile` |
| Guardrails | You configure `--allow-tool`, permissions, etc. yourself | Built-in guardrails/safe-outputs suited for automated environments |
| Engines | Copilot CLI only | Copilot, Claude Code, OpenAI Codex, or Google Gemini — selectable |
| Best for | Simple one-off steps, full control over exact CLI flags | Recurring automations, org-wide rollout, less manual security review per workflow |

## When direct invocation is fine

Simple, low-risk automation you fully control — e.g. a scheduled summary job with tightly scoped `--allow-tool` flags and no untrusted-input triggers (see [security-and-cost.md](security-and-cost.md)). See [cli-invocation-patterns.md](cli-invocation-patterns.md) for the exact syntax.

## Adopting `gh-aw`

1. Install the extension: `gh extension install github/gh-aw` (or the install script if auth fails), then `gh auth login --scopes repo,workflow`.
2. Add a workflow: `gh aw add-wizard <owner>/<repo>/<workflow-name>` (interactive) or `gh aw add` (non-interactive). Example: `gh aw add-wizard githubnext/agentics/daily-repo-status --engine copilot`.
3. Set up auth: for the Copilot engine, create a fine-grained PAT with **Copilot Requests: Read**, then `gh secret set COPILOT_GITHUB_TOKEN < token.txt`. This is separate from `GITHUB_TOKEN` because the agent needs elevated Copilot API access an ephemeral workflow token doesn't carry.
4. The wizard commits both the `.md` source and the generated `.lock.yml` (the compiled workflow GitHub Actions actually runs) to `.github/workflows/` — don't hand-edit the lock file; re-run `gh aw compile` after changing frontmatter.
5. Your workflow needs the `copilot-requests: write` permission in addition to whatever `gh-aw` frontmatter you configure.

## Further reading

`gh-aw` covers many more patterns than this skill duplicates: issue triage, PR review, release notes, docs automation, cross-repo orchestration, safe outputs, and sandboxing. See the [gh-aw reference docs](https://github.github.com/gh-aw/) when a task needs more than a single scheduled `copilot -p` step.
