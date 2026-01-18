<!-- Parent: ../AGENTS.md -->
# DotnetDevKR.TailwindCSS.WebTest

## Purpose

A Blazor WebAssembly demonstration application showcasing how to use the DotnetDevKR.TailwindCSS NuGet package. This is a reference implementation for end users.

## Key Files

- `DotnetDevKR.TailwindCSS.WebTest.csproj` - Blazor WebAssembly project
  - Targets .NET 9.0
  - References `DotnetDevKR.TailwindCSS` package (version 0.0.1 - local dev)
  - Configures TailwindCSS compilation via `Configure` target
- `Program.cs` - Application entry point and DI setup
- `App.razor` - Root Blazor component with router
- `_Imports.razor` - Shared Razor imports for all components
- `tailwind.css` - TailwindCSS input file with `@import "tailwindcss"`

## Subdirectories

- `Pages/` - Blazor page components (see [Pages/AGENTS.md](Pages/AGENTS.md))
- `Layout/` - Layout components (see [Layout/AGENTS.md](Layout/AGENTS.md))
- `wwwroot/` - Static web assets (see [wwwroot/AGENTS.md](wwwroot/AGENTS.md))
- `Properties/` - Launch settings

## For AI Agents

### TailwindCSS Configuration

The project demonstrates the required MSBuild configuration:

```xml
<Target Name="Configure" BeforeTargets="RunTailwindCSSTask">
  <PropertyGroup>
    <InputFilename>$(ProjectDir)tailwind.css</InputFilename>
    <OutputFilename>wwwroot\css\app.css</OutputFilename>
    <IsMinify>false</IsMinify>
    <DebugMode>true</DebugMode>
  </PropertyGroup>
</Target>
```

This:
- Takes `tailwind.css` as input
- Outputs to `wwwroot/css/app.css`
- Disables minification for development
- Enables source maps

### Running the Demo

```bash
# Ensure TailwindCSS executables are in runtime folder
cd DotnetDevKR.TailwindCSS/runtime && ./install.sh

# Run the demo app
dotnet run --project DotnetDevKR.TailwindCSS.WebTest
```

### Build Flow

1. MSBuild starts
2. `Configure` target sets TailwindCSS properties
3. `RunTailwindCSSTask` runs before Build
4. `TailwindCSSTask` compiles `tailwind.css` → `wwwroot/css/app.css`
5. Blazor build continues with compiled CSS

### Important Notes

- The project references package version `0.0.1` for local development
- Uses file watching for hot reload support (configured in `.props`)
- Output CSS goes to `wwwroot/css/` for static file serving

## Dependencies

- `Microsoft.AspNetCore.Components.WebAssembly` (v9.0.7)
- `Microsoft.AspNetCore.Components.WebAssembly.DevServer` (v9.0.7)
- `DotnetDevKR.TailwindCSS` (local reference)
