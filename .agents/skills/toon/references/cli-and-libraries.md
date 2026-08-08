# CLI & Libraries

Every conversion here delegates to the official implementation — no custom parsing.

## CLI

`@toon-format/cli` (TypeScript-based, works on any host with Node):

```bash
npx @toon-format/cli input.json -o output.toon   # encode (auto-detected by extension)
npx @toon-format/cli input.toon -o output.json    # decode
npm install -g @toon-format/cli && toon input.json -o output.toon   # global install
cat data.json | toon --stats                      # stdin, with token-savings report
```

| Option | Effect |
|--------|--------|
| `-o, --output <file>` | Output path (stdout if omitted) |
| `-e, --encode` / `-d, --decode` | Force mode (needed for stdin, which defaults to encode) |
| `--delimiter <char>` | `,` (default), tab (`$'\t'` in bash/zsh), or `\|` |
| `--indent <n>` | Spaces per level (default 2) |
| `--stats` | Token-count estimate + savings (encode only; builds full string, costs memory) |
| `--no-strict` | Relax decode validation (counts, indentation, delimiter); last-write-wins on dup keys |
| `--verbose` | Full stack traces on error |

Streams both directions — safe for large files/pipes. Exit code `1` on error, with a caret pointing at the offending column.

## Python

`toon-format` on PyPI, or `pip install git+https://github.com/toon-format/toon-python.git` (beta, API may still shift before 1.0).

```python
from toon_format import encode, decode, estimate_savings, count_tokens

encode({"users": [{"id": 1, "name": "Alice"}, {"id": 2, "name": "Bob"}]})
# users[2]{id,name}:
#   1,Alice
#   2,Bob

decode("items[2]: apple,banana")   # {'items': ['apple', 'banana']}

estimate_savings(data)             # {'savings_percent': 42.3, ...}
count_tokens(toon_str)             # requires `tiktoken` extra
```

`encode(value, {"delimiter": "\t", "indent": 4, "lengthMarker": "#"})`; `decode(s, {"indent": 2, "strict": True})`. CLI entry point: `toon input.json -o output.toon` (same flags as the npm CLI, kebab-cased). Optional `pip install "toon-format[pydantic]"` adds `ToonPydanticModel` (`schema_to_toon()`, `model_validate_toon()`, `model_dump_toon()`).

## Java

Maven Central `dev.toonformat:jtoon` (100% spec v3.0 compliant):

```java
import dev.toonformat.jtoon.JToon;

JToon.encode(data);                          // Object -> TOON
JToon.encode(data, new EncodeOptions(2, Delimiter.TAB, false, KeyFolding.OFF, 3));
JToon.encodeJson(jsonString);                 // JSON string -> TOON
JToon.decode(toon);                           // TOON -> Object (Map/List/primitives)
JToon.decodeToJson(toon);                     // TOON -> JSON string
```

`EncodeOptions(indent, delimiter, lengthMarker, keyFolding, flattenDepth)`; `DecodeOptions(indent, delimiter, strict)`. `@JsonIgnore` (Jackson) excludes fields from encoding.

## Rust

crates.io `toon-format` (as library: `cargo add toon-format`; as CLI: `cargo install toon-format`, binary name `toon`):

```rust
use toon_format::{encode_default, decode_default, encode, EncodeOptions, Delimiter};
use serde_json::json;

let toon = encode_default(&json!({"users":[{"id":1,"name":"Alice"}]}))?;
let opts = EncodeOptions::new().with_delimiter(Delimiter::Pipe);
let toon = encode(&data, &opts)?;
let value: serde_json::Value = decode_default(&toon)?;
```

Works with any `Serialize`/`Deserialize` type, not just `serde_json::Value`. `toon --interactive` (or `-i`) launches a TUI with live conversion, diffs, and round-trip testing. Optional `layout` cargo feature exposes decoder layout metadata (tabular/list/inline, declared lengths) for building validators/formatters — experimental, not part of the spec.

## .NET

NuGet `Toon.Format` (.NET Standard 2.0, .NET 8/9/10):

```csharp
using Toon.Format;

var toon = ToonEncoder.Encode(data);
var toon = ToonEncoder.Encode(data, new ToonEncodeOptions { Delimiter = ToonDelimiter.TAB });
var node = ToonDecoder.Decode(toon);          // JsonNode
var typed = ToonDecoder.Decode<MyType>(toon); // strongly typed
```

`ToonEncodeOptions { Indent, Delimiter, KeyFolding }`; `ToonDecodeOptions { Indent, Strict, ExpandPaths }`.

## Dart

`toon_format` on pub.dev is currently a **namespace reservation only** — no working encoder/decoder yet. Don't depend on it; use the CLI or another language's library/service instead until it ships.

## Type Normalization (all libraries)

| JSON-incompatible value | Becomes |
|---|---|
| `NaN`, `±Infinity` | `null` |
| Decimal/BigDecimal/BigInteger (in range) | canonical decimal number |
| Date/time types (`DateTime`, `LocalDate`, `Instant`, …) | quoted ISO 8601 string |
| `Optional<T>` / nullable | unwrapped value or `null` |
| `Map`/dictionary | object with string keys |
| `Set`, `Stream`, other collections | array |
| `-0` | `0` |

## Choosing a Delimiter

Comma (default) is safest for natural-language strings. Tab or pipe reduce tokens further on wide tabular data and need less quote-escaping — but require declaring it in the prompt ("fields are tab-separated") when the model must read or produce the output. See [llm-integration-patterns.md](llm-integration-patterns.md).
