# Wiring CBM into an Agent, Skill, or Prompt

When you create or edit an agent, subagent, skill, or prompt that operates on a codebase, replace `grep`/`ripgrep`/glob-and-read exploration with CBM CLI calls. This mirrors CBM's own documented agent-integration model (Scout/Verify/Auditor tiers, README "Multi-Agent Support"), generalized to any workflow stage.

## Why this replaces grep/search

A single graph query answers what would otherwise take a cascade of `grep → read file → grep again → read more files`. `search_graph`/`query_graph` return structured, pre-resolved relationships (import-aware, type-inferred call edges) that a text search cannot reconstruct — e.g. "who calls this function across packages" or "what does this route handler call" is one `trace_path`/`query_graph` call versus dozens of file reads.

Before wiring in CBM, confirm it applies: [references/installation.md](references/installation.md) covers checking install state and index freshness. A stale or absent index still requires `index_repository` first — CBM cannot answer structural questions about code it hasn't parsed.

## Per-stage tool selection

| Stage | Goal | CLI tool(s) | Why |
| --- | --- | --- | --- |
| Setup | Confirm CBM is usable before relying on it | `index_status`, `list_projects` | Fails fast if the project isn't indexed or the index is stale, instead of silently falling back to slower search. |
| Discovery / scout | Fast, provisional orientation in an unfamiliar area of the codebase | `get_graph_schema`, `get_architecture`, `search_graph` (incl. `semantic_query`) | Cheap, broad calls that establish what exists (languages, packages, routes, hotspots) before narrowing — no absence or exhaustive-impact claims yet. |
| Locating symbols | Find the exact qualified name/location before acting on it | `search_graph` (`name_pattern`, `label`, `file` filters), `search_code` | `get_code_snippet` and `trace_path` require exact names; `search_graph` is how you discover them first. |
| Understanding relationships | Who calls this, what does this call, what inherits/implements it | `trace_path` (BFS, depth 1-5), `query_graph` (Cypher for multi-hop or filtered patterns `search_graph` can't express) | Import-aware, type-inferred resolution across files/packages in one call, replacing a manual call-chain grep. |
| Reading implementation | Get the actual source once the target symbol is confirmed | `get_code_snippet` | Reads by qualified name — no need to grep a file path first. |
| Verification / task-directed work (default tier) | Before making a claim or edit, confirm evidence is complete for the files in scope | `check_index_coverage` for every cited path, then `get_code_snippet`/`trace_path` for exact source | A clean graph result only means "no recorded gap" — read flagged/skipped ranges directly from source before asserting completeness or absence (e.g. dead code, "no callers"). |
| Change impact | What does an uncommitted diff affect, and how risky | `detect_changes` | Maps the diff to affected symbols and blast radius with risk classification — one call instead of manually tracing every changed function's callers. |
| Dead code / cleanup | Find unused functions before removing or flagging them | `query_graph` with `WHERE NOT EXISTS { (f)<-[:CALLS]-() }`, or the dedicated dead-code detection | Graph-native exclusion of entry points (route handlers, `main()`, framework decorators) that a naive "zero grep hits" check would misclassify. |
| Cross-service / architecture review | HTTP routes, gRPC/GraphQL/tRPC, pub-sub channels, cross-repo links | `get_architecture`, `query_graph` (`HTTP_CALLS`/`ASYNC_CALLS`/`EMITS`/`LISTENS_ON`/`CROSS_*` edges) | These relationships are cross-file/cross-service by nature; only the graph has them pre-resolved with confidence scoring. |
| Broader / auditor-tier review | Bounded-scope, complete-pagination verification across a wider area | `search_graph`/`query_graph` with explicit pagination, `check_index_coverage` for the full relevant scope | Matches CBM's own Auditor tier: complete relevant pagination and explicit unresolved limitations, not a sampled pass. |
| Architecture decisions | Persist or retrieve the rationale behind a structural decision | `manage_adr` | Keeps design rationale attached to the graph across sessions/teammates instead of a separate untracked doc. |

## Authoring checklist

When an agent/skill/prompt you're writing or editing touches a codebase:

1. State explicitly that CBM's CLI is the required exploration path, not `grep`/`find`/reading files ad hoc.
2. Have it check `index_status` (and `index_repository` if absent/stale) before querying.
3. Match the tool to the stage using the table above, narrowest call first (schema/architecture before deep traversal).
4. Require `check_index_coverage` before any negative claim ("no callers", "dead code", "nothing uses this").
5. Only fall back to a raw file read/grep for content CBM does not index as a graph relationship (e.g. reading a config value, a comment, or a non-code text file).
