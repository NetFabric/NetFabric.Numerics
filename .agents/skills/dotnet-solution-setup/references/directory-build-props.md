# Directory.Build.props — Full Reference

MSBuild auto-imports `Directory.Build.props` from the project's directory or any ancestor, before the project file's own properties — put it at the repo root (or solution root) so every project inherits it.

## Complete Example

```xml
<Project>

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <LangVersion>latest</LangVersion>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
    <Deterministic>true</Deterministic>

    <!-- Code-quality analyzers (CAxxxx) -->
    <EnableNETAnalyzers>true</EnableNETAnalyzers>
    <AnalysisLevel>latest</AnalysisLevel>
    <AnalysisMode>Recommended</AnalysisMode>

    <!-- Docs & globalization -->
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);CS1591</NoWarn> <!-- missing XML doc comment; drop once docs are complete -->
    <InvariantGlobalization>true</InvariantGlobalization>

    <!-- Repeat repo metadata once instead of per-project -->
    <Authors>Your Org</Authors>
    <Company>Your Org</Company>
    <RepositoryUrl>https://github.com/org/repo</RepositoryUrl>
    <PackageLicenseExpression>MIT</PackageLicenseExpression>
  </PropertyGroup>

  <!-- SourceLink: only meaningful for packable/publishable projects, harmless elsewhere -->
  <PropertyGroup>
    <PublishRepositoryUrl>true</PublishRepositoryUrl>
    <EmbedUntrackedSources>true</EmbedUntrackedSources>
    <DebugType>portable</DebugType>
    <IncludeSymbols>true</IncludeSymbols>
    <SymbolPackageFormat>snupkg</SymbolPackageFormat>
    <!-- Set true only on CI: rewrites local paths to repo-relative ones -->
    <ContinuousIntegrationBuild Condition="'$(CI)' == 'true' OR '$(TF_BUILD)' == 'true'">true</ContinuousIntegrationBuild>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.SourceLink.GitHub" Version="8.0.0" PrivateAssets="All" />
  </ItemGroup>

</Project>
```

## Property Rationale

| Property | Why |
|----------|-----|
| `AnalysisLevel=latest` | opt into newest analyzer rules as the SDK updates, instead of freezing at the SDK's TFM-tied default |
| `AnalysisMode=Recommended` | stricter than `Default`; use `All` for greenfield repos willing to triage every rule |
| `TreatWarningsAsErrors` + `EnforceCodeStyleInBuild` | turns IDE-only suggestions (`IDE0xxx`) and analyzer warnings (`CAxxxx`) into build failures — style drift can't merge |
| `GenerateDocumentationFile` | required for doc-comment analyzers (`CS1591` etc.) and for consumers' IntelliSense |
| `InvariantGlobalization` | smaller, faster apps; opt out (`false`) if the app does culture-sensitive formatting/sorting |
| `DebugType=portable` | cross-platform PDB format required by SourceLink (avoid `full`, which is Windows-only) |
| `ContinuousIntegrationBuild` | must be conditioned on a CI env var — always-true breaks local "Just My Code" debugging and makes local builds non-reproducible in a useful way |

## Test Project Overrides

Test projects shouldn't inherit packable/publish properties. Add a second, narrower props file at `tests/Directory.Build.props` (MSBuild uses the *closest* one, so re-`Import` the root file explicitly):

```xml
<Project>
  <Import Project="$([MSBuild]::GetPathOfFileAbove(Directory.Build.props, $(MSBuildThisFileDirectory)..))" />
  <PropertyGroup>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
    <NoWarn>$(NoWarn);CA1707</NoWarn> <!-- underscores in test method names -->
  </PropertyGroup>
</Project>
```

## Git-Based Versioning

Add a `GlobalPackageReference` (applies to every project without touching each `.csproj`) in `Directory.Packages.props`, not `Directory.Build.props`:

```xml
<ItemGroup>
  <GlobalPackageReference Include="Nerdbank.GitVersioning" Version="3.6.*" />
</ItemGroup>
```

Alternative: `MinVer`. Both derive `Version`/`InformationalVersion` from git tags — avoids hand-editing version numbers per release.

## Deterministic / Reproducible Builds

- `Deterministic=true` is the C# compiler default since .NET SDK — set it explicitly for clarity.
- Starting with .NET 11, `dotnet pack` produces deterministic `.nupkg` files by default; the only variable is file-modification timestamps.
- Pin a reproducible timestamp with `DeterministicTimestamp` (or the `SOURCE_DATE_EPOCH` env var), e.g. from the last commit: `export SOURCE_DATE_EPOCH=$(git log -1 --pretty=%ct)`.
