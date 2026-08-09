---
name: python-tui-apps
description: "Create polished Python terminal user interfaces and skill scripts with Textual and Rich. Use when: building TUI apps; adding interactive terminal workflows; designing tabs, progress bars, checkboxes, radio buttons, tables, charts, forms, status views, keyboard navigation, color, or responsive terminal layouts; making Python scripts visually appealing and informative. Every skill script defaults to a TUI on an interactive terminal and provides --no-tui for deterministic stream output. DO NOT USE FOR: browser GUIs; desktop-native GUIs; plain shell commands with no interactive workflow; non-Python applications."
---

# Python TUI Apps

## Anatomy

| File | Purpose | Target Size |
| --- | --- | --- |
| `SKILL.md` | Defaults, workflow, component quick reference | <100 lines |
| [references/implementation.md](references/implementation.md) | Textual/Rich architecture, CLI contract, stream fallback | <200 lines |
| [references/ux-and-components.md](references/ux-and-components.md) | Component selection, visual design, interaction states | <200 lines |
| [references/testing-and-accessibility.md](references/testing-and-accessibility.md) | Headless tests, terminal sizes, accessibility, release checks | <200 lines |

## Required Contract

| Concern | Rule |
| --- | --- |
| Default | Launch the Textual TUI when input and output are interactive terminals |
| Model/automation mode | Accept `--no-tui`; emit ordered Rich/plain stream output; never prompt or animate |
| Redirected I/O | Fall back to stream mode unless the user explicitly forces a TUI |
| Core logic | Keep operations and state independent of Textual widgets and Rich rendering |
| Progress | Report phase, completed/total work, current item, and useful failure context |
| Color | Use a restrained semantic palette; preserve meaning without color |
| Controls | Choose widgets that match the data and action; do not add decorative controls |
| Exit | Support cancel/back, clean shutdown, stable exit codes, and a final result summary |

## Workflow

1. Define the primary task, user decisions, long-running phases, errors, and final artifact.
2. Design the stream event/result model before either renderer.
3. Implement `--no-tui` and redirected-I/O fallback; keep stdout machine-safe when required.
4. Compose the Textual app from semantic widgets and responsive containers.
5. Add keyboard paths, focus states, loading/empty/error/success states, and confirmation for destructive actions.
6. Test both renderers, narrow and standard terminal sizes, cancellation, errors, and piped output.

## Component Quick Reference

| Need | Prefer |
| --- | --- |
| Distinct views | `TabbedContent` / `TabPane`; preserve each tab's state |
| Mutually exclusive choice | `RadioSet` + `RadioButton` or `Select` for compact forms |
| Multiple choices | `SelectionList` or `Checkbox`; show selected count |
| Determinate work | `ProgressBar`; pair percentage with counts and current phase |
| Unknown-duration work | `LoadingIndicator`; use only while work is genuinely indeterminate |
| Records and comparison | `DataTable`; align numbers and keep columns scannable |
| Trends/distributions | `Sparkline`, compact bars, or `textual-plotext` when a chart adds insight |
| Commands | `Button` plus keyboard binding; make the primary action visually dominant |
| Guidance/results | `Static`, `Label`, `Markdown`, `Log`, or `RichLog` based on content |

## References

| File | Load When |
| --- | --- |
| [references/implementation.md](references/implementation.md) | Starting a Python TUI, separating logic/renderers, or adding `--no-tui` |
| [references/ux-and-components.md](references/ux-and-components.md) | Choosing controls, color, layout, charts, or interaction states |
| [references/testing-and-accessibility.md](references/testing-and-accessibility.md) | Writing tests, checking terminal sizes, or preparing release validation |