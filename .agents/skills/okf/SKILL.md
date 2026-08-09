---
name: okf
description: "Produce, consume, validate, and navigate Open Knowledge Format (OKF) bundles. Use when: creating an OKF knowledge bundle; converting catalogs, wikis, schemas, metrics, runbooks, or APIs to OKF; parsing or indexing OKF; designing an OKF producer or consumer; validating OKF v0.2 conformance; traversing index.md files and concept links; minimizing agent context while reading OKF documentation; handling provenance, trust, lifecycle, sources, or Attested Computation concepts. DO NOT USE FOR: OpenAPI schemas; generic Markdown documentation with no OKF bundle; knowledge graphs that do not use OKF files; runtime packaging or serving protocols not defined by OKF."
---

# Open Knowledge Format

OKF v0.2 represents portable knowledge as a directory tree of UTF-8 Markdown concept files with YAML frontmatter. The file path identifies the concept; `index.md` enables progressive disclosure; links form a graph.

## Anatomy

| File | Purpose | Target Size |
| --- | --- | --- |
| `SKILL.md` | Routing, model, and workflow | <100 lines |
| `references/specification.md` | Local OKF v0.2 normative digest | <200 lines |
| `references/producer-guide.md` | Bundle design, authoring, and validation | <200 lines |
| `references/consumer-guide.md` | Parsing, trust, traversal, and attestation | <200 lines |
| `references/efficient-reading.md` | Low-context progressive reading for humans and agents | <200 lines |

## Core Model

| Element | Rule |
| --- | --- |
| Bundle | Directory tree; git, archive, or repository subdirectory |
| Concept | Any non-reserved `.md` file; YAML frontmatter plus Markdown body |
| Concept ID | Bundle-relative file path without `.md` |
| Required metadata | Non-empty `type`; all other fields are optional |
| Reserved files | `index.md` for listings; `log.md` for chronological updates |
| Relationships | Standard Markdown links; bundle-relative links are preferred |
| Extensibility | Producers may add fields; consumers must tolerate unknown fields and types |

## Route The Task

| Goal | Load |
| --- | --- |
| Check exact requirements or compatibility | [specification.md](references/specification.md) |
| Create, export, or maintain a bundle | [producer-guide.md](references/producer-guide.md) |
| Parse, search, render, or execute from a bundle | [consumer-guide.md](references/consumer-guide.md) |
| Read a large bundle with minimal context | [efficient-reading.md](references/efficient-reading.md) |

## Default Workflow

1. Read the root `index.md` if present; otherwise list paths without opening bodies.
2. Parse frontmatter first and select concepts by path, `type`, `tags`, lifecycle, and description.
3. Open only selected bodies; follow links and `sources` when the task requires more evidence.
4. Apply trust, freshness, and attestation rules without rejecting optional or unknown data.
5. Preserve unknown metadata when rewriting; validate the conformance checklist before publishing.

## Sources

| Source | Role |
| --- | --- |
| [OKF v0.2 specification](https://github.com/GoogleCloudPlatform/knowledge-catalog/blob/main/okf/SPEC.md) | Normative source |
| [Google Cloud introduction](https://cloud.google.com/blog/products/data-analytics/how-the-open-knowledge-format-can-improve-data-sharing) | Design rationale and v0.1 context |

## Reference Files

| File | Load When |
| --- | --- |
| [references/specification.md](references/specification.md) | Resolving normative fields, conformance, versioning, or v0.1 migration |
| [references/producer-guide.md](references/producer-guide.md) | Designing and writing bundles or producer pipelines |
| [references/consumer-guide.md](references/consumer-guide.md) | Building readers, indexes, search, UIs, or agents |
| [references/efficient-reading.md](references/efficient-reading.md) | Traversing OKF documentation efficiently and selectively |
