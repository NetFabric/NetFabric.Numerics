# Research Workflow

## New Skill: Step-by-Step

1. **Scope** — Define: subject, primary users, trigger phrases, explicit exclusions
2. **Library docs** — `mcp_context7_resolve-library-id` → `mcp_context7_get-library-docs`
3. **MS docs** — `microsoft_docs_search` → `microsoft_docs_fetch` for depth
4. **Code samples** — `microsoft_code_sample_search` for practical patterns
5. **Authoritative sources** — cross-reference ≥2 sources (see priority below)
6. **Distill** — extract: core concepts, key APIs, common patterns, gotchas; discard history
7. **Draft** — write SKILL.md skeleton; push detail to reference files
8. **Validate** — check description ≤1024 chars; verify reference table is complete

## MCP Tools

| Goal | Tool | Method |
|------|------|--------|
| Resolve library ID | context7 | `mcp_context7_resolve-library-id` |
| Get library docs | context7 | `mcp_context7_get-library-docs` |
| Search MS/Azure topics | microsoftdocs | `microsoft_docs_search` |
| Fetch full MS page | microsoftdocs | `microsoft_docs_fetch` |
| Find code samples | microsoftdocs | `microsoft_code_sample_search` |

## Source Priority

1. Official language / framework docs (context7)
2. Microsoft Learn (microsoftdocs MCP)
3. RFC / IETF / ISO specifications
4. Core maintainer blogs or repos (e.g., language designers, project leads)
5. Community consensus (top GitHub discussions, canonical SO answers)

Avoid: outdated tutorials, AI-generated summaries without citations.

## Update Existing Skill

1. Identify changed area (API change, deprecation, new feature)
2. Re-fetch only the affected docs section
3. Edit the specific reference file(s)
4. Update SKILL.md summary line if the trigger phrase set changed
5. Re-validate frontmatter description length

## Quality Checklist

- [ ] ≥2 authoritative sources consulted
- [ ] Outdated content removed or marked deprecated
- [ ] SKILL.md has no content that belongs only in references
- [ ] Every reference file appears in the SKILL.md table
- [ ] Description ≤1024 chars and includes exclusions
- [ ] No placeholder or "TODO" lines left in files
