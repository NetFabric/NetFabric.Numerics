# Planning and Tracking Diagrams

Gantt, Kanban, and Requirement diagrams document project execution and formal traceability rather than code structure or runtime behavior.

## Gantt Chart

**When to use:** a project schedule — task durations, dependencies, and milestones over calendar time.

````markdown
```mermaid
gantt
    title Release Plan
    dateFormat YYYY-MM-DD
    section Design
        Spec           :done,    des1, 2025-01-01, 5d
        Review         :active,  des2, after des1, 3d
    section Build
        Implement      :         imp1, after des2, 10d
        Milestone      :milestone, m1, after imp1, 0d
```
````

Each task line is `Name : [tags,] [id,] <start>, <end-or-duration>`. Optional tags `done`, `active`, `crit`, `milestone` (first, if present) control rendering. `after <taskId>` chains a task off another's end instead of an explicit date. `section <name>` groups rows; `excludes weekends` (or specific dates) skips non-working days when computing durations.

## Kanban Diagram

**When to use:** a task board with columns (Todo/In Progress/Done) — lighter-weight than Gantt, no dates or dependencies.

````markdown
```mermaid
kanban
  Todo
    docs[Create documentation]
  [In progress]
    id6[Build renderer]
  id11[Done]
    id5[Define getData]
```
````

A column is `columnId[Column Title]` (or a bare title, auto-generating an id); indented lines below it are tasks: `taskId[Task description]`. Attach metadata with `@{ assigned: 'name', ticket: 'ABC-1', priority: 'High' }` after a task — `priority` accepts `'Very High' | 'High' | 'Low' | 'Very Low'`.

## Requirement Diagram

**When to use:** formal SysML-style traceability — linking requirements to the elements (tests, components, docs) that satisfy or verify them.

````markdown
```mermaid
requirementDiagram
    requirement login_req {
        id: 1
        text: user can log in
        risk: high
        verifymethod: test
    }
    element auth_service {
        type: simulation
    }
    auth_service - satisfies -> login_req
```
````

`requirement <name> { id: ... text: ... risk: <Low|Medium|High> verifymethod: <Analysis|Inspection|Test|Demonstration> }` defines a requirement; `requirement` can be swapped for `functionalRequirement`, `interfaceRequirement`, `performanceRequirement`, `physicalRequirement`, or `designConstraint`. `element <name> { type: ... docref: ... }` defines a connected artifact. Relationships: `<source> - <type> -> <destination>` where `<type>` is one of `contains`, `copies`, `derives`, `satisfies`, `verifies`, `refines`, `traces`.
