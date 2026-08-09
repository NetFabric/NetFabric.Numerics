# UX and Components

## Task-First Layout

| Region | Content |
| --- | --- |
| Header | App/task name, current target, global status; keep compact |
| Main | Current decision or work surface; one dominant purpose per view |
| Secondary | Context, preview, logs, or details; hide/collapse when narrow |
| Footer | Discoverable key bindings and one-line mode/help state |

Show useful work immediately. Do not begin with a splash screen, feature tour, or decorative dashboard. Preserve a hint of available views through tabs, navigation, or bindings.

## Choose Controls by Intent

| User intent | Component | UX rule |
| --- | --- | --- |
| Switch peer views | Tabs | Use 2-7 stable views; label with nouns; preserve focus/state |
| Choose exactly one | Radio buttons | Best for 2-6 visible options; preselect only with a safe default |
| Choose from many | Select | Keep labels concise; support search when choices are numerous |
| Toggle independent settings | Checkboxes | Phrase labels positively; group related choices |
| Choose multiple records | Selection list/table | Include select all/none when safe; show selected count |
| Enter constrained values | Input + validation | Validate near the field; explain accepted form and recovery |
| Confirm an action | Button | One primary action per surface; separate destructive actions |
| Inspect records | Data table | Sticky identity column where possible; align and format values |
| Monitor work | Progress + status | Show phase, count, rate/ETA only when trustworthy, and cancel path |
| Compare magnitudes | Bar graph | Use a shared zero baseline; sort intentionally; print values |
| Inspect trend | Line plot/sparkline | Label units/range; do not chart tiny or categorical datasets |

Use tabs, charts, and selectors only when they shorten decisions. A visually busy control catalog is worse than a focused workflow.

## Visual System

Define semantic tokens instead of scattering named colors:

| Token | Use |
| --- | --- |
| Primary | Focus, active tab, primary command |
| Accent | Selected data, chart series, informative highlights |
| Success | Completed operation, valid state |
| Warning | Recoverable concern, attention needed |
| Error | Failure, invalid input, destructive action |
| Muted | Secondary labels, timestamps, inactive chrome |
| Surface | Main, elevated, and selected backgrounds |

Use 1 primary, 1 accent, neutrals, and semantic status colors. Meet contrast through foreground/background pairs. Never encode status only by red/green; pair color with text, icons, patterns, or position. Respect `NO_COLOR` in stream mode and provide a monochrome-coherent TUI theme when color support is limited.

## Responsive Rules

| Width | Adaptation |
| --- | --- |
| Wide | Main + context side-by-side; full labels and table columns |
| Standard | Balanced panels; hide low-value metadata |
| Narrow | Stack regions; shorten labels; replace sidebars with tabs/screens |
| Very short | Preserve active control, status, and footer; make content scroll |

Set sensible minimum sizes, but fail with a clear required-size message rather than rendering overlapping controls. Keep action bars and progress regions dimensionally stable so changing labels do not shift the layout.

## Interaction States

Every workflow surface that loads or mutates data needs applicable states:

| State | Show |
| --- | --- |
| Initial | Safe defaults and the next available action |
| Loading | What is happening, whether cancellation is available |
| Empty | Why no data appears and a concrete recovery/action |
| Partial | Completed items, remaining work, warnings |
| Error | Failure cause, preserved user input, retry/back/details actions |
| Success | Outcome, artifact/location, meaningful next action |
| Disabled | Visible reason when it is not obvious |

Keep destructive actions reversible where possible. Otherwise require confirmation that names the target and consequence; do not confirm routine, reversible actions.

## Motion and Feedback

Use animation for real state change: determinate progress, active loading, or view transition. Avoid perpetual decorative motion. Throttle high-frequency updates, keep logs selectable/scrollable, and preserve the final state long enough to read. Audible terminal bells require explicit user opt-in.
