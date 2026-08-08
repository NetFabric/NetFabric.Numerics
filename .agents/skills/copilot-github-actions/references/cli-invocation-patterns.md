# CLI Invocation Patterns

## Full example workflow

```yaml
name: Daily summary
on:
  workflow_dispatch:
  schedule:
    - cron: '30 17 * * *'
permissions:
  contents: read
jobs:
  daily-summary:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v6
        with: { fetch-depth: 0 }
      - uses: actions/setup-node@v7
      - run: npm install -g @github/copilot
      - name: Run Copilot CLI
        env:
          COPILOT_GITHUB_TOKEN: ${{ secrets.PERSONAL_ACCESS_TOKEN }}
        run: |
          copilot -p "Review the git log for this repository and write a bullet point summary of all code changes made today, with links to each commit. Above the list, give a max-100-word description. Write to summary.md" --allow-tool='shell(git:*)' --allow-tool=write --no-ask-user
          cat summary.md >> "$GITHUB_STEP_SUMMARY"
```

## Key options for automation

| Option | Purpose |
|---|---|
| `-p PROMPT` / `--prompt PROMPT` | Run one prompt non-interactively, exit after completion |
| `-s` / `--silent` | Print only the agent response, no session metadata — best for capturing output in a variable |
| `--no-ask-user` | Disable the `ask_user` tool; required since there's no human to answer prompts |
| `--allow-tool=TOOL` | Grant permission for a specific tool/pattern without prompting (repeatable) |
| `--deny-tool=TOOL` | Explicitly block a tool/pattern; deny always wins over allow |
| `--allow-all-tools` / `--allow-all` / `--yolo` | Allow everything without confirmation — avoid outside sandboxes; may be centrally disabled |
| `--output-format=json` | Emit JSONL (one event per line) instead of text, for programmatic parsing |
| `--model=MODEL` | Pin a model explicitly for consistent behavior across runs |
| `--share=PATH` / `--share-gist` | Save the full session transcript to a file or a secret gist |

## Tool permission patterns

`--allow-tool`/`--deny-tool` take `Kind(argument)` patterns; omitting the argument matches all tools of that kind.

| Kind | Example |
|---|---|
| `shell` | `shell(git:*)` matches `git push`/`git pull`; `shell(git push)` matches only that exact command |
| `read` | `read`, `read(.env)` |
| `write` | `write`, `write(src/*.ts)` |
| `url` | `url(github.com)`, `url(https://*.api.com)` |
| `MCP-SERVER-NAME` | `MyMCP(create_issue)`, or bare `MyMCP` for all of that server's tools |

```bash
copilot --allow-tool='shell(git:*)' --deny-tool='shell(git push)' -p "..."
```

## Shell scripting patterns

```bash
# Capture output in a variable
result=$(copilot -p 'What Node.js version does this project require? Number only.' -s)

# Use in a conditional
if copilot -p 'Any TypeScript errors? Reply YES or NO.' -s | grep -qi "no"; then
  echo "clean"
fi

# Loop over files
for file in src/api/*.ts; do
  copilot -p "Review $file for error handling issues" -s --allow-tool=read >> review.md
done
```

## Tips

* Give precise, unambiguous prompts — name exact files/functions/changes.
* Quote prompts in single quotes to avoid shell interpretation of special characters.
* Always scope `--allow-tool`/`--allow-url` to what the task actually needs; avoid `--allow-all` outside a sandbox.
* Set `--model` explicitly for reproducible CI behavior instead of relying on the default.
