# DotnetDevKR.TailwindCSS

[![NuGet Version](https://img.shields.io/nuget/v/DotnetDevKR.TailwindCSS.svg)](https://www.nuget.org/packages/DotnetDevKR.TailwindCSS/)
[![License: MPL 2.0](https://img.shields.io/badge/License-MPL%202.0-brightgreen.svg)](https://opensource.org/licenses/MPL-2.0)

A .NET MSBuild integration package for TailwindCSS that automatically compiles your TailwindCSS files during the build process. This package includes cross-platform TailwindCSS standalone executables and provides seamless integration with .NET projects.

## Features

- 🚀 **Automatic compilation** during MSBuild process
- 📦 **No Node.js dependency** - uses standalone TailwindCSS CLI
- 🔧 **MSBuild integration** with customizable properties

## Installation

Install the NuGet package in your .NET project:

```bash
dotnet add package DotnetDevKR.TailwindCSS
```

## Quick Start

> [!Warning]
> Rebuilding when `dotnet watch` is not working, so If you use `dotnet watch`, hit `Ctrl` + `R`

1. **Install the package** in your project
2. **Create a TailwindCSS input file** (e.g., `tailwind.css`):
   ```css
   @import "tailwindcss";
   ```
3. **Configure MSBuild properties** in your `.csproj` file:
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
4. **Build your project** - TailwindCSS will be compiled automatically!

## Configuration

Configure the TailwindCSS compilation by setting these properties in your project file:

| Property         | Description                               | Default           | Required |
| ---------------- | ----------------------------------------- | ----------------- | -------- |
| `InputFilename`  | Path to the input TailwindCSS file        | -                 | No       |
| `OutputFilename` | Path where the compiled CSS will be saved | -                 | ✅ Yes   |
| `IsMinify`       | Enable CSS minification                   | `false`           | No       |
| `DebugMode`      | Generate source maps                      | `false`           | No       |
| `ProjectDir`     | Project directory for TailwindCSS context | Current directory | No       |

### Example Configuration

```xml
<Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="DotnetDevKR.TailwindCSS" Version="0.0.1" />
  </ItemGroup>

  <Target Name="Configure" BeforeTargets="RunTailwindCSSTask">
    <PropertyGroup>
      <InputFilename>$(ProjectDir)tailwind.css</InputFilename>
      <OutputFilename>wwwroot\css\app.css</OutputFilename>
      <IsMinify Condition="'$(Configuration)' == 'Release'">true</IsMinify>
      <DebugMode Condition="'$(Configuration)' == 'Debug'">true</DebugMode>
    </PropertyGroup>
  </Target>

</Project>
```

## Development Workflow

### For Development

- Set `DebugMode="true"` to generate source maps
- Set `IsMinify="false"` for readable CSS output

### For Production

- Set `IsMinify="true"` to reduce file size
- Set `DebugMode="false"` to disable source maps

## Example Projects

Check out the `DotnetDevKR.TailwindCSS.WebTest` folder for a complete Blazor WebAssembly example that demonstrates:

- Basic TailwindCSS integration
- MSBuild configuration
- File structure organization

## How It Works

1. **MSBuild Integration**: The package registers a build task that runs before the main build
2. **Platform Detection**: Automatically detects your OS and architecture
3. **TailwindCSS Execution**: Runs the appropriate TailwindCSS standalone executable
4. **File Processing**: Compiles your input CSS file and outputs the result

## Requirements

- .NET 6.0 or higher
- Any platform supported by .NET (Windows, macOS, Linux)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the Mozilla Public License 2.0 - see the [LICENSE.md](LICENSE.md) file for details.

## Acknowledgments

- [TailwindCSS](https://tailwindcss.com/) for the amazing CSS framework (MIT LICENSE)
- [TailwindCSS CLI](https://github.com/tailwindlabs/tailwindcss) for the standalone executable
- [AspNetCore.SassCompiler](https://github.com/koenvzeijl/AspNetCore.SassCompiler) – inspiration for CSS compilation and tooling integration

---

Made with ❤️ by [DotnetDevKR](https://github.com/dotnetdev-kr) and [Forum](https://forum.dotnetdev.kr)
