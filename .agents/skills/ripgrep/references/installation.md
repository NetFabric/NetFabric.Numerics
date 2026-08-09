# Installation

## Check First

```bash
command -v rg
rg --version
```

`command -v rg` succeeds only when the `rg` executable is discoverable through `PATH`. The second command confirms that it runs and reports its version and enabled features.

## Install

Prefer the platform package manager. Do not install when `rg --version` already succeeds unless the user asks for an upgrade.

| Platform | Command |
| --- | --- |
| macOS / Linux with Homebrew | `brew install ripgrep` |
| macOS with MacPorts | `sudo port install ripgrep` |
| Debian / Ubuntu | `sudo apt-get install ripgrep` |
| Fedora | `sudo dnf install ripgrep` |
| Arch Linux | `sudo pacman -S ripgrep` |
| openSUSE | `sudo zypper install ripgrep` |
| Windows with WinGet | `winget install BurntSushi.ripgrep.MSVC` |
| Windows with Chocolatey | `choco install ripgrep` |
| Windows with Scoop | `scoop install ripgrep` |
| Rust toolchain | `cargo install ripgrep` |

Use a precompiled archive from the [official releases](https://github.com/BurntSushi/ripgrep/releases) when no listed package manager is available. Prefer a system package over compiling with Cargo unless a source build is required.

Commands requiring elevated privileges may prompt for credentials. Ask the user to run those commands directly when the environment cannot handle an interactive privilege prompt.

## Verify

```bash
command -v rg && rg --version
rg -F 'verification text' path/to/text-file
```

If installation succeeded but discovery fails, open a new shell or add the package manager's binary directory to `PATH`, then rerun both checks.

## Sources

- [Official ripgrep repository and installation guide](https://github.com/BurntSushi/ripgrep#installation)
- [Official ripgrep releases](https://github.com/BurntSushi/ripgrep/releases)
