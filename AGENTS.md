# DotnetDevKR.TailwindCSS

## Purpose

A .NET MSBuild integration package that enables TailwindCSS compilation during the build process without requiring Node.js. This solution packages TailwindCSS standalone CLI executables for cross-platform support and integrates them seamlessly into the .NET build pipeline.

## Key Files

- `DotnetDevKR.TailwindCSS.sln` - Visual Studio solution file containing all projects
- `DotnetDevKR.TailwindCSS.slnx` - Solution explorer file
- `README.md` - Project documentation with installation and usage instructions
- `LICENSE.md` - Mozilla Public License 2.0
- `nuget.config` - NuGet package source configuration
- `.editorconfig` - Editor formatting standards

## Subdirectories

- `.github/` - GitHub automation (see [.github/AGENTS.md](.github/AGENTS.md))
- `DotnetDevKR.TailwindCSS/` - Main NuGet package with MSBuild task (see [DotnetDevKR.TailwindCSS/AGENTS.md](DotnetDevKR.TailwindCSS/AGENTS.md))
- `DotnetDevKR.TailwindCSS.Compile/` - TailwindCSS compiler utility (see [DotnetDevKR.TailwindCSS.Compile/AGENTS.md](DotnetDevKR.TailwindCSS.Compile/AGENTS.md))
- `DotnetDevKR.TailwindCSS.WebTest/` - Blazor demo application (see [DotnetDevKR.TailwindCSS.WebTest/AGENTS.md](DotnetDevKR.TailwindCSS.WebTest/AGENTS.md))

## For AI Agents

### Project Architecture

This is a three-project solution:
1. **Main Package** (`DotnetDevKR.TailwindCSS`) - Distributes as a NuGet package, provides MSBuild integration
2. **Compiler Library** (`DotnetDevKR.TailwindCSS.Compile`) - Core compilation logic using standalone TailwindCSS CLI
3. **Demo App** (`DotnetDevKR.TailwindCSS.WebTest`) - Blazor WebAssembly reference implementation

### Versioning Scheme

The package uses a 4-part version: `{TailwindMajor}.{TailwindMinor}.{TailwindPatch}.{Revision}`
- Example: `4.1.17.0` means TailwindCSS v4.1.17, first package revision

### Build Commands

```bash
# Build entire solution
dotnet build DotnetDevKR.TailwindCSS.sln

# Run the demo app
dotnet run --project DotnetDevKR.TailwindCSS.WebTest

# Pack NuGet package
dotnet pack ./DotnetDevKR.TailwindCSS/DotnetDevKR.TailwindCSS.csproj --configuration Release
```

### Important Implementation Details

- Uses standalone TailwindCSS CLI (no Node.js dependency)
- Supports Windows (x64/arm64), Linux (x64/arm64/armv7), macOS (x64/arm64)
- Integrates via MSBuild task that runs `BeforeTargets="Build"`
- TailwindCSS executables are downloaded by `install.sh` during CI/CD

### Common Tasks

| Task | Location |
|------|----------|
| Modify MSBuild integration | `DotnetDevKR.TailwindCSS/build/*.targets` |
| Update compiler logic | `DotnetDevKR.TailwindCSS.Compile/TailwindCSSCompiler.cs` |
| Add platform support | `TailwindCSSCompiler.GetExecutablePath()` |
| Modify CI/CD | `.github/workflows/tailwindcss-auto-update.yaml` |

## Dependencies

- .NET 6.0+ SDK
- TailwindCSS standalone CLI (bundled in package)
- Microsoft.Build.Utilities.Core for MSBuild task implementation
