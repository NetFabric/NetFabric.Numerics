# Efficient OKF Reading

## Progressive Disclosure Strategy

Read structure before content. The default unit of context is an index entry or frontmatter record, not a full concept body.

```text
root index or path listing
  -> relevant directory index
  -> selected frontmatter
  -> selected concept body
  -> task-relevant links and sources
  -> deeper evidence only when needed
```

| Layer | Read | Skip Until Needed |
| --- | --- | --- |
| 1. Bundle map | Root `index.md`, directory names, `okf_version` | Concept bodies, `log.md` history |
| 2. Section map | Relevant nested `index.md` entries and descriptions | Unrelated directories |
| 3. Metadata filter | Path, `type`, `title`, `description`, `tags`, lifecycle, trust | Body prose and examples |
| 4. Concept answer | Relevant headings and nearby paragraphs | Whole-file ingestion |
| 5. Evidence | Matching sources, verification, linked concepts | Unrelated outgoing links |
| 6. Computation | Contract, sanctioned code, executor, attester | Runtime execution unless requested |

## Agent Reading Algorithm

1. State the information need as types, terms, tags, paths, and freshness or trust constraints.
2. Open root `index.md`; if absent, list `.md` paths and parse frontmatter in bulk.
3. Select the smallest relevant directory and read its `index.md`.
4. Rank entries by path/title match, description relevance, tags, lifecycle, freshness, and required trust.
5. Open frontmatter for the top candidates before loading any body.
6. Read only body headings likely to answer the task, then expand around matching sections.
7. Follow links only when the answer depends on related definitions, dependencies, lineage, or computation.
8. Follow `sources` only to verify a claim or assess evidence.
9. Stop when the answer has enough support; record unresolved or stale evidence explicitly.

## Selection Heuristics

| Task | First filter | Follow next |
| --- | --- | --- |
| Find a definition | `type`, title, description | Body definition section |
| Understand a schema | Asset type, resource, `# Schema` signal | Linked tables or field references |
| Locate a runbook | `type: Playbook`, tags | Trigger and steps sections |
| Compute a metric | Metric link to `Attested Computation` | Contract, computation, executor, attester |
| Check current truth | `status`, `stale_after`, latest verification | Sources and resource |
| Trace provenance | `sources` with internal paths | Recurse with cycle and depth limits |
| Build a summary | Index descriptions and frontmatter | Bodies only for missing summaries |
| Review changes | Scoped `log.md` and version-control diff | Changed concepts only |

## Context Budgeting

Set explicit limits before traversing:

| Limit | Practical default |
| --- | --- |
| Candidate concepts | Top 5-10 after metadata filtering |
| Link depth | 1; increase only for unresolved dependencies |
| Provenance depth | 1-2 with a visited set |
| Body sections | Matching heading plus adjacent context |
| Examples | One representative example unless comparison is required |
| Logs | Relevant scope and date window only |

Prefer metadata batches over repeated full-file reads. Cache parsed frontmatter by path plus content hash. Maintain separate lightweight indexes for text, tags/types, links, and source relationships.

## Reading Without Index Files

Missing indexes are valid. Synthesize a temporary view:

1. Walk paths without opening bodies.
2. Exclude reserved `index.md` and `log.md` from concepts.
3. Parse each concept's frontmatter only.
4. Group by directory, then summarize with `title` or filename plus `description`.
5. Cache the synthesized index; invalidate it when paths or frontmatter change.

Do not reject malformed candidates silently. Separate conformant concepts, parse failures, and unresolved paths so useful knowledge remains consumable.

## Efficient Evidence Reading

When a claim has a footnote such as `[^policy]`:

1. Match `policy` to `sources[].id`.
2. Inspect source title, author, recency, and usage window before fetching it.
3. Follow an internal concept path and recurse into its sources only if stronger provenance is required.
4. Fetch an external source only when the answer needs verification beyond the bundle.
5. Treat scope descriptors as evidence metadata, not fetch targets.

This avoids loading every citation and prevents positional source errors after agent rewrites.

## Efficient Producer-Friendly Documentation

Consumers read efficiently only when producers provide good navigation. When maintaining a bundle:

- Put a concise description on every important concept.
- Keep directory indexes current and grouped by user intent.
- Use stable paths and meaningful filenames.
- Put one independent concept per file.
- Use headings with conventional names where applicable.
- Link narrative concepts to reusable definitions and computations.
- Record sources in frontmatter and join body claims by stable IDs.
- Keep long computations or mirrored material under `references/` and link to them.
- Use scoped `log.md` files so readers can inspect recent changes without replaying all history.

## Stop Conditions

Stop expanding context when all are true:

- The selected concept directly answers the task.
- Required definitions and links resolve or are explicitly reported missing.
- Trust, lifecycle, and freshness meet the caller's threshold or are disclosed.
- Claims needing evidence have a resolved source.
- A computation answer uses the sanctioned contract rather than inferred logic.

Do not read the entire bundle merely to gain confidence. Prefer a justified, task-relevant subgraph and state any bounded-search limitations.
