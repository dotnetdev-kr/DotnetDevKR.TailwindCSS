<!-- Parent: ../AGENTS.md -->
# DotnetDevKR.TailwindCSS/build

## Purpose

MSBuild integration files that are packaged with the NuGet package. These files are automatically imported by consuming projects to enable TailwindCSS compilation during build.

## Key Files

- `DotnetDevKR.TailwindCSS.props` - MSBuild properties file (imported first)
  - Includes runtime executables as hidden content
  - Sets up file watching for Razor, HTML, CSHTML, and CS files
  - Ensures rebuild triggers on file changes

- `DotnetDevKR.TailwindCSS.targets` - MSBuild targets file (imported last)
  - Defines `RunTailwindCSSTask` target running before `Build`
  - Registers `TailwindCSSTask` via `UsingTask`
  - Passes configuration properties to the task

## Subdirectories

None.

## For AI Agents

### MSBuild Integration Flow

NuGet packages with `build/` content follow these conventions:
1. `{PackageId}.props` imported early (for property definitions)
2. `{PackageId}.targets` imported late (for target definitions)

### Target Definition

```xml
<Target Name="RunTailwindCSSTask"
        BeforeTargets="Build"
        Inputs="@(Watch);$(InputFilename)"
        Outputs="$(OutputFilename)">
```

Key aspects:
- Runs before `Build` target
- Uses incremental build: only runs if inputs changed
- Input files: watched files + input CSS
- Output file: compiled CSS

### File Watching

The `.props` file sets up file watching:
```xml
<Watch Include="**\*.razor" AlwaysRebuild="true" />
<Watch Include="**\*.html" AlwaysRebuild="true" />
<Watch Include="**\*.cshtml" AlwaysRebuild="true" />
<Watch Include="**\*.cs" AlwaysRebuild="true" />
```

This ensures TailwindCSS recompiles when any Razor/HTML/C# file changes (to pick up new utility classes).

### Task Registration

```xml
<UsingTask TaskName="TailwindCSSTask"
           AssemblyFile="$(MSBuildThisFileDirectory)..\lib\net6.0\DotnetDevKR.TailwindCSS.dll" />
```

The task is loaded from the package's `lib/net6.0/` folder.

### Consumer Configuration

Consuming projects should define properties before `RunTailwindCSSTask`:

```xml
<Target Name="Configure" BeforeTargets="RunTailwindCSSTask">
  <PropertyGroup>
    <InputFilename>...</InputFilename>
    <OutputFilename>...</OutputFilename>
    <IsMinify>...</IsMinify>
    <DebugMode>...</DebugMode>
  </PropertyGroup>
</Target>
```

## Dependencies

- MSBuild engine (via `dotnet build` or Visual Studio)
- `DotnetDevKR.TailwindCSS.dll` in package's `lib/` folder
