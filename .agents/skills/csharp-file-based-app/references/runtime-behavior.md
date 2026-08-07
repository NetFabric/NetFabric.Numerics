# Runtime Behavior Reference

## Shell Execution

Enable direct execution on Unix-like systems via shebang + executable bit.

```csharp
#!/usr/bin/env -S dotnet --
#:package Spectre.Console

using Spectre.Console;

AnsiConsole.MarkupLine("[green]Hello, World![/]");
```

```bash
chmod +x file.cs
./file.cs
```

- `-S` lets `env` split the shebang into separate args so `--` can be included; `--` tells `dotnet` to forward all remaining args to the app instead of parsing them as its own flags.
- If `-S` isn't supported, fall back to `#!/usr/bin/env dotnet` — but then `dotnet` may consume args that collide with its own CLI parameters (e.g. `--help`).
- Use `LF` line endings, no BOM.

## User Secrets

File-based apps get a stable user-secrets ID hashed from the full file path — no `UserSecretsId` property needed.

```bash
dotnet user-secrets set "ApiKey" "your-secret-value" --file file.cs
dotnet user-secrets list --file file.cs
```

`list` prints secret values in plain text — don't run it in scripts executed in public/CI logs.

## Launch Profiles

Flat launch settings file named `<AppName>.run.json`, next to the `.cs` file, instead of `Properties/launchSettings.json`:

```json
{
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5000",
      "environmentVariables": { "ASPNETCORE_ENVIRONMENT": "Development" }
    }
  }
}
```

Multiple apps in one directory each get their own file:

```text
📁 myapps/
├── foo.cs
├── foo.run.json
├── bar.cs
└── bar.run.json
```

Profile selection order: `--launch-profile` flag → `DOTNET_LAUNCH_PROFILE` env var → first profile in the file.

```bash
dotnet run app.cs --launch-profile https
```

If both `<AppName>.run.json` and `Properties/launchSettings.json` exist, the traditional location wins; the CLI logs a warning.

## Build Caching

The SDK caches build output keyed on: source file content, directive configuration, SDK version, and implicit build file existence/content.

Gotchas:
- Changes to implicit build files (`Directory.Build.props`, etc.) may not trigger a rebuild.
- Moving a file to a different directory doesn't invalidate its cache entry.
- Running multiple instances of the same file-based app concurrently can race over shared build output — `dotnet build file.cs` once first, then start concurrent instances with `dotnet run file.cs --no-build`.

Workarounds:

```bash
dotnet clean file-based-apps            # purge all cached artifacts (default: unused 30+ days)
dotnet clean file-based-apps --days 7   # custom retention window
dotnet clean file.cs && dotnet build file.cs   # force a clean rebuild for one app
```

## Implicit Build Files

File-based apps respect MSBuild/NuGet files found in the same or parent directories:

| File | Effect |
|------|--------|
| `Directory.Build.props` | Shared MSBuild properties |
| `Directory.Build.targets` | Custom build targets, executed during build |
| `Directory.Packages.props` | Central package management |
| `nuget.config` | Package sources |
| `global.json` | Pins the SDK version used |

## Folder Layout Recommendations

**Avoid project file cones** — don't put file-based apps inside a `.csproj`'s directory tree; its implicit settings can interfere.

```text
❌ 📁 MyProject/
   ├── MyProject.csproj
   ├── Program.cs
   └── 📁 scripts/
       └── utility.cs   # bad: inside the project cone

✅ 📁 MyProject/
   ├── MyProject.csproj
   └── Program.cs
   📁 scripts/
   └── utility.cs        # good: sibling, outside the cone
```

**Isolate from ambient implicit files** — a repo-root `Directory.Build.props` affects every file-based app beneath it; give scripts their own directory (and its own `Directory.Build.props`) when they need different settings.

```text
❌ 📁 repo/
   ├── Directory.Build.props   # affects everything below
   ├── app1.cs
   └── app2.cs

✅ 📁 repo/
   ├── Directory.Build.props
   ├── 📁 projects/
   │   └── MyProject.csproj
   └── 📁 scripts/
       ├── Directory.Build.props   # isolated
       ├── app1.cs
       └── app2.cs
```

## Default Included Items

- The single `.cs` file is always included.
- `Microsoft.NET.Sdk.Web` also includes `*.json` config files.
- Non-default SDKs may include ResX resource files.
