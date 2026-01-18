<!-- Parent: ../AGENTS.md -->
# .github/workflows

## Purpose

GitHub Actions workflow definitions for automated TailwindCSS version tracking and NuGet package publishing.

## Key Files

- `tailwindcss-auto-update.yaml` - Daily automation workflow that:
  - Checks for new TailwindCSS releases via GitHub API
  - Compares against current NuGet package version
  - Downloads new TailwindCSS executables when updates are available
  - Builds, packs, and publishes updated NuGet packages
  - Creates GitHub releases with changelog

## Subdirectories

None.

## For AI Agents

### Workflow Jobs

1. **check-update** - Determines if TailwindCSS has a new version
   - Fetches latest TailwindCSS release from GitHub API
   - Retrieves current package version from NuGet.org API
   - Compares versions and outputs `update_needed` flag

2. **build-and-publish** - Builds and publishes when update needed
   - Updates version in `.csproj` file
   - Downloads TailwindCSS executables via `install.sh`
   - Builds and packs the solution
   - Publishes to NuGet.org and GitHub Packages
   - Creates GitHub release

### Version Calculation

The workflow calculates new versions using the scheme:
```
{TailwindMajor}.{TailwindMinor}.{TailwindPatch}.{Revision}
```

When TailwindCSS updates, revision resets to 1.

### Important Environment Variables

- `NUGET_API_KEY` - NuGet.org publishing key (secret)
- `GITHUB_TOKEN` - GitHub Packages publishing (auto-provided)

### Schedule

Runs daily at 00:00 UTC via cron: `'0 0 * * *'`

Can also be triggered manually via `workflow_dispatch`.

### Modifying the Workflow

- To add new platforms: Update `install.sh` and `TailwindCSSCompiler.GetExecutablePath()`
- To change publishing targets: Modify the `Publish to *` steps
- To adjust version scheme: Update the `Check if update is needed` step

## Dependencies

- GitHub Actions environment
- `curl` for API calls
- .NET SDK 9.x
- Repository secrets: `NUGET_API_KEY`
