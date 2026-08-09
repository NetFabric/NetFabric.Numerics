# OKF v0.2 Specification Digest

This local digest preserves the normative requirements of the [upstream OKF v0.2 specification](https://github.com/GoogleCloudPlatform/knowledge-catalog/blob/main/okf/SPEC.md). Consult upstream for canonical wording and worked examples.

## Bundle And Documents

| Item | Requirement |
| --- | --- |
| Bundle | A self-contained directory tree distributed as git, an archive, or a subdirectory |
| Concept | One UTF-8 Markdown file with YAML frontmatter at byte zero and a Markdown body |
| Concept ID | File path within the bundle with the `.md` suffix removed |
| Reserved names | `index.md` and `log.md` MUST NOT be concept documents |
| Other Markdown | Every other `.md` file is a concept document |

```text
bundle/
  index.md
  log.md
  concepts/
    index.md
    example.md
  references/
    source.md
```

## Concept Frontmatter

```yaml
---
type: Metric
title: Weekly active users
description: Distinct users active during a seven-day window.
resource: https://example.com/catalog/weekly-active-users
tags: [analytics, engagement]
status: stable
---
```

| Field | Cardinality | Meaning |
| --- | --- | --- |
| `type` | Required, non-empty | Unregistered concept kind used for routing and presentation |
| `title` | Optional | Display name; consumers may derive it from the filename |
| `description` | Optional | One-sentence preview, index, or search summary |
| `resource` | Optional | Canonical URI for the described asset |
| `tags` | Optional list | Cross-cutting categorization |

Producers MAY add keys. Consumers MUST tolerate unknown `type` values and fields, SHOULD preserve unknown keys when round-tripping, and MUST NOT reject a document because optional metadata is absent.

The body has no required sections. Producers SHOULD prefer headings, lists, tables, and fenced code. Conventional headings are `# Schema`, `# Examples`, and `# Computation`.

## Provenance

```yaml
sources:
  - id: policy
    resource: https://example.com/policy
    title: Measurement policy
    author: team:analytics
    usage_count: 1200
    last_modified: 2026-06-01
usage_window: { from: 2026-06-01, to: 2026-06-30 }
```

| Field | Rule |
| --- | --- |
| `sources[].resource` | Required per source; URL, path, or non-followable scope descriptor |
| `sources[].id` | Stable optional key; SHOULD exist when body claims cite the source |
| `sources[].title` | Optional display label |
| `author` | Optional actor-style authority signal |
| `usage_count` | Optional liveness signal, not a credibility score |
| `last_modified` | Optional source date in `YYYY-MM-DD` |
| `usage_window` | Shared `{ from, to }`; a source may override it |

Attribute a claim with `[^source-id]`; join the footnote label to `sources[].id`. Express lineage through links and linked concepts' sources, not a dedicated lineage field.

## Trust And Lifecycle

```yaml
generated: { by: catalog-agent/2.1, at: 2026-06-20T22:53:05Z }
verified:
  - { by: process:nightly-check, at: 2026-06-21T02:00:00Z }
  - { by: human:reviewer-id, at: 2026-06-22T09:00:00Z }
status: stable
stale_after: 2026-09-23
```

| Family | Rule |
| --- | --- |
| `generated` | Optional; `by` is required when present; `at` is ISO 8601 last meaningful change |
| `verified` | Optional mapping or list of `{ by, at }`; consumers MUST normalize a mapping to one item |
| Trust tier | No verification = unverified; non-human only = machine-confirmed; any `human:` = human-reviewed |
| `status` | `draft`, `stable`, or `deprecated`; absent means `stable` |
| `stale_after` | Absolute `YYYY-MM-DD`; stale when `today >= stale_after` |

Actors use `<producer>/<version>` for tools, `human:<id>` for people, and `process:<id>` for automation. Producers MUST use `human:` for human authorship or confirmation. Trust tiers are advisory, not access control.

## Links And Paths

Concepts MAY use bundle-relative links such as `/tables/orders.md` or ordinary relative links. Bundle-relative links are recommended. A link is a directed, untyped relationship whose prose supplies meaning. Consumers MUST tolerate broken links.

`resource`, `sources[].resource`, `computation`, `executor.resource`, and `attester.resource` accept an absolute URL, bundle-relative path, or relative path. `references/` is a convention for mirrored material, executors, and attesters, not a requirement.

## Index And Log Files

| File | Requirements |
| --- | --- |
| `index.md` | MAY appear in any directory; groups relative links under headings; SHOULD copy concept descriptions |
| Root `index.md` | Only index allowed frontmatter, solely to declare `okf_version: "0.2"` |
| `log.md` | MAY appear at any level; flat date groups newest first; date headings MUST be `YYYY-MM-DD` |

Producers MAY generate indexes. Consumers MAY synthesize missing indexes by scanning paths and frontmatter.

## Attested Computation

`type: Attested Computation` defines a sanctioned computation independently from concepts that link to its result.

| Field | Rule |
| --- | --- |
| `runtime` | Required for this type; defines parameter binding semantics |
| `parameters` | Typed named holes: `{ name, type, required }` |
| `computation` | Optional path to computation file; absent means one fence under `# Computation` |
| `executor.resource` | Instructions or code that runs the computation |
| `executor.receipt` | Evidence fields the run must return |
| `attester.resource` | Deterministic, non-LLM code that checks a receipt |

Provide the computation either inline or by file, never both. An agent MAY supply only declared parameter values and MUST NOT author or edit the sanctioned computation. Receipts and attestation verdicts are runtime artifacts and are not stored in the bundle.

Verification checks that the definition matches policy; attestation checks one execution. A fresh verification does not replace per-run attestation.

## Conformance

A v0.2 bundle is conformant when:

1. Every non-reserved `.md` file has parseable YAML frontmatter.
2. Every concept has a non-empty `type`.
3. Present `index.md` and `log.md` files follow their defined structures.

Consumers MUST NOT reject missing optional fields, unknown types, unknown fields, broken links, or missing indexes. They SHOULD derive trust and staleness only as specified and SHOULD surface failing attestations.

## Version Compatibility

Minor versions add backward-compatible optional features; major versions may break requirements. Consumers that do not understand `okf_version` SHOULD attempt best-effort consumption.

For v0.1 input, use `timestamp` only when `generated` is absent. Prefer v0.2 `sources`; consumers MAY parse the legacy `# Citations` section as a fallback.
