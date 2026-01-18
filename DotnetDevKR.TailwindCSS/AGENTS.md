<!-- Parent: ../AGENTS.md -->
# DotnetDevKR.TailwindCSS

## Purpose

The main NuGet package project that provides MSBuild integration for TailwindCSS compilation. This is the package users install via `dotnet add package DotnetDevKR.TailwindCSS`.

## Key Files

- `DotnetDevKR.TailwindCSS.csproj` - NuGet package project definition
  - Targets .NET 6.0
  - References `Microsoft.Build.Utilities.Core` for MSBuild task support
  - Includes `DotnetDevKR.TailwindCSS.Compile` as a bundled dependency
  - Packages build targets, props, and runtime executables
- `TailwindCSSTask.cs` - MSBuild task implementation
  - Inherits from `Microsoft.Build.Utilities.Task`
  - Properties: `InputFilename`, `OutputFilename`, `ProjectDir`, `IsMinify`, `DebugMode`
  - Delegates compilation to `TailwindCSSCompiler`

## Subdirectories

- `build/` - MSBuild integration files (see [build/AGENTS.md](build/AGENTS.md))
- `runtime/` - TailwindCSS executables and installer (see [runtime/AGENTS.md](runtime/AGENTS.md))

## For AI Agents

### Package Structure

When packed, the NuGet package contains:
```
lib/
  net6.0/
    DotnetDevKR.TailwindCSS.dll
    DotnetDevKR.TailwindCSS.Compile.dll
build/
  DotnetDevKR.TailwindCSS.props
  DotnetDevKR.TailwindCSS.targets
  runtime/
    tailwindcss-* (platform executables)
README.md
```

### MSBuild Integration

The package uses standard NuGet MSBuild integration:
1. `.props` file is imported at the start of the consuming project
2. `.targets` file is imported at the end
3. `RunTailwindCSSTask` target runs before `Build`

### TailwindCSSTask Properties

| Property | Required | Description |
|----------|----------|-------------|
| `OutputFilename` | Yes | Where to write compiled CSS |
| `InputFilename` | No | Source CSS file |
| `ProjectDir` | No | Working directory for TailwindCSS |
| `IsMinify` | No | Enable minification |
| `DebugMode` | No | Generate source maps |

### Modifying Build Logic

1. Task properties → `TailwindCSSTask.cs`
2. When task runs → `build/DotnetDevKR.TailwindCSS.targets`
3. Default configurations → `build/DotnetDevKR.TailwindCSS.props`

## Dependencies

- `Microsoft.Build.Utilities.Core` (v15.9.20) - MSBuild task base class
- `DotnetDevKR.TailwindCSS.Compile` - Internal compiler library
