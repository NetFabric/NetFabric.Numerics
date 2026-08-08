# CBM Tools, Graph Model, and CLI Invocation

Source: [github.com/DeusData/codebase-memory-mcp](https://github.com/DeusData/codebase-memory-mcp) README ("MCP Tools", "Graph Data Model" sections) and `docs/llms.txt`.

## CLI vs MCP — always use the CLI

Every tool below is exposed both as an MCP tool (JSON-RPC over stdio, for an MCP-connected client) and as a **one-shot CLI command**: `codebase-memory-mcp cli <tool_name> [flags]`. The CLI never starts or connects to the coordination daemon and leaves no standing process — it holds a lease only for the command's lifetime. **Prefer the CLI in every skill, agent, or prompt you author or edit**: it is a plain subprocess call with plain stdout, so it costs a small fraction of the tokens an MCP round trip does. Use `codebase-memory-mcp cli <tool_name> --help` to see flags generated from that tool's input schema; `--json` returns the full MCP result envelope when needed; JSON args can be piped on stdin instead of flags.

```bash
codebase-memory-mcp cli search_graph --project my-project --name-pattern '.*Handler.*' --label Function
codebase-memory-mcp cli trace_path --project my-project --function-name Search --direction both
codebase-memory-mcp cli query_graph --project my-project --query 'MATCH (f:Function) RETURN f.name LIMIT 5'
```

## The 15 tools

### Indexing

| Tool | Purpose |
| --- | --- |
| `index_repository` | Index a repository into the graph. Auto-sync (background watcher) keeps it fresh afterward. |
| `list_projects` | List all indexed projects with node/edge counts. |
| `delete_project` | Remove a project and all its graph data. |
| `index_status` | Check indexing status of a project — the freshness/sync check. |

### Querying & analysis

| Tool | Purpose |
| --- | --- |
| `search_graph` | Structured search by label, name pattern, file pattern, degree filters; paginated via limit/offset. Also accepts `semantic_query` for vector (meaning-based) search over the whole graph, powered by bundled nomic-embed-code embeddings — no API key, fully local. |
| `trace_path` | BFS traversal of who calls a function and what it calls (alias: `trace_call_path`). Depth 1-5. |
| `detect_changes` | Maps an uncommitted git diff to affected symbols and blast radius, with risk classification. |
| `query_graph` | Executes read-only Cypher-like graph queries (openCypher subset — see below). |
| `get_graph_schema` | Node/edge counts, relationship patterns, property definitions per label. Run this first when exploring an unfamiliar project. |
| `get_code_snippet` | Reads source code for a function by qualified name (`<project>.<path_parts>.<name>` — discover exact names via `search_graph` first). |
| `get_architecture` | Codebase overview in one call: languages, packages, entry points, routes, hotspots, boundaries, layers, clusters. |
| `search_code` | Grep-like text search, but scoped to indexed project files only (graph-augmented, not a raw filesystem grep). |
| `manage_adr` | CRUD for Architecture Decision Records, persisted alongside the graph so design rationale survives across sessions. |
| `ingest_traces` | Ingests runtime traces to validate `HTTP_CALLS` edges against real request activity. |
| `check_index_coverage` | Targeted index-coverage check for a set of paths — confirms whether specific files were fully parsed/indexed before you trust an absence result (e.g. "no callers found"). |

## Graph data model

### Node labels

`Project`, `Package`, `Folder`, `File`, `Module`, `Class`, `Function`, `Method`, `Interface`, `Enum`, `Type`, `Route`, `Resource` (the last two also cover Infrastructure-as-Code: Kubernetes/Kustomize resources and modules).

### Edge types

Core structural edges (from the Graph Data Model reference):

`CONTAINS_PACKAGE`, `CONTAINS_FOLDER`, `CONTAINS_FILE`, `DEFINES`, `DEFINES_METHOD`, `IMPORTS`, `CALLS`, `CALL_REFERENCE`, `HTTP_CALLS`, `ASYNC_CALLS`, `IMPLEMENTS`, `HANDLES`, `USAGE`, `CONFIGURES`, `WRITES`, `MEMBER_OF`, `TESTS`, `USES_TYPE`, `FILE_CHANGES_WITH`.

Additional analysis/cross-service edges documented elsewhere in the same reference:

| Edge | Meaning |
| --- | --- |
| `INHERITS` | Class/interface inheritance, alongside `IMPLEMENTS`. |
| `EMITS` / `LISTENS_ON` | Pub-sub channel detection (Socket.IO, EventEmitter, generic message buses) across 8 languages, with constant resolution. |
| `DATA_FLOWS` | Value flow from argument to parameter, including field-access chains. |
| `SIMILAR_TO` | Near-duplicate/copy-pasted code — MinHash + LSH, Jaccard scored. |
| `SEMANTICALLY_RELATED` | Conceptually similar functions with mismatched vocabulary (same language, score ≥ 0.80) — vector/embedding based, not name matching. |
| `CROSS_*` | Cross-repo edges linking nodes across multiple repositories indexed under the same store (cross-repo intelligence). |

`USAGE` specifically means an identifier is used but no unique callable target could be proven (ambiguous/complex expression) — it is the graph's explicit "uncertain" edge, as opposed to `CALLS`/`CALL_REFERENCE` which resolve to one exact target.

### Cypher subset (`query_graph`)

Read-only openCypher subset: `MATCH`/`OPTIONAL MATCH` (multiple), `WHERE`, `WITH` (+ `WITH … WHERE`), `RETURN`, `ORDER BY`, `SKIP`, `LIMIT`, `DISTINCT`, `UNWIND`, `UNION`/`UNION ALL`, `CASE`; label alternation `(n:A|B)`; variable-length paths `[*1..3]`; comparison/boolean/`IN`/`CONTAINS`/`STARTS WITH`/`ENDS WITH`/`IS [NOT] NULL`/regex `=~`/label test `n:Label`; single-hop `EXISTS { (n)-[:TYPE]->() }` (useful for dead-code queries, e.g. `WHERE NOT EXISTS { (f)<-[:CALLS]-() }`); aggregates `count`/`sum`/`avg`/`min`/`max`/`collect`; a curated function set (`labels`, `type`, `id`, `toLower`, `size`, `coalesce`, `substring`, etc.). Write clauses, `MERGE`/`CALL`, list/map literals, comprehensions, and parameters are outside the subset and fail with an explicit `unsupported …` error rather than silently returning empty results.
