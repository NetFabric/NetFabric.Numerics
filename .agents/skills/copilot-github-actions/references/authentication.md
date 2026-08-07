# Authentication

## Two options

| Method | How it works | Best for |
|---|---|---|
| `GITHUB_TOKEN` | Built-in Actions token; no secrets to create/rotate | Org-owned repos; recommended for automation at scale |
| Personal access token (PAT) | Fine-grained PAT with "Copilot Requests" permission, stored as a secret | Repos without the org policy enabled, or billing to a specific user's seat |

## Using `GITHUB_TOKEN`

Requires the org policy **"Allow use of Copilot CLI billed to the organization"** (enabled by default for orgs with Copilot CLI turned on — verify under org Copilot policy settings). Grant the workflow permission and pass the token as an env var:

```yaml
permissions:
  contents: read
  copilot-requests: write
jobs:
  copilot:
    steps:
      - run: npm install -g @github/copilot
      - run: copilot --allow-all -p "Summarize the changes in this commit"
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
```

`--allow-all`/`--yolo` (or targeted `--allow-tool` flags) is required since there's no user to answer interactive prompts. Requires a recent CLI version — update with `copilot update` or reinstall via npm.

## Using a PAT (`COPILOT_GITHUB_TOKEN`)

1. Create a fine-grained PAT at `github.com/settings/personal-access-tokens/new` with the **Copilot Requests** account permission (classic `ghp_` tokens are not supported).
2. Store it as an Actions repository secret (Settings → Secrets and variables → Actions), e.g. named `PERSONAL_ACCESS_TOKEN`.
3. Pass it as `COPILOT_GITHUB_TOKEN` so it doesn't collide with any `GITHUB_TOKEN` usage elsewhere in the workflow:

```yaml
- name: Run Copilot CLI
  env:
    COPILOT_GITHUB_TOKEN: ${{ secrets.PERSONAL_ACCESS_TOKEN }}
  run: copilot -p "..." --allow-tool='shell(git:*)' --allow-tool=write --no-ask-user
```

## Token precedence

`copilot login` checks environment-variable tokens in this order when no interactive login has been done: `COPILOT_GITHUB_TOKEN` → `GH_TOKEN` → `GITHUB_TOKEN`. Supported token types: fine-grained PATs (v2) with Copilot Requests, OAuth tokens from the Copilot CLI app, OAuth tokens from `gh`. Classic PATs are not supported.

## Billing

* PAT: AI credits draw from the token owner's Copilot seat; their license determines available models/features. Works in any repo, but ties usage to one person.
* `GITHUB_TOKEN` in a personally-owned repo: billed to that owner's seat.
* `GITHUB_TOKEN` in an org-owned repo: metered directly to the organization (requires the org policy above) — no individual user attribution, so per-user Copilot budgets don't apply. Use cost centers and org billing dashboards to track/limit this spend instead.

## GitHub Enterprise Cloud with data residency

Use `copilot login --host https://example.ghe.com` (or the equivalent host config) when authenticating against a data-residency GHE Cloud instance instead of `github.com`.
