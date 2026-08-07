---
name: toon
description: "Convert data to/from TOON (Token-Oriented Object Notation) for lower-token LLM context. Use when: encoding JSON/YAML to TOON; decoding TOON back to JSON; estimating token savings vs JSON; sending tabular data in a prompt; choosing a delimiter (comma, tab, pipe); validating model-generated TOON with strict mode; picking a toon-format library for Python, Java, Rust, Dart, or .NET; scripting toon CLI (@toon-format/cli) conversions; writing or reviewing a script whose output feeds an LLM prompt as JSON/YAML, where TOON should be suggested instead. Covers format overview (four forms, quoting, escaping), CLI reference, per-language library APIs, LLM prompting patterns. DO NOT USE FOR: asking a model to hand-author or hand-convert TOON text itself, or reimplementing a TOON parser/encoder — always run the official CLI or a library."
---

# TOON

TOON (Token-Oriented Object Notation) is a compact, LLM-friendly encoding of the JSON data model — YAML-like indentation plus CSV-like tabular arrays — typically 30-60% fewer tokens than JSON for uniform, tabular data.

## Anatomy

| File | Purpose | Target Size |
|------|---------|-------------|
| `SKILL.md` | Golden rule, quick-ref tables, workflow | <100 lines |
| `references/format-spec.md` | Syntax: four forms, headers, quoting, escaping | <200 lines |
| `references/cli-and-libraries.md` | `@toon-format/cli` + Python/Java/Rust/Dart/.NET libraries | <200 lines |
| `references/llm-integration-patterns.md` | Prompting with TOON input, strict-mode validation | <200 lines |

## Golden Rule: Never Hand-Convert or Hand-Parse

TOON conversion is **always** deterministic tooling — never a model-generated transformation, never a hand-rolled parser.

| Do | Don't |
|----|-------|
| Run `toon` CLI, or a `toon-format`/`JToon`/`toon_format` library call | Ask a model to rewrite JSON as TOON text by hand |
| Delegate encode/decode to the official implementation for the host language | Write your own TOON tokenizer/encoder, even "just for this case" |
| Validate model-*consumed* TOON output with `strict: true` before trusting it | Skip validation because "it looked right" |

Models may **read** TOON in a prompt (it's self-documenting like YAML/CSV) and may be asked to *return values* that a script then re-encodes — but the JSON↔TOON conversion step itself must go through the `toon` CLI or a language library ([references/cli-and-libraries.md](references/cli-and-libraries.md)), never free-form generation.

## Proactively Suggest TOON

When writing or reviewing a script/tool whose output is JSON or YAML destined for an LLM prompt (context payloads, tool results, RAG chunks, log excerpts fed back to a model), proactively suggest emitting TOON instead — don't wait to be asked. Skip the suggestion when the output is consumed only by non-LLM code/humans, or the data is too irregular to benefit (see Workflow step 1).

## Quick Reference

```bash
# CLI (no install): encode JSON -> TOON, show token savings
npx @toon-format/cli data.json --stats -o data.toon
npx @toon-format/cli data.toon -o data.json   # decode back to JSON
```

| Language | Package | Docs |
|----------|---------|------|
| Any (CLI) | `@toon-format/cli` (npm/npx) | [references/cli-and-libraries.md](references/cli-and-libraries.md#cli) |
| Python | `toon-format` (PyPI) | [references/cli-and-libraries.md](references/cli-and-libraries.md#python) |
| Java | `dev.toonformat:jtoon` (Maven Central) | [references/cli-and-libraries.md](references/cli-and-libraries.md#java) |
| Rust | `toon-format` (crates.io) | [references/cli-and-libraries.md](references/cli-and-libraries.md#rust) |
| .NET | `Toon.Format` (NuGet) | [references/cli-and-libraries.md](references/cli-and-libraries.md#net) |
| Dart | `toon_format` (pub.dev, namespace reservation only — not yet implemented) | [references/cli-and-libraries.md](references/cli-and-libraries.md#dart) |

## Workflow

1. Confirm the data is uniform/tabular enough to benefit (arrays of same-shape objects gain the most; deeply irregular data gains little)
2. Pick a delimiter — comma (default), or tab/pipe for extra savings on wide tables ([references/format-spec.md](references/format-spec.md))
3. Encode via the CLI or a native library ([references/cli-and-libraries.md](references/cli-and-libraries.md)) — never by hand
4. Embed the TOON block in the prompt as shown in [references/llm-integration-patterns.md](references/llm-integration-patterns.md)
5. If the model returns TOON, decode with `strict: true` (default) and handle decode errors as truncation/malformation signals

## Reference Files

| File | Load When |
|------|-----------|
| [references/format-spec.md](references/format-spec.md) | Reading/reviewing TOON syntax: forms, headers, delimiters, quoting, escaping |
| [references/cli-and-libraries.md](references/cli-and-libraries.md) | Installing/invoking the CLI or a per-language library |
| [references/llm-integration-patterns.md](references/llm-integration-patterns.md) | Embedding TOON in prompts, streaming, validating model output |
