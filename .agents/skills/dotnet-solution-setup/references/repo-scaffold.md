# Repo Scaffold — .slnx, .editorconfig, global.json & Friends

## .slnx Solution File

- `dotnet new sln` creates `.slnx` by default since .NET 10 SDK (available since .NET SDK 9.0.200 via `dotnet new sln --format slnx`).
- Migrate an existing `.sln`: `dotnet sln migrate` — writes the new `.slnx`, leaves the `.sln` in place (delete it once verified).
- `dotnet build|publish|restore|format` and Visual Studio 17.12+ / MSBuild all accept `.slnx` directly — no extra tooling.
- `Directory.Solution.props`/`.targets` and solution filters (`.slnf`) both work with `.slnx`; when both a `.slnx` and `.slnf` exist, the `.slnx` takes priority.
- Plain, diff-friendly XML — no more GUID churn or merge conflicts from Visual Studio re-serializing a `.sln`.

```xml
<!-- Example: MySolution.slnx -->
<Solution>
  <Folder Name="/src/">
    <Project Path="src/MyApp/MyApp.csproj" />
  </Folder>
  <Folder Name="/tests/">
    <Project Path="tests/MyApp.Tests/MyApp.Tests.csproj" />
  </Folder>
</Solution>
```

## .editorconfig

- Scaffold: `dotnet new editorconfig` (or `--empty` for a blank file).
- `root = true` at the top stops EditorConfig from searching parent directories.
- Pair every style rule (`dotnet_style_*`, `csharp_style_*`) with a `severity` so `EnforceCodeStyleInBuild` can fail the build, not just flag in the IDE:

```ini
root = true

[*.cs]
dotnet_style_qualification_for_field = false:warning
csharp_style_namespace_declarations = file_scoped:error
csharp_new_line_before_open_brace = none

[*.{csproj,props,targets}]
indent_size = 2
```

- Keep repo-wide rules at the root; add a nested `.editorconfig` only for folder-specific overrides (rare — prefer one source of truth).

## global.json — Pin the SDK

```json
{
  "sdk": {
    "version": "10.0.100",
    "rollForward": "latestMinor"
  }
}
```

- Prevents "works on my machine" drift when contributors have different SDKs installed.
- `rollForward: latestMinor` tolerates patch/minor SDK updates but locks the major version — safest default for a team repo.

## .gitattributes — Deterministic Line Endings

```gitattributes
* text=auto eol=lf
*.cs text eol=lf
*.sln text eol=crlf
*.slnx text eol=lf
```

Prevents CRLF/LF churn across contributor OSes, which otherwise pollutes diffs and can break `Deterministic`/SourceLink hashing consistency.

## .config/dotnet-tools.json — Local Tool Manifest

```bash
dotnet new tool-manifest
dotnet tool install dotnet-format
```

Commit `.config/dotnet-tools.json` so `dotnet tool restore` gives every contributor (and CI) the same tool versions instead of relying on global installs.

## CI: Enforce Formatting & Style

```bash
dotnet tool restore
dotnet format --verify-no-changes
dotnet build -warnaserror
```

`dotnet format --verify-no-changes` fails the pipeline on any style violation `EnforceCodeStyleInBuild` didn't already catch (e.g. whitespace-only `IDE` rules some editors auto-fix locally but forget to commit).
