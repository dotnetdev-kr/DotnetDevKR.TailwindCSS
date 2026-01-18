<!-- Parent: ../AGENTS.md -->
# DotnetDevKR.TailwindCSS.WebTest/Layout

## Purpose

Blazor layout components that define the application's visual structure. These wrap page content and provide consistent navigation and styling.

## Key Files

- `MainLayout.razor` - Primary layout component
  - Wraps page content via `@Body`
  - Provides consistent app structure

- `MainLayout.razor.css` - Scoped CSS for MainLayout
  - CSS isolation (only applies to this component)
  - May contain non-TailwindCSS styles

- `NavMenu.razor` - Navigation menu component
  - Provides site navigation links

- `NavMenu.razor.css` - Scoped CSS for NavMenu
  - CSS isolation for navigation styling

## Subdirectories

None.

## For AI Agents

### Layout Structure

Blazor layouts use `@inherits LayoutComponentBase` and render child content with `@Body`:

```razor
@inherits LayoutComponentBase

<div class="page">
    <NavMenu />
    <main>@Body</main>
</div>
```

### CSS Isolation

Files ending in `.razor.css` are scoped to their component:
- `MainLayout.razor.css` → only applies to `MainLayout.razor`
- At build time, Blazor generates unique attribute selectors

### TailwindCSS Integration

TailwindCSS classes can be used alongside scoped CSS:
- Use TailwindCSS utilities for rapid styling: `class="flex p-4"`
- Use `.razor.css` for complex component-specific styles

### Modifying Layout

1. Edit `.razor` files for structure/markup
2. Edit `.razor.css` files for scoped styles
3. Use TailwindCSS classes inline for utility styles
4. Rebuild to recompile CSS

## Dependencies

- `Microsoft.AspNetCore.Components` for layout base class
- TailwindCSS for utility classes
