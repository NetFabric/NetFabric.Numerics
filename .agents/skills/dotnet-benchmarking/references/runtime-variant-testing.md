# Testing Under Environment-Variable Variants

Some behavior (feature flags, hardware-capability toggles, GC modes, ...) only differs when a specific environment variable is set. Cover every relevant variant in unit tests too, not just benchmarks — even code that doesn't read the variable directly may depend on something that does.

## `.runsettings` Files

Create one file per scenario and set variables under `RunConfiguration/EnvironmentVariables`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
    <RunConfiguration>
        <EnvironmentVariables>
            <SOME_FEATURE_FLAG>0</SOME_FEATURE_FLAG>
        </EnvironmentVariables>
    </RunConfiguration>
</RunSettings>
```

Naming convention: prefix files with `_` (e.g. `_Scalar.runsettings`) so they sort together and stand out in a file list among regular source files.

## Visual Studio

`Test > Configure Run Settings > Select Solution Wide runsettings File`, then pick the file for the scenario to run. Visual Studio auto-detects a single `.runsettings` file in the solution but requires manual selection when multiple exist.

## Command Line

`dotnet test` supports environment variables inline or via a settings file — useful for CI pipelines and editors without Visual Studio's run-settings picker:

```shell
# Inline, one variable at a time
dotnet test -e:SOME_FEATURE_FLAG=0

# Via a .runsettings file
dotnet test -s:_Scalar.runsettings
```

`-e`/`--environment` and `-s`/`--settings` can both be repeated/combined as needed.

For the specific `DOTNET_Enable*` variables that control SIMD width (`DOTNET_EnableHWIntrinsic`, `DOTNET_EnableAVX2`, `DOTNET_EnableAVX512`, ...) and the `_Scalar`/`_Vector128`/`_Vector256`/`_Vector512` file set built from them, see the `dotnet-simd` skill's `testing-and-benchmarking.md` reference.
