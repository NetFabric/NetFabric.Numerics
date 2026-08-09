# OKF Producer Guide

## Design The Bundle

1. Choose a stable bundle root and distribution method; prefer git when history and diffs matter.
2. Define domain-oriented directories without inventing a universal taxonomy.
3. Give each independently useful unit of knowledge one concept file.
4. Use stable, readable paths because the path is the concept ID.
5. Reserve `index.md` and `log.md`; never use either as a concept name.

```text
catalog/
  index.md
  datasets/
    index.md
    sales.md
  metrics/
    index.md
    weekly-active-users.md
  playbooks/
    incident-response.md
  references/
    policies/
      measurement.md
```

## Write Concepts

Start with a non-empty `type`, then add only metadata the producer can maintain accurately.

```markdown
---
type: Metric
title: Weekly active users
description: Distinct users with a qualifying event in the trailing seven days.
tags: [engagement, product]
status: stable
generated: { by: catalog-exporter/1.4, at: 2026-08-09T10:00:00Z }
sources:
  - id: measurement-policy
    resource: /references/policies/measurement.md
    title: Product measurement policy
---

# Definition

A user is active after producing a qualifying event.[^measurement-policy]

[^measurement-policy]: Product measurement policy
```

| Decision | Preferred choice |
| --- | --- |
| Type name | Descriptive, self-explanatory, stable within the bundle |
| Title | Human display name; do not encode identity only in the title |
| Description | One sentence that supports selection without opening the body |
| Body | Structured headings, tables, lists, and language-tagged fences |
| Internal link | Bundle-relative `/path/concept.md` when moves are likely |
| Claim source | `sources[].id` plus a matching Markdown footnote |
| Extension field | Namespaced or clearly documented; never shadow standard fields |

## Record Trustable Metadata

| Question | Field |
| --- | --- |
| What was this derived from? | `sources` |
| Who or what wrote the current content? | `generated.by` |
| When did meaningful content last change? | `generated.at` |
| Who checked it against its source? | `verified` |
| Is it ready or retired? | `status` |
| When must a consumer reconsider it? | `stale_after` |

Do not invent a credibility score. Record objective source signals (`author`, `usage_count`, `last_modified`, `usage_window`) and let each consumer evaluate them.

Use `<producer>/<version>`, `human:<id>`, or `process:<id>` for actors. Keep generation and verification separate. Update `generated.at` after meaningful content changes; add verification only after checking against sources or the resource.

## Build Indexes For Progressive Disclosure

```markdown
# Metrics

- [Weekly active users](metrics/weekly-active-users.md) - Distinct users active in the trailing seven days.
- [Retention](metrics/retention.md) - Cohort return rate after first use.

# Operations

- [Playbooks](playbooks/) - Operational response procedures.
```

At each useful directory boundary:

1. Group concepts and subdirectories under meaningful headings.
2. Copy each concept's short description into its entry.
3. Link relatively from the index.
4. Keep the index scannable; move detail to concepts.
5. Regenerate it when concepts are added, removed, moved, or renamed.

The root index may declare `okf_version: "0.2"`; nested indexes must not contain frontmatter.

## Produce Attested Computations

1. Create one `type: Attested Computation` concept per independently trusted computation.
2. Declare `runtime` and every parameter the caller may supply.
3. Store the computation in one body fence or one file referenced by `computation`.
4. Point `executor.resource` to run instructions or code and declare receipt fields.
5. Point `attester.resource` to deterministic code that independently validates the receipt.
6. Link narrative metrics or assets to the computation concept.

Never let an agent rewrite the sanctioned computation at execution time. Parameter values are the only permitted variable surface.

## Producer Pipeline

```text
extract source metadata
  -> map one knowledge unit per concept
  -> render frontmatter and structured body
  -> add links, sources, and trust metadata
  -> generate indexes and logs
  -> validate YAML, paths, reserved files, and required type
  -> publish bundle
```

Preserve manually curated sections during regeneration. Use stable ordering for frontmatter, lists, and indexes to keep diffs reviewable. Preserve unknown fields when updating existing concepts.

## Validation Checklist

- Every non-reserved `.md` starts with parseable YAML frontmatter.
- Every concept has a non-empty `type`.
- `index.md` and `log.md` follow their reserved formats.
- Root-only `okf_version` is quoted as `"0.2"`.
- Every `sources` entry has `resource`; cited sources have stable `id` values.
- Dates and datetimes use the specified ISO forms.
- Human actors use the `human:` prefix.
- Attested computations have `runtime` and exactly one computation source.
- Generated indexes match the directory contents.
- Broken links are reported for maintenance but do not make the bundle nonconformant.
