<!-- Parent: ../AGENTS.md -->
# DotnetDevKR.TailwindCSS.WebTest/Pages

## Purpose

Blazor routable page components for the demo application. Contains the main pages that demonstrate TailwindCSS usage.

## Key Files

- `Home.razor` - Home page component (route: `/`)
  - Entry point for the demo application
  - Should demonstrate TailwindCSS utility classes

## Subdirectories

None.

## For AI Agents

### Page Routing

Blazor pages use the `@page` directive for routing:
```razor
@page "/"
```

### Using TailwindCSS

When adding TailwindCSS classes to pages:
1. Add utility classes directly to elements: `<div class="flex items-center p-4">`
2. Build the project to trigger CSS compilation
3. Changes picked up automatically (file watching enabled)

### Testing CSS Changes

If TailwindCSS classes aren't applying:
1. Verify build completed without errors
2. Check `wwwroot/css/app.css` was generated
3. Ensure `index.html` includes the CSS file
4. Check browser dev tools for class presence

## Dependencies

- Blazor component framework
- Layout components from `../Layout/`
