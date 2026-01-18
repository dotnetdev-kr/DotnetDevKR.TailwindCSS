<!-- Parent: ../AGENTS.md -->
# DotnetDevKR.TailwindCSS/runtime

## Purpose

Contains TailwindCSS standalone CLI executables for all supported platforms and the installation script used during CI/CD to download them.

## Key Files

- `install.sh` - Bash script to download TailwindCSS executables
  - Downloads from GitHub releases: `https://github.com/tailwindlabs/tailwindcss/releases/download/{version}/`
  - Supports version parameter: `./install.sh v4.1.17`
  - Makes non-Windows executables executable (`chmod +x`)

### Downloaded Executables (not in git)

| File | Platform |
|------|----------|
| `tailwindcss-linux-x64` | Linux x64 |
| `tailwindcss-linux-arm64` | Linux ARM64 |
| `tailwindcss-linux-armv7` | Linux ARMv7 |
| `tailwindcss-macos-x64` | macOS x64 |
| `tailwindcss-macos-arm64` | macOS ARM64 (Apple Silicon) |
| `tailwindcss-windows-x64.exe` | Windows x64 |
| `tailwindcss-windows-arm64.exe` | Windows ARM64 |

## Subdirectories

None.

## For AI Agents

### Installation Script Usage

```bash
# Download default version
./install.sh

# Download specific version
./install.sh v4.1.17

# During CI/CD (from workflow)
chmod +x ./DotnetDevKR.TailwindCSS/runtime/install.sh
./DotnetDevKR.TailwindCSS/runtime/install.sh v4.1.18
```

### Important Notes

- Executables are NOT committed to git (too large, frequently updated)
- The `install.sh` script is the source of truth for supported platforms
- Executables are downloaded to the same directory as `install.sh`
- TODO in script: SHA256 checksum verification (not yet implemented)

### Package Distribution

When packing:
```xml
<None Include="runtime\**\*" Pack="true" PackagePath="build/runtime" />
```

Executables end up at `build/runtime/` in the NuGet package, and `TailwindCSSCompiler` resolves paths relative to `MSBuildThisFileDirectory`.

### Adding a New Platform

1. Add the filename to the `FILES` array in `install.sh`
2. Add a case to `TailwindCSSCompiler.GetExecutablePath()`
3. Run `./install.sh` to download it
4. Test locally before publishing

### Security Considerations

- Executables are downloaded from official TailwindCSS GitHub releases
- HTTPS is used for all downloads
- Consider implementing checksum verification (noted TODO in script)

## Dependencies

- `curl` for downloading
- `chmod` for making executables (non-Windows)
- Bash shell
