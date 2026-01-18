<!-- Parent: ../AGENTS.md -->
# DotnetDevKR.TailwindCSS.Compile

## Purpose

Core TailwindCSS compilation library that wraps the standalone TailwindCSS CLI. This is an internal library bundled with the main NuGet package - not published separately.

## Key Files

- `DotnetDevKR.TailwindCSS.Compile.csproj` - Library project definition
  - Targets .NET 6.0
  - Uses `RootNamespace="DotnetDevKR.TailwindCSS"` (shared namespace)
  - `SuppressDependenciesWhenPacking=true` (no transitive deps)
- `TailwindCSSCompiler.cs` - Core compiler implementation
  - `CompileAsync()` - Main compilation method
  - `CreateTailwindCSSProcess()` - Process setup for TailwindCSS CLI
  - `GetExecutablePath()` - Platform-specific executable resolution

## Subdirectories

None.

## For AI Agents

### Compilation Flow

```
CompileAsync()
    ↓
CreateTailwindCSSProcess()
    ↓ (builds command with arguments)
GetExecutablePath()
    ↓ (resolves OS/arch specific binary)
Process.Start()
    ↓
await WaitForExitAsync()
```

### Platform Support

The `GetExecutablePath()` method supports:

| OS | Architecture | Executable |
|----|--------------|------------|
| Windows | x64 | `runtime\tailwindcss-windows-x64.exe` |
| Windows | arm64 | `runtime\tailwindcss-windows-arm64.exe` |
| Linux | x64 | `runtime/tailwindcss-linux-x64` |
| Linux | arm64 | `runtime/tailwindcss-linux-arm64` |
| macOS | x64 | `runtime/tailwindcss-macos-x64` |
| macOS | arm64 | `runtime/tailwindcss-macos-arm64` |

### TailwindCSS CLI Arguments

The compiler builds arguments like:
```
-i {InputFilename} -o {OutputFilename} --cwd {ProjectDir} [--minify] [--map]
```

- `-i` - Input CSS file
- `-o` - Output CSS file
- `--cwd` - Working directory (for finding tailwind.config.js)
- `--minify` - Enable CSS minification
- `--map` - Generate source maps (debug mode)

### Error Handling

Non-zero exit codes throw exceptions with stderr content. The MSBuild task catches these and reports via `Log.HasLoggedErrors`.

### Adding New Platform Support

1. Add case to `GetExecutablePath()` switch statement
2. Add executable to `install.sh` FILES array
3. Ensure executable is included in package (check `.csproj`)

## Dependencies

- `System.Diagnostics.Process` for CLI invocation
- `System.Runtime.InteropServices.RuntimeInformation` for platform detection
