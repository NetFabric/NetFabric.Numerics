# LLM Integration Patterns

## Why TOON in Prompts

Beyond raw token savings, explicit `[N]` lengths and `{fields}` headers act as structure guardrails — easier for a model to track rows, easier for you to detect truncation when decoding a response.

## Sending TOON as Input

Show the format, don't describe it — models parse it like familiar YAML/CSV once they see one example. Always generate the block with the CLI/library from [cli-and-libraries.md](cli-and-libraries.md), never type it by hand.

````md
Data is in TOON format (2-space indent, arrays show length and fields).

```toon
users[3]{id,name,role,lastLogin}:
  1,Ada,admin,"2025-01-15T10:30:00Z"
  2,Bob,user,"2025-01-14T15:22:00Z"
  3,Cleo,user,"2025-01-13T09:45:00Z"
```

Task: Summarize the user roles and their last activity.
````

- Nested-uniform columns and keyed-tabular maps (feature flags, per-environment config) both stay in tabular form — no extra explanation needed, see [format-spec.md](format-spec.md).
- Full-line `#` comments are stripped by decoders before parsing, so hand-annotating source data (e.g. `# Only active users, exported 2025-01-15`) is safe and survives model output that echoes them back.
- Label the code fence ` ```toon ` or ` ```yaml ` — either renders fine; the structure is what the model reads, not syntax highlighting.

## Requesting TOON as Output

Be explicit when the model must *return* TOON:

- Show the expected header (e.g. `users[N]{id,name,role}:`) so the model fills rows instead of re-deriving keys.
- State the rules: fixed indent size, no trailing spaces, `[N]` must match the row count.
- Ask for "output only the code block" to simplify extraction.

The model may choose *values* (e.g. which rows to keep), but the encoding of those values into TOON syntax is still the model's job only for the output text — treat that text as **untrusted input to decode and validate**, never as the canonical conversion path for your own JSON data. If you already have the data in JSON, encode it yourself via the CLI/library; don't round-trip through a model just to reformat it.

## Validation with Strict Mode

Always decode model-generated TOON with strict mode (the default in every implementation) before trusting it:

```python
from toon_format import decode

try:
    data = decode(model_output, {"strict": True})
except Exception as e:
    # Malformed TOON: count mismatch, bad escape, truncation, etc.
    ...
```

Strict mode checks array-length counts, indentation multiples, and delimiter/escape correctness — exactly the signals you want to catch truncated or hallucinated output. Only relax to `strict: false` for known-good, already-validated data.

## Delimiter Choice for Token Efficiency

Tab (`delimiter: '\t'`) or pipe often tokenize better than comma and need less quote-escaping. Tell the model explicitly ("fields are tab-separated") whenever it must read or produce that delimiter — the header alone (`items[2\t]{...}`) is not always enough context for a model to infer it reliably.

## Streaming Large Datasets

For thousands of records, stream instead of materializing the full string:

- Node/TS: `encodeLines()` / `decodeFromLines()` / `decodeStream()` from `@toon-format/toon`.
- CLI: `toon huge-dataset.json -o output.toon` already streams both directions without full in-memory buffering.
- Rust: enable the `json_stream` cargo feature for progressive `Read -> Write` encoding.

Peak memory scales with data *depth*, not total size — safe for arbitrarily large flat/tabular datasets as long as individual nested structures fit in memory.

## Tips

- Keep prompt examples small (2-5 rows) — models generalize the pattern; large examples burn tokens without improving accuracy.
- Re-validate every model-produced TOON response; don't assume a response that "looks right" decodes cleanly.
- Never skip straight to asking a model "convert this JSON to TOON" as a substitute for running the CLI/library — that reintroduces the exact token cost and error risk TOON is meant to remove.
