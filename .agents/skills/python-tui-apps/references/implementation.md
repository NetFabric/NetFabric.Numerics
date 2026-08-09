# Implementation

## Architecture

```text
arguments + terminal capabilities
              |
         mode selection
          /         \
 Textual renderer   stream renderer
          \         /
       shared operations
       events + result
```

| Layer | Owns | Must Not Own |
| --- | --- | --- |
| Operations | Validation, domain work, cancellation, progress events, result | Textual widgets, terminal escape codes |
| TUI renderer | Screens, focus, bindings, reactive state, notifications | Business decisions |
| Stream renderer | Ordered messages, Rich tables, final summary, stderr errors | Prompts, cursor movement, animation |
| Entry point | Argument parsing, capability detection, renderer selection, exit code | Domain work |

Represent progress as typed events such as `Started`, `Advanced`, `Warning`, `Failed`, and `Completed`. Both renderers consume the same events, which prevents behavior drift and makes stream output useful to models.

## CLI Contract

Every Python skill script with an interactive workflow must support:

```text
tool.py [--no-tui] [--force-tui] [--no-color] [domain arguments...]
```

| Option/state | Behavior |
| --- | --- |
| Default + TTY | Run Textual |
| `--no-tui` | Run stream renderer; no prompts, cursor control, spinners, or live rewrites |
| Redirected stdin/stdout | Run stream renderer automatically |
| `--force-tui` | Attempt Textual despite auto-detection; fail clearly if unsupported |
| `--no-color` or `NO_COLOR` | Remove color while retaining labels, symbols, and structure |
| Conflicting mode flags | Reject with argument-parser error |

Use `sys.stdin.isatty()` and `sys.stdout.isatty()` for capability detection. Do not infer model usage from environment names. The explicit `--no-tui` contract is stable across harnesses.

## Entry-Point Pattern

```python
from __future__ import annotations

import argparse
import os
import sys

from rich.console import Console


def use_tui(args: argparse.Namespace) -> bool:
    if args.no_tui:
        return False
    if args.force_tui:
        return True
    return sys.stdin.isatty() and sys.stdout.isatty()


def main() -> int:
    parser = argparse.ArgumentParser()
    mode = parser.add_mutually_exclusive_group()
    mode.add_argument("--no-tui", action="store_true")
    mode.add_argument("--force-tui", action="store_true")
    parser.add_argument("--no-color", action="store_true")
    args = parser.parse_args()

    if use_tui(args):
        return WorkflowApp().run() or 0

    no_color = args.no_color or "NO_COLOR" in os.environ
    console = Console(no_color=no_color, force_interactive=False)
    error_console = Console(stderr=True, no_color=no_color, force_interactive=False)
    return run_stream(console, error_console)


if __name__ == "__main__":
    raise SystemExit(main())
```

Keep stdout for requested results or machine-consumable data. Send diagnostics and errors to stderr. If the script offers JSON, make it a separate `--format json` mode with no decoration.

## Textual Structure

| Feature | Pattern |
| --- | --- |
| Composition | Build stable widget structure in `compose()` |
| State | Use reactive attributes; update widgets from state changes |
| Long work | Use workers; keep the message loop responsive; expose cancellation |
| Styling | Put reusable styles in `.tcss`; use variables for semantic colors |
| Navigation | Add visible bindings for quit, back, help, and primary actions |
| Feedback | Use notifications for transient events and persistent regions for actionable errors |

Dependencies: use `textual` for the interactive app and `rich` for stream rendering. Add `textual-plotext` only when plots materially improve decisions.

## Stream UX

Stream mode remains designed, not degraded. Print a short title/context line, one durable line per phase or significant event, warnings with remediation, and a final status plus artifact paths or next action. Avoid repeated progress lines for every item; throttle updates by count or elapsed work.
