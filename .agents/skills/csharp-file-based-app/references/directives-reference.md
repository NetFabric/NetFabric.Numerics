# Directives Reference

`#:` directives configure the virtual project the SDK generates. Place them at the top of the file, before any code.

## `#:sdk`

Chooses the target SDK. Default: `Microsoft.NET.Sdk`.

```csharp
#:sdk Microsoft.NET.Sdk.Web
#:sdk Aspire.AppHost.Sdk@13.0.2
```

Version pin uses `@version` suffix, same as `#:package`.

## `#:package`

Adds a NuGet package reference.

```csharp
#:package Newtonsoft.Json
#:package Serilog@3.1.1
#:package Spectre.Console@*
```

- Omitting the version works only with central package management (`Directory.Packages.props`).
- Otherwise pin a version explicitly, or use `@*` for latest.

## `#:project`

References another project file or a directory containing one.

```csharp
#:project ../SharedLibrary/SharedLibrary.csproj
```

## `#:property`

Sets an MSBuild property.

```csharp
#:property TargetFramework=net10.0
#:property PublishAot=false
#:property OutputPath=./output
```

### Conditional property values

Supports MSBuild property functions/expressions for env-var-driven config:

```csharp
// Env var with fallback default
#:property LogLevel=$([MSBuild]::ValueOrDefault('$(LOG_LEVEL)', 'Information'))

// Conditional boolean expression
#:property EnableLogging=$([System.Convert]::ToBoolean($([MSBuild]::ValueOrDefault('$(ENABLE_LOGGING)', 'true'))))
```

Direct `$(VARIABLE_NAME)` references work too, but provide no fallback if unset. See MSBuild [property functions](/en-us/visualstudio/msbuild/property-functions) docs.

## `#:include`

Available in .NET 11 Preview 3 and .NET SDK 10.0.300+. Adds extra files to the compilation/package.

```csharp
#:include helpers.cs
#:include models/customer.cs
#:include shared/**/*.cs
#:include $(MSBuildProjectName).*.cs
```

Item-type mapping by extension:

| Extension | Item type |
|-----------|-----------|
| `*.cs` | `Compile` |
| `*.resx` | `EmbeddedResource` |
| `*.json` | `None` |
| `*.razor` | `Content` |

- Included `.cs` files can add types/methods/namespaces but **cannot** add top-level statements (only the entry file can).
- Supports literal paths, glob patterns, and MSBuild properties.
- Glob patterns **disable build caching** for that app.
