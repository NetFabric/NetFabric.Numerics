# Central Package Management (CPM) — Full Reference

## Enabling

Create `Directory.Packages.props` at the repo root:

```xml
<Project>
  <PropertyGroup>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
    <CentralPackageTransitivePinningEnabled>true</CentralPackageTransitivePinningEnabled>
  </PropertyGroup>

  <ItemGroup>
    <PackageVersion Include="Microsoft.Extensions.Configuration" Version="10.0.0" />
    <PackageVersion Include="xunit" Version="2.9.2" />
  </ItemGroup>

  <!-- Applies to every project automatically; use for build-time-only deps -->
  <ItemGroup>
    <GlobalPackageReference Include="Microsoft.SourceLink.GitHub" Version="8.0.0" />
    <GlobalPackageReference Include="Nerdbank.GitVersioning" Version="3.6.*" />
  </ItemGroup>
</Project>
```

Each project's `.csproj` then omits the version:

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.Extensions.Configuration" />
</ItemGroup>
```

Auto-generate the skeleton: `dotnet new packagesprops`.

## Key Behaviors

| Feature | Property | Notes |
|---------|----------|-------|
| Central versions | `ManagePackageVersionsCentrally=true` | required; NuGet auto-imports `Directory.Packages.props` if present (since .NET 5) even without this flag — rename the file if you don't want that |
| Transitive pinning | `CentralPackageTransitivePinningEnabled=true` | promotes a transitive dependency to top-level when you pin a higher version than requested; downgrades below the requested version raise `NU1109` |
| Per-project override | `<PackageReference Include="X" VersionOverride="1.2.3" />` | escape hatch for one project; disable repo-wide via `CentralPackageVersionOverrideEnabled=false` |
| Global references | `<GlobalPackageReference>` in `Directory.Packages.props` | added to every project with `PrivateAssets=All` implicitly — use for analyzers, SourceLink, versioning tools, never for runtime libraries |
| Per-TFM versions | MSBuild `Condition` on `<PackageVersion Update="X" Condition="...">` | needed when a package drops support for an older TFM your repo still targets |

## Multiple `Directory.Packages.props` Files

Only the **closest** file to a project is auto-imported. A nested one does *not* inherit the root file's `<PackageVersion>` entries unless you import it explicitly:

```xml
<Project>
  <Import Project="$([MSBuild]::GetPathOfFileAbove(Directory.Packages.props, $(MSBuildThisFileDirectory)..))" />
  <ItemGroup>
    <PackageVersion Update="Newtonsoft.Json" Version="13.0.3" />
  </ItemGroup>
</Project>
```

Prefer a single root-level file for a mono-repo unless different solutions genuinely need different dependency graphs.

## NU1507 — Multiple Package Sources

CPM warns if `NuGet.config` defines more than one package source, because it can't tell which source should resolve which package. Fix with [package source mapping](https://aka.ms/nuget-package-source-mapping):

```xml
<!-- NuGet.config -->
<configuration>
  <packageSourceMapping>
    <packageSource key="nuget.org">
      <package pattern="*" />
    </packageSource>
  </packageSourceMapping>
</configuration>
```

Or reduce to a single source if internal feeds aren't in use.

## Disabling for One Project

```xml
<PropertyGroup>
  <ManagePackageVersionsCentrally>false</ManagePackageVersionsCentrally>
</PropertyGroup>
```

Use sparingly — defeats the purpose of a repo-wide single source of truth for versions.
