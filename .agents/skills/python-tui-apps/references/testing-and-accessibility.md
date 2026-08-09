# Testing and Accessibility

## Test Matrix

| Area | Required checks |
| --- | --- |
| Operations | Success, validation failure, runtime failure, cancellation, partial result |
| Mode selection | TTY default, `--no-tui`, redirected I/O, `--force-tui`, conflicting flags |
| Stream output | Stable ordering, no prompt/animation, stdout vs stderr, no-color, exit codes |
| Keyboard | Tab/Shift+Tab, arrows where expected, Enter/Space activation, Escape/back, quit |
| Focus | Visible focus, logical order, focus restoration after modal/view changes |
| Layout | At least 120x40, 80x24, and the documented minimum size |
| States | Loading, empty, partial, warning, error, success, disabled |
| Color | Default theme, alternate/light theme when offered, monochrome/no-color meaning |

## Textual Tests

Use `App.run_test()` for headless interaction and `Pilot` to press keys, click controls, and resize the simulated terminal.

```python
import pytest

from tool.app import WorkflowApp


@pytest.mark.asyncio
async def test_keyboard_workflow_completes() -> None:
    app = WorkflowApp(fake_backend=True)
    async with app.run_test(size=(80, 24)) as pilot:
        await pilot.press("tab", "space", "enter")
        await pilot.pause()
        assert app.result.succeeded


@pytest.mark.asyncio
async def test_narrow_terminal_preserves_primary_action() -> None:
    app = WorkflowApp(fake_backend=True)
    async with app.run_test(size=(120, 40)) as pilot:
        await pilot.resize_terminal(60, 20)
        assert app.query_one("#run").display
```

Prefer behavioral assertions over snapshots. Add SVG snapshot tests for a few stable, high-risk screens: dense tables, narrow layouts, dialogs, and error states. Normalize timestamps, paths, random values, and terminal dimensions before snapshotting.

## Stream Tests

Run the entry point as a subprocess so terminal detection, descriptors, exit codes, and output channels are real. Assert that `--no-tui`:

- exits without waiting for input;
- emits no cursor-control or alternate-screen escape sequences;
- writes requested data to stdout and diagnostics to stderr;
- returns `0` for success and documented nonzero codes for failure/cancellation;
- remains understandable with color disabled;
- produces bounded output for large jobs.

## Accessibility Review

| Concern | Check |
| --- | --- |
| Keyboard-only use | Every action is reachable; no mouse-only hover action |
| Focus | Current focus is obvious in color and shape/border/text |
| Color vision | Status includes words/symbols; chart series differ beyond hue |
| Cognitive load | One primary task per view; concise labels; progressive disclosure |
| Screen size | No overlap/clipping at supported minimum; scrolling is discoverable |
| Time | Users can pause/cancel long work; no short-lived essential messages |
| Errors | Explain what failed, what was preserved, and how to recover |
| Animation | No decorative looping; provide static feedback when interaction is disabled |

## Release Gate

1. Run unit tests for shared operations.
2. Run headless TUI interaction tests at all supported sizes.
3. Run stream subprocess tests with pipes and `--no-color`.
4. Manually complete the main workflow using keyboard only.
5. Inspect each state in the default and limited-color themes.
6. Verify cancellation leaves the terminal usable and partial artifacts documented.
7. Confirm help output documents `--no-tui`, output channels, and exit codes.

Treat visual appeal as testable: hierarchy is clear at a glance, values align, labels fit, status colors are consistent, controls do not shift during updates, and every decorative element earns its space.
