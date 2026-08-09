# OKF Consumer Guide

## Parse Permissively

1. Identify `index.md` and `log.md` as reserved documents.
2. Treat every other `.md` file as a concept candidate.
3. Decode UTF-8 and parse YAML frontmatter before parsing the body.
4. Require a non-empty `type` only when validating conformance.
5. Preserve unknown fields and render unknown types as generic concepts.
6. Continue when optional fields, indexes, or link targets are absent.

Normalize data at the boundary:

| Input | Normalized form |
| --- | --- |
| Missing `status` | `stable` |
| Missing `verified` | Empty verification list; trust tier `unverified` |
| Mapping `verified: { by, at }` | One-element verification list |
| Unknown `type` | Generic concept with original type retained |
| Unknown key | Retained extension metadata |
| Missing title | Filename-derived display title |
| Unsupported `okf_version` | Best-effort parse plus compatibility warning |

## Build A Catalog

Extract a lightweight record without loading the body:

```text
concept_id, path, type, title, description, resource, tags,
status, stale_after, generated, verified, sources, extension_fields
```

Index frontmatter for filtering and previews. Parse Markdown links separately to build directed graph edges. Resolve path-valued fields against the containing document unless they begin at the bundle root or are absolute URLs.

Treat a non-followable `sources[].resource` as a scope descriptor, not a broken path. Treat `references/` as a convention, not a special parser mode.

## Evaluate Trust And Freshness

```text
if any verified.by starts with "human:": human-reviewed
else if verified is non-empty:              machine-confirmed
else:                                       unverified

stale = stale_after exists and today >= stale_after
```

Keep these signals separate:

| Signal | Meaning |
| --- | --- |
| Trust tier | Kind of actor that verified the concept |
| Latest `verified.at` | Recency of verification |
| `generated.at` | Last meaningful content change |
| `stale_after` | Producer's absolute freshness boundary |
| Source credibility fields | Evidence for consumer-specific judgment |
| `status` | Draft, current, or deprecated lifecycle state |

Do not use trust tiers as authorization. Do not collapse source signals into an OKF-defined score. Surface uncertainty, draft/deprecated state, staleness, and absent verification to the caller.

## Traverse Relationships

| Relationship source | Consumer behavior |
| --- | --- |
| Directory hierarchy | Parent/child navigation only |
| Markdown link | Directed untyped edge; use surrounding prose for semantics |
| `sources[].resource` to concept | Provenance edge; optionally recurse through sources |
| External URL | External leaf or resource |
| Broken internal link | Retain edge and report unresolved target |

Use a visited set keyed by concept ID when traversing links or provenance. Bound depth and breadth for agent context. Prefer a task-driven subgraph over loading the full bundle.

## Consume Attested Computations

1. Discover `type: Attested Computation` directly or through a narrative concept link.
2. Read the contract from frontmatter.
3. Load the computation from `computation` or the single fence under `# Computation`.
4. Accept values only for declared `parameters`; bind them according to `runtime`.
5. Follow `executor.resource`; require a receipt shaped by `executor.receipt`.
6. Run the deterministic code at `attester.resource` consumer-side.
7. Confirm the executed or compiled artifact equals the sanctioned computation with declared bindings.
8. Confirm the displayed result matches the receipt's authoritative source.
9. Refuse to display a failing attestation; warn or refuse when stale; surface successful evidence.

Never treat an agent-authored replacement computation as equivalent. Do not store runtime receipts or verdicts in the bundle. Verification checks the definition; attestation checks each run.

## Search And Presentation

| Feature | Source |
| --- | --- |
| Navigation labels | `title`, else filename |
| Search snippets | `description`, then a short body extract |
| Facets | `type`, `tags`, `status`, trust tier, stale state |
| Asset action | `resource` |
| Evidence panel | `sources`, generation, verification, lifecycle |
| Graph | Markdown links and internal source references |

Rank matches using consumer policy, not a field claimed to be an objective trust score. Prefer stable, non-stale, sufficiently verified concepts when relevance is otherwise equal, but allow users to inspect lower-trust results.

## Compatibility

For v0.1 concepts, read `timestamp` only when `generated` is absent. Read v0.2 `sources` first; optionally parse legacy `# Citations` when no sources exist. Preserve legacy fields on round-trip unless performing an explicit migration.

## Consumer Conformance Tests

- Unknown `type` and extension fields do not fail parsing.
- Missing optional metadata and missing `index.md` do not fail parsing.
- Bare and list forms of `verified` produce the same normalized list.
- Missing status becomes `stable`; equality with `stale_after` is stale.
- Human verification outranks machine-only verification.
- Broken links remain queryable as unresolved edges.
- Unsupported versions attempt best-effort consumption.
- Failing attestation remains visible and gates the result.
