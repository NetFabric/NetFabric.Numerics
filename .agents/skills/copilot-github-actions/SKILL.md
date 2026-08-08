---
name: copilot-github-actions
description: "Automate GitHub Copilot CLI in GitHub Actions workflows. USE FOR: installing/running `copilot -p` in workflow steps, choosing GITHUB_TOKEN vs a personal access token (COPILOT_GITHUB_TOKEN) for authentication, setting the copilot-requests write permission, minimal-permission tool allowlisting (--allow-tool, --deny-tool, --no-ask-user, -s/--silent, --output-format=json), triggers (schedule, workflow_dispatch, push), the recommended GitHub Agentic Workflows (gh-aw) alternative to direct CLI invocation, and security/cost considerations (fork PR prompt-injection risk, billing attribution, cost centers). DO NOT USE FOR: embedding the Copilot agent runtime into your own application (use copilot-sdk instead); general GitHub Actions authoring unrelated to Copilot; the Copilot code review or PR features outside of CLI automation."
---

# Copilot CLI + GitHub Actions

Run `copilot` (the GitHub Copilot CLI) as a step in a GitHub Actions workflow to automate AI-powered tasks in CI/CD — summaries, reports, reviews, scaffolding.

## Core concepts

| Concept | Description |
|---|---|
| Workflow pattern | Trigger → checkout/setup → install CLI → authenticate → run `copilot -p PROMPT` |
| Programmatic mode | `-p`/`--prompt` runs one prompt non-interactively and exits; no TTY needed |
| `GITHUB_TOKEN` auth | Built-in Actions token; no secrets to manage; needs `copilot-requests: write` permission |
| PAT auth | Fine-grained PAT with "Copilot Requests" permission, passed via `COPILOT_GITHUB_TOKEN` secret |
| Token precedence | `COPILOT_GITHUB_TOKEN` > `GH_TOKEN` > `GITHUB_TOKEN` |
| GitHub Agentic Workflows (`gh-aw`) | Recommended layer over raw CLI invocation; adds guardrails for automated/untrusted contexts |
| Least privilege | Scope `permissions:`, use `--allow-tool`/`--deny-tool` instead of `--allow-all`/`--yolo` |

## Minimal example (direct CLI invocation)

```yaml
name: Daily summary
on:
  workflow_dispatch:
  schedule:
    - cron: '30 17 * * *'
permissions:
  contents: read
  copilot-requests: write
jobs:
  daily-summary:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v6
        with: { fetch-depth: 0 }
      - run: npm install -g @github/copilot
      - name: Run Copilot CLI
        run: |
          copilot -p "Summarize today's commits, with links, under 100 words. Write to summary.md" \
            --allow-tool='shell(git:*)' --allow-tool=write --no-ask-user -s
          cat summary.md >> "$GITHUB_STEP_SUMMARY"
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
```

## Workflow: automate a task

1. Decide direct CLI invocation vs. GitHub Agentic Workflows (recommended for most cases) → [gh-aw-vs-direct.md](references/gh-aw-vs-direct.md)
2. Choose auth: `GITHUB_TOKEN` (org policy required) or a PAT via `COPILOT_GITHUB_TOKEN` → [authentication.md](references/authentication.md)
3. Write the prompt/step with minimal permissions and a scoped tool allowlist → [cli-invocation-patterns.md](references/cli-invocation-patterns.md)
4. Review triggers and cost attribution before enabling on fork-PR events or org-wide → [security-and-cost.md](references/security-and-cost.md)

## Reference Files

| File | Load When |
|---|---|
| [references/authentication.md](references/authentication.md) | Setting up GITHUB_TOKEN vs PAT auth, org policy, token precedence |
| [references/cli-invocation-patterns.md](references/cli-invocation-patterns.md) | Writing the workflow step: prompt options, tool allowlists, output capture, shell patterns |
| [references/gh-aw-vs-direct.md](references/gh-aw-vs-direct.md) | Deciding between raw `copilot` steps and GitHub Agentic Workflows |
| [references/security-and-cost.md](references/security-and-cost.md) | Fork PR risk, prompt injection, least privilege, billing/cost centers |
