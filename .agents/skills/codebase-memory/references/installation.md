# Installing, Checking, and Keeping CBM in Sync

Source of truth: [github.com/DeusData/codebase-memory-mcp](https://github.com/DeusData/codebase-memory-mcp) (README, `docs/CONFIGURATION.md`, `install.sh`/`install.ps1`).

## Check if it's installed

```bash
command -v codebase-memory-mcp && codebase-memory-mcp --version
```

`--version` prints `codebase-memory-mcp <semver>` and exits. No binary on `PATH` → not installed. If it's installed but not on `PATH`, check `$HOME/.local/bin`.

To see which agent/editor config files an install would touch (without writing anything), use the installer's dry run:

```bash
codebase-memory-mcp install --dry-run
```

## Install

One-line install (macOS / Linux):

```bash
curl -fsSL https://raw.githubusercontent.com/DeusData/codebase-memory-mcp/main/install.sh | bash
```

Add the 3D graph visualization UI variant:

```bash
curl -fsSL https://raw.githubusercontent.com/DeusData/codebase-memory-mcp/main/install.sh | bash -s -- --ui
```

Windows (PowerShell — inspect the script first, then unblock it since it was downloaded from the internet):

```powershell
Invoke-WebRequest -Uri https://raw.githubusercontent.com/DeusData/codebase-memory-mcp/main/install.ps1 -OutFile install.ps1
Unblock-File .\install.ps1
.\install.ps1            # add -ui for the graph UI
```

Also published on npm, PyPI, Homebrew, Scoop, Winget, Chocolatey, AUR (`codebase-memory-mcp-bin`), and `go install`. Package-manager installs only verify and cache the binary; running the native `install` command (which the shell/PowerShell scripts do for you) is what activates it account-wide and configures detected agent/editor clients.

The `install` command auto-detects installed coding agents/editors (Claude Code, Copilot CLI/VS Code, Cursor, Codex, Gemini CLI, Windsurf, Kiro, and more) and writes their MCP server entry plus durable instructions/skills/hooks where supported. Restart the agent afterward, then say "Index this project."

### Build from source

```bash
git clone https://github.com/DeusData/codebase-memory-mcp.git
cd codebase-memory-mcp
scripts/build.sh              # standard binary
scripts/build.sh --with-ui    # with graph visualization
```

## Update

Updates are run from the install script, not from inside the running binary (a running executable can't safely replace its own image, especially on Windows). Re-running `install.sh`/`install.ps1` is the update path — it stops the daemon, retires the old binary, installs the new one. Running `codebase-memory-mcp update` validates flags and prints the exact command to run rather than fetching anything itself. Installed via npm/pip? Use that package manager instead: `npm install -g codebase-memory-mcp@latest` / `pip install -U codebase-memory-mcp`.

## Check if a project's index is in sync

An indexed project can drift from the working tree (files changed since the last index). Check freshness with the `index_status` tool, always via the CLI:

```bash
codebase-memory-mcp cli index_status --project <name>
```

Reports whether the index is current or stale. A background watcher (`auto_watch`, default `true`) re-indexes incrementally on git-based change detection once a project is registered, so most sessions never need a manual reindex — `index_status` is how you confirm that instead of assuming it. For a fresh clone with no local index yet but a committed team-shared graph artifact (`.codebase-memory/graph.db.zst`), `index_repository` imports that artifact first and only incrementally indexes the local diff — check `index_status` afterward, not before, to see the result.

If the project is absent, stale, or not `ready`, index the current repository
and then confirm status again:

```bash
codebase-memory-mcp cli index_repository --repo-path "$PWD" --name <name>
codebase-memory-mcp cli index_status --project <name>
```

In a squad, the orchestrator must complete installation and this initial index
check before dispatching codebase work. Child agents still recheck freshness
after upstream edits, but they must not be the first line of defense for a
missing installation.

Before citing "no results found" as evidence of absence (e.g. dead code, no callers), call `check_index_coverage` for the paths in scope — a clean result means only "no recorded gap in the index," not full verification; read any flagged/skipped ranges directly from source before making a negative claim.

```bash
codebase-memory-mcp cli check_index_coverage --project <name> --paths src/foo.ts,src/bar.ts
```

## Config

```bash
codebase-memory-mcp config list
codebase-memory-mcp config get auto_index
codebase-memory-mcp config set auto_index true          # auto-index new projects on MCP session start
codebase-memory-mcp config set auto_index_limit 50000   # max files for auto-index
codebase-memory-mcp config set auto_watch false          # opt a session out of the background watcher
codebase-memory-mcp config reset auto_index
```

Key environment variables (full list in `docs/CONFIGURATION.md`): `CBM_CACHE_DIR` (default `~/.cache/codebase-memory-mcp` — where indexes and config live), `CBM_ALLOWED_ROOT` (confine `index_repository` to a directory), `CBM_LOG_LEVEL`, `CBM_WORKERS`.

## Uninstall

```bash
codebase-memory-mcp uninstall
```

Removes owned agent config entries, skills, hooks, instructions, and the binary. Existing graph indexes are listed and deleted only after confirmation. The install script placed beside the binary is reported (path printed), not deleted — it may not be owned by the uninstaller.
